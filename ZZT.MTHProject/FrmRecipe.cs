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
using thinger.DataConvertLib;
using ZZT.MTHHelper;
using ZZT.MTHModels;

namespace ZZT.MTHProject
{
    public partial class FrmRecipe : System.Windows.Forms.Form
    {
        /// <summary>
        /// 配方管理窗体构造函数
        /// </summary>
        /// <param name="devPath">设备配置INI文件路径，用于记录当前应用配方</param>
        public FrmRecipe(string devPath)
        {
            InitializeComponent();

            //确保Recipe目录存在，首次运行时自动创建，避免目录不存在导致的异常
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            // 显示当前设备应用中的配方名称
            this.lbl_CurrentRecipe.Text = CommonMethods.Device.CurrentRecipe;
            // 将当前配方名称回填到输入框，便于直接修改或应用
            this.txt_RecipeName.Text = CommonMethods.Device.CurrentRecipe;
            // 加载并刷新配方列表显示
            RefreshRecipe();

            this.devPath = devPath;
        }

        // 设备配置文件路径（INI），用于持久化"当前配方"字段
        private string devPath=string.Empty;

        // 配方文件存储目录：程序启动目录下的 Recipe 文件夹，每个配方保存为一个 .ini 文件
        private string basePath = Application.StartupPath + "\\Recipe";

        // 内存中缓存的全部配方信息列表，由 GetAllRecipe 方法从 Recipe 目录加载
        private List<RecipeInfo> recipeInfos = new List<RecipeInfo>();

        /// <summary>
        /// 添加配方按钮点击事件
        /// 流程：非空验证 → 查重 → 收集配方数据 → 写入INI文件 → 刷新列表
        /// </summary>
        private void btn_Add_Click(object sender, EventArgs e)
        {
            //非空验证：配方名称不能为空
            if (this.txt_RecipeName.Text.Trim().Length == 0)
            {
                new FrmMsgBoxWithoutAck("配方名称为空，请检查！", "添加配方").Show();
                return;
            }

            //查重：校验配方名称是否已存在
            var info = recipeInfos.Where(c => c.RecipeName == this.txt_RecipeName.Text.Trim()).FirstOrDefault();
            if (info != null)
            {
                new FrmMsgBoxWithoutAck("当前配方名称已存在，请检查！", "添加配方").Show();
                return;
            }

            //收集界面上6个站点的配方数据
            var recipeInfo = GetRecipeInfo();
            //序列化为JSON写入INI文件
            bool result = AddRecipe(recipeInfo);

            if (result)
            {
                //添加成功后刷新列表
                RefreshRecipe();

                new FrmMsgBoxWithoutAck("配方添加成功！", "添加配方").Show();
            }
            else
            {
                new FrmMsgBoxWithoutAck("配方添加失败！", "添加配方").Show();
            }
        }

        /// <summary>
        /// 修改配方按钮点击事件
        /// 流程：非空验证 → 查存在（必须先存在才能修改）→ 收集数据 → 覆盖写入INI
        /// 注意：修改复用 AddRecipe，因同名文件会直接被覆盖
        /// </summary>
        private void btn_Modify_Click(object sender, EventArgs e)
        {
            //非空验证
            if (this.txt_RecipeName.Text.Trim().Length == 0)
            {
                new FrmMsgBoxWithoutAck("配方名称为空，请检查！", "修改配方").Show();
                return;
            }

            //查存在：修改前必须确认配方已存在
            var info = recipeInfos.Where(c => c.RecipeName == this.txt_RecipeName.Text.Trim()).FirstOrDefault();
            if (info == null)
            {
                new FrmMsgBoxWithoutAck("当前配方名称不存在，无法修改！", "修改配方").Show();
                return;
            }

            //收集最新配方数据并覆盖写入（同名INI会被覆盖）
            var recipeInfo = GetRecipeInfo();
            bool result = AddRecipe(recipeInfo);
            if (result)
            {
                RefreshRecipe();
                new FrmMsgBoxWithoutAck("配方修改成功！", "修改配方").Show();
            }
            else
            {
                new FrmMsgBoxWithoutAck("配方修改失败！", "修改配方").Show();
            }
        }

        /// <summary>
        /// 删除配方按钮点击事件
        /// 流程：非空验证 → 查存在 → 弹出确认对话框 → 删除INI文件 → 刷新列表
        /// </summary>
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            //非空验证
            if (this.txt_RecipeName.Text.Trim().Length == 0)
            {
                new FrmMsgBoxWithoutAck("配方名称为空，请检查！", "删除配方").Show();
                return;
            }

            //查存在：删除前必须确认配方已存在
            var info = recipeInfos.Where(c => c.RecipeName == this.txt_RecipeName.Text.Trim()).FirstOrDefault();
            if (info == null)
            {
                new FrmMsgBoxWithoutAck("当前配方名称不存在，请检查！", "删除配方").Show();
                return;
            }

