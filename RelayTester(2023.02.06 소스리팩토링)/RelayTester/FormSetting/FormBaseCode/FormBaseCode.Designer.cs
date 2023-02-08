namespace RelayTester
{
    partial class FormBaseCode
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grbMenu = new System.Windows.Forms.GroupBox();
            this.btnDelDtl = new System.Windows.Forms.Button();
            this.btnAddDtl = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.grbContent = new System.Windows.Forms.GroupBox();
            this.dgvCommonDtl = new System.Windows.Forms.DataGridView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.dgvCommonMst = new System.Windows.Forms.DataGridView();
            this.grbQuery = new System.Windows.Forms.GroupBox();
            this.lblCodeName = new System.Windows.Forms.Label();
            this.txtCodeName = new System.Windows.Forms.TextBox();
            this.grbMenu.SuspendLayout();
            this.grbContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCommonDtl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCommonMst)).BeginInit();
            this.grbQuery.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbMenu
            // 
            this.grbMenu.Controls.Add(this.btnDelDtl);
            this.grbMenu.Controls.Add(this.btnAddDtl);
            this.grbMenu.Controls.Add(this.btnSave);
            this.grbMenu.Controls.Add(this.btnDelete);
            this.grbMenu.Controls.Add(this.btnAdd);
            this.grbMenu.Controls.Add(this.btnQuery);
            this.grbMenu.Controls.Add(this.btnNew);
            this.grbMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbMenu.Location = new System.Drawing.Point(0, 0);
            this.grbMenu.Name = "grbMenu";
            this.grbMenu.Size = new System.Drawing.Size(1152, 76);
            this.grbMenu.TabIndex = 0;
            this.grbMenu.TabStop = false;
            // 
            // btnDelDtl
            // 
            this.btnDelDtl.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDelDtl.Image = global::RelayTester.Properties.Resources.page_white_delete;
            this.btnDelDtl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelDtl.Location = new System.Drawing.Point(551, 21);
            this.btnDelDtl.Name = "btnDelDtl";
            this.btnDelDtl.Size = new System.Drawing.Size(154, 42);
            this.btnDelDtl.TabIndex = 6;
            this.btnDelDtl.Text = "디테일삭제";
            this.btnDelDtl.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnDelDtl.UseVisualStyleBackColor = true;
            this.btnDelDtl.Click += new System.EventHandler(this.btnDelDtl_Click);
            // 
            // btnAddDtl
            // 
            this.btnAddDtl.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAddDtl.Image = global::RelayTester.Properties.Resources.page_white_add;
            this.btnAddDtl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddDtl.Location = new System.Drawing.Point(391, 20);
            this.btnAddDtl.Name = "btnAddDtl";
            this.btnAddDtl.Size = new System.Drawing.Size(154, 42);
            this.btnAddDtl.TabIndex = 5;
            this.btnAddDtl.Text = "디테일추가";
            this.btnAddDtl.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnAddDtl.UseVisualStyleBackColor = true;
            this.btnAddDtl.Click += new System.EventHandler(this.btnAddDtl_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSave.Image = global::RelayTester.Properties.Resources.page_white_database;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(711, 21);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 42);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "저장";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDelete.Image = global::RelayTester.Properties.Resources.page_white_delete;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(295, 21);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 42);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "삭제";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAdd.Image = global::RelayTester.Properties.Resources.page_white_add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(199, 21);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 42);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "추가";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnQuery.Image = global::RelayTester.Properties.Resources.page_white_magnify;
            this.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuery.Location = new System.Drawing.Point(103, 21);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(90, 42);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "조회";
            this.btnQuery.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnNew
            // 
            this.btnNew.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnNew.Image = global::RelayTester.Properties.Resources.page_white;
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.Location = new System.Drawing.Point(7, 21);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(90, 42);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "신규";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // grbContent
            // 
            this.grbContent.Controls.Add(this.dgvCommonDtl);
            this.grbContent.Controls.Add(this.splitter1);
            this.grbContent.Controls.Add(this.dgvCommonMst);
            this.grbContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbContent.Location = new System.Drawing.Point(0, 140);
            this.grbContent.Name = "grbContent";
            this.grbContent.Size = new System.Drawing.Size(1152, 655);
            this.grbContent.TabIndex = 1;
            this.grbContent.TabStop = false;
            // 
            // dgvCommonDtl
            // 
            this.dgvCommonDtl.AllowUserToAddRows = false;
            this.dgvCommonDtl.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCommonDtl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCommonDtl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCommonDtl.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCommonDtl.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvCommonDtl.Location = new System.Drawing.Point(327, 17);
            this.dgvCommonDtl.Name = "dgvCommonDtl";
            this.dgvCommonDtl.RowTemplate.Height = 23;
            this.dgvCommonDtl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCommonDtl.Size = new System.Drawing.Size(814, 635);
            this.dgvCommonDtl.TabIndex = 1;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(317, 17);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(10, 635);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // dgvCommonMst
            // 
            this.dgvCommonMst.AllowUserToAddRows = false;
            this.dgvCommonMst.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCommonMst.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCommonMst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCommonMst.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvCommonMst.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvCommonMst.Location = new System.Drawing.Point(3, 17);
            this.dgvCommonMst.Name = "dgvCommonMst";
            this.dgvCommonMst.RowTemplate.Height = 23;
            this.dgvCommonMst.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCommonMst.Size = new System.Drawing.Size(314, 635);
            this.dgvCommonMst.TabIndex = 0;
            this.dgvCommonMst.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCommonMst_CellClick);
            // 
            // grbQuery
            // 
            this.grbQuery.Controls.Add(this.lblCodeName);
            this.grbQuery.Controls.Add(this.txtCodeName);
            this.grbQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbQuery.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grbQuery.Location = new System.Drawing.Point(0, 76);
            this.grbQuery.Name = "grbQuery";
            this.grbQuery.Size = new System.Drawing.Size(1152, 64);
            this.grbQuery.TabIndex = 2;
            this.grbQuery.TabStop = false;
            this.grbQuery.Text = " 조회조건";
            // 
            // lblCodeName
            // 
            this.lblCodeName.AutoSize = true;
            this.lblCodeName.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCodeName.Location = new System.Drawing.Point(12, 19);
            this.lblCodeName.Name = "lblCodeName";
            this.lblCodeName.Size = new System.Drawing.Size(76, 30);
            this.lblCodeName.TabIndex = 1;
            this.lblCodeName.Text = "코드명";
            // 
            // txtCodeName
            // 
            this.txtCodeName.Font = new System.Drawing.Font("맑은 고딕", 15.75F);
            this.txtCodeName.Location = new System.Drawing.Point(94, 16);
            this.txtCodeName.Name = "txtCodeName";
            this.txtCodeName.Size = new System.Drawing.Size(169, 35);
            this.txtCodeName.TabIndex = 0;
            this.txtCodeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodeName_KeyDown);
            // 
            // FormBaseCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 795);
            this.Controls.Add(this.grbContent);
            this.Controls.Add(this.grbQuery);
            this.Controls.Add(this.grbMenu);
            this.Name = "FormBaseCode";
            this.Text = "기초코드관리";
            this.Load += new System.EventHandler(this.FormCommonCode_Load);
            this.grbMenu.ResumeLayout(false);
            this.grbContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCommonDtl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCommonMst)).EndInit();
            this.grbQuery.ResumeLayout(false);
            this.grbQuery.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbMenu;
        public System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.GroupBox grbContent;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.GroupBox grbQuery;
        private System.Windows.Forms.Label lblCodeName;
        public System.Windows.Forms.TextBox txtCodeName;
        public System.Windows.Forms.DataGridView dgvCommonMst;
        public System.Windows.Forms.DataGridView dgvCommonDtl;
        public System.Windows.Forms.Button btnAddDtl;
        public System.Windows.Forms.Button btnDelDtl;
        private System.Windows.Forms.Splitter splitter1;
    }
}