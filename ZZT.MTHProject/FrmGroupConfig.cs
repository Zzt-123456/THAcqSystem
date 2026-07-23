using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZZT.MTHProject;
using ZZT.MTHHelper;
using ZZT.MTHModels;

namespace ZZT.MTHProject
{
    public partial class FrmGroupConfig : System.Windows.Forms.Form
    {
        /// <summary>
        /// 通信组配置窗体构造函数
        /// 流程：初始化控件 → 绑定存储区下拉框 → 设置DataGridView不自动生成列 →
        ///       检查/创建Config目录 → 加载已有通信组 → 刷新列表显示
        /// </summary>
        public FrmGroupConfig()
        {
            InitializeComponent();

            //绑定存储区下拉框数据源：4种Modbus存储区类型
            this.cmb_StoreArea.DataSource = new string[] { "输入线圈", "输出线圈", "输入寄存器", "输出寄存器" };

            //禁止DataGridView根据数据源自动生成列（列由设计器预先配置）
            this.dgv_Main.AutoGenerateColumns = false;

            //确保Config目录存在，不存在则创建
            string configDir = Path.GetDirectoryName(groupPath);
            if (!Directory.Exists(configDir))
            {
                Directory.CreateDirectory(configDir);
            }

            //从Excel读取所有已配置的通信组
            TotalGroups = GetAllGroups();

            //刷新DataGridView显示
            RefreshGroup();
        }

        // Group.xlsx配置文件路径：存储所有通信组定义
        private string groupPath = Application.StartupPath + "\\Config\\Group.xlsx";

        // 内存中缓存的全部通信组列表
        private List<Group> TotalGroups = new List<Group>();

        #region 获取所有通信组
        /// <summary>
        /// 获取所有通信组：从Group.xlsx读取全部通信组记录
        /// 文件不存在则返回空列表；读取异常弹窗提示并返回空列表
        /// </summary>
        /// <returns>通信组列表</returns>
        private List<Group> GetAllGroups()
        {
            try
            {
                //文件不存在直接返回空集合（首次运行场景）
                if (!File.Exists(groupPath))
                {
                    return new List<Group>();
                }
                //使用MiniExcel将Excel每行映射为Group对象
                return MiniExcel.Query<Group>(groupPath).ToList();
            }
            catch (Exception ex)
            {
                new FrmMsgBoxWithoutAck("读取通信组配置失败：" + ex.Message, "读取配置").Show();
                return new List<Group>();
            }

        }
        #endregion

        #region 更新通信组集合
        /// <summary>
        /// 刷新DataGridView显示：重新绑定TotalGroups到列表
        /// 先置空再绑定，确保界面刷新
        /// </summary>
        private void RefreshGroup()
        {
            if (TotalGroups != null && TotalGroups.Count > 0)
            {
                this.dgv_Main.DataSource = null;
                this.dgv_Main.DataSource = TotalGroups;
            }
        }
        #endregion

        #region 判断组名称是否存在
        /// <summary>
        /// 查重：判断指定通信组名称是否已存在
        /// </summary>
        /// <param name="groupName">待校验的通信组名称</param>
        /// <returns>存在返回true，否则false</returns>
        private bool IsGroupNameExits(string groupName)
        {
            return TotalGroups.FindAll(c => c.GroupName == groupName).ToList().Count > 0;
        }
        #endregion

        #region 增删改通信组
        /// <summary>
        /// 添加通信组按钮点击事件
        /// 流程：非空验证 → 查重 → 构造Group对象加入列表 → 保存到Excel → 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Add_Click(object sender, EventArgs e)
        {
            string groupName = this.txt_GroupName.Text.Trim();

            //非空验证
            if (groupName.Length == 0)
            {
                new FrmMsgBoxWithoutAck("通信组名称不能为空！", "添加通信组").Show();
                return;
            }

            //查重：名称必须唯一
            if (IsGroupNameExits(groupName))
            {
                new FrmMsgBoxWithoutAck("通信组名称已经存在！", "添加通信组").Show();
                return;
            }

            //构造新通信组对象并加入内存列表
            TotalGroups.Add(new Group()
            {
                GroupName = groupName,
                Start = Convert.ToUInt16(this.num_Start.Text),
                Length = Convert.ToUInt16(this.num_Length.Text),
                StoreArea = this.cmb_StoreArea.Text.Trim(),
                Remark = this.txt_Remark.Text.Trim(),
            });

            try
            {
                //全量覆盖保存到Excel（覆盖原文件）
                MiniExcel.SaveAs(groupPath, TotalGroups, overwriteFile: true);

                //刷新数据
                RefreshGroup();
            }
            catch (Exception ex)
            {
                new FrmMsgBoxWithoutAck("添加通信组失败：" + ex.Message, "添加通信组").Show();
            }

        }

