namespace ZZT.MTHProject
{
    partial class FrmAlarm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_AlarmType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtp_Start = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtp_End = new System.Windows.Forms.DateTimePicker();
            this.btn_Export = new System.Windows.Forms.Button();
            this.btn_Query = new System.Windows.Forms.Button();
            this.dgv_Main = new System.Windows.Forms.DataGridView();
            this.InsertTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlarmType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VarName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Main)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(41, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "报警类型";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmb_AlarmType
            // 
            this.cmb_AlarmType.FormattingEnabled = true;
            this.cmb_AlarmType.Location = new System.Drawing.Point(145, 63);
            this.cmb_AlarmType.Name = "cmb_AlarmType";
            this.cmb_AlarmType.Size = new System.Drawing.Size(121, 28);
            this.cmb_AlarmType.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(296, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "开始时间";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtp_Start
            // 
            this.dtp_Start.CalendarFont = new System.Drawing.Font("微软雅黑", 11.5F);
            this.dtp_Start.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtp_Start.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_Start.Location = new System.Drawing.Point(400, 64);
            this.dtp_Start.Name = "dtp_Start";
            this.dtp_Start.Size = new System.Drawing.Size(200, 26);
            this.dtp_Start.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(630, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "结束时间";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtp_End
            // 
            this.dtp_End.CalendarFont = new System.Drawing.Font("微软雅黑", 11.5F);
            this.dtp_End.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtp_End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_End.Location = new System.Drawing.Point(734, 64);
            this.dtp_End.Name = "dtp_End";
            this.dtp_End.Size = new System.Drawing.Size(200, 26);
            this.dtp_End.TabIndex = 4;
            // 
            // btn_Export
            // 
            this.btn_Export.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(27)))), ((int)(((byte)(78)))));
            this.btn_Export.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Export.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Export.ForeColor = System.Drawing.Color.White;
            this.btn_Export.Location = new System.Drawing.Point(1176, 62);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(183, 31);
            this.btn_Export.TabIndex = 5;
            this.btn_Export.Text = "导 出 Excel";
            this.btn_Export.UseVisualStyleBackColor = false;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // btn_Query
            // 
            this.btn_Query.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(27)))), ((int)(((byte)(78)))));
            this.btn_Query.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Query.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Query.ForeColor = System.Drawing.Color.White;
            this.btn_Query.Location = new System.Drawing.Point(964, 62);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(182, 31);
            this.btn_Query.TabIndex = 5;
            this.btn_Query.Text = "时 间 查 询";
            this.btn_Query.UseVisualStyleBackColor = false;
            this.btn_Query.Click += new System.EventHandler(this.btn_Query_Click);
            // 
            // dgv_Main
            // 
            this.dgv_Main.AllowUserToAddRows = false;
            this.dgv_Main.AllowUserToDeleteRows = false;
            this.dgv_Main.AllowUserToResizeColumns = false;
            this.dgv_Main.AllowUserToResizeRows = false;
            this.dgv_Main.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(30)))), ((int)(((byte)(78)))));
            this.dgv_Main.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_Main.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(30)))), ((int)(((byte)(78)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Main.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_Main.ColumnHeadersHeight = 45;
            this.dgv_Main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_Main.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InsertTime,
            this.AlarmType,
            this.Note,
            this.Operator,
            this.VarName});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(40)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Main.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_Main.EnableHeadersVisualStyles = false;
            this.dgv_Main.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(146)))), ((int)(((byte)(235)))));
            this.dgv_Main.Location = new System.Drawing.Point(45, 135);
            this.dgv_Main.Name = "dgv_Main";
            this.dgv_Main.ReadOnly = true;
            this.dgv_Main.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(40)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(40)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Main.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_Main.RowHeadersWidth = 55;
            this.dgv_Main.RowTemplate.Height = 40;
            this.dgv_Main.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Main.Size = new System.Drawing.Size(1314, 556);
            this.dgv_Main.TabIndex = 9;
            this.dgv_Main.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgv_Main_RowPostPaint);
            // 
            // InsertTime
            // 
            this.InsertTime.DataPropertyName = "InsertTime";
            this.InsertTime.HeaderText = "日期时间";
            this.InsertTime.Name = "InsertTime";
            this.InsertTime.ReadOnly = true;
            this.InsertTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InsertTime.Width = 200;
            // 
            // AlarmType
            // 
            this.AlarmType.DataPropertyName = "AlarmType";
            this.AlarmType.HeaderText = "报警类型";
            this.AlarmType.Name = "AlarmType";
            this.AlarmType.ReadOnly = true;
            this.AlarmType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AlarmType.Width = 150;
            // 
            // Note
            // 
            this.Note.DataPropertyName = "Note";
            this.Note.HeaderText = "日志报警信息";
            this.Note.Name = "Note";
            this.Note.ReadOnly = true;
            this.Note.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Note.Width = 450;
            // 
            // Operator
            // 
            this.Operator.DataPropertyName = "Operator";
            this.Operator.HeaderText = "操作员";
            this.Operator.Name = "Operator";
            this.Operator.ReadOnly = true;
            this.Operator.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Operator.Width = 200;
            // 
            // VarName
            // 
            this.VarName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.VarName.DataPropertyName = "VarName";
            this.VarName.HeaderText = "变量名称";
            this.VarName.Name = "VarName";
            this.VarName.ReadOnly = true;
            this.VarName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FrmAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ZZT.MTHProject.Properties.Resources.BackGround;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1394, 736);
            this.Controls.Add(this.dgv_Main);
            this.Controls.Add(this.btn_Query);
            this.Controls.Add(this.btn_Export);
            this.Controls.Add(this.dtp_End);
            this.Controls.Add(this.dtp_Start);
            this.Controls.Add(this.cmb_AlarmType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmAlarm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "报警追溯";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Main)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_AlarmType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtp_Start;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtp_End;
        private System.Windows.Forms.Button btn_Export;
        private System.Windows.Forms.Button btn_Query;
        private System.Windows.Forms.DataGridView dgv_Main;
        private System.Windows.Forms.DataGridViewTextBoxColumn InsertTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlarmType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Note;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operator;
        private System.Windows.Forms.DataGridViewTextBoxColumn VarName;
    }
}