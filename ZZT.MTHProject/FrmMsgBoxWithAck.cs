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
    /// 带确认按钮的自定义消息框。
    /// 核心功能：替代系统 MessageBox，提供与项目 UI 风格一致的提示窗体。
    /// 该窗体以模态方式（ShowDialog）显示，用户需主动点击“确认”或“取消”按钮关闭，
    /// 并通过 DialogResult 返回用户的选择结果（OK 或 Cancel）。
    /// 使用场景：需要用户确认的操作，如“导出成功，是否立即打开？”等。
    /// 窗体无边框，通过 Panel_MouseDown/Panel_MouseMove 实现拖动。
    /// </summary>
    public partial class FrmMsgBoxWithAck : System.Windows.Forms.Form
    {
        /// <summary>
        /// 构造函数：根据传入的内容和标题初始化消息框。
        /// </summary>
        /// <param name="content">消息正文内容</param>
        /// <param name="title">消息框标题</param>
        public FrmMsgBoxWithAck(string content,string title)
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
        /// 确认按钮点击事件：返回 DialogResult.OK 通知调用方用户已确认。
        /// </summary>
        private void btn_Sure_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 取消按钮点击事件：返回 DialogResult.Cancel 通知调用方用户已取消。
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 右上角关闭按钮点击事件：等同于取消操作，返回 DialogResult.Cancel。
        /// </summary>
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
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
