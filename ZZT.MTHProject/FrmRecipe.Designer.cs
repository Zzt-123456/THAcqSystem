namespace ZZT.MTHProject
{
    partial class FrmRecipe
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
            this.panelEnhanced1 = new ZZT.MTHControlLib.PanelEnhanced();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_Apply = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Modify = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.txt_RecipeName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_CurrentRecipe = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_Main = new System.Windows.Forms.DataGridView();
            this.GroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReMark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recipeControl4 = new ZZT.MTHControlLib.RecipeControl();
            this.recipeControl6 = new ZZT.MTHControlLib.RecipeControl();
            this.recipeControl3 = new ZZT.MTHControlLib.RecipeControl();
            this.recipeControl5 = new ZZT.MTHControlLib.RecipeControl();
            this.recipeControl2 = new ZZT.MTHControlLib.RecipeControl();
            this.recipeControl1 = new ZZT.MTHControlLib.RecipeControl();
            this.panelEnhanced1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Main)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEnhanced1
            // 
            this.panelEnhanced1.BackgroundImage = global::ZZT.MTHProject.Properties.Resources.BackGround;
            this.panelEnhanced1.Controls.Add(this.splitContainer1);
            this.panelEnhanced1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEnhanced1.Location = new System.Drawing.Point(0, 0);
            this.panelEnhanced1.Name = "panelEnhanced1";
            this.panelEnhanced1.Size = new System.Drawing.Size(1394, 736);
            this.panelEnhanced1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btn_Apply);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Delete);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Modify);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Add);
            this.splitContainer1.Panel1.Controls.Add(this.txt_RecipeName);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.lbl_CurrentRecipe);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.dgv_Main);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.recipeControl4);
            this.splitContainer1.Panel2.Controls.Add(this.recipeControl6);
            this.splitContainer1.Panel2.Controls.Add(this.recipeControl3);
            this.splitContainer1.Panel2.Controls.Add(this.recipeControl5);
            this.splitContainer1.Panel2.Controls.Add(this.recipeControl2);
            this.splitContainer1.Panel2.Controls.Add(this.recipeControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1394, 736);
            this.splitContainer1.SplitterDistance = 328;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // btn_Apply
            // 
            this.btn_Apply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(27)))), ((int)(((byte)(78)))));
            this.btn_Apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Apply.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btn_Apply.ForeColor = System.Drawing.Color.White;
            this.btn_Apply.Location = new System.Drawing.Point(177, 664);
            this.btn_Apply.Name = "btn_Apply";
            this.btn_Apply.Size = new System.Drawing.Size(98, 42);
            this.btn_Apply.TabIndex = 10;
            this.btn_Apply.Text = "应用配方";
            this.btn_Apply.UseVisualStyleBackColor = false;
            this.btn_Apply.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(27)))), ((int)(((byte)(78)))));
            this.btn_Delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Delete.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btn_Delete.ForeColor = System.Drawing.Color.White;
            this.btn_Delete.Location = new System.Drawing.Point(51, 664);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(98, 42);
            this.btn_Delete.TabIndex = 10;
            this.btn_Delete.Text = "删除配方";
            this.btn_Delete.UseVisualStyleBackColor = false;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Modify
            // 
            this.btn_Modify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(27)))), ((int)(((byte)(78)))));
            this.btn_Modify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Modify.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btn_Modify.ForeColor = System.Drawing.Color.White;
            this.btn_Modify.Location = new System.Drawing.Point(177, 591);
            this.btn_Modify.Name = "btn_Modify";
            this.btn_Modify.Size = new System.Drawing.Size(98, 42);
            this.btn_Modify.TabIndex = 10;
            this.btn_Modify.Text = "修改配方";
            this.btn_Modify.UseVisualStyleBackColor = false;
            this.btn_Modify.Click += new System.EventHandler(this.btn_Modify_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(27)))), ((int)(((byte)(78)))));
            this.btn_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Add.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btn_Add.ForeColor = System.Drawing.Color.White;
            this.btn_Add.Location = new System.Drawing.Point(51, 591);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(98, 42);
            this.btn_Add.TabIndex = 10;
            this.btn_Add.Text = "添加配方";
            this.btn_Add.UseVisualStyleBackColor = false;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // txt_RecipeName
            // 
            this.txt_RecipeName.Font = new System.Drawing.Font("微软雅黑", 12.5F);
            this.txt_RecipeName.Location = new System.Drawing.Point(170, 522);
            this.txt_RecipeName.Name = "txt_RecipeName";
            this.txt_RecipeName.Size = new System.Drawing.Size(100, 29);
            this.txt_RecipeName.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(58, 525);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "配方名称：";
            // 
            // lbl_CurrentRecipe
            // 
            this.lbl_CurrentRecipe.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_CurrentRecipe.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.lbl_CurrentRecipe.ForeColor = System.Drawing.Color.White;
            this.lbl_CurrentRecipe.Location = new System.Drawing.Point(170, 474);
            this.lbl_CurrentRecipe.Name = "lbl_CurrentRecipe";
            this.lbl_CurrentRecipe.Size = new System.Drawing.Size(100, 31);
            this.lbl_CurrentRecipe.TabIndex = 0;
            this.lbl_CurrentRecipe.Text = "Z";
            this.lbl_CurrentRecipe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 11.5F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(58, 479);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前配方：";
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(30)))), ((int)(((byte)(78)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Main.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Main.ColumnHeadersHeight = 35;
            this.dgv_Main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_Main.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GroupName,
            this.ReMark});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(40)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Main.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_Main.EnableHeadersVisualStyles = false;
            this.dgv_Main.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(146)))), ((int)(((byte)(235)))));
            this.dgv_Main.Location = new System.Drawing.Point(43, 25);
            this.dgv_Main.Name = "dgv_Main";
            this.dgv_Main.ReadOnly = true;
            this.dgv_Main.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(40)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(40)))), ((int)(((byte)(86)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Main.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_Main.RowHeadersVisible = false;
            this.dgv_Main.RowHeadersWidth = 55;
            this.dgv_Main.RowTemplate.Height = 40;
            this.dgv_Main.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Main.Size = new System.Drawing.Size(247, 424);
            this.dgv_Main.TabIndex = 8;
            this.dgv_Main.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Main_CellClick);
            // 
            // GroupName
            // 
            this.GroupName.DataPropertyName = "GroupName";
            this.GroupName.HeaderText = "序号";
            this.GroupName.Name = "GroupName";
            this.GroupName.ReadOnly = true;
            this.GroupName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GroupName.Width = 120;
            // 
            // ReMark
            // 
            this.ReMark.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ReMark.DataPropertyName = "Remark";
            this.ReMark.HeaderText = "配方名称";
            this.ReMark.Name = "ReMark";
            this.ReMark.ReadOnly = true;
            this.ReMark.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // recipeControl4
            // 
            this.recipeControl4.BackColor = System.Drawing.Color.Transparent;
            this.recipeControl4.DevName = "4#站点";
            this.recipeControl4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.recipeControl4.Location = new System.Drawing.Point(6, 371);
            this.recipeControl4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.recipeControl4.Name = "recipeControl4";

            this.recipeControl4.Size = new System.Drawing.Size(377, 360);
            this.recipeControl4.TabIndex = 0;
            // 
            // recipeControl6
            // 
            this.recipeControl6.BackColor = System.Drawing.Color.Transparent;
            this.recipeControl6.DevName = "6#站点";
            this.recipeControl6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.recipeControl6.Location = new System.Drawing.Point(693, 371);
            this.recipeControl6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.recipeControl6.Name = "recipeControl6";

            this.recipeControl6.Size = new System.Drawing.Size(377, 358);
            this.recipeControl6.TabIndex = 0;
            // 
            // recipeControl3
            // 
            this.recipeControl3.BackColor = System.Drawing.Color.Transparent;
            this.recipeControl3.DevName = "3#站点";
            this.recipeControl3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.recipeControl3.Location = new System.Drawing.Point(693, 5);
            this.recipeControl3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.recipeControl3.Name = "recipeControl3";

            this.recipeControl3.Size = new System.Drawing.Size(377, 358);
            this.recipeControl3.TabIndex = 0;
            // 
            // recipeControl5
            // 
            this.recipeControl5.BackColor = System.Drawing.Color.Transparent;
            this.recipeControl5.DevName = "5#站点";
            this.recipeControl5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.recipeControl5.Location = new System.Drawing.Point(351, 371);
            this.recipeControl5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.recipeControl5.Name = "recipeControl5";

            this.recipeControl5.Size = new System.Drawing.Size(377, 358);
            this.recipeControl5.TabIndex = 0;
            // 
            // recipeControl2
            // 
            this.recipeControl2.BackColor = System.Drawing.Color.Transparent;
            this.recipeControl2.DevName = "2#站点";
            this.recipeControl2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.recipeControl2.Location = new System.Drawing.Point(351, 5);
            this.recipeControl2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.recipeControl2.Name = "recipeControl2";

            this.recipeControl2.Size = new System.Drawing.Size(377, 358);
            this.recipeControl2.TabIndex = 0;
            // 
            // recipeControl1
            // 
            this.recipeControl1.BackColor = System.Drawing.Color.Transparent;
            this.recipeControl1.DevName = "1#站点";
            this.recipeControl1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.recipeControl1.Location = new System.Drawing.Point(6, 5);
            this.recipeControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.recipeControl1.Name = "recipeControl1";

            this.recipeControl1.Size = new System.Drawing.Size(377, 358);
            this.recipeControl1.TabIndex = 0;
            // 
            // FrmRecipe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1394, 736);
            this.Controls.Add(this.panelEnhanced1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmRecipe";
            this.Text = "配方管理";
            this.panelEnhanced1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Main)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ZZT.MTHControlLib.PanelEnhanced panelEnhanced1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgv_Main;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReMark;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_CurrentRecipe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_RecipeName;
        private System.Windows.Forms.Button btn_Apply;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Modify;
        private System.Windows.Forms.Button btn_Add;
        private ZZT.MTHControlLib.RecipeControl recipeControl4;
        private ZZT.MTHControlLib.RecipeControl recipeControl6;
        private ZZT.MTHControlLib.RecipeControl recipeControl3;
        private ZZT.MTHControlLib.RecipeControl recipeControl5;
        private ZZT.MTHControlLib.RecipeControl recipeControl2;
        private ZZT.MTHControlLib.RecipeControl recipeControl1;
    }
}