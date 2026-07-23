using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZT.MTHModels
{
    /// <summary>
    /// 实际采集数据实体类
    /// <para>对应数据库 ActualData 表，用于持久化每个采样周期 6 个监测站点的温湿度快照。</para>
    /// <para>业务场景：通信线程按固定周期（如每分钟）从各站点读取温湿度后，组装成一条 ActualData 记录
    /// 写入数据库，供"历史趋势"曲线、"报警追溯"查询等界面检索展示。</para>
    /// <para>设计说明：所有字段均为 string 类型以兼容 SQLite 等弱类型存储，数值在写入前已按工程量转换完成
    /// （单位：温度 ℃、湿度 %RH），时间字段为格式化后的字符串。</para>
    /// </summary>
    public class ActualData
    {
        /// <summary>
        /// 记录插入时间（采样时刻）
        /// <para>格式通常为 "yyyy-MM-dd HH:mm:ss"，作为历史查询的时间轴依据。</para>
        /// </summary>
        public string InsertTime { get; set; }

        /// <summary>
        /// 1 号站温度（单位：℃，工程量已转换）
        /// </summary>
        public string Station1Temp { get; set; }
        /// <summary>
        /// 1 号站湿度（单位：%RH，工程量已转换）
        /// </summary>
        public string Station1Humidity { get; set; }

        /// <summary>
        /// 2 号站温度（单位：℃，工程量已转换）
        /// </summary>
        public string Station2Temp { get; set; }
        /// <summary>
        /// 2 号站湿度（单位：%RH，工程量已转换）
        /// </summary>
        public string Station2Humidity { get; set; }

        /// <summary>
        /// 3 号站温度（单位：℃，工程量已转换）
        /// </summary>
        public string Station3Temp { get; set; }
        /// <summary>
        /// 3 号站湿度（单位：%RH，工程量已转换）
        /// </summary>
        public string Station3Humidity { get; set; }

        /// <summary>
        /// 4 号站温度（单位：℃，工程量已转换）
        /// </summary>
        public string Station4Temp { get; set; }
        /// <summary>
        /// 4 号站湿度（单位：%RH，工程量已转换）
        /// </summary>
        public string Station4Humidity { get; set; }

        /// <summary>
        /// 5 号站温度（单位：℃，工程量已转换）
        /// </summary>
        public string Station5Temp { get; set; }
        /// <summary>
        /// 5 号站湿度（单位：%RH，工程量已转换）
        /// </summary>
        public string Station5Humidity { get; set; }

        /// <summary>
        /// 6 号站温度（单位：℃，工程量已转换）
        /// </summary>
        public string Station6Temp { get; set; }
        /// <summary>
        /// 6 号站湿度（单位：%RH，工程量已转换）
        /// </summary>
        public string Station6Humidity { get; set; }
    }
}
