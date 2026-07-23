using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using thinger.DataConvertLib;
using ZZT.MTHBLL;
using ZZT.MTHControlLib;
using ZZT.MTHHelper;
using ZZT.MTHModels;
using static ZZT.MTHModels.Enum;


namespace ZZT.MTHProject
{
    public partial class FrmMain : System.Windows.Forms.Form
    {
        //主窗体构造函数：完成界面初始化、导航按钮注册、报警列表监听、定时存储器初始化及窗体事件绑定
        public FrmMain()
        {
            InitializeComponent();

            //将界面上的6个导航按钮按顺序加入集合，便于左右切换时按索引定位
            naviButtons.Add(this.navi_Monitor);
            naviButtons.Add(this.naviButton2);
            naviButtons.Add(this.naviButton3);
            naviButtons.Add(this.naviButton4);
            naviButtons.Add(this.naviButton5);
            naviButtons.Add(this.naviButton6);

            //订阅滚动报警列表的集合变化事件，当报警新增或消除时自动刷新滚动文字
            actualAlarmList.CollectionChanged += ActualAlarmList_CollectionChanged;

            //初始化定时存储定时器：1秒间隔、自动重复触发，启动后周期性存储实时数据到数据库
            storeTimer.Interval = 1000;
            storeTimer.AutoReset = true;
            storeTimer.Elapsed += StoreTimer_Elapsed;
            storeTimer.Start();

            //绑定窗体加载与关闭事件
            this.Load += FrmMain_Load;
            this.FormClosing += FrmMain_FormClosing;
        }



        //定时器触发回调：每秒刷新界面时间和通信状态指示灯，并在通信正常且所有模块数据齐全时将实时数据写入数据库
        private void StoreTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //更新时间和通信状态（在UI线程执行，避免跨线程访问控件）
            this.Invoke(new Action(() =>
            {
                this.lbl_CurrentTime.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToString("HH:mm:ss") + " " + week;

                this.led_CommState.Value = CommonMethods.Device.IsConnected;
            }));

            //仅在通信连接成功时执行实时数据存储
            if (CommonMethods.Device.IsConnected)
            {
                //判断6个模块的温度、湿度变量是否全部读取到值（不为null），任一缺失则跳过本次存储
                bool result = CommonMethods.Device["模块1温度"] != null;
                result &= CommonMethods.Device["模块1湿度"] != null;
                result &= CommonMethods.Device["模块2温度"] != null;
                result &= CommonMethods.Device["模块2湿度"] != null;
                result &= CommonMethods.Device["模块3温度"] != null;
                result &= CommonMethods.Device["模块3湿度"] != null;
                result &= CommonMethods.Device["模块4温度"] != null;
                result &= CommonMethods.Device["模块4湿度"] != null;
                result &= CommonMethods.Device["模块5温度"] != null;
                result &= CommonMethods.Device["模块5湿度"] != null;
                result &= CommonMethods.Device["模块6温度"] != null;
                result &= CommonMethods.Device["模块6湿度"] != null;

                if (result)
                {
                    //构造实时数据对象并调用BLL写入数据库
                    actualDataManage.AddActualData(new ActualData()
                    {
                        InsertTime = CurrentTime,
                        Station1Temp = CommonMethods.Device["模块1温度"].ToString(),
                        Station1Humidity = CommonMethods.Device["模块1湿度"].ToString(),
                        Station2Temp = CommonMethods.Device["模块2温度"].ToString(),
                        Station2Humidity = CommonMethods.Device["模块2湿度"].ToString(),
                        Station3Temp = CommonMethods.Device["模块3温度"].ToString(),
                        Station3Humidity = CommonMethods.Device["模块3湿度"].ToString(),
                        Station4Temp = CommonMethods.Device["模块4温度"].ToString(),
                        Station4Humidity = CommonMethods.Device["模块4湿度"].ToString(),
                        Station5Temp = CommonMethods.Device["模块5温度"].ToString(),
                        Station5Humidity = CommonMethods.Device["模块5湿度"].ToString(),
                        Station6Temp = CommonMethods.Device["模块6温度"].ToString(),
                        Station6Humidity = CommonMethods.Device["模块6湿度"].ToString(),
                    });
                }
            }
        }

