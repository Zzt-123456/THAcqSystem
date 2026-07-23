using System;
using System.Collections.Generic;

using System.Data.SqlClient;
using System.Data;

using System.Configuration;

namespace ZZT.MTHDAL
{
    /// <summary>
    /// 通用数据访问类（数据访问层 DAL 的基础设施）
    /// 所属层级：DAL（Data Access Layer），是整个 DAL 层的公共基座
    /// 核心职责：
    ///   1. 封装 ADO.NET 的核心操作（SqlConnection、SqlCommand、SqlDataAdapter），
    ///      为上层各 Service 类提供统一的 SQL 执行入口；
    ///   2. 提供多种查询模式：非查询执行(ExecuteNonQuery)、单值查询(ExecuteScalar)、
    ///      只读流式查询(ExecuteReader)、数据集查询(GetDataSet)；
    ///   3. 统一管理数据库连接的生命周期（打开与关闭），通过 try-finally 保证连接必定释放；
    ///   4. 支持事务提交(ExecuteNonQueryByTran)，保证多条 SQL 的原子性。
    /// 设计说明：
    ///   所有方法均为 static 静态方法，无需实例化即可调用，减少对象创建开销；
    ///   连接字符串从 App.config 的 connString 配置项读取，实现配置与代码分离。
    /// </summary>
    public class SQLHelper
    {
        //读取配置文件获得连接字符串
        //使用 static readonly 语义：应用启动时读取一次，全局共享，避免每次访问数据库都重新读取配置
        //连接字符串定义在 App.config 的 <connectionStrings> 节点中，名为 connString
        private static string connString = ConfigurationManager.ConnectionStrings["connString"].ToString();

        /// <summary>
        /// 执行insert、update、delete类型的SQL语句
        /// </summary>
        /// <param name="cmdText">SQL语句或存储过程名称</param>
        /// <param name="paramArray">参数数组</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText, SqlParameter[] paramArray = null)
        {
            //创建连接对象（此时尚未真正连接数据库，需调用 Open 才会建立连接）
            SqlConnection conn = new SqlConnection(connString);
            //创建命令对象，绑定 SQL 文本与连接
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            //若传入了参数数组，则一次性添加到命令中
            if (paramArray != null)
            {
                cmd.Parameters.AddRange(paramArray);
            }
            try
            {
                conn.Open();  //打开数据库连接
                return cmd.ExecuteNonQuery();  //执行并返回受影响行数
            }
            catch (Exception ex)
            {
                //拼接错误信息（包含时间戳与方法签名），便于定位问题
                string errorMsg = $"{DateTime.Now}  : 执行 public static int ExecuteNonQuery(string cmdText, SqlParameter[] paramArray = null)方法发生异常：{ex.Message}";
                //在这个地方写入日志...


                //将异常包装后向上抛出，由上层统一处理
                throw new Exception("执行public static int ExecuteNonQuery(string cmdText, SqlParameter[] paramArray = null)方法发生异常：" + ex.Message);
            }
            finally   //以上不管是否发生异常，都会执行的代码
            {
                //无论执行成功还是抛出异常，finally 块都会执行，确保连接被关闭
                //如果不关闭连接，会导致连接泄漏，最终耗尽连接池
                conn.Close();
            }
        }

