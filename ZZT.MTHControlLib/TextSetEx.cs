using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZZT.MTHControlLib
{
    /// <summary>
    /// 扩展参数设置控件（TextSetEx）：TextSet 的可编辑扩展版本。
    /// 与 TextSet 的差异：数值显示由只读的 lbl_Value 标签改为可编辑的 NumericUpDown
    ///   （num_Value）数值输入框，CurrentValue 类型由 string 改为 float，
    ///  便于直接在界面上录入限值参数（如温度/湿度的高低限）。
    /// 视觉特征：左侧为标题标签 lbl_Title（说明参数含义，如“1#站点温度高限”），
    ///   中间为 num_Value 数值输入框（支持上下调节与键盘输入），
    ///   右侧为单位标签 lbl_Unit（如“℃”、“%RH”）。
    /// 使用场景：在 RecipeControl 配方管理界面中作为子控件，每个站点使用 4 个该控件
    ///   分别录入温度高限、温度低限、湿度高限、湿度低限，通过 CurrentValue 读写浮点数值。
    /// 绘图说明：本控件本身为复合控件，GDI+ 绘制由内部子控件各自负责，
    ///   通过 SetStyle 启用双缓冲等优化样式避免闪烁。
    /// </summary>
    public partial class TextSetEx : System.Windows.Forms.UserControl
    {
        /// <summary>
        /// 构造函数：初始化组件并设置控件样式。
        /// </summary>
        public TextSetEx()
        {
            InitializeComponent();

            //设置控件样式：以下 SetStyle 调用用于优化 GDI+ 绘图性能与表现
            //AllPaintingInWmPaint：忽略 WM_ERASEBKGND，仅在 WM_PAINT 中完成全部绘制，减少闪烁
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //DoubleBuffer：启用双缓冲，先在内存位图绘制再输出到屏幕，避免重绘闪烁
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            //ResizeRedraw：尺寸变化时整体重绘，保证 GDI+ 自绘内容随缩放正确刷新
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            //Selectable：允许控件接收焦点，便于键盘交互
            this.SetStyle(ControlStyles.Selectable, true);
            //SupportsTransparentBackColor：支持透明背景色，便于与父容器叠加
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        //标题名称字段：默认“1#站点温度高限”，显示在标题标签上
        private string titleName = "1#站点温度高限";

        /// <summary>
        /// 标题名称属性：设置或显示该参数项的标题文字。
        /// 设计器特性：[Browsable(true)]/Category/Description，属性面板可见可编辑。
        /// 赋值时同步刷新 lbl_Title 文本。
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示标题名称")]
        public string TitleName
        {
            get { return titleName; }
            set
            {
                titleName = value;
                //刷新标题标签文字
                this.lbl_Title.Text = titleName;
            }
        }

        //单位字段：默认“℃”，显示在数值右侧的单位标签上
        private string unit = "℃";

        /// <summary>
        /// 单位属性：设置或显示数值的单位（如“℃”、“%RH”）。
        /// 设计器特性：[Browsable(true)]/Category/Description。
        /// 赋值时同步刷新 lbl_Unit 文本。
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示单位名称")]
        public string Unit
        {
            get { return unit; }
            set
            {
                unit = value;
                //刷新单位标签文字
                this.lbl_Unit.Text = unit;
            }
        }

        //当前数值字段：浮点型，默认 0.0f，与 NumericUpDown 输入框双向绑定
        private float currentValue = 0.0f;

        /// <summary>
        /// 当前数值属性：设置或显示当前参数值（浮点型，可直接编辑）。
        /// 设计器特性：[Browsable(true)]/Category/Description。
        /// 读取时从 num_Value.Value 实时获取（Convert.ToSingle 转换 decimal->float），
        ///   保证读到的是用户当前输入的最新值；
        /// 写入时仅当值变化才更新 num_Value.Value（Convert.ToDecimal 转换 float->decimal），
        ///   避免无谓刷新。
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示当前数值")]
        public float CurrentValue
        {
            get
            {
                //读取时从 NumericUpDown 实时取值（decimal 转 float）
                currentValue = Convert.ToSingle(this.num_Value.Value);
                return currentValue;
            }
            set
            {
                //仅当值变化时才更新输入框，避免无谓刷新
                if (currentValue != value)
                {
                    currentValue = value;
                    //将 float 转为 decimal 赋给 NumericUpDown（float->decimal）
                    this.num_Value.Value = Convert.ToDecimal(currentValue);
                }
            }
        }
    }
}