        //============= 配置文件路径（位于程序目录下Config文件夹中） =============
        //设备参数配置文件（INI格式），存储IP、端口、当前配方等
        private string devPath = Application.StartupPath + "\\Config\\Device.ini";
        //通信组配置文件（Excel格式），描述各通信组的存储区、起始地址、长度等
        private string groupPath = Application.StartupPath + "\\Config\\Group.xlsx";
        //通信变量配置文件（Excel格式），描述各变量的名称、数据类型、偏移、量程转换等
        private string variablePath = Application.StartupPath + "\\Config\\Variable.xlsx";

        //通信线程取消令牌源，用于在窗体退出时安全终止DeviceCommunication通信循环
        private CancellationTokenSource cts;

        //当前活动报警列表（ObservableCollection在增删时会触发CollectionChanged事件，进而更新界面滚动报警文字）
        private ObservableCollection<string> actualAlarmList = new ObservableCollection<string>();

        //系统日志业务对象，负责将报警日志写入数据库
        private SysLogManage sysLogManage = new SysLogManage();

        //实时数据业务对象，负责将各模块温湿度实时数据写入数据库
        private ActualDataManage actualDataManage = new ActualDataManage();
        //定时存储定时器，周期性触发实时数据落库
        private System.Timers.Timer storeTimer = new System.Timers.Timer();

        //当前的页面索引（左右切换按钮使用，指向naviButtons集合中的当前选中项）
        private int CurrentIndex = 0;

        //导航按钮集合，按界面顺序排列，供左右切换按索引访问
        private List<NaviButton> naviButtons = new List<NaviButton>();

