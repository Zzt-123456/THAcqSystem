using MiniExcelLibs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZT.MTHModels
{
    /// <summary>
    /// 通信变量实体类
    /// <para>对应配置文件中的"变量"概念，是 Modbus 数据采集的最小逻辑单位。</para>
    /// <para>业务场景：每个 Variable 隶属于一个 Group（通过 GroupName 关联），定义从该 Group
    /// 已读取的原始数据块中按"起始偏移 + 数据类型 + 位偏移/长度"解析出一个具体变量值，
    /// 并可选地配置线性转换（Scale/Offset）与边沿报警（PosAlarm/NegAlarm）。</para>
    /// <para>实际工程值换算公式：实际值 = 寄存器原始值 × Scale + Offset</para>
    /// <para>持久化：配置项通过 MiniExcel 导入导出；运行时字段（VarValue、PosCacheValue、NegCacheValue）
    /// 标记 [ExcelIgnore] 不参与持久化。</para>
    /// </summary>
    public class Variable
    {
        /// <summary>
        /// 变量名称（全局唯一标识）
        /// <para>同一设备内不可重名，作为 Device.CurrentValue 字典的 Key 使用，界面层据此检索实时值。</para>
        /// <para>取值示例："1号站温度"、"1号站湿度"、"压缩机运行状态"。</para>
        /// </summary>
        public string VarName { get; set; }

        /// <summary>
        /// 起始偏移（相对所属 Group.Start 的偏移量，不是绝对 Modbus 地址）
        /// <para>数据类型：ushort，取值范围 0 ~ 65535。</para>
        /// <para>说明：实际 Modbus 地址 = Group.Start + Variable.Start。例如 Group.Start=0、Variable.Start=2，
        /// 则该变量从第 3 个寄存器（地址 40003）开始读取。</para>
        /// </summary>
        public ushort Start { get; set; }

        /// <summary>
        /// 数据类型（字符串形式，取值参考 Enum.DataType 枚举名）
        /// <para>常见取值："Bool"、"Short"、"UShort"、"Int"、"UInt"、"Float"、"Long"、"ULong"、"String"。</para>
        /// <para>决定从原始字节流中按多少字节、何种编码解析变量值，例如 Float 占 2 个寄存器（4 字节）。</para>
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 位偏移或数据长度（依 DataType 语义不同）
        /// <para>- 当 DataType="Bool" 时：表示线圈/寄存器内的位偏移，取值 0 ~ 15。</para>
        /// <para>- 当 DataType="String" 时：表示字符串占用的寄存器（字）数量。</para>
        /// <para>- 其他数值类型时：通常不使用，由 DataType 自身决定占用长度。</para>
        /// </summary>
        public int OffsetOrLength { get; set; }

        /// <summary>
        /// 所属通信组名称（外键，对应 Group.GroupName）
        /// <para>建立 Variable 与 Group 的多对一关系，配置加载时据此将 Variable 挂载到对应 Group.VarList。</para>
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 备注说明（可选）
        /// <para>工程人员填写的变量用途、物理意义、单位等说明性信息，不参与程序逻辑。</para>
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否启用上升沿报警（Rising Edge Alarm）
        /// <para>true=启用：当变量值由 false 跳变为 true 时触发报警，由 true 跳变为 false 时消除报警。</para>
        /// <para>典型用于"故障发生"型信号（如过温、过压、设备停机等）。</para>
        /// <para>注意：仅对布尔语义变量有效，Device.CheckAlarm 内部会将 VarValue 转换为 bool 后比较。</para>
        /// </summary>
        public bool PosAlarm { get; set; }

        /// <summary>
        /// 是否启用下降沿报警（Falling Edge Alarm）
        /// <para>true=启用：当变量值由 true 跳变为 false 时触发报警，由 false 跳变为 true 时消除报警。</para>
        /// <para>典型用于"信号丢失"型信号（如通信断开、电源掉电、安全回路断开等）。</para>
        /// <para>注意：仅对布尔语义变量有效。</para>
        /// </summary>
        public bool NegAlarm { get; set; }

        /// <summary>
        /// 线性转换系数（Scale）
        /// <para>默认值 1.0f。实际工程值 = 寄存器原始值 × Scale + Offset。</para>
        /// <para>典型场景：寄存器存的是放大 10 倍的温度整数（如 255 表示 25.5℃），则 Scale=0.1。</para>
        /// </summary>
        public float Scale { get; set; } = 1.0f;

        /// <summary>
        /// 线性转换偏移量（Offset）
        /// <para>默认值 0.0f。实际工程值 = 寄存器原始值 × Scale + Offset。</para>
        /// <para>典型场景：传感器零点迁移，如压力变送器 4mA 起始对应 0，需减去偏移量。</para>
        /// </summary>
        public float Offset { get; set; } = 0.0f;

        /// <summary>
        /// 变量当前实时值（运行时字段，非配置项）
        /// <para>通信线程解析并完成线性转换后写入；类型为 object 以兼容 Bool/数值/字符串等多种数据类型。</para>
        /// <para>[ExcelIgnore] 表示不参与 MiniExcel 导入导出。</para>
        /// </summary>
        [ExcelIgnore]
        public object VarValue { get; set; }

        /// <summary>
        /// 上升沿报警检测的上一次状态缓存（运行时字段，非配置项）
        /// <para>默认 false。Device.CheckAlarm 用它保存上一次的布尔值，与本次值比较识别 false→true 跳变。</para>
        /// <para>[ExcelIgnore] 表示不参与 MiniExcel 导入导出。</para>
        /// </summary>
        [ExcelIgnore]
        public bool PosCacheValue { get; set; } = false;

        /// <summary>
        /// 下降沿报警检测的上一次状态缓存（运行时字段，非配置项）
        /// <para>默认 true（与 PosCacheValue 相反，确保首次 true→false 跳变能被识别为下降沿报警）。</para>
        /// <para>Device.CheckAlarm 用它保存上一次的布尔值，与本次值比较识别 true→false 跳变。</para>
        /// <para>[ExcelIgnore] 表示不参与 MiniExcel 导入导出。</para>
        /// </summary>
        [ExcelIgnore]
        public bool NegCacheValue { get; set; } = true;
    }
}
