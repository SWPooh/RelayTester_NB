namespace RelayTester
{
    partial class FormMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.성적서ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FormSelectResult = new System.Windows.Forms.ToolStripMenuItem();
            this.FormReportPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.FormErrorReport = new System.Windows.Forms.ToolStripMenuItem();
            this.설정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FormBaseCode = new System.Windows.Forms.ToolStripMenuItem();
            this.FormSetCorrectValue = new System.Windows.Forms.ToolStripMenuItem();
            this.FormSetTester = new System.Windows.Forms.ToolStripMenuItem();
            this.FormSetSchedule = new System.Windows.Forms.ToolStripMenuItem();
            this.FormSetTestValue = new System.Windows.Forms.ToolStripMenuItem();
            this.FormSetErrorCode = new System.Windows.Forms.ToolStripMenuItem();
            this.FormSetActionError = new System.Windows.Forms.ToolStripMenuItem();
            this.FormSetWorker = new System.Windows.Forms.ToolStripMenuItem();
            this.FormSetAdminPW = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRelayTester1 = new System.Windows.Forms.Button();
            this.btnRelayTester2 = new System.Windows.Forms.Button();
            this.btnRelayTester3 = new System.Windows.Forms.Button();
            this.cmbIOGbn = new System.Windows.Forms.ComboBox();
            this.ConnectOption = new System.Windows.Forms.Label();
            this.lbl_mainLot = new System.Windows.Forms.Label();
            this.txt_mainLot = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.성적서ToolStripMenuItem,
            this.설정ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1576, 38);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 성적서ToolStripMenuItem
            // 
            this.성적서ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FormSelectResult,
            this.FormReportPrint,
            this.FormErrorReport});
            this.성적서ToolStripMenuItem.Name = "성적서ToolStripMenuItem";
            this.성적서ToolStripMenuItem.Size = new System.Drawing.Size(88, 34);
            this.성적서ToolStripMenuItem.Text = "성적서";
            // 
            // FormSelectResult
            // 
            this.FormSelectResult.Image = ((System.Drawing.Image)(resources.GetObject("FormSelectResult.Image")));
            this.FormSelectResult.Name = "FormSelectResult";
            this.FormSelectResult.Size = new System.Drawing.Size(293, 34);
            this.FormSelectResult.Text = "성적서용 결과값 선택";
            this.FormSelectResult.Click += new System.EventHandler(this.FormSelectResult_Click);
            // 
            // FormReportPrint
            // 
            this.FormReportPrint.Image = ((System.Drawing.Image)(resources.GetObject("FormReportPrint.Image")));
            this.FormReportPrint.Name = "FormReportPrint";
            this.FormReportPrint.Size = new System.Drawing.Size(293, 34);
            this.FormReportPrint.Text = "시험 성적서 출력";
            this.FormReportPrint.Click += new System.EventHandler(this.FormReportPrint_Click);
            // 
            // FormErrorReport
            // 
            this.FormErrorReport.Image = ((System.Drawing.Image)(resources.GetObject("FormErrorReport.Image")));
            this.FormErrorReport.Name = "FormErrorReport";
            this.FormErrorReport.Size = new System.Drawing.Size(293, 34);
            this.FormErrorReport.Text = "오류조치결과 출력";
            this.FormErrorReport.Click += new System.EventHandler(this.FormErrorReport_Click);
            // 
            // 설정ToolStripMenuItem
            // 
            this.설정ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FormBaseCode,
            this.FormSetCorrectValue,
            this.FormSetTester,
            this.FormSetSchedule,
            this.FormSetTestValue,
            this.FormSetErrorCode,
            this.FormSetActionError,
            this.FormSetWorker,
            this.FormSetAdminPW});
            this.설정ToolStripMenuItem.Name = "설정ToolStripMenuItem";
            this.설정ToolStripMenuItem.Size = new System.Drawing.Size(67, 34);
            this.설정ToolStripMenuItem.Text = "설정";
            // 
            // FormBaseCode
            // 
            this.FormBaseCode.Image = ((System.Drawing.Image)(resources.GetObject("FormBaseCode.Image")));
            this.FormBaseCode.Name = "FormBaseCode";
            this.FormBaseCode.Size = new System.Drawing.Size(293, 34);
            this.FormBaseCode.Text = "기초코드관리";
            this.FormBaseCode.Click += new System.EventHandler(this.FormBaseCode_Click);
            // 
            // FormSetCorrectValue
            // 
            this.FormSetCorrectValue.Image = ((System.Drawing.Image)(resources.GetObject("FormSetCorrectValue.Image")));
            this.FormSetCorrectValue.Name = "FormSetCorrectValue";
            this.FormSetCorrectValue.Size = new System.Drawing.Size(293, 34);
            this.FormSetCorrectValue.Text = "보정값 관리";
            this.FormSetCorrectValue.Click += new System.EventHandler(this.FormSetCorrectValue_Click);
            // 
            // FormSetTester
            // 
            this.FormSetTester.Image = ((System.Drawing.Image)(resources.GetObject("FormSetTester.Image")));
            this.FormSetTester.Name = "FormSetTester";
            this.FormSetTester.Size = new System.Drawing.Size(293, 34);
            this.FormSetTester.Text = "계전기 관리";
            this.FormSetTester.Click += new System.EventHandler(this.FormSetTester_Click);
            // 
            // FormSetSchedule
            // 
            this.FormSetSchedule.Image = ((System.Drawing.Image)(resources.GetObject("FormSetSchedule.Image")));
            this.FormSetSchedule.Name = "FormSetSchedule";
            this.FormSetSchedule.Size = new System.Drawing.Size(293, 34);
            this.FormSetSchedule.Text = "스케줄 관리";
            this.FormSetSchedule.Click += new System.EventHandler(this.FormSetSchedule_Click);
            // 
            // FormSetTestValue
            // 
            this.FormSetTestValue.Image = ((System.Drawing.Image)(resources.GetObject("FormSetTestValue.Image")));
            this.FormSetTestValue.Name = "FormSetTestValue";
            this.FormSetTestValue.Size = new System.Drawing.Size(293, 34);
            this.FormSetTestValue.Text = "시험 기준값 관리";
            this.FormSetTestValue.Click += new System.EventHandler(this.FormSetTestValue_Click);
            // 
            // FormSetErrorCode
            // 
            this.FormSetErrorCode.Image = ((System.Drawing.Image)(resources.GetObject("FormSetErrorCode.Image")));
            this.FormSetErrorCode.Name = "FormSetErrorCode";
            this.FormSetErrorCode.Size = new System.Drawing.Size(293, 34);
            this.FormSetErrorCode.Text = "에러코드 관리";
            this.FormSetErrorCode.Click += new System.EventHandler(this.FormSetErrorCode_Click);
            // 
            // FormSetActionError
            // 
            this.FormSetActionError.Image = ((System.Drawing.Image)(resources.GetObject("FormSetActionError.Image")));
            this.FormSetActionError.Name = "FormSetActionError";
            this.FormSetActionError.Size = new System.Drawing.Size(293, 34);
            this.FormSetActionError.Text = "조치코드 관리";
            this.FormSetActionError.Click += new System.EventHandler(this.FormSetActionError_Click);
            // 
            // FormSetWorker
            // 
            this.FormSetWorker.Image = ((System.Drawing.Image)(resources.GetObject("FormSetWorker.Image")));
            this.FormSetWorker.Name = "FormSetWorker";
            this.FormSetWorker.Size = new System.Drawing.Size(293, 34);
            this.FormSetWorker.Text = "작업자 관리";
            this.FormSetWorker.Click += new System.EventHandler(this.FormSetWorker_Click);
            // 
            // FormSetAdminPW
            // 
            this.FormSetAdminPW.Image = ((System.Drawing.Image)(resources.GetObject("FormSetAdminPW.Image")));
            this.FormSetAdminPW.Name = "FormSetAdminPW";
            this.FormSetAdminPW.Size = new System.Drawing.Size(293, 34);
            this.FormSetAdminPW.Text = "관리자 비밀번호 변경";
            this.FormSetAdminPW.Click += new System.EventHandler(this.FormSetAdminPW_Click);
            // 
            // btnRelayTester1
            // 
            this.btnRelayTester1.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRelayTester1.Location = new System.Drawing.Point(475, 2);
            this.btnRelayTester1.Name = "btnRelayTester1";
            this.btnRelayTester1.Size = new System.Drawing.Size(162, 36);
            this.btnRelayTester1.TabIndex = 5;
            this.btnRelayTester1.Text = "1번 시험기";
            this.btnRelayTester1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRelayTester1.UseVisualStyleBackColor = true;
            this.btnRelayTester1.Click += new System.EventHandler(this.btnRelayTester1_Click);
            // 
            // btnRelayTester2
            // 
            this.btnRelayTester2.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRelayTester2.Location = new System.Drawing.Point(643, 2);
            this.btnRelayTester2.Name = "btnRelayTester2";
            this.btnRelayTester2.Size = new System.Drawing.Size(162, 36);
            this.btnRelayTester2.TabIndex = 6;
            this.btnRelayTester2.Text = "2번 시험기";
            this.btnRelayTester2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRelayTester2.UseVisualStyleBackColor = true;
            this.btnRelayTester2.Click += new System.EventHandler(this.btnRelayTester2_Click);
            // 
            // btnRelayTester3
            // 
            this.btnRelayTester3.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRelayTester3.Location = new System.Drawing.Point(811, 2);
            this.btnRelayTester3.Name = "btnRelayTester3";
            this.btnRelayTester3.Size = new System.Drawing.Size(162, 36);
            this.btnRelayTester3.TabIndex = 7;
            this.btnRelayTester3.Text = "3번 시험기";
            this.btnRelayTester3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRelayTester3.UseVisualStyleBackColor = true;
            this.btnRelayTester3.Click += new System.EventHandler(this.btnRelayTester3_Click);
            // 
            // cmbIOGbn
            // 
            this.cmbIOGbn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbIOGbn.BackColor = System.Drawing.Color.White;
            this.cmbIOGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIOGbn.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cmbIOGbn.ForeColor = System.Drawing.Color.Black;
            this.cmbIOGbn.FormattingEnabled = true;
            this.cmbIOGbn.Items.AddRange(new object[] {
            "대아티아이(내부)",
            "대아티아이(외부)"});
            this.cmbIOGbn.Location = new System.Drawing.Point(1260, 5);
            this.cmbIOGbn.Name = "cmbIOGbn";
            this.cmbIOGbn.Size = new System.Drawing.Size(146, 29);
            this.cmbIOGbn.TabIndex = 11;
            this.cmbIOGbn.SelectedIndexChanged += new System.EventHandler(this.cmbIOGbn_SelectedIndexChanged);
            // 
            // ConnectOption
            // 
            this.ConnectOption.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectOption.AutoSize = true;
            this.ConnectOption.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ConnectOption.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectOption.ForeColor = System.Drawing.Color.Black;
            this.ConnectOption.Location = new System.Drawing.Point(1181, 9);
            this.ConnectOption.Name = "ConnectOption";
            this.ConnectOption.Size = new System.Drawing.Size(74, 21);
            this.ConnectOption.TabIndex = 10;
            this.ConnectOption.Text = "연결구분";
            this.ConnectOption.Click += new System.EventHandler(this.ConnectOption_Click);
            // 
            // lbl_mainLot
            // 
            this.lbl_mainLot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_mainLot.AutoSize = true;
            this.lbl_mainLot.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lbl_mainLot.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_mainLot.ForeColor = System.Drawing.Color.Black;
            this.lbl_mainLot.Location = new System.Drawing.Point(1412, 9);
            this.lbl_mainLot.Name = "lbl_mainLot";
            this.lbl_mainLot.Size = new System.Drawing.Size(77, 21);
            this.lbl_mainLot.TabIndex = 13;
            this.lbl_mainLot.Text = "LOT 조회";
            this.lbl_mainLot.Visible = false;
            this.lbl_mainLot.Click += new System.EventHandler(this.lbl_mainLot_Click);
            // 
            // txt_mainLot
            // 
            this.txt_mainLot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_mainLot.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.txt_mainLot.Location = new System.Drawing.Point(1495, 5);
            this.txt_mainLot.Name = "txt_mainLot";
            this.txt_mainLot.Size = new System.Drawing.Size(69, 29);
            this.txt_mainLot.TabIndex = 14;
            this.txt_mainLot.Text = "2020";
            this.txt_mainLot.Visible = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1576, 637);
            this.Controls.Add(this.txt_mainLot);
            this.Controls.Add(this.lbl_mainLot);
            this.Controls.Add(this.cmbIOGbn);
            this.Controls.Add(this.ConnectOption);
            this.Controls.Add(this.btnRelayTester3);
            this.Controls.Add(this.btnRelayTester2);
            this.Controls.Add(this.btnRelayTester1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "계전기 종합 시험기 V4.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem 설정ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FormSetWorker;
        private System.Windows.Forms.ToolStripMenuItem FormSetTestValue;
        private System.Windows.Forms.ToolStripMenuItem FormSetSchedule;
        private System.Windows.Forms.ToolStripMenuItem 성적서ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FormSelectResult;
        private System.Windows.Forms.ToolStripMenuItem FormSetTester;
        public System.Windows.Forms.Button btnRelayTester1;
        public System.Windows.Forms.Button btnRelayTester2;
        public System.Windows.Forms.Button btnRelayTester3;
        public System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FormSetAdminPW;
        private System.Windows.Forms.ToolStripMenuItem FormReportPrint;
        private System.Windows.Forms.ToolStripMenuItem FormSetCorrectValue;
        private System.Windows.Forms.ToolStripMenuItem FormBaseCode;
        private System.Windows.Forms.ToolStripMenuItem FormSetActionError;
        private System.Windows.Forms.ToolStripMenuItem FormErrorReport;
        private System.Windows.Forms.ToolStripMenuItem FormSetErrorCode;
        public System.Windows.Forms.ComboBox cmbIOGbn;
        public System.Windows.Forms.Label ConnectOption;
        public System.Windows.Forms.Label lbl_mainLot;
        public System.Windows.Forms.TextBox txt_mainLot;
    }
}

