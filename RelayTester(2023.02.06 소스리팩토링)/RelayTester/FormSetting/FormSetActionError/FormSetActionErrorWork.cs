using RelayTester.CommonProcess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelayTester
{
    public class FormSetActionErrorWork
    {
        public FormSetActionError Form_ActionError;

        DbLink Dblink = new DbLink();
        public DataSet mainDS = new DataSet();

        public FormSetActionErrorWork(FormSetActionError form) 
        {
            Form_ActionError = form;
        }

        public void QueryClick()
        {
            Dgv_ret_DataLoad();
        } //조회버튼 클릭 이벤트

        public void NewClick()
        {
            try
            {
                string errCode = Form_ActionError.cmb_ErrorCode.Text;
                string actCode = Form_ActionError.txtActCode.Text;
                string actType = Form_ActionError.txtActType.Text;
                bool flag = true;
                int rowcnt = 0;

                string dgv_errCode = string.Empty;
                string dgv_actcode = string.Empty;
                string dgv_actType = string.Empty;

                if (Form_ActionError.txtActCode.Text.Trim().Length != 0 && Form_ActionError.txtActType.Text.Trim().Length != 0) // textbox 입력여부 판단
                {
                    // 하단에 조회된 데이터중에 입력데이터와 중복되는 데이터가 있는지 확인하고 이후에 db에 인서트

                    while (flag)
                    {
                        dgv_errCode = Form_ActionError.dgv_ret.Rows[rowcnt].Cells[0].Value.ToString();
                        dgv_actcode = Form_ActionError.dgv_ret.Rows[rowcnt].Cells[1].Value.ToString();
                        dgv_actType = Form_ActionError.dgv_ret.Rows[rowcnt].Cells[2].Value.ToString();

                        if (dgv_errCode == errCode && dgv_actcode == actCode && dgv_actType == actType)
                        {
                            MessageBox.Show("중복되는 조치코드 내용이 존재합니다.\n 다시 확인해주세요!");
                            flag = false;
                            break;
                        }

                        if (rowcnt < Form_ActionError.dgv_ret.RowCount - 1)
                        {
                            rowcnt++;
                        }
                        else
                            break;



                    }

                    if (flag) //DB 연결하여 데이터 INSERT
                    {

                        Dblink.ModifyMethod("EXEC _FActionError '" + actCode + "', '" + actType + "', '" + errCode + "', 'New'");
                        Dgv_ret_DataLoad();
                        Form_ActionError.txtActType.Clear();
                        Form_ActionError.txtActCode.Clear();
                    }


                }
                else
                    MessageBox.Show("입력되지 않은 정보가 있습니다.\n 다시 확인해주세요!");
            }
            catch (Exception ex)
            {

            }
        } //추가버튼 클릭 이벤트

        public void DeleteClick()
        {
            try
            {
                if (Form_ActionError.dgv_ret.SelectedRows.Count > 0)
                {
                    string dgv_errCode = Form_ActionError.dgv_ret.SelectedRows[0].Cells[0].Value.ToString();
                    string dgv_actcode = Form_ActionError.dgv_ret.SelectedRows[0].Cells[1].Value.ToString();
                    string dgv_actType = Form_ActionError.dgv_ret.SelectedRows[0].Cells[2].Value.ToString();

                    Dblink.ModifyMethod("EXEC _FActionError '" + dgv_actcode + "', '" + dgv_actType + "','" + dgv_errCode + "',  'Delete'");
                    Dgv_ret_DataLoad();
                }

            }
            catch (Exception ex)
            {

            }
        } //삭제버튼 클릭 이벤트

        public void ErrorDataLoad()
        {
            try
            {
                string query = "EXEC _FActionError '','','','ActComb'";
                Dblink.AllSelect(query, mainDS);

                Form_ActionError.cmb_ErrorCode.DataSource = mainDS.Tables[0];
                Form_ActionError.cmb_ErrorCode.DisplayMember = "Err_Code_Name";
                Form_ActionError.cmb_ErrorCode.ValueMember = "Err_Code_Type";

            }
            catch (Exception ex)
            {

            }
        } //콤보박스 데이터 조회

        public void Dgv_ret_DataLoad()
        {
            try
            {

                mainDS.Clear();
                Form_ActionError.dgv_ret.DataSource = null;
                Form_ActionError.dgv_ret.Rows.Clear();

                string query = "EXEC _FActionError '','','','Select'";
                Dblink.AllSelect(query, mainDS);

                for (int i = 0; i < mainDS.Tables[0].Rows.Count; i++)
                {
                    Form_ActionError.dgv_ret.Rows.Add();


                    if (i > 0 && Form_ActionError.dgv_ret.Rows[i - 1].Cells[0].Value.ToString() == mainDS.Tables[0].Rows[i]["ecodename"].ToString())
                    {
                        Form_ActionError.dgv_ret.Rows[i].Cells[0].Value = mainDS.Tables[0].Rows[i]["ecodename"].ToString();
                        //dgv_ret.Rows[i].Cells[0].Style.ForeColor = Color.White;

                    }
                    else
                    {
                        Form_ActionError.dgv_ret.Rows[i].Cells[0].Value = mainDS.Tables[0].Rows[i]["ecodename"].ToString();
                    }
                    Form_ActionError.dgv_ret.Rows[i].Cells[1].Value = mainDS.Tables[0].Rows[i]["acodetype"].ToString();
                    Form_ActionError.dgv_ret.Rows[i].Cells[2].Value = mainDS.Tables[0].Rows[i]["acodename"].ToString();

                }

                Form_ActionError.dgv_ret.ColumnHeadersHeight = 61;

            }
            catch (Exception ex)
            {
                MessageBox.Show("DGV 로드 오류");
            }
        } //데이터 조회
        

    }
}
