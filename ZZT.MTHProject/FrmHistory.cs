using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using thinger.DataConvertLib;
using ZZT.MTHBLL;
using ZZT.MTHControlLib;

namespace ZZT.MTHProject
{
    /// <summary>
    /// 历史数据查询窗体。
    /// 核心功能：通过勾选最多 12 个温湿度参数（CheckBoxEx 列表），
    /// 按时间范围（默认近 2 小时，最长 1 天）查询 ActualData 表，
    /// 将结果在 StripChartX 趋势图控件上绘制曲线；
    /// 支持快速查询（近 5 小时）、保存趋势图为 JPG、导出曲线数据为 CSV。
    /// 使用场景：操作员分析温湿度历史走势、排查异常时段数据时使用。
    /// </summary>
    public partial class FrmHistory : System.Windows.Forms.Form
    {
        /// <summary>
        /// 构造函数：初始化控件、收集 12 个参数复选框、配置趋势图样式。
        /// </summary>
        public FrmHistory()
        {
            //设计器自动生成的初始化
            InitializeComponent();

            //将界面上的 12 个 CheckBoxEx 控件加入集合，便于统一遍历处理
            //每个 CheckBoxEx 的 Tag 存放数据库字段名，Text 存放中文显示名称
            CheckBoxList.Add(this.checkBoxEx1);
            CheckBoxList.Add(this.checkBoxEx2);
            CheckBoxList.Add(this.checkBoxEx3);
            CheckBoxList.Add(this.checkBoxEx4);
            CheckBoxList.Add(this.checkBoxEx5);
            CheckBoxList.Add(this.checkBoxEx6);
            CheckBoxList.Add(this.checkBoxEx7);
            CheckBoxList.Add(this.checkBoxEx8);
            CheckBoxList.Add(this.checkBoxEx9);
            CheckBoxList.Add(this.checkBoxEx10);
            CheckBoxList.Add(this.checkBoxEx11);
            CheckBoxList.Add(this.checkBoxEx12);

            //默认查询时间范围：当前时间往前推 2 小时到当前时间
            this.dtp_Start.Value = DateTime.Now.AddHours(-2.0f);
            this.dtp_End.Value = DateTime.Now;

            //设置X轴类型为字符串类型（实际为格式化后的时间字符串 HH:mm:ss）
            this.chart_HistoryTrend.XDataType = SeeSharpTools.JY.GUI.StripChartX.XAxisDataType.String;

            //隐藏图例（曲线颜色与勾选框颜色对应，无需图例说明）
            this.chart_HistoryTrend.LegendVisible = false;

            //设置趋势图最多显示 10 万个数据点
            this.chart_HistoryTrend.DisplayPoints = 100000;

            //Y1轴范围固定为 0~100，覆盖温湿度的常见取值范围
            this.chart_HistoryTrend.AxisY.Minimum = 0.0f;
            this.chart_HistoryTrend.AxisY.Maximum = 100.0f;
            //禁用 Y 轴自动缩放，避免数据变化导致坐标轴跳动
            this.chart_HistoryTrend.AxisY.AutoScale = false;
        }

        //当前选中的参数字典：Key=数据库字段名，Value=中文显示名称
        private Dictionary<string, string> ParamList = new Dictionary<string, string>();

        //实时数据业务层对象，封装了对 ActualData 表的查询方法
        private ActualDataManage actualDataManage = new ActualDataManage();

        //12 个参数复选框的集合，便于统一遍历获取选中状态
        private List<CheckBoxEx> CheckBoxList = new List<CheckBoxEx>();

