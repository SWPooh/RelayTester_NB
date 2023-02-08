#define _TEST1
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
using System.Runtime.InteropServices;
using System.IO;
using System.Collections;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RelayTester
{
    public partial class FormReportPrint : Form
    {
        public DataSet masterDS = new DataSet();
        public DataSet mainDS = new DataSet();
        DbLink Dblink = new DbLink();

        FormProgressbar progressbar = new FormProgressbar("데이터베이스 확인중...");
        Thread loadReport = null;
        Thread progressThread;

        public int rowindex;

        //미리보기&출력물 헤더

#if _TEST1_


////
#endif

        string[] strHeaders_print1 = { "", "겉모양,\n구조", "\n\nA", "치수\n\nB", "\n\nC", "접점\n점촉저항", "접점\n간격", "접점\n접촉력", "낙하\n전류", "최소\n동작전류", " 동작 (정", "시간 격)      ", "코일\n저항", "절연저항", "내전압" };
        string[] strHeaders_print2 = { "제작\n번호", "균열,유\n해한 홈,\n기타의\n결함이\n없이\n미려하고\n견고함", "155.1\nmm\n±2.0", "88mm\n±1.0", "62mm\n±1.0", "DC\n100mA\n시 전압\n강하\n10mV\n이하", "최소동작\n1.2mm\n이상\n접촉순간\n0.5mm\n이상", "15gf\n이상", "정격전류\n30%이상(12mA\n이상)", "정격전류 80%\n이하(32mA\n이하)", "동작\n(ms)\n30-140", "복구\n(ms)\n5-15", "600Ω\n±5%", "DC 500V\n10MΩ\n이상", "AC1,000V\n60Hz\n1분간" };

        //본화면 헤더
       

        string[] strHeaders_paint = { "겉모양,\n구조", "치수", "접점 접촉저항", "접점 간격", "접점\n접촉력", "낙하\n전류", "최소\n동작전류", "동작시간(정격)", "코일\n저항", "절연저항", "내전압" };

        //엑셀 헤더
       

        #region Print Member Variables
        const string strConnectionString = "data source=localhost;Integrated Security=SSPI;Initial Catalog=Northwind;";
        StringFormat strFormat; //Used to format the grid rows.
        StringFormat strFormat2; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0; //Used for the header height
        int pageNum = 1;
        #endregion

        public int rowCount = 0;
        public int rowNum = 0;

        public FormReportPrint()
        {
            InitializeComponent();
        }

        private void FormTestReportNew_Load(object sender, EventArgs e)
        {
            try
            {
                this.cmbRelayTypeSearch.DataSource = EqType("EXEC _SEquipTypeQuery");
                this.cmbRelayTypeSearch.DisplayMember = "Code_Dtl_Name";
                this.cmbRelayTypeSearch.ValueMember = "Code_Dtl";
                this.cmbRelayTypeSearch.SelectedIndex = 0;

                mtxtLot.Select();

                HeaderSet("DR1"); //기본으로 무극 선택


                mtxDateF.Text = string.Format("{0:D4}{1:D2}{2:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                mtxDateT.Text = string.Format("{0:D4}{1:D2}{2:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FTR101", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        public DataTable EqType(string sQuery)
        {
            DataSet ds = new DataSet();
            Dblink.AllSelect(sQuery, ds);
            return ds.Tables[0];
        }

        private void GridResetMethod()
        {
            dgvReportDtl.ColumnHeadersHeight = 150;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {

            try
            {
                //검색조건 초기화
                foreach (Control ctl in this.grbQuery.Controls)
                {
                    if (ctl.GetType().ToString().Equals("System.Windows.Forms.TextBox") || ctl.GetType().ToString().Equals("System.Windows.Forms.MaskedTextBox"))
                    {
                        ctl.Text = "";
                    }
                }
                cmbRelayTypeSearch.SelectedValue = "01";

                chkDate.Checked = false;
                chkLot.Checked = false;
                chkPalletId.Checked = false;

                mtxDateF.Text = string.Format("{0:D4}{1:D2}{2:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                mtxDateT.Text = string.Format("{0:D4}{1:D2}{2:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                mtxPalletIdF.Text = string.Empty;
                mtxPalletIdT.Text = string.Empty;


                //마스터 초기화
                foreach (Control ctl in this.grbReportMst.Controls)
                {
                    if (ctl.GetType().ToString().Equals("System.Windows.Forms.TextBox") || ctl.GetType().ToString().Equals("System.Windows.Forms.MaskedTextBox"))
                    {
                        ctl.Text = "";
                    }
                }

                //스케줄 디테일 초기화
                mainDS.Clear();
                dgvReportDtl.Rows.Clear();
                GridResetMethod();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FTR102", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnLotQuery_Click(object sender, EventArgs e)
        {
            FormReportLotPop lp = new FormReportLotPop();


            lp.sRelayType = this.cmbRelayTypeSearch.SelectedValue.ToString();

            if (lp.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mtxtLot.Text = lp.sSeq;
                cmbRelayTypeSearch.SelectedValue = lp.sType;
                chkLot.Checked = true;
            }

        }

        private bool queryCheck()
        {
            if (!chkLot.Checked && !chkDate.Checked && !chkPalletId.Checked)
            {
                if (MessageBox.Show("조회조건을 지정하지 않을 경우,\n조회 시간이 오래 걸릴 수 있습니다.\n\n계속 조회하시겠습니까? ", "조회", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return false;
                }
            }
            if (chkLot.Checked)
            {
                if(string.IsNullOrEmpty(mtxtLot.Text.Replace("-", "").Trim()))
                {
                    MessageBox.Show("LOT번호를 조회해 주세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            if (chkDate.Checked)
            {
                if (!isYYYYMMDD(mtxDateF.Text) || !isYYYYMMDD(mtxDateT.Text))
                {
                    MessageBox.Show("날짜 입력 형식을 확인해 주세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if(Convert.ToInt32(mtxDateF.Text.Replace("-", "").Trim()) > Convert.ToInt32(mtxDateT.Text.Replace("-", "").Trim()))
                {
                    MessageBox.Show("시작일자가 종료일자보다 큽니다. 다시 입력해 주세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            if (chkPalletId.Checked)
            {
                if (mtxPalletIdF.Text.Length !=5 || mtxPalletIdT.Text.Length != 5)
                {
                    MessageBox.Show("제작번호 형식을 확인해 주세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            return true;
        }

        private static bool isYYYYMMDD(string date)
        {
            return Regex.IsMatch(date, @"^(19|20)\d{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[0-1])$");
        }

        private void ResultInput(string sType)
        {
            DataSet RefValDS = new DataSet();
            RefValDS.Clear();
            string pQuery = string.Empty;
            pQuery = "EXEC _SRefValQuery";
            Dblink.AllSelect(pQuery, RefValDS);

            txtContactNG.Text = "-";
            txtDropCurrNG.Text = "-";
            txtOperCurrNG.Text = "-";
            txtRNTimeNG.Text = "-";
            txtNRTimeNG.Text = "-";
            txtResiNG.Text = "-";

            if (sType == "01" || sType == "05" || sType == "06" || sType == "07" || sType == "09" || sType == "10")    //무극, 무극-중부하, 무극(ABS), 무극(PGS), 무극-중부하(1-4), 무극-테크빌
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
            else if (sType == "02" || sType == "08")                                  //유극, 유극(PGS)
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
            else if (sType == "03")                                                          //자기유지             
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
        public DataSet RelayDS = new DataSet();

       public void ShowFormPregressbar()
        {
            progressbar.ShowDialog();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (queryCheck())
                {
                    this.Cursor = Cursors.WaitCursor;

                    string pQueryRelay = string.Empty;

                    string temp = cmbRelayTypeSearch.SelectedValue.ToString();

                    pQueryRelay = string.Format("EXEC _SReportMasterQuery '" + cmbRelayTypeSearch.SelectedValue + "'");
                    RelayDS.Clear();
                    pictureBox1.Image = null;
                    pictureBox2.Image = null;
                    Dblink.AllSelect(pQueryRelay, RelayDS);
                    
                    progressThread = new Thread(ShowFormPregressbar);
                    progressThread.Start();

                    for (int i = 0; i < RelayDS.Tables[0].Rows.Count; i++)
                    {
                        txtNm.Text = RelayDS.Tables[0].Rows[i]["RelayName"].ToString();
                        txtSpec.Text = RelayDS.Tables[0].Rows[i]["RelayCode"].ToString();
                        txtRelSpec.Text = RelayDS.Tables[0].Rows[i]["RelatedSpec"].ToString();
                        txtReportSpec.Text = RelayDS.Tables[0].Rows[i]["ReportSpec"].ToString();
                        txtSizeA.Text = RelayDS.Tables[0].Rows[i]["SizeA"].ToString();
                        txtSizeB.Text = RelayDS.Tables[0].Rows[i]["SizeB"].ToString();
                        txtSizeC.Text = RelayDS.Tables[0].Rows[i]["SizeC"].ToString();
                        txtSizeD.Text = RelayDS.Tables[0].Rows[i]["SizeD"].ToString();
                        txtDate.Text = string.Format("{0:D4}.{1:D2}.{2:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                        if (RelayDS.Tables[0].Rows[i]["Img1Name"].ToString() != null && RelayDS.Tables[0].Rows[i]["Img1Name"].ToString() != "")
                        {
                            pictureBox1.Image = ByteArrayToImage((Byte[])RelayDS.Tables[0].Rows[i]["Img1"]);
                            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        if (RelayDS.Tables[0].Rows[i]["Img2Name"].ToString() != null && RelayDS.Tables[0].Rows[i]["Img2Name"].ToString() != "")
                        {
                            pictureBox2.Image = ByteArrayToImage((Byte[])RelayDS.Tables[0].Rows[i]["Img2"]);
                            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                    }
                   
                    HeaderSet(txtSpec.Text.Substring(0, 3));

                    string pQuery = string.Empty;
                    string sLot = string.Empty;
                    string sDateF = string.Empty;
                    string sDateT = string.Empty;
                    string sPalletIdF = string.Empty;
                    string sPalletIdT = string.Empty;
   
                    //OK값 자동으로 입력 안되게 변경 -2017.08.17

                    if (chkLot.Checked)
                        sLot = mtxtLot.Text.Replace("-", "").Trim();
                    else
                        sLot = "0000";

                    if (chkDate.Checked)
                    {
                        sDateF = mtxDateF.Text.Replace("-", "").Trim();
                        sDateT = mtxDateT.Text.Replace("-", "").Trim();
                    }
                    else
                    {
                        sDateF = "00000000";
                        sDateT = "00000000";
                    }

                    if (chkPalletId.Checked)
                    {
                        sPalletIdF = mtxPalletIdF.Text.Trim();
                        sPalletIdT = mtxPalletIdT.Text.Trim();
                    }
                    else
                    {
                        sPalletIdF = "00000";
                        sPalletIdT = "00000";
                    }

                    pQuery = "EXEC _SReportDetailQueryNew5 '" + sLot + "', '" + cmbRelayTypeSearch.SelectedValue + "','" + txtSpec.Text.Substring(0, 3) + "','" + sDateF + "','" + sDateT + "','" + sPalletIdF + "','" + sPalletIdT + "'";
                    mainDS.Clear();
                    dgvReportDtl.DataSource = null;
                    dgvReportDtl.Rows.Clear();

                    
                    Console.WriteLine("Data loding In");
                   
                    Dblink.AllSelect(pQuery, mainDS);
                    progressbar.ChangeProgressBarLabel("데이터를 불러오는 중입니다...");

                    Console.WriteLine("Data loding Out");
                   

                    for (int i = 0; i < mainDS.Tables[0].Rows.Count; i++)
                    {
                        Console.WriteLine("RowIndex : " + i);
                        rowCount = i + 1;
                        rowNum = mainDS.Tables[0].Rows.Count;

                        progressbar.StartProgressbar(rowNum, rowCount);

                        dgvReportDtl.Rows.Add();

                        dgvReportDtl.Rows[i].Cells["No"].Value = mainDS.Tables[0].Rows[i]["No"].ToString();
                        dgvReportDtl.Rows[i].Cells["PalletId5"].Value = mainDS.Tables[0].Rows[i]["PalletId5"].ToString();
                        dgvReportDtl.Rows[i].Cells["Point"].Value = mainDS.Tables[0].Rows[i]["Point_NM"].ToString();
                        dgvReportDtl.Rows[i].Cells["Surface"].Value = "";
                        dgvReportDtl.Rows[i].Cells["SizeA"].Value = "";
                        dgvReportDtl.Rows[i].Cells["SizeB"].Value = "";
                        dgvReportDtl.Rows[i].Cells["SizeC"].Value = "";
                        dgvReportDtl.Rows[i].Cells["SizeD"].Value = "";
                        dgvReportDtl.Rows[i].Cells["Contact"].Value = mainDS.Tables[0].Rows[i]["Contact"].ToString();

                        dgvReportDtl.Rows[i].Cells["Gap"].Value = "";
                        dgvReportDtl.Rows[i].Cells["Touch"].Value = "";
                        dgvReportDtl.Rows[i].Cells["DropCurr"].Value = mainDS.Tables[0].Rows[i]["DropCurr"].ToString();
                        dgvReportDtl.Rows[i].Cells["CaliDropA"].Value = mainDS.Tables[0].Rows[i]["CaliDropA"].ToString();
                        dgvReportDtl.Rows[i].Cells["CaliDropB"].Value = mainDS.Tables[0].Rows[i]["CaliDropB"].ToString();
                        dgvReportDtl.Rows[i].Cells["OperCurr"].Value = mainDS.Tables[0].Rows[i]["OperCurr"].ToString();
                        dgvReportDtl.Rows[i].Cells["CaliOperA"].Value = mainDS.Tables[0].Rows[i]["CaliOperA"].ToString();
                        dgvReportDtl.Rows[i].Cells["CaliOperB"].Value = mainDS.Tables[0].Rows[i]["CaliOperB"].ToString();
                        dgvReportDtl.Rows[i].Cells["CurrRate"].Value = mainDS.Tables[0].Rows[i]["CurrRate"].ToString();
                        if (txtSpec.Text.Substring(0, 3) == "DR3")      //자기유지일때 동작시간, 복구시간을 합치고 전환시간으로 표기함
                        {
                            dgvReportDtl.Rows[i].Cells["RNTime"].Value = (Convert.ToDouble(mainDS.Tables[0].Rows[i]["RNTime"]) + Convert.ToDouble(mainDS.Tables[0].Rows[i]["NRTime"])).ToString();
                            dgvReportDtl.Rows[i].Cells["NRTime"].Value = "-";
                        }
                        else
                        {
                            if (mainDS.Tables[0].Rows[i]["RNTime"].ToString() == "0")    //측정값이 0인것, 즉 측정대상 접점이 아닌것은 -로 표기
                                dgvReportDtl.Rows[i].Cells["RNTime"].Value = "-";
                            else
                                dgvReportDtl.Rows[i].Cells["RNTime"].Value = mainDS.Tables[0].Rows[i]["RNTime"].ToString();

                            if (mainDS.Tables[0].Rows[i]["NRTime"].ToString() == "0")    //측정값이 0인것, 즉 측정대상 접점이 아닌것은 -로 표기
                                dgvReportDtl.Rows[i].Cells["NRTime"].Value = "-";
                            else
                                dgvReportDtl.Rows[i].Cells["NRTime"].Value = mainDS.Tables[0].Rows[i]["NRTime"].ToString();
                        }
                        dgvReportDtl.Rows[i].Cells["Resi"].Value = mainDS.Tables[0].Rows[i]["Resi"].ToString();
                        dgvReportDtl.Rows[i].Cells["CaliCVoltA"].Value = mainDS.Tables[0].Rows[i]["CaliCVoltA"].ToString();
                        dgvReportDtl.Rows[i].Cells["CaliCCurrA"].Value = mainDS.Tables[0].Rows[i]["CaliCCurrA"].ToString();
                        dgvReportDtl.Rows[i].Cells["CaliCResiB"].Value = mainDS.Tables[0].Rows[i]["CaliCResiB"].ToString();
                        dgvReportDtl.Rows[i].Cells["IResi"].Value = "";
                        dgvReportDtl.Rows[i].Cells["IStrength"].Value = "";
                        dgvReportDtl.Rows[i].Cells["LastTime"].Value = mainDS.Tables[0].Rows[i]["LastTime"].ToString();

                        //마스터 수량 입력
                        txtQty.Text = mainDS.Tables[0].Rows[i]["No"].ToString() + "EA";

                        Thread.Sleep(500);
                    }

                    if (mainDS.Tables[0].Rows.Count > 0)    //조회후 조회된 PalletId중 최소값과 최대값을 박아줌
                    {
                        mtxPalletIdF.Text = mainDS.Tables[0].Rows[0]["PalletId5"].ToString();
                        mtxPalletIdT.Text = mainDS.Tables[0].Rows[mainDS.Tables[0].Rows.Count - 1]["PalletId5"].ToString();
                    }

                    ResultInput(cmbRelayTypeSearch.SelectedValue.ToString());
                    ////마스터 수량 입력

                    GridResetMethod();
                    this.Cursor = Cursors.Default;
                }

            }

            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FTR103", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                //progressbar.ClearProgressbar();
                progressbar.CloseProgressBar();
            }
        }

       
        private void HeaderSet(string RelayType)
        {
            try
            {
                
                //계전기 종류마다 성적서 헤더가 달라짐
                switch (RelayType)
                {
                    case "DR1":   //무극
                        dgvReportDtl.Columns["No"].HeaderText = "순번";
                        dgvReportDtl.Columns["PalletId5"].HeaderText = "제작 번호";
                        dgvReportDtl.Columns["Point"].HeaderText = "접점";
                        dgvReportDtl.Columns["Surface"].HeaderText = "균열, 유해한 홈, 기타의 결함이 없이 미려하고 견고함";
                        dgvReportDtl.Columns["SizeA"].HeaderText = "155.1\nmm\n±1.2";
                        dgvReportDtl.Columns["SizeB"].HeaderText = "88\nmm\n±0.8";
                        dgvReportDtl.Columns["SizeC"].HeaderText = "62\nmm\n±0.8";
                        dgvReportDtl.Columns["SizeD"].HeaderText = "130.5\nmm\n±1.2";
                        dgvReportDtl.Columns["Contact"].HeaderText = "DC 100mA 시\n전압강하\n10mV\n이하";
                     
                        dgvReportDtl.Columns["Gap"].HeaderText = "동작 및 복구 시 1.2mm이상,\n절체 시 0.5mm이상";
                        dgvReportDtl.Columns["Touch"].HeaderText = "15gf 이상";
                        dgvReportDtl.Columns["DropCurr"].HeaderText = "정격전류 30 % 이상(12mA이상)";
                        dgvReportDtl.Columns["CaliDropA"].HeaderText = "A";
                        dgvReportDtl.Columns["CaliDropB"].HeaderText = "B";
                        dgvReportDtl.Columns["OperCurr"].HeaderText = "정격전류 80 % 이하(32mA이하)";
                        dgvReportDtl.Columns["CaliOperA"].HeaderText = "A";
                        dgvReportDtl.Columns["CaliOperB"].HeaderText = "B";
                        dgvReportDtl.Columns["CurrRate"].HeaderText = "강상\n전류\n35%\n이상";
                        dgvReportDtl.Columns["RNTime"].HeaderText = "동작 30-140 (ms)";
                        dgvReportDtl.Columns["NRTime"].HeaderText = "복구 5-15 (ms)";
                        dgvReportDtl.Columns["Resi"].HeaderText = "600Ω\n±5%";
                        dgvReportDtl.Columns["CaliCVoltA"].HeaderText = "전압A";
                        dgvReportDtl.Columns["CaliCCurrA"].HeaderText = "전류A";
                        dgvReportDtl.Columns["CaliCResiB"].HeaderText = "B";
                        dgvReportDtl.Columns["IResi"].HeaderText = "DC500V\n10MΩ\n이상";
                        dgvReportDtl.Columns["IStrength"].HeaderText = "AC1,000V 60Hz 1분간";
                        dgvReportDtl.Columns["LastTime"].HeaderText = "시험일자";

                        dgvReportDtl.Columns["CurrRate"].Visible = true;        //자기유지때 낙하강상비 false했을까봐 true해줌
                        dgvReportDtl.Columns["NRTime"].Visible = true;          //자기유지때 복구시간 false했을까봐 true해줌
                        break;

                    case "TR1":   //무극-테크빌(2022.10.26)
                        dgvReportDtl.Columns["No"].HeaderText = "순번";
                        dgvReportDtl.Columns["PalletId5"].HeaderText = "제작 번호";
                        dgvReportDtl.Columns["Point"].HeaderText = "접점";
                        dgvReportDtl.Columns["Surface"].HeaderText = "균열, 유해한 홈, 기타의 결함이 없이 미려하고 견고함";
                        dgvReportDtl.Columns["SizeA"].HeaderText = "155.1\nmm\n±1.2";
                        dgvReportDtl.Columns["SizeB"].HeaderText = "88\nmm\n±0.8";
                        dgvReportDtl.Columns["SizeC"].HeaderText = "62\nmm\n±0.8";
                        dgvReportDtl.Columns["SizeD"].HeaderText = "130.5\nmm\n±1.2";
                        dgvReportDtl.Columns["Contact"].HeaderText = "DC 100mA 시\n전압강하\n10mV\n이하";
                       
                        dgvReportDtl.Columns["Gap"].HeaderText = "동작 및 복구 시 1.2mm이상,\n절체 시 0.5mm이상";
                        dgvReportDtl.Columns["Touch"].HeaderText = "15gf 이상";
                        dgvReportDtl.Columns["DropCurr"].HeaderText = "정격전류 30 % 이상(12mA이상)";
                        dgvReportDtl.Columns["CaliDropA"].HeaderText = "A";
                        dgvReportDtl.Columns["CaliDropB"].HeaderText = "B";
                        dgvReportDtl.Columns["OperCurr"].HeaderText = "정격전류 80 % 이하(32mA이하)";
                        dgvReportDtl.Columns["CaliOperA"].HeaderText = "A";
                        dgvReportDtl.Columns["CaliOperB"].HeaderText = "B";
                        dgvReportDtl.Columns["CurrRate"].HeaderText = "강상\n전류\n35%\n이상";
                        dgvReportDtl.Columns["RNTime"].HeaderText = "동작 30-140 (ms)";
                        dgvReportDtl.Columns["NRTime"].HeaderText = "복구 5-15 (ms)";
                        dgvReportDtl.Columns["Resi"].HeaderText = "600Ω\n±5%";
                        dgvReportDtl.Columns["CaliCVoltA"].HeaderText = "전압A";
                        dgvReportDtl.Columns["CaliCCurrA"].HeaderText = "전류A";
                        dgvReportDtl.Columns["CaliCResiB"].HeaderText = "B";
                        dgvReportDtl.Columns["IResi"].HeaderText = "DC500V\n10MΩ\n이상";
                        dgvReportDtl.Columns["IStrength"].HeaderText = "AC1,000V 60Hz 1분간";
                        dgvReportDtl.Columns["LastTime"].HeaderText = "시험일자";

                        dgvReportDtl.Columns["CurrRate"].Visible = true;        //자기유지때 낙하강상비 false했을까봐 true해줌
                        dgvReportDtl.Columns["NRTime"].Visible = true;          //자기유지때 복구시간 false했을까봐 true해줌
                        break;

                    case "DR2":   //유극
                        dgvReportDtl.Columns["No"].HeaderText = "순번";
                        dgvReportDtl.Columns["PalletId5"].HeaderText = "제작 번호";
                        dgvReportDtl.Columns["Point"].HeaderText = "접점";
                        dgvReportDtl.Columns["Surface"].HeaderText = "균열, 유해한 홈, 기타의 결함이 없이 미려하고 견고함";
                        dgvReportDtl.Columns["SizeA"].HeaderText = "155.1\nmm\n±1.2";
                        dgvReportDtl.Columns["SizeB"].HeaderText = "88\nmm\n±0.8";
                        dgvReportDtl.Columns["SizeC"].HeaderText = "62\nmm\n±0.8";
                        dgvReportDtl.Columns["SizeD"].HeaderText = "130.5\nmm\n±1.2";
                        dgvReportDtl.Columns["Contact"].HeaderText = "DC 100mA 시\n전압강하\n10mV\n이하";
                        //dgvReportDtl.Columns["CaliContactA"].HeaderText = "A";
                        //dgvReportDtl.Columns["CaliContactB"].HeaderText = "B";
                        dgvReportDtl.Columns["Gap"].HeaderText = "동작 및 복구 시 1.2mm이상,\n절체 시 0.5mm이상";
                        dgvReportDtl.Columns["Touch"].HeaderText = "15gf 이상";
                        dgvReportDtl.Columns["DropCurr"].HeaderText = "정격전류 30 % 이상(18mA이상)";
                        dgvReportDtl.Columns["CaliDropA"].HeaderText = "A";
                        dgvReportDtl.Columns["CaliDropB"].HeaderText = "B";
                        dgvReportDtl.Columns["OperCurr"].HeaderText = "정격전류 80 % 이하(48mA이하)";
                        dgvReportDtl.Columns["CaliOperA"].HeaderText = "A";
                        dgvReportDtl.Columns["CaliOperB"].HeaderText = "B";
                        dgvReportDtl.Columns["CurrRate"].HeaderText = "강상\n전류\n35%\n이상";
                        dgvReportDtl.Columns["RNTime"].HeaderText = "동작 30-140 (ms)";
                        dgvReportDtl.Columns["NRTime"].HeaderText = "복구 10-50 (ms)";
                        dgvReportDtl.Columns["Resi"].HeaderText = "400Ω\n±5%";
                        dgvReportDtl.Columns["CaliCVoltA"].HeaderText = "전압A";
                        dgvReportDtl.Columns["CaliCCurrA"].HeaderText = "전류A";
                        dgvReportDtl.Columns["CaliCResiB"].HeaderText = "B";
                        dgvReportDtl.Columns["IResi"].HeaderText = "DC500V\n10MΩ\n이상";
                        dgvReportDtl.Columns["IStrength"].HeaderText = "AC1,000V 60Hz 1분간";
                        dgvReportDtl.Columns["LastTime"].HeaderText = "시험일자";

                        dgvReportDtl.Columns["CurrRate"].Visible = true;        //자기유지때 낙하강상비 false했을까봐 true해줌
                        dgvReportDtl.Columns["NRTime"].Visible = true;          //자기유지때 복구시간 false했을까봐 true해줌
                        break;

                    case "DR3":   //자기유지
                        dgvReportDtl.Columns["No"].HeaderText = "순번";
                        dgvReportDtl.Columns["PalletId5"].HeaderText = "제작 번호";
                        dgvReportDtl.Columns["Point"].HeaderText = "접점";
                        dgvReportDtl.Columns["Surface"].HeaderText = "균열, 유해한 홈, 기타의 결함이 없이 미려하고 견고함";
                        dgvReportDtl.Columns["SizeA"].HeaderText = "155.1\nmm\n±1.2";
                        dgvReportDtl.Columns["SizeB"].HeaderText = "88\nmm\n±0.8";
                        dgvReportDtl.Columns["SizeC"].HeaderText = "62\nmm\n±0.8";
                        dgvReportDtl.Columns["SizeD"].HeaderText = "130.5\nmm\n±1.2";
                        dgvReportDtl.Columns["Contact"].HeaderText = "DC 100mA 시\n전압강하\n10mV\n이하";
                     
                        dgvReportDtl.Columns["Gap"].HeaderText = "동작 및 복구 시 1.2mm이상,\n절체 시 0.6mm이상";
                        dgvReportDtl.Columns["Touch"].HeaderText = "15gf 이상";
                        dgvReportDtl.Columns["DropCurr"].HeaderText = "정격전류 60 % 이상(24mA이상)";
                        dgvReportDtl.Columns["CaliDropA"].HeaderText = "A";
                        dgvReportDtl.Columns["CaliDropB"].HeaderText = "B";
                        dgvReportDtl.Columns["OperCurr"].HeaderText = "정격전류 80 % 이하(32mA이하)";
                        dgvReportDtl.Columns["CaliOperA"].HeaderText = "A";
                        dgvReportDtl.Columns["CaliOperB"].HeaderText = "B";
                        dgvReportDtl.Columns["CurrRate"].HeaderText = "강상\n전류\n35%\n이상";
                        dgvReportDtl.Columns["RNTime"].HeaderText = "60-180\n(ms)\n";
                        dgvReportDtl.Columns["NRTime"].HeaderText = "복구 5-15 (ms)";
                        dgvReportDtl.Columns["Resi"].HeaderText = "600Ω\n±5%";
                        dgvReportDtl.Columns["CaliCVoltA"].HeaderText = "전압A";
                        dgvReportDtl.Columns["CaliCCurrA"].HeaderText = "전류A";
                        dgvReportDtl.Columns["CaliCResiB"].HeaderText = "B";
                        dgvReportDtl.Columns["IResi"].HeaderText = "DC500V\n10MΩ\n이상";
                        dgvReportDtl.Columns["IStrength"].HeaderText = "AC1,000V 60Hz 1분간";
                        dgvReportDtl.Columns["LastTime"].HeaderText = "시험일자";

                        dgvReportDtl.Columns["CurrRate"].Visible = false;   //자기유지는 낙하강상비 없음
                        dgvReportDtl.Columns["NRTime"].Visible = false;     //자기유지는 복구시간 없음
                        break;
                }

                dgvReportDtl.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FTR104", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvReportDtl_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                DataGridView gv = (DataGridView)sender;

                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                string[] strHeaders_Line1;
                string[] strHeaders_Line2;
                string[] strHeaders_Line3;
                if (txtSpec.Text == "DR300")    //자기유지일때 헤더텍스트 변경됨
                {
                    strHeaders_Line1 = new string[]{ "겉모양,\n구조", "치수", "접점 접촉저항", "접점 간격", "접점\n접촉력", "동작특성", "전환\n시간", " 코일저항", "절연저항", "내전압" };
                    strHeaders_Line2 = new string[]{ "A", "B", "C", "D", "전환전류N", "전환전류R", "낙하강상비", "보정값(코일저항 전압수신값*A)\n(코일저항 전류수신값*A)\n(코일저항+B)" };
                    strHeaders_Line3 = new string[]{ "보정값(전환전류N 수신값*A+B)", "보정값(전환전류R 수신값*A+B)" };
                }
                else
                {
                    strHeaders_Line1 = new string[] { "겉모양,\n구조", "치수", "접점 접촉저항", "접점 간격", "접점\n접촉력", "동작특성", "동작시간\n(정격)", " 코일저항", "절연저항", "내전압" };
                    strHeaders_Line2 = new string[] { "A", "B", "C", "D", "낙하전류", "최소 동작전류", "낙하강상비", "보정값(코일저항 전압수신값*A)\n(코일저항 전류수신값*A)\n(코일저항+B)" };
                    strHeaders_Line3 = new string[] { "보정값(낙하전류 수신값*A+B)", "보정값(동작전류 수신값*A+B)" };
                }
                // Category Painting
                {

                    int width;

                    Rectangle r1 = gv.GetCellDisplayRectangle(3, -1, false);
                    width = gv.GetCellDisplayRectangle(3, -1, false).Width;
                    r1.X += 1;
                    r1.Y += 1;
                    r1.Width = width - 2;
                    r1.Height = 35 - 2;
                    e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r1);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), r1);
                    e.Graphics.DrawString(strHeaders_Line1[0], gv.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor), r1, format);


                    Rectangle r2 = gv.GetCellDisplayRectangle(4, -1, false);
                    width = gv.GetCellDisplayRectangle(4, -1, false).Width + gv.GetCellDisplayRectangle(5, -1, false).Width + gv.GetCellDisplayRectangle(6, -1, false).Width + gv.GetCellDisplayRectangle(7, -1, false).Width;
                    r2.X += 1;
                    r2.Y += 1;
                    r2.Width = width - 2;
                    r2.Height = 35 - 2;
                    e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r2);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), r2);
                    e.Graphics.DrawString(strHeaders_Line1[1], gv.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor), r2, format);

                    Rectangle r2_1 = gv.GetCellDisplayRectangle(4, -1, false);
                    r2_1.X += 1;
                    r2_1.Y = r2.Y + r2.Height + 1;
                    r2_1.Width = r2_1.Width - 2;
                    r2_1.Height = 35 - 2;
                    e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r2_1);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), r2_1);
                    e.Graphics.DrawString(strHeaders_Line2[0], gv.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor), r2_1, format);


                    Rectangle r2_2 = gv.GetCellDisplayRectangle(5, -1, false);
                    r2_2.X += 1;
                    r2_2.Y = r2.Y + r2.Height + 1;
                    r2_2.Width = r2_2.Width - 2;
                    r2_2.Height = 35 - 2;
                    e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r2_2);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), r2_2);
                    e.Graphics.DrawString(strHeaders_Line2[1], gv.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor), r2_2, format);

                    Rectangle r2_3 = gv.GetCellDisplayRectangle(6, -1, false);
                    r2_3.X += 1;
                    r2_3.Y = r2.Y + r2.Height + 1;
                    r2_3.Width = r2_3.Width - 2;
                    r2_3.Height = 35 - 2;
                    e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r2_3);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), r2_3);
                    e.Graphics.DrawString(strHeaders_Line2[2], gv.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor), r2_3, format);


                    Rectangle r2_4 = gv.GetCellDisplayRectangle(7, -1, false);
                    r2_4.X += 1;
                    r2_4.Y = r2.Y + r2.Height + 1;
                    r2_4.Width = r2_4.Width - 2;
                    r2_4.Height = 35 - 2;
                    e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r2_4);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), r2_4);
                    e.Graphics.DrawString(strHeaders_Line2[3], gv.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor), r2_4, format);


                    Rectangle r3 = gv.GetCellDisplayRectangle(8, -1, false);
                    width = gv.GetCellDisplayRectangle(8, -1, false).Width;
                    //width = gv.GetCellDisplayRectangle(8, -1, false).Width + gv.GetCellDisplayRectangle(9, -1, false).Width + gv.GetCellDisplayRectangle(10, -1, false).Width;
                    r3.X += 1;
                    r3.Y += 1;
                    r3.Width = width - 2;
                    r3.Height = 35 - 2;
                    e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r3);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), r3);
                    e.Graphics.DrawString(strHeaders_Line1[2], gv.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor), r3, format);


                    //Rectangle r3_1 = gv.GetCellDisplayRectangle(9, -1, false);
                    //width = gv.GetCellDisplayRectangle(9, -1, false).Width + gv.GetCellDisplayRectangle(10, -1, false).Width;
                    //r3_1.X += 1;
                    //r3_1.Y = r3.Y + r3.Height + 1;
                    //r3_1.Width = width - 2;
                    //r3_1.Height = 35 - 2;
                    //e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r3_1);
                    //e.Graphics.FillRectangle(new SolidBrush(Color.White), r3_1);
                    //e.Graphics.DrawString(strHeaders_Line2[4], gv.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor), r3_1, format);


                    Rectangle r4 = gv.GetCellDisplayRectangle(9, -1, false);
                    r4.X += 1;
                    r4.Y += 1;
                    r4.Width = r4.Width - 2;
                    r4.Height = 35 - 2;
                    e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r4);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), r4);
                    e.Graphics.DrawString(strHeaders_Line1[3], gv.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor), r4, format);


                    Rectangle r5 = gv.GetCellDisplayRectangle(10, -1, false);
                    r5.X += 1;
                    r5.Y += 1;
                    r5.Width = r5.Width - 2;
                    r5.Height = 35 - 2;
                    e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r5);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), r5);
                    e.Graphics.DrawString(strHeaders_Line1[4], gv.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor), r5, format);

                    Rectangle r6 = gv.GetCellDisplayRectangle(11, -1, false);
                    width = gv.GetCellDisplayRectangle(11, -1, false).Width + gv.GetCellDisplayRectangle(12, -1, false).Width + gv.GetCellDisplayRectangle(13, -1, false).Width + gv.GetCellDisplayRectangle(14, -1, false).Width + gv.GetCellDisplayRectangle(15, -1, false).Width + gv.GetCellDisplayRectangle(16, -1, false).Width + gv.GetCellDisplayRectangle(17, -1, false).Width;
                    r6.X += 1;
                    r6.Y += 1;
                    r6.Width = width - 2;
                    r6.Height = 35 - 2;
                    e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r6);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), r6);
                    e.Graphics.DrawString(strHeaders_Line1[5], gv.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor), r6, format);

                    Rectangle r6_1 = gv.GetCellDisplayRectangle(11, -1, false);
                    width = gv.GetCellDisplayRectangle(11, -1, false).Width + gv.GetCellDisplayRectangle(12, -1, false).Width + gv.GetCellDisplayRectangle(13, -1, false).Width;
                    r6_1.X += 1;
                    r6_1.Y = r6.Y + r6.Height + 1;
                    r6_1.Width = width - 2;
                    r6_1.Height = 35 - 2;
                    e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r6_1);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), r6_1);
                    e.Graphics.DrawString(strHeaders_Line2[4], gv.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor), r6_1, format);

                    Rectangle r6_1_1 = gv.GetCellDisplayRectangle(12, -1, false);
                    width = gv.GetCellDisplayRectangle(12, -1, false).Width + gv.GetCellDisplayRectangle(13, -1, false).Width;
                    r6_1_1.X += 1;
                    r6_1_1.Y = r6_1.Y + r6_1.Height + 1;
                    r6_1_1.Width = width - 2;
                    r6_1_1.Height = 35 - 2;
                    e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r6_1_1);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), r6_1_1);
                    e.Graphics.DrawString(strHeaders_Line3[0], gv.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor), r6_1_1, format);


                    Rectangle r6_2 = gv.GetCellDisplayRectangle(14, -1, false);
                    width = gv.GetCellDisplayRectangle(14, -1, false).Width + gv.GetCellDisplayRectangle(15, -1, false).Width + gv.GetCellDisplayRectangle(16, -1, false).Width;
                    r6_2.X += 1;
                    r6_2.Y = r6.Y + r6.Height + 1;
                    r6_2.Width = width - 2;
                    r6_2.Height = 35 - 2;
                    e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r6_2);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), r6_2);
                    e.Graphics.DrawString(strHeaders_Line2[5], gv.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor), r6_2, format);

                    Rectangle r6_2_1 = gv.GetCellDisplayRectangle(15, -1, false);
                    width = gv.GetCellDisplayRectangle(15, -1, false).Width + gv.GetCellDisplayRectangle(16, -1, false).Width;
                    r6_2_1.X += 1;
                    r6_2_1.Y = r6_2.Y + r6_2.Height + 1;
                    r6_2_1.Width = width - 2;
                    r6_2_1.Height = 35 - 2;
                    e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r6_2_1);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), r6_2_1);
                    e.Graphics.DrawString(strHeaders_Line3[1], gv.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor), r6_2_1, format);


                    Rectangle r6_3 = gv.GetCellDisplayRectangle(17, -1, false);
                    width = gv.GetCellDisplayRectangle(17, -1, false).Width;
                    r6_3.X += 1;
                    r6_3.Y = r6.Y + r6.Height + 1;
                    r6_3.Width = width - 2;
                    r6_3.Height = 35 - 2;
                    e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r6_3);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), r6_3);
                    e.Graphics.DrawString(strHeaders_Line2[6], gv.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor), r6_3, format);


                    Rectangle r7 = gv.GetCellDisplayRectangle(18, -1, false);
                    width = gv.GetCellDisplayRectangle(18, -1, false).Width + gv.GetCellDisplayRectangle(19, -1, false).Width;
                    r7.X += 1;
                    r7.Y += 1;
                    r7.Width = width - 2;
                    r7.Height = 35 - 2;
                    e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r7);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), r7);
                    e.Graphics.DrawString(strHeaders_Line1[6], gv.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor), r7, format);


                    Rectangle r8 = gv.GetCellDisplayRectangle(20, -1, false);
                    width = gv.GetCellDisplayRectangle(20, -1, false).Width + gv.GetCellDisplayRectangle(21, -1, false).Width + gv.GetCellDisplayRectangle(22, -1, false).Width + gv.GetCellDisplayRectangle(23, -1, false).Width;
                    r8.X += 1;
                    r8.Y += 1;
                    r8.Width = width - 2;
                    r8.Height = 35 - 2;
                    e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r8);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), r8);
                    e.Graphics.DrawString(strHeaders_Line1[7], gv.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor), r8, format);


                    Rectangle r8_1 = gv.GetCellDisplayRectangle(21, -1, false);
                    width = gv.GetCellDisplayRectangle(21, -1, false).Width + gv.GetCellDisplayRectangle(22, -1, false).Width + gv.GetCellDisplayRectangle(23, -1, false).Width;
                    r8_1.X += 1;
                    r8_1.Y = r8.Y + r8.Height + 1;
                    r8_1.Width = width - 2;
                    r8_1.Height = 35 + 35 - 2;
                    e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r8_1);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), r8_1);
                    e.Graphics.DrawString(strHeaders_Line2[7], gv.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor), r8_1, format);



                    Rectangle r9 = gv.GetCellDisplayRectangle(24, -1, false);
                    width = gv.GetCellDisplayRectangle(24, -1, false).Width;
                    r9.X += 1;
                    r9.Y += 1;
                    r9.Width = width - 2;
                    r9.Height = 35 - 2;
                    e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r9);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), r9);
                    e.Graphics.DrawString(strHeaders_Line1[8], gv.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor), r9, format);



                    Rectangle r10 = gv.GetCellDisplayRectangle(25, -1, false);
                    width = gv.GetCellDisplayRectangle(25, -1, false).Width;
                    r10.X += 1;
                    r10.Y += 1;
                    r10.Width = width - 2;
                    r10.Height = 35 - 2;
                    e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r10);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), r10);
                    e.Graphics.DrawString(strHeaders_Line1[9], gv.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor), r10, format);



                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FTR105", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        Bitmap ByteArrayToImage(byte[] b)
        {
            MemoryStream ms = new MemoryStream();
            byte[] pData = b;
            ms.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(ms, false);
            Bitmap NewImg = new Bitmap(bm);
            ms.Dispose();

            return bm;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void mtxtLot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnQuery_Click(null, null);
            }
        }

        private void mtxtLot_Click(object sender, EventArgs e)
        {
            ((MaskedTextBox)sender).SelectAll();
        }

        #region 출력

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvReportDtl.Rows.Count > 0)
            {
                this.printPreviewDialog1.Document = this.printDocument1;
                this.printPreviewDialog1.Size = new System.Drawing.Size(1000, 800);
                this.printPreviewDialog1.StartPosition = FormStartPosition.CenterScreen;

                this.printPreviewDialog1.ShowDialog();
            }
            else
            {
                MessageBox.Show("출력할 자료가 없습니다.\n 조회 후 다시 시도하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                string sRelayType = string.Empty;
                switch (cmbRelayTypeSearch.SelectedValue.ToString())
                {
                    case "01":
                        sRelayType = "소형무극선조계전기";
                        break;

                    case "02":
                        sRelayType = "소형유극선조계전기";
                        break;

                    case "03":
                        sRelayType = "자기유지";
                        break;
                }

                //Set the left margin
                int iLeftMargin = e.MarginBounds.Left;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //For the first page to print set the cell width and header height
                if (bFirstPage)
                {
                    //테두리
                    Pen p = new Pen(Color.Black, 1);
                    //p.Alignment = PenAlignment.Inset;
                    //e.Graphics.DrawRectangle(p, e.MarginBounds);
                    //가로줄
                    e.Graphics.DrawLine(p, e.MarginBounds.Left, 100, e.MarginBounds.Right, 100);//위쪽 외곽선
                    e.Graphics.DrawLine(p, e.MarginBounds.Left, 170, e.MarginBounds.Right, 170);
                    e.Graphics.DrawLine(p, e.MarginBounds.Left, 195, e.MarginBounds.Right, 195);
                    e.Graphics.DrawLine(p, e.MarginBounds.Left, 220, e.MarginBounds.Right, 220);
                    e.Graphics.DrawLine(p, e.MarginBounds.Left, 245, 490, 245);
                    e.Graphics.DrawLine(p, e.MarginBounds.Left, 270, e.MarginBounds.Right, 270);

                    //세로줄
                    e.Graphics.DrawLine(p, 100, e.MarginBounds.Top, 100, 700);//왼쪽 외곽선
                    e.Graphics.DrawLine(p, 727, e.MarginBounds.Top, 727, 700);//오른쪽 외곽선
                    e.Graphics.DrawLine(p, 250, 170, 250, 270);
                    e.Graphics.DrawLine(p, 490, 170, 490, 270);
                    e.Graphics.DrawLine(p, 608, 170, 608, 270);

                    //헤더
                    Font drawFont = new Font("맑은 고딕", 9, FontStyle.Bold);
                    string sHeader = "TEST REPORT(" + sRelayType + ")";
                    e.Graphics.DrawString(sHeader,
                        drawFont,
                        Brushes.Black,
                        e.MarginBounds.Left + (e.MarginBounds.Width - e.Graphics.MeasureString(sHeader, drawFont, e.MarginBounds.Width).Width),
                        e.MarginBounds.Top - e.Graphics.MeasureString(sHeader, drawFont, e.MarginBounds.Width).Height - 13);

                    //타이틀 한글
                    string sTitle = "시 험 성 적 서";
                    drawFont = new Font("맑은 고딕", 20);
                    e.Graphics.DrawString(sTitle,
                        drawFont,
                        Brushes.Black,
                        e.MarginBounds.Left + (e.MarginBounds.Width / 2) - ((e.Graphics.MeasureString(sTitle, drawFont, e.MarginBounds.Width).Width) / 2),
                        e.MarginBounds.Top);

                    //타이틀 영문
                    sTitle = "TEST REPORT";
                    e.Graphics.DrawString(sTitle,
                        drawFont,
                        Brushes.Black,
                        e.MarginBounds.Left + (e.MarginBounds.Width / 2) - ((e.Graphics.MeasureString(sTitle, drawFont, e.MarginBounds.Width).Width) / 2),
                        e.MarginBounds.Top + 30);

                    //라벨 한글
                    string sLabel = "품 명";
                    drawFont = new Font("맑은 고딕", 11, FontStyle.Bold);
                    e.Graphics.DrawString(sLabel,
                        drawFont,
                        Brushes.Black,
                        160, 170);

                    sLabel = "규 격";
                    e.Graphics.DrawString(sLabel,
                        drawFont,
                        Brushes.Black,
                        160, 195);

                    sLabel = "수 량";
                    e.Graphics.DrawString(sLabel,
                        drawFont,
                        Brushes.Black,
                        160, 220);

                    sLabel = "관련규격";
                    e.Graphics.DrawString(sLabel,
                        drawFont,
                        Brushes.Black,
                        150, 245);

                    sLabel = "날  짜";
                    e.Graphics.DrawString(sLabel,
                        drawFont,
                        Brushes.Black,
                        525, 170);

                    sLabel = "시 험 자";
                    e.Graphics.DrawString(sLabel,
                        drawFont,
                        Brushes.Black,
                        520, 195);

                    sLabel = "확 인 자";
                    e.Graphics.DrawString(sLabel,
                        drawFont,
                        Brushes.Black,
                        640, 195);

                    //품명
                    e.Graphics.DrawString(txtNm.Text,
                        drawFont,
                        Brushes.Black,
                        370 - ((e.Graphics.MeasureString(txtNm.Text, drawFont, e.MarginBounds.Width).Width) / 2), 170);

                    //규격
                    e.Graphics.DrawString(txtSpec.Text,
                        drawFont,
                        Brushes.Black,
                        370 - ((e.Graphics.MeasureString(txtSpec.Text, drawFont, e.MarginBounds.Width).Width) / 2), 195);

                    //수량
                    e.Graphics.DrawString(txtQty.Text,
                        drawFont,
                        Brushes.Black,
                        370 - ((e.Graphics.MeasureString(txtQty.Text, drawFont, e.MarginBounds.Width).Width) / 2), 220);

                    //관련규격
                    e.Graphics.DrawString(txtRelSpec.Text,
                        drawFont,
                        Brushes.Black,
                        370 - ((e.Graphics.MeasureString(txtRelSpec.Text, drawFont, e.MarginBounds.Width).Width) / 2), 245);

                    //날짜
                    e.Graphics.DrawString(txtDate.Text,
                        drawFont,
                        Brushes.Black,
                        668 - ((e.Graphics.MeasureString(txtDate.Text, drawFont, e.MarginBounds.Width).Width) / 2), 170);

                    //시험자
                    e.Graphics.DrawString(txtTester.Text,
                        drawFont,
                        Brushes.Black,
                        550 - ((e.Graphics.MeasureString(txtTester.Text, drawFont, e.MarginBounds.Width).Width) / 2), 230);

                    // 이미지 삽입
                    Image imageFile1 = pictureBox1.Image;
                    Image imageFile2 = pictureBox2.Image;
                    e.Graphics.DrawImage(imageFile1, 200, 280, 210, 410);
                    e.Graphics.DrawImage(imageFile2, 450, 280, 180, 333);


                    foreach (DataGridViewColumn GridCol in dgvReportDtl.Columns)
                    {
                        if (GridCol.Name != "Column16") //시험일자 안나오게
                        {
                            iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                                           (double)iTotalWidth * (double)iTotalWidth *
                                           ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                            //iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                            //            GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                            iHeaderHeight = 160;

                            // Save width and height of headres
                            arrColumnLefts.Add(iLeftMargin);
                            arrColumnWidths.Add(iTmpWidth);
                            iLeftMargin += iTmpWidth;
                        }
                    }
                }
                //arrColumnLefts[arrColumnLefts.Count - 1] = Convert.ToInt32(arrColumnLefts[arrColumnLefts.Count - 1]) + 3;
                //arrColumnWidths[arrColumnWidths.Count - 1] = Convert.ToInt32(arrColumnWidths[arrColumnWidths.Count - 1]) + 3;
                //Loop till all the grid rows not get printed
                while (iRow <= dgvReportDtl.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dgvReportDtl.Rows[iRow];
                    //Set the cell height
                    iCellHeight = GridRow.Height + 10;
                    int iCount = 0;
                    //Check whether the current page settings allo more rows to print
                    if (iTopMargin + iCellHeight >= 940 + e.MarginBounds.Top) //e.MarginBounds.Height
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        pageNum++;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {
                            //페이지
                            Font drawFont = new Font("맑은 고딕", 9);
                            string sPage = "- " + pageNum.ToString() + " -";
                            e.Graphics.DrawString(sPage,
                                drawFont,
                                Brushes.Black,
                                e.MarginBounds.Left + (e.MarginBounds.Width / 2) - ((e.Graphics.MeasureString(sPage, drawFont, e.MarginBounds.Width).Width) / 2),
                                e.MarginBounds.Bottom + e.Graphics.MeasureString(sPage, drawFont, e.MarginBounds.Width).Height - 50);

                            Image imageFile3 = RelayTester.Properties.Resources.DaeaTi_logo;
                            e.Graphics.DrawImage(imageFile3, e.MarginBounds.Right - 130, e.MarginBounds.Bottom - 50, 130, 62);

                            //Draw Columns                 
                            if (bFirstPage)
                            { iTopMargin = 700; }
                            else
                            { iTopMargin = e.MarginBounds.Top; }

                            strFormat = new StringFormat();
                            strFormat.Alignment = StringAlignment.Center;
                            strFormat.LineAlignment = StringAlignment.Center;
                            strFormat.Trimming = StringTrimming.EllipsisCharacter;

                            strFormat2 = new StringFormat();
                            strFormat2.Alignment = StringAlignment.Center;
                            strFormat2.LineAlignment = StringAlignment.Near;
                            strFormat2.Trimming = StringTrimming.EllipsisCharacter;

                            e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Left, iTopMargin, e.MarginBounds.Right, iTopMargin);     //헤더 윗선
                            e.Graphics.DrawLine(Pens.Black, (int)arrColumnLefts[2], iTopMargin + 25, (int)arrColumnLefts[5], iTopMargin + 25);    //헤더 중간선(치수)
                            e.Graphics.DrawLine(Pens.Black, (int)arrColumnLefts[1], iTopMargin + 50, e.MarginBounds.Right, iTopMargin + 50);    //헤더 중간선


                            Font drawHeaderFont1 = new Font("맑은 고딕", 7);
                            Font drawHeaderFont = new Font("맑은 고딕", 7);
                            foreach (DataGridViewColumn GridCol in dgvReportDtl.Columns)
                            {
                                if (GridCol.Name != "Column16") //시험일자 안나오게
                                {
                                    //헤더 배경색
                                    //e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    //    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    //    (int)arrColumnWidths[iCount], iHeaderHeight));

                                    //헤더 선그리기
                                    //e.Graphics.DrawRectangle(Pens.Black,
                                    //    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    //    (int)arrColumnWidths[iCount], iHeaderHeight));

                                    //멀티헤더 위쪽
                                    if (iCount == 11)
                                    {
                                        e.Graphics.DrawLine(Pens.Black, (int)arrColumnLefts[iCount], iTopMargin + 50, (int)arrColumnLefts[iCount], iTopMargin + iHeaderHeight); //동작시간
                                    }
                                    else if (iCount == 3 || iCount == 4)
                                    {
                                        e.Graphics.DrawLine(Pens.Black, (int)arrColumnLefts[iCount], iTopMargin + 25, (int)arrColumnLefts[iCount], iTopMargin + iHeaderHeight); //치수
                                    }
                                    else
                                    {
                                        e.Graphics.DrawLine(Pens.Black, (int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnLefts[iCount], iTopMargin + iHeaderHeight);
                                    }
                                    //헤더 글씨1
                                    e.Graphics.DrawString(strHeaders_print1[iCount], drawHeaderFont1,
                                        new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin + 5,
                                        (int)arrColumnWidths[iCount], iHeaderHeight), strFormat2);

                                    //헤더 글씨2
                                    e.Graphics.DrawString(strHeaders_print2[iCount], drawHeaderFont,
                                        new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin + 20,
                                        (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                    iCount++;
                                }
                            }

                            e.Graphics.DrawLine(Pens.Black, e.MarginBounds.Right, iTopMargin, e.MarginBounds.Right, iTopMargin + iHeaderHeight);    //헤더 오른쪽 끝선

                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;
                        //Draw Columns Contents                
                        Font drawColFont = new Font("맑은 고딕", 8);
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (Cel.OwningColumn.Name != "Column16")    //시험일자 안나오게
                            {
                                if (Cel.Value != null)
                                {
                                    e.Graphics.DrawString(Cel.Value.ToString(), drawColFont,
                                                new SolidBrush(Cel.InheritedStyle.ForeColor),
                                                new RectangleF((int)arrColumnLefts[iCount], (float)iTopMargin,
                                                (int)arrColumnWidths[iCount], (float)iCellHeight), strFormat);
                                }
                                //Drawing Cells Borders 
                                e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)arrColumnLefts[iCount],
                                        iTopMargin, (int)arrColumnWidths[iCount], iCellHeight));

                                iCount++;
                            }
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }

                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                pageNum = 1;
                bFirstPage = true;
                bNewPage = true;

                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in dgvReportDtl.Columns)
                {
                    if (dgvGridCol.Name != "Column16")  //시험일자 안나오게
                    {
                        iTotalWidth += dgvGridCol.Width;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion



        private void dgvReportDtl_Scroll(object sender, ScrollEventArgs e)
        {
            DataGridView gv = (DataGridView)sender;

            Rectangle rtHeader = gv.DisplayRectangle;

            rtHeader.Height = 35;

            gv.Invalidate(rtHeader);

        }

        private void dgvReportDtl_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            DataGridView gv = (DataGridView)sender;

            Rectangle rtHeader = gv.DisplayRectangle;

            rtHeader.Height = 46;

            gv.Invalidate(rtHeader);

        }

        private void btnExcelSave_Click(object sender, EventArgs e)
        {
            if (dgvReportDtl.Rows.Count > 0)
            {
                dgvReportDtlExcel.Rows.Clear();
                DataGridViewRow row = new DataGridViewRow();

                for (int c = 0; c < dgvReportDtl.ColumnCount; c++)
                {
                    dgvReportDtlExcel.Columns[c].HeaderText = dgvReportDtl.Columns[c].HeaderText;
                }

                for (int i = 0; i < dgvReportDtl.Rows.Count; i++)
                {
                    row = (DataGridViewRow)dgvReportDtl.Rows[i].Clone();
                    int intColIndex = 0;
                    foreach (DataGridViewCell cell in dgvReportDtl.Rows[i].Cells)
                    {
                        row.Cells[intColIndex].Value = cell.Value;
                        intColIndex++;
                    }
                    dgvReportDtlExcel.Rows.Add(row);
                }
                dgvReportDtlExcel.AllowUserToAddRows = false;
                dgvReportDtlExcel.Refresh();


                ExportExcel_ForOneDataGrid(true, dgvReportDtlExcel);
            }
            else
            {
                MessageBox.Show("Excel로 저장할 자료가 없습니다.\n 조회 후 다시 시도하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ExportExcel_ForOneDataGrid(bool captions, DataGridView myDataGridView)
        {
            this.saveFileDialog1.FileName = "계전기 자체검사 성적서(내부용)" + txtSpec.Text + "_LOT" + this.mtxtLot.Text.Replace("-", "").Trim() + "_" + txtDate.Text.Replace(".", "").Trim();
            this.saveFileDialog1.DefaultExt = "xls";
            this.saveFileDialog1.Filter = "Excel files (*.xls)|*.xls";
            this.saveFileDialog1.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                int num = 0;
                object missingType = Type.Missing;
                Excel.Application objApp;
                Excel._Workbook objBook;
                Excel.Workbooks objBooks;
                Excel.Sheets objSheets;
                Excel._Worksheet objSheet;
                Excel.Range range;

                string[] headers = new string[myDataGridView.ColumnCount];
                string[] columns = new string[myDataGridView.ColumnCount];
                for (int c = 0; c < myDataGridView.ColumnCount; c++)
                {
                    headers[c] = myDataGridView.Rows[0].Cells[c].OwningColumn.HeaderText.ToString();
                    if (c <= 25)
                    {
                        num = c + 65;
                        columns[c] = Convert.ToString((char)num);
                    }
                    else
                    {
                        columns[c] = Convert.ToString((char)(Convert.ToInt32(c / 26) - 1 + 65)) + Convert.ToString((char)(c % 26 + 65));
                    }
                }
                try
                {
                    objApp = new Excel.Application();
                    objBooks = objApp.Workbooks;
                    objBook = objBooks.Add(Missing.Value);
                    objSheets = objBook.Worksheets;
                    objSheet = (Excel._Worksheet)objSheets.get_Item(1);
                    objApp.DisplayAlerts = false;       //셀병합등 메시지 안뜨게

                    string[] strHeaders_excel1;
                    string[] strHeaders_excel2;
                    string[] strHeaders_excel3;

                    if (txtSpec.Text == "DR300")    //자기유지일때 헤더텍스트 변경됨
                    {
                        strHeaders_excel1 = new string[] { "", "", "", "겉모양,\n구조", "치수", "", "", "", "접점\n접촉저항", "접점 간격", "접점\n접촉력", "전환전류", "", "", "", "", "", "", "전환시간", "", "코일저항", "", "", "", "절연저항", "내전압", "" };
                        strHeaders_excel2 = new string[] { "", "", "", "", "A", "B", "C", "D", "", "", "", "전환전류N", "", "", "전환전류R", "", "", "낙하강상비", "", "", "", "보정값(코일저항 전압수신값*A)(코일저항 전류수신값*A)(코일저항+B)", "", "", "", "", "" };
                        strHeaders_excel3 = new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "보정값(전환전류N 수신값*A+B)", "", "", "보정값(전환전류R 수신값*A+B)", "", "", "", "", "", "", "", "", "", "", "" };
                    }
                    else
                    {
                        strHeaders_excel1 = new string[] { "", "", "", "겉모양, 구조", "치수", "", "", "", "접점 접촉저항", "접점 간격", "접점 접촉력", "동작특성", "", "", "", "", "", "", "동작시간(정격)", "", "코일저항", "", "", "", "절연저항", "내전압", "" };
                        strHeaders_excel2 = new string[] { "", "", "", "", "A", "B", "C", "D", "", "", "", "낙하전류", "", "", "최소 동작전류", "", "", "낙하강상비", "", "", "", "보정값(코일저항 전압수신값*A)(코일저항 전류수신값*A)(코일저항+B)", "", "", "", "", "" };
                        strHeaders_excel3 = new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "보정값(낙하전류 수신값*A+B)", "", "", "보정값(동작전류 수신값*A+B)", "", "", "", "", "", "", "", "", "", "", "" };
                    }
                    //헤더에 텍스트 입력
                    for (int c = 0; c < myDataGridView.ColumnCount; c++)
                    {
                        //멀티헤더 1번줄 입력(strHeaders_excel1 읽어와서 순서대로 입력)
                        range = objSheet.get_Range(columns[c] + "1", Missing.Value);
                        range.set_Value(Missing.Value, strHeaders_excel1[c].Trim());
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬

                        ////멀티헤더 2번줄(strHeaders_excel2 읽어와서 순서대로 입력)
                        range = objSheet.get_Range(columns[c] + "2", Missing.Value);
                        range.set_Value(Missing.Value, strHeaders_excel2[c].Trim());
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬

                        ////멀티헤더 3번줄(strHeaders_excel3 읽어와서 순서대로 입력)
                        range = objSheet.get_Range(columns[c] + "3", Missing.Value);
                        range.set_Value(Missing.Value, strHeaders_excel3[c].Trim());
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬

                        //멀티헤더 4번줄(dgv에 멀티헤더중 맨 아래 헤더 가져와서 입력)
                        range = objSheet.get_Range(columns[c] + "4", Missing.Value);
                        range.set_Value(Missing.Value, headers[c].Trim());
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬
                    }

                    ////////////////////////////////////////////////////////////////////////////////////




                    myDataGridView.SelectAll();
                    DataObject dataobj = myDataGridView.GetClipboardContent();
                    if (dataobj != null)
                    {
                        Clipboard.SetDataObject(dataobj);
                    }

                    //전체 범위 (왼쪽 상단의 Cell부터 사용한 맨마지막 범위까지)

                    range = objSheet.get_Range("A5", "AA" + (myDataGridView.RowCount + 5).ToString());   //헤더다음부터 5더한 크기
                    range.NumberFormat = "@";   //문자열 서식으로 변경
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬


                    range = objSheet.get_Range("A5", Missing.Value);
                    range.Select();
                    objSheet.PasteSpecial(range, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);



                    myDataGridView.ClearSelection();



                    //////////////////////////////////////////////////////////////////////////////////////

                    /*
                                        for (int i = 0; i < myDataGridView.RowCount; i++)
                                        {
                                            for (int j = 0; j < myDataGridView.ColumnCount; j++)
                                            {
                                                range = objSheet.get_Range(columns[j] + Convert.ToString(i + 5), Missing.Value);
                                                range.NumberFormat = "@";   //문자열 서식으로 변경
                                                range.set_Value(Missing.Value, myDataGridView.Rows[i].Cells[j].Value.ToString());
                                                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬
                                            }
                                        }
*/
                    //셀병합
                    range = objSheet.get_Range("A1", "A4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("B1", "B4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("C1", "C4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("D2", "D4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("E3", "E4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("F3", "F4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("G3", "G4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("H3", "H4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("I2", "I4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    //range = objSheet.get_Range("J2", "K3");
                    //range.Merge(false);
                    //range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("J2", "J4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("K2", "K4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("L3", "L4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("O3", "O4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("R3", "R4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("S2", "S4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("T2", "T4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("U2", "U4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("V2", "X3");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈

                    range = objSheet.get_Range("Y2", "Y4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("Z2", "Z4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("AA1", "AA4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈

                    range = objSheet.get_Range("E1", "H1");
                    range.Merge(true);
                    range.WrapText = true;  //줄바꿈
                    //range = objSheet.get_Range("I1", "K1");
                    //range.Merge(true);
                    //range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("L1", "R1");
                    range.Merge(true);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("S1", "T1");
                    range.Merge(true);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("U1", "X1");
                    range.Merge(true);
                    range.WrapText = true;  //줄바꿈


                    range = objSheet.get_Range("L2", "N2");
                    range.Merge(true);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("O2", "Q2");
                    range.Merge(true);
                    range.WrapText = true;  //줄바꿈


                    range = objSheet.get_Range("M3", "N3");
                    range.Merge(true);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("P3", "Q3");
                    range.Merge(true);
                    range.WrapText = true;  //줄바꿈                

                    int relayCnt = 0;

                    if (txtSpec.Text.Substring(0, 3) == "DR3")      //자기유지일경우 접점갯수가 적음
                    {
                        relayCnt = 12;
                    }
                    else
                    {
                        relayCnt = 14;
                    }

                    for (int i = 5; i < myDataGridView.RowCount + 5; i += relayCnt)
                    {
                        range = objSheet.get_Range("A" + Convert.ToString(i), "A" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("B" + Convert.ToString(i), "B" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("D" + Convert.ToString(i), "D" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("E" + Convert.ToString(i), "E" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("F" + Convert.ToString(i), "F" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("G" + Convert.ToString(i), "G" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("H" + Convert.ToString(i), "H" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("J" + Convert.ToString(i), "J" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("K" + Convert.ToString(i), "K" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("L" + Convert.ToString(i), "L" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("M" + Convert.ToString(i), "M" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("N" + Convert.ToString(i), "N" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("O" + Convert.ToString(i), "O" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("P" + Convert.ToString(i), "P" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("Q" + Convert.ToString(i), "Q" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("R" + Convert.ToString(i), "R" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        //range = objSheet.get_Range("S" + Convert.ToString(i), "S" + Convert.ToString(i + relayCnt - 1));
                        //range.Merge(false);
                        //range = objSheet.get_Range("T" + Convert.ToString(i), "T" + Convert.ToString(i + relayCnt - 1));
                        //range.Merge(false);
                        range = objSheet.get_Range("U" + Convert.ToString(i), "U" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("V" + Convert.ToString(i), "V" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("W" + Convert.ToString(i), "W" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("X" + Convert.ToString(i), "X" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("Y" + Convert.ToString(i), "Y" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("Z" + Convert.ToString(i), "Z" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("AA" + Convert.ToString(i), "AA" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        //range = objSheet.get_Range("AB" + Convert.ToString(i), "AB" + Convert.ToString(i + relayCnt - 1));
                        //range.Merge(false);
                        //range = objSheet.get_Range("AC" + Convert.ToString(i), "AC" + Convert.ToString(i + relayCnt - 1));
                        //range.Merge(false);
                    }

                    if (txtSpec.Text.Substring(0, 3) == "DR3")      //자기유지일경우 낙하강상비, 복구시간 삭제(뒤에서 부터 없애야 순서 계산 안하고 편함)
                    {
                        range = objSheet.get_Range("T1", Missing.Value);    //복구시간
                        range.EntireColumn.Delete(Missing.Value);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(range);

                        range = objSheet.get_Range("R1", Missing.Value);    //낙하강상비
                        range.EntireColumn.Delete(Missing.Value);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
                    }

                    //행 높이 조정
                    objApp.ActiveSheet.Rows("1:1").RowHeight = 30;
                    objApp.ActiveSheet.Rows("2:2").RowHeight = 30;
                    objApp.ActiveSheet.Rows("3:3").RowHeight = 40;
                    objApp.ActiveSheet.Rows("4:4").RowHeight = 16.5;

                    //폰트 사이즈 조정
                    objApp.ActiveSheet.Columns("A:Z").Font.Size = 10;

                    objApp.DisplayAlerts = true;

                    objApp.Visible = false;
                    objApp.UserControl = false;
                    objBook.SaveAs(@saveFileDialog1.FileName,
                        Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal,
                        missingType, missingType, missingType, missingType,
                        Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                        missingType, missingType, missingType, missingType, missingType);
                    objBook.Close(false, missingType, missingType);
                    objApp.Quit();
                    Marshal.ReleaseComObject(objBook);
                    Marshal.ReleaseComObject(objApp);
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("엑셀 저장이 완료되었습니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FTR106", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvReportDtl.RowCount > 0)
                {


                    for (int i = 0; i < dgvReportDtl.RowCount; i++)
                    {
                        dgvReportDtl.Rows[i].Cells["Surface"].Value = "적합";
                        dgvReportDtl.Rows[i].Cells["SizeA"].Value = txtSizeA.Text;
                        dgvReportDtl.Rows[i].Cells["SizeB"].Value = txtSizeB.Text;
                        dgvReportDtl.Rows[i].Cells["SizeC"].Value = txtSizeC.Text;
                        dgvReportDtl.Rows[i].Cells["SizeD"].Value = txtSizeD.Text;
                        dgvReportDtl.Rows[i].Cells["Gap"].Value = "적합";
                        dgvReportDtl.Rows[i].Cells["Touch"].Value = "20gf이상";
                        dgvReportDtl.Rows[i].Cells["IResi"].Value = "10GΩ이상";
                        dgvReportDtl.Rows[i].Cells["IStrength"].Value = "이상없음";
                    }

                }
                else
                {
                    MessageBox.Show("일괄 입력할 데이터가 없습니다.\n조회 후 다시 클릭 하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FTR107", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void dgvReportDtl_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex > -1)
            {
                Rectangle r = e.CellBounds;
                r.Y += e.CellBounds.Height / 2;
                r.Height = e.CellBounds.Height / 2;
                e.PaintBackground(r, true);
                e.PaintContent(r);
                e.Handled = true;
            }
        }

        private void cmbRelayTypeSearch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            mtxtLot.Text = "";
        }

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Name.ToString() == "chkLot")
            {
                if (chk.Checked)
                    mtxtLot.Enabled = true;
                else
                    mtxtLot.Enabled = false;
            }
            else if (chk.Name.ToString() == "chkDate")
            {
                if (chk.Checked)
                {
                    mtxDateF.Enabled = true;
                    mtxDateT.Enabled = true;
                }
                else
                {
                    mtxDateF.Enabled = false;
                    mtxDateT.Enabled = false;
                }
            }
            else if (chk.Name.ToString() == "chkPalletId")
            {
                if (chk.Checked)
                {
                    mtxPalletIdF.Enabled = true;
                    mtxPalletIdT.Enabled = true;
                }
                else
                {
                    mtxPalletIdF.Enabled = false;
                    mtxPalletIdT.Enabled = false;
                }
            }
        }

        private void btnExcelSaveSubmit_Click(object sender, EventArgs e)
        {
            if (dgvReportDtl.Rows.Count > 0)
            {
                dgvReportDtlExcelSubmit.Rows.Clear();
                DataGridViewRow row = new DataGridViewRow();

                for (int c = 0; c < dgvReportDtl.ColumnCount; c++)
                {
                    dgvReportDtlExcelSubmit.Columns[c].HeaderText = dgvReportDtl.Columns[c].HeaderText;
                }

                for (int i = 0; i < dgvReportDtl.Rows.Count; i++)
                {
                    if (dgvReportDtl.Rows[i].Cells["Point"].Value.ToString() == "N01" || dgvReportDtl.Rows[i].Cells["Point"].Value.ToString() == "R01")
                    {
                        row = (DataGridViewRow)dgvReportDtl.Rows[i].Clone();
                        int intColIndex = 0;
                        foreach (DataGridViewCell cell in dgvReportDtl.Rows[i].Cells)
                        {
                            if (cell.Value.ToString() == "N01")
                                row.Cells[intColIndex].Value = "N";
                            else if (cell.Value.ToString() == "R01")
                                row.Cells[intColIndex].Value = "R";
                            else
                                row.Cells[intColIndex].Value = cell.Value;

                            intColIndex++;
                        }
                        dgvReportDtlExcelSubmit.Rows.Add(row);
                    }
                }
                dgvReportDtlExcelSubmit.AllowUserToAddRows = false;
                dgvReportDtlExcelSubmit.Refresh();


                ExportExcel_ForOneDataGrid_Submit(true, dgvReportDtlExcelSubmit);
            }
            else
            {
                MessageBox.Show("Excel로 저장할 자료가 없습니다.\n 조회 후 다시 시도하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ExportExcel_ForOneDataGrid_Submit(bool captions, DataGridView myDataGridView)
        {
            this.saveFileDialog1.FileName = "계전기 자체검사 성적서(제출용)" + txtSpec.Text + "_LOT" + this.mtxtLot.Text.Replace("-", "").Trim() + "_" + txtDate.Text.Replace(".", "").Trim();
            this.saveFileDialog1.DefaultExt = "xls";
            this.saveFileDialog1.Filter = "Excel files (*.xls)|*.xls";
            this.saveFileDialog1.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                int num = 0;
                object missingType = Type.Missing;
                Excel.Application objApp;
                Excel._Workbook objBook;
                Excel.Workbooks objBooks;
                Excel.Sheets objSheets;
                Excel._Worksheet objSheet;
                Excel.Range range;

                string[] headers = new string[myDataGridView.ColumnCount];
                string[] columns = new string[myDataGridView.ColumnCount];
                for (int c = 0; c < myDataGridView.ColumnCount; c++)
                {
                    headers[c] = myDataGridView.Rows[0].Cells[c].OwningColumn.HeaderText.ToString();
                    if (c <= 25)
                    {
                        num = c + 65;
                        columns[c] = Convert.ToString((char)num);
                    }
                    else
                    {
                        columns[c] = Convert.ToString((char)(Convert.ToInt32(c / 26) - 1 + 65)) + Convert.ToString((char)(c % 26 + 65));
                    }
                }
                try
                {
                    objApp = new Excel.Application();
                    objBooks = objApp.Workbooks;
                    objBook = objBooks.Add(Missing.Value);
                    objSheets = objBook.Worksheets;
                    objSheet = (Excel._Worksheet)objSheets.get_Item(1);
                    objApp.DisplayAlerts = false;   //셀병합 등 메시지 안뜨게

                    string[] strHeaders_excel1;
                    string[] strHeaders_excel2;
                    string[] strHeaders_excel3;

                    if (txtSpec.Text == "DR300")    //자기유지일때 헤더텍스트 변경됨
                    {
                        strHeaders_excel1 = new string[] { "", "", "", "겉모양,\n구조", "치수", "", "", "", "접점\n접촉저항", "접점 간격", "접점\n접촉력", "전환전류", "", "", "", "", "", "", "전환시간", "", "코일저항", "", "", "", "절연저항", "내전압", "" };
                        strHeaders_excel2 = new string[] { "", "", "", "", "A", "B", "C", "D", "", "", "", "전환전류N", "", "", "전환전류R", "", "", "낙하강상비", "", "", "", "보정값(코일저항 전압수신값*A)(코일저항 전류수신값*A)(코일저항+B)", "", "", "", "", "" };
                        strHeaders_excel3 = new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "보정값(전환전류N 수신값*A+B)", "", "", "보정값(전환전류R 수신값*A+B)", "", "", "", "", "", "", "", "", "", "", "" };
                    }
                    else
                    {
                        strHeaders_excel1 = new string[] { "", "", "", "겉모양, 구조", "치수", "", "", "", "접점 접촉저항", "접점 간격", "접점 접촉력", "동작특성", "", "", "", "", "", "", "동작시간(정격)", "", "코일저항", "", "", "", "절연저항", "내전압", "" };
                        strHeaders_excel2 = new string[] { "", "", "", "", "A", "B", "C", "D", "", "", "", "낙하전류", "", "", "최소 동작전류", "", "", "낙하강상비", "", "", "", "보정값(코일저항 전압수신값*A)(코일저항 전류수신값*A)(코일저항+B)", "", "", "", "", "" };
                        strHeaders_excel3 = new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "보정값(낙하전류 수신값*A+B)", "", "", "보정값(동작전류 수신값*A+B)", "", "", "", "", "", "", "", "", "", "", "" };
                    }
                    //헤더에 텍스트 입력
                    for (int c = 0; c < myDataGridView.ColumnCount; c++)
                    {
                        //멀티헤더 1번줄 입력(strHeaders_excel1 읽어와서 순서대로 입력)
                        range = objSheet.get_Range(columns[c] + "1", Missing.Value);
                        range.set_Value(Missing.Value, strHeaders_excel1[c].Trim());
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬
                        range.WrapText = true;  //줄바꿈

                        ////멀티헤더 2번줄(strHeaders_excel2 읽어와서 순서대로 입력)
                        range = objSheet.get_Range(columns[c] + "2", Missing.Value);
                        range.set_Value(Missing.Value, strHeaders_excel2[c].Trim());
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬
                        range.WrapText = true;  //줄바꿈

                        ////멀티헤더 3번줄(strHeaders_excel3 읽어와서 순서대로 입력)
                        range = objSheet.get_Range(columns[c] + "3", Missing.Value);
                        range.set_Value(Missing.Value, strHeaders_excel3[c].Trim());
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬
                        range.WrapText = true;  //줄바꿈

                        //멀티헤더 4번줄(dgv에 멀티헤더중 맨 아래 헤더 가져와서 입력)
                        range = objSheet.get_Range(columns[c] + "4", Missing.Value);
                        range.set_Value(Missing.Value, headers[c].Trim());
                        range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬
                        range.WrapText = true;  //줄바꿈
                    }

                    ////////////////////////////////////////////////////////////////////////////////////




                    myDataGridView.SelectAll();
                    DataObject dataobj = myDataGridView.GetClipboardContent();
                    if (dataobj != null)
                    {
                        Clipboard.SetDataObject(dataobj);
                    }

                    //전체 범위 (왼쪽 상단의 Cell부터 사용한 맨마지막 범위까지)

                    range = objSheet.get_Range("A5", "AA" + (myDataGridView.RowCount + 5).ToString());   //헤더다음부터 5더한 크기
                    range.NumberFormat = "@";   //문자열 서식으로 변경
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬


                    range = objSheet.get_Range("A5", Missing.Value);
                    range.Select();
                    objSheet.PasteSpecial(range, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);



                    myDataGridView.ClearSelection();



                    //////////////////////////////////////////////////////////////////////////////////////

                    /*
                                        for (int i = 0; i < myDataGridView.RowCount; i++)
                                        {
                                            for (int j = 0; j < myDataGridView.ColumnCount; j++)
                                            {
                                                range = objSheet.get_Range(columns[j] + Convert.ToString(i + 5), Missing.Value);
                                                range.NumberFormat = "@";   //문자열 서식으로 변경
                                                range.set_Value(Missing.Value, myDataGridView.Rows[i].Cells[j].Value.ToString());
                                                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬
                                            }
                                        }
*/
                    //셀병합
                    range = objSheet.get_Range("A1", "A4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("B1", "B4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("C1", "C4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("D2", "D4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("E3", "E4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("F3", "F4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("G3", "G4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("H3", "H4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("I2", "I4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    //range = objSheet.get_Range("J2", "K3");
                    //range.Merge(false);
                    //range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("J2", "J4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("K2", "K4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("L3", "L4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("O3", "O4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("R3", "R4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("S2", "S4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("T2", "T4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("U2", "U4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("V2", "X3");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈

                    range = objSheet.get_Range("Y2", "Y4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("Z2", "Z4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("AA1", "AA4");
                    range.Merge(false);
                    range.WrapText = true;  //줄바꿈

                    range = objSheet.get_Range("E1", "H1");
                    range.Merge(true);
                    range.WrapText = true;  //줄바꿈
                    //range = objSheet.get_Range("I1", "K1");
                    //range.Merge(true);
                    //range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("L1", "R1");
                    range.Merge(true);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("S1", "T1");
                    range.Merge(true);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("U1", "X1");
                    range.Merge(true);
                    range.WrapText = true;  //줄바꿈


                    range = objSheet.get_Range("L2", "N2");
                    range.Merge(true);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("O2", "Q2");
                    range.Merge(true);
                    range.WrapText = true;  //줄바꿈


                    range = objSheet.get_Range("M3", "N3");
                    range.Merge(true);
                    range.WrapText = true;  //줄바꿈
                    range = objSheet.get_Range("P3", "Q3");
                    range.Merge(true);
                    range.WrapText = true;  //줄바꿈                

                    int relayCnt = 2;

                    /*제출용은 무조건 N1, R1만 표기하므로 계전기별로 접점갯수 나눌필요 없이 2개로 고정 */
                    //int relayCnt = 0;

                    //if (txtSpec.Text.Substring(0, 3) == "DR3")      //자기유지일경우 접점갯수가 적음
                    //{
                    //    relayCnt = 12;
                    //}
                    //else
                    //{
                    //    relayCnt = 14;
                    //}

                    for (int i = 5; i < myDataGridView.RowCount + 5; i += relayCnt)
                    {
                        range = objSheet.get_Range("A" + Convert.ToString(i), "A" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("B" + Convert.ToString(i), "B" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("D" + Convert.ToString(i), "D" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("E" + Convert.ToString(i), "E" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("F" + Convert.ToString(i), "F" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("G" + Convert.ToString(i), "G" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("H" + Convert.ToString(i), "H" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("J" + Convert.ToString(i), "J" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("K" + Convert.ToString(i), "K" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("L" + Convert.ToString(i), "L" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("M" + Convert.ToString(i), "M" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("N" + Convert.ToString(i), "N" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("O" + Convert.ToString(i), "O" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("P" + Convert.ToString(i), "P" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("Q" + Convert.ToString(i), "Q" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("R" + Convert.ToString(i), "R" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        //range = objSheet.get_Range("S" + Convert.ToString(i), "S" + Convert.ToString(i + relayCnt - 1));
                        //range.Merge(false);
                        //range = objSheet.get_Range("T" + Convert.ToString(i), "T" + Convert.ToString(i + relayCnt - 1));
                        //range.Merge(false);
                        range = objSheet.get_Range("U" + Convert.ToString(i), "U" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("V" + Convert.ToString(i), "V" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("W" + Convert.ToString(i), "W" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("X" + Convert.ToString(i), "X" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("Y" + Convert.ToString(i), "Y" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("Z" + Convert.ToString(i), "Z" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        range = objSheet.get_Range("AA" + Convert.ToString(i), "AA" + Convert.ToString(i + relayCnt - 1));
                        range.Merge(false);
                        //range = objSheet.get_Range("AB" + Convert.ToString(i), "AB" + Convert.ToString(i + relayCnt - 1));
                        //range.Merge(false);
                        //range = objSheet.get_Range("AC" + Convert.ToString(i), "AC" + Convert.ToString(i + relayCnt - 1));
                        //range.Merge(false);
                    }

                    ////////////////////////////성적서 마스터부분 만들기//////////////////////////////

                    //성적서 마스터부분 행추가
                    range = objSheet.get_Range("A1", "AA7");    //행추가
                    range.Insert(Excel.XlInsertShiftDirection.xlShiftDown);

                    range = objSheet.get_Range("A1", Missing.Value);
                    range.set_Value(Missing.Value, "품 명");
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬

                    range = objSheet.get_Range("A2", Missing.Value);
                    range.set_Value(Missing.Value, "규 격");
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬

                    range = objSheet.get_Range("A3", Missing.Value);
                    range.set_Value(Missing.Value, "수 량");
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬

                    range = objSheet.get_Range("A4", Missing.Value);
                    range.set_Value(Missing.Value, "관련규격");
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬

                    range = objSheet.get_Range("S1", Missing.Value);
                    range.set_Value(Missing.Value, "날 짜");
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬

                    range = objSheet.get_Range("S2", Missing.Value);
                    range.set_Value(Missing.Value, "시험자");
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬

                    range = objSheet.get_Range("F1", Missing.Value);
                    range.set_Value(Missing.Value, txtNm.Text + " " + txtSpec.Text);
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬

                    range = objSheet.get_Range("F2", Missing.Value);
                    range.set_Value(Missing.Value, txtReportSpec.Text);
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬

                    range = objSheet.get_Range("F3", Missing.Value);
                    range.set_Value(Missing.Value, txtQty.Text);
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬

                    range = objSheet.get_Range("F4", Missing.Value);
                    range.set_Value(Missing.Value, txtRelSpec.Text);
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬

                    range = objSheet.get_Range("Y1", Missing.Value);
                    range.set_Value(Missing.Value, txtDate.Text);
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬

                    range = objSheet.get_Range("Y2", Missing.Value);
                    range.set_Value(Missing.Value, txtTester.Text);
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;  //정렬

                    range = objSheet.get_Range("A1", "E4");     //성적서 마스터 부분 머지
                    range.Merge(true);

                    range = objSheet.get_Range("F1", "R2");     //성적서 마스터 부분 머지
                    range.Merge(true);

                    range = objSheet.get_Range("F3", "AA4");     //성적서 마스터 부분 머지
                    range.Merge(true);

                    range = objSheet.get_Range("S1", "U2");     //성적서 마스터 부분 머지
                    range.Merge(true);

                    range = objSheet.get_Range("Y1", "AA2");     //성적서 마스터 부분 머지
                    range.Merge(true);

                    //도면1 삽입
                    Bitmap img1 = new Bitmap(pictureBox1.Image);
                    double imgW1 = 0;
                    double imgH1 = 265;
                    imgW1 = Convert.ToDouble(imgH1) * img1.Width / img1.Height;
                    Size resize1 = new Size((int)imgW1, (int)imgH1);
                    Bitmap reimg1 = new Bitmap(img1, resize1);
                    Clipboard.SetImage(reimg1);
                    objApp.ActiveSheet.Paste(objApp.ActiveSheet.Range("G6"));

                    //도면2 삽입
                    Bitmap img2 = new Bitmap(pictureBox2.Image);
                    double imgW2 = 0;
                    double imgH2 = 265;
                    imgW2 = Convert.ToDouble(imgH2) * img2.Width / img2.Height;
                    Size resize2 = new Size((int)imgW2, (int)imgH2);
                    Bitmap reimg2 = new Bitmap(img2, resize2);
                    Clipboard.SetImage(reimg2);
                    objApp.ActiveSheet.Paste(objApp.ActiveSheet.Range("K6"));

                    range = objSheet.get_Range("A5", "AA7");     //도면삽입칸 머지
                    range.Merge(false);


                    range = objSheet.get_Range("A1", "AA" + (myDataGridView.RowCount + 11).ToString());   //헤더를 포함하여 내용이 있는 전체 셀
                    range.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    range.Borders.Weight = Excel.XlBorderWeight.xlThin;




                    ///////////////////////////////////////////////////////////////////////////////


                    if (txtSpec.Text.Substring(0, 3) == "DR3")      //제출용 자기유지일경우 낙하강상비, 복구시간, 보정값 삭제(뒤에서 부터 없애야 순서 계산 안하고 편함)
                    {
                        range = objSheet.get_Range("X1", Missing.Value);    //코일저항 보정값B
                        range.EntireColumn.Delete(Missing.Value);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(range);

                        range = objSheet.get_Range("W1", Missing.Value);    //코일저항 보정값 전류A
                        range.EntireColumn.Delete(Missing.Value);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(range);

                        range = objSheet.get_Range("V1", Missing.Value);    //코일저항 보정값 전압A
                        range.EntireColumn.Delete(Missing.Value);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(range);

                        range = objSheet.get_Range("T1", Missing.Value);    //복구시간
                        range.EntireColumn.Delete(Missing.Value);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(range);

                        range = objSheet.get_Range("R1", Missing.Value);    //낙하강상비
                        range.EntireColumn.Delete(Missing.Value);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(range);

                        range = objSheet.get_Range("Q1", Missing.Value);    //전환전류R 보정값B
                        range.EntireColumn.Delete(Missing.Value);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(range);

                        range = objSheet.get_Range("P1", Missing.Value);    //전환전류R 보정값A
                        range.EntireColumn.Delete(Missing.Value);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(range);

                        range = objSheet.get_Range("N1", Missing.Value);    //전환전류N 보정값B
                        range.EntireColumn.Delete(Missing.Value);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(range);

                        range = objSheet.get_Range("M1", Missing.Value);    //전환전류N 보정값A
                        range.EntireColumn.Delete(Missing.Value);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
                       
                    }
                    else     //제출용 무극, 유극 일경우 보정값 삭제(뒤에서 부터 없애야 순서 계산 안하고 편함)
                    {
                        range = objSheet.get_Range("X1", Missing.Value);    //코일저항 보정값B
                        range.EntireColumn.Delete(Missing.Value);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(range);

                        range = objSheet.get_Range("W1", Missing.Value);    //코일저항 보정값 전류A
                        range.EntireColumn.Delete(Missing.Value);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(range);

                        range = objSheet.get_Range("V1", Missing.Value);    //코일저항 보정값 전압A
                        range.EntireColumn.Delete(Missing.Value);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(range);

                        range = objSheet.get_Range("Q1", Missing.Value);    //동작전류 보정값B
                        range.EntireColumn.Delete(Missing.Value);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(range);

                        range = objSheet.get_Range("P1", Missing.Value);    //동작전류 보정값A
                        range.EntireColumn.Delete(Missing.Value);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(range);

                        range = objSheet.get_Range("N1", Missing.Value);    //낙하전류 보정값B
                        range.EntireColumn.Delete(Missing.Value);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(range);

                        range = objSheet.get_Range("M1", Missing.Value);    //낙하전류 보정값A
                        range.EntireColumn.Delete(Missing.Value);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
                    }


                    //행 높이 조정
                    objApp.ActiveSheet.Rows("1:1").RowHeight = 16.5;
                    objApp.ActiveSheet.Rows("2:2").RowHeight = 16.5;
                    objApp.ActiveSheet.Rows("3:3").RowHeight = 16.5;
                    objApp.ActiveSheet.Rows("4:4").RowHeight = 16.5;
                    objApp.ActiveSheet.Rows("5:5").RowHeight = 5;
                    objApp.ActiveSheet.Rows("6:6").RowHeight = 200;
                    objApp.ActiveSheet.Rows("7:7").RowHeight = 5;
                    objApp.ActiveSheet.Rows("8:8").RowHeight = 30;
                    objApp.ActiveSheet.Rows("9:9").RowHeight = 30;
                    objApp.ActiveSheet.Rows("10:10").RowHeight = 40;
                    objApp.ActiveSheet.Rows("11:11").RowHeight = 16.5;

                    //폰트 사이즈 조정
                    objApp.ActiveSheet.Columns("A:Z").Font.Size = 10;

                    int temp = myDataGridView.RowCount;

                    objApp.DisplayAlerts = true;

                    objApp.Visible = false;
                    objApp.UserControl = false;
                    objBook.SaveAs(@saveFileDialog1.FileName,
                        Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal,
                        missingType, missingType, missingType, missingType,
                        Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                        missingType, missingType, missingType, missingType, missingType);
                    objBook.Close(false, missingType, missingType);
                    objApp.Quit();
                    Marshal.ReleaseComObject(objBook);
                    Marshal.ReleaseComObject(objApp);
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("엑셀 저장이 완료되었습니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FTR106", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        //결과중 정상값 벗어나는것 확인
        private void btnNGCheck_Click(object sender, EventArgs e)
        {
            if (dgvReportDtl.Rows.Count > 0)
            {
                this.Cursor = Cursors.WaitCursor;

                txtContactNG.Text = "-";
                txtDropCurrNG.Text = "-";
                txtOperCurrNG.Text = "-";
                txtRNTimeNG.Text = "-";
                txtNRTimeNG.Text = "-";
                txtResiNG.Text = "-";

                int ContactNGCnt = 0;
                int DropCurrNGCnt = 0;
                int OperCurrNGCnt = 0;
                int RNTimeNGCnt = 0;
                int NRTimeNGCnt = 0;
                int ResiNGCnt = 0;

                for (int i = 0; i < dgvReportDtl.Rows.Count; i++)
                {
                    //접촉저항
                    if (Convert.ToDouble(dgvReportDtl.Rows[i].Cells["Contact"].Value) < Convert.ToDouble(txtCMin.Text) ||
                        Convert.ToDouble(dgvReportDtl.Rows[i].Cells["Contact"].Value) > Convert.ToDouble(txtCMax.Text))
                    {
                        dgvReportDtl.Rows[i].Cells["Contact"].Style.BackColor = Color.Red;
                        ContactNGCnt++;
                    }

                    //낙하전류
                    if (Convert.ToDouble(dgvReportDtl.Rows[i].Cells["DropCurr"].Value) < Convert.ToDouble(txtCDMin.Text) ||
                        Convert.ToDouble(dgvReportDtl.Rows[i].Cells["DropCurr"].Value) > Convert.ToDouble(txtCDMax.Text))
                    {
                        dgvReportDtl.Rows[i].Cells["DropCurr"].Style.BackColor = Color.Red;
                        DropCurrNGCnt++;
                    }

                    //동작전류
                    if (Convert.ToDouble(dgvReportDtl.Rows[i].Cells["OperCurr"].Value) < Convert.ToDouble(txtCOMin.Text) ||
                        Convert.ToDouble(dgvReportDtl.Rows[i].Cells["OperCurr"].Value) > Convert.ToDouble(txtCOMax.Text))
                    {
                        dgvReportDtl.Rows[i].Cells["OperCurr"].Style.BackColor = Color.Red;
                        OperCurrNGCnt++;
                    }

                    //동작시간
                    if(dgvReportDtl.Rows[i].Cells["RNTime"].Value.ToString() != "-")
                        if (Convert.ToDouble(dgvReportDtl.Rows[i].Cells["RNTime"].Value) < Convert.ToDouble(txtTRNMin.Text) ||
                            Convert.ToDouble(dgvReportDtl.Rows[i].Cells["RNTime"].Value) > Convert.ToDouble(txtTRNMax.Text))
                        {
                            dgvReportDtl.Rows[i].Cells["RNTime"].Style.BackColor = Color.Red;
                            RNTimeNGCnt++;
                        }

                    //복구시간
                    if (dgvReportDtl.Rows[i].Cells["NRTime"].Value.ToString() != "-")
                        if (Convert.ToDouble(dgvReportDtl.Rows[i].Cells["NRTime"].Value) < Convert.ToDouble(txtTNRMin.Text) ||
                            Convert.ToDouble(dgvReportDtl.Rows[i].Cells["NRTime"].Value) > Convert.ToDouble(txtTNRMax.Text))
                        {
                            dgvReportDtl.Rows[i].Cells["NRTime"].Style.BackColor = Color.Red;
                            NRTimeNGCnt++;
                        }

                    //코일저항
                    if (Convert.ToDouble(dgvReportDtl.Rows[i].Cells["Resi"].Value) < Convert.ToDouble(txtRMin.Text) ||
                        Convert.ToDouble(dgvReportDtl.Rows[i].Cells["Resi"].Value) > Convert.ToDouble(txtRMax.Text))
                    {
                        dgvReportDtl.Rows[i].Cells["Resi"].Style.BackColor = Color.Red;
                        ResiNGCnt++;
                    }

                }
                txtContactNG.Text = ContactNGCnt.ToString();
                txtDropCurrNG.Text = DropCurrNGCnt.ToString();
                txtOperCurrNG.Text = OperCurrNGCnt.ToString();
                txtRNTimeNG.Text = RNTimeNGCnt.ToString();
                txtNRTimeNG.Text = NRTimeNGCnt.ToString();
                txtResiNG.Text = ResiNGCnt.ToString();
                MessageBox.Show("총 불량 수량은 " + (ContactNGCnt + DropCurrNGCnt + OperCurrNGCnt + RNTimeNGCnt + NRTimeNGCnt + ResiNGCnt).ToString() + " 개 입니다.", "불량체크", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("확인 할 자료가 없습니다.\n 조회 후 다시 시도하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Cursor = Cursors.Default;
        }
    }
}
