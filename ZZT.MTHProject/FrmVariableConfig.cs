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
using thinger.DataConvertLib;

namespace ZZT.MTHProject
{
    public partial class FrmVariableConfig : System.Windows.Forms.Form
    {
        /// <summary>
        /// 通信变量配置窗体构造函数
        /// 流程：
        /// 1. 数据类型下拉框绑定DataType枚举名称
        /// 2. 设置DataGridView不自动生成列
        /// 3. 检查/创建Config目录
        /// 4. 加载所有通信组到下拉框、加载所有变量到内存
        /// 5. 注册通信组下拉框切换事件
        /// 6. 默认选中第一个通信组（触发筛选显示对应变量）
        /// </summary>
        public FrmVariableConfig()
        {
            InitializeComponent();

            //数据类型下拉框：绑定DataType枚举的所有名称
            this.cmb_DataType.DataSource = System.Enum.GetNames(typeof(DataType));

            //禁止DataGridView根据数据源自动生成列
            this.dgv_Main.AutoGenerateColumns = false;

            //确保Config目录存在
            string configDir = Path.GetDirectoryName(groupPath);
            if (!Directory.Exists(configDir))
            {
                Directory.CreateDirectory(configDir);
            }

            //读取所有通信组，用于填充通信组下拉框
            List<Group> TotalGroups = GetAllGroups();

            //读取所有变量到内存
            TotalVariables = GetAllVariables();

            //注册通信组下拉框切换事件（先注册再赋值，确保切换时触发筛选）
            this.cmb_GroupName.SelectedIndexChanged += Cmb_GroupName_SelectedIndexChanged;

            //将通信组名称填充到下拉框，默认选中第一项（触发SelectedIndexChanged筛选变量）
            if (TotalGroups.Count > 0)
            {
                foreach (var item in TotalGroups)
                {
                    this.cmb_GroupName.Items.Add(item.GroupName);
                }
                this.cmb_GroupName.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 通信组下拉框切换事件：切换通信组时重新筛选并刷新变量列表
        /// </summary>
        private void Cmb_GroupName_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshVariable();
        }

        // Group.xlsx路径：读取通信组列表用
        private string groupPath = Application.StartupPath + "\\Config\\Group.xlsx";

        // Variable.xlsx路径：变量配置持久化文件
        private string variablePath = Application.StartupPath + "\\Config\\Variable.xlsx";

        // 内存中缓存的全部变量列表
        private List<Variable> TotalVariables = new List<Variable>();

        #region 获取所有通信组
        /// <summary>
        /// 获取所有通信组：从Group.xlsx读取全部通信组（用于填充通信组下拉框）
        /// 文件不存在返回空列表；异常弹窗并返回空列表
        /// </summary>
        /// <returns>通信组列表</returns>
        private List<Group> GetAllGroups()
        {
            try
            {
                if (!File.Exists(groupPath))
                {
                    return new List<Group>();
                }
                return MiniExcel.Query<Group>(groupPath).ToList();
            }
            catch (Exception ex)
            {
                new FrmMsgBoxWithoutAck("读取通信组配置失败：" + ex.Message, "读取配置").Show();
                return new List<Group>();
            }

        }
        #endregion

        #region 获取所有的变量
        /// <summary>
        /// 获取所有变量：从Variable.xlsx读取全部变量记录
        /// 文件不存在返回空列表；异常弹窗并返回空列表
        /// </summary>
        private List<Variable> GetAllVariables()
        {
            try
            {
                if (!File.Exists(variablePath))
                {
                    return new List<Variable>();
                }
                return MiniExcel.Query<Variable>(variablePath).ToList();
            }
            catch (Exception ex)
            {
                new FrmMsgBoxWithoutAck("读取变量配置失败：" + ex.Message, "读取配置").Show();
                return new List<Variable>();
            }
        }
        #endregion

        #region 根据通信组名称筛选
        /// <summary>
        /// 按通信组名称筛选变量
        /// 名称为空时返回全部变量，否则返回匹配GroupName的变量子集
        /// </summary>
        private List<Variable> GetVariablesByGroupName(string groupName)
        {
            if (groupName.Length == 0)
            {
                return TotalVariables;
            }
            else
            {
                return TotalVariables.FindAll(c => c.GroupName == groupName).ToList();
            }
        }
        #endregion

        #region 更新变量组集合
        /// <summary>
        /// 刷新变量列表：按当前选中的通信组筛选变量并绑定到DataGridView
        /// </summary>
        private void RefreshVariable()
        {
            //根据当前通信组下拉框选中的名称筛选变量
            var list = GetVariablesByGroupName(this.cmb_GroupName.Text.Trim());

            if (list != null && list.Count > 0)
            {
                this.dgv_Main.DataSource = null;
                this.dgv_Main.DataSource = list;
            }
        }
        #endregion

        #region 判断组变量名称是否存在
        /// <summary>
        /// 查重：判断指定变量名称是否已存在
        /// </summary>
        /// <param name="variableName">变量名称</param>
        /// <returns>存在返回true，否则false</returns>
        private bool IsVariableNameExits(string variableName)
        {
            return TotalVariables.FindAll(c => c.VarName == variableName).ToList().Count > 0;
        }
        #endregion

        #region 增删改变量
        /// <summary>
        /// 添加变量按钮点击事件
        /// 流程：非空验证 → 查重 → 构造Variable对象加入列表 → 保存到Excel → 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Add_Click(object sender, EventArgs e)
        {
            string varName = this.txt_VarName.Text.Trim();

            //非空验证
            if (varName.Length == 0)
            {
                new FrmMsgBoxWithoutAck("变量名称不能为空！", "添加变量").Show();
                return;
            }

            //查重：变量名称必须唯一
            if (IsVariableNameExits(varName))
            {
                new FrmMsgBoxWithoutAck("变量名称已经存在！", "添加通信组").Show();
                return;
            }

            //构造新变量对象并加入内存列表
            TotalVariables.Add(new Variable()
            {
                VarName = varName,
                Start = Convert.ToUInt16(this.num_Start.Text),
                OffsetOrLength = Convert.ToUInt16(this.num_OffsetOrLength.Text),
                DataType = this.cmb_DataType.Text.Trim(),
                GroupName = this.cmb_GroupName.Text.Trim(),
                PosAlarm = this.chk_PosAlarm.Checked,
                NegAlarm = this.chk_NegAlarm.Checked,
                Scale = Convert.ToSingle(this.num_Scale.Value),
                Offset = Convert.ToSingle(this.num_Offset.Value),
                Remark = this.txt_Remark.Text.Trim(),
            });

            try
            {
                //全量覆盖保存到Excel
                MiniExcel.SaveAs(variablePath, TotalVariables, overwriteFile: true);

                //刷新数据
                RefreshVariable();
            }
            catch (Exception ex)
            {
                new FrmMsgBoxWithoutAck("添加变量失败：" + ex.Message, "添加变量").Show();
            }

        }

        /// <summary>
        /// 删除变量按钮点击事件
        /// 流程：查存在 → 从列表移除 → 保存到Excel → 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            string variableName = this.txt_VarName.Text.Trim();
            //查存在：删除前必须先存在
            if (!IsVariableNameExits(variableName))
            {
                new FrmMsgBoxWithoutAck("变量名称不存在！", "删除变量").Show();
                return;
            }

            //从内存列表移除匹配项
            TotalVariables.RemoveAll(c => c.VarName == variableName);
            try
            {
                //全量覆盖保存到Excel
                MiniExcel.SaveAs(variablePath, TotalVariables, overwriteFile: true);
                //刷新数据
                RefreshVariable();
            }
            catch (Exception ex)
            {
                new FrmMsgBoxWithoutAck("删除通信组失败：" + ex.Message, "删除通信组").Show();
            }
        }

