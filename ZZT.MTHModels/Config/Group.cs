using MiniExcelLibs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZT.MTHModels
{
    /// <summary>
    /// 通信组实体类
    /// <para>对应配置文件中的"通信组"概念，是 Modbus 批量读取的最小调度单位。</para>
    /// <para>业务场景：一台设备下可配置多个 Group，每个 Group 定义一段连续的 Modbus 存储区
    /// （由 StoreArea + Start + Length 共同确定），通信线程按 Group 一次性读取该连续区域，
    /// 再由挂在组下的 Variable 列表按偏移解析出每个变量的实际值。</para>
    /// <para>这种"组读取"方式相比"逐变量读取"大幅减少了 Modbus TCP 请求次数，是性能优化的关键。</para>
    /// <para>持久化：通过 MiniExcel 导出/导入 Excel 配置文件，VarList 标记 [ExcelIgnore] 不参与导出。</para>
    /// </summary>
    public class Group
    {

        /// <summary>
        /// 通信组名称（唯一标识）
        /// <para>同一台设备内不可重名，作为 Variable.GroupName 的外键关联使用。</para>
        /// <para>取值示例："温度组"、"湿度组"、"状态组"。</para>
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 起始地址（Modbus 寄存器/线圈地址）
        /// <para>数据类型：ushort，取值范围 0 ~ 65535。</para>
        /// <para>与 StoreArea 配合定位 Modbus 读取的起始位置，例如 StoreArea="保持寄存器"、Start=0 表示从地址 40001 开始。</para>
        /// </summary>
        public ushort Start { get; set; }

        /// <summary>
        /// 读取长度（连续读取的寄存器或线圈数量）
        /// <para>数据类型：ushort，取值范围 0 ~ 65535，实际受 Modbus 单次读取上限约束（寄存器≤125，线圈≤2000）。</para>
        /// <para>组下所有 Variable 的 Start（组内偏移）+ 占用长度 之和不应超过此 Length，否则越界。</para>
        /// </summary>
        public ushort Length { get; set; }

        /// <summary>
        /// 存储区名称（对应 Modbus 四种存储区之一）
        /// <para>取值参考 Enum.StoreArea 枚举定义，常见值：</para>
        /// <para>  - "输入线圈"（Input Coil / Discrete Input，只读，地址 1xxxx）</para>
        /// <para>  - "输出线圈"（Output Coil / Coil，读写，地址 0xxxx）</para>
        /// <para>  - "输入寄存器"（Input Register，只读，地址 3xxxx）</para>
        /// <para>  - "保持寄存器"（Holding Register / Output Register，读写，地址 4xxxx）</para>
        /// <para>该字段决定通信线程调用 Modbus 的哪个功能码（01/02/03/04）。</para>
        /// </summary>
        public string StoreArea { get; set; }

        /// <summary>
        /// 备注说明（可选）
        /// <para>供工程人员记录该通信组的用途、所属设备区域等说明性信息，不参与程序逻辑。</para>
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 该通信组下挂载的变量集合（运行时构建，非配置项）
        /// <para>通信线程读取到一组原始数据后，遍历 VarList 按各 Variable 的 Start/DataType/OffsetOrLength
        /// 从原始字节数组中解析出变量值。</para>
        /// <para>[ExcelIgnore] 表示该字段不参与 MiniExcel 的导入导出，仅在内存中维护。</para>
        /// </summary>
        [ExcelIgnore]
        public List<Variable> VarList { get; set; }

    }
}
