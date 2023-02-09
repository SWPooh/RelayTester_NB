namespace RelayTester
{
    partial class FormSetSchedule
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetSchedule));
            this.grbMenu = new System.Windows.Forms.GroupBox();
            this.btnDelDtl = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAddDtl = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.grbScheduleDetail = new System.Windows.Forms.GroupBox();
            this.dgvSchedDtl = new System.Windows.Forms.DataGridView();
            this.grbQuery = new System.Windows.Forms.GroupBox();
            this.cmbSchedTypeSearch = new System.Windows.Forms.ComboBox();
            this.lblEquipTypeSearch = new System.Windows.Forms.Label();
            this.txtSchedNmSearch = new System.Windows.Forms.TextBox();
            this.lblScheduleNmSearch = new System.Windows.Forms.Label();
            this.mtxtSchedSeqSearch = new System.Windows.Forms.MaskedTextBox();
            this.lblSchedSeq = new System.Windows.Forms.Label();
            this.grbSchedMst = new System.Windows.Forms.GroupBox();
            this.btnDefault = new System.Windows.Forms.Button();
            this.chkReportChk = new System.Windows.Forms.CheckBox();
            this.btnExcelUpload = new System.Windows.Forms.Button();
            this.lblScheduleDate = new System.Windows.Forms.Label();
            this.mtxtSchedDate = new System.Windows.Forms.MaskedTextBox();
            this.mtxtSchedSeq = new System.Windows.Forms.MaskedTextBox();
            this.txtSchedRemark = new System.Windows.Forms.TextBox();
            this.lblScheduleRemark = new System.Windows.Forms.Label();
            this.lblScheduleSeq = new System.Windows.Forms.Label();
            this.txtSchedNm = new System.Windows.Forms.TextBox();
            this.lblScheduleNm = new System.Windows.Forms.Label();
            this.cmbSchedType = new System.Windows.Forms.ComboBox();
            this.lblEquipType = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.grbMenu.SuspendLayout();
            this.grbScheduleDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedDtl)).BeginInit();
            this.grbQuery.SuspendLayout();
            this.grbSchedMst.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbMenu
            // 
            this.grbMenu.Controls.Add(this.btnDelDtl);
            this.grbMenu.Controls.Add(this.btnSave);
            this.grbMenu.Controls.Add(this.btnAddDtl);
            this.grbMenu.Controls.Add(this.btnDelete);
            this.grbMenu.Controls.Add(this.btnQuery);
            this.grbMenu.Controls.Add(this.btnNew);
            this.grbMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbMenu.Location = new System.Drawing.Point(0, 0);
            this.grbMenu.Name = "grbMenu";
            this.grbMenu.Size = new System.Drawing.Size(1304, 76);
            this.grbMenu.TabIndex = 0;
            this.grbMenu.TabStop = false;
            // 
            // btnDelDtl
            // 
            this.btnDelDtl.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDelDtl.Image = global::RelayTester.Properties.Resources.page_white_delete;
            this.btnDelDtl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelDtl.Location = new System.Drawing.Point(506, 21);
            this.btnDelDtl.Name = "btnDelDtl";
            this.btnDelDtl.Size = new System.Drawing.Size(169, 42);
            this.btnDelDtl.TabIndex = 8;
            this.btnDelDtl.Text = "디테일삭제";
            this.btnDelDtl.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnDelDtl.UseVisualStyleBackColor = true;
            this.btnDelDtl.Click += new System.EventHandler(this.btnDelDtl_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSave.Image = global::RelayTester.Properties.Resources.page_white_database;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(681, 21);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 42);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "저장";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAddDtl
            // 
            this.btnAddDtl.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAddDtl.Image = global::RelayTester.Properties.Resources.page_white_add;
            this.btnAddDtl.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddDtl.Location = new System.Drawing.Point(331, 20);
            this.btnAddDtl.Name = "btnAddDtl";
            this.btnAddDtl.Size = new System.Drawing.Size(169, 42);
            this.btnAddDtl.TabIndex = 7;
            this.btnAddDtl.Text = "디테일추가";
            this.btnAddDtl.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnAddDtl.UseVisualStyleBackColor = true;
            this.btnAddDtl.Click += new System.EventHandler(this.btnAddDtl_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDelete.Image = global::RelayTester.Properties.Resources.page_white_delete;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(223, 21);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(102, 42);
            this.btnDelete.TabIndex = 3;
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
            this.btnQuery.Location = new System.Drawing.Point(115, 21);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(102, 42);
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
            this.btnNew.Size = new System.Drawing.Size(102, 42);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "신규";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // grbScheduleDetail
            // 
            this.grbScheduleDetail.Controls.Add(this.dgvSchedDtl);
            this.grbScheduleDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbScheduleDetail.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grbScheduleDetail.Location = new System.Drawing.Point(0, 335);
            this.grbScheduleDetail.Name = "grbScheduleDetail";
            this.grbScheduleDetail.Size = new System.Drawing.Size(1304, 414);
            this.grbScheduleDetail.TabIndex = 1;
            this.grbScheduleDetail.TabStop = false;
            this.grbScheduleDetail.Text = "스케줄 등록 디테일";
            // 
            // dgvSchedDtl
            // 
            this.dgvSchedDtl.AllowUserToAddRows = false;
            this.dgvSchedDtl.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSchedDtl.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSchedDtl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSchedDtl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchedDtl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSchedDtl.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSchedDtl.Location = new System.Drawing.Point(3, 25);
            this.dgvSchedDtl.Name = "dgvSchedDtl";
            this.dgvSchedDtl.RowTemplate.Height = 23;
            this.dgvSchedDtl.Size = new System.Drawing.Size(1298, 386);
            this.dgvSchedDtl.TabIndex = 0;
            this.dgvSchedDtl.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSchedDtl_CellClick);
            // 
            // grbQuery
            // 
            this.grbQuery.Controls.Add(this.cmbSchedTypeSearch);
            this.grbQuery.Controls.Add(this.lblEquipTypeSearch);
            this.grbQuery.Controls.Add(this.txtSchedNmSearch);
            this.grbQuery.Controls.Add(this.lblScheduleNmSearch);
            this.grbQuery.Controls.Add(this.mtxtSchedSeqSearch);
            this.grbQuery.Controls.Add(this.lblSchedSeq);
            this.grbQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbQuery.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grbQuery.Location = new System.Drawing.Point(0, 76);
            this.grbQuery.Name = "grbQuery";
            this.grbQuery.Size = new System.Drawing.Size(1304, 64);
            this.grbQuery.TabIndex = 2;
            this.grbQuery.TabStop = false;
            this.grbQuery.Text = " 조회조건";
            // 
            // cmbSchedTypeSearch
            // 
            this.cmbSchedTypeSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSchedTypeSearch.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmbSchedTypeSearch.FormattingEnabled = true;
            this.cmbSchedTypeSearch.Location = new System.Drawing.Point(554, 21);
            this.cmbSchedTypeSearch.Name = "cmbSchedTypeSearch";
            this.cmbSchedTypeSearch.Size = new System.Drawing.Size(156, 38);
            this.cmbSchedTypeSearch.TabIndex = 13;
            // 
            // lblEquipTypeSearch
            // 
            this.lblEquipTypeSearch.AutoSize = true;
            this.lblEquipTypeSearch.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblEquipTypeSearch.Location = new System.Drawing.Point(430, 25);
            this.lblEquipTypeSearch.Name = "lblEquipTypeSearch";
            this.lblEquipTypeSearch.Size = new System.Drawing.Size(118, 30);
            this.lblEquipTypeSearch.TabIndex = 12;
            this.lblEquipTypeSearch.Text = "계전기종류";
            // 
            // txtSchedNmSearch
            // 
            this.txtSchedNmSearch.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtSchedNmSearch.Location = new System.Drawing.Point(819, 23);
            this.txtSchedNmSearch.Name = "txtSchedNmSearch";
            this.txtSchedNmSearch.Size = new System.Drawing.Size(340, 35);
            this.txtSchedNmSearch.TabIndex = 13;
            // 
            // lblScheduleNmSearch
            // 
            this.lblScheduleNmSearch.AutoSize = true;
            this.lblScheduleNmSearch.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblScheduleNmSearch.Location = new System.Drawing.Point(716, 25);
            this.lblScheduleNmSearch.Name = "lblScheduleNmSearch";
            this.lblScheduleNmSearch.Size = new System.Drawing.Size(97, 30);
            this.lblScheduleNmSearch.TabIndex = 12;
            this.lblScheduleNmSearch.Text = "스케줄명";
            // 
            // mtxtSchedSeqSearch
            // 
            this.mtxtSchedSeqSearch.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.mtxtSchedSeqSearch.Location = new System.Drawing.Point(207, 23);
            this.mtxtSchedSeqSearch.Mask = "0000-00-00-000";
            this.mtxtSchedSeqSearch.Name = "mtxtSchedSeqSearch";
            this.mtxtSchedSeqSearch.Size = new System.Drawing.Size(180, 35);
            this.mtxtSchedSeqSearch.TabIndex = 12;
            this.mtxtSchedSeqSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtxtSchedSeqSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtSchedSeqSearch_KeyDown);
            // 
            // lblSchedSeq
            // 
            this.lblSchedSeq.AutoSize = true;
            this.lblSchedSeq.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSchedSeq.Location = new System.Drawing.Point(6, 25);
            this.lblSchedSeq.Name = "lblSchedSeq";
            this.lblSchedSeq.Size = new System.Drawing.Size(195, 30);
            this.lblSchedSeq.TabIndex = 1;
            this.lblSchedSeq.Text = "스케줄 마스터 번호";
            // 
            // grbSchedMst
            // 
            this.grbSchedMst.Controls.Add(this.btnDefault);
            this.grbSchedMst.Controls.Add(this.chkReportChk);
            this.grbSchedMst.Controls.Add(this.btnExcelUpload);
            this.grbSchedMst.Controls.Add(this.lblScheduleDate);
            this.grbSchedMst.Controls.Add(this.mtxtSchedDate);
            this.grbSchedMst.Controls.Add(this.mtxtSchedSeq);
            this.grbSchedMst.Controls.Add(this.txtSchedRemark);
            this.grbSchedMst.Controls.Add(this.lblScheduleRemark);
            this.grbSchedMst.Controls.Add(this.lblScheduleSeq);
            this.grbSchedMst.Controls.Add(this.txtSchedNm);
            this.grbSchedMst.Controls.Add(this.lblScheduleNm);
            this.grbSchedMst.Controls.Add(this.cmbSchedType);
            this.grbSchedMst.Controls.Add(this.lblEquipType);
            this.grbSchedMst.Controls.Add(this.label1);
            this.grbSchedMst.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbSchedMst.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grbSchedMst.Location = new System.Drawing.Point(0, 140);
            this.grbSchedMst.Name = "grbSchedMst";
            this.grbSchedMst.Size = new System.Drawing.Size(1304, 195);
            this.grbSchedMst.TabIndex = 3;
            this.grbSchedMst.TabStop = false;
            this.grbSchedMst.Text = "스케줄 등록 마스터";
            // 
            // btnDefault
            // 
            this.btnDefault.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDefault.Location = new System.Drawing.Point(968, 144);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(191, 42);
            this.btnDefault.TabIndex = 14;
            this.btnDefault.Text = "추가 항목 기본값 입력";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // chkReportChk
            // 
            this.chkReportChk.AutoSize = true;
            this.chkReportChk.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chkReportChk.Location = new System.Drawing.Point(819, 22);
            this.chkReportChk.Name = "chkReportChk";
            this.chkReportChk.Size = new System.Drawing.Size(116, 34);
            this.chkReportChk.TabIndex = 13;
            this.chkReportChk.Text = "성적서용";
            this.chkReportChk.UseVisualStyleBackColor = true;
            // 
            // btnExcelUpload
            // 
            this.btnExcelUpload.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnExcelUpload.Image = ((System.Drawing.Image)(resources.GetObject("btnExcelUpload.Image")));
            this.btnExcelUpload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcelUpload.Location = new System.Drawing.Point(37, 144);
            this.btnExcelUpload.Name = "btnExcelUpload";
            this.btnExcelUpload.Size = new System.Drawing.Size(164, 42);
            this.btnExcelUpload.TabIndex = 9;
            this.btnExcelUpload.Text = "엑셀업로드";
            this.btnExcelUpload.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnExcelUpload.UseVisualStyleBackColor = true;
            this.btnExcelUpload.Click += new System.EventHandler(this.btnExcelUpload_Click);
            // 
            // lblScheduleDate
            // 
            this.lblScheduleDate.AutoSize = true;
            this.lblScheduleDate.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblScheduleDate.Location = new System.Drawing.Point(38, 65);
            this.lblScheduleDate.Name = "lblScheduleDate";
            this.lblScheduleDate.Size = new System.Drawing.Size(167, 30);
            this.lblScheduleDate.TabIndex = 11;
            this.lblScheduleDate.Text = "스케줄 작성일자";
            // 
            // mtxtSchedDate
            // 
            this.mtxtSchedDate.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.mtxtSchedDate.Location = new System.Drawing.Point(207, 63);
            this.mtxtSchedDate.Mask = "0000-00-00";
            this.mtxtSchedDate.Name = "mtxtSchedDate";
            this.mtxtSchedDate.Size = new System.Drawing.Size(180, 35);
            this.mtxtSchedDate.TabIndex = 10;
            this.mtxtSchedDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mtxtSchedSeq
            // 
            this.mtxtSchedSeq.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.mtxtSchedSeq.Enabled = false;
            this.mtxtSchedSeq.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.mtxtSchedSeq.Location = new System.Drawing.Point(207, 22);
            this.mtxtSchedSeq.Mask = "0000-00-00-000";
            this.mtxtSchedSeq.Name = "mtxtSchedSeq";
            this.mtxtSchedSeq.Size = new System.Drawing.Size(180, 35);
            this.mtxtSchedSeq.TabIndex = 9;
            this.mtxtSchedSeq.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtSchedRemark
            // 
            this.txtSchedRemark.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtSchedRemark.Location = new System.Drawing.Point(207, 104);
            this.txtSchedRemark.Multiline = true;
            this.txtSchedRemark.Name = "txtSchedRemark";
            this.txtSchedRemark.Size = new System.Drawing.Size(754, 82);
            this.txtSchedRemark.TabIndex = 8;
            // 
            // lblScheduleRemark
            // 
            this.lblScheduleRemark.AutoSize = true;
            this.lblScheduleRemark.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblScheduleRemark.Location = new System.Drawing.Point(146, 102);
            this.lblScheduleRemark.Name = "lblScheduleRemark";
            this.lblScheduleRemark.Size = new System.Drawing.Size(55, 30);
            this.lblScheduleRemark.TabIndex = 7;
            this.lblScheduleRemark.Text = "비고";
            // 
            // lblScheduleSeq
            // 
            this.lblScheduleSeq.AutoSize = true;
            this.lblScheduleSeq.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblScheduleSeq.Location = new System.Drawing.Point(6, 24);
            this.lblScheduleSeq.Name = "lblScheduleSeq";
            this.lblScheduleSeq.Size = new System.Drawing.Size(195, 30);
            this.lblScheduleSeq.TabIndex = 5;
            this.lblScheduleSeq.Text = "스케줄 마스터 번호";
            // 
            // txtSchedNm
            // 
            this.txtSchedNm.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtSchedNm.Location = new System.Drawing.Point(556, 63);
            this.txtSchedNm.Name = "txtSchedNm";
            this.txtSchedNm.Size = new System.Drawing.Size(404, 35);
            this.txtSchedNm.TabIndex = 4;
            // 
            // lblScheduleNm
            // 
            this.lblScheduleNm.AutoSize = true;
            this.lblScheduleNm.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblScheduleNm.Location = new System.Drawing.Point(453, 65);
            this.lblScheduleNm.Name = "lblScheduleNm";
            this.lblScheduleNm.Size = new System.Drawing.Size(97, 30);
            this.lblScheduleNm.TabIndex = 3;
            this.lblScheduleNm.Text = "스케줄명";
            // 
            // cmbSchedType
            // 
            this.cmbSchedType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSchedType.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmbSchedType.FormattingEnabled = true;
            this.cmbSchedType.Location = new System.Drawing.Point(556, 20);
            this.cmbSchedType.Name = "cmbSchedType";
            this.cmbSchedType.Size = new System.Drawing.Size(156, 38);
            this.cmbSchedType.TabIndex = 2;
            // 
            // lblEquipType
            // 
            this.lblEquipType.AutoSize = true;
            this.lblEquipType.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblEquipType.Location = new System.Drawing.Point(432, 24);
            this.lblEquipType.Name = "lblEquipType";
            this.lblEquipType.Size = new System.Drawing.Size(118, 30);
            this.lblEquipType.TabIndex = 1;
            this.lblEquipType.Text = "계전기종류";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(967, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 127);
            this.label1.TabIndex = 15;
            this.label1.Text = "- 동작전류 \r\n추가1 : 동작개시전압(기본값 : 10)\r\n추가2 : 낙하개시전압(기본값 : 20)\r\n- 코일저항\r\n추가1 : 전압(기본값 : 2" +
    "4)\r\n- 동작시간, 복구시간\r\n추가1 : 전압(기본값 : 24)";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // FormSetSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1304, 749);
            this.Controls.Add(this.grbScheduleDetail);
            this.Controls.Add(this.grbSchedMst);
            this.Controls.Add(this.grbQuery);
            this.Controls.Add(this.grbMenu);
            this.Name = "FormSetSchedule";
            this.Text = "스케줄등록";
            this.Load += new System.EventHandler(this.FormSchedule_Load);
            this.grbMenu.ResumeLayout(false);
            this.grbScheduleDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedDtl)).EndInit();
            this.grbQuery.ResumeLayout(false);
            this.grbQuery.PerformLayout();
            this.grbSchedMst.ResumeLayout(false);
            this.grbSchedMst.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbMenu;
        public System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.GroupBox grbScheduleDetail;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.GroupBox grbQuery;
        private System.Windows.Forms.Label lblSchedSeq;
        public System.Windows.Forms.DataGridView dgvSchedDtl;
        private System.Windows.Forms.GroupBox grbSchedMst;
        private System.Windows.Forms.Label lblEquipType;
        private System.Windows.Forms.ComboBox cmbSchedType;
        private System.Windows.Forms.Label lblScheduleNm;
        public System.Windows.Forms.TextBox txtSchedNm;
        public System.Windows.Forms.TextBox txtSchedRemark;
        private System.Windows.Forms.Label lblScheduleRemark;
        private System.Windows.Forms.Label lblScheduleSeq;
        private System.Windows.Forms.MaskedTextBox mtxtSchedSeq;
        private System.Windows.Forms.Label lblScheduleDate;
        private System.Windows.Forms.MaskedTextBox mtxtSchedDate;
        public System.Windows.Forms.Button btnDelDtl;
        public System.Windows.Forms.Button btnAddDtl;
        private System.Windows.Forms.MaskedTextBox mtxtSchedSeqSearch;
        public System.Windows.Forms.TextBox txtSchedNmSearch;
        private System.Windows.Forms.Label lblScheduleNmSearch;
        private System.Windows.Forms.ComboBox cmbSchedTypeSearch;
        private System.Windows.Forms.Label lblEquipTypeSearch;
        public System.Windows.Forms.Button btnExcelUpload;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox chkReportChk;
        public System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}