        /// <summary>
        /// 修改变量按钮点击事件
        /// 流程：查存在 → 找到对象并更新字段 → 保存到Excel → 刷新
        /// 注意：变量名称作为主键不可修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Modify_Click(object sender, EventArgs e)
        {
            string variableName = this.txt_VarName.Text.Trim();
            //查存在
            if (!IsVariableNameExits(variableName))
            {
                new FrmMsgBoxWithoutAck("变量名称不存在！", "修改变量").Show();
                return;
            }

            //按名称查找并更新除名称外的其他字段
            var variable = TotalVariables.Find(c => c.VarName == variableName);
            variable.Start = Convert.ToUInt16(this.num_Start.Value);
            variable.OffsetOrLength = Convert.ToUInt16(this.num_OffsetOrLength.Value);
            variable.DataType = this.cmb_DataType.Text.Trim();
            variable.GroupName = this.cmb_GroupName.Text.Trim();
            variable.PosAlarm = this.chk_PosAlarm.Checked;
            variable.NegAlarm = this.chk_NegAlarm.Checked;
            variable.Scale = Convert.ToSingle(this.num_Scale.Value);
            variable.Offset = Convert.ToSingle(this.num_Offset.Value);
            variable.Remark = this.txt_Remark.Text.Trim();

            try
            {
                //全量覆盖保存到Excel
                MiniExcel.SaveAs(variablePath, TotalVariables, overwriteFile: true);
                //刷新数据
                RefreshVariable();
            }
            catch (Exception ex)
            {
                new FrmMsgBoxWithoutAck("修改变量失败：" + ex.Message, "修改变量").Show();
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
        /// 单元格格式化：
        /// - 第6、7列（PosAlarm、NegAlarm，Bool类型）显示"启用/禁用"
        /// - 其他列空值显示"--"
        /// </summary>
        private void dgv_Main_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                object value = this.dgv_Main.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                //Bool列（正向报警、负向报警）转换为"启用/禁用"显示
                if (e.ColumnIndex == 6 || e.ColumnIndex == 7)
                {
                    if (value != null)
                    {
                        e.Value = value.ToString() == "True" ? "启用" : "禁用";
                    }
                }
                else
                {
                    //其他列空值统一显示"--"
                    if (value == null || value.ToString().Length == 0)
                    {
                        e.Value = "--";
                    }
                }
            }
        }


        // 关闭按钮：关闭当前窗体
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 列表单元格点击事件：点击行时将对应变量数据回填到编辑区
        /// </summary>
        private void dgv_Main_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                UpdateVariable(TotalVariables[e.RowIndex]);
            }
        }

        /// <summary>
        /// 将指定变量数据填充到编辑区各控件
        /// </summary>
        private void UpdateVariable(Variable variable)
        {
            if (variable != null)
            {
                this.cmb_GroupName.Text = variable.GroupName;
                this.txt_VarName.Text = variable.VarName;
                this.num_Start.Text = variable.Start.ToString();
                this.num_OffsetOrLength.Text = variable.OffsetOrLength.ToString();
                this.cmb_DataType.Text = variable.DataType;
                this.chk_PosAlarm.Checked = variable.PosAlarm;
                this.chk_NegAlarm.Checked = variable.NegAlarm;
                this.num_Scale.Value = Convert.ToDecimal(variable.Scale);
                this.num_Offset.Value = Convert.ToDecimal(variable.Offset);
                this.txt_Remark.Text = variable.Remark;
            }
        }
        #endregion
    }
}
