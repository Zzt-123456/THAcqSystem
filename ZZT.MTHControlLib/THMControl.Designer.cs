namespace ZZT.MTHControlLib
{
    partial class THMControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_Title = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Temp = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_Humidity = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dialPlate = new ZZT.MTHControlLib.DialPlate();
            this.SuspendLayout();
            // 
            // lbl_Title
            // 
            this.lbl_Title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(184)))), ((int)(((byte)(196)))));
            this.lbl_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Title.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.lbl_Title.Location = new System.Drawing.Point(5, 6);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(75, 31);
            this.lbl_Title.TabIndex = 2;
            this.lbl_Title.Text = "1#站点";
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label1.Location = new System.Drawing.Point(17, 255);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "温度:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Temp
            // 
            this.lbl_Temp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_Temp.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Temp.Font = new System.Drawing.Font("微软雅黑", 13.75F, System.Drawing.FontStyle.Bold);
            this.lbl_Temp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(184)))), ((int)(((byte)(196)))));
            this.lbl_Temp.Location = new System.Drawing.Point(80, 255);
            this.lbl_Temp.Name = "lbl_Temp";
            this.lbl_Temp.Size = new System.Drawing.Size(75, 31);
            this.lbl_Temp.TabIndex = 2;
            this.lbl_Temp.Text = "0.0";
            this.lbl_Temp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 13.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(184)))), ((int)(((byte)(196)))));
            this.label3.Location = new System.Drawing.Point(151, 257);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "℃";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.label4.Location = new System.Drawing.Point(224, 255);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 31);
            this.label4.TabIndex = 2;
            this.label4.Text = "湿度:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Humidity
            // 
            this.lbl_Humidity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_Humidity.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Humidity.Font = new System.Drawing.Font("微软雅黑", 13.75F, System.Drawing.FontStyle.Bold);
            this.lbl_Humidity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(184)))), ((int)(((byte)(196)))));
            this.lbl_Humidity.Location = new System.Drawing.Point(287, 255);
            this.lbl_Humidity.Name = "lbl_Humidity";
            this.lbl_Humidity.Size = new System.Drawing.Size(75, 31);
            this.lbl_Humidity.TabIndex = 2;
            this.lbl_Humidity.Text = "0.0";
            this.lbl_Humidity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 13.75F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(184)))), ((int)(((byte)(196)))));
            this.label6.Location = new System.Drawing.Point(358, 257);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 26);
            this.label6.TabIndex = 2;
            this.label6.Text = "％";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dialPlate
            // 
            this.dialPlate.AlarmAngle = 120F;
            this.dialPlate.AlarmColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(184)))), ((int)(((byte)(196)))));
            this.dialPlate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dialPlate.BackColor = System.Drawing.Color.Transparent;
            this.dialPlate.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dialPlate.ForeColor = System.Drawing.Color.White;
            this.dialPlate.Humidity = 0F;
            this.dialPlate.HumidityColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(184)))), ((int)(((byte)(196)))));
            this.dialPlate.HumidityScale = 0.4F;
            this.dialPlate.InThickness = 16;
            this.dialPlate.Location = new System.Drawing.Point(21, 30);
            this.dialPlate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dialPlate.Name = "dialPlate";
            this.dialPlate.OutThinckness = 8;
            this.dialPlate.RangeMax = 90F;
            this.dialPlate.RangeMin = 0F;
            this.dialPlate.RingColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(187)))), ((int)(((byte)(187)))));
            this.dialPlate.Size = new System.Drawing.Size(413, 235);
            this.dialPlate.TabIndex = 0;
            this.dialPlate.Temp = 0F;
            this.dialPlate.TempColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(184)))), ((int)(((byte)(196)))));
            this.dialPlate.TempScale = 0.7F;
            this.dialPlate.TextScale = 0.85F;
            // 
            // THMControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(28)))), ((int)(((byte)(68)))));
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_Humidity);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbl_Temp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.dialPlate);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "THMControl";
            this.Size = new System.Drawing.Size(459, 297);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DialPlate dialPlate;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Temp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_Humidity;
        private System.Windows.Forms.Label label6;
    }
}
