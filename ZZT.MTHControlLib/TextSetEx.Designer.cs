namespace ZZT.MTHControlLib
{
    partial class TextSetEx
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Unit = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.num_Value = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_Value)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.85423F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.90379F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.95044F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_Unit, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_Title, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.num_Value, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(343, 49);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbl_Unit
            // 
            this.lbl_Unit.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Unit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Unit.ForeColor = System.Drawing.Color.White;
            this.lbl_Unit.Location = new System.Drawing.Point(280, 0);
            this.lbl_Unit.Name = "lbl_Unit";
            this.lbl_Unit.Size = new System.Drawing.Size(60, 49);
            this.lbl_Unit.TabIndex = 2;
            this.lbl_Unit.Text = "℃";
            this.lbl_Unit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Title
            // 
            this.lbl_Title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Title.ForeColor = System.Drawing.Color.White;
            this.lbl_Title.Location = new System.Drawing.Point(3, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(165, 49);
            this.lbl_Title.TabIndex = 0;
            this.lbl_Title.Text = "1#站点温度高限";
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // num_Value
            // 
            this.num_Value.DecimalPlaces = 1;
            this.num_Value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.num_Value.Location = new System.Drawing.Point(174, 10);
            this.num_Value.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.num_Value.Name = "num_Value";
            this.num_Value.Size = new System.Drawing.Size(100, 28);
            this.num_Value.TabIndex = 3;
            // 
            // TextSetEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(39)))), ((int)(((byte)(104)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "TextSetEx";
            this.Size = new System.Drawing.Size(343, 49);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.num_Value)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Label lbl_Unit;
        private System.Windows.Forms.NumericUpDown num_Value;
    }
}
