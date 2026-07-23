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
    /// 参数设置控件（TextSet）：FrmParamSet 参数设置界面的核心控件。
    /// 视觉特征：左侧为标题标签 lbl_Title（说明该参数含义，如“1#站点温度高限”），
    ///   中间为数值标签 lbl_Value 显示当前值，右侧为单位标签 lbl_Unit（如“℃”），
    ///   并带有一颗 led_Alarm 报警 LED 指示灯（绿色=正常，红色=报警）。
    /// 使用场景：在参数设置界面中成组排列，每个控件对应一个监控点的限值参数。
    ///   通过 BindVarName 绑定对应变量名，AlarmVarName 绑定报警变量名，
    ///   IsAlarm 控制报警 LED 颜色（true 时 LED 变红，false 时变绿），
    ///   双击数值标签可触发 ControlDoubleClick 事件以弹出编辑窗修改限值。
    /// 绘图说明：LED 灯的 GDI+ 绘制（FillEllipse/Brush 等）由内部 led_Alarm 子控件负责，
    ///   设置 led_Alarm.Value 后由其内部 Invalidate() 重绘；本控件通过 SetStyle 启用双缓冲。
    /// 该控件以 ControlDoubleClick 为默认事件（[DefaultEvent]），双击设计器中的控件
    ///   会自动生成该事件的处理函数骨架。
    /// </summary>
    [DefaultEvent("ControlDoubleClick")]
    public partial class TextSet : UserControl
    {
        /// <summary>
        /// 构造函数：初始化组件并设置控件样式。
        /// </summary>
        public TextSet()
        {
            InitializeComponent();

            //设置控件样式：以下 SetStyle 调用用于优化 GDI+ 绘图性能与表现
            //AllPaintingInWmPaint：忽略 WM_ERASEBKGND，仅在 WM_PAINT 中完成全部绘制，减少闪烁
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //DoubleBuffer：启用双缓冲，先在内存位图绘制再输出到屏幕，避免 LED/文字重绘闪烁
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            //ResizeRedraw：尺寸变化时整体重绘，保证 GDI+ 自绘内容随缩放正确刷新
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            //Selectable：允许控件接收焦点
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

        //绑定变量名字段：默认“模块1温度高限”，用于与数据源建立绑定关系（不直接显示）
        private string bindVarName = "模块1温度高限";

        /// <summary>
        /// 绑定变量名属性：设置或显示该参数对应的绑定变量名称。
        /// 设计器特性：[Browsable(true)]/Category/Description。
        /// 该名称仅作绑定标识，由上层通信层据此读写对应 Modbus 寄存器，不直接参与 GDI+ 绘制。
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示绑定变量名称")]
        public string BindVarName
        {
            get { return bindVarName; }
            set
            {
                bindVarName = value;
            }
        }

        //当前数值字段：字符串形式，默认“0.0”，显示在数值标签上
        private string currentValue = "0.0";

        /// <summary>
        /// 当前数值属性：设置或显示当前参数值（字符串形式）。
        /// 设计器特性：[Browsable(true)]/Category/Description。
        /// 赋值时仅当值变化才刷新 lbl_Value 文本，避免无谓重绘。
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示当前数值")]
        public string CurrentValue
        {
            get { return currentValue; }
            set
            {
                //仅当值变化时才刷新
                if (currentValue != value)
                {
                    currentValue = value;
                    //刷新数值标签文字
                    this.lbl_Value.Text = currentValue;
                }
            }
        }

        //报警绑定变量名字段：默认“模块1温度高”，用于绑定该参数的报警位
        private string alarmVarName = "模块1温度高";

        /// <summary>
        /// 报警绑定变量名属性：设置或显示该参数对应的报警变量名称。
        /// 设计器特性：[Browsable(true)]/Category/Description。
        /// 该名称仅作绑定标识，由上层通信层据此读取报警状态并更新 IsAlarm 属性，不直接参与绘制。
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示报警绑定变量名称")]
        public string AlarmVarName
        {
            get { return alarmVarName; }
            set
            {
                alarmVarName = value;
            }
        }

        //报警状态字段：false=正常（LED 绿），true=报警（LED 红）
        private bool isAlarm;

        /// <summary>
        /// 报警状态属性：设置或显示当前是否处于报警状态。
        /// 设计器特性：[Browsable(true)]/Category/Description。
        /// 报警逻辑：true 时 LED 变红，false 时 LED 变绿。
        /// 赋值时仅当状态变化才更新 led_Alarm.Value；根据项目约定，led_Alarm 内部会在
        ///   Value 变化时调用 Invalidate() 触发 GDI+ 重绘，从而切换 LED 颜色。
        /// </summary>
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示当前报警状态")]
        public bool IsAlarm
        {
            get { return isAlarm; }
            set
            {
                //仅当状态变化时才更新，避免无谓重绘
                if(isAlarm != value)
                {
                    isAlarm = value;
                    //更新 LED 子控件的值，由其内部 Invalidate() 重绘切换红/绿色
                    this.led_Alarm.Value = isAlarm;
                }
            }
        }

        //控件双击事件：标准事件声明语法（public event EventHandler），双击数值标签时触发，
        //  供上层弹出参数编辑窗。注意：勿用 new 关键字隐藏事件字段。
        public event EventHandler ControlDoubleClick;
        //注：以下三个特性原意是为 ControlDoubleClick 事件添加设计器元数据，
        //  但代码位置使其作用于下方方法（属原有逻辑，此处仅注释说明，不改动）。
        [Browsable(true)]
        [Category("自定义事件")]
        [Description("设置控件双击事件")]

        /// <summary>
        /// 数值标签双击事件处理：当用户双击 lbl_Value 数值标签时，
        ///   将双击事件转发为控件的 ControlDoubleClick 事件，供上层处理（如弹出编辑窗）。
        /// </summary>
        /// <param name="sender">事件发送者（lbl_Value）</param>
        /// <param name="e">事件参数</param>
        private void lbl_Value_DoubleClick(object sender, EventArgs e)
        {
            //使用空条件运算符安全触发事件，无订阅者时不抛异常
            ControlDoubleClick?.Invoke(this, e);
        }
    }
}