            //二次确认：弹出确认对话框防止误删
            DialogResult dialogResult = new FrmMsgBoxWithAck("是否确定要删除该配方？", "删除配方").ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                //确认后删除对应的INI文件
                bool result = DeleteRecipe(this.txt_RecipeName.Text.Trim());
                if (result)
                {
                    RefreshRecipe();
                    new FrmMsgBoxWithoutAck("配方删除成功！", "删除配方").Show();
                }
                else
                {
                    new FrmMsgBoxWithoutAck("配方删除失败！", "删除配方").Show();
                }
            }
        }

        /// <summary>
        /// 应用配方按钮点击事件（核心逻辑）
        /// 流程：
        /// 1. 非空验证 + 配方存在验证 + 设备连接验证
        /// 2. 校验配方参数数量为6个站点
        /// 3. 构建6个站点的限值数据（温度/湿度的高低限，统一 ×10 转为整数写入）
        /// 4. 追加24个空闲地址数值（占位，保持地址偏移）
        /// 5. 追加12个报警启用标志（每个站点温度、湿度各1个）
        /// 6. 从寄存器地址36开始批量写入Modbus
        /// 7. 写入成功后将当前配方名记录到设备INI并更新界面显示
        /// </summary>
        private void btn_Apply_Click(object sender, EventArgs e)
        {
            //非空验证
            if (this.txt_RecipeName.Text.Trim().Length == 0)
            {
                new FrmMsgBoxWithoutAck("配方名称为空，请检查！", "应用配方").Show();
                return;
            }

            //配方存在验证：必须先存在才能应用
            var info = recipeInfos.Where(c => c.RecipeName == this.txt_RecipeName.Text.Trim()).FirstOrDefault();
            if (info == null)
            {
                new FrmMsgBoxWithoutAck("当前配方名称不存在，无法应用！", "应用配方").Show();
                return;
            }

            //设备连接验证：未连接则无法下发到设备
            if (!CommonMethods.Device.IsConnected)
            {
                new FrmMsgBoxWithoutAck("请检查设备是否连接正常！", "应用配方").Show();
                return;
            }

            //校验配方数据是否完整（必须为6个站点）
            if (info.RecipeParams.Count == 6)
            {
                List<short> values = new List<short>();
                //步骤1：添加6个站点的温度和湿度的高低限（×10转换为整数，便于整数寄存器存储）
                //每个站点4个值：温度高限、温度低限、湿度高限、湿度低限，共 6×4=24 个
                for (int i = 0; i < 6; i++)
                {
                    values.Add(Convert.ToInt16(info.RecipeParams[i].TempHigh * 10));
                    values.Add(Convert.ToInt16(info.RecipeParams[i].TempLow * 10));
                    values.Add(Convert.ToInt16(info.RecipeParams[i].HumidityHigh * 10));
                    values.Add(Convert.ToInt16(info.RecipeParams[i].HumidityLow * 10));
                }

                //步骤2：添加24个空闲地址数值（占位填充，保证后续报警启用字段地址对齐）
                for (int i = 0; i < 24; i++)
                {
                    values.Add(0);
                }

                //步骤3：继续添加报警启用标志，每个站点2个（温度报警、湿度报警），共 6×2=12 个
                for (int i = 0; i < 6; i++)
                {
                    values.Add(info.RecipeParams[i].TempAlarmEnable ? (short)1 : (short)0);
                    values.Add(info.RecipeParams[i].HumidityAlarmEnable ? (short)1 : (short)0);
                }

                //从寄存器地址36开始批量写入所有数据（按 dataFormat 指定的字节序转换为字节数组）
                bool result = CommonMethods.Modbus.PreSetMultiRegisters(36,ByteArrayLib.GetByteArrayFromShortArray(values.ToArray(), CommonMethods.dataFormat));

                if (result)
                {
                    //写入成功：将当前配方名持久化到设备INI文件
                    string recipeName = this.txt_RecipeName.Text.Trim();
                    IniConfigHelper.WriteIniData("配方参数", "当前配方", recipeName, this.devPath);
                    //同步更新内存中设备的当前配方名
                    CommonMethods.Device.CurrentRecipe = recipeName;
                    //更新界面顶部"当前配方"显示
                    this.lbl_CurrentRecipe.Text = recipeName;
                    new FrmMsgBoxWithoutAck("配方数据写入成功！", "应用配方").Show();
                }
                else
                {
                    new FrmMsgBoxWithoutAck("配方数据写入失败，请检查！", "应用配方").Show();
                }
            }
            else
            {
                new FrmMsgBoxWithoutAck("配方数据不完整，请检查！", "应用配方").Show();
            }
        }

        #region 更新配方列表
        /// <summary>
        /// 刷新DataGridView配方列表
        /// 重新从Recipe目录加载所有配方，填充列表，并高亮选中当前输入框中的配方行
        /// </summary>
        private void RefreshRecipe()
        {
            //重新读取所有配方到内存
            recipeInfos = GetAllRecipe();

            if (recipeInfos.Count > 0)
            {
                this.dgv_Main.Rows.Clear();
                //逐行添加：序号 + 配方名称
                for (int i = 0; i < recipeInfos.Count; i++)
                {
                    this.dgv_Main.Rows.Add();
                    this.dgv_Main.Rows[i].Cells[0].Value = (i + 1).ToString();
                    this.dgv_Main.Rows[i].Cells[1].Value = recipeInfos[i].RecipeName;

                    //高亮当前输入框中的配方行（与名称匹配的设为选中）
                    if(this.txt_RecipeName.Text == recipeInfos[i].RecipeName)
                    {
                        this.dgv_Main.Rows[i].Selected=true;
                    }
                    else
                    {
                        this.dgv_Main.Rows[i].Selected = false;
                    }
                }

                //若存在选中行，将其配方数据回填到6个编辑控件
                if (this.dgv_Main.SelectedRows.Count > 0)
                {
                    SetRecipeInfo(this.recipeInfos[this.dgv_Main.SelectedRows[0].Index]);
                }
            }
        }
        #endregion

        #region 获取配方对象
        /// <summary>
        /// 从界面6个RecipeControl控件收集配方数据，组装为RecipeInfo对象
        /// </summary>
        /// <returns>包含6个站点参数的配方对象</returns>
        private RecipeInfo GetRecipeInfo()
        {
            RecipeInfo recipeInfo = new RecipeInfo();
            recipeInfo.RecipeName = this.txt_RecipeName.Text.Trim();

            //依次收集6个站点的配方参数（温度/湿度的高低限及报警启用）
            recipeInfo.RecipeParams = new List<RecipeParam>()
            {
                this.recipeControl1.RecipeParam,
                this.recipeControl2.RecipeParam,
                this.recipeControl3.RecipeParam,
                this.recipeControl4.RecipeParam,
                this.recipeControl5.RecipeParam,
                this.recipeControl6.RecipeParam,
            };

            return recipeInfo;
        }
        #endregion

        #region 显示当前配方
        /// <summary>
        /// 将RecipeInfo对象的数据设置到6个RecipeControl控件，用于显示当前选中的配方
        /// </summary>
        private void SetRecipeInfo(RecipeInfo recipeInfo)
        {
            this.txt_RecipeName.Text = recipeInfo.RecipeName;
            //参数数量校验后依次回填到6个控件
            if (recipeInfo.RecipeParams.Count == 6)
            {
                this.recipeControl1.RecipeParam = recipeInfo.RecipeParams[0];
                this.recipeControl2.RecipeParam = recipeInfo.RecipeParams[1];
                this.recipeControl3.RecipeParam = recipeInfo.RecipeParams[2];
                this.recipeControl4.RecipeParam = recipeInfo.RecipeParams[3];
                this.recipeControl5.RecipeParam = recipeInfo.RecipeParams[4];
                this.recipeControl6.RecipeParam = recipeInfo.RecipeParams[5];
            }
        }
        #endregion

        #region 添加配方
        /// <summary>
        /// 将配方对象序列化为JSON字符串写入INI文件
        /// 文件名为"配方名.ini"，存储在Recipe目录下；同名文件会被覆盖（修改也复用此方法）
        /// </summary>
        /// <returns>写入是否成功</returns>
        private bool AddRecipe(RecipeInfo recipeInfo)
        {
            string path = basePath + "\\" + recipeInfo.RecipeName + ".ini";
            return IniConfigHelper.WriteIniData("配方", "配方数据", JSONHelper.EntityToJSON(recipeInfo), path);
        }
        #endregion

        #region 获取所有配方
        /// <summary>
        /// 遍历Recipe目录下所有 .ini 文件，反序列化为RecipeInfo列表
        /// 容错处理：如果Recipe目录不存在则自动创建，避免首次运行时抛出异常
        /// </summary>
        private List<RecipeInfo> GetAllRecipe()
        {
            //目录不存在时自动创建，确保首次运行也能正常工作
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(basePath);
            List<FileInfo> fileInfos = directoryInfo.GetFiles("*.ini").ToList();
            List<RecipeInfo> recipeInfos = new List<RecipeInfo>();
            //逐个文件读取并转换为RecipeInfo对象
            foreach (var item in fileInfos)
            {
                recipeInfos.Add(GetRecipe(item.FullName));
            }
            return recipeInfos;
        }
        #endregion

        #region 文件变对象
        /// <summary>
        /// 从指定INI文件读取JSON字符串，反序列化为RecipeInfo对象
        /// </summary>
        /// <param name="path">INI文件完整路径</param>
        private RecipeInfo GetRecipe(string path)
        {
            return JSONHelper.JSONToEntity<RecipeInfo>(IniConfigHelper.ReadIniData("配方", "配方数据", "", path));
        }

        #endregion

        #region 删除配方
        /// <summary>
        /// 删除指定名称的配方INI文件
        /// </summary>
        /// <param name="recipeName">配方名称（不含扩展名）</param>
        /// <returns>删除是否成功（异常返回false）</returns>
        private bool DeleteRecipe(string recipeName)
        {
            try
            {
                File.Delete(basePath + "\\" + recipeName + ".ini");
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 配方列表单元格点击事件：点击行时将对应配方数据回填到编辑区
        /// </summary>
        private void dgv_Main_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var recipeInfo = recipeInfos[e.RowIndex];
                SetRecipeInfo(recipeInfo);
            }
        }


    }
}
