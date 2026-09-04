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
    /// 标题栏控件（Title）：用于显示一组控件的标题名称。
    /// 视觉特征：内部包含一个 lbl_Title 标签，以特定背景色和字体居中显示标题文字。
    /// 使用场景：常作为复合控件（如 RecipeControl）的顶部标题栏，标识下方区域所属的站点
    ///   或分组名称；通过 TitleName 属性设置显示文字。
    /// 绘图说明：标题文字由内部 lbl_Title 标签绘制（GDI+ DrawString），背景色由标签
    ///   BackColor 决定；本控件通过 SetStyle 启用双缓冲等优化样式避免重绘闪烁。
    /// </summary>
    public partial class Title : System.Windows.Forms.UserControl
    {
        /// <summary>
        /// 构造函数：初始化组件并设置控件样式。
        /// 设计器兼容：通过 LicenseManager.UsageMode 区分设计时/运行时，
        ///   避免设计器中加载全局静态资源导致控件类型加载失败。
        /// </summary>
        public Title()
        {
            InitializeComponent();
            //设置控件样式：以下 SetStyle 调用用于优化 GDI+ 绘图性能与表现
            //AllPaintingInWmPaint：忽略 WM_ERASEBKGND，仅在 WM_PAINT 中完成全部绘制，减少闪烁
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //DoubleBuffer：启用双缓冲，先在内存位图绘制再输出到屏幕，避免标题重绘闪烁
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            //ResizeRedraw：尺寸变化时整体重绘，保证 GDI+ 自绘内容随缩放正确刷新
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            //Selectable：允许控件接收焦点
            this.SetStyle(ControlStyles.Selectable, true);
            //SupportsTransparentBackColor：支持透明背景色，便于与父容器叠加
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            //仅在运行时加载 Title 背景图资源（设计时由宿主容器/设计器属性控制）
            //避免设计器因资源解析路径问题导致 Title 类型反射失败
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                this.BackgroundImage = Properties.Resources.Title;
            }
        }

        //标题名称字段：默认“标题名称”，显示在标题栏标签上
        private string titleName = "标题名称";

        /// <summary>
        /// 标题名称属性：设置或显示标题栏文字。
        /// 设计器特性：
        ///   [Browsable(true)] —— 在 Visual Studio 属性面板中可见；
        ///   [Category("自定义属性")] —— 归入“自定义属性”分类分组；
        ///   [Description(...)] —— 属性面板底部显示的说明文字。
        /// 赋值时同步刷新 lbl_Title 文本，由标签内部 GDI+ 重绘显示新标题。
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
                //刷新标题标签文字（触发标签 GDI+ 重绘）
                this.lbl_Title.Text = titleName;
            }
        }
    }
}
