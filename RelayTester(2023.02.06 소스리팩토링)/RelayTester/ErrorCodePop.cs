using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelayTester
{
    public class ErrorCodePop //2022.12.22 Create
    {
        DbLink Dblink = new DbLink();
        public DataTable mainDS = new DataTable();
        
        FormErrorCodePop FORM_Error;
        FormErrorCodePopCon FORM_Error2;

        FormContectResi Form_Contect_Resi;

        

        /*DataGridView dgvCurrRst = new DataGridView();
        DataGridView dgvResiRst = new DataGridView();
        DataGridView dgvRNTRst = new DataGridView();
        DataGridView dgvNRTRst = new DataGridView();*/

        DataTable ds = new DataTable();

        string SeqCode = string.Empty;
       

       public ErrorCodePop(FormContectResi form)
        {
            Form_Contect_Resi = form;
            //FORM_Error2 = new FormErrorCodePop2(Form_Contect_Resi);
        }
        
        public ErrorCodePop(FormErrorCodePop form)
        {
            FORM_Error = form;
        }
        
        

        
        public static Boolean CheckError(DataGridView dgvCurrRst, DataGridView dgvResiRst, DataGridView dgvRNTRst, DataGridView dgvNRTRst)
        {
            bool flag = false;

            for (int i = 0; i < dgvCurrRst.RowCount; i++)
            {

                if (dgvCurrRst.Rows[i].Cells[3].Style.BackColor == Color.Red)
                {
                    return true;
                }
            }

            for (int i = 0; i < dgvResiRst.RowCount; i++)
            {

                if (dgvResiRst.Rows[i].Cells[2].Style.BackColor == Color.Red)
                {
                    return true;
                }
            }

            for (int i = 0; i < dgvRNTRst.RowCount; i++)
            {

                if (dgvRNTRst.Rows[i].Cells[2].Style.BackColor == Color.Red)
                {
                    return true;
                }
            }

            for (int i = 0; i < dgvNRTRst.RowCount; i++)
            {

                if (dgvNRTRst.Rows[i].Cells[2].Style.BackColor == Color.Red)
                {
                    return true;
                }
            }

            return false;
        }

        public void CheckErrorContectResis(string seq) //2023.01.02 Create
        {
            try
            {
                bool flag = false;
                
                SeqCode = seq;

                foreach (Control ctl in Form_Contect_Resi.grbResult.Controls)
                {
                    if (ctl.GetType().ToString().Equals("System.Windows.Forms.TextBox") && ctl.BackColor.Equals(Color.Red))
                    {
                        UpdateContectResistError(seq, ctl);

                        flag = true;
                    }

                }

                if (flag)
                {
                    FORM_Error2 = new FormErrorCodePopCon(Form_Contect_Resi);
                    FORM_Error2.ShowDialog();
                }
            }
            catch(Exception ex)
            {

            }
           
        }

        public void LoadDataError(string testerNum, string testNum)
        {
            string serialNum = string.Empty;
            LoadErroCode(testNum, testerNum);
           
        }

        public void LoadErroCode(string testNum, string testerNum)
        {
            try
            {
                mainDS.Clear();
                FORM_Error.dgv_error.DataSource = null;
                FORM_Error.dgv_error.Rows.Clear();
                mainDS = ConnectionDB(string.Format("EXEC _FErrorCodePop {0}, '{1}' ", testNum, testerNum));

                for (int i = 0; i < mainDS.Rows.Count; i++)
                {
                    FORM_Error.dgv_error.Rows.Add();

                    string errorCode = null;

                    FORM_Error.dgv_error.Rows[i].Cells[0].Value = mainDS.Rows[i][0].ToString();
                    FORM_Error.dgv_error.Rows[i].Cells[1].Value = mainDS.Rows[i][1].ToString();
                    FORM_Error.dgv_error.Rows[i].Cells[2].Value = mainDS.Rows[i][2].ToString();
                    errorCode = mainDS.Rows[i][2].ToString();
                    DataGridViewComboBoxCell dgvCb = (DataGridViewComboBoxCell)FORM_Error.dgv_error.Rows[i].Cells[3];
                    LoadActionCode(dgvCb, errorCode);
                    FORM_Error.dgv_error.Rows[i].Cells[4].Value = mainDS.Rows[i][3].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("DGV 로드 오류");
            }
        }

        public void LoadErroCodeContectResis()
        {
            try
            {
                mainDS.Clear();
                FORM_Error2.dgv_error.DataSource = null;
                FORM_Error2.dgv_error.Rows.Clear();
                
                string query= "EXEC _FErrorCodePop2 '" + SeqCode + "' ";
                DataSet ds = new DataSet();
                Dblink.AllSelect(query, ds);
                mainDS = ds.Tables[0];
                for (int i = 0; i < mainDS.Rows.Count; i++)
                {
                    FORM_Error2.dgv_error.Rows.Add();

                    string errorCode = null;

                    FORM_Error2.dgv_error.Rows[i].Cells[0].Value = mainDS.Rows[i][0].ToString();
                    FORM_Error2.dgv_error.Rows[i].Cells[1].Value = mainDS.Rows[i][1].ToString();
                    errorCode = mainDS.Rows[i][1].ToString();
                    DataGridViewComboBoxCell dgvCb = (DataGridViewComboBoxCell)FORM_Error2.dgv_error.Rows[i].Cells[2];
                    LoadActionCode(dgvCb, errorCode);
                    FORM_Error2.dgv_error.Rows[i].Cells[3].Value = mainDS.Rows[i][2].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("DGV 로드 오류");
            }
        }


        public void LoadActionCode(DataGridViewComboBoxCell dgvCb, string errorCode)
        {

            try
            {
                
                ds = ConnectionDB("EXEC _FActionError '','','" + errorCode + "','ResultActComb'");

                for(int i=0; i<ds.Rows.Count; i++)
                {
                    dgvCb.Items.Add(ds.Rows[i][0]);
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void UpdateActCode()
        {
            try
            {
                for (int i = 0; i < FORM_Error.dgv_error.Rows.Count; i++)
                {
                    string query = "EXEC _FErrorCodePopActUpdate '" + FORM_Error.dgv_error.Rows[i].Cells[4].Value.ToString() + "','" + FORM_Error.dgv_error.Rows[i].Cells[3].Value.ToString() + "' ";
                    Dblink.DBupdate(query);
                }
            }
            catch (Exception ex)
            {

            }
            
        }

        public void UpdateActCodeContectResis() //2023.01.02 Create
        {
            try
            {
                string seq = null;
                string errorcode = null;
                string actcode = null;
                for (int i = 0; i < FORM_Error2.dgv_error.Rows.Count; i++)
                {
                    seq = FORM_Error2.dgv_error.Rows[i].Cells[3].Value.ToString();
                    errorcode = FORM_Error2.dgv_error.Rows[i].Cells[1].Value.ToString();
                    actcode = FORM_Error2.dgv_error.Rows[i].Cells[2].Value.ToString();

                    string query = "EXEC _FErrorCodePopActUpdate2 '" + seq + "','" + actcode + "','" + errorcode + "'";
                    Dblink.DBupdate(query);
                }
            }
            catch (Exception ex)
            {

            }

        }

        public DataTable ConnectionDB(string procedure)
        {
            DataSet ds = new DataSet();
            Dblink.AllSelect(procedure, ds);

            return ds.Tables[0];
        }

        public void UpdateContectResistError(string seq, Control ctl)
        {
            string query = string.Empty;
            string RVal = string.Empty;

            string txtBoxName = ctl.Name.Substring(3);
            RVal = txtBoxName + " 오류";
            query = string.Format("EXEC _FJobDtlInsert2 '{0}','{1}'", seq, RVal);

            Dblink.DBupdate(query);
        }
    }
}

