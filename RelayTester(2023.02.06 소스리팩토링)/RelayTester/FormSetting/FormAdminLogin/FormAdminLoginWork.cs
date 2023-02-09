using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelayTester
{
    public class FormAdminLoginWork
    {
        public FormAdminLogin Form_AdminLogin;

        public DataSet mainDS = new DataSet();
        DbLink Dblink = new DbLink();

        public FormAdminLoginWork(FormAdminLogin form)
        {
            Form_AdminLogin = form;
        }

        public void ConfirmClick()
        {
            try
            {
                string pQuery = "EXEC _SAdminPW '','1'";

                Dblink.AllSelect(pQuery, mainDS);
                if (mainDS.Tables[0].Rows.Count >= 1)
                {
                    if (Form_AdminLogin.txtNowPW.Text != mainDS.Tables[0].Rows[0]["pw"].ToString())
                    {
                        MessageBox.Show("관리자 비밀번호가 일치하지 않습니다.\n다시 확인하여 주시기 바랍니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Form_AdminLogin.txtNowPW.Focus();
                        Form_AdminLogin.txtNowPW.SelectAll();
                        return;
                    }
                }

                Form_AdminLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
                Form_AdminLogin.Close();
            }
            catch (Exception ex)
            {
                Form_AdminLogin.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FA101", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        } //로그인 버튼 클릭 이벤트

        
    }
}
