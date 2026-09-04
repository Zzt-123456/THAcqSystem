using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZZT.MTHControlLib;
using ZZT.MTHHelper;

namespace ZZT.MTHProject
{
    public partial class FrmParamSet : System.Windows.Forms.Form
    {
        /// <summary>
        /// 参数设置窗体构造函数
        /// </summary>
        /// <param name="devPath">设备配置INI文件路径，用于读写IP、端口等通信参数</param>
        public FrmParamSet(string devPath)
        {
            InitializeComponent();
            //保存设备配置文件路径，供后续读写INI使用
            this.devPath=devPath;

            //配置定时器：每500ms触发一次，用于定时刷新限值参数
            updateTimer.Interval = 500;
            updateTimer.Tick += UpdateTimer_Tick;

            //初始化界面参数：显示IP/端口，并读取一次限值和报警参数
            InitParam();

            //为界面控件统一绑定交互事件（TextSet双击、CheckBoxEx选中变化）
            CommonBindEvent();

            //窗体关闭时停止定时器，避免资源泄漏
            this.FormClosing += (sender, e) =>
            {
                this.updateTimer.Stop();
            };

            //启动定时刷新
            this.updateTimer.Start();
        }

        /// <summary>
        /// 定时器Tick事件：每500ms调用GetLimitParam刷新限值参数与报警状态
        /// </summary>
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            GetLimitParam();
        }

        //设备配置INI文件路径
        private string devPath=string.Empty;

        //定时刷新用的定时器
        private Timer updateTimer = new Timer();

        /// <summary>
        /// 通用事件绑定：遍历界面控件统一绑定交互事件
        /// 1. 为TextSet控件绑定双击事件——双击弹出修改窗体
        /// 2. 为CheckBoxEx控件绑定选中变化事件——勾选时写入从站
        /// </summary>
        private void CommonBindEvent()
        {
            //遍历PanelMain中所有TextSet控件，绑定了变量的才绑定双击事件
            foreach (var item in this.PanelMain.Controls.OfType<TextSet>())
            {
                if (item.BindVarName != null && item.BindVarName.ToString().Length > 0)
                {
                    item.ControlDoubleClick += Common_ControlDoubleClick;
                }
            }

            //遍历PanelMain中所有CheckBoxEx控件，Tag中配置了变量名的才绑定选中事件
            foreach (var item in this.PanelMain.Controls.OfType<CheckBoxEx>())
            {
                if (item.Tag != null && item.Tag.ToString().Length > 0)
                {
                    item.CheckedChanged += Common_CheckedChanged;
                }
            }
        }

        /// <summary>
        /// 初始化参数：显示当前设备IP和端口，并读取一次限值参数与报警启用状态
        /// </summary>
        private void InitParam()
        {
            if (CommonMethods.Device != null)
            {
                //显示当前设备的IP地址和端口号
                this.txt_IP.Text = CommonMethods.Device.IPAddress;
                this.txt_Port.Text = CommonMethods.Device.Port.ToString();

                //读取一次限值参数和报警状态
                GetLimitParam();

                //读取一次报警启用开关状态
                GetAlarmParam();
            }
        }

