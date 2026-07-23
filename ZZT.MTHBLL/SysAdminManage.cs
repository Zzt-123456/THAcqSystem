using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZT.MTHDAL;
using ZZT.MTHModels;

namespace ZZT.MTHBLL
{
    /// <summary>
    /// 系统管理员业务管理类（业务逻辑层 BLL）
    /// 所属层级：BLL（Business Logic Layer），位于 UI 层与 DAL 层之间
    /// 核心职责：
    ///   1. 封装管理员账号相关的全部业务逻辑，包括登录验证、增删改查；
    ///   2. 对 DAL 层返回的"受影响行数"进行业务判断，转换为布尔结果供 UI 层使用；
    ///   3. 作为 UI 层访问管理员数据的唯一入口，屏蔽底层 SQL 与数据库细节。
    /// 业务背景：
    ///   系统数据库 SysAdmin 表存储所有操作员账号，每个账号拥有 5 个功能权限位
    ///   （ParamSet 参数设置、Recipe 配方管理、HistoryLog 历史日志、HistoryTrend 历史趋势、UserManage 用户管理），
    ///   本类负责这些账号的生命周期管理与登录鉴权。
    /// </summary>
    public class SysAdminManage
    {
        //持有 DAL 层 SysAdminService 的实例，BLL 通过它执行具体的数据库操作
        //在构造时直接实例化，保证调用方法时 service 已就绪
        private SysAdminService sysAdminService = new SysAdminService();

        /// <summary>
        /// 管理员登录验证
        /// </summary>
        /// <param name="sysAdmin">包含用户输入的登录名和密码的实体对象</param>
        /// <returns>登录成功返回带有权限信息的 SysAdmin 对象；失败返回 null</returns>
        public SysAdmin AdminLogin(SysAdmin sysAdmin)
        {
            //将登录请求转发给 DAL 层，DAL 会根据用户名+密码查询数据库
            //若匹配到唯一记录，DAL 会将权限位回填到 sysAdmin 对象并返回；否则返回 null
            return sysAdminService.AdminLogin(sysAdmin);
        }

        /// <summary>
        /// 新增管理员账号
        /// </summary>
        /// <param name="sysAdmin">包含新账号信息及权限设置的实体对象</param>
        /// <returns>新增成功返回 true，失败返回 false</returns>
        public bool AddSysAdmin(SysAdmin sysAdmin)
        {
            //DAL 层返回受影响行数，==1 表示成功插入一条新账号记录
            return sysAdminService.AddSysAdmin(sysAdmin) == 1;
        }

        /// <summary>
        /// 根据 LoginId 删除管理员账号
        /// </summary>
        /// <param name="loginId">要删除的管理员主键 LoginId</param>
        /// <returns>删除成功返回 true，失败返回 false</returns>
        public bool DeleteSysAdmin(int loginId)
        {
            //==1 表示恰好删除了一条记录，即该账号存在且已被移除
            return sysAdminService.DeleteSysAdmin(loginId) == 1;
        }

        /// <summary>
        /// 修改管理员账号信息（含密码与权限）
        /// </summary>
        /// <param name="sysAdmin">包含修改后信息及 LoginId 的实体对象</param>
        /// <returns>修改成功返回 true，失败返回 false</returns>
        public bool ModifySysAdmin(SysAdmin sysAdmin)
        {
            //==1 表示根据 LoginId 成功更新了一条记录
            return sysAdminService.ModifySysAdmin(sysAdmin) == 1;
        }

        /// <summary>
        /// 查询全部管理员账号列表
        /// </summary>
        /// <returns>包含所有管理员信息的 List 集合，供 UI 层的用户管理界面展示</returns>
        public List<SysAdmin> QuerySysAdmins()
        {
            //直接返回 DAL 查询结果，UI 层可用于绑定列表控件或进行进一步筛选
            return sysAdminService.QuerySysAdmins();
        }
    }
}
