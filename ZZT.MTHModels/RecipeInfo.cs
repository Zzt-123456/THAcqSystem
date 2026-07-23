using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZT.MTHModels
{
    /// <summary>
    /// 配方信息实体类
    /// <para>对应"配方管理"业务模块，一个 RecipeInfo 代表一套完整的温湿度工艺配方。</para>
    /// <para>业务场景：不同产品/工艺需要不同的温湿度控制曲线，工程人员可为每种工艺保存一个配方
    /// （如"配方A"、"配方B"），运行时通过 Device.CurrentRecipe 指定当前生效配方，
    /// 并将配方参数（RecipeParams）批量下发到设备寄存器，实现工艺切换。</para>
    /// <para>结构说明：RecipeName 唯一标识配方，RecipeParams 固定包含 6 个站点（Station1~Station6）
    /// 的温湿度上下限与报警使能配置，与实际监测站点一一对应。</para>
    /// </summary>
    public class RecipeInfo
    {
        /// <summary>
        /// 配方名称（唯一标识）
        /// <para>取值示例："标准工艺"、"高温测试"、"低温存储"。</para>
        /// <para>作为配方文件的文件名或数据库主键使用，Device.CurrentRecipe 引用此名称切换当前配方。</para>
        /// </summary>
        public string RecipeName { get; set; }

        /// <summary>
        /// 配方参数列表（固定 6 个站点，按 Station1~Station6 顺序排列）
        /// <para>每个 RecipeParam 描述单个站点的温度上下限、湿度上下限及对应报警使能开关。</para>
        /// <para>默认初始化为空列表，配方加载时按站点顺序填充 6 个元素。</para>
        /// <para>下发到设备寄存器时，温度/湿度数值需 ×10 转为整数（如 25.5℃ → 255）以适配寄存器整型存储。</para>
        /// </summary>
        public List<RecipeParam> RecipeParams { get; set; } = new List<RecipeParam>();
    }
}
