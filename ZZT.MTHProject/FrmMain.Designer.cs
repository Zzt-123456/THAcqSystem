namespace ZZT.MTHProject
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.TopPanel = new System.Windows.Forms.Panel();
            this.naviButton6 = new ZZT.MTHControlLib.NaviButton();
            this.naviButton3 = new ZZT.MTHControlLib.NaviButton();
            this.naviButton5 = new ZZT.MTHControlLib.NaviButton();
            this.naviButton4 = new ZZT.MTHControlLib.NaviButton();
            this.naviButton2 = new ZZT.MTHControlLib.NaviButton();
            this.navi_Monitor = new ZZT.MTHControlLib.NaviButton();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.lbl_TitleName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.MiddlePanel = new System.Windows.Forms.Panel();
            this.AlarmPanel = new System.Windows.Forms.Panel();
            this.scrollingAlarm = new SeeSharpTools.JY.GUI.ScrollingText();
            this.btn_Right = new System.Windows.Forms.Button();
            this.btn_Left = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_CurrentTime = new System.Windows.Forms.Label();
            this.lbl_User = new System.Windows.Forms.Label();
            this.CorePanel = new ZZT.MTHControlLib.PanelEx();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.led_CommState = new SeeSharpTools.JY.GUI.LED();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.MiddlePanel.SuspendLayout();
            this.AlarmPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.CorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.Transparent;
            this.TopPanel.Controls.Add(this.naviButton6);
            this.TopPanel.Controls.Add(this.naviButton3);
            this.TopPanel.Controls.Add(this.naviButton5);
            this.TopPanel.Controls.Add(this.naviButton4);
            this.TopPanel.Controls.Add(this.naviButton2);
            this.TopPanel.Controls.Add(this.navi_Monitor);
            this.TopPanel.Controls.Add(this.btn_Exit);
            this.TopPanel.Controls.Add(this.lbl_TitleName);
            this.TopPanel.Controls.Add(this.label1);
            this.TopPanel.Controls.Add(this.pictureBox1);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(1440, 133);
            this.TopPanel.TabIndex = 0;
            // 
            // naviButton6
            // 
            this.naviButton6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("naviButton6.BackgroundImage")));
            this.naviButton6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.naviButton6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.naviButton6.IsLeft = false;
            this.naviButton6.IsSelected = false;
            this.naviButton6.Location = new System.Drawing.Point(1296, 71);
            this.naviButton6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.naviButton6.Name = "naviButton6";
            this.naviButton6.Size = new System.Drawing.Size(129, 43);
            this.naviButton6.TabIndex = 3;
            this.naviButton6.TitleName = "用户管理";
            this.naviButton6.Click += new System.EventHandler(this.CommonNaviButton_Click);
            // 
            // naviButton3
            // 
            this.naviButton3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("naviButton3.BackgroundImage")));
            this.naviButton3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.naviButton3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.naviButton3.IsLeft = true;
            this.naviButton3.IsSelected = false;
            this.naviButton3.Location = new System.Drawing.Point(342, 71);
            this.naviButton3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.naviButton3.Name = "naviButton3";
            this.naviButton3.Size = new System.Drawing.Size(129, 43);
            this.naviButton3.TabIndex = 3;
            this.naviButton3.TitleName = "配方管理";
            this.naviButton3.Click += new System.EventHandler(this.CommonNaviButton_Click);
            // 
            // naviButton5
            // 
            this.naviButton5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("naviButton5.BackgroundImage")));
            this.naviButton5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.naviButton5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.naviButton5.IsLeft = false;
            this.naviButton5.IsSelected = false;
            this.naviButton5.Location = new System.Drawing.Point(1135, 71);
            this.naviButton5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.naviButton5.Name = "naviButton5";
            this.naviButton5.Size = new System.Drawing.Size(129, 43);
            this.naviButton5.TabIndex = 3;
            this.naviButton5.TitleName = "历史趋势";
            this.naviButton5.Click += new System.EventHandler(this.CommonNaviButton_Click);
            // 
            // naviButton4
            // 
            this.naviButton4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("naviButton4.BackgroundImage")));
            this.naviButton4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.naviButton4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.naviButton4.IsLeft = false;
            this.naviButton4.IsSelected = false;
            this.naviButton4.Location = new System.Drawing.Point(972, 71);
            this.naviButton4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.naviButton4.Name = "naviButton4";
            this.naviButton4.Size = new System.Drawing.Size(129, 43);
            this.naviButton4.TabIndex = 3;
            this.naviButton4.TitleName = "报警追溯";
            this.naviButton4.Click += new System.EventHandler(this.CommonNaviButton_Click);
            // 
            // naviButton2
            // 
            this.naviButton2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("naviButton2.BackgroundImage")));
            this.naviButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.naviButton2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.naviButton2.IsLeft = true;
            this.naviButton2.IsSelected = false;
            this.naviButton2.Location = new System.Drawing.Point(181, 71);
            this.naviButton2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.naviButton2.Name = "naviButton2";
            this.naviButton2.Size = new System.Drawing.Size(129, 43);
            this.naviButton2.TabIndex = 3;
            this.naviButton2.TitleName = "参数设置";
            this.naviButton2.Click += new System.EventHandler(this.CommonNaviButton_Click);
            // 
            // navi_Monitor
            // 
            this.navi_Monitor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("navi_Monitor.BackgroundImage")));
            this.navi_Monitor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.navi_Monitor.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.navi_Monitor.IsLeft = true;
            this.navi_Monitor.IsSelected = false;
            this.navi_Monitor.Location = new System.Drawing.Point(18, 71);
            this.navi_Monitor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.navi_Monitor.Name = "navi_Monitor";
            this.navi_Monitor.Size = new System.Drawing.Size(129, 43);
            this.navi_Monitor.TabIndex = 3;
            this.navi_Monitor.TitleName = "集中监控";
            this.navi_Monitor.Click += new System.EventHandler(this.CommonNaviButton_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.BackgroundImage = global::ZZT.MTHProject.Properties.Resources.Exit;
            this.btn_Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_Exit.FlatAppearance.BorderSize = 0;
            this.btn_Exit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Exit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Exit.Location = new System.Drawing.Point(1329, 12);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(93, 46);
            this.btn_Exit.TabIndex = 2;
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // lbl_TitleName
            // 
            this.lbl_TitleName.Font = new System.Drawing.Font("微软雅黑", 23.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_TitleName.ForeColor = System.Drawing.Color.White;
            this.lbl_TitleName.Location = new System.Drawing.Point(551, 31);
            this.lbl_TitleName.Name = "lbl_TitleName";
            this.lbl_TitleName.Size = new System.Drawing.Size(340, 61);
            this.lbl_TitleName.TabIndex = 1;
            this.lbl_TitleName.Text = "多温湿度采集监控系统";
            this.lbl_TitleName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(74, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 36);
            this.label1.TabIndex = 1;
            this.label1.Text = "ZZT";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ZZT.MTHProject.Properties.Resources.Logo;
            this.pictureBox1.Location = new System.Drawing.Point(18, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(42, 42);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // MiddlePanel
            // 
            this.MiddlePanel.BackColor = System.Drawing.Color.Transparent;
            this.MiddlePanel.Controls.Add(this.led_CommState);
            this.MiddlePanel.Controls.Add(this.AlarmPanel);
            this.MiddlePanel.Controls.Add(this.btn_Right);
            this.MiddlePanel.Controls.Add(this.btn_Left);
            this.MiddlePanel.Controls.Add(this.pictureBox2);
            this.MiddlePanel.Controls.Add(this.lbl_Title);
            this.MiddlePanel.Controls.Add(this.label6);
            this.MiddlePanel.Controls.Add(this.label3);
            this.MiddlePanel.Controls.Add(this.lbl_CurrentTime);
            this.MiddlePanel.Controls.Add(this.lbl_User);
            this.MiddlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.MiddlePanel.Location = new System.Drawing.Point(0, 133);
            this.MiddlePanel.Name = "MiddlePanel";
            this.MiddlePanel.Size = new System.Drawing.Size(1440, 65);
            this.MiddlePanel.TabIndex = 1;
            // 
            // AlarmPanel
            // 
            this.AlarmPanel.BackgroundImage = global::ZZT.MTHProject.Properties.Resources.Alarm;
            this.AlarmPanel.Controls.Add(this.scrollingAlarm);
            this.AlarmPanel.Location = new System.Drawing.Point(1000, 15);
            this.AlarmPanel.Name = "AlarmPanel";
            this.AlarmPanel.Size = new System.Drawing.Size(309, 41);
            this.AlarmPanel.TabIndex = 3;
            // 
            // scrollingAlarm
            // 
            this.scrollingAlarm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(28)))), ((int)(((byte)(68)))));
            this.scrollingAlarm.BorderColor = System.Drawing.Color.Transparent;
            this.scrollingAlarm.BorderVisible = true;
            this.scrollingAlarm.Cursor = System.Windows.Forms.Cursors.Default;
            this.scrollingAlarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollingAlarm.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.scrollingAlarm.ForeColor = System.Drawing.Color.White;
            this.scrollingAlarm.Location = new System.Drawing.Point(0, 0);
            this.scrollingAlarm.Name = "scrollingAlarm";
            this.scrollingAlarm.Padding = new System.Windows.Forms.Padding(3);
            this.scrollingAlarm.ScrollDirection = SeeSharpTools.JY.GUI.ScrollingText.TextDirection.RightToLeft;
            this.scrollingAlarm.ScrollSpeed = 25;
            this.scrollingAlarm.Size = new System.Drawing.Size(309, 41);
            this.scrollingAlarm.TabIndex = 0;
            this.scrollingAlarm.Text = "当前系统无报警";
            this.scrollingAlarm.VerticleAligment = SeeSharpTools.JY.GUI.ScrollingText.TextVerticalAlignment.Center;
            // 
            // btn_Right
            // 
            this.btn_Right.BackgroundImage = global::ZZT.MTHProject.Properties.Resources.Right;
            this.btn_Right.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_Right.FlatAppearance.BorderSize = 0;
            this.btn_Right.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Right.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Right.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Right.Location = new System.Drawing.Point(828, 10);
            this.btn_Right.Name = "btn_Right";
            this.btn_Right.Size = new System.Drawing.Size(49, 46);
            this.btn_Right.TabIndex = 2;
            this.btn_Right.UseVisualStyleBackColor = true;
            this.btn_Right.Click += new System.EventHandler(this.btn_Right_Click);
            // 
            // btn_Left
            // 
            this.btn_Left.BackgroundImage = global::ZZT.MTHProject.Properties.Resources.Left;
            this.btn_Left.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_Left.FlatAppearance.BorderSize = 0;
            this.btn_Left.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_Left.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Left.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Left.Location = new System.Drawing.Point(558, 10);
            this.btn_Left.Name = "btn_Left";
            this.btn_Left.Size = new System.Drawing.Size(49, 46);
            this.btn_Left.TabIndex = 2;
            this.btn_Left.UseVisualStyleBackColor = true;
            this.btn_Left.Click += new System.EventHandler(this.btn_Left_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ZZT.MTHProject.Properties.Resources.User;
            this.pictureBox2.Location = new System.Drawing.Point(18, 23);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // lbl_Title
            // 
            this.lbl_Title.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold);
            this.lbl_Title.ForeColor = System.Drawing.Color.White;
            this.lbl_Title.Image = global::ZZT.MTHProject.Properties.Resources.Current;
            this.lbl_Title.Location = new System.Drawing.Point(624, 9);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(184, 49);
            this.lbl_Title.TabIndex = 1;
            this.lbl_Title.Text = "集中监控";
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 11.5F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(1325, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 22);
            this.label6.TabIndex = 1;
            this.label6.Text = "通信状态";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 11.5F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(127, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 22);
            this.label3.TabIndex = 1;
            this.label3.Text = "欢迎登录！现在时间是";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_CurrentTime
            // 
            this.lbl_CurrentTime.AutoSize = true;
            this.lbl_CurrentTime.Font = new System.Drawing.Font("微软雅黑", 11.5F, System.Drawing.FontStyle.Bold);
            this.lbl_CurrentTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(233)))), ((int)(((byte)(255)))));
            this.lbl_CurrentTime.Location = new System.Drawing.Point(303, 22);
            this.lbl_CurrentTime.Name = "lbl_CurrentTime";
            this.lbl_CurrentTime.Size = new System.Drawing.Size(256, 22);
            this.lbl_CurrentTime.TabIndex = 1;
            this.lbl_CurrentTime.Text = "2026年6月30日 09:00:00 星期二";
            this.lbl_CurrentTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_User
            // 
            this.lbl_User.Font = new System.Drawing.Font("微软雅黑", 11.5F, System.Drawing.FontStyle.Bold);
            this.lbl_User.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(233)))), ((int)(((byte)(255)))));
            this.lbl_User.Location = new System.Drawing.Point(45, 15);
            this.lbl_User.Name = "lbl_User";
            this.lbl_User.Size = new System.Drawing.Size(68, 36);
            this.lbl_User.TabIndex = 1;
            this.lbl_User.Text = "管理员";
            this.lbl_User.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CorePanel
            // 
            this.CorePanel.BackColor = System.Drawing.Color.Transparent;
            this.CorePanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(79)))), ((int)(((byte)(150)))));
            this.CorePanel.BorderWidth = 3;
            this.CorePanel.BottomGap = 10;
            this.CorePanel.Controls.Add(this.MainPanel);
            this.CorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CorePanel.LeftGap = 20;
            this.CorePanel.Location = new System.Drawing.Point(0, 198);
            this.CorePanel.Name = "CorePanel";
            this.CorePanel.Padding = new System.Windows.Forms.Padding(23, 13, 23, 13);
            this.CorePanel.RightGap = 20;
            this.CorePanel.Size = new System.Drawing.Size(1440, 762);
            this.CorePanel.TabIndex = 2;
            this.CorePanel.TopGap = 10;
            // 
            // MainPanel
            // 
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(23, 13);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1394, 736);
            this.MainPanel.TabIndex = 0;
            // 
            // led_CommState
            // 
            this.led_CommState.BlinkColor = System.Drawing.Color.Lime;
            this.led_CommState.BlinkInterval = 1000;
            this.led_CommState.BlinkOn = false;
            this.led_CommState.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.led_CommState.Interacton = SeeSharpTools.JY.GUI.LED.InteractionStyle.Indicator;
            this.led_CommState.Location = new System.Drawing.Point(1404, 21);
            this.led_CommState.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.led_CommState.Name = "led_CommState";
            this.led_CommState.OffColor = System.Drawing.Color.Red;
            this.led_CommState.OnColor = System.Drawing.Color.Lime;
            this.led_CommState.Size = new System.Drawing.Size(24, 24);
            this.led_CommState.Style = SeeSharpTools.JY.GUI.LED.LedStyle.Circular;
            this.led_CommState.TabIndex = 4;
            this.led_CommState.Value = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ZZT.MTHProject.Properties.Resources.Main;
            this.ClientSize = new System.Drawing.Size(1440, 960);
            this.Controls.Add(this.CorePanel);
            this.Controls.Add(this.MiddlePanel);
            this.Controls.Add(this.TopPanel);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmMain";
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.MiddlePanel.ResumeLayout(false);
            this.MiddlePanel.PerformLayout();
            this.AlarmPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.CorePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel MiddlePanel;
        private System.Windows.Forms.Label lbl_TitleName;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lbl_User;
        private System.Windows.Forms.Button btn_Left;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_CurrentTime;
        private System.Windows.Forms.Button btn_Right;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Panel AlarmPanel;
        private System.Windows.Forms.Label label6;
        private ZZT.MTHControlLib.NaviButton naviButton6;
        private ZZT.MTHControlLib.NaviButton naviButton3;
        private ZZT.MTHControlLib.NaviButton naviButton5;
        private ZZT.MTHControlLib.NaviButton naviButton4;
        private ZZT.MTHControlLib.NaviButton naviButton2;
        private ZZT.MTHControlLib.NaviButton navi_Monitor;
        private ZZT.MTHControlLib.PanelEx CorePanel;
        private System.Windows.Forms.Panel MainPanel;
        private SeeSharpTools.JY.GUI.ScrollingText scrollingAlarm;
        private SeeSharpTools.JY.GUI.LED led_CommState;
    }
}

