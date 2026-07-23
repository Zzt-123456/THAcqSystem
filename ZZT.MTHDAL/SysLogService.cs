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
    /// 系统日志数据访问服务类（数据访问层 DAL）
    /// 所属层级：DAL（Data Access Layer），直接操作 SQL Server 数据库
    /// 核心职责：
    ///   1. 负责数据库 SysLog 表的写入和查询操作；
    ///   2. 提供报警/操作日志的新增功能，记录系统运行过程中的关键事件；
    ///   3. 支持按时间范围和报警类型动态组合条件查询历史日志；
    ///   4. 使用参数化 SQL（SqlParameter）防止 SQL 注入。
    /// 数据表说明：
    ///   SysLog 表存储系统日志，字段包括：
    ///   InsertTime(记录时间)、Note(日志内容)、Operator(操作人)、
    ///   VarName(关联变量名)、AlarmType(报警类型)。
    /// </summary>
    public class SysLogService
    {
        /// <summary>
        /// 插入一条报警记录
        /// 将报警或操作事件持久化到 SysLog 表，供后续追溯查询
        /// </summary>
        /// <param name="sysLog">包含时间、内容、操作人、变量名、报警类型的日志实体</param>
        /// <returns>返回受影响的行数（成功为 1，失败为 0）</returns>
        public int AddSysLog(SysLog sysLog)
        {
            //使用参数化 INSERT 语句，5 个字段与参数一一对应
            string sql = "Insert into SysLog(InsertTime,Note,Operator,VarName,AlarmType) ";
            sql += "values(@InsertTime,@Note,@Operator,@VarName,@AlarmType)";

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
        new SqlParameter("@InsertTime",sysLog.InsertTime),
        new SqlParameter("@Note",sysLog.Note),
        new SqlParameter("@Operator",sysLog.Operator),
        new SqlParameter("@VarName",sysLog.VarName),
        new SqlParameter("@AlarmType",sysLog.AlarmType),
            };
            return SQLHelper.ExecuteNonQuery(sql, sqlParameters);
        }

        /// <summary>
        /// 根据时间差值及报警类型进行查询
        /// 支持动态条件组合：时间范围为必选条件，报警类型为可选条件
        /// </summary>
        /// <param name="start">查询起始时间字符串</param>
        /// <param name="end">查询结束时间字符串</param>
        /// <param name="alarmType">报警类型（空字符串表示不按类型筛选）</param>
        /// <returns>包含查询结果的 DataTable；查询失败或无数据时返回 null</returns>
        public DataTable QuerySysLogByCondition(string start, string end, string alarmType)
        {
            //基础 SQL：按时间范围查询（between 包含边界值）
            string sql = "Select InsertTime,Note,Operator,VarName,AlarmType from SysLog where InsertTime between @Start and @End";
            //使用 List 而非数组，因为参数数量需根据报警类型动态增减
            List<SqlParameter> sqlParameters = new List<SqlParameter>
            {
                new SqlParameter("@Start", start),
                new SqlParameter("@End", end)
            };

            //关键逻辑分支：当传入了报警类型时，动态追加类型筛选条件
            //这种"动态拼接 SQL + 动态添加参数"的方式实现了灵活的条件组合查询
            if (alarmType.Length > 0)
            {
                sql += " and AlarmType=@AlarmType";
                sqlParameters.Add(new SqlParameter("@AlarmType", alarmType));
            }

            try
            {
                //将 List 转为数组后传给 SQLHelper 执行查询
                DataSet dataSet = SQLHelper.GetDataSet(sql, sqlParameters.ToArray());
                if (dataSet.Tables.Count > 0)
                {
                    return dataSet.Tables[0];
                }
                else
                {
                    //无数据表时返回 null，调用方需做空值判断
                    return null;
                }
            }
            catch (Exception)
            {
                //查询发生异常时返回 null，避免异常向上抛出导致程序崩溃
                //注意：此处吞掉了异常，生产环境建议补充日志记录以便排查问题
                return null;
            }
        }
    }
}