        /// <summary>
        /// 查询按钮点击事件：根据勾选的参数和时间范围异步查询数据库并更新趋势图。
        /// </summary>
        private void btn_Query_Click(object sender, EventArgs e)
        {
            //获取用户勾选的参数字典
            ParamList = GetParamList();
            string start = this.dtp_Start.Text;
            string end = this.dtp_End.Text;

            //未勾选任何参数时给出提示并终止查询
            if (ParamList.Count == 0)
            {
                new FrmMsgBoxWithoutAck("请勾选需要查询的参数！", "查询出错").Show();
                return;
            }

            //启动后台任务执行查询，避免阻塞 UI 线程
            Task<OperateResult<DataTable>> task1 = Task.Run(() =>
            {
                return QueryProcess(start, end);
            });

            //查询完成后回调，使用 Invoke 切回 UI 线程更新趋势图
            var task2 = task1.ContinueWith(task =>
            {
                this.Invoke(new Action(() =>
                {
                    if (task.Result.IsSuccess)
                    {
                        //查询成功：更新趋势图
                        UpdateChart(task.Result.Content);
                    }
                    else
                    {
                        //查询失败：弹出无确认按钮的消息框
                        new FrmMsgBoxWithoutAck("查询出错：" + task.Result.Message, "查询出错").Show();
                    }
                }));
            });
        }

        /// <summary>
        /// 根据控件的选中情况获取字典集合。
        /// 遍历 12 个 CheckBoxEx，将勾选项的 Tag（字段名）和 Text（显示名）加入字典。
        /// </summary>
        /// <returns>Key=字段名，Value=中文显示名 的字典</returns>
        private Dictionary<string, string> GetParamList()
        {
            Dictionary<string, string> paramList = new Dictionary<string, string>();
            foreach (var item in CheckBoxList)
            {
                //仅处理 Tag 已设置的复选框（Tag 为空表示未绑定参数）
                if (item.Tag != null && item.Tag.ToString().Length > 0)
                {
                    //只收集被勾选的参数
                    if (item.Checked)
                    {
                        paramList.Add(item.Tag.ToString(), item.Text);
                    }
                }
            }
            return paramList;
        }

        /// <summary>
        /// 根据时间节点查询。
        /// 校验时间合法性后调用 BLL 层查询 ActualData 表。
        /// </summary>
        /// <param name="start">开始时间字符串</param>
        /// <param name="end">结束时间字符串</param>
        /// <returns>包含 DataTable 或错误信息的 OperateResult</returns>
        private OperateResult<DataTable> QueryProcess(string start, string end)
        {
            //判断时间合法性：开始时间必须早于结束时间
            DateTime startTime = Convert.ToDateTime(start);
            DateTime endTime = Convert.ToDateTime(end);
            if (startTime >= endTime)
            {
                return OperateResult.CreateFailResult<DataTable>("开始时间不能大于结束时间");
            }
            //限制单次查询范围不超过 1 天
            TimeSpan timeSpan = endTime - startTime;
            if (timeSpan.TotalDays > 1.0)
            {
                return OperateResult.CreateFailResult<DataTable>("查询范围不能超过1天");
            }
            //调用 BLL 层按时间范围和字段名集合查询 ActualData 表
            DataTable dataTable = actualDataManage.QueryActualDataByCondition(start, end, ParamList.Keys.ToList());
            if (dataTable != null)
            {
                //查询成功：包装为成功结果
                return OperateResult.CreateSuccessResult(dataTable);
            }
            else
            {
                //查询返回 null：包装为失败结果
                return OperateResult.CreateFailResult<DataTable>("未查询到有效数据");
            }
        }

