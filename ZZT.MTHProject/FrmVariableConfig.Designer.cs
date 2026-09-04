namespace ZZT.MTHProject
{
    partial class FrmVariableConfig
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVariableConfig));
            this.panelEx1 = new ZZT.MTHControlLib.PanelEx();
            this.chk_NegAlarm = new ZZT.MTHControlLib.CheckBoxEx();
            this.chk_PosAlarm = new ZZT.MTHControlLib.CheckBoxEx();
            this.dgv_Main = new System.Windows.Forms.DataGridView();
            this.VarName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OffsetOrLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Scale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Offset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PosAlarm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NegAlarm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReMark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Modify = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.cmb_GroupName = new System.Windows.Forms.ComboBox();
            this.cmb_DataType = new System.Windows.Forms.ComboBox();
            this.num_OffsetOrLength = new System.Windows.Forms.NumericUpDown();
            this.num_Offset = new System.Windows.Forms.NumericUpDown();
            this.num_Scale = new System.Windows.Forms.NumericUpDown();
            this.num_Start = new System.Windows.Forms.NumericUpDown();
            this.txt_VarName = new System.Windows.Forms.TextBox();
            this.txt_Remark = new System.Windows.Forms.TextBox();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.btn_Close = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Main)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_OffsetOrLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Offset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Scale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Start)).BeginInit();
            this.TopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.BackColor = System.Drawing.Color.Transparent;
            this.panelEx1.BackgroundImage = global::ZZT.MTHProject.Properties.Resources.MainPanelBG;
            this.panelEx1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(68)))), ((int)(((byte)(78)))));
            this.panelEx1.BorderWidth = 2;
            this.panelEx1.BottomGap = 1;
            this.panelEx1.Controls.Add(this.chk_NegAlarm);
            this.panelEx1.Controls.Add(this.chk_PosAlarm);
            this.panelEx1.Controls.Add(this.dgv_Main);
            this.panelEx1.Controls.Add(this.btn_Modify);
            this.panelEx1.Controls.Add(this.btn_Delete);
            this.panelEx1.Controls.Add(this.btn_Add);
            this.panelEx1.Controls.Add(this.cmb_GroupName);
            this.panelEx1.Controls.Add(this.cmb_DataType);
            this.panelEx1.Controls.Add(this.num_OffsetOrLength);
            this.panelEx1.Controls.Add(this.num_Offset);
            this.panelEx1.Controls.Add(this.num_Scale);
            this.panelEx1.Controls.Add(this.num_Start);
            this.panelEx1.Controls.Add(this.txt_VarName);
            this.panelEx1.Controls.Add(this.txt_Remark);
            this.panelEx1.Controls.Add(this.TopPanel);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.label9);
            this.panelEx1.Controls.Add(this.label8);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.label5);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.label7);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.LeftGap = 1;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.RightGap = 1;
            this.panelEx1.Size = new System.Drawing.Size(1004, 673);
            this.panelEx1.TabIndex = 0;
            this.panelEx1.TopGap = 1;
            this.panelEx1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.panelEx1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseMove);
            // 
            // chk_NegAlarm
            // 
            this.chk_NegAlarm.CheckBackColor = System.Drawing.Color.White;
            this.chk_NegAlarm.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(25)))), ((int)(((byte)(66)))));
            this.chk_NegAlarm.DefaultCheckButtonWidth = 20;
            this.chk_NegAlarm.ForeColor = System.Drawing.Color.White;
            this.chk_NegAlarm.Location = new System.Drawing.Point(445, 161);
            this.chk_NegAlarm.Name = "chk_NegAlarm";
            this.chk_NegAlarm.Size = new System.Drawing.Size(139, 27);
            this.chk_NegAlarm.TabIndex = 8;
            this.chk_NegAlarm.Text = "上升沿报警";
            this.chk_NegAlarm.UseVisualStyleBackColor = true;
            // 
            // chk_PosAlarm
            // 
            this.chk_PosAlarm.CheckBackColor = System.Drawing.Color.White;
            this.chk_PosAlarm.CheckColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(25)))), ((int)(((byte)(66)))));
            this.chk_PosAlarm.DefaultCheckButtonWidth = 20;
            this.chk_PosAlarm.ForeColor = System.Drawing.Color.White;
            this.chk_PosAlarm.Location = new System.Drawing.Point(298, 161);
            this.chk_PosAlarm.Name = "chk_PosAlarm";
            this.chk_PosAlarm.Size = new System.Drawing.Size(139, 27);
            this.chk_PosAlarm.TabIndex = 8;
            this.chk_PosAlarm.Text = "上升沿报警";
            this.chk_PosAlarm.UseVisualStyleBackColor = true;
            // 
            // dgv_Main
            // 
            this.dgv_Main.AllowUserToAddRows = false;
            this.dgv_Main.AllowUserToDeleteRows = false;
            this.dgv_Main.AllowUserToResizeColumns = false;
            this.dgv_Main.AllowUserToResizeRows = false;
            this.dgv_Main.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.dgv_Main.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_Main.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Main.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_Main.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VarName,
            this.Start,
            this.OffsetOrLength,
            this.DataType,
            this.Scale,
            this.Offset,
            this.PosAlarm,
            this.NegAlarm,
            this.GroupName,
            this.ReMark});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(40)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(40)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Main.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_Main.EnableHeadersVisualStyles = false;
            this.dgv_Main.GridColor = System.Drawing.Color.White;
            this.dgv_Main.Location = new System.Drawing.Point(25, 276);
            this.dgv_Main.Name = "dgv_Main";
            this.dgv_Main.ReadOnly = true;
            this.dgv_Main.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(40)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(40)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Main.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_Main.RowHeadersWidth = 45;
            this.dgv_Main.RowTemplate.Height = 40;
            this.dgv_Main.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Main.Size = new System.Drawing.Size(959, 378);
            this.dgv_Main.TabIndex = 7;
            this.dgv_Main.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Main_CellClick);
            this.dgv_Main.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_Main_CellFormatting);
            this.dgv_Main.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgv_Main_RowPostPaint);
            // 
            // VarName
            // 
            this.VarName.DataPropertyName = "VarName";
            this.VarName.HeaderText = "变量名称";
            this.VarName.Name = "VarName";
            this.VarName.ReadOnly = true;
            this.VarName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.VarName.Width = 140;
            // 
            // Start
            // 
            this.Start.DataPropertyName = "Start";
            this.Start.HeaderText = "起始索引";
            this.Start.Name = "Start";
            this.Start.ReadOnly = true;
            this.Start.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Start.Width = 90;
            // 
            // OffsetOrLength
            // 
            this.OffsetOrLength.DataPropertyName = "OffsetOrLength";
            this.OffsetOrLength.HeaderText = "位偏移/长度";
            this.OffsetOrLength.Name = "OffsetOrLength";
            this.OffsetOrLength.ReadOnly = true;
            this.OffsetOrLength.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OffsetOrLength.Width = 110;
            // 
            // DataType
            // 
            this.DataType.DataPropertyName = "DataType";
            this.DataType.HeaderText = "数据类型";
            this.DataType.Name = "DataType";
            this.DataType.ReadOnly = true;
            this.DataType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DataType.Width = 80;
            // 
            // Scale
            // 
            this.Scale.DataPropertyName = "Scale";
            this.Scale.HeaderText = "线性系数";
            this.Scale.Name = "Scale";
            this.Scale.ReadOnly = true;
            this.Scale.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Scale.Width = 80;
            // 
            // Offset
            // 
            this.Offset.DataPropertyName = "Offset";
            this.Offset.HeaderText = "偏移量";
            this.Offset.Name = "Offset";
            this.Offset.ReadOnly = true;
            this.Offset.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Offset.Width = 80;
            // 
            // PosAlarm
            // 
            this.PosAlarm.DataPropertyName = "PosAlarm";
            this.PosAlarm.HeaderText = "上升沿";
            this.PosAlarm.Name = "PosAlarm";
            this.PosAlarm.ReadOnly = true;
            this.PosAlarm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PosAlarm.Width = 80;
            // 
            // NegAlarm
            // 
            this.NegAlarm.DataPropertyName = "NegAlarm";
            this.NegAlarm.HeaderText = "下降沿";
            this.NegAlarm.Name = "NegAlarm";
            this.NegAlarm.ReadOnly = true;
            this.NegAlarm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NegAlarm.Width = 80;
            // 
            // GroupName
            // 
            this.GroupName.DataPropertyName = "GroupName";
            this.GroupName.HeaderText = "通信组";
            this.GroupName.Name = "GroupName";
            this.GroupName.ReadOnly = true;
            this.GroupName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GroupName.Width = 90;
            // 
            // ReMark
            // 
            this.ReMark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ReMark.DataPropertyName = "Remark";
            this.ReMark.HeaderText = "备注说明";
            this.ReMark.Name = "ReMark";
            this.ReMark.ReadOnly = true;
            this.ReMark.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btn_Modify
            // 
            this.btn_Modify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_Modify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Modify.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btn_Modify.ForeColor = System.Drawing.Color.White;
            this.btn_Modify.Location = new System.Drawing.Point(874, 220);
            this.btn_Modify.Name = "btn_Modify";
            this.btn_Modify.Size = new System.Drawing.Size(98, 42);
            this.btn_Modify.TabIndex = 6;
            this.btn_Modify.Text = "修改变量";
            this.btn_Modify.UseVisualStyleBackColor = false;
            this.btn_Modify.Click += new System.EventHandler(this.btn_Modify_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_Delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Delete.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btn_Delete.ForeColor = System.Drawing.Color.White;
            this.btn_Delete.Location = new System.Drawing.Point(734, 220);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(98, 42);
            this.btn_Delete.TabIndex = 6;
            this.btn_Delete.Text = "删除变量";
            this.btn_Delete.UseVisualStyleBackColor = false;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btn_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Add.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btn_Add.ForeColor = System.Drawing.Color.White;
            this.btn_Add.Location = new System.Drawing.Point(594, 220);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(98, 42);
            this.btn_Add.TabIndex = 6;
            this.btn_Add.Text = "新增变量";
            this.btn_Add.UseVisualStyleBackColor = false;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // cmb_GroupName
            // 
            this.cmb_GroupName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_GroupName.FormattingEnabled = true;
            this.cmb_GroupName.Location = new System.Drawing.Point(142, 90);
            this.cmb_GroupName.Name = "cmb_GroupName";
            this.cmb_GroupName.Size = new System.Drawing.Size(121, 29);
            this.cmb_GroupName.TabIndex = 5;
            // 
            // cmb_DataType
            // 
            this.cmb_DataType.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_DataType.FormattingEnabled = true;
            this.cmb_DataType.Location = new System.Drawing.Point(142, 160);
            this.cmb_DataType.Name = "cmb_DataType";
            this.cmb_DataType.Size = new System.Drawing.Size(121, 29);
            this.cmb_DataType.TabIndex = 5;
            // 
            // num_OffsetOrLength
            // 
            this.num_OffsetOrLength.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_OffsetOrLength.Location = new System.Drawing.Point(907, 91);
            this.num_OffsetOrLength.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.num_OffsetOrLength.Name = "num_OffsetOrLength";
            this.num_OffsetOrLength.Size = new System.Drawing.Size(63, 29);
            this.num_OffsetOrLength.TabIndex = 4;
            this.num_OffsetOrLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // num_Offset
            // 
            this.num_Offset.DecimalPlaces = 1;
            this.num_Offset.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_Offset.Location = new System.Drawing.Point(903, 161);
            this.num_Offset.Name = "num_Offset";
            this.num_Offset.Size = new System.Drawing.Size(67, 29);
            this.num_Offset.TabIndex = 4;
            this.num_Offset.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            // 
            // num_Scale
            // 
            this.num_Scale.DecimalPlaces = 1;
            this.num_Scale.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_Scale.Location = new System.Drawing.Point(702, 161);
            this.num_Scale.Name = "num_Scale";
            this.num_Scale.Size = new System.Drawing.Size(67, 29);
            this.num_Scale.TabIndex = 4;
            this.num_Scale.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            // 
            // num_Start
            // 
            this.num_Start.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.num_Start.Location = new System.Drawing.Point(702, 91);
            this.num_Start.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.num_Start.Name = "num_Start";
            this.num_Start.Size = new System.Drawing.Size(67, 29);
            this.num_Start.TabIndex = 4;
            // 
            // txt_VarName
            // 
            this.txt_VarName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_VarName.Location = new System.Drawing.Point(399, 90);
            this.txt_VarName.Name = "txt_VarName";
            this.txt_VarName.Size = new System.Drawing.Size(153, 29);
            this.txt_VarName.TabIndex = 3;
            this.txt_VarName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_Remark
            // 
            this.txt_Remark.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Remark.Location = new System.Drawing.Point(142, 227);
            this.txt_Remark.Name = "txt_Remark";
            this.txt_Remark.Size = new System.Drawing.Size(410, 29);
            this.txt_Remark.TabIndex = 3;
            this.txt_Remark.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.btn_Close);
            this.TopPanel.Controls.Add(this.label1);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(1004, 61);
            this.TopPanel.TabIndex = 0;
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.btn_Close.FlatAppearance.BorderSize = 0;
            this.btn_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(29)))), ((int)(((byte)(84)))));
            this.btn_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.Font = new System.Drawing.Font("微软雅黑", 15.5F);
            this.btn_Close.ForeColor = System.Drawing.Color.White;
            this.btn_Close.Location = new System.Drawing.Point(946, 12);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(46, 42);
            this.btn_Close.TabIndex = 4;
            this.btn_Close.Text = "X";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(21, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "变量配置";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(786, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 21);
            this.label4.TabIndex = 2;
            this.label4.Text = "位偏移长度";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(807, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 21);
            this.label9.TabIndex = 2;
            this.label9.Text = "偏移量";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(599, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 21);
            this.label8.TabIndex = 2;
            this.label8.Text = "线性系数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(599, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "起始索引";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(37, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 21);
            this.label5.TabIndex = 2;
            this.label5.Text = "数据类型";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(37, 230);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 21);
            this.label6.TabIndex = 2;
            this.label6.Text = "备注说明";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(294, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 21);
            this.label7.TabIndex = 2;
            this.label7.Text = "变量名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(21, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "通信组名称";
            // 
            // FrmVariableConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 673);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmVariableConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmGroupConfig";
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Main)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_OffsetOrLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Offset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Scale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Start)).EndInit();
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ZZT.MTHControlLib.PanelEx panelEx1;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown num_OffsetOrLength;
        private System.Windows.Forms.NumericUpDown num_Start;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_DataType;
        private System.Windows.Forms.TextBox txt_Remark;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgv_Main;
        private System.Windows.Forms.Button btn_Modify;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.ComboBox cmb_GroupName;
        private System.Windows.Forms.TextBox txt_VarName;
        private System.Windows.Forms.Label label7;
        private ZZT.MTHControlLib.CheckBoxEx chk_PosAlarm;
        private ZZT.MTHControlLib.CheckBoxEx chk_NegAlarm;
        private System.Windows.Forms.NumericUpDown num_Offset;
        private System.Windows.Forms.NumericUpDown num_Scale;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Start;
        private System.Windows.Forms.DataGridViewTextBoxColumn OffsetOrLength;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scale;
        private System.Windows.Forms.DataGridViewTextBoxColumn Offset;
        private System.Windows.Forms.DataGridViewTextBoxColumn PosAlarm;
        private System.Windows.Forms.DataGridViewTextBoxColumn NegAlarm;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReMark;
    }
}