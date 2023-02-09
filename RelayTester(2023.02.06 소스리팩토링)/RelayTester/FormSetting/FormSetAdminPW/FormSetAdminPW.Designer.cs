namespace RelayTester
{
    partial class FormSetAdminPW
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetAdminPW));
            this.label1 = new System.Windows.Forms.Label();
            this.txtNowPW = new System.Windows.Forms.TextBox();
            this.txtNewPW = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNewPWConfirm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(49, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "현재 비밀번호";
            // 
            // txtNowPW
            // 
            this.txtNowPW.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtNowPW.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtNowPW.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtNowPW.Location = new System.Drawing.Point(244, 19);
            this.txtNowPW.MaxLength = 14;
            this.txtNowPW.Name = "txtNowPW";
            this.txtNowPW.PasswordChar = '*';
            this.txtNowPW.Size = new System.Drawing.Size(332, 43);
            this.txtNowPW.TabIndex = 1;
            // 
            // txtNewPW
            // 
            this.txtNewPW.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtNewPW.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtNewPW.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtNewPW.Location = new System.Drawing.Point(244, 68);
            this.txtNewPW.MaxLength = 14;
            this.txtNewPW.Name = "txtNewPW";
            this.txtNewPW.PasswordChar = '*';
            this.txtNewPW.Size = new System.Drawing.Size(332, 43);
            this.txtNewPW.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(76, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 37);
            this.label2.TabIndex = 5;
            this.label2.Text = "새 비밀번호";
            // 
            // txtNewPWConfirm
            // 
            this.txtNewPWConfirm.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtNewPWConfirm.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtNewPWConfirm.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtNewPWConfirm.Location = new System.Drawing.Point(244, 117);
            this.txtNewPWConfirm.MaxLength = 14;
            this.txtNewPWConfirm.Name = "txtNewPWConfirm";
            this.txtNewPWConfirm.PasswordChar = '*';
            this.txtNewPWConfirm.Size = new System.Drawing.Size(332, 43);
            this.txtNewPWConfirm.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(12, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(226, 37);
            this.label3.TabIndex = 7;
            this.label3.Text = "새 비밀번호 확인";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnConfirm.Image = ((System.Drawing.Image)(resources.GetObject("btnConfirm.Image")));
            this.btnConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirm.Location = new System.Drawing.Point(462, 166);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(114, 46);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "저장";
            this.btnConfirm.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // FormAdminPW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 221);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtNewPWConfirm);
            this.Controls.Add(this.txtNewPW);
            this.Controls.Add(this.txtNowPW);
            this.Controls.Add(this.label1);
            this.Name = "FormAdminPW";
            this.Text = "관리자 비밀번호 변경";
            this.Load += new System.EventHandler(this.FormAdminPW_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btnConfirm;
        public System.Windows.Forms.TextBox txtNowPW;
        public System.Windows.Forms.TextBox txtNewPW;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtNewPWConfirm;
        private System.Windows.Forms.Label label3;
    }
}