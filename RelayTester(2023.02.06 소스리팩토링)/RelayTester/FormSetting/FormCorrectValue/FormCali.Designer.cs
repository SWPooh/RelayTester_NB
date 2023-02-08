namespace RelayTester
{
    partial class FormCali
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
            this.grbMenu = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtCResiB = new System.Windows.Forms.TextBox();
            this.txtCCurrA = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCVoltA = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDropB = new System.Windows.Forms.TextBox();
            this.txtDropA = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOperB = new System.Windows.Forms.TextBox();
            this.txtOperA = new System.Windows.Forms.TextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.grbMenu.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbMenu
            // 
            this.grbMenu.Controls.Add(this.btnSave);
            this.grbMenu.Controls.Add(this.btnQuery);
            this.grbMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbMenu.Font = new System.Drawing.Font("새굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grbMenu.Location = new System.Drawing.Point(0, 0);
            this.grbMenu.Name = "grbMenu";
            this.grbMenu.Size = new System.Drawing.Size(563, 76);
            this.grbMenu.TabIndex = 0;
            this.grbMenu.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSave.Image = global::RelayTester.Properties.Resources.page_white_database;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(114, 21);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 42);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "저장";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnQuery.Image = global::RelayTester.Properties.Resources.page_white_magnify;
            this.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuery.Location = new System.Drawing.Point(6, 21);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(102, 42);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "조회";
            this.btnQuery.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("새굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDelete.Image = global::RelayTester.Properties.Resources.page_white_delete;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(949, 21);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 42);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "삭제";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("새굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAdd.Image = global::RelayTester.Properties.Resources.page_white_add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(869, 21);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 42);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "추가";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
            // 
            // btnNew
            // 
            this.btnNew.Font = new System.Drawing.Font("새굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnNew.Image = global::RelayTester.Properties.Resources.page_white;
            this.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNew.Location = new System.Drawing.Point(709, 21);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(90, 42);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "신규";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtCResiB);
            this.groupBox3.Controls.Add(this.txtCCurrA);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtCVoltA);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtDropB);
            this.groupBox3.Controls.Add(this.txtDropA);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtOperB);
            this.groupBox3.Controls.Add(this.txtOperA);
            this.groupBox3.Controls.Add(this.label64);
            this.groupBox3.Controls.Add(this.label65);
            this.groupBox3.Controls.Add(this.label67);
            this.groupBox3.Controls.Add(this.label66);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox3.Location = new System.Drawing.Point(0, 76);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(563, 297);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "시험기";
            // 
            // txtCResiB
            // 
            this.txtCResiB.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtCResiB.Location = new System.Drawing.Point(387, 253);
            this.txtCResiB.Name = "txtCResiB";
            this.txtCResiB.Size = new System.Drawing.Size(100, 35);
            this.txtCResiB.TabIndex = 69;
            this.txtCResiB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCCurrA
            // 
            this.txtCCurrA.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtCCurrA.Location = new System.Drawing.Point(281, 213);
            this.txtCCurrA.Name = "txtCCurrA";
            this.txtCCurrA.Size = new System.Drawing.Size(100, 35);
            this.txtCCurrA.TabIndex = 68;
            this.txtCCurrA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(6, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(269, 35);
            this.label5.TabIndex = 71;
            this.label5.Text = "코일저항 전류 수신값 x A";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(493, 213);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 35);
            this.label6.TabIndex = 70;
            this.label6.Text = "mA";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCVoltA
            // 
            this.txtCVoltA.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtCVoltA.Location = new System.Drawing.Point(281, 172);
            this.txtCVoltA.Name = "txtCVoltA";
            this.txtCVoltA.Size = new System.Drawing.Size(100, 35);
            this.txtCVoltA.TabIndex = 64;
            this.txtCVoltA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(6, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(269, 35);
            this.label3.TabIndex = 67;
            this.label3.Text = "코일저항 전압 수신값 x A";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(493, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 35);
            this.label4.TabIndex = 66;
            this.label4.Text = "mV";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDropB
            // 
            this.txtDropB.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtDropB.Location = new System.Drawing.Point(387, 110);
            this.txtDropB.Name = "txtDropB";
            this.txtDropB.Size = new System.Drawing.Size(100, 35);
            this.txtDropB.TabIndex = 61;
            this.txtDropB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDropA
            // 
            this.txtDropA.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtDropA.Location = new System.Drawing.Point(281, 110);
            this.txtDropA.Name = "txtDropA";
            this.txtDropA.Size = new System.Drawing.Size(100, 35);
            this.txtDropA.TabIndex = 60;
            this.txtDropA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(6, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(269, 35);
            this.label1.TabIndex = 63;
            this.label1.Text = "낙하전류 수신값 x A + B";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(493, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 35);
            this.label2.TabIndex = 62;
            this.label2.Text = "mA";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtOperB
            // 
            this.txtOperB.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtOperB.Location = new System.Drawing.Point(387, 69);
            this.txtOperB.Name = "txtOperB";
            this.txtOperB.Size = new System.Drawing.Size(100, 35);
            this.txtOperB.TabIndex = 2;
            this.txtOperB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtOperA
            // 
            this.txtOperA.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtOperA.Location = new System.Drawing.Point(281, 69);
            this.txtOperA.Name = "txtOperA";
            this.txtOperA.Size = new System.Drawing.Size(100, 35);
            this.txtOperA.TabIndex = 1;
            this.txtOperA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label64
            // 
            this.label64.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label64.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label64.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label64.Location = new System.Drawing.Point(387, 31);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(100, 33);
            this.label64.TabIndex = 59;
            this.label64.Text = "B";
            this.label64.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label65
            // 
            this.label65.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label65.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label65.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label65.Location = new System.Drawing.Point(281, 31);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(100, 33);
            this.label65.TabIndex = 58;
            this.label65.Text = "A";
            this.label65.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label67
            // 
            this.label67.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label67.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label67.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label67.Location = new System.Drawing.Point(6, 70);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(269, 35);
            this.label67.TabIndex = 57;
            this.label67.Text = "동작전류 수신값 x A + B";
            this.label67.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label66
            // 
            this.label66.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label66.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label66.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label66.Location = new System.Drawing.Point(493, 69);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(58, 35);
            this.label66.TabIndex = 4;
            this.label66.Text = "mA";
            this.label66.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.Location = new System.Drawing.Point(6, 254);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(269, 35);
            this.label7.TabIndex = 72;
            this.label7.Text = "코일저항 + B";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.Location = new System.Drawing.Point(493, 254);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 35);
            this.label8.TabIndex = 73;
            this.label8.Text = "Ω";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormCali
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 373);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.grbMenu);
            this.Name = "FormCali";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "보정값";
            this.Load += new System.EventHandler(this.FormCali_Load);
            this.grbMenu.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbMenu;
        public System.Windows.Forms.Button btnNew;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label67;
        public System.Windows.Forms.TextBox txtOperB;
        public System.Windows.Forms.TextBox txtOperA;
        public System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.TextBox txtCResiB;
        public System.Windows.Forms.TextBox txtCCurrA;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtCVoltA;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtDropB;
        public System.Windows.Forms.TextBox txtDropA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}