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
    /// 仪表盘控件 DialPlate
    /// 用途：继承自 System.Windows.Forms.UserControl，使用 GDI+ 绘制圆形仪表，用于显示温度/湿度数据
    /// 视觉特征：双层圆环结构
    ///   - 外环：上半圆刻度盘，分为报警区段(AlarmColor)和正常区段(RingColor)
    ///   - 内环：双弧显示当前温度(TempColor)与湿度(HumidityColor)
    ///   - 刻度：在外环外侧分布 7 个刻度文字（按 rangeMin~rangeMax 等分）
    /// 使用场景：Modbus TCP 温湿度监控系统的实时数据可视化展示
    /// 绘图流程（OnPaint）：
    ///   1. 用 DrawArc 绘制外环报警段与正常段
    ///   2. TranslateTransform/RotateTransform 将坐标系移到中心并旋转，便于环形布局
    ///   3. 用 FillRectangle 绘制 7 个外环刻度小矩形
    ///   4. 旋转坐标系恢复后，用 DrawString 绘制刻度数字
    ///   5. 用 DrawArc 绘制温度环和湿度环（半圆弧，角度反映实时值占比）
    /// </summary>
    public partial class DialPlate : System.Windows.Forms.UserControl
    {
        /// <summary>
        /// 构造函数：初始化控件并设置字符串格式与控件样式
        /// </summary>
        public DialPlate()
        {
            InitializeComponent();


            //设置控件样式
            //AllPaintingInWmPaint：忽略 WM_ERASEBKGND 消息，减少闪烁
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //DoubleBuffer：启用双缓冲，防止环形动画闪烁
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            //ResizeRedraw：尺寸变化时重绘
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            //Selectable：可接收焦点
            this.SetStyle(ControlStyles.Selectable, true);
            //SupportsTransparentBackColor：支持透明背景
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //刻度文字水平居中
            stringFormat.Alignment = StringAlignment.Center;
            //刻度文字垂直居中
            stringFormat.LineAlignment = StringAlignment.Center;
        }

        //文本对齐格式，用于刻度数字绘制
        private StringFormat stringFormat = new StringFormat();

        #region 外环设计

        //报警区段颜色（外环高亮色）
        private Color alarmColor = Color.FromArgb(36, 184, 196);
        //Browsable/Category/Description 三项设计器特性：在设计器属性面板显示并归类说明
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取报警颜色")]
        public Color AlarmColor
        {
            get { return alarmColor; }
            set
            {
                alarmColor = value;
                this.Invalidate();
            }
        }

        //正常区段颜色（外环底色）
        private Color ringColor = Color.FromArgb(187, 187, 187);
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取圆环整体颜色")]
        public Color RingColor
        {
            get { return ringColor; }
            set
            {
                ringColor = value;
                this.Invalidate();
            }
        }

        //报警角度（0~180），外环上从 180 度起向右扫描的角度区间为报警色
        private float alarmAngle = 120.0f;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取报警角度")]
        public float AlarmAngle
        {
            get { return alarmAngle; }
            set
            {
                alarmAngle = value;
                this.Invalidate();
            }
        }

        //外环宽度（像素）
        private int outThickness = 8;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取外环宽度")]
        public int OutThinckness
        {
            get { return outThickness; }
            set
            {
                outThickness = value;
                this.Invalidate();
            }
        }

        #endregion

        #region 内环设计
        //温度环相对控件宽度的比例（<1.0），决定温度环半径大小
        private float tempScale = 0.7f;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取温度环比例，默认低于1.0f")]
        public float TempScale
        {
            get { return tempScale; }
            set
            {
                //比例超过 1.0f 视为非法，直接返回不修改
                if (value > 1.0f) return;
                tempScale = value;
                this.Invalidate();
            }
        }

        //温度环颜色
        private Color tempColor = Color.FromArgb(36, 184, 196);
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取温度环颜色")]
        public Color TempColor
        {
            get { return tempColor; }
            set
            {
                tempColor = value;
                this.Invalidate();
            }
        }

        //湿度环相对控件宽度的比例（<1.0），决定湿度环半径大小
        private float humidityScale = 0.4f;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取湿度环比例，默认低于1.0f")]
        public float HumidityScale
        {
            get { return humidityScale; }
            set
            {
                if (value > 1.0f) return;
                humidityScale = value;
                this.Invalidate();
            }
        }

        //湿度环颜色
        private Color humidityColor = Color.FromArgb(36, 184, 196);
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取湿度环颜色")]
        public Color HumidityColor
        {
            get { return humidityColor; }
            set
            {
                humidityColor = value;
                this.Invalidate();
            }
        }

        //内环宽度（像素），用于温度环和湿度环的画笔宽度
        private int inThickness = 16;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取内环宽度")]
        public int InThickness
        {
            get { return inThickness; }
            set
            {
                inThickness = value;
                this.Invalidate();
            }
        }

        #endregion

        #region 刻度环设计
        //刻度文字相对控件宽度的比例，决定刻度数字距离中心的远近
        private float textScale = 0.85f;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取刻度环比例，默认低于1.0f")]
        public float TextScale
        {
            get { return textScale; }
            set
            {
                if (value > 1.0f) return;
                textScale = value;
                this.Invalidate();
            }
        }

        //量程下限
        private float rangeMin = 0.0f;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取量程低限")]
        public float RangeMin
        {
            get { return rangeMin; }
            set
            {
                //下限不能高于上限
                if (value > rangeMax) return;
                rangeMin = value;
                this.Invalidate();
            }
        }

        //量程上限
        private float rangeMax = 90.0f;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取量程高限")]
        public float RangeMax
        {
            get { return rangeMax; }
            set
            {
                //上限不能低于下限
                if (value < rangeMin) return;
                rangeMax = value;
                this.Invalidate();
            }
        }
        #endregion

        #region 实时显示
        //温度实时值，绘制时被钳制在 [rangeMin, rangeMax] 范围内
        private float temp = 10.0f;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取温度实时值")]
        public float Temp
        {
            get { return temp; }
            set
            {
                //钳制下限
                if (value < rangeMin)
                {
                    value = rangeMin;
                }
                //钳制上限
                if (value > rangeMax)
                {
                    value = rangeMax;
                }
                temp = value;
                this.Invalidate();
            }
        }

        //湿度实时值，绘制时被钳制在 [rangeMin, rangeMax] 范围内
        private float humidity = 10.0f;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取湿度实时值")]
        public float Humidity
        {
            get { return humidity; }
            set
            {
                if (value < rangeMin)
                {
                    value = rangeMin;
                }
                if (value > rangeMax)
                {
                    value = rangeMax;
                }
                humidity = value;
                this.Invalidate();
            }
        }

        #endregion

        /// <summary>
        /// 重写 OnPaint 方法，使用 GDI+ 自绘仪表盘
        /// 绘图流程：
        /// 1. 获取 Graphics 画布并设置抗锯齿、文字渲染质量
        /// 2. 边界异常判断：尺寸过小或非半圆可行形状时直接返回
        /// 3. 用 Pen + DrawArc 绘制外环报警段和正常段（半圆 180 度到 360 度）
        /// 4. TranslateTransform 将坐标系原点平移到控件中心
        /// 5. RotateTransform 旋转坐标系，便于环形布局刻度
        /// 6. 循环 7 次用 SolidBrush + FillRectangle 绘制外环刻度小矩形
        /// 7. 旋转坐标系回到刻度文字绘制方向
        /// 8. 循环 7 次用 DrawString 绘制刻度数字
        /// 9. 用 DrawArc 绘制温度环和湿度环（角度由实时值占量程比例计算）
        /// </summary>
        /// <param name="e">包含绘图对象和裁剪区域的事件参数</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //获取画布并设置
            Graphics graphics = e.Graphics;
            //抗锯齿，使圆弧更平滑
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //ClearType 文字渲染，使刻度数字更清晰
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            //异常情况处理：尺寸过小或形状无法承载半圆仪表时直接返回
            if (this.Width <= 20 || this.Height <= 20) return;
            if (this.Height < this.Width / 2) return;

            //画外环：报警段，从 180 度起扫描 alarmAngle 度（DrawArc 用 Pen 绘制圆弧）
            Pen pen = new Pen(alarmColor, OutThinckness);
            graphics.DrawArc(pen, new RectangleF(10, 10, this.Width - 20, this.Width - 20), 180, alarmAngle);
            //正常段：从 180+alarmAngle 度起扫描剩余角度
            pen = new Pen(ringColor, OutThinckness);
            graphics.DrawArc(pen, new RectangleF(10, 10, this.Width - 20, this.Width - 20), 180 + alarmAngle, 180 - alarmAngle);

            //转移坐标系：将原点平移到控件中心，便于后续环形布局
            graphics.TranslateTransform(this.Width * 0.5f, this.Width * 0.5f);
            //旋转坐标系：-90 度，使起始方向朝上
            graphics.RotateTransform(-90);

            //循环绘制 7 个外环刻度小矩形（每 30 度一个）
            SolidBrush solidBrush;
            for (int i = 0; i < 7; i++)
            {
                //根据角度位置选择报警色或正常色
                if (30 * i <= alarmAngle)
                {
                    solidBrush = new SolidBrush(alarmColor);
                }
                else
                {
                    solidBrush = new SolidBrush(ringColor);
                }
                //小矩形水平居中（x = -3 即宽度 6 居中）
                float x = -3.0f;
                float width = 6.0f;
                //小矩形高度略大于外环宽度
                float height = outThickness + 4;
                //y 坐标：在外环外侧位置
                float y = (this.Width * 0.5f - 10 + height * 0.5f) * (-1.0f);

                //FillRectangle：用画刷填充刻度小矩形
                graphics.FillRectangle(solidBrush, new RectangleF(x, y, width, height));

                //每绘制一个刻度后顺时针旋转 30 度，准备下一个
                graphics.RotateTransform(30);
            }

            //坐标系旋转回去：抵消前面 7 次共 210 度旋转以及初始 -90 度
            graphics.RotateTransform(-210);
            graphics.RotateTransform(90);

            //绘制刻度：将量程等分为 6 段，生成 7 个刻度值
            float rangeAvg = ((rangeMax - rangeMin) % 6 == 0) ? Convert.ToSingle((rangeMax - rangeMin) / 6) : Convert.ToSingle(((rangeMax - rangeMin) / 6).ToString("f1"));

            for (int i = 0; i < 7; i++)
            {
                //计算当前刻度对应的角度（-180 度到 0 度之间分布）
                float angle = -180f + i * 30.0f;

                //根据角度计算刻度数字所在的 x、y 坐标（极坐标转直角坐标）
                float pointX = Convert.ToSingle(this.Width * textScale * 0.5f * Math.Cos(angle * Math.PI / 180.0f));
                float pointY = Convert.ToSingle(this.Width * textScale * 0.5f * Math.Sin(angle * Math.PI / 180.0f));

                //生成刻度文本
                string text = (rangeMin + rangeAvg * i).ToString();
                //MeasureString：测量文本尺寸，用于居中定位
                SizeF size = graphics.MeasureString(text, this.Font);

                //构造刻度数字的绘制矩形（水平居中）
                RectangleF rectangle = new RectangleF(pointX - size.Width * 0.5f, pointY, size.Width, size.Height);
                //DrawString：绘制刻度数字
                graphics.DrawString(text, this.Font, new SolidBrush(this.ForeColor), rectangle, stringFormat);
            }

            //绘制实际温度湿度环
            //温度环：扫描角度由 (temp - rangeMin) / (rangeMax - rangeMin) * 180 计算
            pen = new Pen(tempColor, inThickness);
            float sweepAngle = (temp - rangeMin) / (rangeMax - rangeMin) * 180.0f;
            //温度环的绘制矩形（左上角坐标为负，使其以中心为圆心）
            float xx = this.Width * tempScale * 0.5f * (-1.0f);
            float yy = this.Width * tempScale * 0.5f * (-1.0f);
            //DrawArc：从 180 度起扫描 sweepAngle 度，绘制温度弧
            graphics.DrawArc(pen, new RectangleF(xx, yy, this.Width * tempScale, this.Width * tempScale), 180.0f, sweepAngle);

            //湿度环：方向与温度环相反（-180 度起扫描）
            pen = new Pen(humidityColor, inThickness);
            sweepAngle = (humidity - rangeMin) / (rangeMax - rangeMin) * 180.0f;
            xx = this.Width * humidityScale * 0.5f * (-1.0f);
            yy = this.Width * humidityScale * 0.5f * (-1.0f);
            //DrawArc：从 -180 度起扫描 sweepAngle 度，绘制湿度弧
            graphics.DrawArc(pen, new RectangleF(xx, yy, this.Width * humidityScale, this.Width * humidityScale), -180.0f, sweepAngle);
        }
    }
}