        /// <summary>
        /// 核心方法：实时读取限值数据并进行报警判断
        /// 处理流程：
        /// 1. 遍历PanelMain中所有TextSet控件
        /// 2. 读取每个TextSet绑定变量的限值并显示（值已×Scale转换为实际物理量）
        /// 3. 报警判断有三个来源：
        ///    (a) 报警启用开关——决定本项报警是否参与判断
        ///    (b) 从站上报的Bool报警标志——直接由从站给出报警状态
        ///    (c) 本地比较——将当前实际值与限值做大于/小于比较
        /// 4. 从AlarmVarName推断报警启用变量名和当前值变量名
        ///    规则：去掉末尾的"高"/"低"得到基础名，
        ///         基础名+"报警启用" 即报警启用变量，
        ///         基础名本身即当前值变量
        /// 5. 当报警状态发生跳变（false↔true）时，通过Device.RaiseAlarm触发报警上报
        /// </summary>
        private void GetLimitParam()
        {
            if (CommonMethods.Device != null && CommonMethods.Device.IsConnected)
            {
                //遍历所有TextSet控件，逐个读取限值并做报警判断
                foreach (var item in this.PanelMain.Controls.OfType<TextSet>())
                {
                    //读取限值并显示（已×Scale转换为实际值）
                    if (item.BindVarName != null && item.BindVarName.ToString().Length > 0)
                    {
                        object value = CommonMethods.Device[item.BindVarName.ToString()];
                        if (value != null)
                        {
                            item.CurrentValue = value.ToString();
                        }
                    }

                    //报警判断：仅当控件配置了AlarmVarName时进行
                    if (item.AlarmVarName != null && item.AlarmVarName.ToString().Length > 0)
                    {
                        string alarmVar = item.AlarmVarName.ToString();

                        //0. 判断报警是否启用
                        //从AlarmVarName推断报警启用变量名
                        //"模块1温度高" → "模块1温度报警启用"
                        //"模块1湿度低" → "模块1湿度报警启用"
                        bool alarmEnabled = true;
                        //通过末尾字符判断报警类型：高报/低报
                        bool isHigh = alarmVar.EndsWith("高");
                        bool isLow = alarmVar.EndsWith("低");
                        if (isHigh || isLow)
                        {
                            //去掉末尾"高"/"低"得到基础变量名，再加"报警启用"得到开关变量名
                            string baseName = alarmVar.Substring(0, alarmVar.Length - 1);
                            string enableVarName = baseName + "报警启用";
                            object enableValue = CommonMethods.Device[enableVarName];
                            if (enableValue != null)
                            {
                                alarmEnabled = enableValue.ToString() == "1";
                            }
                        }

                        //1. 读取从站报警标志（来源之一：由从站直接上报的Bool报警位）
                        object alarmValue = CommonMethods.Device[alarmVar];
                        bool alarmFromSlave = false;
                        if (alarmValue != null)
                        {
                            string strValue = alarmValue.ToString();
                            alarmFromSlave = strValue == "True" || strValue == "true" || strValue == "1";
                        }

                        //2. 本地比较：当前值 vs 限值（来源之二：主站本地比较结果）
                        //两边都已×Scale转换为实际值，直接比较即可
                        bool alarmFromLocal = false;
                        //标记本地比较是否可用（有当前值和限值且能解析为数值）
                        bool localCompareAvailable = false;
                        if ((isHigh || isLow) && item.BindVarName != null && item.BindVarName.ToString().Length > 0)
                        {
                            //当前值变量名 = 去掉末尾"高"/"低"后的基础名
                            string currentVarName = alarmVar.Substring(0, alarmVar.Length - 1);
                            object currentValue = CommonMethods.Device[currentVarName];
                            object limitValue = CommonMethods.Device[item.BindVarName.ToString()];
                            if (currentValue != null && limitValue != null &&
                                float.TryParse(currentValue.ToString(), out float current) &&
                                float.TryParse(limitValue.ToString(), out float limit))
                            {
                                //高报：当前值>限值；低报：当前值<限值
                                alarmFromLocal = isHigh ? current > limit : current < limit;
                                localCompareAvailable = true;
                            }
                        }

                        //综合判断报警状态
                        //关键：本地比较可用时以本地比较为准，否则退回使用从站报警位
                        //原因：从站报警位可能具有锁存特性（触发后不随温度恢复自动清除），
                        //      不能作为"当前是否处于报警"的可靠依据；本地比较反映实时状态，更可靠
                        bool newAlarmState;
                        if (localCompareAvailable)
                        {
                            newAlarmState = alarmEnabled && alarmFromLocal;
                        }
                        else
                        {
                            newAlarmState = alarmEnabled && alarmFromSlave;
                        }

                        //注意：报警事件的触发（日志输出、scrollingAlarm更新）已移至 FrmMain.CheckAlarms 方法
                        //FrmMain 的 storeTimer 每秒调用 CheckAlarms，确保无论用户在哪个页面报警检测都能正常运行
                        //此处仅负责更新控件LED显示状态
                        item.IsAlarm = newAlarmState;
                    }
                }
            }
        }

