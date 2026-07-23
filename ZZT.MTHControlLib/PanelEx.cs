using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZZT.MTHControlLib
{
    /// <summary>
    /// 扩展面板控件 PanelEx
    /// 用途：继承自 System.Windows.Forms.Panel，通过 GDI+ 自绘实现可定制边框样式的容器面板
    /// 视觉特征：可在面板四周绘制一条自定义颜色和宽度的矩形边框，并支持四边内边距(间隔距离)设置
    /// 使用场景：Modbus TCP 温湿度监控系统中作为分组容器或突出显示区域的边框装饰
    /// 绘图流程：在 OnPaint 中用 Pen + DrawRectangle 绘制矩形边框，矩形位置和大小由
    ///           TopGap/BottomGap/LeftGap/RightGap 与 BorderWidth 共同计算得出
    /// </summary>
    public partial class PanelEx : System.Windows.Forms.Panel
    {
        /// <summary>
        /// 构造函数：初始化控件并设置控件样式
        /// </summary>
        public PanelEx()
        {
            InitializeComponent();

            //设置控件样式
            //AllPaintingInWmPaint：忽略 WM_ERASEBKGND 消息，减少闪烁
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //DoubleBuffer：启用双缓冲，防止边框重绘闪烁
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            //ResizeRedraw：尺寸变化时自动重绘边框
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            //Selectable：可接收焦点
            this.SetStyle(ControlStyles.Selectable, true);
            //SupportsTransparentBackColor：支持透明背景
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        //上边间隔距离（像素），边框距面板顶部的留白
        private int topGap = 1;
        //Browsable(true)：在设计器属性面板显示该属性
        //Category：归类到"自定义属性"分组
        //Description：设计器底部显示的属性说明
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示上边间隔距离")]
        public int TopGap
        {
            get { return topGap; }
            set 
            { 
                topGap = value;
                //Invalidate 触发重绘，使新间隔立即生效
                this.Invalidate();
            }
        }

        //下边间隔距离（像素），边框距面板底部的留白
        private int bottomGap = 1;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示下边间隔距离")]
        public int BottomGap
        {
            get { return bottomGap; }
            set 
            { 
                bottomGap = value;
                this.Invalidate();
            }
        }

        //左边间隔距离（像素），边框距面板左侧的留白
        private int leftGap = 1;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示左边间隔距离")]
        public int LeftGap
        {
            get { return leftGap; }
            set 
            { 
                leftGap = value;
                this.Invalidate();
            }
        }

        //右边间隔距离（像素），边框距面板右侧的留白
        private int rightGap = 1;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示右边间隔距离")]
        public int RightGap
        {
            get { return rightGap; }
            set
            { 
                rightGap = value;
                this.Invalidate();
            }
        }

        //边框宽度（像素），决定画笔的粗细
        private int borderWidth = 2;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示边框宽度")]
        public int BorderWidth
        {
            get { return borderWidth; }
            set 
            {
                borderWidth = value;
                this.Invalidate();
            }
        }

        //边框颜色，默认青绿色（与温湿度监控系统主题色一致）
        private Color borderColor = Color.FromArgb(35, 255, 253);
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示边框颜色")]
        public Color BorderColor
        {
            get { return borderColor; }
            set 
            { 
                borderColor = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// 重写 OnPaint 方法，使用 GDI+ 绘制面板边框
        /// 绘图流程：
        /// 1. 通过 e.Graphics 获取画布对象
        /// 2. 用 borderColor 和 borderWidth 创建 Pen 画笔
        /// 3. 计算边框矩形坐标：起点考虑间隔(Gap)和半个边框宽度(避免被裁剪)，尺寸扣除两侧间隔与边框宽度
        /// 4. 用 DrawRectangle 绘制矩形边框
        /// </summary>
        /// <param name="e">包含 Graphics 画布的绘图事件参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //准备画布：从 PaintEventArgs 获取 Graphics 对象
            Graphics graphics = e.Graphics;

            //准备笔：用边框颜色和宽度创建 Pen 画笔
            Pen pen = new Pen(borderColor,borderWidth);

            //准备矩形参数
            //x、y 起点加上 borderWidth * 0.5f，使边框线居中显示不被控件边界裁剪
            float x = leftGap + borderWidth * 0.5f;
            float y = topGap + borderWidth * 0.5f;
            //宽度、高度扣除两侧间隔和边框宽度
            float width = this.Width - leftGap - rightGap - borderWidth;
            float height = this.Height - topGap - bottomGap - borderWidth;

            //绘制矩形：DrawRectangle 用画笔按计算好的位置和尺寸绘制边框
            graphics.DrawRectangle(pen, x, y, width, height);
        }
    }
}
