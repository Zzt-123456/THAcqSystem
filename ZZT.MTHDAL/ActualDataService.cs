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
    /// 实际数据数据访问服务类（数据访问层 DAL）
    /// 所属层级：DAL（Data Access Layer），直接操作 SQL Server 数据库
    /// 核心职责：
    ///   1. 负责数据库 ActualData 表的读写操作，包括新增采集数据和按条件查询历史数据；
    ///   2. 使用参数化 SQL（SqlParameter）防止 SQL 注入攻击；
    ///   3. 通过 SQLHelper 通用类执行 SQL，统一管理连接的打开与关闭。
    /// 数据表说明：
    ///   ActualData 表记录每次采集的温湿度快照，包含 1 个时间字段（InsertTime）
    ///   和 6 个站点的温湿度共 12 个数值字段（Station1Temp~Station6Humidity）。
    /// </summary>
    public class ActualDataService
    {
        /// <summary>
        /// 向 ActualData 表新增一条温湿度采集记录
        /// </summary>
        /// <param name="actualData">包含采集时间及 6 个站点温湿度值的实体对象</param>
        /// <returns>返回受影响的行数（成功为 1，失败为 0）</returns>
        public int AddActualData(ActualData actualData)
        {
            //使用 StringBuilder 拼接 INSERT 语句，避免字符串频繁分配带来的性能开销
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Insert into ActualData(InsertTime,Station1Temp,Station1Humidity,");
            stringBuilder.Append("Station2Temp,Station2Humidity,Station3Temp,Station3Humidity,");
            stringBuilder.Append("Station4Temp,Station4Humidity,Station5Temp,Station5Humidity,");
            stringBuilder.Append("Station6Temp,Station6Humidity) values(@InsertTime,@Station1Temp,@Station1Humidity,");
            stringBuilder.Append("@Station2Temp,@Station2Humidity,@Station3Temp,@Station3Humidity,");
            stringBuilder.Append("@Station4Temp,@Station4Humidity,@Station5Temp,@Station5Humidity,");
            stringBuilder.Append("@Station6Temp,@Station6Humidity)");

            //构建参数数组：每个 @参数 与实体属性一一对应
            //使用参数化查询而非字符串拼接，从根本上防止 SQL 注入
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@InsertTime",actualData.InsertTime),
                new SqlParameter("@Station1Temp",actualData.Station1Temp),
                new SqlParameter("@Station1Humidity",actualData.Station1Humidity),
                new SqlParameter("@Station2Temp",actualData.Station2Temp),
                new SqlParameter("@Station2Humidity",actualData.Station2Humidity),
                new SqlParameter("@Station3Temp",actualData.Station3Temp),
                new SqlParameter("@Station3Humidity",actualData.Station3Humidity),
                new SqlParameter("@Station4Temp",actualData.Station4Temp),
                new SqlParameter("@Station4Humidity",actualData.Station4Humidity),
                new SqlParameter("@Station5Temp",actualData.Station5Temp),
                new SqlParameter("@Station5Humidity",actualData.Station5Humidity),
                new SqlParameter("@Station6Temp",actualData.Station6Temp),
                new SqlParameter("@Station6Humidity",actualData.Station6Humidity),
            };
            //调用通用 SQLHelper 执行非查询操作，返回受影响行数
            return SQLHelper.ExecuteNonQuery(stringBuilder.ToString(), sqlParameters);
        }

        /// <summary>
        /// 按时间范围和指定列查询历史温湿度数据
        /// </summary>
        /// <param name="start">查询起始时间字符串</param>
        /// <param name="end">查询结束时间字符串</param>
        /// <param name="columns">需要查询的列名集合（动态选择关注的站点字段）</param>
        /// <returns>包含查询结果的 DataTable；查询失败或无数据时返回 null</returns>
        public DataTable QueryActualDataByCondition(string start, string end, List<string> columns)
        {
            //动态拼接 SELECT 语句：固定查询 InsertTime 列，再拼接用户指定的其他列
            //string.Join 将列名列表用逗号连接，形成 "col1,col2,col3" 的形式
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Select InsertTime,");
            stringBuilder.Append(string.Join(",", columns));
            stringBuilder.Append(" from ActualData where InsertTime between @Start and @End");

            //时间范围参数化，防止注入
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Start", start),
                new SqlParameter("@End", end),
            };

            try
            {
                //通过 SQLHelper 获取 DataSet 结果集
                DataSet dataSet = SQLHelper.GetDataSet(stringBuilder.ToString(), sqlParameters);
                //判断 DataSet 中是否包含数据表，避免索引越界
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
