using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZT.MTHDAL;
using ZZT.MTHModels;

namespace ZZT.MTHBLL
{
    /// <summary>
    /// 系统日志业务管理类（业务逻辑层 BLL）
    /// 所属层级：BLL（Business Logic Layer），位于 UI 层与 DAL 层之间
    /// 核心职责：
    ///   1. 封装系统日志（含报警记录）相关的业务逻辑，提供"新增日志"与"按条件查询日志"两个核心功能；
    ///   2. 对 DAL 层返回的受影响行数进行业务判断，转换为布尔结果；
    ///   3. 屏蔽底层 SQL 细节，使 UI 层只需关注日志的业务语义。
    /// 业务背景：
    ///   系统运行过程中会产生两类重要记录：操作日志与报警日志，统一存储在数据库 SysLog 表中。
    ///   当温湿度超限触发报警、或用户执行关键操作时，均会通过本类写入日志，
    ///   便于后续按时间范围和报警类型追溯历史事件。
    /// </summary>
    public class SysLogManage
    {
        //持有 DAL 层 SysLogService 的实例，BLL 通过它来读写数据库日志表
        private SysLogService sysLogService = new SysLogService();

        /// <summary>
        /// 新增一条系统日志（报警/操作记录）
        /// </summary>
        /// <param name="sysLog">包含日志时间、内容、操作人、变量名、报警类型的实体对象</param>
        /// <returns>新增成功返回 true，失败返回 false</returns>
        public bool AddSysLog(SysLog sysLog)
        {
            //DAL 层 AddSysLog 返回受影响行数：==1 表示成功写入一条日志记录
            //将行数结果转换为布尔值，简化 UI 层的判断逻辑
            return sysLogService.AddSysLog(sysLog) == 1;
        }

        /// <summary>
        /// 按时间范围与报警类型查询系统日志
        /// </summary>
        /// <param name="start">查询起始时间字符串</param>
        /// <param name="end">查询结束时间字符串</param>
        /// <param name="alarmType">报警类型（为空字符串时表示不按类型筛选，查询所有类型）</param>
        /// <returns>包含查询结果的 DataTable，无数据时返回 null</returns>
        public DataTable QuerySysLogByCondition(string start, string end, string alarmType)
        {
            //将查询请求转发给 DAL 层处理
            //返回 DataTable 便于 UI 层直接绑定到 DataGridView 展示历史日志
            return sysLogService.QuerySysLogByCondition(start, end, alarmType);
        }
    }
}
