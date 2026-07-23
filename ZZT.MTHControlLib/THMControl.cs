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
    /// 温湿度监控控件（THMControl）：用于实时显示单个站点的温度、湿度及模块故障状态。
    /// 视觉特征：顶部为标题栏 lbl_Title（正常为青色背景 #24B8C4，模块故障时变红），
    ///   中部为 dialPlate 表盘控件（GDI+ 自绘的温湿度双指针表盘），
    ///   下方为 lbl_Temp、lbl_Humidity 两个数值标签实时显示温湿度读数。
    /// 使用场景：在主监控界面中以网格形式铺排多个该控件，每个对应一个 Modbus TCP 通道，
    ///   通过 Temp/Humidity 属性接收实时采集值并刷新表盘与数值显示，
    ///   通过 ModuleError 属性反映通信/模块故障并变色告警。
    /// 绘图说明：表盘部分的 GDI+ 绘制（DrawString/DrawRectangle/FillRectangle/Pen/Brush 等）
    ///   由内部 dialPlate 子控件负责，本控件通过 SetStyle 启用双缓冲优化整体刷新。
    /// </summary>
    public partial class THMControl : System.Windows.Forms.UserControl
    {
        /// <summary>
        /// 构造函数：初始化组件并设置控件样式。
        /// </summary>
        public THMControl()
        {
            InitializeComponent();
            //设置控件样式：以下 SetStyle 调用用于优化 GDI+ 绘图性能与表现
            //AllPaintingInWmPaint：忽略 WM_ERASEBKGND，仅在 WM_PAINT 中完成全部绘制，减少闪烁
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //DoubleBuffer：启用双缓冲，先在内存位图绘制再输出到屏幕，避免表盘重绘闪烁
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            //ResizeRedraw：尺寸变化时整体重绘，保证表盘 GDI+ 内容随缩放正确刷新
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            //Selectable：允许控件接收焦点
            this.SetStyle(ControlStyles.Selectable, true);
            //SupportsTransparentBackColor：支持透明背景色，便于与父容器叠加
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        //温度值字段：字符串形式，默认“0.0”，同时驱动数值标签与表盘指针
        private string temp = "0.0";

        /// <summary>
        /// 温度值属性：设置或显示当前温度读数。
        /// 设计器特性：[Browsable(true)] 属性面板可见；[Category("自定义属性")] 归类；
        ///   [Description(...)] 属性面板说明文字。
        /// 赋值时同步刷新 lbl_Temp 文本，并尝试解析为 float 驱动表盘指针；
        ///   解析失败时表盘温度归零，避免异常。
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示温度值")]
        public string Temp
        {
            get { return temp; }
            set
            {
                //仅当值变化时才刷新，避免无谓重绘
                if (temp != value)
                {
                    temp = value;
                    //刷新温度数值标签
                    this.lbl_Temp.Text = temp;

                    //尝试将字符串解析为浮点数以驱动表盘指针
                    if (float.TryParse(temp, out float val))
                    {
                        this.dialPlate.Temp = val;
                    }
                    else
                    {
                        //解析失败时表盘温度归零（容错处理）
                        this.dialPlate.Temp = 0.0f;
                    }
                }
            }
        }

        //湿度值字段：字符串形式，默认“0.0”，同时驱动数值标签与表盘指针
        private string humidity = "0.0";

        /// <summary>
        /// 湿度值属性：设置或显示当前湿度读数。
        /// 设计器特性同 Temp：[Browsable(true)]/Category/Description。
        /// 赋值时同步刷新 lbl_Humidity 文本，并尝试解析为 float 驱动表盘指针；
        ///   解析失败时表盘湿度归零。
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示湿度值")]
        public string Humidity
        {
            get { return humidity; }
            set
            {
                //仅当值变化时才刷新
                if (humidity != value)
                {
                    humidity = value;
                    //刷新湿度数值标签
                    this.lbl_Humidity.Text = humidity;

                    //尝试解析为浮点数以驱动表盘指针
                    if (float.TryParse(humidity, out float val))
                    {
                        this.dialPlate.Humidity = val;
                    }
                    else
                    {
                        //解析失败时表盘湿度归零（容错处理）
                        this.dialPlate.Humidity = 0.0f;
                    }
                }
            }
        }
        

        //模块故障状态字段：默认 false（正常），true 时标题栏背景变红告警
        private bool moduleError = false;

        /// <summary>
        /// 模块故障状态属性：设置或显示模块是否故障。
        /// 设计器特性：[Browsable(true)]/Category/Description。
        /// 故障时（true）将 lbl_Title 背景色设为 Color.Red（红色告警），
        /// 正常时（false）恢复为青色 Color.FromArgb(36, 184, 196)。
        /// 该属性属于布尔状态，直接改写子控件 BackColor 触发重绘，无需额外 Invalidate。
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置模块故障状态，故障时标题背景变红")]
        public bool ModuleError
        {
            get { return moduleError; }
            set
            {
                moduleError = value;
                //故障时标题背景变红，正常时恢复青色（GDI+ 通过 BackColor 触发重绘）
                this.lbl_Title.BackColor = moduleError ? Color.Red : Color.FromArgb(36, 184, 196);
            }
        }

        //站点名标题字段：默认“1#站点”，显示在顶部标题栏
        private string title = "1#站点";

        /// <summary>
        /// 站点名属性：设置或显示当前站点名称（标题栏文字）。
        /// 设计器特性：[Browsable(true)]/Category/Description。
        /// 赋值时同步刷新 lbl_Title 文本。
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示站点名")]
        public string Title
        {
            get { return title; }
            set
            {
                //仅当值变化时才刷新
                if (title != value)
                {
                    title = value;
                    //刷新标题栏文字
                    this.lbl_Title.Text = title;
                }
            }
        }

        /// <summary>
        /// 温度绑定变量名属性：用于与 Modbus 数据源建立绑定关系（温度变量名）。
        /// 设计器特性：[Browsable(true)]/Category/Description，自动实现属性。
        /// 该名称仅作绑定标识，不直接参与 GDI+ 绘制，由上层通信层据此读写对应寄存器。
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示温度绑定变量名称")]
        public string TempVarName { get; set; } = string.Empty;

        /// <summary>
        /// 湿度绑定变量名属性：用于与 Modbus 数据源建立绑定关系（湿度变量名）。
        /// 设计器特性：[Browsable(true)]/Category/Description，自动实现属性。
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示湿度绑定变量名称")]
        public string HumidityVarName { get; set; } = string.Empty;

        /// <summary>
        /// 状态绑定变量名属性：用于与 Modbus 数据源建立绑定关系（模块状态变量名），
        ///   通常对应模块故障标志位，由上层据此更新 ModuleError 属性。
        /// 设计器特性：[Browsable(true)]/Category/Description，自动实现属性。
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示状态绑定变量名称")]
        public string StateVarName { get; set; } = string.Empty;
    }
}
