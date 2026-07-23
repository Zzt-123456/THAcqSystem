using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;
using ZZT.MTHControlLib;

namespace ZZT.MTHProject
{
    public partial class FrmMonitor : System.Windows.Forms.Form
    {
        /// <summary>
        /// 集中监控窗体构造函数
        /// 初始化内容：列表列宽、图表配置、定时器启动（1秒刷新）
        /// </summary>
        public FrmMonitor()
        {
            InitializeComponent();

            //设置日志列表第二列宽度：总宽减去第一列宽度，再减去25(滚动条及边距)，使其自适应填充
            this.lst_Info.Columns[1].Width = this.lst_Info.Width - this.lst_Info.Columns[0].Width - 25;

            //初始化趋势图配置（X轴时间、12条曲线、默认隐藏、1#站点默认显示）
            SetChart();

            //配置定时器：每1秒刷新一次THMControl控件和趋势图数据
            this.updateTimer.Interval = 1000;
            this.updateTimer.Tick += UpdateTimer_Tick;
            this.updateTimer.Start();

            //窗体关闭时停止定时器
            this.FormClosing += (sender, e) =>
            {
                this.updateTimer.Stop();
            };
        }

        /// <summary>
        /// 定时器Tick事件：每1秒更新所有THMControl控件的温湿度状态，并向趋势图追加一组数据
        /// </summary>
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (CommonMethods.Device != null && CommonMethods.Device.IsConnected)
            {
                //遍历主面板中所有THMControl控件，逐个更新温湿度、状态
                foreach (var item in this.MainPanel.Controls.OfType<THMControl>())
                {
                    UpdateTHMControl(item);
                }

                //构造一组(12个)数据，依次为模块1~6的温度和湿度
                List<double> ydata = new List<double>();
                for (int i = 1; i <= 6; i++)
                {
                    ydata.Add(Convert.ToDouble(CommonMethods.Device[$"模块{i}温度"]));
                    ydata.Add(Convert.ToDouble(CommonMethods.Device[$"模块{i}湿度"]));
                }
                //将数据一次性绘制到趋势图（一次绘制一组对应12条曲线）
                this.chart_ActualTrend.PlotSingle(ydata.ToArray());
            }
        }

        private Timer updateTimer = new Timer();

        //当前时间字符串格式（用于日志显示）
        private string CurrentTime
        {
            get { return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); }
        }

        /// <summary>
        /// 更新单个THMControl控件：从Device读取温度、湿度、状态并刷新到控件
        /// </summary>
        private void UpdateTHMControl(THMControl tHMControl)
        {
            if (CommonMethods.Device == null)
            {
                return;
            }

            //读取温度值并更新
            if (CommonMethods.Device[tHMControl.TempVarName] != null)
            {
                tHMControl.Temp = CommonMethods.Device[tHMControl.TempVarName].ToString();
            }

            //读取湿度值并更新
            if (CommonMethods.Device[tHMControl.HumidityVarName] != null)
            {
                tHMControl.Humidity = CommonMethods.Device[tHMControl.HumidityVarName].ToString();
            }

            //读取模块状态，"True"表示模块异常
            if (CommonMethods.Device[tHMControl.StateVarName] != null)
            {
                tHMControl.ModuleError = CommonMethods.Device[tHMControl.StateVarName].ToString() == "True";
            }
        }

        /// <summary>
        /// 趋势图初始化配置
        /// 配置内容：
        /// - X轴为时间戳，格式HH:mm:ss
        /// - 显示图例，最多保留4000个数据点
        /// - Y轴固定范围0~100，不自动缩放
        /// - 共12条曲线：1~6#站点的温度、湿度各1条（偶数索引为温度，奇数索引为湿度）
        /// - 所有曲线初始隐藏，仅1#站点温度/湿度默认显示
        /// </summary>
        private void SetChart()
        {
            //设置X轴数据类型及格式
            this.chart_ActualTrend.XDataType = SeeSharpTools.JY.GUI.StripChartX.XAxisDataType.TimeStamp;
            this.chart_ActualTrend.TimeStampFormat = "HH:mm:ss";
            //设置图例
            this.chart_ActualTrend.LegendVisible = true;
            //设置显示数据点
            this.chart_ActualTrend.DisplayPoints = 4000;
            //Y轴范围
            this.chart_ActualTrend.AxisY.Minimum = 0.0f;
            this.chart_ActualTrend.AxisY.Maximum = 100.0f;
            this.chart_ActualTrend.AxisY.AutoScale = false;
            //清除曲线
            this.chart_ActualTrend.Series.Clear();
            //设置曲线数量
            this.chart_ActualTrend.SeriesCount = 12;
            //设置曲线
            for (int i = 0; i < 12; i++)
            {
                //设置曲线名称：偶数索引为温度，奇数索引为湿度
                this.chart_ActualTrend.Series[i].Name = i % 2 == 0 ? $"{i / 2 + 1}#站点温度" : $"{i / 2 + 1}#站点湿度";
                //设置曲线不可见：默认全部隐藏，由用户勾选CheckBox后显示
                this.chart_ActualTrend.Series[i].Visible = false;
                //设置曲线的粗细
                this.chart_ActualTrend.Series[i].Width = SeeSharpTools.JY.GUI.StripChartXSeries.LineWidth.Middle;
                //设置曲线的Y轴
                this.chart_ActualTrend.Series[i].YPlotAxis = SeeSharpTools.JY.GUI.StripChartXAxis.PlotAxis.Primary;
            }

            //默认显示1#站点的温度和湿度曲线
            this.chk_Temp1.Checked = true;
            this.chk_Humidity1.Checked = true;
        }

        /// <summary>
        /// 通用日志记录方法（可作为委托供其他线程调用）
        /// 说明：
        /// - level为日志级别，限制在0~2之间
        /// - 当跨线程调用时通过Invoke切换到UI线程执行
        /// - 新日志追加到列表底部，并自动滚动到底部确保可见
        /// </summary>
        /// <param name="level">日志级别(0~2)</param>
        /// <param name="log">日志内容</param>
        public void AddLog(int level, string log)
        {
            //日志级别限制在0~2之间
            if (level > 2)
            {
                level = 2;
            }
            if (level < 0)
            {
                level = 0;
            }

            //跨线程调用时，通过Invoke切换到UI线程操作ListView
            if (this.lst_Info.InvokeRequired)
            {
                this.lst_Info.Invoke(new Action<int, string>(AddLog), level, log);
            }
            else
            {
                //创建日志项：第一列为当前时间，level作为图像索引
                ListViewItem listViewItem = new ListViewItem("  " + CurrentTime, level);
                listViewItem.SubItems.Add(log);
                this.lst_Info.Items.Add(listViewItem);
                //让最新的日志显示在最下面：自动滚动到底部确保可见
                this.lst_Info.Items[this.lst_Info.Items.Count - 1].EnsureVisible();
            }
        }

        /// <summary>
        /// CheckBox选中变化事件：根据勾选状态控制对应趋势曲线的显示/隐藏
        /// Tag中存储的是曲线在Series中的索引
        /// </summary>
        private void chk_Common_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBoxEx checkBox)
            {
                if (checkBox.Tag != null && checkBox.Tag.ToString().Length > 0)
                {
                    //从Tag中取出曲线索引，勾选则显示，取消则隐藏
                    int index = Convert.ToInt32(checkBox.Tag.ToString());
                    this.chart_ActualTrend.Series[index].Visible = checkBox.Checked;
                }
            }
        }
    }
}
