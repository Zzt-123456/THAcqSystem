using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZT.MTHModels;

namespace ZZT.MTHDAL
{
    /// <summary>
    /// 管理员数据访问服务类（数据访问层 DAL）
    /// 所属层级：DAL（Data Access Layer），直接操作 SQL Server 数据库
    /// 核心职责：
    ///   1. 负责数据库 SysAdmin 表的全部 CRUD（增删改查）操作；
    ///   2. 提供管理员登录验证功能，根据用户名+密码查询账号并回填权限信息；
    ///   3. 使用参数化 SQL（SqlParameter）防止 SQL 注入；
    ///   4. 通过 SQLHelper 通用类执行 SQL，统一管理数据库连接。
    /// 数据表说明：
    ///   SysAdmin 表存储操作员账号，字段包括：
    ///   LoginId(主键)、LoginName(登录名)、LoginPwd(密码)，
    ///   以及 5 个权限位：ParamSet、Recipe、HistoryLog、HistoryTrend、UserManage（均为 bit 类型）。
    /// </summary>
    public class SysAdminService
    {
        /// <summary>
        /// 管理员登录验证
        /// 根据登录名和密码查询数据库，匹配成功则回填权限信息并返回实体对象
        /// </summary>
        /// <param name="sysAdmin">包含用户输入的 LoginName 和 LoginPwd 的实体对象</param>
        /// <returns>登录成功返回带权限信息的 SysAdmin 对象；失败返回 null</returns>
        public SysAdmin AdminLogin(SysAdmin sysAdmin)
        {
            //登录查询只取权限相关字段，不返回密码，减少敏感数据传输
            string sql = "Select LoginId,ParamSet,Recipe,HistoryLog,HistoryTrend,UserManage";
            sql += " from SysAdmin where LoginName=@LoginName and LoginPwd=@LoginPwd";

            //参数化查询：将用户输入作为参数传入，而非字符串拼接，从根本上防止 SQL 注入
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LoginName", sysAdmin.LoginName),
                new SqlParameter("@LoginPwd", sysAdmin.LoginPwd),
            };

