using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelayTester
{
    public class FormSetErrorCodeWork
    {
        public FormSetErrorCode Form_ErrorCode;

        DbLink Dblink = new DbLink();
        public DataSet mainDS = new DataSet();

        public FormSetErrorCodeWork(FormSetErrorCode form) 
        {
            Form_ErrorCode = form;
        }

        public void QueryClick()
        {
            Dgv_ret_DataLoad();
        } //조회버튼 클릭 이벤트

        public void NewClick()
        {
            try
            {
                string errCode = Form_ErrorCode.txtErrCode.Text;
                string errType = Form_ErrorCode.txtErrType.Text;
                bool flag = true;
                int rowcnt = 0;

                string dgv_errCode = string.Empty;
                string dgv_errType = string.Empty;

                if (Form_ErrorCode.txtErrCode.Text.Trim().Length != 0 && Form_ErrorCode.txtErrType.Text.Trim().Length != 0) // textbox 입력여부 판단
                {
                    // 하단에 조회된 데이터중에 입력데이터와 중복되는 데이터가 있는지 확인하고 이후에 db에 인서트

                    while (flag)
                    {
                        dgv_errCode = Form_ErrorCode.dgv_ret.Rows[rowcnt].Cells[0].Value.ToString();
                        dgv_errType = Form_ErrorCode.dgv_ret.Rows[rowcnt].Cells[1].Value.ToString();

                        if (dgv_errCode == errCode && dgv_errType == errType)
                        {
                            MessageBox.Show("중복되는 조치코드 내용이 존재합니다.\n 다시 확인해주세요!");
                            flag = false;
                            break;
                        }
                        if (rowcnt < Form_ErrorCode.dgv_ret.RowCount - 1)
                        {
                            rowcnt++;
                        }
                        else
                            break;
                    }


                    if (flag) //DB 연결하여 데이터 INSERT
                    {

                        Dblink.ModifyMethod("EXEC _FErrorCode '" + errCode + "', '" + errType + "', '0', 'New'");
                        Dgv_ret_DataLoad();
                        Form_ErrorCode.txtErrType.Clear();
                        Form_ErrorCode.txtErrCode.Clear();
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
                if (Form_ErrorCode.dgv_ret.SelectedRows.Count > 0)
                {
                    string dgv_errCode = Form_ErrorCode.dgv_ret.SelectedRows[0].Cells[0].Value.ToString();
                    string dgv_errType = Form_ErrorCode.dgv_ret.SelectedRows[0].Cells[1].Value.ToString();

                    Dblink.ModifyMethod("EXEC _FErrorCode '" + dgv_errCode + "', '" + dgv_errType + "','" + 0 + "',  'Delete'");
                    Dgv_ret_DataLoad();
                }

            }
            catch (Exception ex)
            {

            }
        } //삭제버튼 클릭 이벤트

        public void Dgv_ret_DataLoad()
        {
            try
            {

                mainDS.Clear();
                Form_ErrorCode.dgv_ret.DataSource = null;
                Form_ErrorCode.dgv_ret.Rows.Clear();

                string query = "EXEC _FErrorCode '','','','Select'";
                Dblink.AllSelect(query, mainDS);

                for (int i = 0; i < mainDS.Tables[0].Rows.Count; i++)
                {
                    Form_ErrorCode.dgv_ret.Rows.Add();

                    Form_ErrorCode.dgv_ret.Rows[i].Cells[0].Value = mainDS.Tables[0].Rows[i]["Err_Code_Type"].ToString();
                    Form_ErrorCode.dgv_ret.Rows[i].Cells[1].Value = mainDS.Tables[0].Rows[i]["Err_Code_Name"].ToString();
                }

                Form_ErrorCode.dgv_ret.ColumnHeadersHeight = 61;

            }
            catch (Exception ex)
            {
                MessageBox.Show("DGV 로드 오류");
            }
        } //데이터 조회
    }
}
