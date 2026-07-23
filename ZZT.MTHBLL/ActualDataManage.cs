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
    /// 实际数据业务管理类（业务逻辑层 BLL）
    /// 所属层级：BLL（Business Logic Layer），位于 UI 层与 DAL 层之间
    /// 核心职责：
    ///   1. 作为 UI 层与 ActualDataService(DAL) 之间的中间桥梁，封装温湿度实际数据相关的业务逻辑；
    ///   2. 对 DAL 层返回的结果进行业务判断（例如将受影响行数转换为布尔结果）；
    ///   3. 屏蔽底层 SQL 细节，使 UI 层只需关注业务语义，无需关心数据如何持久化。
    /// 业务背景：
    ///   本系统通过 Modbus TCP 协议从 6 个监测站点采集温湿度数据，采集到的数据
    ///   最终写入数据库 ActualData 表，本类负责对外提供"新增采集数据"与"按条件查询历史数据"两个业务入口。
    /// </summary>
    public class ActualDataManage
    {
        //持有 DAL 层 ActualDataService 的实例，BLL 通过它来访问数据库
        //之所以在 BLL 层持有 DAL 引用，是为了实现"分层解耦"：UI 层只依赖 BLL，不直接接触 DAL
        private ActualDataService actualDataService = new ActualDataService();

        /// <summary>
        /// 新增一条实际温湿度采集数据
        /// </summary>
        /// <param name="actualData">包含 6 个站点温湿度值及采集时间的实体对象</param>
        /// <returns>新增成功返回 true，失败返回 false</returns>
        public bool AddActualData(ActualData actualData)
        {
            //DAL 层 AddActualData 返回受影响的行数：==1 表示恰好插入一条记录，即新增成功
            //这里将"行数"这一 DAL 语义转换为 UI 层更易用的"成功/失败"布尔语义
            return actualDataService.AddActualData(actualData) == 1;
        }

        /// <summary>
        /// 按时间范围与指定列查询历史温湿度数据
        /// </summary>
        /// <param name="start">查询起始时间字符串</param>
        /// <param name="end">查询结束时间字符串</param>
        /// <param name="columns">需要查询的列名集合（例如只查某几个站点的温度）</param>
        /// <returns>包含查询结果的 DataTable，无数据时返回 null</returns>
        public DataTable QueryActualDataByCondition(string start, string end, List<string> columns)
        {
            //直接将查询请求转发给 DAL 层，因为本场景下查询无需额外业务校验
            //返回 DataTable 便于 UI 层直接绑定到 DataGridView 等控件展示
            return actualDataService.QueryActualDataByCondition(start, end, columns);
        }
    }
}
