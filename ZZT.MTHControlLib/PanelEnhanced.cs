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
    /// 增强面板控件 PanelEnhanced
    /// 用途：继承自 System.Windows.Forms.Panel，重写背景擦除与绘图逻辑，解决面板刷新、放大时的闪烁问题
    /// 视觉特征：当设置了 BackgroundImage 时，将背景图按控件尺寸缩放绘制；未设置时表现为普通面板
    /// 使用场景：Modbus TCP 温湿度监控系统中需要承载背景图且要求无闪烁刷新的容器面板
    /// 实现原理：
    ///   1. 重写 OnPaintBackground 直接 return，禁止基类擦除背景，避免 GDI 先擦除再绘制造成的闪烁
    ///   2. 重写 OnPaint 启用 DoubleBuffered 并使用 DrawImage 绘制背景图，配合双缓冲实现平滑渲染
    /// </summary>
    public partial class PanelEnhanced : System.Windows.Forms.Panel
    {
        /// <summary>
        /// 构造函数：初始化控件
        /// </summary>
        public PanelEnhanced()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 重写背景擦除方法
        /// 直接 return 不执行任何操作，禁止基类的背景擦除行为
        /// 这是消除面板闪烁的关键：避免 GDI 在 OnPaint 之前先用背景色擦除已绘制内容
        /// </summary>
        /// <param name="e">绘图事件参数</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //重载基类的背景擦除方法
            //解决窗体刷新，放大和闪烁
            return;
        }

        /// <summary>
        /// 重写 OnPaint 方法
        /// 绘图流程：
        /// 1. 启用 DoubleBuffered 双缓冲，将绘图先写入内存位图再显示
        /// 2. 若存在 BackgroundImage，设置高质量平滑模式后用 DrawImage 将其缩放绘制到整个面板区域
        /// 3. 调用 base.OnPaint 让基类完成子控件等其他绘制工作
        /// </summary>
        /// <param name="e">包含 Graphics 画布的绘图事件参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //使用双缓冲
            this.DoubleBuffered = true;
            if (this.BackgroundImage != null)
            {
                //设置高质量平滑模式，使背景图缩放后边缘更平滑
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //DrawImage：将背景图按目标矩形(0,0,Width,Height)缩放绘制
                //源矩形使用整张背景图(0,0,背景图宽,背景图高)，GraphicsUnit.Pixel 指定以像素为单位
                e.Graphics.DrawImage(this.BackgroundImage, new Rectangle
                (0, 0, this.Width, this.Height), 0, 0, this.BackgroundImage.Width, this.BackgroundImage.Height, GraphicsUnit.Pixel);
            }
            //调用基类 OnPaint，确保子控件正常绘制
            base.OnPaint(e);
        }

    }
}
