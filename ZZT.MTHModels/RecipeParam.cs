using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZT.MTHModels
{
    /// <summary>
    /// 配方参数实体类（单站点）
    /// <para>描述单个监测站点的温湿度控制阈值与报警使能配置，是 RecipeInfo.RecipeParams 列表的元素类型。</para>
    /// <para>业务场景：配方下发时，TempHigh/TempLow/HumidityHigh/HumidityLow 作为上下限写入设备寄存器，
    /// 设备 PLC 据此进行就地控制与越限判断；TempAlarmEnable/HumidityAlarmEnable 决定上位机是否对该站点
    /// 的温湿度越限进行报警监听。</para>
    /// <para>单位约定：温度 ℃（摄氏度），湿度 %RH（相对湿度）。</para>
    /// <para>寄存器写入换算：浮点数值需 ×10 取整后写入（如 25.5℃ → 寄存器值 255），读取时反向 ÷10 还原。</para>
    /// </summary>
    public class RecipeParam
    {
        /// <summary>
        /// 温度上限（单位：℃）
        /// <para>当实时温度高于此值时视为越上限。取值示例：85.0 表示 85℃。</para>
        /// <para>写入寄存器时按 ×10 取整（如 85.0 → 850）。</para>
        /// </summary>
        public float TempHigh { get; set; }

        /// <summary>
        /// 温度下限（单位：℃）
        /// <para>当实时温度低于此值时视为越下限。取值示例：-20.0 表示 -20℃。</para>
        /// <para>写入寄存器时按 ×10 取整（如 -20.0 → -200）。</para>
        /// </summary>
        public float TempLow { get; set; }

        /// <summary>
        /// 湿度上限（单位：%RH）
        /// <para>当实时湿度高于此值时视为越上限。取值示例：95.0 表示 95%RH。</para>
        /// <para>写入寄存器时按 ×10 取整（如 95.0 → 950）。</para>
        /// </summary>
        public float HumidityHigh { get; set; }

        /// <summary>
        /// 湿度下限（单位：%RH）
        /// <para>当实时湿度低于此值时视为越下限。取值示例：20.0 表示 20%RH。</para>
        /// <para>写入寄存器时按 ×10 取整（如 20.0 → 200）。</para>
        /// </summary>
        public float HumidityLow { get; set; }

        /// <summary>
        /// 温度报警使能开关
        /// <para>true=启用温度越限报警（高于 TempHigh 或低于 TempLow 时报警）；false=忽略温度越限。</para>
        /// </summary>
        public bool TempAlarmEnable { get; set; }

        /// <summary>
        /// 湿度报警使能开关
        /// <para>true=启用湿度越限报警（高于 HumidityHigh 或低于 HumidityLow 时报警）；false=忽略湿度越限。</para>
        /// </summary>
        public bool HumidityAlarmEnable { get; set; }
    }
}
