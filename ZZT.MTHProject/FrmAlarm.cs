using MiniExcelLibs;
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
using ZZT.MTHHelper;

namespace ZZT.MTHProject
{
    /// <summary>
    /// 报警查询窗体。
    /// 核心功能：根据时间范围（默认近 2 小时）与报警类型（触发/消除/全部）
    /// 查询系统日志表 SysLog 中的历史报警记录，并在 DataGridView 中展示；
    /// 同时支持将查询结果导出为 Excel 文件。
    /// 使用场景：运维人员排查温湿度越限报警时使用，限制单次查询不超过 1 天。
    /// </summary>
    public partial class FrmAlarm : System.Windows.Forms.Form
    {
        /// <summary>
        /// 构造函数：初始化界面控件、设置默认查询条件。
        /// </summary>
        public FrmAlarm()
        {
            //设计器自动生成的初始化（按钮、表格、时间选择器等控件）
            InitializeComponent();

            //禁止 DataGridView 根据数据源自动生成列，仅显示设计器中预定义的列
            this.dgv_Main.AutoGenerateColumns = false;

            //初始化报警类型下拉框：全部 / 触发 / 消除
            this.cmb_AlarmType.Items.AddRange(new string[] { "全部", "触发", "消除" });
            //默认选中“全部”，即查询触发和消除的所有报警
            this.cmb_AlarmType.SelectedIndex = 0;

            //默认查询时间范围：当前时间往前推 2 小时到当前时间
            this.dtp_Start.Value = DateTime.Now.AddHours(-2.0f);
            this.dtp_End.Value = DateTime.Now;
        }

        //系统日志业务层对象，封装了对 SysLog 表的查询方法
        private SysLogManage sysLogManage = new SysLogManage();

        /// <summary>
        /// 查询按钮点击事件：异步执行查询，并将结果绑定到 DataGridView。
        /// 使用 Task 避免 UI 线程在数据库查询期间卡顿。
        /// </summary>
        private void btn_Query_Click(object sender, EventArgs e)
        {
            //获取查询条件：开始时间、结束时间、报警类型（“全部”转为空字符串以匹配数据库查询）
            string start = this.dtp_Start.Text;
            string end = this.dtp_End.Text;
            string alarmType = this.cmb_AlarmType.Text == "全部" ? "" : this.cmb_AlarmType.Text;

            //启动后台任务执行数据库查询，返回 OperateResult 包装的 DataTable
            Task<OperateResult<DataTable>> task1 = Task.Run(() =>
            {
                return QueryProcess(start, end, alarmType);
            });

            //任务完成后回调，使用 Invoke 切回 UI 线程更新界面
            var task2 = task1.ContinueWith(task =>
            {
                this.Invoke(new Action(() =>
                {
                    if (task.Result.IsSuccess)
                    {
                        //查询成功：先清空再绑定，触发 DataGridView 重新渲染
                        this.dgv_Main.DataSource = null;
                        this.dgv_Main.DataSource = task.Result.Content;
                    }
                    else
                    {
                        //查询失败：弹出无确认按钮的自定义消息框提示错误信息
                        new FrmMsgBoxWithoutAck("查询失败：" + task.Result.Message, "报警查询").Show();
                    }
                }));
            });
        }

        /// <summary>
        /// 实际执行查询的业务逻辑：校验时间范围后调用 BLL 层查询。
        /// </summary>
        /// <param name="start">开始时间字符串</param>
        /// <param name="end">结束时间字符串</param>
        /// <param name="alarmType">报警类型（空字符串表示全部）</param>
        /// <returns>包含查询结果 DataTable 或错误信息的 OperateResult</returns>
        private OperateResult<DataTable> QueryProcess(string start, string end, string alarmType)
        {
            //判断时间合法性：开始时间必须早于结束时间
            DateTime startTime = Convert.ToDateTime(start);
            DateTime endTime = Convert.ToDateTime(end);
            if (startTime >= endTime)
            {
                return OperateResult.CreateFailResult<DataTable>("开始时间不能大于结束时间");
            }

            //限制单次查询范围不超过 1 天，避免大量数据拖慢系统
            TimeSpan timeSpan = endTime - startTime;

            if (timeSpan.TotalDays > 1.0)
            {
                return OperateResult.CreateFailResult<DataTable>("查询范围不能超过1天");
            }

            //调用 BLL 层按条件查询 SysLog 表
            DataTable dataTable = sysLogManage.QuerySysLogByCondition(start, end, alarmType);
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
        /// DataGridView 行绘制完成后事件：在行头显示行号。
        /// </summary>
        private void dgv_Main_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //调用辅助类统一绘制行号
            DataGridViewHelper.DgvRowPostPaint(sender as DataGridView, e);
        }

        /// <summary>
        /// 导出按钮点击事件：将当前 DataGridView 数据源导出为 Excel 文件。
        /// </summary>
        private void btn_Export_Click(object sender, EventArgs e)
        {
            //创建保存文件对话框，设置过滤器和默认文件名
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XLSX文件(*.xlsx)|*.xlsx|所有文件|*.*";
            saveFileDialog.Title = "导出历史报警";
            //默认文件名包含时间戳，避免覆盖已有文件
            saveFileDialog.FileName = "历史报警" + DateTime.Now.ToString("yyyyMMddHHmmss");
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.AddExtension = true;

            //用户确认保存路径后执行导出
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //使用 MiniExcel 库将 DataGridView 的 DataSource（DataTable）写入 xlsx 文件
                MiniExcel.SaveAs(saveFileDialog.FileName, this.dgv_Main.DataSource);
                //弹出带确认按钮的消息框，询问是否立即打开导出的文件
                if (new FrmMsgBoxWithAck("导出报警成功，是否立即打开？", "打开报警记录").ShowDialog() == DialogResult.OK)
                {
                    //使用系统默认程序打开导出的 Excel 文件
                    Process.Start(saveFileDialog.FileName);
                }
            }
        }
    }
}
