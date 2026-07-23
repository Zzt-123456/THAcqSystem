using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZZT.MTHProject
{
    /// <summary>
    /// 仅提示的自定义消息框（无确认按钮）。
    /// 核心功能：替代系统 MessageBox，用于显示纯提示信息（如错误、警告）。
    /// 该窗体通过 Show 方法非模态显示，不返回 DialogResult，
    /// 用户点击“确定”或右上角关闭按钮后窗体关闭。
    /// 使用场景：仅作信息提示，无需用户确认的场景，如“查询失败”、“用户名不能为空”等。
    /// 窗体无边框，通过 Panel_MouseDown/Panel_MouseMove 实现拖动。
    /// </summary>
    public partial class FrmMsgBoxWithoutAck : Form
    {
        /// <summary>
        /// 构造函数：根据传入的内容和标题初始化消息框。
        /// </summary>
        /// <param name="content">消息正文内容</param>
        /// <param name="title">消息框标题</param>
        public FrmMsgBoxWithoutAck(string content,string title)
        {
            InitializeComponent();

            //将消息框置顶显示，确保用户能注意到提示
            this.TopMost = true;
            //设置消息正文
            this.lbl_Content.Text = content;
            //设置消息框标题
            this.lbl_Title.Text = title;

        }

        /// <summary>
        /// 确认按钮点击事件：直接关闭窗体（无 DialogResult 返回值）。
        /// </summary>
        private void btn_Sure_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 右上角关闭按钮点击事件：直接关闭窗体。
        /// </summary>
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 无边框拖动

        //记录鼠标按下时的坐标，用于计算窗体拖动偏移量
        private Point mPoint;

        /// <summary>
        /// 标题面板鼠标按下事件：记录鼠标起始位置。
        /// </summary>
        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint = new Point(e.X, e.Y);
        }

        /// <summary>
        /// 标题面板鼠标移动事件：按住左键时拖动整个窗体。
        /// </summary>
        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            //仅在鼠标左键按下时拖动，避免误触发
            if(e.Button == MouseButtons.Left)
            {
                //根据鼠标偏移量更新窗体位置
                this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);
            }
        }
        #endregion


    }
}
