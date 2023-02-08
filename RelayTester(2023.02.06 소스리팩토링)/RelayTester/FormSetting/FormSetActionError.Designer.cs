namespace RelayTester
{
    partial class FormSetActionError
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblEquipType = new System.Windows.Forms.Label();
            this.cmb_ErrorCode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtActCode = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtActType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv_ret = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ret)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblEquipType
            // 
            this.lblEquipType.AutoSize = true;
            this.lblEquipType.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblEquipType.Location = new System.Drawing.Point(12, 30);
            this.lblEquipType.Name = "lblEquipType";
            this.lblEquipType.Size = new System.Drawing.Size(97, 30);
            this.lblEquipType.TabIndex = 4;
            this.lblEquipType.Text = "에러유형";
            // 
            // cmb_ErrorCode
            // 
            this.cmb_ErrorCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ErrorCode.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmb_ErrorCode.FormattingEnabled = true;
            this.cmb_ErrorCode.Items.AddRange(new object[] {
            "전체"});
            this.cmb_ErrorCode.Location = new System.Drawing.Point(135, 27);
            this.cmb_ErrorCode.Name = "cmb_ErrorCode";
            this.cmb_ErrorCode.Size = new System.Drawing.Size(156, 38);
            this.cmb_ErrorCode.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(314, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 30);
            this.label1.TabIndex = 15;
            this.label1.Text = "조치코드";
            // 
            // txtActCode
            // 
            this.txtActCode.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtActCode.Location = new System.Drawing.Point(437, 30);
            this.txtActCode.Name = "txtActCode";
            this.txtActCode.Size = new System.Drawing.Size(156, 35);
            this.txtActCode.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtActType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblEquipType);
            this.groupBox1.Controls.Add(this.txtActCode);
            this.groupBox1.Controls.Add(this.cmb_ErrorCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(-3, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1026, 93);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "조치코드 추가";
            // 
            // txtActType
            // 
            this.txtActType.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtActType.Location = new System.Drawing.Point(739, 30);
            this.txtActType.Name = "txtActType";
            this.txtActType.Size = new System.Drawing.Size(156, 35);
            this.txtActType.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(616, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 30);
            this.label2.TabIndex = 18;
            this.label2.Text = "조치내용";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv_ret);
            this.groupBox2.Location = new System.Drawing.Point(-3, 197);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1026, 786);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "조치코드 등록정보";
            // 
            // dgv_ret
            // 
            this.dgv_ret.AllowUserToAddRows = false;
            this.dgv_ret.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_ret.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_ret.ColumnHeadersHeight = 61;
            this.dgv_ret.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_ret.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_ret.EnableHeadersVisualStyles = false;
            this.dgv_ret.Location = new System.Drawing.Point(17, 20);
            this.dgv_ret.MultiSelect = false;
            this.dgv_ret.Name = "dgv_ret";
            this.dgv_ret.RowHeadersVisible = false;
            this.dgv_ret.RowHeadersWidth = 51;
            this.dgv_ret.RowTemplate.Height = 23;
            this.dgv_ret.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_ret.Size = new System.Drawing.Size(878, 759);
            this.dgv_ret.TabIndex = 0;
            this.dgv_ret.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_ret_CellFormatting);
            this.dgv_ret.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_ret_CellPainting);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "에러유형";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "조치코드";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "조치내용";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnNew);
            this.groupBox3.Controls.Add(this.btnDelete);
            this.groupBox3.Controls.Add(this.btnQuery);
            this.groupBox3.Location = new System.Drawing.Point(-3, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1026, 80);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            // 
            // btnNew
            // 
            this.btnNew.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnNew.Image = global::RelayTester.Properties.Resources.page_white_add;
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.Location = new System.Drawing.Point(135, 21);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(102, 42);
            this.btnNew.TabIndex = 25;
            this.btnNew.Text = "추가";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDelete.Image = global::RelayTester.Properties.Resources.page_white_delete;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(243, 20);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(102, 42);
            this.btnDelete.TabIndex = 24;
            this.btnDelete.Text = "삭제";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnQuery.Image = global::RelayTester.Properties.Resources.page_white_magnify;
            this.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuery.Location = new System.Drawing.Point(17, 20);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(102, 42);
            this.btnQuery.TabIndex = 23;
            this.btnQuery.Text = "조회";
            this.btnQuery.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // FormSetActionError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 993);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormSetActionError";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "조치코드 등록";
            this.Load += new System.EventHandler(this.FormActionError_Load);
            this.Shown += new System.EventHandler(this.FormActionError_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ret)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblEquipType;
        private System.Windows.Forms.ComboBox cmb_ErrorCode;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtActCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.DataGridView dgv_ret;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.Button btnNew;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnQuery;
        public System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        public System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        public System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        public System.Windows.Forms.TextBox txtActType;
        private System.Windows.Forms.Label label2;
    }
}