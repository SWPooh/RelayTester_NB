using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelayTester
{
    public partial class FormSetErrorCode : Form
    {
        DbLink Dblink = new DbLink();
        public DataTable mainDS = new DataTable();
        public FormSetErrorCode()
        {
            InitializeComponent();
        }

        private void FormErrorCode_Load(object sender, EventArgs e)
        {
            Dgv_ret_DataLoad();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Dgv_ret_DataLoad();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {

            try
            {
                string errCode = txtErrCode.Text;
                string errType = txtErrType.Text;
                bool flag = true;
                int rowcnt = 0;

                string dgv_errCode = string.Empty;
                string dgv_errType = string.Empty;

                if (txtErrCode.Text.Trim().Length != 0 && txtErrType.Text.Trim().Length != 0) // textbox 입력여부 판단
                {
                    // 하단에 조회된 데이터중에 입력데이터와 중복되는 데이터가 있는지 확인하고 이후에 db에 인서트

                    while (flag)
                    {
                        dgv_errCode = dgv_ret.Rows[rowcnt].Cells[0].Value.ToString();
                        dgv_errType = dgv_ret.Rows[rowcnt].Cells[1].Value.ToString();

                        if (dgv_errCode == errCode && dgv_errType == errType)
                        {
                            MessageBox.Show("중복되는 조치코드 내용이 존재합니다.\n 다시 확인해주세요!");
                            flag = false;
                            break;
                        }
                        if (rowcnt < dgv_ret.RowCount - 1)
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
                        txtErrType.Clear();
                        txtErrCode.Clear();
                    }


                }
                else
                    MessageBox.Show("입력되지 않은 정보가 있습니다.\n 다시 확인해주세요!");
            }
            catch (Exception ex)
            {

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv_ret.SelectedRows.Count > 0)
                {
                    string dgv_errCode = dgv_ret.SelectedRows[0].Cells[0].Value.ToString();
                    string dgv_errType = dgv_ret.SelectedRows[0].Cells[1].Value.ToString();

                    Dblink.ModifyMethod("EXEC _FErrorCode '" + dgv_errCode + "', '" + dgv_errType + "','" + 0 + "',  'Delete'");
                    Dgv_ret_DataLoad();
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void Dgv_ret_DataLoad()
        {
            try
            {

                mainDS.Clear();
                dgv_ret.DataSource = null;
                dgv_ret.Rows.Clear();
                mainDS = DBconnection("EXEC _FErrorCode '','','','Select'");

                for (int i = 0; i < mainDS.Rows.Count; i++)
                {
                    dgv_ret.Rows.Add();

                    dgv_ret.Rows[i].Cells[0].Value = mainDS.Rows[i]["Err_Code_Type"].ToString();
                    dgv_ret.Rows[i].Cells[1].Value = mainDS.Rows[i]["Err_Code_Name"].ToString();
                }

                dgv_ret.ColumnHeadersHeight = 61;

            }
            catch (Exception ex)
            {
                MessageBox.Show("DGV 로드 오류");
            }
        }

        public DataTable DBconnection(string procedure)
        {
            DataSet ds = new DataSet();
            Dblink.AllSelect(procedure, ds);

            return ds.Tables[0];
        }

        
    }
}
