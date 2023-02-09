using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelayTester
{

    public class FormSetAdminPWWork
    {
        public FormSetAdminPW Form_Adminpw;

        public DataSet mainDS = new DataSet();
        DbLink Dblink = new DbLink();

        public FormSetAdminPWWork(FormSetAdminPW form) 
        {
            Form_Adminpw= form;
        }

        public void ConfirmClick()
        {
            try
            {
                string pQuery = "EXEC _SAdminPW '', '1'";

                Dblink.AllSelect(pQuery, mainDS);
                if (mainDS.Tables[0].Rows.Count >= 1)
                {
                    if (Form_Adminpw.txtNowPW.Text != mainDS.Tables[0].Rows[0]["pw"].ToString())
                    {
                        MessageBox.Show("현재 비밀번호가 일치하지 않습니다.\n다시 확인하여 주시기 바랍니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Form_Adminpw.txtNowPW.Focus();
                        Form_Adminpw.txtNowPW.SelectAll();
                        return;
                    }
                }

                if (Form_Adminpw.txtNewPW.Text != Form_Adminpw.txtNewPWConfirm.Text)
                {
                    MessageBox.Show("새 비밀번호와 새 비밀번호 확인이 일치하지 않습니다.\n다시 확인하여 주시기 바랍니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Form_Adminpw.txtNewPW.Focus();
                    Form_Adminpw.txtNewPW.SelectAll();
                    return;
                }

                string pQuery1 = string.Format("EXEC _SAdminPW '{0}', '2'", Form_Adminpw.txtNewPW.Text);
                Dblink.ModifyMethod(pQuery1);

                mainDS.Clear();
                Form_Adminpw.Text = string.Empty;
                Form_Adminpw.Text = string.Empty;
                Form_Adminpw.Text = string.Empty;

                MessageBox.Show("저장이 완료되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Form_Adminpw.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FA201", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}
