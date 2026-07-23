using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thinger.DataConvertLib;
using ZZT.MTHHelper;
using ZZT.MTHModels;

namespace ZZT.MTHProject
{
    //全局共享方法类：集中存放整个应用范围内共享的静态字段和方法
    //包括设备对象、日志委托、通信对象、当前用户、大小端格式，以及变量查找、通用写入等工具方法
    internal class CommonMethods
    {
        //全局设备对象：在FrmMain_Load中由LoadDevice加载，包含IP、端口、通信组、变量等所有设备信息
        public static Device Device { get; set; }

        /// <summary>
        /// 全局日志委托：由FrmMonitor窗体的AddLog方法赋值（在OpenForm创建监控窗体时赋值）
        /// 参数1：日志类型(0=正常/消除, 1=报警/触发)；参数2：日志内容
        /// 调用方使用?.Invoke避免在赋值前调用产生空引用
        /// </summary>
        public static Action<int, string> AddLog;

        /// <summary>
        /// 全局ModbusTCP通信对象：在DeviceCommunication中创建并连接，供读写操作使用
        /// </summary>
        public static ModbusTCP Modbus {  get; set; }

        /// <summary>
        /// 当前登录用户对象：登录成功后赋值，承载各功能模块的权限标志位（ParamSet、Recipe等）
        /// </summary>
        public static SysAdmin CurrentAdmin { get; set; }

        /// <summary>
        /// 数据大小端格式：控制多字节类型(Short/Int/Float等)在Modbus通信中的字节排列顺序
        /// 默认ABCD（大端），可配置为BADC/CDBA/DCBA等
        /// </summary>
        public static DataFormat dataFormat = DataFormat.ABCD;

        /// <summary>
        /// 通过变量名(VarName)遍历设备所有通信组查找对应的Variable对象
        /// 用于在写入或按名取值时定位变量
        /// </summary>
        /// <param name="varName">变量名称</param>
        /// <returns>找到的Variable对象，未找到返回null</returns>
        public static Variable FindVariable(string varName)
        {
            //遍历设备所有通信组
            foreach (var item in Device.GroupList)
            {
                //在当前组的变量列表中查找名称匹配的变量
                var res = item.VarList.Find(c => c.VarName == varName);
                if (res != null)
                {
                    return res;
                }
            }
            return null;
        }

        /// <summary>
        /// 通用写入方法：通过变量名写入变量值到设备
        /// 完整4步流程：
        /// 1) 通过变量名查找Variable对象（FindVariable）
        /// 2) 获取变量的数据类型(DataType)
        /// 3) 对写入值进行反向线性转换（将工程值转回原始值，即逆量程变换）
        /// 4) 按数据类型调用对应的Modbus写入方法（单线圈/单寄存器/多寄存器）
        /// </summary>
        /// <param name="varName">变量名称</param>
        /// <param name="varValue">变量值（工程值，字符串形式）</param>
        /// <returns>写入是否成功</returns>
        public static bool CommonWrite(string varName, string varValue)
        {
            var variable = FindVariable(varName);
            //第一步: 先找到变量对象
            if (variable != null)
            {
                //第二步: 获取变量类型
                DataType dataType = (DataType)System.Enum.Parse(typeof(DataType), variable.DataType, true);

                //第三步: 获取写入数据（反向线性转换：工程值 -> 原始值，公式逆运算）
                var result = MigrationLib.SetMigrationValue(varValue, dataType, variable.Scale.ToString(), variable.Offset.ToString());
                if (result.IsSuccess)
                {
                    try
                    {
                        //第四步: 写入数据（按数据类型选择对应的Modbus写入方法）
                        switch (dataType)
                        {
                            case DataType.Bool:
                                //布尔类型：写单个线圈
                                return Modbus.PreSetSingleCoil(variable.Start, Convert.ToBoolean(result.Content));
                            case DataType.Short:
                                //短整型：写单个寄存器
                                return Modbus.PreSetSingleRegister(variable.Start, Convert.ToInt16(result.Content));
                            case DataType.UShort:
                                //无符号短整型：写单个寄存器
                                return Modbus.PreSetSingleRegister(variable.Start, Convert.ToUInt16(result.Content));
                            case DataType.Int:
                                //32位整型：写多个寄存器（2个），按dataFormat排列字节
                                return Modbus.PreSetMultiRegisters(variable.Start, ByteArrayLib.GetByteArrayFromInt(Convert.ToInt32(result.Content), dataFormat));
                            case DataType.UInt:
                                //无符号32位整型：写多个寄存器
                                return Modbus.PreSetMultiRegisters(variable.Start, ByteArrayLib.GetByteArrayFromUInt(Convert.ToUInt32(result.Content), dataFormat));
                            case DataType.Float:
                                //单精度浮点：写多个寄存器
                                return Modbus.PreSetMultiRegisters(variable.Start, ByteArrayLib.GetByteArrayFromFloat(Convert.ToSingle(result.Content), dataFormat));
                            case DataType.Double:
                                //双精度浮点：写多个寄存器（4个）
                                return Modbus.PreSetMultiRegisters(variable.Start, ByteArrayLib.GetByteArrayFromDouble(Convert.ToDouble(result.Content), dataFormat));
                            case DataType.Long:
                                //64位整型：写多个寄存器
                                return Modbus.PreSetMultiRegisters(variable.Start, ByteArrayLib.GetByteArrayFromLong(Convert.ToInt64(result.Content), dataFormat));
                            case DataType.ULong:
                                //无符号64位整型：写多个寄存器
                                return Modbus.PreSetMultiRegisters(variable.Start, ByteArrayLib.GetByteArrayFromULong(Convert.ToUInt64(result.Content), dataFormat));
                            case DataType.String:
                                //字符串：按ASCII编码转字节后写多个寄存器
                                return Modbus.PreSetMultiRegisters(variable.Start, ByteArrayLib.GetByteArrayFromString(result.Content, Encoding.ASCII));
                            case DataType.ByteArray:
                                //字节数组：由十六进制字符串转字节后写入
                                return Modbus.PreSetMultiRegisters(variable.Start, ByteArrayLib.GetByteArrayFromHexString(result.Content));
                            case DataType.HexString:
                                //十六进制字符串：直接转字节写入
                                return Modbus.PreSetMultiRegisters(variable.Start, ByteArrayLib.GetByteArrayFromHexString(result.Content));
                            default:
                                break;
                        }
                    }
                    catch(Exception)
                    {
                        //转换异常时返回写入失败
                        return false;
                    }

                }
            }
            //变量不存在或转换失败，返回写入失败
            return false;
        }
    }
}
