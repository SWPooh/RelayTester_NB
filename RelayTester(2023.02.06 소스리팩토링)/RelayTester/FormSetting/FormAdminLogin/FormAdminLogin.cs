using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace RelayTester
{
    public partial class FormAdminLogin : Form
    {
        public FormAdminLoginWork codeWork;

        public FormAdminLogin()
        {
            InitializeComponent();
            codeWork = new FormAdminLoginWork(this);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            codeWork.ConfirmClick();
        } //로그인 버튼 클릭

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        } //취소 버튼 클릭

        private void txtNowPW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                codeWork.ConfirmClick();
            }
        } //엔터값 입력 시 이벤트

        private void FormAdminLogin_Load(object sender, EventArgs e)
        {

        }
    }
}