        /// <summary>
        /// 更新Chart控件显示。
        /// 将 DataTable 中的数据转换为二维 Y 数据和 X 时间标签，绘制到趋势图。
        /// </summary>
        /// <param name="dataTable">查询返回的数据表，第 0 列为时间，后续列为各参数值</param>
        private void UpdateChart(DataTable dataTable)
        {
            int rowcount = dataTable.Rows.Count;
            int columnCount = ParamList.Count;

            //重置趋势图：清空旧数据并设置曲线数量等于选中参数数量
            this.chart_HistoryTrend.Clear();
            this.chart_HistoryTrend.SeriesCount = columnCount;

            //为每条曲线设置名称（中文显示名）和线宽
            for (int i = 0; i < ParamList.Count; i++)
            {
                this.chart_HistoryTrend.Series[i].Name = ParamList.Values.ToList()[i];
                this.chart_HistoryTrend.Series[i].Width = SeeSharpTools.JY.GUI.StripChartXSeries.LineWidth.Middle;
            }

            //解析DataTable：构建二维 Y 数据和 X 轴时间标签数组
            //YData[参数索引, 时间索引]，XData[时间索引]
            double[,] YData = new double[columnCount, rowcount];
            string[] XData = new string[rowcount];

            for (int i = 0; i < rowcount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    //数据库字段值为 NULL 时填 0，避免转换异常
                    if (dataTable.Rows[i][j + 1] is DBNull)
                    {
                        YData[j, i] = 0.0f;
                    }
                    else
                    {
                        try
                        {
                            //j+1 是因为第 0 列为时间，参数从第 1 列开始
                            YData[j, i] = Convert.ToDouble(dataTable.Rows[i][j + 1]);
                        }
                        catch (Exception)
                        {
                            //转换失败时填 0，保证曲线绘制不中断
                            YData[j, i] = 0.0f;
                        }
                    }
                }

                //X 轴标签取第 0 列时间，格式化为 HH:mm:ss
                XData[i] = Convert.ToDateTime(dataTable.Rows[i][0]).ToString("HH:mm:ss");
            }

            //调用趋势图控件的 Plot 方法绘制曲线
            this.chart_HistoryTrend.Plot(YData, XData);
        }

        /// <summary>
        /// 快速查询按钮点击事件：将时间范围设置为近 5 小时并立即触发查询。
        /// </summary>
        private void btn_QuickQuery_Click(object sender, EventArgs e)
        {
            //设置查询范围为近 5 小时
            this.dtp_Start.Value = DateTime.Now.AddHours(-5.0f);
            this.dtp_End.Value = DateTime.Now;

            //直接调用查询按钮的点击事件处理方法（参数传 null）
            this.btn_Query_Click(null, null);
        }

        /// <summary>
        /// 保存图片按钮点击事件：将当前趋势图保存为 JPG 文件。
        /// </summary>
        private void btn_SaveImage_Click(object sender, EventArgs e)
        {
            //创建保存文件对话框，配置图片格式与默认文件名
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "图片文件(*.jpg)|*.jpg|所有文件|*.*";
            //默认文件名包含时间戳
            saveFileDialog.FileName = "历史趋势图片" + DateTime.Now.ToString("yyyyMMddHHmmss");
            saveFileDialog.Title = "历史趋势保存";
            saveFileDialog.DefaultExt = "jpg";
            saveFileDialog.AddExtension = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //调用趋势图控件的方法保存为 JPG 图片
                this.chart_HistoryTrend.SaveAsImage(saveFileDialog.FileName);
                //询问用户是否立即打开图片
                if (new FrmMsgBoxWithAck("图片保存成功，是否立即打开？", "打开趋势图片").ShowDialog() == DialogResult.OK)
                {
                    //使用系统默认图片查看器打开
                    Process.Start(saveFileDialog.FileName);
                }
            }
        }

        /// <summary>
        /// 导出 CSV 按钮点击事件：将趋势图数据导出为 CSV 文件。
        /// </summary>
        private void btn_ExportCSV_Click(object sender, EventArgs e)
        {
            //创建保存文件对话框，配置 CSV 格式与默认文件名
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV(*.csv)|*.csv|所有文件|*.*";
            saveFileDialog.FileName = "历史趋势CSV" + DateTime.Now.ToString("yyyyMMddHHmmss");
            saveFileDialog.Title = "历史趋势CSV";
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.AddExtension = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //调用趋势图控件的方法导出 CSV
                this.chart_HistoryTrend.SaveAsCsv(saveFileDialog.FileName);
                //询问用户是否立即打开 CSV 文件
                if (new FrmMsgBoxWithAck("CSV导出成功，是否立即打开？", "打开趋势CSV").ShowDialog() == DialogResult.OK)
                {
                    //使用系统默认程序（如 Excel）打开 CSV
                    Process.Start(saveFileDialog.FileName);
                }
            }
        }
    }
}