            //通过 SQLHelper 执行查询，返回 DataSet
            DataSet dataSet = SQLHelper.GetDataSet(sql, parameters);
            //三重校验：DataSet 非空、包含数据表、且恰好匹配到一条记录（Rows.Count == 1）
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count == 1)
            {
                //将查询结果中的权限位逐一回填到传入的 sysAdmin 对象
                sysAdmin.LoginId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["LoginId"]);
                sysAdmin.ParamSet = Convert.ToBoolean(dataSet.Tables[0].Rows[0]["ParamSet"]);
                sysAdmin.Recipe = Convert.ToBoolean(dataSet.Tables[0].Rows[0]["Recipe"]);
                sysAdmin.HistoryLog = Convert.ToBoolean(dataSet.Tables[0].Rows[0]["HistoryLog"]);
                sysAdmin.HistoryTrend = Convert.ToBoolean(dataSet.Tables[0].Rows[0]["HistoryTrend"]);
                sysAdmin.UserManage = Convert.ToBoolean(dataSet.Tables[0].Rows[0]["UserManage"]);
                return sysAdmin;
            }
            else
            {
                //未匹配到记录（用户名或密码错误）或匹配多条（数据异常），均视为登录失败
                return null;
            }
        }

        /// <summary>
        /// 新增管理员账号
        /// </summary>
        /// <param name="sysAdmin">包含登录名、密码及 5 个权限位的新账号实体</param>
        /// <returns>返回受影响的行数（成功为 1，失败为 0）</returns>
        public int AddSysAdmin(SysAdmin sysAdmin)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("Insert into SysAdmin(LoginName,LoginPwd,ParamSet,Recipe,HistoryLog,HistoryTrend,UserManage)");
            stringBuilder.Append(" values(@LoginName,@LoginPwd,@ParamSet,@Recipe,@HistoryLog,@HistoryTrend,@UserManage)");

            //构建参数数组，与 INSERT 语句中的占位符一一对应
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@LoginName",sysAdmin.LoginName),
                new SqlParameter("@LoginPwd",sysAdmin.LoginPwd),
                new SqlParameter("@ParamSet",sysAdmin.ParamSet),
                new SqlParameter("@Recipe",sysAdmin.Recipe),
                new SqlParameter("@HistoryLog",sysAdmin.HistoryLog),
                new SqlParameter("@HistoryTrend",sysAdmin.HistoryTrend),
                new SqlParameter("@UserManage",sysAdmin.UserManage)
            };

            return SQLHelper.ExecuteNonQuery(stringBuilder.ToString(), sqlParameters);
        }

        /// <summary>
        /// 根据 LoginId 删除管理员账号
        /// </summary>
        /// <param name="loginId">要删除的管理员主键 LoginId</param>
        /// <returns>返回受影响的行数（成功为 1，失败为 0）</returns>
        public int DeleteSysAdmin(int loginId)
        {
            //按主键 LoginId 删除，确保只删除指定的一条记录
            string sql = "Delete from SysAdmin where LoginId=@LoginId";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@LoginId",loginId),
            };

            return SQLHelper.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// 修改管理员账号信息（含登录名、密码及全部权限位）
        /// </summary>
        /// <param name="sysAdmin">包含修改后信息及 LoginId 的实体对象</param>
        /// <returns>返回受影响的行数（成功为 1，失败为 0）</returns>
        public int ModifySysAdmin(SysAdmin sysAdmin)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Update SysAdmin set LoginName=@LoginName,LoginPwd=@LoginPwd,");
            stringBuilder.Append("ParamSet=@ParamSet,Recipe=@Recipe,");
            stringBuilder.Append("HistoryLog=@HistoryLog,HistoryTrend=@HistoryTrend,");
            stringBuilder.Append("UserManage=@UserManage where LoginId=@LoginId");

            //参数数组中 LoginId 作为 WHERE 条件，其余字段为 SET 更新值
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
        new SqlParameter("@LoginId",sysAdmin.LoginId),
        new SqlParameter("@LoginName",sysAdmin.LoginName),
        new SqlParameter("@LoginPwd",sysAdmin.LoginPwd),
        new SqlParameter("@ParamSet",sysAdmin.ParamSet),
        new SqlParameter("@Recipe",sysAdmin.Recipe),
        new SqlParameter("@HistoryLog",sysAdmin.HistoryLog),
        new SqlParameter("@HistoryTrend",sysAdmin.HistoryTrend),
        new SqlParameter("@UserManage",sysAdmin.UserManage)
            };

            return SQLHelper.ExecuteNonQuery(stringBuilder.ToString(), sqlParameters);
        }

        /// <summary>
        /// 查询全部管理员账号列表
        /// 使用 SqlDataReader 流式读取，适用于不需要离线编辑、仅展示列表的场景
        /// </summary>
        /// <returns>包含所有管理员信息的 List 集合</returns>
        public List<SysAdmin> QuerySysAdmins()
        {
            //查询全部字段，包含密码（用户管理界面需要展示和编辑完整信息）
            string sql = "Select LoginId,LoginName,LoginPwd,ParamSet,Recipe,HistoryLog,HistoryTrend,UserManage from SysAdmin";
            //使用 ExecuteReader 流式读取，相比 DataSet 占用内存更少
            SqlDataReader sqlDataReader = SQLHelper.ExecuteReader(sql);
            List<SysAdmin> sysAdmins = new List<SysAdmin>();

            //逐行读取，每行构建一个 SysAdmin 实体并加入列表
            while (sqlDataReader.Read())
            {
                sysAdmins.Add(new SysAdmin()
                {
                    LoginId = Convert.ToInt32(sqlDataReader["LoginId"]),
                    LoginName = sqlDataReader["LoginName"].ToString(),
                    LoginPwd = sqlDataReader["LoginPwd"].ToString(),
                    ParamSet = Convert.ToBoolean(sqlDataReader["ParamSet"]),
                    Recipe = Convert.ToBoolean(sqlDataReader["Recipe"]),
                    HistoryLog = Convert.ToBoolean(sqlDataReader["HistoryLog"]),
                    HistoryTrend = Convert.ToBoolean(sqlDataReader["HistoryTrend"]),
                    UserManage = Convert.ToBoolean(sqlDataReader["UserManage"]),
                });
            }
            return sysAdmins;
        }
    }
}
