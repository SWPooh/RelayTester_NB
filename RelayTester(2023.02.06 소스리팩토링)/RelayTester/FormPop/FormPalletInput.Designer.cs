namespace RelayTester
{
    partial class FormPalletInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPalletInput));
            this.label1 = new System.Windows.Forms.Label();
            this.txtRelayBarcode = new System.Windows.Forms.TextBox();
            this.btnPalletInput = new System.Windows.Forms.Button();
            this.btnPalletMove = new System.Windows.Forms.Button();
            this.btnPalletDel = new System.Windows.Forms.Button();
            this.txtPalNum = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(88, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "바코드";
            // 
            // txtRelayBarcode
            // 
            this.txtRelayBarcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRelayBarcode.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtRelayBarcode.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtRelayBarcode.Location = new System.Drawing.Point(181, 16);
            this.txtRelayBarcode.MaxLength = 14;
            this.txtRelayBarcode.Name = "txtRelayBarcode";
            this.txtRelayBarcode.Size = new System.Drawing.Size(332, 43);
            this.txtRelayBarcode.TabIndex = 1;
            this.txtRelayBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // btnPalletInput
            // 
            this.btnPalletInput.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPalletInput.Image = ((System.Drawing.Image)(resources.GetObject("btnPalletInput.Image")));
            this.btnPalletInput.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPalletInput.Location = new System.Drawing.Point(12, 65);
            this.btnPalletInput.Name = "btnPalletInput";
            this.btnPalletInput.Size = new System.Drawing.Size(163, 46);
            this.btnPalletInput.TabIndex = 2;
            this.btnPalletInput.Text = "파레트 입력";
            this.btnPalletInput.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnPalletInput.UseVisualStyleBackColor = true;
            this.btnPalletInput.Click += new System.EventHandler(this.btnPalletInput_Click);
            // 
            // btnPalletMove
            // 
            this.btnPalletMove.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPalletMove.Image = ((System.Drawing.Image)(resources.GetObject("btnPalletMove.Image")));
            this.btnPalletMove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPalletMove.Location = new System.Drawing.Point(181, 65);
            this.btnPalletMove.Name = "btnPalletMove";
            this.btnPalletMove.Size = new System.Drawing.Size(163, 46);
            this.btnPalletMove.TabIndex = 3;
            this.btnPalletMove.Text = "파레트 이동";
            this.btnPalletMove.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnPalletMove.UseVisualStyleBackColor = true;
            this.btnPalletMove.Click += new System.EventHandler(this.btnPalletMove_Click);
            // 
            // btnPalletDel
            // 
            this.btnPalletDel.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPalletDel.Image = ((System.Drawing.Image)(resources.GetObject("btnPalletDel.Image")));
            this.btnPalletDel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPalletDel.Location = new System.Drawing.Point(350, 65);
            this.btnPalletDel.Name = "btnPalletDel";
            this.btnPalletDel.Size = new System.Drawing.Size(163, 46);
            this.btnPalletDel.TabIndex = 4;
            this.btnPalletDel.Text = "파레트 삭제";
            this.btnPalletDel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnPalletDel.UseVisualStyleBackColor = true;
            this.btnPalletDel.Click += new System.EventHandler(this.btnPalletDel_Click);
            // 
            // txtPalNum
            // 
            this.txtPalNum.BackColor = System.Drawing.SystemColors.Control;
            this.txtPalNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPalNum.Enabled = false;
            this.txtPalNum.Font = new System.Drawing.Font("굴림", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtPalNum.Location = new System.Drawing.Point(12, 12);
            this.txtPalNum.Name = "txtPalNum";
            this.txtPalNum.Size = new System.Drawing.Size(70, 50);
            this.txtPalNum.TabIndex = 5;
            this.txtPalNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FormPalletInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 123);
            this.Controls.Add(this.txtPalNum);
            this.Controls.Add(this.btnPalletDel);
            this.Controls.Add(this.btnPalletMove);
            this.Controls.Add(this.btnPalletInput);
            this.Controls.Add(this.txtRelayBarcode);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPalletInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "파레트 입력";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPalletInput_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btnPalletInput;
        public System.Windows.Forms.Button btnPalletMove;
        public System.Windows.Forms.Button btnPalletDel;
        public System.Windows.Forms.TextBox txtRelayBarcode;
        public System.Windows.Forms.TextBox txtPalNum;
    }
}