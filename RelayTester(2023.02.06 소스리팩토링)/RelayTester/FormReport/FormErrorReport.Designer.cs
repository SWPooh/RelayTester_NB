namespace RelayTester
{
    partial class FormErrorReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormErrorReport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnQuery = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.dtpdate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExcelSave = new System.Windows.Forms.Button();
            this.rbt_all = new System.Windows.Forms.RadioButton();
            this.rbt_one = new System.Windows.Forms.RadioButton();
            this.cmbTestNum = new System.Windows.Forms.ComboBox();
            this.chkTestNum = new System.Windows.Forms.CheckBox();
            this.chkLot = new System.Windows.Forms.CheckBox();
            this.btnLotQuery = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.mtxtLot = new System.Windows.Forms.MaskedTextBox();
            this.lblLot = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvQueryResult = new System.Windows.Forms.DataGridView();
            this.gb_chk = new System.Windows.Forms.GroupBox();
            this.cmbTester = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbRelay = new System.Windows.Forms.ComboBox();
            this.lblEquipTypeSearch = new System.Windows.Forms.Label();
            this.chk_ReturnTime = new System.Windows.Forms.CheckBox();
            this.chk_OpenTime = new System.Windows.Forms.CheckBox();
            this.chk_DropCurrent = new System.Windows.Forms.CheckBox();
            this.chk_Coil = new System.Windows.Forms.CheckBox();
            this.chk_OpenCurrent = new System.Windows.Forms.CheckBox();
            this.chk_ContectResi = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueryResult)).BeginInit();
            this.gb_chk.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnQuery.Image = global::RelayTester.Properties.Resources.page_white_magnify;
            this.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuery.Location = new System.Drawing.Point(17, 20);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(102, 42);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.Text = "조회";
            this.btnQuery.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.dtpdate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnExcelSave);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.rbt_all);
            this.groupBox1.Controls.Add(this.rbt_one);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1568, 150);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "조회정보";
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(284, 20);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(102, 42);
            this.btnPrint.TabIndex = 37;
            this.btnPrint.Text = "출력";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Visible = false;
            // 
            // dtpdate
            // 
            this.dtpdate.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold);
            this.dtpdate.Location = new System.Drawing.Point(127, 85);
            this.dtpdate.Name = "dtpdate";
            this.dtpdate.Size = new System.Drawing.Size(409, 35);
            this.dtpdate.TabIndex = 33;
            this.dtpdate.ValueChanged += new System.EventHandler(this.dtpdate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(12, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 30);
            this.label1.TabIndex = 24;
            this.label1.Text = "시험일자";
            // 
            // btnExcelSave
            // 
            this.btnExcelSave.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnExcelSave.Image = ((System.Drawing.Image)(resources.GetObject("btnExcelSave.Image")));
            this.btnExcelSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcelSave.Location = new System.Drawing.Point(134, 20);
            this.btnExcelSave.Name = "btnExcelSave";
            this.btnExcelSave.Size = new System.Drawing.Size(144, 42);
            this.btnExcelSave.TabIndex = 11;
            this.btnExcelSave.Text = "엑셀저장";
            this.btnExcelSave.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnExcelSave.UseVisualStyleBackColor = true;
            this.btnExcelSave.Click += new System.EventHandler(this.btnExcelSave_Click);
            // 
            // rbt_all
            // 
            this.rbt_all.AutoSize = true;
            this.rbt_all.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.rbt_all.Location = new System.Drawing.Point(1276, 89);
            this.rbt_all.Name = "rbt_all";
            this.rbt_all.Size = new System.Drawing.Size(128, 36);
            this.rbt_all.TabIndex = 37;
            this.rbt_all.TabStop = true;
            this.rbt_all.Text = "전체선택";
            this.rbt_all.UseVisualStyleBackColor = true;
            this.rbt_all.Visible = false;
            this.rbt_all.CheckedChanged += new System.EventHandler(this.rbt_all_CheckedChanged);
            // 
            // rbt_one
            // 
            this.rbt_one.AutoSize = true;
            this.rbt_one.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.rbt_one.Location = new System.Drawing.Point(1418, 89);
            this.rbt_one.Name = "rbt_one";
            this.rbt_one.Size = new System.Drawing.Size(128, 36);
            this.rbt_one.TabIndex = 38;
            this.rbt_one.TabStop = true;
            this.rbt_one.Text = "개별선택";
            this.rbt_one.UseVisualStyleBackColor = true;
            this.rbt_one.Visible = false;
            this.rbt_one.CheckedChanged += new System.EventHandler(this.rbt_one_CheckedChanged);
            // 
            // cmbTestNum
            // 
            this.cmbTestNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTestNum.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmbTestNum.FormattingEnabled = true;
            this.cmbTestNum.Location = new System.Drawing.Point(1144, 45);
            this.cmbTestNum.Name = "cmbTestNum";
            this.cmbTestNum.Size = new System.Drawing.Size(134, 38);
            this.cmbTestNum.TabIndex = 50;
            this.cmbTestNum.Visible = false;
            // 
            // chkTestNum
            // 
            this.chkTestNum.AutoSize = true;
            this.chkTestNum.Location = new System.Drawing.Point(1024, 61);
            this.chkTestNum.Name = "chkTestNum";
            this.chkTestNum.Size = new System.Drawing.Size(15, 14);
            this.chkTestNum.TabIndex = 49;
            this.chkTestNum.UseVisualStyleBackColor = true;
            this.chkTestNum.Visible = false;
            this.chkTestNum.CheckedChanged += new System.EventHandler(this.chkTestNum_CheckedChanged);
            // 
            // chkLot
            // 
            this.chkLot.AutoSize = true;
            this.chkLot.Location = new System.Drawing.Point(601, 59);
            this.chkLot.Name = "chkLot";
            this.chkLot.Size = new System.Drawing.Size(15, 14);
            this.chkLot.TabIndex = 48;
            this.chkLot.UseVisualStyleBackColor = true;
            this.chkLot.CheckedChanged += new System.EventHandler(this.chkLot_CheckedChanged);
            // 
            // btnLotQuery
            // 
            this.btnLotQuery.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLotQuery.Image = global::RelayTester.Properties.Resources.page_white_magnify;
            this.btnLotQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLotQuery.Location = new System.Drawing.Point(876, 45);
            this.btnLotQuery.Name = "btnLotQuery";
            this.btnLotQuery.Size = new System.Drawing.Size(109, 42);
            this.btnLotQuery.TabIndex = 47;
            this.btnLotQuery.Text = "LOT조회";
            this.btnLotQuery.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLotQuery.UseVisualStyleBackColor = true;
            this.btnLotQuery.Click += new System.EventHandler(this.btnLotQuery_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(1044, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 30);
            this.label3.TabIndex = 45;
            this.label3.Text = "검사번호";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label3.Visible = false;
            // 
            // mtxtLot
            // 
            this.mtxtLot.BackColor = System.Drawing.SystemColors.Window;
            this.mtxtLot.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold);
            this.mtxtLot.Location = new System.Drawing.Point(722, 47);
            this.mtxtLot.Mask = "00-AA";
            this.mtxtLot.Name = "mtxtLot";
            this.mtxtLot.Size = new System.Drawing.Size(134, 35);
            this.mtxtLot.TabIndex = 42;
            this.mtxtLot.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtxtLot.TextChanged += new System.EventHandler(this.mtxtLot_TextChanged);
            // 
            // lblLot
            // 
            this.lblLot.AutoSize = true;
            this.lblLot.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblLot.Location = new System.Drawing.Point(622, 49);
            this.lblLot.Name = "lblLot";
            this.lblLot.Size = new System.Drawing.Size(94, 30);
            this.lblLot.TabIndex = 43;
            this.lblLot.Text = "LOT번호";
            this.lblLot.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvQueryResult);
            this.groupBox2.Location = new System.Drawing.Point(13, 377);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1567, 674);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "조회결과";
            // 
            // dgvQueryResult
            // 
            this.dgvQueryResult.AllowUserToAddRows = false;
            this.dgvQueryResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvQueryResult.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvQueryResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvQueryResult.ColumnHeadersHeight = 41;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvQueryResult.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvQueryResult.Location = new System.Drawing.Point(6, 20);
            this.dgvQueryResult.Name = "dgvQueryResult";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvQueryResult.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvQueryResult.RowHeadersVisible = false;
            this.dgvQueryResult.RowHeadersWidth = 51;
            this.dgvQueryResult.RowTemplate.Height = 23;
            this.dgvQueryResult.Size = new System.Drawing.Size(1555, 648);
            this.dgvQueryResult.TabIndex = 10;
            this.dgvQueryResult.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvQueryResult_CellFormatting);
            this.dgvQueryResult.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvQueryResult_CellPainting);
            // 
            // gb_chk
            // 
            this.gb_chk.Controls.Add(this.cmbTester);
            this.gb_chk.Controls.Add(this.label2);
            this.gb_chk.Controls.Add(this.cmbRelay);
            this.gb_chk.Controls.Add(this.lblEquipTypeSearch);
            this.gb_chk.Controls.Add(this.cmbTestNum);
            this.gb_chk.Controls.Add(this.chk_ReturnTime);
            this.gb_chk.Controls.Add(this.chkTestNum);
            this.gb_chk.Controls.Add(this.chk_OpenTime);
            this.gb_chk.Controls.Add(this.chkLot);
            this.gb_chk.Controls.Add(this.chk_DropCurrent);
            this.gb_chk.Controls.Add(this.btnLotQuery);
            this.gb_chk.Controls.Add(this.chk_Coil);
            this.gb_chk.Controls.Add(this.label3);
            this.gb_chk.Controls.Add(this.chk_OpenCurrent);
            this.gb_chk.Controls.Add(this.mtxtLot);
            this.gb_chk.Controls.Add(this.chk_ContectResi);
            this.gb_chk.Controls.Add(this.lblLot);
            this.gb_chk.Location = new System.Drawing.Point(13, 168);
            this.gb_chk.Name = "gb_chk";
            this.gb_chk.Size = new System.Drawing.Size(1567, 203);
            this.gb_chk.TabIndex = 38;
            this.gb_chk.TabStop = false;
            this.gb_chk.Text = "조회항목";
            // 
            // cmbTester
            // 
            this.cmbTester.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTester.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmbTester.FormattingEnabled = true;
            this.cmbTester.Items.AddRange(new object[] {
            "전체"});
            this.cmbTester.Location = new System.Drawing.Point(93, 46);
            this.cmbTester.Name = "cmbTester";
            this.cmbTester.Size = new System.Drawing.Size(152, 38);
            this.cmbTester.TabIndex = 54;
            this.cmbTester.SelectedIndexChanged += new System.EventHandler(this.cmbTester_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(11, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 30);
            this.label2.TabIndex = 53;
            this.label2.Text = "시험기";
            // 
            // cmbRelay
            // 
            this.cmbRelay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRelay.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmbRelay.FormattingEnabled = true;
            this.cmbRelay.Items.AddRange(new object[] {
            "전체"});
            this.cmbRelay.Location = new System.Drawing.Point(414, 46);
            this.cmbRelay.Name = "cmbRelay";
            this.cmbRelay.Size = new System.Drawing.Size(152, 38);
            this.cmbRelay.TabIndex = 52;
            this.cmbRelay.SelectedIndexChanged += new System.EventHandler(this.cmbRelay_SelectedIndexChanged);
            // 
            // lblEquipTypeSearch
            // 
            this.lblEquipTypeSearch.AutoSize = true;
            this.lblEquipTypeSearch.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblEquipTypeSearch.Location = new System.Drawing.Point(289, 49);
            this.lblEquipTypeSearch.Name = "lblEquipTypeSearch";
            this.lblEquipTypeSearch.Size = new System.Drawing.Size(125, 30);
            this.lblEquipTypeSearch.TabIndex = 51;
            this.lblEquipTypeSearch.Text = "계전기 종류";
            // 
            // chk_ReturnTime
            // 
            this.chk_ReturnTime.AutoSize = true;
            this.chk_ReturnTime.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold);
            this.chk_ReturnTime.Location = new System.Drawing.Point(686, 132);
            this.chk_ReturnTime.Name = "chk_ReturnTime";
            this.chk_ReturnTime.Size = new System.Drawing.Size(116, 34);
            this.chk_ReturnTime.TabIndex = 44;
            this.chk_ReturnTime.Text = "복귀시간";
            this.chk_ReturnTime.UseVisualStyleBackColor = true;
            this.chk_ReturnTime.Visible = false;
            // 
            // chk_OpenTime
            // 
            this.chk_OpenTime.AutoSize = true;
            this.chk_OpenTime.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold);
            this.chk_OpenTime.Location = new System.Drawing.Point(552, 132);
            this.chk_OpenTime.Name = "chk_OpenTime";
            this.chk_OpenTime.Size = new System.Drawing.Size(116, 34);
            this.chk_OpenTime.TabIndex = 43;
            this.chk_OpenTime.Text = "동작시간";
            this.chk_OpenTime.UseVisualStyleBackColor = true;
            this.chk_OpenTime.Visible = false;
            // 
            // chk_DropCurrent
            // 
            this.chk_DropCurrent.AutoSize = true;
            this.chk_DropCurrent.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold);
            this.chk_DropCurrent.Location = new System.Drawing.Point(284, 132);
            this.chk_DropCurrent.Name = "chk_DropCurrent";
            this.chk_DropCurrent.Size = new System.Drawing.Size(116, 34);
            this.chk_DropCurrent.TabIndex = 42;
            this.chk_DropCurrent.Text = "낙하전류";
            this.chk_DropCurrent.UseVisualStyleBackColor = true;
            this.chk_DropCurrent.Visible = false;
            // 
            // chk_Coil
            // 
            this.chk_Coil.AutoSize = true;
            this.chk_Coil.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold);
            this.chk_Coil.Location = new System.Drawing.Point(418, 132);
            this.chk_Coil.Name = "chk_Coil";
            this.chk_Coil.Size = new System.Drawing.Size(116, 34);
            this.chk_Coil.TabIndex = 41;
            this.chk_Coil.Text = "코일저항";
            this.chk_Coil.UseVisualStyleBackColor = true;
            this.chk_Coil.Visible = false;
            // 
            // chk_OpenCurrent
            // 
            this.chk_OpenCurrent.AutoSize = true;
            this.chk_OpenCurrent.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold);
            this.chk_OpenCurrent.Location = new System.Drawing.Point(150, 132);
            this.chk_OpenCurrent.Name = "chk_OpenCurrent";
            this.chk_OpenCurrent.Size = new System.Drawing.Size(116, 34);
            this.chk_OpenCurrent.TabIndex = 40;
            this.chk_OpenCurrent.Text = "동작전류";
            this.chk_OpenCurrent.UseVisualStyleBackColor = true;
            this.chk_OpenCurrent.Visible = false;
            // 
            // chk_ContectResi
            // 
            this.chk_ContectResi.AutoSize = true;
            this.chk_ContectResi.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold);
            this.chk_ContectResi.Location = new System.Drawing.Point(16, 132);
            this.chk_ContectResi.Name = "chk_ContectResi";
            this.chk_ContectResi.Size = new System.Drawing.Size(116, 34);
            this.chk_ContectResi.TabIndex = 39;
            this.chk_ContectResi.Text = "접촉저항";
            this.chk_ContectResi.UseVisualStyleBackColor = true;
            this.chk_ContectResi.Visible = false;
            // 
            // FormErrorReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 1062);
            this.Controls.Add(this.gb_chk);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormErrorReport";
            this.Text = "FormErrorReport";
            this.Load += new System.EventHandler(this.FormErrorReport_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueryResult)).EndInit();
            this.gb_chk.ResumeLayout(false);
            this.gb_chk.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnQuery;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Button btnExcelSave;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.DateTimePicker dtpdate;
        public System.Windows.Forms.GroupBox gb_chk;
        public System.Windows.Forms.CheckBox chk_ReturnTime;
        public System.Windows.Forms.CheckBox chk_OpenTime;
        public System.Windows.Forms.CheckBox chk_DropCurrent;
        public System.Windows.Forms.CheckBox chk_Coil;
        public System.Windows.Forms.CheckBox chk_OpenCurrent;
        public System.Windows.Forms.CheckBox chk_ContectResi;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.MaskedTextBox mtxtLot;
        public System.Windows.Forms.Label lblLot;
        public System.Windows.Forms.ComboBox cmbTestNum;
        public System.Windows.Forms.CheckBox chkTestNum;
        public System.Windows.Forms.CheckBox chkLot;
        public System.Windows.Forms.Button btnLotQuery;
        public System.Windows.Forms.Button btnPrint;
        public System.Windows.Forms.RadioButton rbt_all;
        public System.Windows.Forms.RadioButton rbt_one;
        public System.Windows.Forms.ComboBox cmbTester;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox cmbRelay;
        public System.Windows.Forms.Label lblEquipTypeSearch;
        public System.Windows.Forms.DataGridView dgvQueryResult;
    }
}