        //当前时间字符串（格式：yyyy-MM-dd HH:mm:ss），用于日志记录的时间戳
        private string CurrentTime
        {
            get { return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); }
        }

        //窗体加载事件：按严格顺序完成系统启动流程
        //启动顺序至关重要：
        //  1) 先打开集中监控窗体——此步骤同时给全局 CommonMethods.AddLog 委托赋值（由FrmMonitor的AddLog方法提供日志输出能力）
        //  2) 再加载设备信息（从配置文件读取IP、端口、通信组、变量），加载过程中需要通过AddLog输出日志，所以必须先完成步骤1
        //  3) 最后订阅报警事件并启动通信线程，开始与设备实时通信
        //若顺序颠倒，AddLog为null将导致日志输出失败或异常
        private void FrmMain_Load(object sender, EventArgs e)
        {
            //更新用户名称
            this.lbl_User.Text = CommonMethods.CurrentAdmin.LoginName;

            //第一步：先打开集中监控窗体（同时给 AddLog 赋值，使后续日志能够正常输出）
            CommonNaviButton_Click(this.navi_Monitor, null);

            //第二步：再加载设备信息（依赖AddLog输出加载日志）
            CommonMethods.Device = LoadDevice(devPath, groupPath, variablePath);

            //第三步：设备加载成功后，订阅报警事件并启动通信线程
            if(CommonMethods.Device != null)
            {
                CommonMethods.AddLog?.Invoke(0, "设备信息加载成功");

                //创建取消令牌源，用于退出时通知通信线程停止
                cts= new CancellationTokenSource();

                //订阅设备的报警触发/消除事件
                CommonMethods.Device.AlarmTrigEvent += Device_AlarmTrigEvent;

                //开启多线程实时通信（在后台线程中持续与设备进行Modbus通信）
                Task.Run(new Action(() =>
                {
                    DeviceCommunication(CommonMethods.Device);
                }), cts.Token);
            }
        }

        /// <summary>
        /// 报警触发事件回调：在报警触发(true)或消除(false)时执行三个动作——
        /// 1) 输出日志到监控界面（通过AddLog委托）
        /// 2) 将报警记录写入数据库（通过sysLogManage）
        /// 3) 更新滚动报警列表（actualAlarmList增删，触发CollectionChanged自动刷新界面滚动文字）
        /// </summary>
        /// <param name="ackType">true=报警触发，false=报警消除</param>
        /// <param name="variable">报警变量</param>
        private void Device_AlarmTrigEvent(bool ackType, Variable variable)
        {
            if (ackType)
            {
                //报警触发：动作1-输出日志（日志类型1表示报警）
                CommonMethods.AddLog(1, variable.Remark + "触发");

                //动作2-写入报警日志到数据库
                sysLogManage.AddSysLog(new SysLog()
                {
                    InsertTime = CurrentTime,
                    Note = variable.Remark,
                    AlarmType = "触发",
                    Operator = CommonMethods.CurrentAdmin.LoginName,
                    VarName = variable.VarName
                });

                //动作3-加入滚动报警列表（去重处理，避免同一报警重复显示）
                if (!this.actualAlarmList.Contains(variable.Remark))
                {
                    this.actualAlarmList.Add(variable.Remark);
                }
            }
            else
            {
                //报警消除：动作1-输出日志（日志类型0表示正常/消除）
                CommonMethods.AddLog(0, variable.Remark + "消除");

                //动作2-写入报警消除日志到数据库
                sysLogManage.AddSysLog(new SysLog()
                {
                    InsertTime = CurrentTime,
                    Note = variable.Remark,
                    AlarmType = "消除",
                    Operator = CommonMethods.CurrentAdmin.LoginName,
                    VarName = variable.VarName
                });

                //动作3-从滚动报警列表移除
                if (this.actualAlarmList.Contains(variable.Remark))
                {
                    this.actualAlarmList.Remove(variable.Remark);
                }
            }
        }

        //滚动报警列表集合变化回调：当actualAlarmList发生增删时，更新界面滚动报警文字
        //空集合显示"当前系统无报警"，非空时将所有活动报警用空格拼接显示
        private void ActualAlarmList_CollectionChanged(object sender,
        System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //通过Invoke切回UI线程访问控件
            this.Invoke(new Action(() =>
            {
                //根据集合的数量进行处理
                switch (actualAlarmList.Count)
                {
                    case 0:
                        //无报警时显示提示文字
                        this.scrollingAlarm.Text = "当前系统无报警";
                        break;
                    default:
                        //有报警时将所有报警用空格拼接后显示（滚动控件会自动滚动展示）
                        this.scrollingAlarm.Text = string.Join(" ", actualAlarmList);
                        break;
                }
            }));
        }

        //核心通信循环：在后台线程中持续运行，完成设备连接与数据轮询
        //while循环：以cts.IsCancellationRequested为退出条件，窗体关闭时通过cts.Cancel()通知线程退出
        //每轮循环判断设备连接状态：已连接则读取数据，未连接则尝试建立连接
        private void DeviceCommunication(Device device)
        {
            //通信线程主循环，直到收到取消信号才退出
            while (!cts.IsCancellationRequested)
            {
                if (device.IsConnected)
                {
                    //===== 已连接分支：遍历通信组，按存储区类型读取并解析变量 =====
                    //通信读取
                    foreach (var gp in device.GroupList)
                    {
                        byte[] data = null;
                        //应该返回的字节长度（用于校验返回数据完整性）
                        int reqLength = 0;

                        //根据存储区类型分流：线圈类（位）使用ReadCoils，寄存器类（字）使用ReadRegisters
                        if (gp.StoreArea == "输入线圈" || gp.StoreArea == "输出线圈")
                        {
                            //----- 线圈类存储区读取 -----
                            switch (gp.StoreArea)
                            {
                                case "输入线圈":
                                    data = CommonMethods.Modbus.ReadInputCoils(gp.Start, gp.Length);
                                    //线圈按位存储，转换为字节长度
                                    reqLength = ShortLib.GetByteLengthFromBoolLength(gp.Length);
                                    break;
                                case "输出线圈":
                                    data = CommonMethods.Modbus.ReadOutputCoils(gp.Start, gp.Length);
                                    reqLength = ShortLib.GetByteLengthFromBoolLength(gp.Length);
                                    break;
                                default:
                                    break;
                            }
                            //数据完整校验：返回非空且长度匹配预期
                            if (data != null && data.Length == reqLength)
                            {
                                //变量解析：遍历该组下所有变量，从字节数组中按位提取值
                                foreach (var variable in gp.VarList)
                                {
                                    DataType dataType = (DataType)System.Enum.Parse(typeof(DataType), variable.DataType, true);

                                    //start为变量在本次读取数据中的相对偏移（变量绝对地址 - 组起始地址）
                                    int start = variable.Start - gp.Start;

                                    //线圈数据按位存储，乘2转为字节偏移（与库的索引方式保持一致）
                                    start *= 2;

                                    switch (dataType)
                                    {
                                        case DataType.Bool:
                                            //线圈类型变量直接按位提取
                                            variable.VarValue = BitLib.GetBitFromByteArray(data, start, variable.OffsetOrLength);
                                            break;
                                        default:
                                            break;
                                    }

                                    //处理
                                    //直接更新（线圈变量无需线性转换）
                                    device.UpdateVariable(variable);
                                }


                            }
                            else
                            {
                                //读取失败或长度不匹配，标记断线并跳出组遍历，进入重连分支
                                device.IsConnected = false;
                                break;
                            }
                        }
                        else
                        {
                            //----- 寄存器类存储区读取（输入寄存器/输出寄存器） -----
                            switch (gp.StoreArea)
                            {
                                case "输入寄存器":
                                    data = CommonMethods.Modbus.ReadInputRegisters(gp.Start, gp.Length);
                                    //寄存器按字(2字节)存储，字节数 = 寄存器数 * 2
                                    reqLength = gp.Length * 2;
                                    break;
                                case "输出寄存器":
                                    data = CommonMethods.Modbus.ReadOutputRegisters(gp.Start, gp.Length);
                                    reqLength = gp.Length * 2;
                                    break;
                                default:
                                    break;
                            }
                            if (data != null && data.Length == reqLength)
                            {
                                //变量解析：遍历该组下所有变量，根据数据类型从字节数组中提取值
                                foreach (var variable in gp.VarList)
                                {
                                    DataType dataType = (DataType)System.Enum.Parse(typeof(DataType), variable.DataType, true);

                                    //start为变量在本次读取数据中的相对字节偏移（变量绝对地址 - 组起始地址）*2
                                    int start = variable.Start - gp.Start;

                                    //寄存器地址转换为字节地址（1个寄存器=2字节）
                                    start *= 2;

                                    //根据变量数据类型，使用对应的转换库从字节数组解析出实际值
                                    //所有多字节类型都传入dataFormat控制大小端解析顺序
                                    switch (dataType)
                                    {
                                        case DataType.Bool:
                                            //从2字节中按位提取（BADC/DCBA时需反转字节顺序）
                                            variable.VarValue = BitLib.GetBitFrom2BytesArray(data, start, variable.OffsetOrLength, CommonMethods.dataFormat == DataFormat.BADC || CommonMethods.dataFormat == DataFormat.DCBA);
                                            break;
                                        case DataType.Byte:
                                            //单字节：根据大小端决定取低字节还是高字节
                                            variable.VarValue = ByteLib.GetByteFromByteArray(data, CommonMethods.dataFormat == DataFormat.BADC || CommonMethods.dataFormat == DataFormat.DCBA ? start : start + 1);
                                            break;
                                        case DataType.Short:
                                            variable.VarValue = ShortLib.GetShortFromByteArray(data, start, CommonMethods.dataFormat);
                                            break;
                                        case DataType.UShort:
                                            variable.VarValue = UShortLib.GetUShortFromByteArray(data, start, CommonMethods.dataFormat);
                                            break;
                                        case DataType.Int:
                                            variable.VarValue = IntLib.GetIntFromByteArray(data, start, CommonMethods.dataFormat);
                                            break;
                                        case DataType.UInt:
                                            variable.VarValue = UIntLib.GetUIntFromByteArray(data, start, CommonMethods.dataFormat);
                                            break;
                                        case DataType.Float:
                                            variable.VarValue = FloatLib.GetFloatFromByteArray(data, start, CommonMethods.dataFormat);
                                            break;
                                        case DataType.Double:
                                            variable.VarValue = DoubleLib.GetDoubleFromByteArray(data, start, CommonMethods.dataFormat);
                                            break;
                                        case DataType.Long:
                                            variable.VarValue = LongLib.GetLongFromByteArray(data, start, CommonMethods.dataFormat);
                                            break;
                                        case DataType.ULong:
                                            variable.VarValue = ULongLib.GetULongFromByteArray(data, start, CommonMethods.dataFormat);
                                            break;
                                        case DataType.String:
                                            //字符串按OffsetOrLength长度、ASCII编码提取
                                            variable.VarValue = StringLib.GetStringFromByteArrayByEncoding(data, start, variable.OffsetOrLength, Encoding.ASCII);
                                            break;
                                        case DataType.ByteArray:
                                            variable.VarValue = ByteArrayLib.GetByteArrayFromByteArray(data, start, variable.OffsetOrLength);
                                            break;
                                        case DataType.HexString:
                                            variable.VarValue = StringLib.GetHexStringFromByteArray(data, start, variable.OffsetOrLength);
                                            break;
                                        default:
                                            break;
                                    }


                                    //处理
                                    //寄存器类变量需要先做线性转换（量程变换），再更新到设备
                                    //线性转换公式：实际值 = 原始值 * Scale + Offset
                                    variable.VarValue = MigrationLib.GetMigrationValue(variable.VarValue, variable.Scale.ToString(), variable.Offset.ToString()).Content;

                                    //更新变量值到设备字典，并触发报警检测
                                    device.UpdateVariable(variable);

                                }


                            }
                            else
                            {
                                //读取失败或长度不匹配，标记断线并跳出组遍历
                                device.IsConnected = false;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    //===== 未连接分支：尝试建立ModbusTCP连接 =====
                    //非首次连接（即重连场景），需要先断开旧连接并延时等待，避免频繁重连
                    if (device.ReConnectSign)
                    {
                        CommonMethods.Modbus?.DisConnect();

                        //延时ReConnectTime毫秒后再次尝试连接
                        Thread.Sleep(device.ReConnectTime);
                    }
                    //通信连接：每次创建新的ModbusTCP对象并尝试连接设备IP:Port
                    CommonMethods.Modbus = new ModbusTCP();
                    device.IsConnected = CommonMethods.Modbus.Connect(device.IPAddress, device.Port);

                    //区分首次连接与重连，输出不同的日志提示
                    if (device.ReConnectSign)
                    {
                        //重连场景：标志位已为true，输出重连结果
                        CommonMethods.AddLog?.Invoke(device.IsConnected?0:1, device.IsConnected ? "控制器重新连接成功" : "控制器重新连接失败");
                    }
                    else
                    {
                        //首次连接场景：输出初次连接结果，并将标志位置为true，后续断线即走重连逻辑
                        CommonMethods.AddLog?.Invoke(device.IsConnected ? 0 : 1, device.IsConnected ? "控制器初次连接成功" : "控制器初次连接失败");
                        device.ReConnectSign = true;
                    }
                }
            }
        }

        #region 加载设备信息
        //从INI配置文件读取设备参数，并调用LoadGroup获取通信组集合，最终构造Device对象
        private Device LoadDevice(string devicePath, string groupPath, string variablePath)
        {
            // 判断设备配置文件是否存在
            if (!File.Exists(devicePath))
            {
                CommonMethods.AddLog?.Invoke(1, "设备文件不存在");
                return null;
            }

            //先加载通信组（内部会同时加载变量并将变量关联到对应组）
            List<Group> gpList = LoadGroup(groupPath, variablePath);

            if (gpList != null && gpList.Count > 0)
            {
                try
                {
                    //从INI读取设备参数：IP地址（默认127.0.0.1）、端口号（默认502）、当前配方
                    return new Device()
                    {
                        IPAddress = IniConfigHelper.ReadIniData("设备参数", "IP地址", "127.0.0.1", devicePath),
                        Port = Convert.ToInt32(IniConfigHelper.ReadIniData("设备参数", "端口号", "502", devicePath)),
                        CurrentRecipe = IniConfigHelper.ReadIniData("配方参数", "当前配方", "", devicePath),
                        GroupList = gpList
                    };
                }
                catch (Exception ex)
                {
                    //日志写入
                    CommonMethods.AddLog?.Invoke(1, "设备信息加载失败: " + ex.Message);
                    return null;
                }
            }
            else
            {
                CommonMethods.AddLog?.Invoke(1, "设备文件不存在");
                return null;
            }
        }

        /// <summary>
        /// 通信组及通信变量解析
        /// 从Excel读取通信组(Group.xlsx)和变量(Variable.xlsx)，
        /// 然后根据GroupName将变量关联到对应的通信组中
        /// </summary>
        /// <param name="groupPath">通信组Excel路径</param>
        /// <param name="variablePath">变量Excel路径</param>
        /// <returns>装配好变量的通信组集合</returns>
        private List<Group> LoadGroup(string groupPath, string variablePath)
        {
            // 判断通信组文件是否存在
            if (!File.Exists(groupPath))
            {
                CommonMethods.AddLog?.Invoke(1, "通信组文件不存在");
                return null;
            }
            // 判断变量文件是否存在
            if (!File.Exists(variablePath))
            {
                CommonMethods.AddLog?.Invoke(1, "通信变量文件不存在");
                return null;
            }

            // 先解析通信组（MiniExcel将Excel每行映射为Group对象）
            List<Group> GpList = null;
            try
            {
                GpList = MiniExcel.Query<Group>(groupPath).ToList();
            }
            catch (Exception ex)
            {
                CommonMethods.AddLog?.Invoke(1, "通信组加载失败: " + ex.Message);
                return null;
            }
            // 再解析通信变量（MiniExcel将Excel每行映射为Variable对象）
            List<Variable> VarList = null;
            try
            {
                VarList = MiniExcel.Query<Variable>(variablePath).ToList();
            }
            catch (Exception ex)
            {
                CommonMethods.AddLog?.Invoke(1, "通信变量加载失败: " + ex.Message);
                return null;
            }

            //将变量按GroupName关联到对应的通信组
            if (GpList != null && VarList != null)
            {
                foreach (var group in GpList)
                {
                    //从全局变量列表中筛选出属于当前组的变量，挂到组的VarList属性上
                    group.VarList = VarList.FindAll(c => c.GroupName == group.GroupName).ToList();
                }
            }
            else
            {
                return null;
            }
            return GpList;
        }

        #endregion

        #region 通用窗体切换

        /// <summary>
        /// 通用窗体切换：导航按钮点击处理流程
        /// 1) 根据导航按钮的TitleName找到对应的FormNames枚举
        /// 2) 进行用户权限校验（不同功能模块对应不同权限项），权限不足弹窗提示并中止
        /// 3) 权限校验通过后调用OpenForm切换窗体、更新标题、设置选中状态
        /// </summary>
        /// <param name="sender">点击的导航按钮</param>
        /// <param name="e"></param>
        private void CommonNaviButton_Click(object sender, EventArgs e)
        {
            if (sender is NaviButton navi)
            {
                //判断按钮标题是否对应一个有效的窗体枚举
                if (System.Enum.IsDefined(typeof(FormNames), navi.TitleName))
                {
                    //拿到导航按钮对应的窗体枚举值
                    FormNames formNames = (FormNames)System.Enum.Parse(typeof(FormNames), navi.TitleName, true);

                    //用户权限处理：逐项校验当前登录用户是否拥有进入该模块的权限
                    switch (formNames)
                    {
                        case FormNames.参数设置:
                            if (!CommonMethods.CurrentAdmin.ParamSet)
                            {
                                new FrmMsgBoxWithoutAck("用户权限不足，请切换用户！", "权限不足").ShowDialog();
                                return;
                            }
                            break;
                        case FormNames.配方管理:
                            if (!CommonMethods.CurrentAdmin.Recipe)
                            {
                                new FrmMsgBoxWithoutAck("用户权限不足，请切换用户！", "权限不足").ShowDialog();
                                return;
                            }
                            break;
                        case FormNames.报警追溯:
                            if (!CommonMethods.CurrentAdmin.HistoryLog)
                            {
                                new FrmMsgBoxWithoutAck("用户权限不足，请切换用户！", "权限不足").ShowDialog();
                                return;
                            }
                            break;
                        case FormNames.历史趋势:
                            if (!CommonMethods.CurrentAdmin.HistoryTrend)
                            {
                                new FrmMsgBoxWithoutAck("用户权限不足，请切换用户！", "权限不足").ShowDialog();
                                return;
                            }
                            break;
                        case FormNames.用户管理:
                            if (!CommonMethods.CurrentAdmin.UserManage)
                            {
                                new FrmMsgBoxWithoutAck("用户权限不足，请切换用户！", "权限不足").ShowDialog();
                                return;
                            }
                            break;
                        default:
                            break;
                    }

                    //窗体切换：将目标窗体嵌入主面板

                    OpenForm(this.MainPanel, formNames);

                    //设置Title

                    SetTitle(this.lbl_Title, formNames);

                    //设置选中：将当前按钮置为选中状态，其他按钮取消选中

                    SetNaviButtonSelected(this.TopPanel, navi);
                }
            }
        }

        /// <summary>
        /// 通用打开窗体：将目标窗体嵌入到主Panel中显示
        /// 机制说明：
        /// 1) 遍历Panel中已有的Form控件，若目标窗体已存在则直接BringToFront前置显示
        /// 2) 若目标窗体不存在，则关闭非固定窗体（枚举值>=临界窗体的视为非固定窗体，会被关闭以释放资源）
        /// 3) 创建新窗体实例，设置TopLevel=false（作为子控件嵌入）、Dock=Fill铺满、无边框，加入Panel并显示
        /// 注：集中监控窗体创建时会同步给全局AddLog委托赋值
        /// </summary>
        /// <param name="mainPanel">窗体嵌入的容器面板</param>
        /// <param name="formNames">目标窗体枚举</param>
        private void OpenForm(Panel mainPanel, FormNames formNames)
        {
            int total = mainPanel.Controls.Count;
            int closeCount = 0;
            bool isFind = false;
            //遍历面板中已有的子控件，查找目标窗体是否已存在
            for (int i = 0; i < total; i++)
            {
                //closeCount用于补偿已关闭窗体导致的索引偏移
                Control ct = mainPanel.Controls[i - closeCount];
                if (ct is Form frm)
                {
                    //如果当前Form是我们需要操作的窗体
                    if (frm.Text == formNames.ToString())
                    {
                        //已存在则前置显示，无需重新创建
                        frm.BringToFront();
                        isFind = true;
                        break;
                    }
                    //如果当前Form不是我们需要操作的窗体，然后判断是否为固定窗体，如果不是，则关闭，如果是，则不做处理
                    else if ((FormNames)System.Enum.Parse(typeof(FormNames), frm.Text, true) >= FormNames.临界窗体)
                    {
                        //非固定窗体（枚举值大于等于临界窗体）关闭以释放资源
                        frm.Close();
                        closeCount++;
                    }
                }
            }
            //目标窗体不存在则创建新实例
            if (isFind == false)
            {
                Form frm = null;
                switch (formNames)
                {
                    case FormNames.集中监控:
                        frm = new FrmMonitor();
                        //监控窗体创建时，将其AddLog方法赋值给全局委托，使其他模块能通过CommonMethods.AddLog输出日志
                        CommonMethods.AddLog=((FrmMonitor)frm).AddLog;
                        break;
                    case FormNames.参数设置:
                        frm = new FrmParamSet(devPath);
                        break;
                    case FormNames.配方管理:
                        frm = new FrmRecipe(devPath);
                        break;
                    case FormNames.报警追溯:
                        frm = new FrmAlarm();
                        break;
                    case FormNames.历史趋势:
                        frm = new FrmHistory();
                        break;
                    case FormNames.用户管理:
                        frm = new FrmUserManage();
                        break;
                    default:
                        break;
                }
                if (frm != null)
                {
                    //设置非顶层窗体（子窗体模式，可嵌入Panel）
                    frm.TopLevel = false;
                    //去除边框
                    frm.FormBorderStyle = FormBorderStyle.None;
                    //填充
                    frm.Dock = DockStyle.Fill;
                    //设置父容器为容器控件
                    frm.Parent = mainPanel;
                    //置前
                    frm.BringToFront();
                    //显示
                    frm.Show();

                }
            }
        }

        /// <summary>
        /// 设置标题内容
        /// </summary>
        /// <param name="lable">标题控件</param>
        /// <param name="formNames">窗体枚举名称</param>
        private void SetTitle(Label lable, FormNames formNames)
        {
            lable.Text = formNames.ToString();
        }

        /// <summary>
        /// 设置导航按钮选中
        /// </summary>
        /// <param name="topPanel">导航按钮容器</param>
        /// <param name="naviButton">导航按钮</param>
        private void SetNaviButtonSelected(Panel topPanel, NaviButton naviButton)
        {
            foreach (var item in topPanel.Controls.OfType<NaviButton>())
            {
                item.IsSelected = false;
            }
            naviButton.IsSelected = true;
        }

        //退出按钮点击：关闭主窗体（会触发FrmMain_FormClosing进行资源释放确认）
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 减少闪烁
        //重写CreateParams：通过添加WS_EX_COMPOSITED扩展样式(0x02000000)启用双缓冲，
        //使窗体及其子控件绘制到缓冲区再一次性显示，有效减少界面闪烁
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        #endregion

        #region 获取星期信息
        //中文星期数组，索引0=星期日，与DateTime.DayOfWeek枚举值对应
        private string[] weeks = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };

        //获取当前日期对应的中文星期字符串
        private string week
        {
            get { return weeks[Convert.ToInt32(DateTime.Now.DayOfWeek)]; }
        }
        #endregion

        #region 窗体退出确认
        //窗体关闭事件：弹出确认对话框，确认退出后释放资源（停止定时器、断开通信、取消通信线程）
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = new FrmMsgBoxWithAck("是否确认要退出系统？", "退出系统").ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                //停止实时数据存储定时器
                storeTimer.Stop();
                //断开Modbus通信连接
                CommonMethods.Modbus?.DisConnect();
                //取消通信线程（通知DeviceCommunication的while循环退出）
                cts?.Cancel();
            }
            else
            {
                //取消退出，保持窗体运行
                e.Cancel = true;
            }
        }
        #endregion

        #region 左右切换
        //右切换按钮：索引递增，切换到下一个导航窗体，到末尾则保持不变
        private void btn_Right_Click(object sender, EventArgs e)
        {
            CurrentIndex++;
            if (CurrentIndex < naviButtons.Count)
            {
                CommonNaviButton_Click(naviButtons[CurrentIndex], null);
            }
            else
            {
                //已到末尾，回退索引避免越界
                CurrentIndex--;
            }
        }

        //左切换按钮：索引递减，切换到上一个导航窗体，到开头则保持不变
        private void btn_Left_Click(object sender, EventArgs e)
        {
            CurrentIndex--;
            if (CurrentIndex >= 0)
            {
                CommonNaviButton_Click(naviButtons[CurrentIndex], null);
            }
            else
            {
                //已到开头，回退索引避免越界
                CurrentIndex++;
            }
        }
        #endregion
    }
}
