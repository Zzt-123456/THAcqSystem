using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZZT.MTHHelper
{
    /// <summary>
    /// Modbus TCP 通信类
    /// ==================================================================
    /// 用途：基于 .NET Socket 实现 Modbus TCP 协议的主站（Master/Client）通信能力。
    ///
    /// 核心职责：
    ///   1. 建立 / 断开与 Modbus TCP 从站（Slave/Server，如温湿度采集模块）的 TCP 连接；
    ///   2. 实现常用功能码（FC）：
    ///        01H - Read Coils            读取多个输出线圈（可读写开关量）
    ///        02H - Read Discrete Inputs  读取多个输入线圈（只读开关量）
    ///        03H - Read Holding Registers 读取多个保持寄存器（可读写 16 位寄存器，最常用）
    ///        04H - Read Input Registers   读取多个输入寄存器（只读 16 位寄存器）
    ///        05H - Write Single Coil     写入单个线圈
    ///        06H - Write Single Register 写入单个保持寄存器
    ///        0FH - Write Multiple Coils  写入多个线圈
    ///        10H - Write Multiple Registers 写入多个保持寄存器
    ///   3. 提供通用的报文发送与接收（含超时与互斥控制）能力。
    ///
    /// 使用场景：
    ///   - 本项目中作为多通道温湿度监控的底层通信组件；
    ///   - 上层业务通过 ReadOutputRegisters / PreSetSingleRegister 等方法读写设备寄存器，
    ///     获取温湿度数据、设置报警阈值或下发配方参数。
    ///
    /// Modbus TCP 协议要点：
    ///   1. 通信基于 TCP/IP，默认端口 502；
    ///   2. 报文 = MBAP 头（7 字节）+ PDU（功能码 + 数据）；
    ///      MBAP（Modbus Application Protocol）头结构：
    ///        字节 0-1：Transaction ID  事务标识（用于请求/响应配对，本实现固定为 0x0000）
    ///        字节 2-3：Protocol ID     协议标识（Modbus 协议为 0x0000）
    ///        字节 4-5：Length          后续字节数（含 Unit ID）
    ///        字节 6  ：Unit/Slave ID   从站地址（本实现通过 SlaveId 属性设置，默认 0x01）
    ///   3. 字节序：Modbus 协议规定 16 位及以上数据采用大端序（Big Endian / ABCD）传输，
    ///      即高字节在前、低字节在后。例如 0x1234 在报文中为 0x12 0x34。
    ///      对于 32 位数据（占据 2 个连续寄存器），不同设备可能采用不同字节序：
    ///        ABCD：标准大端序（高字在前，字内高字节在前）
    ///        DCBA：标准小端序（低字在前，字内低字节在前）
    ///        BADC：字内交换的大端序
    ///        CDAB：字交换的大端序
    ///      本类返回原始字节数组，字节序解释由上层调用方根据设备实际情况处理。
    ///
    /// 线程安全：
    ///   - 同一 ModbusTCP 实例的发送/接收操作通过 SimpleHybirdLock 互斥；
    ///   - 多线程并发调用时，会自动排队执行，避免报文错乱。
    /// </summary>
    public class ModbusTCP
    {
        #region 字段与属性

        /// <summary>
        /// 发送超时时间（毫秒）
        /// 控制 Socket.Send 的阻塞等待上限，超过此时间未发送成功将抛出异常。
        /// </summary>
        public int SendTimeOut { get; set; } = 2000;

        /// <summary>
        /// 接收超时时间（毫秒）
        /// 控制 Socket.Receive 的阻塞等待上限，超过此时间未收到数据将抛出异常。
        /// </summary>
        public int ReceiveTimeOut { get; set; } = 2000;

        // 创建一个 Socket 对象
        // 实际的 TCP 通信底层通道，在 Connect 方法中实例化
        private Socket socket;

        /// <summary>
        ///  锁对象
        /// 用于保证同一时刻只有一个线程能执行 SendAndReceive，避免多线程并发导致
        /// 报文交错（例如线程 A 发送的请求被线程 B 当作自己的响应读取）。
        /// </summary>
        private SimpleHybirdLock hybirdLock = new SimpleHybirdLock();

        /// <summary>
        /// 每次接收前延时的时间（毫秒）
        /// 在轮询接收循环中，每次检查缓冲区前的等待时间，避免 CPU 空转占用过高。
        /// </summary>
        public int SleepTime { get; set; } =50;

        /// <summary>
        /// 最大的等待次数
        /// 在未收到任何数据时，最多循环等待的次数。总等待时间 ≈ SleepTime × MaxWaitTimes。
        /// 超过此次数仍未收到数据则判定为超时失败。
        /// </summary>
        public int MaxWaitTimes { get; set; } = 20;

        /// <summary>
        /// 单元标识
        /// Modbus 从站地址，默认 0x01。在多设备串口总线（RTU/ASCII）中用于寻址，
        /// 在 TCP 模式下通常也用作设备逻辑地址。
        /// </summary>
        public byte SlaveId { get; set; } = 0x01;

        #endregion

        #region 建立连接与断开连接

        /// <summary>
        /// 建立连接
        /// 创建 TCP Socket 并连接到目标从站设备。
        /// </summary>
        /// <param name="ip">IP 地址</param>
        /// <param name="port">端口 号</param>
        /// <returns>返回结果（true 表示连接成功，false 表示连接失败）</returns>
        public bool Connect(string ip, int port)
        {
            // Socket 实例化：IPv4 + 流式套接字 + TCP 协议
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 设置发送/接收超时，避免在网络异常时无限阻塞
            this.socket.SendTimeout = SendTimeOut;
            this.socket.ReceiveTimeout = ReceiveTimeOut;

            try
            {
                // 优先尝试将字符串解析为 IPAddress（性能更好，且避免 DNS 解析）
                if (IPAddress.TryParse(ip, out IPAddress ipAddress))
                {
                    this.socket.Connect(ipAddress, port);
                }
                else
                {
                    // 不是合法 IP 字符串时，按主机名方式连接（内部会做 DNS 解析）
                    this.socket.Connect(ip, port);
                }
            }
            catch (Exception)
            {
                // 连接异常（目标不可达、端口未开放、超时等）时返回 false，由上层决定重试或报错
                return false;
            }
            return true;
        }

        /// <summary>
        /// 断开连接
        /// 关闭 Socket 释放底层 TCP 连接资源。
        /// </summary>
        public void DisConnect()
        {
            if (this.socket != null)
            {
                this.socket.Close();
            }
        }

        #endregion

        #region 01H 读取输出线圈

        /// <summary>
        /// 01H 读取输出线圈
        /// ==================================================================
        /// 对应 Modbus 功能码 0x01（Read Coils），读取从站的可读写开关量输出区。
        /// 请求报文结构（共 12 字节）：
        ///   MBAP 头(7) + 功能码(1) + 起始地址(2) + 线圈数量(2)
        /// 响应报文结构：
        ///   MBAP 头(7) + 功能码(1) + 字节计数(1) + 线圈数据(N)
        ///   其中 N = ceil(length / 8)，每个线圈占 1 个 bit，按低位到高位排列。
        /// </summary>
        /// <param name="start">起始线圈地址</param>
        /// <param name="length">线圈长度（要读取的线圈数量）</param>
        /// <returns>返回数据（字节数组，每个 bit 表示一个线圈状态；读取失败返回 null）</returns>
        public byte[] ReadOutputCoils(ushort start, ushort length)
        {
            // 第一步：拼接报文

            // 创建一个 ByteArray 对象（自定义工具类，便于顺序追加字节）
            ByteArray SendCommand = new ByteArray();

            // 报文整体结构：事务处理 + 协议标识 + 长度 + 单元标识 + 功能码 + 起始线圈地址 + 线圈长度

            // 事务处理(0x0000) + 协议标识(0x0000)：本实现固定为 0，不做请求/响应配对
            SendCommand.Add(0x00, 0x00, 0x00, 0x00);

            // 长度(0x0006) + 单元标识(SlaveId) + 功能码(0x01)
            // 长度 = 后续字节数 = 1(单元) + 1(功能码) + 2(地址) + 2(数量) = 6
            SendCommand.Add(0x00, 0x06, SlaveId, 0x01);

            // 起始线圈地址(2 字节，大端序) + 线圈长度(2 字节，大端序)
            SendCommand.Add(start);
            SendCommand.Add(length);

            byte[] receive = null;

            // 计算响应中数据部分的字节数：每 8 个线圈占 1 字节，不足补齐
            int byteLength = length % 8 == 0 ? length / 8 : length / 8 + 1;

            // 第二步、第三步：发送并接收报文
            if (SendAndReceive(SendCommand.Array, ref receive))
            {
                // 第四步：验证报文
                // 响应总长度应为：MBAP 头(7) + 功能码(1) + 字节计数(1) + 数据(byteLength) = 9 + byteLength
                if (receive.Length == 9 + byteLength)
                {
                    // 校验从站地址、功能码、字节计数三项是否与预期一致
                    if (receive[6] == SlaveId && receive[7] == 0x01 && receive[8] == byteLength)
                    {
                        // 第五步：解析报文
                        // 提取数据部分（跳过 MBAP 头 7 字节 + 单元 ID 1 字节 + 功能码 1 字节 + 字节计数 1 字节 = 9 字节）
                        byte[] result = new byte[byteLength];

                        Array.Copy(receive, 9, result, 0, byteLength);

                        return result;
                    }
                }
            }
            return null;
        }
        #endregion

        #region 02H 读取输入线圈

        /// <summary>
        /// 002H 读取输入线圈
        /// ==================================================================
        /// 对应 Modbus 功能码 0x02（Read Discrete Inputs），读取从站的只读开关量输入区。
        /// 报文结构与 01H 完全相同，仅功能码由 0x01 改为 0x02。
        /// 区别在于：输入线圈通常对应物理 DI 通道，只能读取不能写入。
        /// </summary>
        /// <param name="start">起始线圈地址</param>
        /// <param name="length">线圈长度</param>
        /// <returns>返回数据</returns>
        public byte[] ReadInputCoils(ushort start, ushort length)
        {
            // 第一步：拼接报文

            // 创建一个 ByteArray 对象
            ByteArray SendCommand = new ByteArray();

            // 事务处理 + 协议标识 + 长度 + 单元标识 + 功能码 + 起始线圈地址 + 线圈长度

            // 事务处理 + 协议标识
            SendCommand.Add(0x00, 0x00, 0x00, 0x00);

            // 长度 + 单元标识 + 功能码(0x02)
            SendCommand.Add(0x00, 0x06, SlaveId, 0x02);

            // 起始线圈地址 + 线圈长度
            SendCommand.Add(start);
            SendCommand.Add(length);

            byte[] receive = null;

            // 计算响应数据字节数（同 01H 计算）
            int byteLength = length % 8 == 0 ? length / 8 : length / 8 + 1;

            // 第二步、第三步：发送并接收报文
            if (SendAndReceive(SendCommand.Array, ref receive))
            {
                // 第四步：验证报文
                if (receive.Length == 9 + byteLength)
                {
                    // 校验从站地址、功能码（0x02）、字节计数
                    if (receive[6] == SlaveId && receive[7] == 0x02 && receive[8] == byteLength)
                    {
                        // 第五步：解析报文
                        byte[] result = new byte[byteLength];

                        Array.Copy(receive, 9, result, 0, byteLength);

                        return result;
                    }
                }
            }
            return null;
        }
        #endregion

        #region 03H 读取输出寄存器

        /// <summary>
        /// 读取输出寄存器
        /// ==================================================================
        /// 对应 Modbus 功能码 0x03（Read Holding Registers），读取从站的可读写 16 位寄存器区。
        /// 这是温湿度监控中最常用的功能码：温度、湿度、阈值等参数通常存储在保持寄存器中。
        /// 报文结构与 01H 类似，但响应中每个寄存器占 2 字节（而非 8 个线圈占 1 字节）。
        /// </summary>
        /// <param name="start">起始寄存器地址</param>
        /// <param name="length">寄存器长度（要读取的寄存器个数，每个寄存器 2 字节）</param>
        /// <returns>返回字节数组（长度 = length × 2；读取失败返回 null）</returns>
        public byte[] ReadOutputRegisters(ushort start, ushort length)
        {
            // 第一步：拼接报文

            // 创建一个 ByteArray 对象
            ByteArray SendCommand = new ByteArray();

            // 事务处理 + 协议标识 + 长度 + 单元标识 + 功能码 + 起始寄存器地址 + 寄存器长度

            // 事务处理 + 协议标识
            SendCommand.Add(0x00, 0x00, 0x00, 0x00);

            // 长度 + 单元标识 + 功能码(0x03)
            SendCommand.Add(0x00, 0x06, SlaveId, 0x03);

            // 起始寄存器地址 + 寄存器长度
            SendCommand.Add(start);
            SendCommand.Add(length);

            byte[] receive = null;

            // 寄存器数据字节数 = 寄存器个数 × 2（每个寄存器 16 位 = 2 字节）
            int byteLength = length * 2;

            // 第二步、第三步：发送并接收报文
            if (SendAndReceive(SendCommand.Array, ref receive))
            {
                // 第四步：验证报文
                if (receive.Length == 9 + byteLength)
                {
                    // 校验从站地址、功能码（0x03）、字节计数
                    if (receive[6] == SlaveId && receive[7] == 0x03 && receive[8] == byteLength)
                    {
                        // 第五步：解析报文
                        byte[] result = new byte[byteLength];

                        Array.Copy(receive, 9, result, 0, byteLength);

                        return result;
                    }
                }
            }
            return null;
        }

        #endregion

        #region 04H 读取输入寄存器

        /// <summary>
        /// 读取输入寄存器
        /// ==================================================================
        /// 对应 Modbus 功能码 0x04（Read Input Registers），读取从站的只读 16 位寄存器区。
        /// 通常对应模拟量输入（AI）通道，例如传感器采集的原始 AD 值。
        /// 报文结构与 03H 完全相同，仅功能码由 0x03 改为 0x04。
        /// </summary>
        /// <param name="start">起始寄存器地址</param>
        /// <param name="length">寄存器长度</param>
        /// <returns>返回字节数组</returns>
        public byte[] ReadInputRegisters(ushort start, ushort length)
        {
            // 第一步：拼接报文

            // 创建一个 ByteArray 对象
            ByteArray SendCommand = new ByteArray();

            // 事务处理 + 协议标识 + 长度 + 单元标识 + 功能码 + 起始寄存器地址 + 寄存器长度

            // 事务处理 + 协议标识
            SendCommand.Add(0x00, 0x00, 0x00, 0x00);

            // 长度 + 单元标识 + 功能码(0x04)
            SendCommand.Add(0x00, 0x06, SlaveId, 0x04);

            // 起始寄存器地址 + 寄存器长度
            SendCommand.Add(start);
            SendCommand.Add(length);

            byte[] receive = null;

            // 寄存器数据字节数 = 寄存器个数 × 2
            int byteLength = length * 2;

            // 第二步、第三步：发送并接收报文
            if (SendAndReceive(SendCommand.Array, ref receive))
            {
                // 第四步：验证报文
                if (receive.Length == 9 + byteLength)
                {
                    // 校验从站地址、功能码（0x04）、字节计数
                    if (receive[6] == SlaveId && receive[7] == 0x04 && receive[8] == byteLength)
                    {
                        // 第五步：解析报文
                        byte[] result = new byte[byteLength];

                        Array.Copy(receive, 9, result, 0, byteLength);

                        return result;
                    }
                }
            }
            return null;
        }

        #endregion

        #region 05H 预置单个线圈

        /// <summary>
        /// 预置单个线圈
        /// ==================================================================
        /// 对应 Modbus 功能码 0x05（Write Single Coil），向从站写入单个线圈状态。
        /// 请求报文结构（共 12 字节）：
        ///   MBAP 头(7) + 功能码(1) + 线圈地址(2) + 线圈值(2)
        /// 线圈值约定：ON  = 0xFF 0x00；OFF = 0x00 0x00（非 0 即为 ON）
        /// 响应报文：从站原样回显请求报文（12 字节），通过比对即可判断是否成功。
        /// </summary>
        /// <param name="start">线圈地址</param>
        /// <param name="value">线圈值（true=ON, false=OFF）</param>
        /// <returns>返回结果（true 表示写入成功且从站确认；false 表示失败）</returns>
        public bool PreSetSingleCoil(ushort start, bool value)
        {
            // 第一步：拼接报文

            // 创建一个 ByteArray 对象
            ByteArray SendCommand = new ByteArray();

            // 事务处理 + 协议标识 + 长度 + 单元标识 + 功能码 + 线圈地址 + 线圈值（0xFF 0x00 / 0x00 0x00）

            // 事务处理 + 协议标识
            SendCommand.Add(0x00, 0x00, 0x00, 0x00);

            // 长度 + 单元标识 + 功能码(0x05)
            SendCommand.Add(0x00, 0x06, SlaveId, 0x05);

            // 线圈地址
            SendCommand.Add(start);

            // 线圈值：ON 写 0xFF00，OFF 写 0x0000（按 Modbus 协议规定）
            SendCommand.Add(value ? (byte)0xFF : (byte)0x00, 0x00);

            byte[] receive = null;

            // 第二步、第三步：发送并接收报文
            if (SendAndReceive(SendCommand.Array, ref receive))
            {
                // 第四步：验证报文
                // 05H 的响应是请求的原样回显，总长度应为 12 字节
                if (receive.Length == 12)
                {
                    // 通过整段字节比对判断从站是否正确接收并回显
                    return ByteArrayEquals(SendCommand.Array, receive);
                }
            }
            return false;
        }

        #endregion

        #region 06H 预置单个寄存器

        /// <summary>
        /// 预置单个寄存器
        /// ==================================================================
        /// 对应 Modbus 功能码 0x06（Write Single Register），向从站写入单个 16 位保持寄存器。
        /// 请求报文结构（共 12 字节）：
        ///   MBAP 头(7) + 功能码(1) + 寄存器地址(2) + 寄存器值(2)
        /// 响应报文：原样回显请求报文（12 字节）。
        /// 本方法提供三个重载：直接传字节数组、传 short、传 ushort，便于上层使用。
        /// </summary>
        /// <param name="start">寄存器地址</param>
        /// <param name="value">寄存器值（2 字节字节数组，调用方需自行处理字节序）</param>
        /// <returns>返回结果</returns>
        public bool PreSetSingleRegister(ushort start, byte[] value)
        {
            // 第一步：拼接报文

            // 创建一个 ByteArray 对象
            ByteArray SendCommand = new ByteArray();

            // 事务处理 + 协议标识 + 长度 + 单元标识 + 功能码 + 寄存器地址 + 寄存器值

            // 事务处理 + 协议标识
            SendCommand.Add(0x00, 0x00, 0x00, 0x00);

            // 长度 + 单元标识 + 功能码(0x06)
            SendCommand.Add(0x00, 0x06, SlaveId, 0x06);

            // 寄存器地址
            SendCommand.Add(start);

            // 寄存器值（2 字节，调用方应保证大端序）
            SendCommand.Add(value);

            byte[] receive = null;

            // 第二步、第三步：发送并接收报文
            if (SendAndReceive(SendCommand.Array, ref receive))
            {
                // 第四步：验证报文
                // 06H 的响应同样是请求的原样回显，总长度 12 字节
                if (receive.Length == 12)
                {
                    return ByteArrayEquals(SendCommand.Array, receive);
                }
            }
            return false;
        }

        /// <summary>
        /// 预置单个寄存器
        /// 重载：传入 short 类型值，内部转换为 2 字节大端序字节数组后调用核心方法。
        /// 注意：BitConverter.GetBytes 在小端系统上返回小端序，故需 Reverse 后再发送（Modbus 要求大端序）。
        /// </summary>
        /// <param name="start">寄存器地址</param>
        /// <param name="value">Short 类型</param>
        /// <returns>返回结果</returns>
        public bool PreSetSingleRegister(ushort start, short value)
        {
            // BitConverter.GetBytes 在 x86/x64 平台返回小端序字节，Reverse 反转为大端序以符合 Modbus 规范
            return PreSetSingleRegister(start, BitConverter.GetBytes(value).Reverse().ToArray());
        }

        /// <summary>
        /// 预置单个寄存器
        /// 重载：传入 ushort 类型值（最常用，因为寄存器本质是无符号 16 位）。
        /// </summary>
        /// <param name="start">寄存器地址</param>
        /// <param name="value">UShort 类型</param>
        /// <returns>返回结果</returns>
        public bool PreSetSingleRegister(ushort start, ushort value)
        {
            // 同上，BitConverter 默认小端序，反转后转大端序
            return PreSetSingleRegister(start, BitConverter.GetBytes(value).Reverse().ToArray());
        }

        #endregion

        #region 0FH 预置多个线圈

        /// <summary>
        /// 预置多个线圈
        /// ==================================================================
        /// 对应 Modbus 功能码 0x0F（Write Multiple Coils），向从站一次性写入多个线圈。
        /// 请求报文结构（变长）：
        ///   MBAP 头(7) + 功能码(1) + 起始线圈地址(2) + 线圈数量(2) + 字节计数(1) + 字节数据(N)
        /// 响应报文结构（固定 12 字节）：
        ///   MBAP 头(7) + 功能码(1) + 起始线圈地址(2) + 线圈数量(2)
        /// 通过比对响应的前 12 字节（修正长度字段后）来判断是否写入成功。
        /// </summary>
        /// <param name="start">起始线圈地址</param>
        /// <param name="value">写入值（bool 数组，每个元素对应一个线圈状态）</param>
        /// <returns>返回结果</returns>
        public bool PreSetMultiCoils(ushort start, bool[] value)
        {
            // 第一步：拼接报文

            // 创建一个 ByteArray 对象
            ByteArray SendCommand = new ByteArray();

            // 将 bool 数组打包为字节数组（每 8 个 bool 占 1 字节）
            byte[] setArray = GetByteArrayFromBoolArray(value);

            // 事务处理 + 协议标识 + 长度 + 单元标识 + 功能码 + 起始线圈地址 + 线圈数量 + 字节计数 + 字节数据

            // 事务处理 + 协议标识
            SendCommand.Add(0x00, 0x00, 0x00, 0x00);

            // 长度：后续字节数 = 单元标识(1) + 功能码(1) + 起始地址(2) + 线圈数量(2) + 字节计数(1) + 数据(N)
            SendCommand.Add((short)(7 + setArray.Length));

            // 单元标识 + 功能码(0x0F)

            SendCommand.Add(SlaveId, 0x0F);

            // 起始线圈地址
            SendCommand.Add(start);

            // 线圈数量
            SendCommand.Add((short)value.Length);

            // 字节计数（数据部分的字节数）
            SendCommand.Add((byte)setArray.Length);

            // 字节数据（线圈状态打包后的字节数组）
            SendCommand.Add(setArray);

            byte[] receive = null;

            // 第二步、第三步：发送并接收报文
            if (SendAndReceive(SendCommand.Array, ref receive))
            {
                // 第四步：验证报文
                // 构造预期响应：取请求的前 12 字节，将长度字段改为 0x06（响应中无数据部分，长度仅为 6）
                byte[] send = new byte[12];

                Array.Copy(SendCommand.Array, 0, send, 0, 12);

                send[4] = 0x00;
                send[5] = 0x06;

                // 比对响应是否与预期一致
                return ByteArrayEquals(send, receive);
            }
            return false;
        }
        #endregion

        #region 10H 预置多个寄存器

        /// <summary>
        /// 预置多个寄存器
        /// ==================================================================
        /// 对应 Modbus 功能码 0x10（Write Multiple Registers），向从站一次性写入多个连续保持寄存器。
        /// 请求报文结构（变长）：
        ///   MBAP 头(7) + 功能码(1) + 起始寄存器地址(2) + 寄存器数量(2) + 字节计数(1) + 字节数据(N)
        ///   其中 N = 寄存器数量 × 2
        /// 响应报文结构（固定 12 字节）：
        ///   MBAP 头(7) + 功能码(1) + 起始寄存器地址(2) + 寄存器数量(2)
        /// </summary>
        /// <param name="start">起始寄存器地址</param>
        /// <param name="value">写入值（字节数组，长度必须为偶数，每 2 字节代表一个寄存器）</param>
        /// <returns>返回结果</returns>
        public bool PreSetMultiRegisters(ushort start, byte[] values)
        {
            // 第一步：拼接报文

            // 校验入参：不能为空、长度不能为 0、必须为偶数（每个寄存器占 2 字节）
            if (values == null || values.Length == 0 || values.Length % 2 == 1)
            {
                return false;
            }

            // 创建一个 ByteArray 对象
            ByteArray SendCommand = new ByteArray();

            // 事务处理 + 协议标识 + 长度 + 单元标识 + 功能码 + 起始寄存器地址 + 寄存器数量 + 字节计数 + 字节数据

            // 事务处理 + 协议标识
            SendCommand.Add(0x00, 0x00, 0x00, 0x00);

            // 长度：后续字节数 = 单元标识(1) + 功能码(1) + 起始地址(2) + 寄存器数量(2) + 字节计数(1) + 数据(N)
            SendCommand.Add((short)(7 + values.Length));

            // 单元标识 + 功能码(0x10)

            SendCommand.Add(SlaveId, 0x10);

            // 起始寄存器地址
            SendCommand.Add(start);

            // 寄存器数量 = 字节数 / 2
            SendCommand.Add((short)(values.Length / 2));

            // 字节计数
            SendCommand.Add((byte)(values.Length));

            // 字节数据
            SendCommand.Add(values);

            byte[] receive = null;

            // 第二步、第三步：发送并接收报文
            if (SendAndReceive(SendCommand.Array, ref receive))
            {
                // 第四步：验证报文
                // 同 0FH：响应固定 12 字节，构造预期响应（取请求前 12 字节，长度字段改为 0x06）
                byte[] send = new byte[12];

                Array.Copy(SendCommand.Array, 0, send, 0, 12);

                send[4] = 0x00;
                send[5] = 0x06;

                return ByteArrayEquals(send, receive);
            }
            return false;
        }
        #endregion

        #region 通用发送并接收方法

        /// <summary>
        /// 发送并接收方法
        /// ==================================================================
        /// 所有功能码方法的底层通信入口，完成"加锁 → 发送 → 接收 → 解锁"完整流程。
        /// 接收策略说明：
        ///   - 采用"短睡眠 + 轮询"的方式，避免 Receive 长时间阻塞；
        ///   - 每次循环先 Sleep(SleepTime) 让出 CPU，再检查 socket.Available；
        ///   - 当缓冲区有数据时读取并写入 MemoryStream，缓冲区为空时计数；
        ///   - 缓冲区为空但已读取过数据 → 视为接收完成，跳出循环；
        ///   - 缓冲区为空且未读取任何数据，累计等待次数超过 MaxWaitTimes → 超时返回 false。
        /// </summary>
        /// <param name="send">发送报文</param>
        /// <param name="receive">接收报文（使用 ref 以便将结果回传给调用方）</param>
        /// <returns>返回结果（true 表示成功收到响应；false 表示发送失败或接收超时）</returns>
        private bool SendAndReceive(byte[] send, ref byte[] receive)
        {
            // 加锁：保证同一时刻只有一个线程在收发，避免报文交错
            hybirdLock.Enter();

            byte[] buffer = new byte[1024];
            MemoryStream stream = new MemoryStream();

            try
            {
                // 发送报文：将完整的请求字节序列发送给从站
                socket.Send(send, send.Length, SocketFlags.None);

                int timer = 0;

                // 接收循环：持续读取直到数据收完或超时
                while (true)
                {
                    // 让出 CPU 一小段时间，避免空转导致 CPU 占用过高
                    Thread.Sleep(SleepTime);

                    // 判断缓冲区有没有数据
                    if (socket.Available > 0)
                    {
                        // 接收数据并放到 Buffer
                        int count = socket.Receive(buffer, SocketFlags.None);

                        // 将读取的数据放到 Stream 中（用于拼接多次接收的分片）
                        stream.Write(buffer, 0, count);
                    }
                    else
                    {
                        // 缓冲区暂无数据
                        timer++;

                        // 先判断 Stream 有没有数据：若已收到过数据，则认为一帧接收完毕
                        if (stream.Length > 0)
                        {
                            break;
                        }
                        // 超时读取：从未收到数据且等待次数已达上限
                        else if (timer > MaxWaitTimes)
                        {
                            return false;
                        }
                        // 注：此分支与上方 stream.Length > 0 判断重复，理论上不可达，保留以保持原有逻辑
                        else if (stream.Length > 0)
                        {
                            break;
                        }
                    }
                }

                // 将 MemoryStream 转换为数组作为最终响应
                receive = stream.ToArray();
                return true;
            }
            catch (Exception)
            {
                // 任何异常（连接断开、超时等）均视为通信失败
                return false;
            }
            finally
            {
                // 无论成功或失败都必须释放锁，避免死锁
                hybirdLock.Leave();
            }
        }

        #endregion

        #region 数组比较方法

        /// <summary>
        /// 数组比较方法    0x01 0x02   01-02
        /// ==================================================================
        /// 将两个字节数组转换为十六进制字符串后比较，用于校验 Modbus 响应是否符合预期。
        /// 示例：b1 = {0x01, 0x02} → "01-02"；b2 同样则相等返回 true。
        /// </summary>
        /// <param name="b1">字节数组 1</param>
        /// <param name="b2">字节数组 2</param>
        /// <returns>两个数组内容完全相同返回 true，否则 false</returns>
        private bool ByteArrayEquals(byte[] b1, byte[] b2)
        {
            // BitConverter.ToString 输出形如 "01-02-FF-00" 的字符串，可直接用 == 比较
            return BitConverter.ToString(b1) == BitConverter.ToString(b2);
        }

        #endregion

        #region 将布尔数组转换成字节数组

        /// <summary>
        /// 将 bool 数组打包为字节数组
        /// ==================================================================
        /// 用于 0FH（写多个线圈）功能码的请求构造：每 8 个 bool 压缩为 1 字节。
        /// 位排列规则：bool[0] → 最低位 (bit0)，bool[7] → 最高位 (bit7)，符合 Modbus 规范。
        /// 例如：bool[8] = {true, false, true, false, false, false, false, false} → 0x05
        /// </summary>
        /// <param name="value">bool 数组</param>
        /// <returns>打包后的字节数组</returns>
        private byte[] GetByteArrayFromBoolArray(bool[] value)
        {
            // 计算目标字节数：不足 8 的倍数向上取整
            int byteLength = value.Length % 8 == 0 ? value.Length / 8 : value.Length / 8 + 1;

            byte[] result = new byte[byteLength];

            for (int i = 0; i < result.Length; i++)
            {
                // 获取每个字节的值

                // 计算当前字节需要处理多少个 bit：通常是 8，最后一个字节可能不足 8
                int total = value.Length < 8 * (i + 1) ? value.Length - 8 * i : 8;

                // 将每个 bool 设置到对应 bit 位
                for (int j = 0; j < total; j++)
                {
                    result[i] = SetBitValue(result[i], j, value[8 * i + j]);
                }
            }
            return result;
        }

        /// <summary>
        /// 将某个字节某个位置位或复位
        /// </summary>
        /// <param name="src">原始字节</param>
        /// <param name="bit">位索引（0-7，0 表示最低位）</param>
        /// <param name="value">目标值（true=置 1，false=清 0）</param>
        /// <returns>修改后的字节</returns>
        private byte SetBitValue(byte src, int bit, bool value)
        {
            // 置 1：用按位或（|）将对应位设为 1
            // 清 0：用按位与（&）+ 取反（~）将对应位清 0
            return value ? (byte)(src | (byte)Math.Pow(2, bit)) : (byte)(src & ~(byte)Math.Pow(2, bit));
        }
        #endregion
    }

    #region ByteArray
    /// <summary>
    /// ByteArray 工具类，一般用来做报文拼接
    /// ==================================================================
    /// 用途：封装 List&lt;byte&gt;，提供一系列便捷的 Add 重载，用于按顺序构建 Modbus 报文。
    /// 设计动机：
    ///   - Modbus 报文由多种类型（byte、byte[]、short、ushort）顺序拼接而成；
    ///   - 直接用 List&lt;byte&gt; 需要手动处理多字节类型的大小端转换；
    ///   - 本类内置 ushort/short 按大端序拆分的逻辑，使报文构造代码更清晰。
    /// </summary>
    public class ByteArray
    {
        #region 初始化

        // 内部存储：使用 List<byte> 便于动态追加
        private List<byte> list = new List<byte>();

        #endregion

        #region 属性

        /// <summary>
        /// List 集合
        /// </summary>
        public List<byte> List
        {
            get { return list; }
        }

        /// <summary>
        /// Array 数组
        /// 将内部 List 转换为 byte[] 返回，便于直接传给 Socket.Send。
        /// </summary>
        public byte[] Array
        {
            get { return list.ToArray(); }
        }

        /// <summary>
        /// 长度
        /// </summary>
        public int Length
        {
            get { return list.Count; }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 添加一个字节
        /// </summary>
        /// <param name="item">字节值</param>
        public void Add(byte item)
        {
            list.Add(item);
        }

        /// <summary>
        /// 添加一个数组
        /// </summary>
        /// <param name="array">字节数组</param>
        public void Add(byte[] array)
        {
            list.AddRange(array);
        }

        /// <summary>
        /// 添加一个集合
        /// </summary>
        /// <param name="list">字节集合</param>
        public void Add(List<byte> list)
        {
            list.AddRange(list);
        }

        /// <summary>
        /// 连续添加两个字节
        /// </summary>
        /// <param name="item1">第 1 个字节</param>
        /// <param name="item2">第 2 个字节</param>
        public void Add(byte item1, byte item2)
        {
            Add(new byte[] { item1, item2 });
        }

        /// <summary>
        /// 连续添加三个字节
        /// </summary>
        /// <param name="item1">第 1 个字节</param>
        /// <param name="item2">第 2 个字节</param>
        /// <param name="item3">第 3 个字节</param>
        public void Add(byte item1, byte item2, byte item3)
        {
            Add(new byte[] { item1, item2, item3 });
        }


        /// <summary>
        /// 连续添加四个字节
        /// </summary>
        /// <param name="item1">第 1 个字节</param>
        /// <param name="item2">第 2 个字节</param>
        /// <param name="item3">第 3 个字节</param>
        /// <param name="item4">第 4 个字节</param>
        public void Add(byte item1, byte item2, byte item3, byte item4)
        {
            Add(new byte[] { item1, item2, item3, item4 });
        }


        /// <summary>
        /// 连续添加五个字节
        /// </summary>
        /// <param name="item1">第 1 个字节</param>
        /// <param name="item2">第 2 个字节</param>
        /// <param name="item3">第 3 个字节</param>
        /// <param name="item4">第 4 个字节</param>
        /// <param name="item5">第 5 个字节</param>
        public void Add(byte item1, byte item2, byte item3, byte item4, byte item5)
        {
            Add(new byte[] { item1, item2, item3, item4, item5 });
        }

        /// <summary>
        /// 添加一个 ByteArray 对象
        /// </summary>
        /// <param name="byteArray">另一个 ByteArray 实例</param>
        public void Add(ByteArray byteArray)
        {
            Add(byteArray.Array);
        }

        /// <summary>
        /// 添加一个 Short 类型
        /// 按 Modbus 大端序拆分：高字节在前，低字节在后。
        /// 例如：(short)0x1234 → 字节序列 0x12 0x34
        /// </summary>
        /// <param name="value">short 值</param>
        public void Add(short value)
        {
            // 先右移 8 位取高字节，再取低字节
            Add((byte)(value >> 8));
            Add((byte)(value));
        }

        /// <summary>
        /// 添加一个 UShort 类型
        /// 同 Short，按大端序拆分为 2 字节。
        /// </summary>
        /// <param name="value">ushort 值</param>
        public void Add(ushort value)
        {
            Add((byte)(value >> 8));
            Add((byte)(value));
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            list.Clear();
        }
        #endregion
    }

    #endregion

    #region 简单的混合锁
    /// <summary>
    /// 一个简单的混合线程同步锁，采用了基元用户加基元内核同步构造实现
    /// ==================================================================
    /// 用途：在多线程环境下提供轻量级互斥锁，用于 ModbusTCP.SendAndReceive 的串行化。
    ///
    /// 设计原理（混合锁 Hybrid Lock）：
    ///   - 用户模式（user-mode）：通过 Interlocked 原子操作自旋等待，无需进入内核态，性能极高；
    ///   - 内核模式（kernel-mode）：通过 AutoResetEvent 阻塞等待，线程真正挂起，不占 CPU；
    ///   - 混合策略：
    ///       1) 第一个线程获取锁时直接进入（用户模式，无内核开销）；
    ///       2) 出现竞争时，后续线程使用内核模式阻塞（避免自旋浪费 CPU）。
    ///
    /// 为什么不直接用 lock(monitor)？
    ///   - lock 在轻度竞争下也是用户模式自旋 + 内核模式的混合，但行为受 CLR 控制；
    ///   - 本类显式控制两种模式的切换时机，便于在嵌入式 / 工业控制场景下做精细调优。
    /// </summary>

    public sealed class SimpleHybirdLock : IDisposable
    {

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                // 释放内核等待句柄
                m_waiterLock.Close();

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~SimpleHybirdLock() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion

        /// <summary>
        /// 基元用户模式构造同步锁
        /// 表示当前正在等待锁的线程数：0 = 无锁，1 = 持有锁（无竞争），>1 = 持有锁且有竞争
        /// </summary>
        private Int32 m_waiters = 0;

        /// <summary>
        /// 基元内核模式构造同步锁
        /// AutoResetEvent 初始为 false（无信号），竞争线程通过 WaitOne 阻塞，持有者释放时通过 Set 唤醒一个等待者。
        /// </summary>
        private AutoResetEvent m_waiterLock = new AutoResetEvent(false);

        /// <summary>
        /// 获取锁
        /// </summary>
        public void Enter()
        {
            // 原子自增等待计数：
            //   - 若返回 1（即原值为 0），表示当前无竞争，直接进入临界区（用户模式，零内核开销）；
            //   - 若返回 >1，表示已有线程持有锁，本线程需进入内核模式等待。
            if (Interlocked.Increment(ref m_waiters) == 1) return;//用户锁可以使用的时候，直接返回，第一次调用时发生
                                                                  //当发生锁竞争时，使用内核同步构造锁
            // 阻塞当前线程，直到持有者调用 Leave 并 Set 此事件
            m_waiterLock.WaitOne();
        }

        /// <summary>
        /// 离开锁
        /// </summary>
        public void Leave()
        {
            // 原子自减等待计数：
            //   - 若返回 0，表示已无其他等待者，无需唤醒，直接返回；
            //   - 若返回 >0，表示仍有线程在等待，需通过 Set 唤醒一个。
            if (Interlocked.Decrement(ref m_waiters) == 0) return;//没有可用的锁的时候
            // 唤醒一个阻塞在 WaitOne 的线程
            m_waiterLock.Set();
        }

        /// <summary>
        /// 获取当前锁是否在等待当中
        /// </summary>
        public bool IsWaitting => m_waiters == 0;
    }
    #endregion

}