        /// <summary>
        /// 删除通信组按钮点击事件
        /// 流程：查存在 → 从列表移除 → 保存到Excel → 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            string groupName = this.txt_GroupName.Text.Trim();
            //查存在：删除前必须先存在
            if (!IsGroupNameExits(groupName))
            {
                new FrmMsgBoxWithoutAck("通信组名称不存在！", "删除通信组").Show();
                return;
            }

            //从内存列表移除匹配项
            TotalGroups.RemoveAll(c => c.GroupName == groupName);
            try
            {
                //全量覆盖保存到Excel
                MiniExcel.SaveAs(groupPath, TotalGroups, overwriteFile: true);
                //刷新数据
                RefreshGroup();
            }
            catch (Exception ex)
            {
                new FrmMsgBoxWithoutAck("删除通信组失败：" + ex.Message, "删除通信组").Show();
            }
        }

        /// <summary>
        /// 修改通信组按钮点击事件
        /// 流程：查存在 → 找到对象并更新字段 → 保存到Excel → 刷新
        /// 注意：通信组名称不可修改（作为主键定位记录）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Modify_Click(object sender, EventArgs e)
        {
            string groupName = this.txt_GroupName.Text.Trim();
            //查存在
            if (!IsGroupNameExits(groupName))
            {
                new FrmMsgBoxWithoutAck("通信组名称不存在！", "修改通信组").Show();
                return;
            }

            //按名称查找并更新除名称外的其他字段
            var group = TotalGroups.Find(c => c.GroupName == groupName);
            group.Start = Convert.ToUInt16(this.num_Start.Value);
            group.Length = Convert.ToUInt16(this.num_Length.Value);
            group.StoreArea = this.cmb_StoreArea.Text.Trim();
            group.Remark = this.txt_Remark.Text.Trim();

            try
            {
                //全量覆盖保存到Excel
                MiniExcel.SaveAs(groupPath, TotalGroups, overwriteFile: true);
                //刷新数据
                RefreshGroup();
            }
            catch (Exception ex)
            {
                new FrmMsgBoxWithoutAck("修改通信组失败：" + ex.Message, "修改通信组").Show();
            }
        }
        #endregion

        #region 无边框拖动
        // 实现方式：窗体设置为无边框后，通过监听Panel的鼠标按下/移动事件模拟标题栏拖动。
        // 鼠标按下时记录起始坐标mPoint，鼠标左键按住移动时根据偏移量更新窗体Location。

        // 鼠标按下时的起始坐标
        private Point mPoint;

        // 鼠标按下：记录按下时相对于控件的坐标
        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint = new Point(e.X, e.Y);
        }

        // 鼠标移动：左键按住时，按偏移量平移窗体位置实现拖动
        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);
            }
        }
        #endregion

        #region DataGridView事件
        // 行尾绘制：调用通用Helper绘制行号
        private void dgv_Main_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewHelper.DgvRowPostPaint((DataGridView)sender, e);
        }


        /// <summary>
        /// 单元格格式化：空值显示为"—"，避免空白难以区分
        /// </summary>
        private void dgv_Main_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                object value = this.dgv_Main.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                //值为null或空字符串时统一显示"—"
                if (value == null || value.ToString().Length == 0)
                {
                    e.Value = "—";
                }
            }
        }


        // 关闭按钮：关闭当前窗体
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 列表单元格点击事件：点击行时将对应通信组数据回填到编辑框
        /// </summary>
        private void dgv_Main_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                UpdateGroup(TotalGroups[e.RowIndex]);
            }
        }

        /// <summary>
        /// 将指定通信组数据填充到编辑区各控件
        /// </summary>
        private void UpdateGroup(Group group)
        {
            if (group != null)
            {
                this.txt_GroupName.Text = group.GroupName;
                this.num_Start.Text = group.Start.ToString();
                this.num_Length.Text = group.Length.ToString();
                this.cmb_StoreArea.Text = group.StoreArea;
                this.txt_Remark.Text = group.Remark;
            }
        }
        #endregion
    }
}
