namespace RelayTester
{
    partial class FormProgressbar
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
            this.prbLoad = new System.Windows.Forms.ProgressBar();
            this.lb_LoadCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_prbText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // prbLoad
            // 
            this.prbLoad.Location = new System.Drawing.Point(52, 72);
            this.prbLoad.Name = "prbLoad";
            this.prbLoad.Size = new System.Drawing.Size(526, 61);
            this.prbLoad.TabIndex = 0;
            // 
            // lb_LoadCount
            // 
            this.lb_LoadCount.AutoSize = true;
            this.lb_LoadCount.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_LoadCount.Location = new System.Drawing.Point(267, 145);
            this.lb_LoadCount.Name = "lb_LoadCount";
            this.lb_LoadCount.Size = new System.Drawing.Size(21, 20);
            this.lb_LoadCount.TabIndex = 1;
            this.lb_LoadCount.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(329, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "%";
            // 
            // label_prbText
            // 
            this.label_prbText.AutoSize = true;
            this.label_prbText.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_prbText.Location = new System.Drawing.Point(48, 29);
            this.label_prbText.Name = "label_prbText";
            this.label_prbText.Size = new System.Drawing.Size(0, 29);
            this.label_prbText.TabIndex = 3;
            // 
            // FormProgressbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(640, 219);
            this.ControlBox = false;
            this.Controls.Add(this.label_prbText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lb_LoadCount);
            this.Controls.Add(this.prbLoad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormProgressbar";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "작업 진행률";
            this.Load += new System.EventHandler(this.FormProgressbar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar prbLoad;
        private System.Windows.Forms.Label lb_LoadCount;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label_prbText;
    }
}