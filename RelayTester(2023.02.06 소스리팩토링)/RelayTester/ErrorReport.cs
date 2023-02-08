using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace RelayTester
{
    public class ErrorReport //2022.12.29 Create
    {
        DataTable tmpDt;

        FormErrorReport errorReport = null;

        public DataSet ds;
        DbLink Dblink = new DbLink();
        public ErrorReport(FormErrorReport form) 
        { 
            errorReport= form;
        }
        public void LoadSelectData() //조회 버튼 이벤트
        {
            errorReport.dgvQueryResult.DataSource = TmpTableErrorReport();

            errorReport.dgvQueryResult.Columns[5].Visible = false; //시험기
            errorReport.dgvQueryResult.Columns[6].Visible = false; //계전기유형
            errorReport.dgvQueryResult.Columns[7].Visible = false; //Lot번호
            errorReport.dgvQueryResult.Columns[8].Visible = false; //시험번호
            errorReport.dgvQueryResult.Columns[9].Visible = false; //Seq
        }

        private DataTable TmpTableErrorReport()
        {
            tmpDt = new DataTable("TmpTableErrorReport");
            string pQuery = string.Empty;
            DateTime dt = errorReport.dtpdate.Value;
            string date = dt.ToString("yyyyMMdd");

            pQuery = string.Format("EXEC _FErrorReportInquery '" + date + "'");

            tmpDt = LoadCmb(pQuery);

            return tmpDt;
        }

        public DataTable LoadCmb(string query)
        {
            ds = new DataSet();
            Dblink.AllSelect(query, ds);
            return ds.Tables[0];
        }

        public void LoadTesterCmb() //시험기 콤보박스 로드(1번,2번 등)
        {
            string pQuery = string.Empty;

            pQuery = "EXEC _FErrorReport '','','','','','TesterNum'";

            errorReport.cmbTester.DataSource = LoadCmb(pQuery);
            errorReport.cmbTester.DisplayMember = "Code_Dtl_Name";
            errorReport.cmbTester.ValueMember = "Code_Dtl";
            
            errorReport.cmbTester.SelectedIndex = 0;
        }

        public void LoadRelayCmb() //계전기 콤보박스 로드(무극, 유극 등)
        {
            string pQuery = string.Empty;
            pQuery = "EXEC _FErrorReport '','','','','','RelayType'";
            errorReport.cmbRelay.DataSource = LoadCmb(pQuery);
            errorReport.cmbRelay.DisplayMember = "Code_Dtl_Name";
            errorReport.cmbRelay.ValueMember = "Code_Dtl";

            errorReport.cmbRelay.SelectedIndex = 0;
        }

        public void LoadTestNum() //검사번호 콤보박스 로드
        {
            if(errorReport.chkTestNum.Checked)
            {
                string pQuery = string.Empty;
                string testerNum = int.Parse(errorReport.cmbTester.SelectedValue.ToString()).ToString();

                string relayNum = errorReport.cmbRelay.SelectedValue.ToString();

                DateTime dt = errorReport.dtpdate.Value;
                string testDate = dt.ToString("yyyyMMdd");

                string lotNum = null;

                if (errorReport.chkLot.Checked)
                {
                    lotNum = errorReport.mtxtLot.Text.Replace("-", "");
                    pQuery = "EXEC _FErrorReport '" + testerNum + "','" + relayNum + "','" + testDate + "','" + lotNum + "','','TestNum_Lot'";
                }
                else
                {
                    pQuery = "EXEC _FErrorReport '" + testerNum + "','" + relayNum + "','" + testDate + "','','','TestNum'";
                }

                errorReport.cmbTestNum.DataSource = LoadCmb(pQuery);
                errorReport.cmbTestNum.DisplayMember = "TestNum";
                //errorReport.cmbTestNum.ValueMember = "Code_Dtl";
            }

        }

        public void LoadLotData()
        {
            try
            {
                FormReportLotPop lp = new FormReportLotPop();

                lp.sRelayType = this.errorReport.cmbRelay.SelectedValue.ToString();

                if (lp.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    errorReport.mtxtLot.Text = lp.sSeq;
                    errorReport.cmbRelay.SelectedValue = lp.sType;
                    //errorReport.chkLot.Checked = true;
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public void RadioButtonAllChk() //전체선택 시 올 체크, 개별선택 시 올 클리어
        {
            if(errorReport.rbt_all.Checked)
            {
                errorReport.chk_ContectResi.Checked = true;
                errorReport.chk_OpenCurrent.Checked = true;
                errorReport.chk_DropCurrent.Checked = true;
                errorReport.chk_Coil.Checked = true;
                errorReport.chk_OpenTime.Checked = true;
                errorReport.chk_ReturnTime.Checked = true;

                errorReport.chk_ContectResi.Enabled = false;
                errorReport.chk_OpenCurrent.Enabled= false;
                errorReport.chk_DropCurrent.Enabled = false;
                errorReport.chk_Coil.Enabled = false;
                errorReport.chk_OpenTime.Enabled = false;
                errorReport.chk_ReturnTime.Enabled = false;
            }
            
        }

        public void RadioButtonOneChk() //개별선택 시 올 체크, 개별선택 시 올 클리어
        {
            if (errorReport.rbt_one.Checked)
            {
                errorReport.chk_ContectResi.Checked = false;
                errorReport.chk_OpenCurrent.Checked = false;
                errorReport.chk_DropCurrent.Checked = false;
                errorReport.chk_Coil.Checked = false;
                errorReport.chk_OpenTime.Checked = false;
                errorReport.chk_ReturnTime.Checked = false;

                errorReport.chk_ContectResi.Enabled = true;
                errorReport.chk_OpenCurrent.Enabled = true;
                errorReport.chk_DropCurrent.Enabled = true;
                errorReport.chk_Coil.Enabled = true;
                errorReport.chk_OpenTime.Enabled = true;
                errorReport.chk_ReturnTime.Enabled = true;
            }
        }

        public void SelectResult()
        {
            errorReport.dgvQueryResult.DataSource= null;    

            DataTable dt = new DataTable();
            dt = tmpDt;
            dt = SelectTester(dt);
            dt = SelectRelay(dt);
            dt = SelectLot(dt);

            if(errorReport.cmbTester.SelectedIndex == 0 && errorReport.cmbRelay.SelectedIndex == 0)
            {
                LoadSelectData();
                //errorReport.dgvQueryResult.DataSource = TmpTableErrorReport();
            }
            else
            {
                errorReport.dgvQueryResult.DataSource = dt;

                if(dt != null)
                {
                    errorReport.dgvQueryResult.Columns[5].Visible = false; //시험기
                    errorReport.dgvQueryResult.Columns[6].Visible = false; //계전기유형
                    errorReport.dgvQueryResult.Columns[7].Visible = false; //Lot번호
                    errorReport.dgvQueryResult.Columns[8].Visible = false; //시험번호
                    errorReport.dgvQueryResult.Columns[9].Visible = false; //Seq
                }
                
            }
        }

        public DataTable SelectTester(DataTable result) //시험기 선택 필터
        {
           DataTable dt = new DataTable();  

            
            if (errorReport.cmbTester.SelectedIndex == 1) //1번
            {
                result.DefaultView.RowFilter = "시험기 = '1'";
                dt = result.DefaultView.ToTable();
            }
            else if (errorReport.cmbTester.SelectedIndex == 2) //2번
            {
                result.DefaultView.RowFilter = "시험기 = '2'";
                dt = result.DefaultView.ToTable();
            }
            else if (errorReport.cmbTester.SelectedIndex == 3) //3번
            {
                result.DefaultView.RowFilter = "시험기 = '3'";
                dt = result.DefaultView.ToTable();
            }
            else if(errorReport.cmbTester.SelectedIndex == 4)
            {
                result.DefaultView.RowFilter = "시험기 = '5'";
                dt = result.DefaultView.ToTable();
            }
            
            else 
            {
                
                dt = result;    
            }

            return dt;
        }


        public DataTable SelectRelay(DataTable result) //계전기 종류 선택 필터 
        {
            DataTable dt = new DataTable();


            string RelayType = errorReport.cmbRelay.Text;

            if (RelayType != "전체" && result != null && result.Rows.Count > 0) 
            {
                result.DefaultView.RowFilter = "계전기유형 = '" + RelayType + "'";
                dt = result.DefaultView.ToTable();
            }
            else
            {
                dt = result;
            } 

            return dt;

        }

        public DataTable SelectLot(DataTable result)
        {
            DataTable dt = new DataTable();

            string lot = errorReport.mtxtLot.Text.Trim().Replace("-","").ToString();
            bool chk = errorReport.chkLot.Checked;

            if (lot.Length > 0 && result != null && result.Rows.Count > 0 && chk != false ) 
            {
                result.DefaultView.RowFilter = "Lot번호 = '" + lot + "'";
                dt = result.DefaultView.ToTable();
            }
            else
            {
                dt = result;
            }

            return dt;
        }

    }
}
