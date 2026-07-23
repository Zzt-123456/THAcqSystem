using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZZT.MTHModels;

namespace ZZT.MTHControlLib
{
    /// <summary>
    /// 配方参数控件（RecipeControl）：用于配方管理界面，每个站点对应一个该控件。
    /// 视觉特征：顶部为 Title 标题栏（显示站点名称），下方排列 4 个 TextSetEx 子控件
    ///   分别用于输入温度高限、温度低限、湿度高限、湿度低限，并配有 2 个 CheckBoxEx
    ///   复选框用于勾选温度报警启用、湿度报警启用。
    /// 使用场景：在配方管理界面中作为单站点的参数录入卡片，通过 RecipeParam 属性
    ///   整体读写该站点的全部配方参数（温湿度高低限 + 报警启用开关）。
    /// 该控件为 UserControl 复合控件，内部子控件的绘制由各自负责，控件自身通过
    ///   SetStyle 启用双缓冲等 GDI+ 优化样式以避免闪烁。
    /// </summary>
    public partial class RecipeControl : UserControl
    {
        /// <summary>
        /// 构造函数：初始化组件并设置控件样式。
        /// </summary>
        public RecipeControl()
        {
            InitializeComponent();

            //设置控件样式：以下 SetStyle 调用用于优化 GDI+ 绘图性能与表现
            //AllPaintingInWmPaint：忽略 WM_ERASEBKGND 窗口消息，仅在 WM_PAINT 中完成所有绘制，减少背景擦除引起的闪烁
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //DoubleBuffer：启用双缓冲，先在内存位图中绘制再一次性输出到屏幕，避免 GDI+ 重绘闪烁
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            //ResizeRedraw：控件尺寸改变时整体重绘，保证 GDI+ 自绘内容随缩放正确刷新
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            //Selectable：允许控件接收焦点，便于键盘交互
            this.SetStyle(ControlStyles.Selectable, true);
            //SupportsTransparentBackColor：支持透明背景色，便于与父容器 GDI+ 绘图叠加
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        //设备名称字段：默认“1#站点”，作为配方卡片的标题前缀
        private string devName = "1#站点";

        /// <summary>
        /// 设备名称属性：设置或显示当前站点名称。
        /// 设计器特性说明：
        ///   [Browsable(true)] —— 在 Visual Studio 属性面板中可见；
        ///   [Category("自定义属性")] —— 归入“自定义属性”分类分组；
        ///   [Description(...)] —— 属性面板底部显示的说明文字。
        /// 赋值时会同步刷新标题栏及 4 个 TextSetEx 子控件的标题，使界面统一显示该站点名。
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示设备名称")]
        public string DevName
        {
            get { return devName; }
            set
            {
                devName = value;
                //同步更新顶部 Title 标题栏文字
                this.title1.TitleName = devName;
                //同步更新 4 个 TextSetEx 子控件的标题（拼接“温度高限/低限/湿度高限/低限”）
                this.textSetEx1.TitleName = devName + "温度高限";
                this.textSetEx2.TitleName = devName + "温度低限";
                this.textSetEx3.TitleName = devName + "湿度高限";
                this.textSetEx4.TitleName = devName + "湿度低限";
            }
        }

        //配方参数字段：缓存当前站点的配方数据（温湿度高低限 + 报警启用开关）
        private RecipeParam recipeParam = new RecipeParam();

        /// <summary>
        /// 配方参数属性：整体读取或写入当前站点的配方数据。
        /// 设计器特性说明：
        ///   [Browsable(false)] —— 不在属性面板中显示（属于复合数据，通过代码整体读写）；
        ///   [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        ///     —— 设计器生成的代码不会序列化该属性，避免设计器文件中产生冗余赋值。
        /// 读取时调用 GetRecipeParam() 从子控件汇总，写入时调用 SetRecipeParam() 分发到子控件。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RecipeParam RecipeParam
        {
            get
            {
                //读取属性时，从各子控件实时汇总配方参数
                recipeParam = GetRecipeParam();
                return recipeParam;
            }
            set
            {
                recipeParam = value;
                //写入属性时，将配方参数分发到各子控件显示
                SetRecipeParam(recipeParam);
            }
        }

        /// <summary>
        /// 从子控件汇总生成 RecipeParam 对象。
        /// 温度高低限、湿度高低限分别取自 textSetEx1~4 的 CurrentValue（float 数值），
        /// 温度/湿度报警启用取自两个 CheckBoxEx 的 Checked 状态。
        /// </summary>
        /// <returns>汇总后的配方参数对象</returns>
        private RecipeParam GetRecipeParam()
        {
            return new RecipeParam()
            {
                //温度高限：来自第 1 个 TextSetEx
                TempHigh = this.textSetEx1.CurrentValue,
                //温度低限：来自第 2 个 TextSetEx
                TempLow = this.textSetEx2.CurrentValue,
                //湿度高限：来自第 3 个 TextSetEx
                HumidityHigh = this.textSetEx3.CurrentValue,
                //湿度低限：来自第 4 个 TextSetEx
                HumidityLow = this.textSetEx4.CurrentValue,
                //温度报警启用：来自第 1 个复选框
                TempAlarmEnable = this.checkBoxEx1.Checked,
                //湿度报警启用：来自第 2 个复选框
                HumidityAlarmEnable = this.checkBoxEx2.Checked
            };
        }

        /// <summary>
        /// 将 RecipeParam 对象中的值分发到各子控件进行显示。
        /// 与 GetRecipeParam() 互为逆操作。
        /// </summary>
        /// <param name="recipeParam">要分发显示的配方参数对象</param>
        private void SetRecipeParam(RecipeParam recipeParam)
        {
            //温度高限 -> textSetEx1
            this.textSetEx1.CurrentValue = recipeParam.TempHigh;
            //温度低限 -> textSetEx2
            this.textSetEx2.CurrentValue = recipeParam.TempLow;
            //湿度高限 -> textSetEx3
            this.textSetEx3.CurrentValue = recipeParam.HumidityHigh;
            //湿度低限 -> textSetEx4
            this.textSetEx4.CurrentValue = recipeParam.HumidityLow;
            //温度报警启用 -> checkBoxEx1
            this.checkBoxEx1.Checked = recipeParam.TempAlarmEnable;
            //湿度报警启用 -> checkBoxEx2
            this.checkBoxEx2.Checked = recipeParam.HumidityAlarmEnable;
        }
    }
}
