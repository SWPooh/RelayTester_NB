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
        public DataSet mainDS = new DataSet();
        DbLink Dblink = new DbLink();

        public FormAdminLogin()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string pQuery = "EXEC _SAdminPW '','1'";

                Dblink.AllSelect(pQuery, mainDS);
                if (mainDS.Tables[0].Rows.Count >= 1)
                {
                    if (txtNowPW.Text != mainDS.Tables[0].Rows[0]["pw"].ToString())
                    {
                        MessageBox.Show("관리자 비밀번호가 일치하지 않습니다.\n다시 확인하여 주시기 바랍니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtNowPW.Focus();
                        txtNowPW.SelectAll();
                        return;
                    }
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FA101", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void txtNowPW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnConfirm_Click(null, null);
            }
        }

    }
}

