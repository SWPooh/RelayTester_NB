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
    public partial class FormSelectResult : Form
    {
        public DataSet RelayDS = new DataSet();
        public DataSet ContectDS = new DataSet();
        public DataSet CurrDS = new DataSet();
        public DataSet ResiDS = new DataSet();
        public DataSet RNTDS = new DataSet();
        public DataSet NRTDS = new DataSet();
        DbLink Dblink = new DbLink();

        public FormSelectResult()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

        }

        private void FormSelectResult_Load(object sender, EventArgs e)
        {
            try
            {
                rbReport.Checked = true;

                this.cmbRelayTypeSearch.DataSource = EqType("EXEC _SEquipTypeQuery");
                this.cmbRelayTypeSearch.DisplayMember = "Code_Dtl_Name";
                this.cmbRelayTypeSearch.ValueMember = "Code_Dtl";
                this.cmbRelayTypeSearch.SelectedIndex = 0;

                mtxtLot.Select();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FSR101", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        public DataTable EqType(string sQuery)
        {
            DataSet ds = new DataSet();
            Dblink.AllSelect(sQuery, ds);
            return ds.Tables[0];
        }

        private void GridResetMethod(string sPalletId)
        {

            ////////////////컬럼명////////////////
            //계전기목록
            dgvRelayList.Columns["PalletId"].HeaderCell.Value = "계전기ID";
            //접촉저항
            dgvContect.Columns["ReportChk"].HeaderCell.Value = "성적서";
            dgvContect.Columns["RVal1"].HeaderCell.Value = "N1";
            dgvContect.Columns["RVal2"].HeaderCell.Value = "N2";
            dgvContect.Columns["RVal3"].HeaderCell.Value = "N3";
            dgvContect.Columns["RVal4"].HeaderCell.Value = "N4";
            if (sPalletId.Substring(0, 3) != "DR3")
            {
                dgvContect.Columns["RVal5"].HeaderCell.Value = "N5";
                dgvContect.Columns["RVal6"].HeaderCell.Value = "N6";
            }
            dgvContect.Columns["RVal7"].HeaderCell.Value = "N7";
            dgvContect.Columns["RVal8"].HeaderCell.Value = "N8";
            //dgvContect.Columns["RVal9"].HeaderCell.Value = "N9";
            //dgvContect.Columns["RVal10"].HeaderCell.Value = "N10";
            dgvContect.Columns["RVal11"].HeaderCell.Value = "R1";
            dgvContect.Columns["RVal12"].HeaderCell.Value = "R2";
            dgvContect.Columns["RVal13"].HeaderCell.Value = "R3";
            dgvContect.Columns["RVal14"].HeaderCell.Value = "R4";
            //dgvContect.Columns["RVal15"].HeaderCell.Value = "R5";
            //dgvContect.Columns["RVal16"].HeaderCell.Value = "R6";
            //dgvContect.Columns["RVal17"].HeaderCell.Value = "R7";
            //dgvContect.Columns["RVal18"].HeaderCell.Value = "R8";
            dgvContect.Columns["RVal19"].HeaderCell.Value = "R9";
            dgvContect.Columns["RVal20"].HeaderCell.Value = "R10";
            dgvContect.Columns["RcvTimeR"].HeaderCell.Value = "일시";
            //동작전류
            dgvCurr.Columns["ReportChk"].HeaderCell.Value = "성적서";
            dgvCurr.Columns["RVal1"].HeaderCell.Value = "동작전류";
            dgvCurr.Columns["RVal2"].HeaderCell.Value = "낙하전류";
            dgvCurr.Columns["RcvTimeR"].HeaderCell.Value = "일시";
            //코일저항
            dgvResi.Columns["ReportChk"].HeaderCell.Value = "성적서";
            dgvResi.Columns["RVal3"].HeaderCell.Value = "저항";
            dgvResi.Columns["RcvTimeR"].HeaderCell.Value = "일시";
            //동작시간
            dgvRNT.Columns["ReportChk"].HeaderCell.Value = "성적서";
            dgvRNT.Columns["RVal1"].HeaderCell.Value = "N1";
            dgvRNT.Columns["RVal2"].HeaderCell.Value = "N2";
            dgvRNT.Columns["RVal3"].HeaderCell.Value = "N3";
            dgvRNT.Columns["RVal4"].HeaderCell.Value = "N4";
            if (sPalletId.Substring(0, 3) != "DR3")
            {
                dgvRNT.Columns["RVal5"].HeaderCell.Value = "N5";
                dgvRNT.Columns["RVal6"].HeaderCell.Value = "N6";
            }
            dgvRNT.Columns["RVal7"].HeaderCell.Value = "N7";
            dgvRNT.Columns["RVal8"].HeaderCell.Value = "N8";
            //dgvRNT.Columns["RVal9"].HeaderCell.Value = "N9";
            //dgvRNT.Columns["RVal10"].HeaderCell.Value = "N10";
            dgvRNT.Columns["RcvTimeR"].HeaderCell.Value = "일시";
            //복구시간
            dgvNRT.Columns["ReportChk"].HeaderCell.Value = "성적서";
            dgvNRT.Columns["RVal1"].HeaderCell.Value = "R1";
            dgvNRT.Columns["RVal2"].HeaderCell.Value = "R2";
            dgvNRT.Columns["RVal3"].HeaderCell.Value = "R3";
            dgvNRT.Columns["RVal4"].HeaderCell.Value = "R4";
            //dgvNRT.Columns["RVal5"].HeaderCell.Value = "R5";
            //dgvNRT.Columns["RVal6"].HeaderCell.Value = "R6";
            //dgvNRT.Columns["RVal7"].HeaderCell.Value = "R7";
            //dgvNRT.Columns["RVal8"].HeaderCell.Value = "R8";
            dgvNRT.Columns["RVal9"].HeaderCell.Value = "R9";
            dgvNRT.Columns["RVal10"].HeaderCell.Value = "R10";
            dgvNRT.Columns["RcvTimeR"].HeaderCell.Value = "일시";

            ////////////////비지블////////////////
            dgvContect.Columns["Seq"].Visible = false;
            dgvCurr.Columns["Seq"].Visible = false;
            dgvResi.Columns["Seq"].Visible = false;
            dgvRNT.Columns["Seq"].Visible = false;
            dgvNRT.Columns["Seq"].Visible = false;

            ////////////////사이즈////////////////
            dgvRelayList.Columns["PalletId"].Width = 150;

            dgvContect.Columns["ReportChk"].Width = 70;
            dgvContect.Columns["RcvTimeR"].Width = 171;
            dgvContect.Columns["RVal1"].Width = 70;
            dgvContect.Columns["RVal2"].Width = 70;
            dgvContect.Columns["RVal3"].Width = 70;
            dgvContect.Columns["RVal4"].Width = 70;
            if (sPalletId.Substring(0, 3) != "DR3")
            {
                dgvContect.Columns["RVal5"].Width = 70;
                dgvContect.Columns["RVal6"].Width = 70;
            }
            dgvContect.Columns["RVal7"].Width = 70;
            dgvContect.Columns["RVal8"].Width = 70;
            //dgvContect.Columns["RVal9"].Width = 70;
            //dgvContect.Columns["RVal10"].Width = 70;
            dgvContect.Columns["RVal11"].Width = 70;
            dgvContect.Columns["RVal12"].Width = 70;
            dgvContect.Columns["RVal13"].Width = 70;
            dgvContect.Columns["RVal14"].Width = 70;
            //dgvContect.Columns["RVal15"].Width = 70;
            //dgvContect.Columns["RVal16"].Width = 70;
            //dgvContect.Columns["RVal17"].Width = 70;
            //dgvContect.Columns["RVal18"].Width = 70;
            dgvContect.Columns["RVal19"].Width = 70;
            dgvContect.Columns["RVal20"].Width = 70;

            dgvCurr.Columns["ReportChk"].Width = 70;
            dgvCurr.Columns["RcvTimeR"].Width = 171;

            dgvResi.Columns["ReportChk"].Width = 70;
            dgvResi.Columns["RcvTimeR"].Width = 171;

            dgvRNT.Columns["ReportChk"].Width = 70;
            dgvRNT.Columns["RVal1"].Width = 110;
            dgvRNT.Columns["RcvTimeR"].Width = 171;
            dgvRNT.Columns["RVal1"].Width = 70;
            dgvRNT.Columns["RVal2"].Width = 70;
            dgvRNT.Columns["RVal3"].Width = 70;
            dgvRNT.Columns["RVal4"].Width = 70;
            if (sPalletId.Substring(0, 3) != "DR3")
            {
                dgvRNT.Columns["RVal5"].Width = 70;
                dgvRNT.Columns["RVal6"].Width = 70;
            }
            dgvRNT.Columns["RVal7"].Width = 70;
            dgvRNT.Columns["RVal8"].Width = 70;
            //dgvRNT.Columns["RVal9"].Width = 70;
            //dgvRNT.Columns["RVal10"].Width = 70;

            dgvNRT.Columns["ReportChk"].Width = 70;
            dgvNRT.Columns["RVal1"].Width = 110;
            dgvNRT.Columns["RcvTimeR"].Width = 171;
            dgvNRT.Columns["RVal1"].Width = 70;
            dgvNRT.Columns["RVal2"].Width = 70;
            dgvNRT.Columns["RVal3"].Width = 70;
            dgvNRT.Columns["RVal4"].Width = 70;
            //dgvNRT.Columns["RVal5"].Width = 70;
            //dgvNRT.Columns["RVal6"].Width = 70;
            //dgvNRT.Columns["RVal7"].Width = 70;
            //dgvNRT.Columns["RVal8"].Width = 70;
            dgvNRT.Columns["RVal9"].Width = 70;
            dgvNRT.Columns["RVal10"].Width = 70;

            ////////////////읽기전용////////////////
            dgvContect.Columns["RVal1"].ReadOnly = true;
            dgvContect.Columns["RVal2"].ReadOnly = true;
            dgvContect.Columns["RVal3"].ReadOnly = true;
            dgvContect.Columns["RVal4"].ReadOnly = true;
            if (sPalletId.Substring(0, 3) != "DR3")
            {
                dgvContect.Columns["RVal5"].ReadOnly = true;
                dgvContect.Columns["RVal6"].ReadOnly = true;
            }
            dgvContect.Columns["RVal7"].ReadOnly = true;
            dgvContect.Columns["RVal8"].ReadOnly = true;
            //dgvContect.Columns["RVal9"].ReadOnly = true;
            //dgvContect.Columns["RVal10"].ReadOnly = true;
            dgvContect.Columns["RVal11"].ReadOnly = true;
            dgvContect.Columns["RVal12"].ReadOnly = true;
            dgvContect.Columns["RVal13"].ReadOnly = true;
            dgvContect.Columns["RVal14"].ReadOnly = true;
            //dgvContect.Columns["RVal15"].ReadOnly = true;
            //dgvContect.Columns["RVal16"].ReadOnly = true;
            //dgvContect.Columns["RVal17"].ReadOnly = true;
            //dgvContect.Columns["RVal18"].ReadOnly = true;
            dgvContect.Columns["RVal19"].ReadOnly = true;
            dgvContect.Columns["RVal20"].ReadOnly = true;
            dgvContect.Columns["RcvTimeR"].ReadOnly = true;

            dgvCurr.Columns["RVal1"].ReadOnly = true;
            dgvCurr.Columns["RVal2"].ReadOnly = true;
            dgvCurr.Columns["RcvTimeR"].ReadOnly = true;

            dgvResi.Columns["RVal3"].ReadOnly = true;
            dgvResi.Columns["RcvTimeR"].ReadOnly = true;

            dgvRNT.Columns["RVal1"].ReadOnly = true;
            dgvRNT.Columns["RVal2"].ReadOnly = true;
            dgvRNT.Columns["RVal3"].ReadOnly = true;
            dgvRNT.Columns["RVal4"].ReadOnly = true;
            if (sPalletId.Substring(0, 3) != "DR3")
            {
                dgvRNT.Columns["RVal5"].ReadOnly = true;
                dgvRNT.Columns["RVal6"].ReadOnly = true;
            }
            dgvRNT.Columns["RVal7"].ReadOnly = true;
            dgvRNT.Columns["RVal8"].ReadOnly = true;
            //dgvRNT.Columns["RVal9"].ReadOnly = true;
            //dgvRNT.Columns["RVal10"].ReadOnly = true;
            dgvRNT.Columns["RcvTimeR"].ReadOnly = true;

            dgvNRT.Columns["RVal1"].ReadOnly = true;
            dgvNRT.Columns["RVal2"].ReadOnly = true;
            dgvNRT.Columns["RVal3"].ReadOnly = true;
            dgvNRT.Columns["RVal4"].ReadOnly = true;
            //dgvNRT.Columns["RVal5"].ReadOnly = true;
            //dgvNRT.Columns["RVal6"].ReadOnly = true;
            //dgvNRT.Columns["RVal7"].ReadOnly = true;
            //dgvNRT.Columns["RVal8"].ReadOnly = true;
            dgvNRT.Columns["RVal9"].ReadOnly = true;
            dgvNRT.Columns["RVal10"].ReadOnly = true;
            dgvNRT.Columns["RcvTimeR"].ReadOnly = true;
            dgvCurr.Columns["RcvTimeR"].ReadOnly = true;
        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            mtxtLot.Text = "";
            cmbRelayTypeSearch.SelectedValue = "01";
            RelayDS.Clear();
            ContectDS.Clear();
            CurrDS.Clear();
            ResiDS.Clear();
            RNTDS.Clear();
            NRTDS.Clear();

            foreach (Control ctl in this.panel3.Controls)
            {
                if (ctl.GetType().ToString().Equals("System.Windows.Forms.TextBox"))
                {
                    ctl.Text = "";
                }
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                FormReportLotPop lp = new FormReportLotPop();

                lp.sRelayType = this.cmbRelayTypeSearch.SelectedValue.ToString();

                if (lp.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    btnNew_Click(null, null);
                    mtxtLot.Text = lp.sSeq;
                    cmbRelayTypeSearch.SelectedValue = lp.sType;

                    string pQuery = string.Empty;
                    pQuery = "EXEC _SSelectResultMasterQuery '" + lp.sSeq + "' , '" + lp.sType + "'";
                    RelayDS.Clear();
                    dgvRelayList.DataSource = null;


                    Dblink.AllSelect(pQuery, RelayDS);
                    dgvRelayList.DataSource = RelayDS.Tables[0];

                    if (RelayDS.Tables[0].Rows.Count > 0)
                    {
                        DataGridViewCellEventArgs cellIndex = new DataGridViewCellEventArgs(0, 0);
                        ResultInput(lp.sType);  // 시험 기준값 불러오기
                        dgvRelayList_CellClick(null, cellIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FSR102", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void ResultInput(string sType)
        {
            DataSet RefValDS = new DataSet();
            RefValDS.Clear();
            string pQuery = string.Empty;
            pQuery = "EXEC _SRefValQuery";
            Dblink.AllSelect(pQuery, RefValDS);

            if (sType == "01" || sType == "05" || sType == "06" || sType == "07" || sType == "09" || sType == "10" || sType == "11"    //무극, 무극-중부하, 무극(ABS), 무극(PGS), 무극-중부하(1-4), 무극-테크빌
#if(TRADD)
                || sType == "12" || sType == "13")    // 유극T, 자기유지T
#else
                )
#endif
            {
                txtCMin.Text = RefValDS.Tables[0].Rows[0]["CRNMin"].ToString();
                txtCMax.Text = RefValDS.Tables[0].Rows[0]["CRNMax"].ToString();
                txtCOMin.Text = RefValDS.Tables[0].Rows[0]["CNOMin"].ToString();
                txtCOMax.Text = RefValDS.Tables[0].Rows[0]["CNOMax"].ToString();
                txtCDMin.Text = RefValDS.Tables[0].Rows[0]["CNDMin"].ToString();
                txtCDMax.Text = RefValDS.Tables[0].Rows[0]["CNDMax"].ToString();
                txtRMin.Text = RefValDS.Tables[0].Rows[0]["RNMin"].ToString();
                txtRMax.Text = RefValDS.Tables[0].Rows[0]["RNMax"].ToString();
                txtTRNMin.Text = RefValDS.Tables[0].Rows[0]["TNRNMin"].ToString();
                txtTRNMax.Text = RefValDS.Tables[0].Rows[0]["TNRNMax"].ToString();
                txtTNRMin.Text = RefValDS.Tables[0].Rows[0]["TNNRMin"].ToString();
                txtTNRMax.Text = RefValDS.Tables[0].Rows[0]["TNNRMax"].ToString();
            }
            else if (sType == "02" || sType == "08"  //유극, 유극(PGS)

#if (TRADD)
                || sType == "12")    // 유극T
#else
                )
#endif
            {
                txtCMin.Text = RefValDS.Tables[0].Rows[0]["CRPMin"].ToString();
                txtCMax.Text = RefValDS.Tables[0].Rows[0]["CRPMax"].ToString();
                txtCOMin.Text = RefValDS.Tables[0].Rows[0]["CPOMin"].ToString();
                txtCOMax.Text = RefValDS.Tables[0].Rows[0]["CPOMax"].ToString();
                txtCDMin.Text = RefValDS.Tables[0].Rows[0]["CPDMin"].ToString();
                txtCDMax.Text = RefValDS.Tables[0].Rows[0]["CPDMax"].ToString();
                txtRMin.Text = RefValDS.Tables[0].Rows[0]["RPMin"].ToString();
                txtRMax.Text = RefValDS.Tables[0].Rows[0]["RPMax"].ToString();
                txtTRNMin.Text = RefValDS.Tables[0].Rows[0]["TPRNMin"].ToString();
                txtTRNMax.Text = RefValDS.Tables[0].Rows[0]["TPRNMax"].ToString();
                txtTNRMin.Text = RefValDS.Tables[0].Rows[0]["TPNRMin"].ToString();
                txtTNRMax.Text = RefValDS.Tables[0].Rows[0]["TPNRMax"].ToString();
            }
            else if (sType == "04")                                                      //유극(저전류)
            {
                txtCMin.Text = RefValDS.Tables[0].Rows[0]["CRPLMin"].ToString();
                txtCMax.Text = RefValDS.Tables[0].Rows[0]["CRPLMax"].ToString();
                txtCOMin.Text = RefValDS.Tables[0].Rows[0]["CPOLMin"].ToString();
                txtCOMax.Text = RefValDS.Tables[0].Rows[0]["CPOLMax"].ToString();
                txtCDMin.Text = RefValDS.Tables[0].Rows[0]["CPDLMin"].ToString();
                txtCDMax.Text = RefValDS.Tables[0].Rows[0]["CPDLMax"].ToString();
                txtRMin.Text = RefValDS.Tables[0].Rows[0]["RPLMin"].ToString();
                txtRMax.Text = RefValDS.Tables[0].Rows[0]["RPLMax"].ToString();
                txtTRNMin.Text = RefValDS.Tables[0].Rows[0]["TPRNLMin"].ToString();
                txtTRNMax.Text = RefValDS.Tables[0].Rows[0]["TPRNLMax"].ToString();
                txtTNRMin.Text = RefValDS.Tables[0].Rows[0]["TPNRLMin"].ToString();
                txtTNRMax.Text = RefValDS.Tables[0].Rows[0]["TPNRLMax"].ToString();
            }
            else if (sType == "03"         //자기유지
#if (TRADD)
                || sType == "13")    //자기유지T
#else
                )
#endif
            {
                txtCMin.Text = RefValDS.Tables[0].Rows[0]["CRSMin"].ToString();
                txtCMax.Text = RefValDS.Tables[0].Rows[0]["CRSMax"].ToString();
                txtCOMin.Text = RefValDS.Tables[0].Rows[0]["CSOMin"].ToString();
                txtCOMax.Text = RefValDS.Tables[0].Rows[0]["CSOMax"].ToString();
                txtCDMin.Text = RefValDS.Tables[0].Rows[0]["CSDMin"].ToString();
                txtCDMax.Text = RefValDS.Tables[0].Rows[0]["CSDMax"].ToString();
                txtRMin.Text = RefValDS.Tables[0].Rows[0]["RSMin"].ToString();
                txtRMax.Text = RefValDS.Tables[0].Rows[0]["RSMax"].ToString();
                txtTRNMin.Text = RefValDS.Tables[0].Rows[0]["TSRNMin"].ToString();
                txtTRNMax.Text = RefValDS.Tables[0].Rows[0]["TSRNMax"].ToString();
                txtTNRMin.Text = RefValDS.Tables[0].Rows[0]["TSNRMin"].ToString();
                txtTNRMax.Text = RefValDS.Tables[0].Rows[0]["TSNRMax"].ToString();
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                foreach (DataRow item in ContectDS.Tables[0].Rows)
                {
                    if (item.RowState == DataRowState.Modified)  //수정
                    {
                        string pQuery = "EXEC _SSelectResultSave " + item["Seq"].ToString() + ", '" + Convert.ToInt32(item["ReportChk"]) + "'";
                        Dblink.ModifyMethod(pQuery);
                    }
                }
                foreach (DataRow item in CurrDS.Tables[0].Rows)
                {
                    if (item.RowState == DataRowState.Modified)  //수정
                    {
                        string pQuery = "EXEC _SSelectResultSave " + item["Seq"].ToString() + ", '" + Convert.ToInt32(item["ReportChk"]) + "'";
                        Dblink.ModifyMethod(pQuery);
                    }
                }
                foreach (DataRow item in ResiDS.Tables[0].Rows)
                {
                    if (item.RowState == DataRowState.Modified)  //수정
                    {
                        string pQuery = "EXEC _SSelectResultSave " + item["Seq"].ToString() + ", '" + Convert.ToInt32(item["ReportChk"]) + "'";
                        Dblink.ModifyMethod(pQuery);
                    }
                }
                foreach (DataRow item in RNTDS.Tables[0].Rows)
                {
                    if (item.RowState == DataRowState.Modified)  //수정
                    {
                        string pQuery = "EXEC _SSelectResultSave " + item["Seq"].ToString() + ", '" + Convert.ToInt32(item["ReportChk"]) + "'";
                        Dblink.ModifyMethod(pQuery);
                    }
                }
                foreach (DataRow item in NRTDS.Tables[0].Rows)
                {
                    if (item.RowState == DataRowState.Modified)  //수정
                    {
                        string pQuery = "EXEC _SSelectResultSave " + item["Seq"].ToString() + ", '" + Convert.ToInt32(item["ReportChk"]) + "'";
                        Dblink.ModifyMethod(pQuery);
                    }
                }
                //this.btnQuery_Click(null, null);
                this.Cursor = Cursors.Default;
                MessageBox.Show("저장이 완료되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FSR103", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvRelayList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    this.Cursor = Cursors.WaitCursor;

                    string pQuery = string.Empty;
                    string sPalletId = string.Empty;
                    string sLot = string.Empty;
                    string sReport = string.Empty;

                    sLot = mtxtLot.Text.Replace("-", "");
                    sPalletId = dgvRelayList.Rows[dgvRelayList.CurrentCellAddress.Y].Cells[0].Value.ToString();
                    if (rbAll.Checked)
                        sReport = "2";
                    else if (rbReport.Checked)
                        sReport = "1";
                    else if (rbTest.Checked)
                        sReport = "0";

                    string pQueryContect = string.Empty;
                    string pQueryCurr = string.Empty;
                    string pQueryResi = string.Empty;
                    string pQueryRNT = string.Empty;
                    string pQueryNRT = string.Empty;

                    pQueryContect = "EXEC _SSelectResultDetailQuery2 '" + sLot + "', '" + sPalletId + "', '" + sReport + "', '05'";
                    pQueryCurr = "EXEC _SSelectResultDetailQuery2 '" + sLot + "', '" + sPalletId + "', '" + sReport + "', '01'";
                    pQueryResi = "EXEC _SSelectResultDetailQuery2 '" + sLot + "', '" + sPalletId + "', '" + sReport + "', '02'";
                    pQueryRNT = "EXEC _SSelectResultDetailQuery2 '" + sLot + "', '" + sPalletId + "', '" + sReport + "', '03'";
                    pQueryNRT = "EXEC _SSelectResultDetailQuery2 '" + sLot + "', '" + sPalletId + "', '" + sReport + "', '04'";

                    
                    ContectDS.Reset();
                    CurrDS.Reset();
                    ResiDS.Reset();
                    RNTDS.Reset();
                    NRTDS.Reset();
                    dgvContect.DataSource = null;
                    dgvCurr.DataSource = null;
                    dgvResi.DataSource = null;
                    dgvRNT.DataSource = null;
                    dgvNRT.DataSource = null;


                    //dgvContect.AutoGenerateColumns = true;

                    Dblink.AllSelect(pQueryContect, ContectDS);
                    dgvContect.DataSource = ContectDS.Tables[0];
                    for (int i = 0; i < dgvContect.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgvContect.Rows[i].Cells["ReportChk"].Value) == true)
                        {
                            dgvContect.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                        else
                        {
                            dgvContect.Rows[i].Cells["ReportChk"].Value = false;
                            dgvContect.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                        }

                        //컬럼 돌면서 정상범위 벗어난것 빨간색으로 변경
                        for(int j = 1; j < dgvContect.Columns.Count - 2; j++)
                        {
                            if(Convert.ToDouble(dgvContect.Rows[i].Cells[j].Value) < Convert.ToDouble(txtCMin.Text) || Convert.ToDouble(dgvContect.Rows[i].Cells[j].Value) > Convert.ToDouble(txtCMax.Text))
                            {
                                dgvContect.Rows[i].Cells[j].Style.ForeColor = Color.Red;
                            }
                            else
                            {
                                dgvContect.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                            }
                        }
                    }

                    Dblink.AllSelect(pQueryCurr, CurrDS);
                    dgvCurr.DataSource = CurrDS.Tables[0];
                    for (int i = 0; i < dgvCurr.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgvCurr.Rows[i].Cells["ReportChk"].Value) == true)
                        {
                            dgvCurr.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                        else
                        {
                            dgvCurr.Rows[i].Cells["ReportChk"].Value = false;
                            dgvCurr.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                        }

                        //컬럼 돌면서 정상범위 벗어난것 빨간색으로 변경
                        if (Convert.ToDouble(dgvCurr.Rows[i].Cells[1].Value) < Convert.ToDouble(txtCOMin.Text) || Convert.ToDouble(dgvCurr.Rows[i].Cells[1].Value) > Convert.ToDouble(txtCOMax.Text))
                        {
                            dgvCurr.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                        }
                        else
                        {
                            dgvCurr.Rows[i].Cells[1].Style.ForeColor = Color.Black;
                        }

                        if (Convert.ToDouble(dgvCurr.Rows[i].Cells[2].Value) < Convert.ToDouble(txtCDMin.Text) || Convert.ToDouble(dgvCurr.Rows[i].Cells[2].Value) > Convert.ToDouble(txtCDMax.Text))
                        {
                            dgvCurr.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                        }
                        else
                        {
                            dgvCurr.Rows[i].Cells[2].Style.ForeColor = Color.Black;
                        }
                    }

                    Dblink.AllSelect(pQueryResi, ResiDS);
                    dgvResi.DataSource = ResiDS.Tables[0];
                    for (int i = 0; i < dgvResi.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgvResi.Rows[i].Cells["ReportChk"].Value) == true)
                        {
                            dgvResi.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                        else
                        {
                            dgvResi.Rows[i].Cells["ReportChk"].Value = false;
                            dgvResi.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                        }

                        if (Convert.ToDouble(dgvResi.Rows[i].Cells[1].Value) < Convert.ToDouble(txtRMin.Text) || Convert.ToDouble(dgvResi.Rows[i].Cells[1].Value) > Convert.ToDouble(txtRMax.Text))
                        {
                            dgvResi.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                        }
                        else
                        {
                            dgvResi.Rows[i].Cells[1].Style.ForeColor = Color.Black;
                        }
                    }

                    Dblink.AllSelect(pQueryRNT, RNTDS);
                    dgvRNT.DataSource = RNTDS.Tables[0];
                    for (int i = 0; i < dgvRNT.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgvRNT.Rows[i].Cells["ReportChk"].Value) == true)
                        {
                            dgvRNT.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                        else
                        {
                            dgvRNT.Rows[i].Cells["ReportChk"].Value = false;
                            dgvRNT.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                        }

                        //컬럼 돌면서 정상범위 벗어난것 빨간색으로 변경
                        for (int j = 1; j < dgvRNT.Columns.Count - 2; j++)
                        {
                            if (Convert.ToDouble(dgvRNT.Rows[i].Cells[j].Value) < Convert.ToDouble(txtTRNMin.Text) || Convert.ToDouble(dgvRNT.Rows[i].Cells[j].Value) > Convert.ToDouble(txtTRNMax.Text))
                            {
                                dgvRNT.Rows[i].Cells[j].Style.ForeColor = Color.Red;
                            }
                            else
                            {
                                dgvRNT.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                            }
                        }
                    }

                    Dblink.AllSelect(pQueryNRT, NRTDS);
                    dgvNRT.DataSource = NRTDS.Tables[0];
                    for (int i = 0; i < dgvNRT.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgvNRT.Rows[i].Cells["ReportChk"].Value) == true)
                        {
                            dgvNRT.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                        else
                        {
                            dgvNRT.Rows[i].Cells["ReportChk"].Value = false;
                            dgvNRT.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                        }

                        //컬럼 돌면서 정상범위 벗어난것 빨간색으로 변경
                        for (int j = 1; j < dgvNRT.Columns.Count - 2; j++)
                        {
                            if (Convert.ToDouble(dgvNRT.Rows[i].Cells[j].Value) < Convert.ToDouble(txtTNRMin.Text) || Convert.ToDouble(dgvNRT.Rows[i].Cells[j].Value) > Convert.ToDouble(txtTNRMax.Text))
                            {
                                dgvNRT.Rows[i].Cells[j].Style.ForeColor = Color.Red;
                            }
                            else
                            {
                                dgvNRT.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                            }
                        }
                    }

                    GridResetMethod(sPalletId);
                    this.Cursor = Cursors.Default; 
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FSR104", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void mtxtLot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQuery_Click(null, null);
            }
        }

        private void mtxtLot_Click(object sender, EventArgs e)
        {
            ((MaskedTextBox)sender).SelectAll();
        }

        private void rb_Click(object sender, EventArgs e)
        {
            if (dgvRelayList.Rows.Count > 0)
            {
                DataGridViewCellEventArgs cellIndex = new DataGridViewCellEventArgs(0, 0);
                dgvRelayList_CellClick(null, cellIndex);
            }
        }

        private void dgvContect_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 && e.ColumnIndex == 0)
                {
                    int cnt = 0;
                    for (int i = 0; i < dgvContect.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgvContect.Rows[i].Cells["ReportChk"].Value))
                        {
                            cnt++;
                            break;
                        }
                    }

                    if (cnt > 0)
                    {
                        for (int i = 0; i < dgvContect.Rows.Count; i++)
                        {
                            dgvContect.Rows[i].Cells["ReportChk"].Value = false;
                            dgvContect.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dgvContect.Rows.Count; i++)
                        {
                            dgvContect.Rows[i].Cells["ReportChk"].Value = true;
                            dgvContect.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                }
                dgvContect.EndEdit();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FSR105", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvCurr_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 && e.ColumnIndex == 0)
                {
                    int cnt = 0;
                    for (int i = 0; i < dgvCurr.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgvCurr.Rows[i].Cells["ReportChk"].Value))
                        {
                            cnt++;
                            break;
                        }
                    }

                    if (cnt > 0)
                    {
                        for (int i = 0; i < dgvCurr.Rows.Count; i++)
                        {
                            dgvCurr.Rows[i].Cells["ReportChk"].Value = false;
                            dgvCurr.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dgvCurr.Rows.Count; i++)
                        {
                            dgvCurr.Rows[i].Cells["ReportChk"].Value = true;
                            dgvCurr.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                }
                dgvCurr.EndEdit();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FSR106", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvResi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 && e.ColumnIndex == 0)
                {
                    int cnt = 0;
                    for (int i = 0; i < dgvResi.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgvResi.Rows[i].Cells["ReportChk"].Value))
                        {
                            cnt++;
                            break;
                        }
                    }

                    if (cnt > 0)
                    {
                        for (int i = 0; i < dgvResi.Rows.Count; i++)
                        {
                            dgvResi.Rows[i].Cells["ReportChk"].Value = false;
                            dgvResi.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dgvResi.Rows.Count; i++)
                        {
                            dgvResi.Rows[i].Cells["ReportChk"].Value = true;
                            dgvResi.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                }
                dgvResi.EndEdit();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FSR107", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvRNT_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 && e.ColumnIndex == 0)
                {
                    int cnt = 0;
                    for (int i = 0; i < dgvRNT.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgvRNT.Rows[i].Cells["ReportChk"].Value))
                        {
                            cnt++;
                            break;
                        }
                    }

                    if (cnt > 0)
                    {
                        for (int i = 0; i < dgvRNT.Rows.Count; i++)
                        {
                            dgvRNT.Rows[i].Cells["ReportChk"].Value = false;
                            dgvRNT.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dgvRNT.Rows.Count; i++)
                        {
                            dgvRNT.Rows[i].Cells["ReportChk"].Value = true;
                            dgvRNT.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                }
                dgvRNT.EndEdit();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FSR108", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvNRT_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 && e.ColumnIndex == 0)
                {
                    int cnt = 0;
                    for (int i = 0; i < dgvNRT.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgvNRT.Rows[i].Cells["ReportChk"].Value))
                        {
                            cnt++;
                            break;
                        }
                    }

                    if (cnt > 0)
                    {
                        for (int i = 0; i < dgvNRT.Rows.Count; i++)
                        {
                            dgvNRT.Rows[i].Cells["ReportChk"].Value = false;
                            dgvNRT.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dgvNRT.Rows.Count; i++)
                        {
                            dgvNRT.Rows[i].Cells["ReportChk"].Value = true;
                            dgvNRT.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                }
                dgvNRT.EndEdit();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FSR109", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvContect_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (Convert.ToBoolean(dgvContect.Rows[e.RowIndex].Cells["ReportChk"].Value))
                        {
                            dgvContect.Rows[e.RowIndex].Cells["ReportChk"].Value = false;
                            dgvContect.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gainsboro;
                        }
                        else
                        {
                            dgvContect.Rows[e.RowIndex].Cells["ReportChk"].Value = true;
                            dgvContect.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FSR110", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvCurr_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (Convert.ToBoolean(dgvCurr.Rows[e.RowIndex].Cells["ReportChk"].Value))
                        {
                            dgvCurr.Rows[e.RowIndex].Cells["ReportChk"].Value = false;
                            dgvCurr.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gainsboro;
                        }
                        else
                        {
                            dgvCurr.Rows[e.RowIndex].Cells["ReportChk"].Value = true;
                            dgvCurr.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FSR111", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvResi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (Convert.ToBoolean(dgvResi.Rows[e.RowIndex].Cells["ReportChk"].Value))
                        {
                            dgvResi.Rows[e.RowIndex].Cells["ReportChk"].Value = false;
                            dgvResi.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gainsboro;
                        }
                        else
                        {
                            dgvResi.Rows[e.RowIndex].Cells["ReportChk"].Value = true;
                            dgvResi.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FSR112", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvRNT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (Convert.ToBoolean(dgvRNT.Rows[e.RowIndex].Cells["ReportChk"].Value))
                        {
                            dgvRNT.Rows[e.RowIndex].Cells["ReportChk"].Value = false;
                            dgvRNT.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gainsboro;
                        }
                        else
                        {
                            dgvRNT.Rows[e.RowIndex].Cells["ReportChk"].Value = true;
                            dgvRNT.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FSR113", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvNRT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (Convert.ToBoolean(dgvNRT.Rows[e.RowIndex].Cells["ReportChk"].Value))
                        {
                            dgvNRT.Rows[e.RowIndex].Cells["ReportChk"].Value = false;
                            dgvNRT.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gainsboro;
                        }
                        else
                        {
                            dgvNRT.Rows[e.RowIndex].Cells["ReportChk"].Value = true;
                            dgvNRT.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FSR113", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void dgvRelayList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                if (dgvRelayList.RowCount > 0)
                {
                    DataGridViewCellEventArgs cellIndex = new DataGridViewCellEventArgs(dgvRelayList.CurrentCellAddress.X, dgvRelayList.CurrentCellAddress.Y);
                    dgvRelayList_CellClick(null, cellIndex);
                }
            }
        }
    }
}
