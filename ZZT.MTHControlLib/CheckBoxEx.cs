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
    /// 扩展复选框控件 CheckBoxEx
    /// 用途：继承自 System.Windows.Forms.CheckBox，通过 GDI+ 自绘实现自定义外观的复选框
    /// 视觉特征：可自定义选中框宽度、选中勾的颜色、选中框背景颜色；文字居中显示在选中框右侧
    /// 使用场景：Modbus TCP 温湿度监控系统中需要统一风格 UI 的勾选项设置（如参数开关、报警启用等）
    /// 绘图流程：在 OnPaint 中使用 Graphics 对象依次绘制选中框背景(FillRectangle)、
    ///           选中框边框(DrawRectangle)、勾选标记(DrawLines)以及文本(DrawString)
    /// </summary>
    public partial class CheckBoxEx : System.Windows.Forms.CheckBox
    {
        /// <summary>
        /// 构造函数：初始化控件并设置字符串格式与控件样式
        /// </summary>
        public CheckBoxEx()
        {
            InitializeComponent();
            //设置文本在垂直方向居中对齐
            stringFormat.LineAlignment = StringAlignment.Center;
            //设置文本在水平方向居中对齐
            stringFormat.Alignment = StringAlignment.Center;

            //设置控件样式
            //AllPaintingInWmPaint：忽略 WM_ERASEBKGND 消息，减少闪烁
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //DoubleBuffer：启用双缓冲，先将绘图绘制到内存位图再显示，防止闪烁
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            //ResizeRedraw：控件大小改变时自动重绘
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            //Selectable：控件可接收焦点
            this.SetStyle(ControlStyles.Selectable, true);
            //SupportsTransparentBackColor：支持透明背景色
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        //文本对齐格式，控制绘制文字时的居中方式
        private StringFormat stringFormat= new StringFormat();

        //默认选中框宽度（像素）
        private int defaultCheckButtonWidth = 20;

        //Browsable(true)：在属性设计器中显示该属性
        //Category：将属性归类到"自定义属性"分组下
        //Description：在设计器底部显示的属性说明文字
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示选中框的宽度")]
        public int DefaultCheckButtonWidth
        {
            get { return defaultCheckButtonWidth; }
            set
            {
                defaultCheckButtonWidth = value;
                //Invalidate 触发重绘，使新宽度立即生效
                this.Invalidate();
            }
        }

        //勾选标记颜色，默认深蓝色
        private Color checkColor = Color.FromArgb(3, 25, 66);

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示选中颜色")]
        public Color CheckColor
        {
            get { return checkColor; }
            set
            {
                checkColor = value;
                this.Invalidate();
            }
        }

        //选中框背景颜色，默认白色
        private Color checkBackColor = Color.White;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示选中框背景颜色")]
        public Color CheckBackColor
        {
            get { return checkBackColor; }
            set
            {
                checkBackColor = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// 重写 OnPaint 方法，使用 GDI+ 自绘复选框外观
        /// 绘图流程：
        /// 1. 通过 e.Graphics 获取画布对象
        /// 2. 设置抗锯齿与文字渲染质量
        /// 3. 调用 CalculatorRec 计算选中框与文本的矩形区域
        /// 4. 用 SolidBrush + FillRectangle 填充选中框背景
        /// 5. 用 Pen + DrawRectangle 绘制选中框边框
        /// 6. 若处于选中状态，调用 DrawCheckedFlag 绘制勾选标记
        /// 7. 用 DrawString 绘制文本
        /// </summary>
        /// <param name="e">包含绘图对象和裁剪区域的事件参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            base.OnPaintBackground(e);

            //从 PaintEventArgs 中获取 Graphics 画布对象，所有 GDI+ 绘图均通过它完成
            Graphics graphics = e.Graphics;
            //设置抗锯齿模式，使绘制的线条和曲线更平滑
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //设置文字渲染提示为 ClearType 网格适配，提升文字清晰度
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            //声明选中框矩形和文本矩形
            Rectangle checkRec;
            Rectangle textRec;

            //计算两个矩形的位置和大小
            CalculatorRec(out checkRec, out textRec);

            //创建实心画刷，用于填充选中框背景色
            SolidBrush solidBrush = new SolidBrush(checkBackColor);
            //FillRectangle：用画刷填充选中框背景区域
            graphics.FillRectangle(solidBrush, checkRec);

            //创建画笔，用于绘制选中框边框
            Pen pen = new Pen(Color.LightGray);
            //DrawRectangle：用画笔绘制选中框边框
            graphics.DrawRectangle(pen, checkRec);

            if (this.CheckState == CheckState.Checked)
            {
                //画勾选：当控件处于选中状态时，绘制勾选标记
                DrawCheckedFlag(graphics, checkRec,checkColor);
            }

            //绘制文本：使用 DrawString 将 Text 属性文本绘制到文本矩形中
            graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), textRec, this.stringFormat);
        }


        /// <summary>
        /// 计算矩形
        /// </summary>
        /// <param name="checkRec">输出：选中框矩形区域</param>
        /// <param name="textRec">输出：文本矩形区域</param>
        private void CalculatorRec(out Rectangle checkRec, out Rectangle textRec)
        {
            //选中框：左侧 3 像素留白，垂直居中，宽高均为 defaultCheckButtonWidth
            checkRec = new Rectangle(3, (this.Height - defaultCheckButtonWidth) / 2, defaultCheckButtonWidth, defaultCheckButtonWidth);
            //文本：位于选中框右侧，留 3 像素间隔，占满剩余宽度
            textRec = new Rectangle(checkRec.Right + 3, 0, Width - checkRec.Right - 6, this.Height);
        }

        /// <summary>
        /// 在选中框内绘制勾选标记（"对勾"形状）
        /// 通过三个点构造折线，使用 DrawLines 一次性绘制完成
        /// </summary>
        /// <param name="graphics">画布对象</param>
        /// <param name="rectangle">选中框矩形区域</param>
        /// <param name="color">勾选标记颜色</param>
        private void DrawCheckedFlag(Graphics graphics, Rectangle rectangle, Color color)
        {
            //构造勾选标记的三个关键点（折线）
            PointF[] pointFs = new PointF[3];

            //第一个点：勾选左下起点
            pointFs[0] = new PointF(rectangle.X + rectangle.Width / 4.5f, rectangle.Y + rectangle.Height / 2.5f);
            //第二个点：勾选中下转折点
            pointFs[1] = new PointF(rectangle.X + rectangle.Width / 2.5f, rectangle.Bottom - rectangle.Height / 3.0f);
            //第三个点：勾选右上终点
            pointFs[2] = new PointF(rectangle.Right - rectangle.Width / 4.0f, rectangle.Y + rectangle.Height / 4.5f);

            //创建宽度为 2 像素的画笔
            Pen pen = new Pen(color, 2.0f);
            //DrawLines：依次连接三个点绘制折线，形成勾选标记
            graphics.DrawLines(pen, pointFs);
        }

    }
}
