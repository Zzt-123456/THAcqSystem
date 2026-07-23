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
    /// 参数修改窗体。
    /// 核心功能：用于修改下位机（Modbus TCP 设备）的某个变量值。
    /// 通过 CommonMethods.CommonWrite 将新值写入到绑定变量 bindVarName。
    /// 使用场景：用户在主界面点击某个可写参数时弹出该窗体，输入新值后确认下发。
    /// 窗体无边框，通过 Panel_MouseDown/Panel_MouseMove 实现拖动。
    /// </summary>
    public partial class FrmModify : Form
    {
        /// <summary>
        /// 默认构造函数（无实际用途，仅为设计器支持）。
        /// </summary>
        public FrmModify()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 带参构造函数：业务实际使用的构造方法。
        /// </summary>
        /// <param name="titleName">窗体标题（如“修改温度上限”）</param>
        /// <param name="bindVarName">绑定的变量标签名（对应下位机变量地址）</param>
        /// <param name="currentValue">当前值（用于在界面上展示）</param>
        public FrmModify(string titleName, string bindVarName, string currentValue)
        {
            InitializeComponent();

            //设置窗体标题为传入的标题名
            this.lbl_Title.Text = titleName;
            //保存待修改变量的标签名，供确认按钮事件使用
            this.bindVarName = bindVarName;
            //在界面上展示当前值
            this.lbl_CurrentValue.Text = currentValue;

            //让输入框获得焦点，方便用户直接输入新值
            this.txt_SetValue.Focus();
        }

        /// <summary>
        /// 变量标签：保存待修改的下位机变量名（与 Modbus 变量映射对应）。
        /// </summary>
        private string bindVarName = string.Empty;

        /// <summary>
        /// 确认按钮点击事件：将输入的新值下发到下位机变量。
        /// </summary>
        private void btn_Sure_Click(object sender, EventArgs e)
        {
            //调用公共方法写入下位机变量，返回是否写入成功
            var result = CommonMethods.CommonWrite(this.bindVarName, this.txt_SetValue.Text.Trim());
            if (result)
            {
                //写入成功：设置 DialogResult 为 OK，关闭窗体并通知调用方
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                //写入失败：弹出无确认按钮的消息框提示
                new FrmMsgBoxWithoutAck("参数修改失败，请检查参数！", "参数修改").Show();
            }
        }

        /// <summary>
        /// 取消按钮点击事件：放弃修改并关闭窗体。
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            //设置 DialogResult 为 Cancel，通知调用方用户取消操作
            this.DialogResult = DialogResult.Cancel;
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
            if (e.Button == MouseButtons.Left)
            {
                //根据鼠标偏移量更新窗体位置
                this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);
            }
        }
        #endregion

        /// <summary>
        /// 输入框键盘按下事件：按下回车键时触发确认按钮逻辑，提升操作便捷性。
        /// </summary>
        private void txt_SetValue_KeyDown(object sender, KeyEventArgs e)
        {
            //按下回车键等同于点击确认按钮
            if(e.KeyCode == Keys.Enter)
            {
                this.btn_Sure_Click(null, null);
            }
        }
    }
}
