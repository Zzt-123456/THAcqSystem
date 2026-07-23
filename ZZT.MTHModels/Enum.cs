using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZT.MTHModels
{
    /// <summary>
    /// 枚举定义容器类
    /// <para>集中存放本项目所有业务枚举类型，供各层共用。</para>
    /// <para>注意：此类命名为 Enum 仅作命名空间容器使用，与 System.Enum 区分。</para>
    /// </summary>
    public class Enum
    {
        /// <summary>
        /// 系统窗体名称枚举
        /// <para>对应系统中各功能窗体的标识，主要用于：</para>
        /// <para>  1. 权限控制：SysAdmin 中各权限字段（ParamSet/Recipe/HistoryLog 等）与对应窗体一一映射；</para>
        /// <para>  2. 窗体导航：主界面按枚举值定位并打开对应功能窗体。</para>
        /// </summary>
        public enum FormNames
        {
            /// <summary>
            /// 集中监控窗体：实时展示 6 个站点温湿度、设备状态的主界面
            /// </summary>
            集中监控,
            /// <summary>
            /// 临界窗体：展示接近报警阈值的变量，用于预警提示
            /// </summary>
            临界窗体,
            /// <summary>
            /// 参数设置窗体：配置设备 IP/端口、通信组、变量等通信参数
            /// </summary>
            参数设置,
            /// <summary>
            /// 配方管理窗体：管理温湿度工艺配方（增删改查、下发到设备）
            /// </summary>
            配方管理,
            /// <summary>
            /// 报警追溯窗体：查询历史报警记录（触发/消除时间、变量、操作人）
            /// </summary>
            报警追溯,
            /// <summary>
            /// 历史趋势窗体：按时间段绘制温湿度历史曲线
            /// </summary>
            历史趋势,
            /// <summary>
            /// 用户管理窗体：管理系统用户账号及其功能权限
            /// </summary>
            用户管理,
        }
    }
}
