using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZZT.MTHBLL;
using ZZT.MTHHelper;
using ZZT.MTHModels;

namespace ZZT.MTHProject
{
    /// <summary>
    /// 用户管理窗体。
    /// 核心功能：对系统用户账号进行增加、修改、删除、查询（CRUD）操作，
    /// 并配置每个用户对各功能模块（参数设置、配方、历史日志、历史趋势、用户管理）的访问权限。
    /// 数据通过 BLL 层操作数据库 SysAdmin 表（密码以明文存储，仅用于项目演示）。
    /// 使用场景：管理员维护系统用户列表、调整权限时使用。
    /// </summary>
    public partial class FrmUserManage : System.Windows.Forms.Form
    {
        /// <summary>
        /// 构造函数：初始化控件、配置 DataGridView 列、加载用户列表。
        /// </summary>
        public FrmUserManage()
        {
            //设计器自动生成的初始化
            InitializeComponent();

            //禁止 DataGridView 根据数据源自动生成列，仅显示设计器中预定义的列
            this.dgv_UserManage.AutoGenerateColumns = false;

            //首次加载用户列表数据
            UpdateData();
        }

        //系统用户业务层对象，封装了对 SysAdmin 表的增删改查方法
        private SysAdminManage sysAdminManage = new SysAdminManage();
        //当前从数据库查询出的用户列表（缓存，便于增删改后刷新界面）
        private List<SysAdmin> sysAdmins = new List<SysAdmin>();

        /// <summary>
        /// 刷新用户列表：从数据库重新查询所有用户并绑定到 DataGridView。
        /// </summary>
        private void UpdateData()
        {
            //查询所有用户账号
            sysAdmins = sysAdminManage.QuerySysAdmins();
            if (sysAdmins.Count > 0)
            {
                //先清空再绑定，触发 DataGridView 重新渲染
                this.dgv_UserManage.DataSource = null;
                this.dgv_UserManage.DataSource = sysAdmins;
            }
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Add_Click(object sender, EventArgs e)
        {
            //数据验证：用户名不能为空
            if (this.txt_LoginName.Text.Length == 0)
            {
                new FrmMsgBoxWithoutAck("用户名不能为空！", "添加用户").Show();
                return;
            }

            //数据验证：密码不能为空
            if (this.txt_LoginPwd.Text.Length == 0)
            {
                new FrmMsgBoxWithoutAck("用户密码不能为空！", "添加用户").Show();
                return;
            }

            //数据验证：确认密码不能为空
            if (this.txt_LoginPwd2.Text.Length == 0)
            {
                new FrmMsgBoxWithoutAck("确认密码不能为空！", "添加用户").Show();
                return;
            }

            //数据验证：两次输入的密码必须一致
            if (this.txt_LoginPwd.Text.Trim() != this.txt_LoginPwd2.Text.Trim())
            {
                new FrmMsgBoxWithoutAck("两次输入密码不一致！", "添加用户").Show();
                return;
            }

            //数据验证：用户名不能重复
            if (sysAdmins.Where(c => c.LoginName == this.txt_LoginName.Text.Trim()).ToList().Count > 0)
            {
                new FrmMsgBoxWithoutAck("该用户名已经存在！", "添加用户").Show();
                return;
            }

            //构建新增用户对象（密码明文存储，仅用于项目演示场景）
            SysAdmin sysAdmin = new SysAdmin()
            {
                LoginName = this.txt_LoginName.Text.Trim(),
                LoginPwd = this.txt_LoginPwd.Text.Trim(),
                //以下为各功能模块的访问权限
                ParamSet = this.chk_ParamSet.Checked,
                Recipe = this.chk_Recipe.Checked,
                HistoryLog = this.chk_HistoryLog.Checked,
                HistoryTrend = this.chk_HistoryTrend.Checked,
                UserManage = this.chk_UserManage.Checked
            };

            //调用 BLL 层添加用户
            if (sysAdminManage.AddSysAdmin(sysAdmin))
            {
                //添加成功：刷新用户列表
                UpdateData();
            }
            else
            {
                //添加失败：弹出提示
                new FrmMsgBoxWithoutAck("添加用户失败，请检查！", "添加用户").Show();
            }
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Modify_Click(object sender, EventArgs e)
        {
            //数据验证：用户名不能为空
            if (this.txt_LoginName.Text.Length == 0)
            {
                new FrmMsgBoxWithoutAck("用户名不能为空！", "添加用户").Show();
                return;
            }

            //数据验证：密码不能为空
            if (this.txt_LoginPwd.Text.Length == 0)
            {
                new FrmMsgBoxWithoutAck("用户密码不能为空！", "添加用户").Show();
                return;
            }

            //数据验证：确认密码不能为空
            if (this.txt_LoginPwd2.Text.Length == 0)
            {
                new FrmMsgBoxWithoutAck("确认密码不能为空！", "添加用户").Show();
                return;
            }

            //数据验证：两次输入的密码必须一致
            if (this.txt_LoginPwd.Text.Trim() != this.txt_LoginPwd2.Text.Trim())
            {
                new FrmMsgBoxWithoutAck("两次输入密码不一致！", "添加用户").Show();
                return;
            }

            //数据验证：用户名不能重复（注：此处为原始代码逻辑，保留以兼容）
            if (sysAdmins.Where(c => c.LoginName == this.txt_LoginName.Text.Trim()).ToList().Count > 0)
            {
                new FrmMsgBoxWithoutAck("该用户名已经存在！", "添加用户").Show();
                return;
            }

            //判断是否修改用户名称：若改了用户名，需要再次校验新用户名是否已存在
            if (sysAdmins[this.dgv_UserManage.SelectedRows[0].Index].LoginName != this.txt_LoginName.Text.Trim())
            {
                if (sysAdmins.Where(c => c.LoginName == this.txt_LoginName.Text.Trim()).ToList().Count > 0)
                {
                    new FrmMsgBoxWithoutAck("该用户名已经存在！", "修改用户").Show();
                    return;
                }
            }

            //构建修改用户对象，LoginId 取自当前选中的行
            SysAdmin sysAdmin = new SysAdmin()
            {
                LoginId = sysAdmins[this.dgv_UserManage.SelectedRows[0].Index].LoginId,
                LoginName = this.txt_LoginName.Text.Trim(),
                LoginPwd = this.txt_LoginPwd.Text.Trim(),
                //以下为各功能模块的访问权限
                ParamSet = this.chk_ParamSet.Checked,
                Recipe = this.chk_Recipe.Checked,
                HistoryLog = this.chk_HistoryLog.Checked,
                HistoryTrend = this.chk_HistoryTrend.Checked,
                UserManage = this.chk_UserManage.Checked
            };

            //调用 BLL 层修改用户
            if (sysAdminManage.ModifySysAdmin(sysAdmin))
            {
                //修改成功：刷新用户列表
                UpdateData();
            }
            else
            {
                //修改失败：弹出提示
                new FrmMsgBoxWithoutAck("修改用户失败，请检查！", "修改用户").Show();
            }
        }

        /// <summary>
        /// 删除按钮点击事件：删除当前选中的用户。
        /// </summary>
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            //根据选中行的 LoginId 调用 BLL 层删除用户
            if (sysAdminManage.DeleteSysAdmin(sysAdmins[this.dgv_UserManage.SelectedRows[0].Index].LoginId))
            {
                //删除成功：刷新用户列表
                UpdateData();
            }
            else
            {
                //删除失败：弹出提示
                new FrmMsgBoxWithoutAck("删除用户失败，请检查！", "删除用户").Show();
            }
        }

        /// <summary>
        /// 清空按钮点击事件：清空输入框和权限勾选项，便于重新录入。
        /// </summary>
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            //清空三个文本框
            this.txt_LoginName.Clear();
            this.txt_LoginPwd.Clear();
            this.txt_LoginPwd2.Clear();

            //取消所有权限勾选
            SetChecked(false);
        }