        /// <summary>
        /// 读取报警启用状态：遍历所有CheckBoxEx，依据其Tag变量名从设备读取值并设置勾选状态
        /// </summary>
        private void GetAlarmParam()
        {
            if (CommonMethods.Device != null && CommonMethods.Device.IsConnected)
            {
                //遍历所有CheckBoxEx报警启用开关，根据从站读取的值刷新勾选状态
                foreach (var item in this.PanelMain.Controls.OfType<CheckBoxEx>())
                {
                    if (item.Tag != null && item.Tag.ToString().Length > 0)
                    {
                        object value = CommonMethods.Device[item.Tag.ToString()];
                        if (value != null)
                        {
                            //值为"1"表示报警启用，勾选；否则取消勾选
                            item.Checked = value.ToString() == "1";
                        }
                    }
                }
            }
        }

        private void btn_GroupConfig_Click(object sender, EventArgs e)
        {
            new FrmGroupConfig().ShowDialog();
        }

        private void btn_VarConfig_Click(object sender, EventArgs e)
        {
            new FrmVariableConfig().ShowDialog();
        }

        /// <summary>
        /// 确定按钮：将IP和端口保存到INI文件，并设置设备为断开状态触发重连
        /// </summary>
        private void btn_Sure_Click(object sender, EventArgs e)
        {
            //将IP地址写入INI配置文件
            bool result=IniConfigHelper.WriteIniData("设备参数", "IP地址", this.txt_IP.Text, devPath);

            //将端口号写入INI配置文件
            result&= IniConfigHelper.WriteIniData("设备参数", "端口号", this.txt_Port.Text, devPath);

            if (result)
            {
                //同步更新内存中Device对象的IP和端口
                if (CommonMethods.Device != null)
                {
                    CommonMethods.Device.IPAddress = this.txt_IP.Text.Trim();
                    CommonMethods.Device.Port = Convert.ToInt32(this.txt_Port.Text.Trim());
                }

                DialogResult dialogResult= new FrmMsgBoxWithoutAck("通信参数设置成功","通信设置").ShowDialog();

                //用户确认后，将设备置为断开，通信线程会自动触发重连使用新参数
                if (dialogResult == DialogResult.OK && CommonMethods.Device != null)
                {
                    CommonMethods.Device.IsConnected = false;
                }
            }
            else
            {
                new FrmMsgBoxWithoutAck("通信参数设置失败", "通信设置").Show();
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            if (CommonMethods.Device != null)
            {
                this.txt_IP.Text = CommonMethods.Device.IPAddress;
                this.txt_Port.Text = CommonMethods.Device.Port.ToString();
            }
        }

        /// <summary>
        /// TextSet双击事件：弹出限值修改窗体，传入标题、绑定变量名和当前值
        /// </summary>
        private void Common_ControlDoubleClick(object sender, EventArgs e)
        {
            if (sender is TextSet textset)
            {
                if (textset.BindVarName != null && textset.BindVarName.ToString().Length > 0)
                {
                    //弹出修改窗体，传入标题、绑定的变量名和当前值供用户修改并写回从站
                    FrmModify frmModify = new FrmModify(textset.TitleName, textset.BindVarName, textset.CurrentValue);
                    frmModify.ShowDialog();
                }
            }
        }

        /// <summary>
        /// CheckBoxEx选中变化事件：将勾选状态写入从站
        /// 若写入失败则回滚勾选状态，保持界面与从站一致
        /// </summary>
        private void Common_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBoxEx checkbox)
            {
                if (checkbox.Tag != null && checkbox.Tag.ToString().Length > 0)
                {
                    //根据勾选状态写入"1"(启用)或"0"(禁用)到从站
                    bool result = CommonMethods.CommonWrite(checkbox.Tag.ToString(), checkbox.Checked ? "1" : "0");
                    if (result == false)
                    {
                        //写入失败：临时解绑事件→回滚勾选状态→重新绑定事件，避免回滚时再次触发本方法
                        checkbox.CheckedChanged -= Common_CheckedChanged;
                        checkbox.Checked = !checkbox.Checked;
                        checkbox.CheckedChanged += Common_CheckedChanged;
                    }
                }
            }
        }

    }
}
