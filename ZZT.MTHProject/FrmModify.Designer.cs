namespace ZZT.MTHProject
{
    partial class FrmModify
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
            this.panelEx1 = new ZZT.MTHControlLib.PanelEx();
            this.txt_SetValue = new System.Windows.Forms.TextBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Sure = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_CurrentValue = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.BackColor = System.Drawing.Color.Transparent;
            this.panelEx1.BackgroundImage = global::ZZT.MTHProject.Properties.Resources.TitleBG;
            this.panelEx1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelEx1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(79)))), ((int)(((byte)(150)))));
            this.panelEx1.BorderWidth = 2;
            this.panelEx1.BottomGap = 1;
            this.panelEx1.Controls.Add(this.txt_SetValue);
            this.panelEx1.Controls.Add(this.btn_Cancel);
            this.panelEx1.Controls.Add(this.btn_Sure);
            this.panelEx1.Controls.Add(this.btn_Close);
            this.panelEx1.Controls.Add(this.label4);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.lbl_CurrentValue);
            this.panelEx1.Controls.Add(this.lbl_Title);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.LeftGap = 1;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.RightGap = 1;
            this.panelEx1.Size = new System.Drawing.Size(472, 364);
            this.panelEx1.TabIndex = 0;
            this.panelEx1.TopGap = 1;
            this.panelEx1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.panelEx1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseMove);
            // 
            // txt_SetValue
            // 
            this.txt_SetValue.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.txt_SetValue.Location = new System.Drawing.Point(233, 193);
            this.txt_SetValue.Name = "txt_SetValue";
            this.txt_SetValue.Size = new System.Drawing.Size(200, 29);
            this.txt_SetValue.TabIndex = 8;
            this.txt_SetValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_SetValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_SetValue_KeyDown);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(27)))), ((int)(((byte)(78)))));
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancel.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btn_Cancel.ForeColor = System.Drawing.Color.White;
            this.btn_Cancel.Location = new System.Drawing.Point(307, 275);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(98, 42);
            this.btn_Cancel.TabIndex = 7;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Sure
            // 
            this.btn_Sure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(27)))), ((int)(((byte)(78)))));
            this.btn_Sure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Sure.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btn_Sure.ForeColor = System.Drawing.Color.White;
            this.btn_Sure.Location = new System.Drawing.Point(79, 275);
            this.btn_Sure.Name = "btn_Sure";
            this.btn_Sure.Size = new System.Drawing.Size(98, 42);
            this.btn_Sure.TabIndex = 7;
            this.btn_Sure.Text = "确定";
            this.btn_Sure.UseVisualStyleBackColor = false;
            this.btn_Sure.Click += new System.EventHandler(this.btn_Sure_Click);
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
            this.btn_Close.Location = new System.Drawing.Point(414, 3);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(46, 42);
            this.btn_Close.TabIndex = 5;
            this.btn_Close.Text = "X";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(99, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "修改值：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(99, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "当前值：";
            // 
            // lbl_CurrentValue
            // 
            this.lbl_CurrentValue.BackColor = System.Drawing.Color.Transparent;
            this.lbl_CurrentValue.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.lbl_CurrentValue.ForeColor = System.Drawing.Color.White;
            this.lbl_CurrentValue.Location = new System.Drawing.Point(229, 143);
            this.lbl_CurrentValue.Name = "lbl_CurrentValue";
            this.lbl_CurrentValue.Size = new System.Drawing.Size(204, 33);
            this.lbl_CurrentValue.TabIndex = 3;
            this.lbl_CurrentValue.Text = "0.0";
            this.lbl_CurrentValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Title
            // 
            this.lbl_Title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Title.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.lbl_Title.ForeColor = System.Drawing.Color.White;
            this.lbl_Title.Location = new System.Drawing.Point(229, 90);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(204, 33);
            this.lbl_Title.TabIndex = 3;
            this.lbl_Title.Text = "1#站点温度高限";
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(99, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "参数名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "参数修改";
            // 
            // FrmModify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 364);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmModify";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数修改";
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ZZT.MTHControlLib.PanelEx panelEx1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Sure;
        private System.Windows.Forms.Label lbl_CurrentValue;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.TextBox txt_SetValue;
    }
}