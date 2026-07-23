using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZT.MTHModels
{
    /// <summary>
    /// 系统日志实体类
    /// <para>对应数据库 SysLog 表，统一记录系统运行过程中的报警事件与关键操作日志。</para>
    /// <para>业务场景：</para>
    /// <para>  1. 报警日志：Device.AlarmTrigEvent 触发时，将报警触发/消除事件写入此表，
    ///          记录变量名、报警类型（触发/消除）、发生时间，供"报警追溯"窗体查询；</para>
    /// <para>  2. 操作日志：用户执行配方下发、参数修改、登录登出等关键操作时，记录操作人与操作内容。</para>
    /// <para>设计说明：所有字段均为 string 类型以兼容 SQLite 弱类型存储；InsertTime 为格式化时间字符串。</para>
    /// </summary>
    public class SysLog
    {
        /// <summary>
        /// 记录插入时间（事件发生时刻）
        /// <para>格式通常为 "yyyy-MM-dd HH:mm:ss"，作为报警追溯与日志查询的时间轴依据。</para>
        /// </summary>
        public string InsertTime { get; set; }

        /// <summary>
        /// 日志信息（事件描述/操作内容）
        /// <para>报警日志中为报警说明；操作日志中为具体的操作描述，如"下发配方[标准工艺]"、"用户登录"。</para>
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 报警类型（仅报警日志使用，操作日志可为空）
        /// <para>取值："触发"（报警发生）、"消除"（报警恢复）。</para>
        /// <para>对应 Device.AlarmTrigEvent 的第一个参数：true→"触发"，false→"消除"。</para>
        /// </summary>
        public string AlarmType { get; set; }

        /// <summary>
        /// 操作人员（触发该日志的用户名）
        /// <para>报警日志中可为"系统"（自动触发）；操作日志中为当前登录用户的 LoginName。</para>
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 变量名称（仅报警日志使用，操作日志可为空）
        /// <para>对应 Variable.VarName，标识是哪个变量发生了报警触发/消除。</para>
        /// </summary>
        public string VarName { get; set; }
    }
}
