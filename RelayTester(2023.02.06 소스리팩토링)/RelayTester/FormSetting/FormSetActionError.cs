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
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace RelayTester
{
    public partial class FormSetActionError : Form
    {
        DbLink Dblink = new DbLink();
        public DataTable mainDS = new DataTable();

        public FormSetActionError()
        {
            InitializeComponent();
        }

        private void FormActionError_Load(object sender, EventArgs e)
        {
            ErrorDataLoad();
        }



        private void FormActionError_Shown(object sender, EventArgs e)
        {
            this.cmb_ErrorCode.SelectedIndex = 0;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Dgv_ret_DataLoad();
        }

        public DataTable DBconnection(string procedure)
        {
            DataSet ds = new DataSet();
            Dblink.AllSelect(procedure, ds);

            return ds.Tables[0];
        }

        public void ErrorDataLoad()
        {
            try
            {

                this.cmb_ErrorCode.DataSource = DBconnection("EXEC _FActionError '','','','ActComb'");
                this.cmb_ErrorCode.DisplayMember = "Err_Code_Name";
                this.cmb_ErrorCode.ValueMember = "Err_Code_Type";

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
                mainDS = DBconnection("EXEC _FActionError '','','','Select'" );

                for (int i = 0; i < mainDS.Rows.Count; i++)
                {
                    dgv_ret.Rows.Add();


                    if (i > 0 && dgv_ret.Rows[i - 1].Cells[0].Value.ToString() == mainDS.Rows[i]["ecodename"].ToString())
                    {
                        dgv_ret.Rows[i].Cells[0].Value = mainDS.Rows[i]["ecodename"].ToString();
                        //dgv_ret.Rows[i].Cells[0].Style.ForeColor = Color.White;

                    }
                    else
                    {
                        dgv_ret.Rows[i].Cells[0].Value = mainDS.Rows[i]["ecodename"].ToString();
                    }
                    dgv_ret.Rows[i].Cells[1].Value = mainDS.Rows[i]["acodetype"].ToString();
                    dgv_ret.Rows[i].Cells[2].Value = mainDS.Rows[i]["acodename"].ToString();

                }

                dgv_ret.ColumnHeadersHeight = 61;

            }
            catch (Exception ex)
            {
                MessageBox.Show("DGV 로드 오류");
            }
        }

        bool IsTheSameCellValue(int column, int row)
        {
            DataGridViewCell cell1 = dgv_ret[column, row];
            DataGridViewCell cell2 = dgv_ret[column, row - 1];

            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }

            return cell1.Value.ToString() == cell2.Value.ToString();
        }

        private void dgv_ret_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;

            if (e.RowIndex < 1 || e.ColumnIndex < 0)
            {
                return;
            }

            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Top = dgv_ret.AdvancedCellBorderStyle.Top;
            }
        }

        private void dgv_ret_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0)
                return;

            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                string errCode = cmb_ErrorCode.Text;
                string actCode = txtActCode.Text;
                string actType = txtActType.Text;
                bool flag = true;
                int rowcnt = 0;

                string dgv_errCode = string.Empty;
                string dgv_actcode = string.Empty;
                string dgv_actType = string.Empty;

                if (txtActCode.Text.Trim().Length != 0 && txtActType.Text.Trim().Length != 0) // textbox 입력여부 판단
                {
                    // 하단에 조회된 데이터중에 입력데이터와 중복되는 데이터가 있는지 확인하고 이후에 db에 인서트

                    while(flag)
                    {
                        dgv_errCode = dgv_ret.Rows[rowcnt].Cells[0].Value.ToString();
                        dgv_actcode = dgv_ret.Rows[rowcnt].Cells[1].Value.ToString();
                        dgv_actType = dgv_ret.Rows[rowcnt].Cells[2].Value.ToString();

                        if (dgv_errCode == errCode && dgv_actcode == actCode && dgv_actType == actType)
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

                    if(flag) //DB 연결하여 데이터 INSERT
                    {

                        Dblink.ModifyMethod("EXEC _FActionError '" + actCode + "', '" + actType + "', '" + errCode + "', 'New'");
                        Dgv_ret_DataLoad();
                        txtActType.Clear();
                        txtActCode.Clear();
                    }
                    
                    
                }
                else
                    MessageBox.Show("입력되지 않은 정보가 있습니다.\n 다시 확인해주세요!");
            }
            catch(Exception ex)
            {

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgv_ret.SelectedRows.Count > 0)
                {
                    string dgv_errCode = dgv_ret.SelectedRows[0].Cells[0].Value.ToString();
                    string dgv_actcode = dgv_ret.SelectedRows[0].Cells[1].Value.ToString();
                    string dgv_actType = dgv_ret.SelectedRows[0].Cells[2].Value.ToString();

                    Dblink.ModifyMethod("EXEC _FActionError '" + dgv_actcode + "', '" + dgv_actType + "','" + dgv_errCode + "',  'Delete'");
                    Dgv_ret_DataLoad();
                }

            }
            catch(Exception ex)
            {

            }
        }
    }
}