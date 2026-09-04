namespace ZZT.MTHProject
{
    partial class FrmMonitor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMonitor));
            SeeSharpTools.JY.GUI.StripChartXSeries stripChartXSeries1 = new SeeSharpTools.JY.GUI.StripChartXSeries();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.MainPanel = new ZZT.MTHControlLib.PanelEnhanced();
            this.thmControl1 = new ZZT.MTHControlLib.THMControl();
            this.lst_Info = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkBoxEx12 = new ZZT.MTHControlLib.CheckBoxEx();
            this.thmControl2 = new ZZT.MTHControlLib.THMControl();
            this.checkBoxEx8 = new ZZT.MTHControlLib.CheckBoxEx();
            this.thmControl3 = new ZZT.MTHControlLib.THMControl();
            this.checkBoxEx4 = new ZZT.MTHControlLib.CheckBoxEx();
            this.thmControl4 = new ZZT.MTHControlLib.THMControl();
            this.checkBoxEx11 = new ZZT.MTHControlLib.CheckBoxEx();
            this.thmControl5 = new ZZT.MTHControlLib.THMControl();
            this.checkBoxEx7 = new ZZT.MTHControlLib.CheckBoxEx();
            this.thmControl6 = new ZZT.MTHControlLib.THMControl();
            this.checkBoxEx3 = new ZZT.MTHControlLib.CheckBoxEx();
            this.title1 = new ZZT.MTHControlLib.Title();
            this.checkBoxEx10 = new ZZT.MTHControlLib.CheckBoxEx();
            this.title2 = new ZZT.MTHControlLib.Title();
            this.checkBoxEx6 = new ZZT.MTHControlLib.CheckBoxEx();
            this.chart_ActualTrend = new SeeSharpTools.JY.GUI.StripChartX();
            this.chk_Humidity1 = new ZZT.MTHControlLib.CheckBoxEx();
            this.chk_Temp1 = new ZZT.MTHControlLib.CheckBoxEx();
            this.checkBoxEx9 = new ZZT.MTHControlLib.CheckBoxEx();
            this.checkBoxEx5 = new ZZT.MTHControlLib.CheckBoxEx();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "info.png");
            this.imageList1.Images.SetKeyName(1, "warning.png");
            this.imageList1.Images.SetKeyName(2, "error.png");
            // 
            // MainPanel
            // 
            this.MainPanel.BackgroundImage = global::ZZT.MTHProject.Properties.Resources.MainPanelBG;
            this.MainPanel.Controls.Add(this.thmControl1);
            this.MainPanel.Controls.Add(this.lst_Info);
            this.MainPanel.Controls.Add(this.checkBoxEx12);
            this.MainPanel.Controls.Add(this.thmControl2);
            this.MainPanel.Controls.Add(this.checkBoxEx8);
            this.MainPanel.Controls.Add(this.thmControl3);
            this.MainPanel.Controls.Add(this.checkBoxEx4);
            this.MainPanel.Controls.Add(this.thmControl4);
            this.MainPanel.Controls.Add(this.checkBoxEx11);
            this.MainPanel.Controls.Add(this.thmControl5);
            this.MainPanel.Controls.Add(this.checkBoxEx7);
            this.MainPanel.Controls.Add(this.thmControl6);
            this.MainPanel.Controls.Add(this.checkBoxEx3);
            this.MainPanel.Controls.Add(this.title1);
            this.MainPanel.Controls.Add(this.checkBoxEx10);
            this.MainPanel.Controls.Add(this.title2);
            this.MainPanel.Controls.Add(this.checkBoxEx6);
            this.MainPanel.Controls.Add(this.chart_ActualTrend);
            this.MainPanel.Controls.Add(this.chk_Humidity1);
            this.MainPanel.Controls.Add(this.chk_Temp1);
            this.MainPanel.Controls.Add(this.checkBoxEx9);
            this.MainPanel.Controls.Add(this.checkBoxEx5);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1394, 736);
            this.MainPanel.TabIndex = 5;
            // 
            // thmControl1
            // 
            this.thmControl1.BackColor = System.Drawing.Color.Transparent;
            this.thmControl1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.thmControl1.ForeColor = System.Drawing.Color.White;
            this.thmControl1.Humidity = "0.0";
            this.thmControl1.HumidityVarName = "模块1湿度";
            this.thmControl1.Location = new System.Drawing.Point(11, 14);
            this.thmControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.thmControl1.ModuleError = false;
            this.thmControl1.Name = "thmControl1";
            this.thmControl1.Size = new System.Drawing.Size(390, 241);
            this.thmControl1.StateVarName = "模块异常";
            this.thmControl1.TabIndex = 0;
            this.thmControl1.Temp = "0.0";
            this.thmControl1.TempVarName = "模块1温度";
            this.thmControl1.Title = "1#站点";
            // 
            // lst_Info
            // 
            this.lst_Info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lst_Info.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lst_Info.ForeColor = System.Drawing.Color.White;
            this.lst_Info.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lst_Info.HideSelection = false;
            this.lst_Info.Location = new System.Drawing.Point(813, 540);
            this.lst_Info.Name = "lst_Info";
            this.lst_Info.ShowItemToolTips = true;
            this.lst_Info.Size = new System.Drawing.Size(553, 175);
            this.lst_Info.SmallImageList = this.imageList1;
            this.lst_Info.TabIndex = 4;
            this.lst_Info.UseCompatibleStateImageBehavior = false;
            this.lst_Info.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "日志时间";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "日志内容";
            this.columnHeader2.Width = 200;
            // 
            // checkBoxEx12
            // 
            this.checkBoxEx12.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxEx12.CheckBackColor = System.Drawing.Color.White;
            this.checkBoxEx12.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(25)))), ((int)(((byte)(66)))));
            this.checkBoxEx12.DefaultCheckButtonWidth = 20;
            this.checkBoxEx12.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBoxEx12.ForeColor = System.Drawing.Color.White;
            this.checkBoxEx12.Location = new System.Drawing.Point(1201, 455);
            this.checkBoxEx12.Name = "checkBoxEx12";
            this.checkBoxEx12.Size = new System.Drawing.Size(107, 31);
            this.checkBoxEx12.TabIndex = 3;
            this.checkBoxEx12.Tag = "11";
            this.checkBoxEx12.Text = "6#站点湿度";
            this.checkBoxEx12.UseVisualStyleBackColor = false;
            this.checkBoxEx12.CheckedChanged += new System.EventHandler(this.chk_Common_CheckedChanged);
            // 
            // thmControl2
            // 
            this.thmControl2.BackColor = System.Drawing.Color.Transparent;
            this.thmControl2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.thmControl2.ForeColor = System.Drawing.Color.White;
            this.thmControl2.Humidity = "0.0";
            this.thmControl2.HumidityVarName = "模块2湿度";
            this.thmControl2.Location = new System.Drawing.Point(416, 11);
            this.thmControl2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.thmControl2.ModuleError = false;
            this.thmControl2.Name = "thmControl2";
            this.thmControl2.Size = new System.Drawing.Size(390, 241);
            this.thmControl2.StateVarName = "模块2异常";
            this.thmControl2.TabIndex = 0;
            this.thmControl2.Temp = "0.0";
            this.thmControl2.TempVarName = "模块2温度";
            this.thmControl2.Title = "2#站点";
            // 
            // checkBoxEx8
            // 
            this.checkBoxEx8.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxEx8.CheckBackColor = System.Drawing.Color.White;
            this.checkBoxEx8.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(25)))), ((int)(((byte)(66)))));
            this.checkBoxEx8.DefaultCheckButtonWidth = 20;
            this.checkBoxEx8.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBoxEx8.ForeColor = System.Drawing.Color.White;
            this.checkBoxEx8.Location = new System.Drawing.Point(1201, 422);
            this.checkBoxEx8.Name = "checkBoxEx8";
            this.checkBoxEx8.Size = new System.Drawing.Size(107, 31);
            this.checkBoxEx8.TabIndex = 3;
            this.checkBoxEx8.Tag = "7";
            this.checkBoxEx8.Text = "4#站点湿度";
            this.checkBoxEx8.UseVisualStyleBackColor = false;
            this.checkBoxEx8.CheckedChanged += new System.EventHandler(this.chk_Common_CheckedChanged);
            // 
            // thmControl3
            // 
            this.thmControl3.BackColor = System.Drawing.Color.Transparent;
            this.thmControl3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.thmControl3.ForeColor = System.Drawing.Color.White;
            this.thmControl3.Humidity = "0.0";
            this.thmControl3.HumidityVarName = "模块3湿度";
            this.thmControl3.Location = new System.Drawing.Point(11, 252);
            this.thmControl3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.thmControl3.ModuleError = false;
            this.thmControl3.Name = "thmControl3";
            this.thmControl3.Size = new System.Drawing.Size(390, 241);
            this.thmControl3.StateVarName = "模块3异常";
            this.thmControl3.TabIndex = 0;
            this.thmControl3.Temp = "0.0";
            this.thmControl3.TempVarName = "模块3温度";
            this.thmControl3.Title = "3#站点";
            // 
            // checkBoxEx4
            // 
            this.checkBoxEx4.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxEx4.CheckBackColor = System.Drawing.Color.White;
            this.checkBoxEx4.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(25)))), ((int)(((byte)(66)))));
            this.checkBoxEx4.DefaultCheckButtonWidth = 20;
            this.checkBoxEx4.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBoxEx4.ForeColor = System.Drawing.Color.White;
            this.checkBoxEx4.Location = new System.Drawing.Point(1201, 389);
            this.checkBoxEx4.Name = "checkBoxEx4";
            this.checkBoxEx4.Size = new System.Drawing.Size(107, 31);
            this.checkBoxEx4.TabIndex = 3;
            this.checkBoxEx4.Tag = "3";
            this.checkBoxEx4.Text = "2#站点湿度";
            this.checkBoxEx4.UseVisualStyleBackColor = false;
            this.checkBoxEx4.CheckedChanged += new System.EventHandler(this.chk_Common_CheckedChanged);
            // 
            // thmControl4
            // 
            this.thmControl4.BackColor = System.Drawing.Color.Transparent;
            this.thmControl4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.thmControl4.ForeColor = System.Drawing.Color.White;
            this.thmControl4.Humidity = "0.0";
            this.thmControl4.HumidityVarName = "模块4湿度";
            this.thmControl4.Location = new System.Drawing.Point(416, 252);
            this.thmControl4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.thmControl4.ModuleError = false;
            this.thmControl4.Name = "thmControl4";
            this.thmControl4.Size = new System.Drawing.Size(390, 241);
            this.thmControl4.StateVarName = "模块4异常";
            this.thmControl4.TabIndex = 0;
            this.thmControl4.Temp = "0.0";
            this.thmControl4.TempVarName = "模块4温度";
            this.thmControl4.Title = "4#站点";
            // 
            // checkBoxEx11
            // 
            this.checkBoxEx11.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxEx11.CheckBackColor = System.Drawing.Color.White;
            this.checkBoxEx11.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(25)))), ((int)(((byte)(66)))));
            this.checkBoxEx11.DefaultCheckButtonWidth = 20;
            this.checkBoxEx11.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBoxEx11.ForeColor = System.Drawing.Color.White;
            this.checkBoxEx11.Location = new System.Drawing.Point(1075, 455);
            this.checkBoxEx11.Name = "checkBoxEx11";
            this.checkBoxEx11.Size = new System.Drawing.Size(107, 31);
            this.checkBoxEx11.TabIndex = 3;
            this.checkBoxEx11.Tag = "10";
            this.checkBoxEx11.Text = "6#站点温度";
            this.checkBoxEx11.UseVisualStyleBackColor = false;
            this.checkBoxEx11.CheckedChanged += new System.EventHandler(this.chk_Common_CheckedChanged);
            // 
            // thmControl5
            // 
            this.thmControl5.BackColor = System.Drawing.Color.Transparent;
            this.thmControl5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.thmControl5.ForeColor = System.Drawing.Color.White;
            this.thmControl5.Humidity = "0.0";
            this.thmControl5.HumidityVarName = "模块5湿度";
            this.thmControl5.Location = new System.Drawing.Point(11, 492);
            this.thmControl5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.thmControl5.ModuleError = false;
            this.thmControl5.Name = "thmControl5";
            this.thmControl5.Size = new System.Drawing.Size(390, 241);
            this.thmControl5.StateVarName = "模块5异常";
            this.thmControl5.TabIndex = 0;
            this.thmControl5.Temp = "0.0";
            this.thmControl5.TempVarName = "模块5温度";
            this.thmControl5.Title = "5#站点";
            // 
            // checkBoxEx7
            // 
            this.checkBoxEx7.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxEx7.CheckBackColor = System.Drawing.Color.White;
            this.checkBoxEx7.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(25)))), ((int)(((byte)(66)))));
            this.checkBoxEx7.DefaultCheckButtonWidth = 20;
            this.checkBoxEx7.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBoxEx7.ForeColor = System.Drawing.Color.White;
            this.checkBoxEx7.Location = new System.Drawing.Point(1075, 422);
            this.checkBoxEx7.Name = "checkBoxEx7";
            this.checkBoxEx7.Size = new System.Drawing.Size(107, 31);
            this.checkBoxEx7.TabIndex = 3;
            this.checkBoxEx7.Tag = "6";
            this.checkBoxEx7.Text = "4#站点温度";
            this.checkBoxEx7.UseVisualStyleBackColor = false;
            this.checkBoxEx7.CheckedChanged += new System.EventHandler(this.chk_Common_CheckedChanged);
            // 
            // thmControl6
            // 
            this.thmControl6.BackColor = System.Drawing.Color.Transparent;
            this.thmControl6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.thmControl6.ForeColor = System.Drawing.Color.White;
            this.thmControl6.Humidity = "0.0";
            this.thmControl6.HumidityVarName = "模块6湿度";
            this.thmControl6.Location = new System.Drawing.Point(416, 492);
            this.thmControl6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.thmControl6.ModuleError = false;
            this.thmControl6.Name = "thmControl6";
            this.thmControl6.Size = new System.Drawing.Size(390, 241);
            this.thmControl6.StateVarName = "模块6异常";
            this.thmControl6.TabIndex = 0;
            this.thmControl6.Temp = "0.0";
            this.thmControl6.TempVarName = "模块6温度";
            this.thmControl6.Title = "6#站点";
            // 
            // checkBoxEx3
            // 
            this.checkBoxEx3.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxEx3.CheckBackColor = System.Drawing.Color.White;
            this.checkBoxEx3.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(25)))), ((int)(((byte)(66)))));
            this.checkBoxEx3.DefaultCheckButtonWidth = 20;
            this.checkBoxEx3.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBoxEx3.ForeColor = System.Drawing.Color.White;
            this.checkBoxEx3.Location = new System.Drawing.Point(1075, 389);
            this.checkBoxEx3.Name = "checkBoxEx3";
            this.checkBoxEx3.Size = new System.Drawing.Size(107, 31);
            this.checkBoxEx3.TabIndex = 3;
            this.checkBoxEx3.Tag = "2";
            this.checkBoxEx3.Text = "2#站点温度";
            this.checkBoxEx3.UseVisualStyleBackColor = false;
            this.checkBoxEx3.CheckedChanged += new System.EventHandler(this.chk_Common_CheckedChanged);
            // 
            // title1
            // 
            this.title1.BackColor = System.Drawing.Color.Transparent;
            this.title1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("title1.BackgroundImage")));
            this.title1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.title1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.title1.Location = new System.Drawing.Point(813, 11);
            this.title1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.title1.Name = "title1";
            this.title1.Size = new System.Drawing.Size(109, 31);
            this.title1.TabIndex = 1;
            this.title1.TitleName = "实时趋势";
            // 
            // checkBoxEx10
            // 
            this.checkBoxEx10.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxEx10.CheckBackColor = System.Drawing.Color.White;
            this.checkBoxEx10.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(25)))), ((int)(((byte)(66)))));
            this.checkBoxEx10.DefaultCheckButtonWidth = 20;
            this.checkBoxEx10.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBoxEx10.ForeColor = System.Drawing.Color.White;
            this.checkBoxEx10.Location = new System.Drawing.Point(949, 455);
            this.checkBoxEx10.Name = "checkBoxEx10";
            this.checkBoxEx10.Size = new System.Drawing.Size(107, 31);
            this.checkBoxEx10.TabIndex = 3;
            this.checkBoxEx10.Tag = "9";
            this.checkBoxEx10.Text = "5#站点湿度";
            this.checkBoxEx10.UseVisualStyleBackColor = false;
            this.checkBoxEx10.CheckedChanged += new System.EventHandler(this.chk_Common_CheckedChanged);
            // 
            // title2
            // 
            this.title2.BackColor = System.Drawing.Color.Transparent;
            this.title2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("title2.BackgroundImage")));
            this.title2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.title2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.title2.Location = new System.Drawing.Point(813, 492);
            this.title2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.title2.Name = "title2";
            this.title2.Size = new System.Drawing.Size(109, 31);
            this.title2.TabIndex = 1;
            this.title2.TitleName = "系统日志";
            // 
            // checkBoxEx6
            // 
            this.checkBoxEx6.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxEx6.CheckBackColor = System.Drawing.Color.White;
            this.checkBoxEx6.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(25)))), ((int)(((byte)(66)))));
            this.checkBoxEx6.DefaultCheckButtonWidth = 20;
            this.checkBoxEx6.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBoxEx6.ForeColor = System.Drawing.Color.White;
            this.checkBoxEx6.Location = new System.Drawing.Point(949, 422);
            this.checkBoxEx6.Name = "checkBoxEx6";
            this.checkBoxEx6.Size = new System.Drawing.Size(107, 31);
            this.checkBoxEx6.TabIndex = 3;
            this.checkBoxEx6.Tag = "5";
            this.checkBoxEx6.Text = "3#站点湿度";
            this.checkBoxEx6.UseVisualStyleBackColor = false;
            this.checkBoxEx6.CheckedChanged += new System.EventHandler(this.chk_Common_CheckedChanged);
            // 
            // chart_ActualTrend
            // 
            this.chart_ActualTrend.AxisX.AutoScale = false;
            this.chart_ActualTrend.AxisX.AutoZoomReset = false;
            this.chart_ActualTrend.AxisX.Color = System.Drawing.Color.White;
            this.chart_ActualTrend.AxisX.InitWithScaleView = false;
            this.chart_ActualTrend.AxisX.IsLogarithmic = false;
            this.chart_ActualTrend.AxisX.LabelAngle = 0;
            this.chart_ActualTrend.AxisX.LabelEnabled = true;
            this.chart_ActualTrend.AxisX.LabelFormat = null;
            this.chart_ActualTrend.AxisX.MajorGridColor = System.Drawing.Color.White;
            this.chart_ActualTrend.AxisX.MajorGridCount = 4;
            this.chart_ActualTrend.AxisX.MajorGridEnabled = true;
            this.chart_ActualTrend.AxisX.MajorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.Dash;
            this.chart_ActualTrend.AxisX.Maximum = 1000D;
            this.chart_ActualTrend.AxisX.Minimum = 0D;
            this.chart_ActualTrend.AxisX.MinorGridColor = System.Drawing.Color.Black;
            this.chart_ActualTrend.AxisX.MinorGridEnabled = false;
            this.chart_ActualTrend.AxisX.MinorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.DashDot;
            this.chart_ActualTrend.AxisX.TickWidth = 1F;
            this.chart_ActualTrend.AxisX.Title = "";
            this.chart_ActualTrend.AxisX.TitleOrientation = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextOrientation.Auto;
            this.chart_ActualTrend.AxisX.TitlePosition = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextPosition.Center;
            this.chart_ActualTrend.AxisX.ViewMaximum = 1000D;
            this.chart_ActualTrend.AxisX.ViewMinimum = 0D;
            this.chart_ActualTrend.AxisX2.AutoScale = false;
            this.chart_ActualTrend.AxisX2.AutoZoomReset = false;
            this.chart_ActualTrend.AxisX2.Color = System.Drawing.Color.Black;
            this.chart_ActualTrend.AxisX2.InitWithScaleView = false;
            this.chart_ActualTrend.AxisX2.IsLogarithmic = false;
            this.chart_ActualTrend.AxisX2.LabelAngle = 0;
            this.chart_ActualTrend.AxisX2.LabelEnabled = true;
            this.chart_ActualTrend.AxisX2.LabelFormat = null;
            this.chart_ActualTrend.AxisX2.MajorGridColor = System.Drawing.Color.Black;
            this.chart_ActualTrend.AxisX2.MajorGridCount = 6;
            this.chart_ActualTrend.AxisX2.MajorGridEnabled = true;
            this.chart_ActualTrend.AxisX2.MajorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.Dash;
            this.chart_ActualTrend.AxisX2.Maximum = 1000D;
            this.chart_ActualTrend.AxisX2.Minimum = 0D;
            this.chart_ActualTrend.AxisX2.MinorGridColor = System.Drawing.Color.Black;
            this.chart_ActualTrend.AxisX2.MinorGridEnabled = false;
            this.chart_ActualTrend.AxisX2.MinorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.DashDot;
            this.chart_ActualTrend.AxisX2.TickWidth = 1F;
            this.chart_ActualTrend.AxisX2.Title = "";
            this.chart_ActualTrend.AxisX2.TitleOrientation = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextOrientation.Auto;
            this.chart_ActualTrend.AxisX2.TitlePosition = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextPosition.Center;
            this.chart_ActualTrend.AxisX2.ViewMaximum = 1000D;
            this.chart_ActualTrend.AxisX2.ViewMinimum = 0D;
            this.chart_ActualTrend.AxisY.AutoScale = true;
            this.chart_ActualTrend.AxisY.AutoZoomReset = false;
            this.chart_ActualTrend.AxisY.Color = System.Drawing.Color.White;
            this.chart_ActualTrend.AxisY.InitWithScaleView = false;
            this.chart_ActualTrend.AxisY.IsLogarithmic = false;
            this.chart_ActualTrend.AxisY.LabelAngle = 0;
            this.chart_ActualTrend.AxisY.LabelEnabled = true;
            this.chart_ActualTrend.AxisY.LabelFormat = null;
            this.chart_ActualTrend.AxisY.MajorGridColor = System.Drawing.Color.White;
            this.chart_ActualTrend.AxisY.MajorGridCount = 6;
            this.chart_ActualTrend.AxisY.MajorGridEnabled = true;
            this.chart_ActualTrend.AxisY.MajorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.Dash;
            this.chart_ActualTrend.AxisY.Maximum = 3.5D;
            this.chart_ActualTrend.AxisY.Minimum = 0.5D;
            this.chart_ActualTrend.AxisY.MinorGridColor = System.Drawing.Color.Black;
            this.chart_ActualTrend.AxisY.MinorGridEnabled = false;
            this.chart_ActualTrend.AxisY.MinorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.DashDot;
            this.chart_ActualTrend.AxisY.TickWidth = 1F;
            this.chart_ActualTrend.AxisY.Title = "";
            this.chart_ActualTrend.AxisY.TitleOrientation = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextOrientation.Auto;
            this.chart_ActualTrend.AxisY.TitlePosition = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextPosition.Center;
            this.chart_ActualTrend.AxisY.ViewMaximum = 3.5D;
            this.chart_ActualTrend.AxisY.ViewMinimum = 0.5D;
            this.chart_ActualTrend.AxisY2.AutoScale = true;
            this.chart_ActualTrend.AxisY2.AutoZoomReset = false;
            this.chart_ActualTrend.AxisY2.Color = System.Drawing.Color.Black;
            this.chart_ActualTrend.AxisY2.InitWithScaleView = false;
            this.chart_ActualTrend.AxisY2.IsLogarithmic = false;
            this.chart_ActualTrend.AxisY2.LabelAngle = 0;
            this.chart_ActualTrend.AxisY2.LabelEnabled = true;
            this.chart_ActualTrend.AxisY2.LabelFormat = null;
            this.chart_ActualTrend.AxisY2.MajorGridColor = System.Drawing.Color.Black;
            this.chart_ActualTrend.AxisY2.MajorGridCount = 6;
            this.chart_ActualTrend.AxisY2.MajorGridEnabled = true;
            this.chart_ActualTrend.AxisY2.MajorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.Dash;
            this.chart_ActualTrend.AxisY2.Maximum = 3.5D;
            this.chart_ActualTrend.AxisY2.Minimum = 0.5D;
            this.chart_ActualTrend.AxisY2.MinorGridColor = System.Drawing.Color.Black;
            this.chart_ActualTrend.AxisY2.MinorGridEnabled = false;
            this.chart_ActualTrend.AxisY2.MinorGridType = SeeSharpTools.JY.GUI.StripChartXAxis.GridStyle.DashDot;
            this.chart_ActualTrend.AxisY2.TickWidth = 1F;
            this.chart_ActualTrend.AxisY2.Title = "";
            this.chart_ActualTrend.AxisY2.TitleOrientation = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextOrientation.Auto;
            this.chart_ActualTrend.AxisY2.TitlePosition = SeeSharpTools.JY.GUI.StripChartXAxis.AxisTextPosition.Center;
            this.chart_ActualTrend.AxisY2.ViewMaximum = 3.5D;
            this.chart_ActualTrend.AxisY2.ViewMinimum = 0.5D;
            this.chart_ActualTrend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.chart_ActualTrend.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.chart_ActualTrend.ChartAreaBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.chart_ActualTrend.Direction = SeeSharpTools.JY.GUI.StripChartX.ScrollDirection.LeftToRight;
            this.chart_ActualTrend.DisplayPoints = 4000;
            this.chart_ActualTrend.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.chart_ActualTrend.ForeColor = System.Drawing.Color.White;
            this.chart_ActualTrend.GradientStyle = SeeSharpTools.JY.GUI.StripChartX.ChartGradientStyle.None;
            this.chart_ActualTrend.LegendBackColor = System.Drawing.Color.Transparent;
            this.chart_ActualTrend.LegendFont = new System.Drawing.Font("微软雅黑", 11F);
            this.chart_ActualTrend.LegendForeColor = System.Drawing.Color.White;
            this.chart_ActualTrend.LegendVisible = true;
            stripChartXSeries1.Color = System.Drawing.Color.Red;
            stripChartXSeries1.Marker = SeeSharpTools.JY.GUI.StripChartXSeries.MarkerType.None;
            stripChartXSeries1.Name = "1#站点温度";
            stripChartXSeries1.Type = SeeSharpTools.JY.GUI.StripChartXSeries.LineType.FastLine;
            stripChartXSeries1.Visible = true;
            stripChartXSeries1.Width = SeeSharpTools.JY.GUI.StripChartXSeries.LineWidth.Thin;
            stripChartXSeries1.XPlotAxis = SeeSharpTools.JY.GUI.StripChartXAxis.PlotAxis.Primary;
            stripChartXSeries1.YPlotAxis = SeeSharpTools.JY.GUI.StripChartXAxis.PlotAxis.Primary;
            this.chart_ActualTrend.LineSeries.Add(stripChartXSeries1);
            this.chart_ActualTrend.Location = new System.Drawing.Point(813, 55);
            this.chart_ActualTrend.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chart_ActualTrend.Miscellaneous.CheckInfinity = false;
            this.chart_ActualTrend.Miscellaneous.CheckNaN = false;
            this.chart_ActualTrend.Miscellaneous.CheckNegtiveOrZero = false;
            this.chart_ActualTrend.Miscellaneous.DirectionChartCount = 3;
            this.chart_ActualTrend.Miscellaneous.Fitting = SeeSharpTools.JY.GUI.StripChartX.FitType.Range;
            this.chart_ActualTrend.Miscellaneous.MaxSeriesCount = 32;
            this.chart_ActualTrend.Miscellaneous.MaxSeriesPointCount = 4000;
            this.chart_ActualTrend.Miscellaneous.SplitLayoutColumnInterval = 0F;
            this.chart_ActualTrend.Miscellaneous.SplitLayoutDirection = SeeSharpTools.JY.GUI.StripChartXUtility.LayoutDirection.LeftToRight;
            this.chart_ActualTrend.Miscellaneous.SplitLayoutRowInterval = 0F;
            this.chart_ActualTrend.Miscellaneous.SplitViewAutoLayout = true;
            this.chart_ActualTrend.Name = "chart_ActualTrend";
            this.chart_ActualTrend.NextTimeStamp = new System.DateTime(((long)(0)));
            this.chart_ActualTrend.ScrollType = SeeSharpTools.JY.GUI.StripChartX.StripScrollType.Cumulation;
            this.chart_ActualTrend.SeriesCount = 1;
            this.chart_ActualTrend.Size = new System.Drawing.Size(553, 318);
            this.chart_ActualTrend.SplitView = false;
            this.chart_ActualTrend.StartIndex = 0;
            this.chart_ActualTrend.TabIndex = 2;
            this.chart_ActualTrend.TimeInterval = System.TimeSpan.Parse("00:00:00");
            this.chart_ActualTrend.TimeStampFormat = null;
            this.chart_ActualTrend.XCursor.AutoInterval = true;
            this.chart_ActualTrend.XCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this.chart_ActualTrend.XCursor.Interval = 0.001D;
            this.chart_ActualTrend.XCursor.Mode = SeeSharpTools.JY.GUI.StripChartXCursor.CursorMode.Zoom;
            this.chart_ActualTrend.XCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.chart_ActualTrend.XCursor.Value = double.NaN;
            this.chart_ActualTrend.XDataType = SeeSharpTools.JY.GUI.StripChartX.XAxisDataType.Index;
            this.chart_ActualTrend.YCursor.AutoInterval = true;
            this.chart_ActualTrend.YCursor.Color = System.Drawing.Color.DeepSkyBlue;
            this.chart_ActualTrend.YCursor.Interval = 0.001D;
            this.chart_ActualTrend.YCursor.Mode = SeeSharpTools.JY.GUI.StripChartXCursor.CursorMode.Disabled;
            this.chart_ActualTrend.YCursor.SelectionColor = System.Drawing.Color.LightGray;
            this.chart_ActualTrend.YCursor.Value = double.NaN;
            // 
            // chk_Humidity1
            // 
            this.chk_Humidity1.BackColor = System.Drawing.Color.Transparent;
            this.chk_Humidity1.CheckBackColor = System.Drawing.Color.White;
            this.chk_Humidity1.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(25)))), ((int)(((byte)(66)))));
            this.chk_Humidity1.DefaultCheckButtonWidth = 20;
            this.chk_Humidity1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.chk_Humidity1.ForeColor = System.Drawing.Color.White;
            this.chk_Humidity1.Location = new System.Drawing.Point(949, 389);
            this.chk_Humidity1.Name = "chk_Humidity1";
            this.chk_Humidity1.Size = new System.Drawing.Size(107, 31);
            this.chk_Humidity1.TabIndex = 3;
            this.chk_Humidity1.Tag = "1";
            this.chk_Humidity1.Text = "1#站点湿度";
            this.chk_Humidity1.UseVisualStyleBackColor = false;
            this.chk_Humidity1.CheckedChanged += new System.EventHandler(this.chk_Common_CheckedChanged);
            // 
            // chk_Temp1
            // 
            this.chk_Temp1.BackColor = System.Drawing.Color.Transparent;
            this.chk_Temp1.CheckBackColor = System.Drawing.Color.White;
            this.chk_Temp1.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(25)))), ((int)(((byte)(66)))));
            this.chk_Temp1.DefaultCheckButtonWidth = 20;
            this.chk_Temp1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.chk_Temp1.ForeColor = System.Drawing.Color.White;
            this.chk_Temp1.Location = new System.Drawing.Point(824, 389);
            this.chk_Temp1.Name = "chk_Temp1";
            this.chk_Temp1.Size = new System.Drawing.Size(107, 31);
            this.chk_Temp1.TabIndex = 3;
            this.chk_Temp1.Tag = "0";
            this.chk_Temp1.Text = "1#站点温度";
            this.chk_Temp1.UseVisualStyleBackColor = false;
            this.chk_Temp1.CheckedChanged += new System.EventHandler(this.chk_Common_CheckedChanged);
            // 
            // checkBoxEx9
            // 
            this.checkBoxEx9.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxEx9.CheckBackColor = System.Drawing.Color.White;
            this.checkBoxEx9.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(25)))), ((int)(((byte)(66)))));
            this.checkBoxEx9.DefaultCheckButtonWidth = 20;
            this.checkBoxEx9.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBoxEx9.ForeColor = System.Drawing.Color.White;
            this.checkBoxEx9.Location = new System.Drawing.Point(824, 455);
            this.checkBoxEx9.Name = "checkBoxEx9";
            this.checkBoxEx9.Size = new System.Drawing.Size(107, 31);
            this.checkBoxEx9.TabIndex = 3;
            this.checkBoxEx9.Tag = "8";
            this.checkBoxEx9.Text = "5#站点温度";
            this.checkBoxEx9.UseVisualStyleBackColor = false;
            this.checkBoxEx9.CheckedChanged += new System.EventHandler(this.chk_Common_CheckedChanged);
            // 
            // checkBoxEx5
            // 
            this.checkBoxEx5.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxEx5.CheckBackColor = System.Drawing.Color.White;
            this.checkBoxEx5.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(25)))), ((int)(((byte)(66)))));
            this.checkBoxEx5.DefaultCheckButtonWidth = 20;
            this.checkBoxEx5.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBoxEx5.ForeColor = System.Drawing.Color.White;
            this.checkBoxEx5.Location = new System.Drawing.Point(824, 422);
            this.checkBoxEx5.Name = "checkBoxEx5";
            this.checkBoxEx5.Size = new System.Drawing.Size(107, 31);
            this.checkBoxEx5.TabIndex = 3;
            this.checkBoxEx5.Tag = "4";
            this.checkBoxEx5.Text = "3#站点温度";
            this.checkBoxEx5.UseVisualStyleBackColor = false;
            this.checkBoxEx5.CheckedChanged += new System.EventHandler(this.chk_Common_CheckedChanged);
            // 
            // FrmMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1394, 736);
            this.Controls.Add(this.MainPanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmMonitor";
            this.Text = "集中监控";
            this.MainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ZZT.MTHControlLib.THMControl thmControl3;
        private ZZT.MTHControlLib.THMControl thmControl5;
        private ZZT.MTHControlLib.THMControl thmControl2;
        private ZZT.MTHControlLib.THMControl thmControl4;
        private ZZT.MTHControlLib.THMControl thmControl6;
        private ZZT.MTHControlLib.Title title1;
        private ZZT.MTHControlLib.Title title2;
        private SeeSharpTools.JY.GUI.StripChartX chart_ActualTrend;
        private ZZT.MTHControlLib.CheckBoxEx chk_Temp1;
        private ZZT.MTHControlLib.CheckBoxEx chk_Humidity1;
        private ZZT.MTHControlLib.CheckBoxEx checkBoxEx3;
        private ZZT.MTHControlLib.CheckBoxEx checkBoxEx4;
        private ZZT.MTHControlLib.CheckBoxEx checkBoxEx5;
        private ZZT.MTHControlLib.CheckBoxEx checkBoxEx6;
        private ZZT.MTHControlLib.CheckBoxEx checkBoxEx7;
        private ZZT.MTHControlLib.CheckBoxEx checkBoxEx8;
        private ZZT.MTHControlLib.CheckBoxEx checkBoxEx9;
        private ZZT.MTHControlLib.CheckBoxEx checkBoxEx10;
        private ZZT.MTHControlLib.CheckBoxEx checkBoxEx11;
        private ZZT.MTHControlLib.CheckBoxEx checkBoxEx12;
        private System.Windows.Forms.ListView lst_Info;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private ZZT.MTHControlLib.PanelEnhanced MainPanel;
        private ZZT.MTHControlLib.THMControl thmControl1;
    }
}