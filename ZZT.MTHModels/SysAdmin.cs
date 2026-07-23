using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZT.MTHModels
{
    /// <summary>
    /// 系统管理员（用户）实体类
    /// <para>对应数据库 SysAdmin 表，存储系统登录账号及其对各功能模块的访问权限。</para>
    /// <para>业务场景：用户登录时按 LoginName + LoginPwd 校验身份，登录成功后将该用户的权限位
    /// （ParamSet/Recipe/HistoryLog/HistoryTrend/UserManage）缓存到会话，主界面据此控制各功能窗体
    /// （对应 Enum.FormNames 枚举）的可见性与可操作性，实现细粒度的功能权限控制。</para>
    /// <para>权限模型：采用布尔字段独立控制各模块，未采用角色组模式，每个用户单独配置权限。</para>
    /// </summary>
    public class SysAdmin
    {
        /// <summary>
        /// 用户登录ID（主键，自增）
        /// <para>数据库主键，唯一标识一条用户记录。</para>
        /// </summary>
        public int LoginId { get; set; }

        /// <summary>
        /// 登录用户名
        /// <para>登录时输入的账号名，建议全系统唯一。取值示例："admin"、"operator01"。</para>
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 登录密码
        /// <para>登录时输入的密码明文/密文（依实际存储策略而定），与 LoginName 配合校验身份。</para>
        /// <para>安全提示：生产环境建议存储哈希值而非明文。</para>
        /// </summary>
        public string LoginPwd { get; set; }

        /// <summary>
        /// 参数设置模块权限（对应 Enum.FormNames.参数设置）
        /// <para>true=允许访问"参数设置"窗体（配置设备通信参数、通信组、变量）；false=禁止访问。</para>
        /// </summary>
        public bool ParamSet { get; set; }

        /// <summary>
        /// 配方管理模块权限（对应 Enum.FormNames.配方管理）
        /// <para>true=允许访问"配方管理"窗体（增删改查配方、下发配方到设备）；false=禁止访问。</para>
        /// </summary>
        public bool Recipe { get; set; }

        /// <summary>
        /// 历史日志/报警追溯模块权限（对应 Enum.FormNames.报警追溯）
        /// <para>true=允许访问"报警追溯"窗体（查询历史报警与操作日志）；false=禁止访问。</para>
        /// </summary>
        public bool HistoryLog { get; set; }

        /// <summary>
        /// 历史趋势模块权限（对应 Enum.FormNames.历史趋势）
        /// <para>true=允许访问"历史趋势"窗体（查看温湿度历史曲线）；false=禁止访问。</para>
        /// </summary>
        public bool HistoryTrend { get; set; }

        /// <summary>
        /// 用户管理模块权限（对应 Enum.FormNames.用户管理）
        /// <para>true=允许访问"用户管理"窗体（新增/编辑/删除用户、分配权限）；false=禁止访问。</para>
        /// <para>通常仅超级管理员拥有此权限，避免普通用户越权提权。</para>
        /// </summary>
        public bool UserManage { get; set; }
    }
}