        /// <summary>
        /// 返回单一结果的查询
        /// 仅返回查询结果集第一行第一列的值，适用于 COUNT、MAX 等聚合查询
        /// </summary>
        /// <param name="sql">查询SQL语句</param>
        /// <returns>第一行第一列的值（object 类型，需调用方自行转换）</returns>
        public static object ExecuteScalar(string cmdText, SqlParameter[] paramArray = null)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            if (paramArray != null)
            {
                cmd.Parameters.AddRange(paramArray);
            }
            try
            {
                conn.Open();
                return cmd.ExecuteScalar();  //执行查询并返回首行首列
            }
            catch (Exception ex)
            {
                //在这个地方写入日志...

                throw new Exception("执行 public object ExecuteScalar(string cmdText, SqlParameter[] paramArray = null方法发生异常：" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 执行返回一个只读结果集的查询
        /// 返回 SqlDataReader，适用于需要逐行流式读取大量数据的场景（占用内存少）
        /// 注意：DataReader 占用期间连接不可复用，读取完毕后必须关闭
        /// </summary>
        /// <param name="sql">查询SQL语句</param>
        /// <returns>只读向前的数据读取器 SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string cmdText, SqlParameter[] paramArray = null)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            if (paramArray != null)
            {
                cmd.Parameters.AddRange(paramArray);
            }
            try
            {
                conn.Open();
                //CommandBehavior.CloseConnection：当 Reader 关闭时自动关闭关联的连接
                //必须添加这个枚举，否则调用方关闭 Reader 后连接仍会保持打开，导致连接泄漏
                return cmd.ExecuteReader(CommandBehavior.CloseConnection); //必须添加这个枚举
            }
            catch (Exception ex)
            {
                //在这个地方写入日志...

                throw new Exception("执行 public object SqlDataReader(string cmdText, SqlParameter[] paramArray = null)方法发生异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 返回包含一张数据表的数据集的查询
        /// 使用 SqlDataAdapter 将查询结果填充到 DataSet，适用于离线数据操作场景
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="tableName">数据表的名称（可为空，为空时由系统自动命名）</param>
        /// <returns>包含查询结果的 DataSet</returns>
        public static DataSet GetDataSet(string sql, string tableName = null)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            //数据适配器充当"桥梁"，负责在连接打开时自动填充 DataSet 并关闭连接
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                //根据是否指定表名选择不同的 Fill 重载
                if (tableName == null)
                    da.Fill(ds);
                else
                    da.Fill(ds, tableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("执行 public DataSet GetDataSet(string sql, string tableName = null)方法发生异常：" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 返回包含一张数据表的数据集的查询（带参数版本）
        /// 与无参版本功能相同，额外支持 SqlParameter 参数化查询，防止 SQL 注入
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="paramArray">SQL参数数组</param>
        /// <param name="tableName">数据表的名称（可为空）</param>
        /// <returns>包含查询结果的 DataSet</returns>
        public static DataSet GetDataSet(string sql, SqlParameter[] paramArray = null, string tableName = null)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (paramArray != null)
            {
                cmd.Parameters.AddRange(paramArray);
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                if (tableName == null)
                    da.Fill(ds);
                else
                    da.Fill(ds, tableName);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("执行 public DataSet GetDataSet(string sql, string tableName = null)方法发生异常：" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 执行查询，返回一个或多个表的DataSet
        /// 通过字典传入"表名->SQL语句"的映射，在一次连接中填充多张表，减少连接开销
        /// </summary>
        /// <param name="dicTableAndSql">键值对集合：键为结果表名，值为对应的查询SQL</param>
        /// <returns>包含多张数据表的 DataSet</returns>
        public static DataSet GetDataSet(Dictionary<string, string> dicTableAndSql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                //遍历字典，依次执行每条 SQL 并以指定表名填充到 DataSet 中
                foreach (string tbName in dicTableAndSql.Keys)
                {
                    cmd.CommandText = dicTableAndSql[tbName];
                    da.Fill(ds, tbName);
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception("执行 public DataSet GetDataSet(Dictionary<string,string> dicTableAndSql)方法发生异常：" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 基于事务提交
        /// 在同一事务中执行多条参数化 SQL，全部成功才提交，任意一条失败则回滚
        /// 适用于需要保证数据一致性的批量操作场景（如同时写入多张关联表）
        /// </summary>
        /// <param name="sql">要执行的SQL语句（所有批次共用同一条SQL，仅参数不同）</param>
        /// <param name="paramArrayList">多组参数数组的列表，每组对应一次执行</param>
        /// <returns>事务提交成功返回 true，失败返回 false（异常向上抛出）</returns>
        public static bool ExecuteNonQueryByTran(string sql, List<SqlParameter[]> paramArrayList)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                cmd.Transaction = conn.BeginTransaction();   //开启事务
                cmd.CommandText = sql;
                //遍历每组参数，重复执行同一条 SQL（每次执行前清空旧参数，防止参数累积冲突）
                foreach (SqlParameter[] param in paramArrayList)
                {
                    cmd.Parameters.Clear();   //清空上一轮的参数，避免参数重复添加报错
                    cmd.Parameters.AddRange(param);
                    cmd.ExecuteNonQuery();
                }
                cmd.Transaction.Commit();  //提交事务(同时自动清除事务)
                return true;
            }
            catch (Exception ex)
            {
                //发生异常时回滚事务，撤销已执行的所有操作，保证数据一致性
                if (cmd.Transaction != null)
                    cmd.Transaction.Rollback();//回滚事务(同时自动清除事务)
                throw new Exception("ExecuteNonQueryByTran(string sql,List<SqlParameter[]> paramArrayList)时出现错误：" + ex.Message);
            }
            finally
            {
                //清理事务对象并关闭连接
                if (cmd.Transaction != null)
                    cmd.Transaction = null;
                conn.Close();
            }
        }


    }
}