        /// <summary>
        /// 统一设置所有权限复选框的选中状态。
        /// </summary>
        /// <param name="value">true=全部勾选，false=全部取消</param>
        private void SetChecked(bool value)
        {
            this.chk_ParamSet.Checked = value;
            this.chk_Recipe.Checked = value;
            this.chk_HistoryLog.Checked = value;
            this.chk_HistoryTrend.Checked = value;
            this.chk_UserManage.Checked = value;
        }

        /// <summary>
        /// 全选按钮点击事件：根据当前“参数设置”权限状态取反，实现全选/全不选切换。
        /// </summary>
        private void btn_SelectAll_Click(object sender, EventArgs e)
        {
            //取反当前 chk_ParamSet 状态作为目标状态
            SetChecked(!this.chk_ParamSet.Checked);
        }

        /// <summary>
        /// DataGridView 行绘制完成后事件：在行头显示行号。
        /// </summary>
        private void dgv_UserManage_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //调用辅助类统一绘制行号
            DataGridViewHelper.DgvRowPostPaint(sender as DataGridView, e);
        }

        /// <summary>
        /// DataGridView 单元格点击事件：点击某行时将该行用户信息回填到输入框，便于修改。
        /// </summary>
        private void dgv_UserManage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //排除点击表头（RowIndex < 0）的情况
            if (e.RowIndex >= 0)
            {
                //根据行索引从缓存列表取出用户并回填
                UpdateInfo(sysAdmins[e.RowIndex]);
            }
        }

        /// <summary>
        /// 将用户信息回填到输入框和权限复选框，供用户查看和修改。
        /// </summary>
        /// <param name="sysAdmin">要回填的用户对象</param>
        private void UpdateInfo(SysAdmin sysAdmin)
        {
            if (sysAdmin != null)
            {
                //回填基本信息：用户名、密码、确认密码（两次均显示原密码）
                this.txt_LoginName.Text = sysAdmin.LoginName;
                this.txt_LoginPwd.Text = sysAdmin.LoginPwd;
                this.txt_LoginPwd2.Text = sysAdmin.LoginPwd;
                //回填各功能模块的访问权限
                this.chk_ParamSet.Checked = sysAdmin.ParamSet;
                this.chk_Recipe.Checked = sysAdmin.Recipe;
                this.chk_HistoryLog.Checked = sysAdmin.HistoryLog;
                this.chk_HistoryTrend.Checked = sysAdmin.HistoryTrend;
                this.chk_UserManage.Checked = sysAdmin.UserManage;
            }
        }

        /// <summary>
        /// DataGridView 单元格格式化事件：将布尔权限列显示为“启用/禁用”。
        /// </summary>
        private void dgv_UserManage_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //仅处理数据行（RowIndex >= 0）和权限列（ColumnIndex >= 2，跳过 LoginId 和 LoginName 列）
            if (e.RowIndex >= 0 && e.ColumnIndex >= 2)
            {
                object value = this.dgv_UserManage.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (value != null)
                {
                    //true 显示为“启用”，false 显示为“禁用”
                    if (value.ToString().ToLower() == "true")
                    {
                        e.Value = "启用";
                    }
                    else
                    {
                        e.Value = "禁用";
                    }
                }
            }
        }
    }
}
