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
    /// 导航按钮控件 NaviButton
    /// 用途：继承自 System.Windows.Forms.UserControl，用于主窗体(FrmMain)左侧导航菜单的按钮项
    /// 视觉特征：通过切换背景图片实现"选中/未选中"状态视觉差异，支持左侧和右侧两种样式
    ///           控件内部嵌入一个 Label(lbl_Title) 显示按钮文字
    /// 使用场景：Modbus TCP 温湿度监控系统主界面的功能模块切换（如设备列表、实时监控、报警记录等）
    /// 交互逻辑：通过 IsSelected 切换选中背景图，IsLeft 决定使用左侧或右侧资源图片
    ///           标签点击事件转发为控件自身的 Click 事件，方便外部统一订阅
    /// 设计器特性：[DefaultEvent("Click")] 指定双击控件时默认生成 Click 事件处理函数
    /// </summary>
    [DefaultEvent("Click")]
    public partial class NaviButton : System.Windows.Forms.UserControl
    {
        /// <summary>
        /// 构造函数：初始化控件并设置控件样式
        /// </summary>
        public NaviButton()
        {
            InitializeComponent();

            //设置控件样式
            //AllPaintingInWmPaint：忽略 WM_ERASEBKGND 消息，减少闪烁
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //DoubleBuffer：启用双缓冲，背景图切换更平滑
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            //ResizeRedraw：尺寸变化时自动重绘
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            //Selectable：可接收焦点
            this.SetStyle(ControlStyles.Selectable, true);
            //SupportsTransparentBackColor：支持透明背景，便于背景图边缘平滑过渡
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        //是否处于选中状态，true 时显示选中背景图
        private bool isSelected = false;
        //Browsable(true)：在设计器属性面板显示该属性
        //Category：归类到"自定义属性"分组
        //Description：属性说明文字（设计器底部显示）
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示导航按钮是否选中")]

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                //状态变化后立即更新背景图
                UpdateImage();
            }
        }

        //是否为左侧导航按钮（true=左侧，false=右侧），决定使用哪一组背景图资源
        private bool isLeft = true;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示导航按钮是否为左边")]

        public bool IsLeft
        {
            get { return isLeft; }
            set
            {
                isLeft = value;
                //方向变化后立即更新背景图
                UpdateImage();
            }
        }


        /// <summary>
        /// 统一更新背景
        /// 根据 isLeft 和 isSelected 组合，从项目资源文件中选取对应的背景图赋值给 BackgroundImage
        /// 资源命名约定：LeftSelected/LeftUnSelected/RightSelected/RightUnSelected
        /// </summary>
        private void UpdateImage()
        {
            if (this.isLeft)
            {
                //左侧按钮：根据选中状态选择 LeftSelected 或 LeftUnSelected
                this.BackgroundImage = isSelected ? Properties.Resources.LeftSelected : Properties.Resources.LeftUnSelected;
            }
            else
            {
                //右侧按钮：根据选中状态选择 RightSelected 或 RightUnSelected
                this.BackgroundImage = isSelected ? Properties.Resources.RightSelected : Properties.Resources.RightUnSelected;
            }
        }

        //按钮显示文字，默认"导航按钮"
        private string titleName = "导航按钮";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或显示导航按钮文本内容")]

        public string TitleName
        {
            get { return titleName; }
            set
            {
                titleName = value;
                //同步将文本写入内部 Label 控件显示
                this.lbl_Title.Text = titleName;
            }
        }

        /// <summary>
        /// 内部 Label 的点击事件处理
        /// 将 Label 的点击转发为 NaviButton 控件自身的 Click 事件
        /// 这样外部订阅 NaviButton.Click 即可响应用户点击文字区域的操作
        /// </summary>
        /// <param name="sender">事件发送者（内部 Label）</param>
        /// <param name="e">事件参数</param>
        private void lbl_Title_Click(object sender, EventArgs e)
        {
            //触发基类 OnClick，从而引发 NaviButton.Click 事件
            base.OnClick(e);
        }
    }

}

