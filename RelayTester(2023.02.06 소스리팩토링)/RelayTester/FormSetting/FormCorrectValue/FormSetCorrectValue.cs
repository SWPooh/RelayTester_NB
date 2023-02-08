using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace RelayTester
{
    public partial class FormSetCorrectValue : Form
    {
        public FormCorrectValueWork codeWork;

        public FormSetCorrectValue()
        {
            InitializeComponent();
            codeWork = new FormCorrectValueWork(this);
        }

        private void FormCaliMng_Load(object sender, EventArgs e)
        {
            codeWork.FormLoad();
            //btnQuery_Click(null, null);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            codeWork.SaveClick();
        }
        private void dgvCommonMst_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            codeWork.CellClick();
        }
    }
}
