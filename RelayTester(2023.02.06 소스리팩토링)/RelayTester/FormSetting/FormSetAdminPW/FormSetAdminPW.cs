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
    public partial class FormSetAdminPW : Form
    {
        public FormSetAdminPWWork codeWork;

        public FormSetAdminPW()
        {
            InitializeComponent();
            codeWork = new FormSetAdminPWWork(this);
        }

        private void FormAdminPW_Load(object sender, EventArgs e)
        {

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            codeWork.ConfirmClick();
        }


    }
}

