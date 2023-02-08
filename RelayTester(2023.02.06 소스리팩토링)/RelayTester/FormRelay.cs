using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO.Ports;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections;
using System.IO;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace RelayTester
{
    public partial class FormRelay : Form
    {
        DbLink Dblink = new DbLink();
        CreateMsg CreateMsg = new CreateMsg();
        ReceiveMsg ReceiveMsg = new ReceiveMsg();
        CommonRelay commonRelay = new CommonRelay();
        FormErrorCodePop errorCodePop = null;  //2022.12.22
        JobResult jobResult;                //2022.12.27
        Serial serial;

        public DataSet mainDS = new DataSet();
        public DataSet schedMstDS = new DataSet();
        public DataSet schedDtlDS = new DataSet();
        public DataSet refValDS = new DataSet();

        Button moveBT = new Button();
        Button nextbT = new Button();

        //작업 상태 구분용
        string jobFlag;

        //에러시 오류코드 및 작업자 업로드 2022.12.26
        string errorCode = null;
        string worker = null;
        string update = null;
        string SchedLoc = null;

        //전송받은 데이터를 위한 큐
        //private Queue msg = new Queue();
        //private ManualResetEvent Controller = new ManualResetEvent(false);
        //private Thread QueueThread = null;
        private Object QueueLockObj = new object();
        //public bool IsAlive { get; private set; }

        //리턴 타임아웃 체크용
        //public int ReturnTimerCnt = 0;
        //public System.Timers.Timer ReturnTimer = new System.Timers.Timer();

        //System.Timers.Timer AgingTimer = new System.Timers.Timer();

        //시간결과 팝업창
        FormTimeResult tr = new FormTimeResult();

        FormMain formMain;
        public FormRelay()
        {
            InitializeComponent();
        }
        //메뉴바에 버튼 받아오기 위해 메인폼 상속받음
        public FormRelay(FormMain _form)
        {
            InitializeComponent();
            formMain = _form;
            errorCodePop = new FormErrorCodePop(this);
            jobResult = new JobResult(this);
            serial = new Serial(this);
            SP1 = new SerialPort();
            
            SP1.DataReceived += new SerialDataReceivedEventHandler(SP1_DataReceived);
        }


        private void FormRelay_Load(object sender, EventArgs e)
        {
            try
            {
                lblColor1.BackColor = Color.FromArgb(150, 165, 42, 42);
                lblColor2.BackColor = Color.FromArgb(150, 0, 0, 139);
                lblColor3.BackColor = Color.FromArgb(150, 0, 128, 0);
                if (txtRelayNum.Text == "01")
                {
                    this.cmbRelayType.DataSource = RelayType("EXEC _SEquipTypeQuery '3'"); ;
                }
                /*else if (txtRelayNum.Text == "02") //2번 시험기 유무극 통합이슈로 인한 무극처리 (23.01.19)
                {
                    this.cmbRelayType.DataSource = RelayType("EXEC _SEquipTypeQuery '4'"); ;
                }*/
                else
                {
                    this.cmbRelayType.DataSource = RelayType("EXEC _SEquipTypeQuery '6'"); //3번 시험기 유무극 통합 (23.01.19)
                }
               
                this.cmbRelayType.DisplayMember = "Code_Dtl_Name";
                this.cmbRelayType.ValueMember = "Code_Dtl";


                this.cmbWorker.DataSource = RelayType("EXEC _SEmpQuery");
                this.cmbWorker.DisplayMember = "EmpName";
                this.cmbWorker.ValueMember = "EmpSeq";
                this.cmbWorker.SelectedIndex = 0;
                
                serial.GetComPort();
                serial.Connection(SP1, lblPortNum.Text, btnDisconnect, btnConnect, lblConnectStatus);

                //////////////////////////////////////////////////////////////////////////////
                for (int i = 1; i <= 30; i++)
                {
                    dgvCurrRst.Rows.Add(i, "", "", null);
                    dgvResiRst.Rows.Add(i, "", "", null);
                    dgvRNTRst.Rows.Add(i, "", "", null);
                    dgvNRTRst.Rows.Add(i, "", "", null);
                }

                CheckForIllegalCrossThreadCalls = false;

                //메시지 수신을 위한 큐 스레드
                /*this.QueueThread = new Thread(this.ExecuteMessage)
                {
                    IsBackground = true,
                    Name = "MessageQueue"
                };
                this.IsAlive = true;
                this.QueueThread.Start();*/

                /*AgingTimer.Interval = 60000; // 1분
                AgingTimer.Elapsed += new System.Timers.ElapsedEventHandler(AgingTimer_Elapsed);

                ReturnTimer.Interval = 1000;
                ReturnTimer.Elapsed += new System.Timers.ElapsedEventHandler(ReturnTimer_Elapsed);*/

                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR201", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

       
        //계전기 종류 콤보박스
        public DataTable RelayType(string sQuery)
        {
            DataSet ds = new DataSet();
            Dblink.AllSelect(sQuery, ds);

            return ds.Tables[0];
        }

        //신규버튼 클릭
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                //마스터 초기화
                foreach (Control ctl in this.grbSetting.Controls)
                {
                    if (ctl.GetType().ToString().Equals("System.Windows.Forms.TextBox") || ctl.GetType().ToString().Equals("System.Windows.Forms.MaskedTextBox") || ctl.GetType().ToString().Equals("System.Windows.Forms.RichTextBox"))
                    {
                        if (ctl.Name != "txtRelayNum")
                            ctl.Text = "";
                    }
                }

                mtxtLot.Enabled = true;

                this.cmbWorker.SelectedIndex = 0;

                //파레트 초기화
                foreach (Control ctl in this.grbPallet.Controls)
                {
                    if (ctl.GetType().ToString().Equals("System.Windows.Forms.Button"))
                    {
                        ctl.Text = "";
                    }
                }
                PalletBtnColor();

                //스케줄 초기화
                foreach (Control ctl in this.grbSched.Controls)
                {
                    if (ctl.GetType().ToString().Equals("System.Windows.Forms.TextBox") || ctl.GetType().ToString().Equals("System.Windows.Forms.MaskedTextBox"))
                    {
                        ctl.Text = "";
                    }
                }
                schedDtlDS.Clear();

                //결과 그리드 초기화
                dgvCurrRst.Rows.Clear();
                dgvResiRst.Rows.Clear();
                dgvRNTRst.Rows.Clear();
                dgvNRTRst.Rows.Clear();
                for (int i = 1; i <= 30; i++)
                {
                    dgvCurrRst.Rows.Add(i, "", "", null);
                    dgvResiRst.Rows.Add(i, "", "", null);
                    dgvRNTRst.Rows.Add(i, "", "", null);
                    dgvNRTRst.Rows.Add(i, "", "", null);
                }
                //작업 초기화
                foreach (Control ctl in this.grbJobCtr.Controls)
                {
                    if (ctl.GetType().ToString().Equals("System.Windows.Forms.TextBox"))
                    {
                        ctl.Text = "";
                    }
                }
                foreach (Control ctl in this.grbTimeResult.Controls)
                {
                    if (ctl.GetType().ToString().Equals("System.Windows.Forms.Label"))
                    {
                        if (ctl.Name.Substring(0, 7) == "lblTime")
                        {
                            ctl.BackColor = SystemColors.Control;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR202", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        //저장 버튼
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //LOT번호 필수체크
                if (this.mtxtLot.Text.Replace("-", "").Trim() == null || this.mtxtLot.Text.Replace("-", "").Trim() == "")
                {
                    MessageBox.Show("Lot번호는 필수 항목 입니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tabControl1.SelectedIndex = 0;
                    mtxtLot.Select();
                    return;
                }

                //계전기종류 필수체크
                if (this.cmbRelayType.SelectedValue == null || this.cmbRelayType.SelectedValue.ToString() == "")
                {
                    MessageBox.Show("계전기종류는 필수 항목 입니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tabControl1.SelectedIndex = 0;
                    this.cmbRelayType.Select();
                    return;
                }

                //작업자 필수체크
                if (this.cmbWorker.SelectedValue == null || this.cmbWorker.SelectedValue.ToString() == "")
                {
                    MessageBox.Show("작업자는 필수 항목 입니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    tabControl1.SelectedIndex = 0;
                    this.cmbWorker.Select();
                    return;
                }

                DataSet tempDS = new DataSet();

                string pQuery = string.Empty;
                pQuery = string.Format("_SLotCheckQuery '{0}', '{1}','{2}', '1'", this.mtxtLot.Text.Replace("-", "").Trim(), int.Parse(txtRelayNum.Text).ToString(), cmbRelayType.Text); //릴레이 타입 추가 2023.01.29

                Dblink.AllSelect(pQuery, tempDS);

                int cnt = Convert.ToInt32(tempDS.Tables[0].Rows[0][0]);

                if (cnt < 1)
                {
                    //마스터 저장 쿼리
                    string pQuery1 = string.Format("EXEC _SJobMasterSave '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','1'"
                        , this.mtxtLot.Text.Replace("-", ""), int.Parse(txtRelayNum.Text).ToString(), this.cmbWorker.SelectedValue, this.cmbRelayType.SelectedValue, this.txtRemark.Text
                        , this.btn01.Text, this.btn02.Text, this.btn03.Text, this.btn04.Text, this.btn05.Text, this.btn06.Text, this.btn07.Text, this.btn08.Text, this.btn09.Text, this.btn10.Text
                        , this.btn11.Text, this.btn12.Text, this.btn13.Text, this.btn14.Text, this.btn15.Text, this.btn16.Text, this.btn17.Text, this.btn18.Text, this.btn19.Text, this.btn20.Text
                        , this.btn21.Text, this.btn22.Text, this.btn23.Text, this.btn24.Text, this.btn25.Text, this.btn26.Text, this.btn27.Text, this.btn28.Text, this.btn29.Text, this.btn30.Text);

                    Dblink.ModifyMethod(pQuery1);
                }
                else
                {
                    string pQuery1 = string.Format("EXEC _SJobMasterSave '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','2'"
                       , this.mtxtLot.Text.Replace("-", ""), int.Parse(txtRelayNum.Text).ToString(), this.cmbWorker.SelectedValue, this.cmbRelayType.SelectedValue, this.txtRemark.Text
                       , this.btn01.Text, this.btn02.Text, this.btn03.Text, this.btn04.Text, this.btn05.Text, this.btn06.Text, this.btn07.Text, this.btn08.Text, this.btn09.Text, this.btn10.Text
                       , this.btn11.Text, this.btn12.Text, this.btn13.Text, this.btn14.Text, this.btn15.Text, this.btn16.Text, this.btn17.Text, this.btn18.Text, this.btn19.Text, this.btn20.Text
                       , this.btn21.Text, this.btn22.Text, this.btn23.Text, this.btn24.Text, this.btn25.Text, this.btn26.Text, this.btn27.Text, this.btn28.Text, this.btn29.Text, this.btn30.Text);

                    Dblink.ModifyMethod(pQuery1);
                }

                mtxtLot.Enabled = false;
                MessageBox.Show("저장이 완료되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR203", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //조회버튼
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                FormLotPop lp = new FormLotPop();

                lp.sRelayNum = int.Parse(txtRelayNum.Text).ToString();

                if (lp.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string pQuery = string.Empty;

                    pQuery = "EXEC _SJobMasterQuery '" + lp.sSeq + "', '" + lp.sType + "', '" + lp.sRelayNum + "', '1'";
                    btnNew_Click(null, null);
                    mainDS.Clear();

                    Dblink.AllSelect(pQuery, mainDS);

                    for (int i = 0; i < mainDS.Tables[0].Rows.Count; i++)
                    {
                        this.mtxtLot.Text = mainDS.Tables[0].Rows[i]["Lot"].ToString();
                        this.txtRelayNum.Text = mainDS.Tables[0].Rows[i]["RelayNum"].ToString();
                        this.cmbWorker.SelectedValue = mainDS.Tables[0].Rows[i]["Worker"].ToString();
                        this.cmbRelayType.SelectedValue = mainDS.Tables[0].Rows[i]["RelayType"].ToString();
                        this.txtRemark.Text = mainDS.Tables[0].Rows[i]["Remark"].ToString();
                        this.btn01.Text = mainDS.Tables[0].Rows[i]["Pallet01"].ToString();
                        this.btn02.Text = mainDS.Tables[0].Rows[i]["Pallet02"].ToString();
                        this.btn03.Text = mainDS.Tables[0].Rows[i]["Pallet03"].ToString();
                        this.btn04.Text = mainDS.Tables[0].Rows[i]["Pallet04"].ToString();
                        this.btn05.Text = mainDS.Tables[0].Rows[i]["Pallet05"].ToString();
                        this.btn06.Text = mainDS.Tables[0].Rows[i]["Pallet06"].ToString();
                        this.btn07.Text = mainDS.Tables[0].Rows[i]["Pallet07"].ToString();
                        this.btn08.Text = mainDS.Tables[0].Rows[i]["Pallet08"].ToString();
                        this.btn09.Text = mainDS.Tables[0].Rows[i]["Pallet09"].ToString();
                        this.btn10.Text = mainDS.Tables[0].Rows[i]["Pallet10"].ToString();
                        this.btn11.Text = mainDS.Tables[0].Rows[i]["Pallet11"].ToString();
                        this.btn12.Text = mainDS.Tables[0].Rows[i]["Pallet12"].ToString();
                        this.btn13.Text = mainDS.Tables[0].Rows[i]["Pallet13"].ToString();
                        this.btn14.Text = mainDS.Tables[0].Rows[i]["Pallet14"].ToString();
                        this.btn15.Text = mainDS.Tables[0].Rows[i]["Pallet15"].ToString();
                        this.btn16.Text = mainDS.Tables[0].Rows[i]["Pallet16"].ToString();
                        this.btn17.Text = mainDS.Tables[0].Rows[i]["Pallet17"].ToString();
                        this.btn18.Text = mainDS.Tables[0].Rows[i]["Pallet18"].ToString();
                        this.btn19.Text = mainDS.Tables[0].Rows[i]["Pallet19"].ToString();
                        this.btn20.Text = mainDS.Tables[0].Rows[i]["Pallet20"].ToString();
                        this.btn21.Text = mainDS.Tables[0].Rows[i]["Pallet21"].ToString();
                        this.btn22.Text = mainDS.Tables[0].Rows[i]["Pallet22"].ToString();
                        this.btn23.Text = mainDS.Tables[0].Rows[i]["Pallet23"].ToString();
                        this.btn24.Text = mainDS.Tables[0].Rows[i]["Pallet24"].ToString();
                        this.btn25.Text = mainDS.Tables[0].Rows[i]["Pallet25"].ToString();
                        this.btn26.Text = mainDS.Tables[0].Rows[i]["Pallet26"].ToString();
                        this.btn27.Text = mainDS.Tables[0].Rows[i]["Pallet27"].ToString();
                        this.btn28.Text = mainDS.Tables[0].Rows[i]["Pallet28"].ToString();
                        this.btn29.Text = mainDS.Tables[0].Rows[i]["Pallet29"].ToString();
                        this.btn30.Text = mainDS.Tables[0].Rows[i]["Pallet30"].ToString();

                    }
                }
                mtxtLot.Enabled = false;
                PalletBtnColor();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR204", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //삭제버튼
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string sLot = mtxtLot.Text.Replace("-", "").Trim();
                if (sLot != "")
                {
                    if (MessageBox.Show("해당 작업을 삭제 하시겠습니까?\r삭제된 데이터는 복구할수 없습니다.", "작업 삭제", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //릴레이타입 조건 추가(2023.01.29)
                        string pQuery = string.Format("EXEC _SJobMasterSave '{0}','{1}','','{2}','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','3'"
                                                        , sLot, txtRelayNum.Text, cmbRelayType.SelectedValue);
                        Dblink.ModifyMethod(pQuery);

                        btnNew_Click(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR204", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //각 파레트 버튼 클릭시 공통 이벤트
        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.mtxtLot.Text.Replace("-", "").Trim().Length == 4)
                {
                    DataSet tempDS = new DataSet();

                    string pQuerytemp = string.Empty;
                    pQuerytemp = string.Format("EXEC _SLotCheckQuery '{0}', '{1}', '{2}'", this.mtxtLot.Text.Replace("-", "").Trim(), this.txtRelayNum.Text, cmbRelayType.Text); //릴레이타입 추가
                    Dblink.AllSelect(pQuerytemp, tempDS);

                    int cnt = Convert.ToInt32(tempDS.Tables[0].Rows[0][0]);

                    if (cnt < 1)
                    {
                        if (MessageBox.Show("기본 설정 내용이 저장되지 않았습니다. 저장후 진행 하시겠습니까?", "저장", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            btnSave_Click(null, null);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("잘못된 LOT번호 또는 LOT번호가 없습니다. 다시 입력해 주세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mtxtLot.Focus();
                    mtxtLot.SelectAll();
                    return;
                }

                /////////////////////////////////////////////////////////접촉저항 바코드 바코드 등록화면으로 이동/////////////////////////////////////////////////////////////////////
                FormContectResi cr = new FormContectResi();
                Button sendbT = (Button)sender;

                if (lblPalletMove.Text == "" || lblPalletMove.Text == null)       //이동이 아닌경우
                {
                    //파레트에 이미 적혀있는 바코드를 팝업에 표시
                    if (sendbT.Text != null)
                    {
                        cr.txtRelayBarcodeSearch.Text = sendbT.Text;
                    }
                    cr.txtPalNum.Text = sendbT.Name.Substring(3, 2);
                    cr.mtxtLot.Text = this.mtxtLot.Text.Replace("-", "").Trim();
                    //cr.tb_testerType.Text = txtRelayNum.Text;
                    cr.tb_worker.Text = cmbWorker.Text;
                Retry:

                    cr.ShowDialog();

                    switch (cr.btnflag)
                    {
                        case "input":   //입력

                            //이미 등록된 바코드 인지 체크
                            foreach (Control ctl in this.grbPallet.Controls)
                            {
                                if (ctl.GetType().ToString().Equals("System.Windows.Forms.Button"))
                                {
                                    if (ctl.Name != sendbT.Name && ctl.Text == cr.txtRelayBarcode.Text)
                                    {
                                        MessageBox.Show("이미 등록된 바코드 입니다. 다시 인식하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        goto Retry;
                                    }
                                }
                            }

                            if (!commonRelay.InputBarcode(cmbRelayType.SelectedValue.ToString(), cr.txtRelayBarcode.Text.Substring(0, 5)))   //공통로직 CommonRelay로 뺌(2022.10.26)
                            {
                                MessageBox.Show("기본 설정의 계전기 종류와 인식한 바코드의 계전기 종류가 다릅니다. 확인 후 다시 인식하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                goto Retry;
                            }
                            /*
                            switch (cmbRelayType.SelectedValue.ToString())
                            {
                                case "01":  //무극
                                    if (cr.txtRelayBarcode.Text.Substring(0, 5) != "DR100")
                                    {
                                        MessageBox.Show("기본 설정의 계전기 종류와 인식한 바코드의 계전기 종류가 다릅니다. 확인 후 다시 인식하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        goto Retry;
                                    }
                                    break;

                                case "02":  //유극
                                    if (cr.txtRelayBarcode.Text.Substring(0, 5) != "DR200")
                                    {
                                        MessageBox.Show("기본 설정의 계전기 종류와 인식한 바코드의 계전기 종류가 다릅니다. 확인 후 다시 인식하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        goto Retry;
                                    }
                                    break;

                                case "03":  //자기유지
                                    if (cr.txtRelayBarcode.Text.Substring(0, 5) != "DR300")
                                    {
                                        MessageBox.Show("기본 설정의 계전기 종류와 인식한 바코드의 계전기 종류가 다릅니다. 확인 후 다시 인식하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        goto Retry;
                                    }
                                    break;

                                case "04":  //유극-저전류
                                    if (cr.txtRelayBarcode.Text.Substring(0, 5) != "DR211")
                                    {
                                        MessageBox.Show("기본 설정의 계전기 종류와 인식한 바코드의 계전기 종류가 다릅니다. 확인 후 다시 인식하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        goto Retry;
                                    }
                                    break;

                                case "05":  //무극-중부하
                                    if (cr.txtRelayBarcode.Text.Substring(0, 5) != "DR130")
                                    {
                                        MessageBox.Show("기본 설정의 계전기 종류와 인식한 바코드의 계전기 종류가 다릅니다. 확인 후 다시 인식하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        goto Retry;
                                    }
                                    break;

                                case "06":  //무극-ABS
                                    if (cr.txtRelayBarcode.Text.Substring(0, 5) != "DR170")
                                    {
                                        MessageBox.Show("기본 설정의 계전기 종류와 인식한 바코드의 계전기 종류가 다릅니다. 확인 후 다시 인식하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        goto Retry;
                                    }
                                    break;

                                case "07":  //무극-PGS
                                    if (cr.txtRelayBarcode.Text.Substring(0, 5) != "DR190")
                                    {
                                        MessageBox.Show("기본 설정의 계전기 종류와 인식한 바코드의 계전기 종류가 다릅니다. 확인 후 다시 인식하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        goto Retry;
                                    }
                                    break;

                                case "08":  //유극-PGS
                                    if (cr.txtRelayBarcode.Text.Substring(0, 5) != "DR290")
                                    {
                                        MessageBox.Show("기본 설정의 계전기 종류와 인식한 바코드의 계전기 종류가 다릅니다. 확인 후 다시 인식하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        goto Retry;
                                    }
                                    break;

                                case "09":  //무극-중부하(1-4)
                                    if (cr.txtRelayBarcode.Text.Substring(0, 5) != "DR131")
                                    {
                                        MessageBox.Show("기본 설정의 계전기 종류와 인식한 바코드의 계전기 종류가 다릅니다. 확인 후 다시 인식하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        goto Retry;
                                    }
                                    break;
                            }
                            */
                            sendbT.Text = cr.txtRelayBarcode.Text;
                            if (chkPalletContinue.Checked)
                            {

                                string btnNum = String.Format("{0:00}", Convert.ToInt32(sendbT.Name.Substring(3, 2)) + 1);

                                if (Convert.ToInt32(btnNum) < 31)
                                {
                                    foreach (Control ctl in this.grbPallet.Controls)
                                    {
                                        if (ctl.GetType().ToString().Equals("System.Windows.Forms.Button"))
                                        {
                                            if (ctl.Name == "btn" + btnNum)
                                            {
                                                nextbT = (Button)ctl;
                                                break;
                                            }
                                        }
                                    }
                                    btn_Click(nextbT, e);
                                }
                                else
                                {
                                    chkPalletContinue.Checked = false;
                                }
                            }
                            break;
                        case "move":    //이동
                            lblPalletMove.Text = cr.txtRelayBarcode.Text;
                            moveBT = (Button)sender;
                            break;
                        case "del":     //삭제
                            sendbT.Text = "";
                            break;
                    }
                }
                else                                                            //이동인 경우
                {
                    string tempPallet = sendbT.Text;
                    sendbT.Text = lblPalletMove.Text;
                    moveBT.Text = tempPallet;

                    lblPalletMove.Text = "";

                }
                PalletBtnColor();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR205", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //파레트 초기화 버튼
        private void btnPalletReset_Click(object sender, EventArgs e)
        {
            try
            {
                //파레트 초기화
                foreach (Control ctl in this.grbPallet.Controls)
                {
                    if (ctl.GetType().ToString().Equals("System.Windows.Forms.Button"))
                    {
                        ctl.Text = "";
                    }
                }
                PalletBtnColor();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR206", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void mtxtLot_Click(object sender, EventArgs e)
        {
            ((MaskedTextBox)sender).SelectAll();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            serial.Connection(SP1, lblPortNum.Text, btnDisconnect, btnConnect, lblConnectStatus);
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            serial.Disconnection(SP1, btnDisconnect, btnConnect, lblConnectStatus);
        }


        private void btnSchedLoad_Click(object sender, EventArgs e)
        {
            try
            {
                FormSchedPop sp = new FormSchedPop();

                if (txtRelayNum.Text == "1")
                {
                    sp.sSched_Type1 = "03";
                    sp.sSched_Type2 = "00";
                }
                /*else if (txtRelayNum.Text == "2")
                 {
                      sp.sSched_Type1 = "01";
                      sp.sSched_Type2 = "05";
                 }
                 else if (txtRelayNum.Text == "3")
                 {
                     sp.sSched_Type1 = "02";
                     sp.sSched_Type2 = "04";
                 }*/
                else if (txtRelayNum.Text == "2")
                {
                    if (cmbRelayType.Text.Contains("무극") || cmbRelayType.Text.Contains("중부하"))
                    {
                        sp.sSched_Type1 = "01"; //무극 시험 
                        sp.sSched_Type2 = "05";
                    }
                    else if (cmbRelayType.Text.Contains("유극"))
                    {
                        sp.sSched_Type1 = "02"; //유극 시험 
                        sp.sSched_Type2 = "04"; //저전류
                    }
                }
                else if (txtRelayNum.Text == "3")
                {
                    if (cmbRelayType.Text.Contains("무극") || cmbRelayType.Text.Contains("중부하"))
                    {
                        sp.sSched_Type1 = "01";
                        sp.sSched_Type2 = "05";
                    }
                    else if (cmbRelayType.Text.Contains("유극"))
                    {
                        sp.sSched_Type1 = "02";
                        sp.sSched_Type2 = "04";
                    }
                }


                if (sp.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string pQuery1 = string.Empty;
                    string pQuery2 = string.Empty;

                    pQuery1 = "EXEC _SScheduleMasterQuery '" + sp.sSeq + "'";
                    schedMstDS.Clear();

                    Dblink.AllSelect(pQuery1, schedMstDS);

                    for (int i = 0; i < schedMstDS.Tables[0].Rows.Count; i++)
                    {
                        this.mtxtSchedSeq.Text = schedMstDS.Tables[0].Rows[i]["Sched_Seq"].ToString();
                        this.txtSchedNm.Text = schedMstDS.Tables[0].Rows[i]["Sched_Name"].ToString();
                        this.chkReportChk.Checked = Convert.ToBoolean(schedMstDS.Tables[0].Rows[i]["ReportChk"]);
                    }

                    pQuery2 = "EXEC _SScheduleDetailQuery '" + sp.sSeq + "'";

                    schedDtlDS.Clear();
                    dgvSchedDtl.DataSource = null;

                    Dblink.AllSelect(pQuery2, schedDtlDS);
                    dgvSchedDtl.DataSource = schedDtlDS.Tables[0];
                    GridResetMethod();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR214", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //스케줄에 체크 헤더 더블클릭시 전체선택, 해제

        private void dgvSchedDtl_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1 && e.ColumnIndex == 1)
                {
                    int cnt = 0;
                    for (int i = 0; i < dgvSchedDtl.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgvSchedDtl.Rows[i].Cells["chk"].Value))
                        {
                            cnt++;
                            break;
                        }
                    }

                    if (cnt > 0)
                    {
                        for (int i = 0; i < dgvSchedDtl.Rows.Count; i++)
                        {
                            dgvSchedDtl.Rows[i].Cells["chk"].Value = false;
                            dgvSchedDtl.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dgvSchedDtl.Rows.Count; i++)
                        {
                            dgvSchedDtl.Rows[i].Cells["chk"].Value = true;
                            dgvSchedDtl.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                }
                dgvSchedDtl.EndEdit();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR217", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //스케줄에 로우 클릭시 체크 되도록
        private void dgvSchedDtl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0 || e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5)
                    {
                        if (Convert.ToBoolean(dgvSchedDtl.Rows[e.RowIndex].Cells["chk"].Value))
                        {
                            dgvSchedDtl.Rows[e.RowIndex].Cells["chk"].Value = false;
                            dgvSchedDtl.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gainsboro;
                        }
                        else
                        {
                            dgvSchedDtl.Rows[e.RowIndex].Cells["chk"].Value = true;
                            dgvSchedDtl.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR218", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnJobStart_Click(object sender, EventArgs e)
        {
            try
            {
                serial.roopCnt = 0;
                jobResult.IncreaseTestNum();

                //작업 저장여부 확인
                if (mtxtLot.Text.Replace("-", "").Trim() == "")
                {
                    MessageBox.Show("작업이 저장되지 않았습니다. 작업 저장후 다시 시도해 주세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //시리얼 통신 연결 확인
                if (!SP1.IsOpen)
                {
                    MessageBox.Show("장비와 연결이 끊어졌습니다.\r장비와 연결 후 다시 시도하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                //스케줄이 있는지 확인
                if (dgvSchedDtl.Rows.Count <= 0)
                {
                    MessageBox.Show("스케줄이 없습니다. 스케줄 조회후 시작할 수 있습니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                int chkCnt = 0;

                for (int i = 0; i < dgvSchedDtl.Rows.Count; i++)        //스케줄 돌면서 계전기 바코드 등록 안되어있는 스케줄 제거
                {
                    //반복횟수가 0인거는 체크 해제
                    if (Convert.ToInt32(dgvSchedDtl.Rows[i].Cells["rpt_cnt"].Value) < 1)
                        dgvSchedDtl.Rows[i].Cells["chk"].Value = false;

                    //에이징이 아닐떄만(에이징일때는 바코드 입력되어있는지 체크 안해도 됨
                    if (dgvSchedDtl.Rows[i].Cells["Test_Type"].Value.ToString() != "99" && dgvSchedDtl.Rows[i].Cells["Test_Type"].Value.ToString() != "98" && dgvSchedDtl.Rows[i].Cells["Test_Type"].Value.ToString() != "90")
                    {
                        foreach (Control ctl in this.grbPallet.Controls)
                        {
                            if (ctl.GetType().ToString().Equals("System.Windows.Forms.Button"))
                            {
                                if (dgvSchedDtl.Rows[i].Cells["Sched_Loc"].Value.ToString() == ctl.Name.ToString().Substring(3, 2))
                                {
                                    if (ctl.Text == "" || ctl.Text == null)
                                        dgvSchedDtl.Rows[i].Cells["chk"].Value = false;
                                }
                            }
                        }
                    }

                    if (Convert.ToBoolean(dgvSchedDtl.Rows[i].Cells["chk"].Value))
                    {
                        dgvSchedDtl.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        chkCnt++;
                    }
                    else
                    {
                        dgvSchedDtl.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                    }

                }

                //체크된게 없으면 작업시작 안함.
                if (chkCnt < 1)
                {
                    MessageBox.Show("진행 가능한 스케줄이 없습니다.\r바코드 입력정보와 반복횟수를 확인하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                btnJobStart.Enabled = false;
                btnJobStop.Enabled = true;
                btnSchedLoad.Enabled = false;
                btnCntClear.Enabled = false;
                btnCali.Enabled = false;
                dgvSchedDtl.Enabled = false;

                dgvCurrRst.ClearSelection();
                dgvResiRst.ClearSelection();
                dgvRNTRst.ClearSelection();
                dgvNRTRst.ClearSelection();

                //시험 기준값 가져오기
                string pQueryTemp = string.Empty;
                pQueryTemp = "EXEC _SRefValQuery";
                Dblink.AllSelect(pQueryTemp, refValDS);

                Add_Log("[작업]작업이 시작되었습니다.");
                //작업진행
                serial.ReturnTimerCnt = 0;

                lblJobStatus.Text = "시험중";
                lblJobStatus.BackColor = Color.GreenYellow;

                jobFlag = "ON";
                Add_Log("start");
                ProcessingJob();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR219", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        public void btnJobStop_Click(object sender, EventArgs e)
        {
            try
            {
                jobFlag = "OFF";

                if (dgvSchedDtl.Rows[dgvSchedDtl.CurrentCellAddress.Y].Cells["Test_Type"].Value.ToString() == "99" || dgvSchedDtl.Rows[dgvSchedDtl.CurrentCellAddress.Y].Cells["Test_Type"].Value.ToString() == "98")     //에이징(시간)일때
                {
                    byte[] bytestosend;

                    string sndMsg = string.Empty;
                    bytestosend = CreateMsg.MsgMain(this.mtxtLot.Text.Replace("-", "").Trim(), this.cmbRelayType.SelectedValue.ToString(), this.txtRelayNum.Text.ToString(), dgvSchedDtl.Rows[dgvSchedDtl.CurrentCellAddress.Y].Cells["Test_Type"].Value.ToString(), "", "0", "", "", "", "stop");

                    //로그 찍기 위해 String으로 만들어 줌.
                    int intRecSize = bytestosend.Length;
                    string[] tempArray = new string[intRecSize];
                    string sendMsg = string.Empty;
                    for (int iTemp = 0; iTemp < intRecSize; iTemp++)
                    {
                        sendMsg += string.Format("{0:X2} ", bytestosend[iTemp]);
                    }
                    sendMsg = sendMsg.Substring(0, sendMsg.Length - 1);

                    txtSendMsg.Text = sendMsg;
                    SP1.Write(bytestosend, 0, bytestosend.Length);

                    Add_Log("PC -> 계전기 : " + sendMsg);

                    if (dgvSchedDtl.Rows[dgvSchedDtl.CurrentCellAddress.Y].Cells["Test_Type"].Value.ToString() == "99")
                        serial.AgingTimer.Stop();
                    else
                        serial.ReturnTimer.Stop();
                }
                else
                {
                    serial.ReturnTimer.Stop();
                }

                Add_Log("[작업]작업을 정지하였습니다.");

                lblJobStatus.Text = "시험대기";
                lblJobStatus.BackColor = Color.Orange;

                btnJobStart.Enabled = true;
                btnJobStop.Enabled = false;
                btnSchedLoad.Enabled = true;
                btnCntClear.Enabled = true;
                btnCali.Enabled = true;
                dgvSchedDtl.Enabled = true;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR220", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //카운트 클리어 버튼
        private void btnCntClear_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytestosend;

                bytestosend = CreateMsg.MsgMain(this.mtxtLot.Text.Replace("-", "").Trim(), this.cmbRelayType.SelectedValue.ToString(), this.txtRelayNum.Text.ToString(), "90", "", "2", "", "", "", "");
                //로그 찍기 위해 스트링으로 만들어 줌.
                int intRecSize = bytestosend.Length;
                string[] tempArray = new string[intRecSize];
                string sendMsg = string.Empty;
                for (int iTemp = 0; iTemp < intRecSize; iTemp++)
                {
                    sendMsg += string.Format("{0:X2} ", bytestosend[iTemp]);
                }
                sendMsg = sendMsg.Substring(0, sendMsg.Length - 1);

                txtSendMsg.Text = sendMsg;
                Add_Log("PC -> 계전기 : " + sendMsg);
                SP1.Write(bytestosend, 0, bytestosend.Length);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR227", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //동작시간, 복구시간 상세 버튼
        private void btnTimeRst_Click(object sender, EventArgs e)
        {
            try
            {
                tr.StartPosition = FormStartPosition.Manual;
                tr.Location = new Point(550, 220);
                tr.ShowDialog();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR228", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //작업결과 리셋 버튼
        private void btnResultClear_Click(object sender, EventArgs e)
        {
            try
            {
                dgvCurrRst.Rows.Clear();
                dgvResiRst.Rows.Clear();
                dgvRNTRst.Rows.Clear();
                dgvNRTRst.Rows.Clear();

                for (int i = 1; i <= 30; i++)
                {
                    dgvCurrRst.Rows.Add(i, "", "", null);
                    dgvResiRst.Rows.Add(i, "", "", null);
                    dgvRNTRst.Rows.Add(i, "", "", null);
                    dgvNRTRst.Rows.Add(i, "", "", null);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR229", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnCali_Click(object sender, EventArgs e)
        {
            try
            {
                FormAdminLogin al = new FormAdminLogin();

                if (al.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FormCali cali = new FormCali();

                    cali.groupBox3.Text = txtRelayNum.Text + "번 시험기";
                    cali.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR230", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        ////////////////// Click 이벤트/////////////////////



        //계전기 종류에따라 파레트 버튼 색깔 변경
        private void PalletBtnColor()
        {
            try
            {
                //파레트 초기화
                foreach (Control ctl in this.grbPallet.Controls)
                {
                    if (ctl.GetType().ToString().Equals("System.Windows.Forms.Button"))
                    {
                        if (ctl.Text.Length > 5)
                        {
                            ctl.BackColor = commonRelay.BtnColor(ctl.Text.Substring(0, 3));     //공통로직 CommonRelay로 뺌(2022.10.26)
                            /*
                            switch (ctl.Text.Substring(0, 3))
                            {
                                case "DR1":  //무극
                                    ctl.BackColor = Color.FromArgb(100, 165, 42, 42);
                                    break;
                                case "TR1":  //무극-테크빌
                                    ctl.BackColor = Color.FromArgb(100, 165, 42, 42);
                                    break;
                                case "DR2":  //유극
                                    ctl.BackColor = Color.FromArgb(100, 0, 0, 139);
                                    break;
                                case "DR3": //자기유지
                                    ctl.BackColor = Color.FromArgb(100, 0, 128, 0);
                            }   
                         */
                        }
                        else
                        {
                            ctl.BackColor = Color.FromArgb(0, 0, 0, 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR207", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //LOT번호에서 포커스 떠날때 LOT번호 체크
        private void mtxtLot_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.mtxtLot.Text.Replace("-", "").Trim().Length == 4)
                {
                    DataSet tempDS = new DataSet();

                    string pQuerytemp = string.Empty;
                    //릴레이타입 조건 추가 2023.01.29
                    pQuerytemp = string.Format("EXEC _SLotCheckQuery '{0}', '{1}','{2}' ,'1'", this.mtxtLot.Text.Replace("-", "").Trim(), int.Parse(this.txtRelayNum.Text), cmbRelayType.SelectedValue);
                    Dblink.AllSelect(pQuerytemp, tempDS);

                    int cnt = Convert.ToInt32(tempDS.Tables[0].Rows[0][0]);

                    if (cnt > 0)
                    {
                        if (MessageBox.Show("이미 등록된 LOT번호 입니다.\r조회 하시겠습니까?", "조회", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            string pQuery = string.Empty;

                            //릴레이타입 조건 추가 2023.01.29
                            pQuery = "EXEC _SJobMasterQuery '" + this.mtxtLot.Text.Replace("-", "").Trim() + "', '" + cmbRelayType.SelectedValue + "', " + int.Parse(this.txtRelayNum.Text) + ", '1'";
                            btnNew_Click(null, null);
                            mainDS.Clear();

                            Dblink.AllSelect(pQuery, mainDS);

                            for (int i = 0; i < mainDS.Tables[0].Rows.Count; i++)
                            {
                                this.mtxtLot.Text = mainDS.Tables[0].Rows[i]["Lot"].ToString();
                                this.txtRelayNum.Text = mainDS.Tables[0].Rows[i]["RelayNum"].ToString();
                                this.cmbWorker.SelectedValue = mainDS.Tables[0].Rows[i]["Worker"].ToString();
                                this.cmbRelayType.SelectedValue = mainDS.Tables[0].Rows[i]["RelayType"].ToString();
                                this.txtRemark.Text = mainDS.Tables[0].Rows[i]["Remark"].ToString();
                                this.btn01.Text = mainDS.Tables[0].Rows[i]["Pallet01"].ToString();
                                this.btn02.Text = mainDS.Tables[0].Rows[i]["Pallet02"].ToString();
                                this.btn03.Text = mainDS.Tables[0].Rows[i]["Pallet03"].ToString();
                                this.btn04.Text = mainDS.Tables[0].Rows[i]["Pallet04"].ToString();
                                this.btn05.Text = mainDS.Tables[0].Rows[i]["Pallet05"].ToString();
                                this.btn06.Text = mainDS.Tables[0].Rows[i]["Pallet06"].ToString();
                                this.btn07.Text = mainDS.Tables[0].Rows[i]["Pallet07"].ToString();
                                this.btn08.Text = mainDS.Tables[0].Rows[i]["Pallet08"].ToString();
                                this.btn09.Text = mainDS.Tables[0].Rows[i]["Pallet09"].ToString();
                                this.btn10.Text = mainDS.Tables[0].Rows[i]["Pallet10"].ToString();
                                this.btn11.Text = mainDS.Tables[0].Rows[i]["Pallet11"].ToString();
                                this.btn12.Text = mainDS.Tables[0].Rows[i]["Pallet12"].ToString();
                                this.btn13.Text = mainDS.Tables[0].Rows[i]["Pallet13"].ToString();
                                this.btn14.Text = mainDS.Tables[0].Rows[i]["Pallet14"].ToString();
                                this.btn15.Text = mainDS.Tables[0].Rows[i]["Pallet15"].ToString();
                                this.btn16.Text = mainDS.Tables[0].Rows[i]["Pallet16"].ToString();
                                this.btn17.Text = mainDS.Tables[0].Rows[i]["Pallet17"].ToString();
                                this.btn18.Text = mainDS.Tables[0].Rows[i]["Pallet18"].ToString();
                                this.btn19.Text = mainDS.Tables[0].Rows[i]["Pallet19"].ToString();
                                this.btn20.Text = mainDS.Tables[0].Rows[i]["Pallet20"].ToString();
                                this.btn21.Text = mainDS.Tables[0].Rows[i]["Pallet21"].ToString();
                                this.btn22.Text = mainDS.Tables[0].Rows[i]["Pallet22"].ToString();
                                this.btn23.Text = mainDS.Tables[0].Rows[i]["Pallet23"].ToString();
                                this.btn24.Text = mainDS.Tables[0].Rows[i]["Pallet24"].ToString();
                                this.btn25.Text = mainDS.Tables[0].Rows[i]["Pallet25"].ToString();
                                this.btn26.Text = mainDS.Tables[0].Rows[i]["Pallet26"].ToString();
                                this.btn27.Text = mainDS.Tables[0].Rows[i]["Pallet27"].ToString();
                                this.btn28.Text = mainDS.Tables[0].Rows[i]["Pallet28"].ToString();
                                this.btn29.Text = mainDS.Tables[0].Rows[i]["Pallet29"].ToString();
                                this.btn30.Text = mainDS.Tables[0].Rows[i]["Pallet30"].ToString();
                            }
                            mtxtLot.Enabled = false;
                            PalletBtnColor();
                        }
                        else
                        {
                            btnNew_Click(null, null);
                        }
                    }
                }
                else if (this.mtxtLot.Text.Replace("-", "").Trim().Length == 0)
                {

                }
                else
                {
                    MessageBox.Show("잘못된 LOT번호 입니다. 다시 입력해 주세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    mtxtLot.Focus();
                    mtxtLot.SelectAll();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR208", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        public void WorkerLoad()
        {
            worker = cmbWorker.Text;
        } //2022.12.27 추가
     


        //PORT1에서 받는 데이터
        private void SP1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                Add_Log("ReadData");
                serial.ReadData(sender,e, SP1);
               
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR209", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

       

        public void ErrorProcess()
        {
            serial.ReturnTimer.Stop(); // T/O체크하는것 멈춤

            //메인 폼 상단에 버튼을 현재 시험기로 표시되도록
            if (txtRelayNum.Text == "1")
            {
                formMain.btnRelayTester1.BackColor = Color.Yellow;
                formMain.btnRelayTester2.BackColor = Color.Transparent;
                formMain.btnRelayTester3.BackColor = Color.Transparent;
            }
            else if (txtRelayNum.Text == "2")
            {
                formMain.btnRelayTester1.BackColor = Color.Transparent;
                formMain.btnRelayTester2.BackColor = Color.Yellow;
                formMain.btnRelayTester3.BackColor = Color.Transparent;
            }
            else if (txtRelayNum.Text == "3")
            {
                formMain.btnRelayTester1.BackColor = Color.Transparent;
                formMain.btnRelayTester2.BackColor = Color.Transparent;
                formMain.btnRelayTester3.BackColor = Color.Yellow;
            }

            this.Activate();    //불량 발생한폼 띄우기
        }
        //받은 메시지 처리
        public void ReceiveData(string msg)
        {
            try
            {
                //23.01.31 Relaytype 추가
                //string cmbrelaytypeValue = cmbRelayType.SelectedValue.ToString();
                String[] rtnArray = ReceiveMsg.RMsgMain(this.mtxtLot.Text.Replace("-", "").Trim(), msg, chkReportChk.Checked, txtTestNum.Text, txtRelayNum.Text, cmbRelayType.SelectedValue.ToString());
               
                

                if (rtnArray[1] != "99")        //에이징(시간)일떄는 입력, 체크 안함
                {
                    if (rtnArray[1] != "98" && rtnArray[1] != "90")         //에이징(횟수), 카운트 클리어 일때는 결과 입력만 안함
                    {
                        if (rbNGSkip.Checked)   //NG발생시 SKIP이 체크되어 있으면
                        {
                            ResultInput(rtnArray);  //불량 발생시
                        }
                        else if (rbNGCheck.Checked)  //NG발생시 확인이 체크되어 있으면
                        {
                            if (!ResultInput(rtnArray))  //불량 발생시
                            {
                                //ErrorProcess(); //에러발생시 처리
                                if (MessageBox.Show(this, txtRelayNum.Text + "번 시험기에서 불량이 발생했습니다.\n계속 진행하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    //계속 진행시 리턴타이머 재가동
                                    serial.ReturnTimerCnt = 0;
                                    serial.ReturnTimer.Start();
                                }
                                else
                                {
                                    //계속 진행 안할경우 작업 종료
                                    btnJobStop_Click(null, null);
                                    return;
                                }
                            }
                        }
                    }
                    if (Convert.ToBoolean(dgvSchedDtl.Rows[dgvSchedDtl.CurrentCellAddress.Y].Cells["chk"].Value))       //현재 줄이 체크되어 있으면
                    {
                        //반복횟수 -1
                        dgvSchedDtl.Rows[dgvSchedDtl.CurrentCellAddress.Y].Cells["Rpt_Cnt"].Value = Convert.ToInt32(dgvSchedDtl.Rows[dgvSchedDtl.CurrentCellAddress.Y].Cells["Rpt_Cnt"].Value) - 1;

                        if (Convert.ToInt32(dgvSchedDtl.Rows[dgvSchedDtl.CurrentCellAddress.Y].Cells["Rpt_Cnt"].Value) <= 0)    //반복횟수가 0이면 체크뺌
                        {
                            dgvSchedDtl.Rows[dgvSchedDtl.CurrentCellAddress.Y].Cells["chk"].Value = false;

                            //Console.Write(dgvSchedDtl.Rows.Count.ToString());

                            if (dgvSchedDtl.Rows.Count == (dgvSchedDtl.CurrentCellAddress.Y) + 1)       //현재 줄이 마지막 줄이면 종료
                            {

                                this.Invoke(new MethodInvoker(delegate ()
                                {
                                    Add_Log("[작업]작업이 완료되었습니다. ReceiveData");
                                   ProcessingJob(); //오류팝업
                                    //MessageBox.Show(this, "작업이 완료되었습니다.", "작업완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }));

                                btnJobStop_Click(null, null);

                                return;
                            }
                        }
                    }
                    System.Threading.Thread.Sleep(2000);
                    Add_Log("sleep");
                    ProcessingJob();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR211", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private bool ResultInput(String[] rtnArray)
        {
            try
            {
                bool ErrorYn = true; //2022.12.27 

                bool result = true;
                bool timeOKNG = true;
                int rowNum;

                string NGPoint = "";

                double max = 0;
                double min = Convert.ToDouble(rtnArray[3]);

                //무극, 유극, 자기유지에 따른 결과값을 가져오기 위해
                string minName1 = string.Empty;
                string maxName1 = string.Empty;
                string minName2 = string.Empty;
                string maxName2 = string.Empty;

                DataSet tempDS = new DataSet();
                
                string RelayType = string.Empty;
                string pQuery = string.Empty;

                string mtxlot = mtxtLot.Text.Replace("-", "").Trim();
                string relaynum = txtRelayNum.Text;
                string cmbrelaytypeValue = cmbRelayType.SelectedValue.ToString();
                //pQuery = "EXEC _SJobMasterQuery '" + this.mtxtLot.Text.Replace("-", "").Trim() + "', '" + String.Format("{0:00}", Convert.ToInt32(rtnArray[2])) + "', '" + this.txtRelayNum.Text + "', '2'";
                 pQuery = string.Format("EXEC _SJobMasterQuery2 '{0}', '{1}', '{2}','{3}', '1'", this.mtxtLot.Text.Replace("-", "").Trim(), SchedLoc, relaynum, cmbrelaytypeValue);
                //string pQuery = string.Format("EXEC _SJobMasterQuery2 '{0}', '{1}', '{2}','{3}', '1'", Lot, SchedLoc, RelayNum, RelayType);

                tempDS.Clear();


                Dblink.AllSelect(pQuery, tempDS);

                RelayType = tempDS.Tables[0].Rows[0][0].ToString().ToUpper().Substring(0, 5);

                switch (rtnArray[1])
                {
                    
                    case "01":  //동작전류

                        rowNum = Convert.ToInt32(rtnArray[2]) - 1;

                        dgvCurrRst.Rows[rowNum].Cells["CurrOperVal"].Value = Math.Round(Convert.ToDouble(rtnArray[3])).ToString(); //화면에 뿌려지는 결과값 정수로 표현 -20170825
                        dgvCurrRst.Rows[rowNum].Cells["CurrDropVal"].Value = Math.Round(Convert.ToDouble(rtnArray[4])).ToString(); //화면에 뿌려지는 결과값 정수로 표현 -20170825

                        //무극, 유극, 자기유지에 따른 결과값을 가져오기 위해
                        string[] rtn1 = commonRelay.ResultColName1(RelayType);     //공통로직 CommonRelay로 뺌(2022.10.26)
                        minName1 = rtn1[0];
                        maxName1 = rtn1[1];
                        minName2 = rtn1[2];
                        maxName2 = rtn1[3];

                        

                        //작업창 하단에 기준값 표시
                        txtCOMin.Text = refValDS.Tables[0].Rows[0][minName1].ToString();
                        txtCOMax.Text = refValDS.Tables[0].Rows[0][maxName1].ToString();
                        txtCDMin.Text = refValDS.Tables[0].Rows[0][minName2].ToString();
                        txtCDMax.Text = refValDS.Tables[0].Rows[0][maxName2].ToString();


                        ///////////////////////////////////////////////////////////////////////////////에러판별 코드
                        int openCnt = 0; //2022.12.27

                        //동작전류 결과값, 양품체크는 반올림 하기 전의 값을 가지고 판단 -20170825
                        if (Convert.ToDouble(rtnArray[3]) >= Convert.ToDouble(refValDS.Tables[0].Rows[0][minName1]) && Convert.ToDouble(rtnArray[3]) <= Convert.ToDouble(refValDS.Tables[0].Rows[0][maxName1]))
                        {
                            dgvCurrRst.Rows[rowNum].Cells["CurrOperVal"].Style.ForeColor = Color.Green;
                        }
                        else
                        {
                            result = false;
                            dgvCurrRst.Rows[rowNum].Cells["CurrOperVal"].Style.ForeColor = Color.Red;
                            errorCode = "E_OpenCurrent"; //2022.12.27
                            openCnt++;

                        }

                        //낙하전류 결과값, 양품체크는 반올림 하기 전의 값을 가지고 판단 -20170825
                        if (Convert.ToDouble(rtnArray[4]) >= Convert.ToDouble(refValDS.Tables[0].Rows[0][minName2]) && Convert.ToDouble(rtnArray[4]) <= Convert.ToDouble(refValDS.Tables[0].Rows[0][maxName2]))
                        {
                            dgvCurrRst.Rows[rowNum].Cells["CurrDropVal"].Style.ForeColor = Color.Green;
                        }
                        else
                        {
                            result = false;
                            dgvCurrRst.Rows[rowNum].Cells["CurrDropVal"].Style.ForeColor = Color.Red;
                            errorCode = "E_DownCurrent"; //2022.12.27
                            openCnt++;
                        }

                        if (openCnt > 1)
                            ErrorYn = false;



                        //결과 적용
                        if (result)
                        {
                            dgvCurrRst.Rows[rowNum].Cells["CurrRst"].Style.BackColor = Color.GreenYellow;
                        }
                        else
                        {
                            dgvCurrRst.Rows[rowNum].Cells["CurrRst"].Style.BackColor = Color.Red;
                        }

                        break;

                    case "02":  //코일저항

                        rowNum = Convert.ToInt32(rtnArray[2]) - 1;

                        dgvResiRst.Rows[rowNum].Cells["ResiVal"].Value = (Math.Round(Convert.ToDouble(rtnArray[5]))).ToString();  //전압, 전류로 저항값 구한것 표시

                        //무극, 유극, 자기유지에 따른 결과값을 가져오기 위해
                        string[] rtn2 = commonRelay.ResultColName2(RelayType);     //공통로직 CommonRelay로 뺌(2022.10.26)
                        minName1 = rtn2[0];
                        maxName1 = rtn2[1];

                        

                        //작업창 하단에 기준값 표시
                        txtRMin.Text = refValDS.Tables[0].Rows[0][minName1].ToString();
                        txtRMax.Text = refValDS.Tables[0].Rows[0][maxName1].ToString();

                        //저항 결과값
                        if (Convert.ToDouble(dgvResiRst.Rows[rowNum].Cells["ResiVal"].Value) >= Convert.ToDouble(refValDS.Tables[0].Rows[0][minName1]) && Convert.ToDouble(dgvResiRst.Rows[rowNum].Cells["ResiVal"].Value) <= Convert.ToDouble(refValDS.Tables[0].Rows[0][maxName1]))
                        {
                            dgvResiRst.Rows[rowNum].Cells["ResiVal"].Style.ForeColor = Color.Green;
                        }
                        else
                        {
                            result = false;
                            dgvResiRst.Rows[rowNum].Cells["ResiVal"].Style.ForeColor = Color.Red;
                           
                        }

                        //결과 적용
                        if (result)
                        {
                            dgvResiRst.Rows[rowNum].Cells["ResiRst"].Style.BackColor = Color.GreenYellow;
                        }
                        else
                        {
                            dgvResiRst.Rows[rowNum].Cells["ResiRst"].Style.BackColor = Color.Red;
                            errorCode = "E_CoilResistance";
                        }
                        break;

                    case "03":  //동작시간

                        rowNum = Convert.ToInt32(rtnArray[2]) - 1;

                        dgvRNTRst.Rows[rowNum].Cells["RNVal"].Value = rtnArray[3];  //첫번째 결과값만 표시


                        //무극, 유극, 자기유지에 따른 결과값을 가져오기 위해
                        string[] rtn3 = commonRelay.ResultColName3(RelayType);     //공통로직 CommonRelay로 뺌(2022.10.26)
                        minName1 = rtn3[0];
                        maxName1 = rtn3[1];

                       

                        //작업창 하단에 기준값 표시
                        txtTRNMin.Text = refValDS.Tables[0].Rows[0][minName1].ToString();
                        txtTRNMax.Text = refValDS.Tables[0].Rows[0][maxName1].ToString();

                        for (int i = 3; i < 13; i++)
                        {
                            timeOKNG = TimeResult("03", i, Convert.ToInt32(rtnArray[i]), Convert.ToInt32(refValDS.Tables[0].Rows[0][minName1]), Convert.ToInt32(refValDS.Tables[0].Rows[0][maxName1]));

                            if (max < Convert.ToInt32(rtnArray[i]))     //결과값중에 제일 큰값을 구함
                                max = Convert.ToInt32(rtnArray[i]);

                            if (Convert.ToInt32(rtnArray[i]) != 0 && min > Convert.ToInt32(rtnArray[i]))    //결과값 중에 제일 작은값을 구함(0은제외)
                                min = Convert.ToInt32(rtnArray[i]);

                            if (!timeOKNG)
                            {
                                NGPoint += "N" + (i - 2).ToString() + ", ";
                                result = false;
                            }
                        }

                        //동작시간 결과값
                        if (Convert.ToInt32(dgvRNTRst.Rows[rowNum].Cells["RNVal"].Value) >= Convert.ToInt32(refValDS.Tables[0].Rows[0][minName1]) && Convert.ToInt32(dgvRNTRst.Rows[rowNum].Cells["RNVal"].Value) <= Convert.ToInt32(refValDS.Tables[0].Rows[0][maxName1]))
                        {
                            dgvRNTRst.Rows[rowNum].Cells["RNVal"].Style.ForeColor = Color.Green;
                        }
                        else
                        {
                            result = false;
                            dgvRNTRst.Rows[rowNum].Cells["RNVal"].Style.ForeColor = Color.Red;
                           
                        }

                        //결과 적용
                        if (result)
                        {
                            if (max - min >= 3)  //최대값과 최소값 찿이가 3ms이상이면 결과가 OK이라도 다르게 표시해줌
                            {
                                dgvRNTRst.Rows[rowNum].Cells["RNRst"].Style.BackColor = Color.Orange;
                                dgvRNTRst.Rows[rowNum].Cells["RNRst"].Value = "3ms이상";
                            }
                            else
                            {
                                dgvRNTRst.Rows[rowNum].Cells["RNRst"].Style.BackColor = Color.GreenYellow;
                            }
                        }
                        else
                        {
                            dgvRNTRst.Rows[rowNum].Cells["RNRst"].Style.BackColor = Color.Red;
                            dgvRNTRst.Rows[rowNum].Cells["RNRst"].Value = NGPoint;
                            errorCode = "E_OpenTime";
                        }
                        break;


                    case "04":  //복구시간

                        rowNum = Convert.ToInt32(rtnArray[2]) - 1;

                        dgvNRTRst.Rows[rowNum].Cells["NRVal"].Value = rtnArray[3];  //첫번째 결과값만 표시

                        //무극, 유극, 자기유지에 따른 결과값을 가져오기 위해
                        string[] rtn4 = commonRelay.ResultColName4(RelayType);     //공통로직 CommonRelay로 뺌(2022.10.26)
                        minName1 = rtn4[0];
                        maxName1 = rtn4[1];

                       

                        //작업창 하단에 기준값 표시
                        txtTNRMin.Text = refValDS.Tables[0].Rows[0][minName1].ToString();
                        txtTNRMax.Text = refValDS.Tables[0].Rows[0][maxName1].ToString();


                        for (int i = 3; i < 13; i++)
                        {
                            timeOKNG = TimeResult("04", i, Convert.ToInt32(rtnArray[i]), Convert.ToInt32(refValDS.Tables[0].Rows[0][minName1]), Convert.ToInt32(refValDS.Tables[0].Rows[0][maxName1]));

                            if (max < Convert.ToInt32(rtnArray[i]))     //결과값중에 제일 큰값을 구함
                                max = Convert.ToInt32(rtnArray[i]);

                            if (Convert.ToInt32(rtnArray[i]) != 0 && min > Convert.ToInt32(rtnArray[i]))    //결과값 중에 제일 작은값을 구함(0은제외)
                                min = Convert.ToInt32(rtnArray[i]);

                            if (!timeOKNG)
                            {
                                NGPoint += "R" + (i - 2).ToString() + ", ";
                                result = false;
                            }

                        }
                        //복구시간 결과값
                        if (Convert.ToInt32(dgvNRTRst.Rows[rowNum].Cells["NRVal"].Value) >= Convert.ToInt32(refValDS.Tables[0].Rows[0][minName1]) && Convert.ToInt32(dgvNRTRst.Rows[rowNum].Cells["NRVal"].Value) <= Convert.ToInt32(refValDS.Tables[0].Rows[0][maxName1]))
                        {
                            dgvNRTRst.Rows[rowNum].Cells["NRVal"].Style.ForeColor = Color.Green;
                        }
                        else
                        {
                            result = false;
                            dgvNRTRst.Rows[rowNum].Cells["NRVal"].Style.ForeColor = Color.Red;
                            
                        }

                        //결과 적용
                        if (result)
                        {
                            if (max - min >= 3)  //최대값과 최소값 찿이가 3ms이상이면 결과가 OK이라도 다르게 표시해줌
                            {
                                dgvNRTRst.Rows[rowNum].Cells["NRRst"].Style.BackColor = Color.Orange;
                                dgvNRTRst.Rows[rowNum].Cells["NRRst"].Value = "3ms이상";
                            }
                            else
                            {
                                dgvNRTRst.Rows[rowNum].Cells["NRRst"].Style.BackColor = Color.GreenYellow;
                            }
                        }
                        else
                        {
                            dgvNRTRst.Rows[rowNum].Cells["NRRst"].Style.BackColor = Color.Red;
                            dgvNRTRst.Rows[rowNum].Cells["NRRst"].Value = NGPoint;
                            errorCode = "E_ReturnTime";
                        }
                        break;
                }

                ErrorCodeUpadte(ErrorYn); //2022.12.27 추가
                errorCode = null;
                return result;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR212", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
        }

        public void ErrorCodeUpadte2() //2022.12.27 추가
        {
            try
            {
                string ErrorUpateQuery = string.Empty;
                ErrorUpateQuery = string.Format("EXEC _FJobResult3 '{0}','{1}' ", ReceiveMsg.ErrorChkSeq, "E_OpenCurrent");
                Dblink.ModifyMethod(ErrorUpateQuery);
                //// 동작전류, 낙하전류 동시 fail인 경우 동작전류 errorCode를 먼저 업데이트한다.

                DataSet ds = new DataSet();

                string JobDtlSearchQry = string.Format("EXEC _FJobResult4 '{0}'", ReceiveMsg.ErrorChkSeq);
                Dblink.AllSelect(JobDtlSearchQry, ds);

                String[] rtnArray = new String[ds.Tables[0].Columns.Count];

                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    rtnArray[i] = ds.Tables[0].Rows[0][i].ToString();
                }

                errorCode = "E_DownCurrent";
                string ErrorCodeInsert = string.Format("EXEC _FJobResult5 '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}' "
                          , rtnArray[1], rtnArray[2], rtnArray[3], rtnArray[4], rtnArray[5], rtnArray[6], rtnArray[7], rtnArray[8], rtnArray[9], rtnArray[10], rtnArray[11], rtnArray[12], rtnArray[13], rtnArray[14], rtnArray[15], rtnArray[16], rtnArray[17], rtnArray[18], rtnArray[19], rtnArray[20], rtnArray[21], rtnArray[22], rtnArray[23], rtnArray[24], rtnArray[25], rtnArray[26], rtnArray[27], rtnArray[28], rtnArray[29], rtnArray[30], rtnArray[31], rtnArray[32], rtnArray[33], rtnArray[34], errorCode, rtnArray[36], rtnArray[37], rtnArray[38], rtnArray[39]
                          );
                Dblink.ModifyMethod(ErrorCodeInsert);
            }
            catch(Exception ex)
            {

            }

        }

        public void ErrorCodeUpadte(bool ErrorYn) //2022.12.27추가
        {
            try
            {
                string ErrorUpateQuery = string.Empty;

                if (ErrorYn) //FAIL 이 하나만 일어난 경우(동작전류, 낙하전류, 코일저항, 동작시간, 복귀시간 등)
                {
                    ErrorUpateQuery = string.Format("EXEC _FJobResult3 '{0}','{1}' ", ReceiveMsg.ErrorChkSeq, errorCode);
                    Dblink.ModifyMethod(ErrorUpateQuery);

                }
                else //동작전류와 낙하전류가 동시에 FAIL 인 경우
                {
                    // SEQ로 _TJOBDTL 테이블 SELECT 하여 데이터값 ARRAY에 저장
                    ErrorCodeUpadte2();
                }

                //seq 찾아서 에러코드만 업데이트하는 프로시져 실행


            }
            catch (Exception ex)
            {
                Add_Log("ErrorCodeUpdate Fail!! \n" + ex.Message);
            }
        } 


        //동작시간, 복구시간 결과 색깔로 표시
        private bool TimeResult(string testType, int num, int val, int min, int max)
        {
            bool result = true;
            try
            {
                //작업화면에 뿌려주는 부분
                foreach (Control ctl in this.grbTimeResult.Controls)
                {
                    if (ctl.GetType().ToString().Equals("System.Windows.Forms.Label"))
                    {
                        if (Convert.ToInt32(ctl.Name.Replace("lblTime", "")) == (num - 2))
                        {
                            if (testType == "03")
                            {
                                ctl.Text = "N" + (num - 2).ToString();
                            }
                            else if (testType == "04")
                            {
                                ctl.Text = "R" + (num - 2).ToString();
                            }

                            if (val >= min && val <= max)
                            {
                                if (testType == "03")
                                {
                                    ctl.BackColor = Color.GreenYellow;
                                }
                                else if (testType == "04")
                                {
                                    ctl.BackColor = Color.Orange;
                                }
                            }
                            else if (val == 0)
                            {
                                ctl.BackColor = SystemColors.Control;
                            }
                            else
                            {
                                ctl.BackColor = Color.Red;
                                result = false;
                            }

                        }
                    }
                }

                //팝업창에 뿌려주는 부분
                if (tr.Created) //팝업이 열려있을때만
                {
                    foreach (Control ctl in tr.Controls)
                    {
                        if (ctl.GetType().ToString().Equals("System.Windows.Forms.Label"))
                        {
                            if (ctl.Name.Substring(0, 5) != "label")    //번호 표시용 라벨이 아니면, 즉 결과값 표시해주는 칸이면
                            {
                                if (Convert.ToInt32(ctl.Name.Replace("lblTime", "")) == (num - 2))
                                {
                                    if (val >= min && val <= max)
                                    {
                                        ctl.Text = val.ToString();
                                        ctl.Visible = true;
                                        ctl.BackColor = Color.GreenYellow;
                                    }
                                    else if (val == 0)
                                    {
                                        ctl.Text = "";
                                        ctl.Visible = false;
                                        ctl.BackColor = SystemColors.Control;
                                    }
                                    else
                                    {
                                        ctl.Text = val.ToString();
                                        ctl.Visible = true;
                                        ctl.BackColor = Color.Red;
                                    }
                                }
                            }
                            else                                        //번호 표시용 라벨이면
                            {
                                if (Convert.ToInt32(ctl.Name.Replace("label", "")) == (num - 2))
                                {
                                    if (testType == "03")
                                    {
                                        ctl.Text = "N" + (num - 2).ToString();
                                    }
                                    else if (testType == "04")
                                    {
                                        ctl.Text = "R" + (num - 2).ToString();
                                    }
                                }
                            }
                        }

                        if (ctl.GetType().ToString().Equals("System.Windows.Forms.PictureBox"))
                        {
                            if (Convert.ToInt32(ctl.Name.Replace("pbTime", "")) == (num - 2))
                            {
                                if (testType == "03")
                                {
                                    if (val == 0)
                                    {
                                        ctl.BackgroundImage = RelayTester.Properties.Resources.grey;
                                    }
                                    else
                                    {
                                        ctl.BackgroundImage = RelayTester.Properties.Resources.green;
                                    }
                                }
                                else if (testType == "04")
                                {
                                    if (val == 0)
                                    {
                                        ctl.BackgroundImage = RelayTester.Properties.Resources.grey;
                                    }
                                    else
                                    {
                                        ctl.BackgroundImage = RelayTester.Properties.Resources.orange;
                                    }
                                }
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR213", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return result;
            }
        }
      
        private void GridResetMethod()
        {
            try
            {
                //체크박스 추가
                if (dgvSchedDtl.Columns.Contains("chk") == true)
                {
                    dgvSchedDtl.Columns.Remove("chk");
                }
                DataGridViewCheckBoxColumn column1 = new DataGridViewCheckBoxColumn();
                {
                    column1.DataPropertyName = "check";
                    column1.FlatStyle = FlatStyle.Standard;
                }

                column1.Name = "chk";
                dgvSchedDtl.Columns.Insert(1, column1);

                //콤보박스 추가
                if (dgvSchedDtl.Columns.Contains("Test_Type") == true)
                {
                    dgvSchedDtl.Columns.Remove("Test_Type");
                }
                AddComboBoxColumns();

                ////컬럼명
                dgvSchedDtl.Columns["chk"].HeaderCell.Value = "체크";
                dgvSchedDtl.Columns["Sched_Ord"].HeaderCell.Value = "순서";
                dgvSchedDtl.Columns["Sched_Loc"].HeaderCell.Value = "위치";
                dgvSchedDtl.Columns["Test_Type"].HeaderCell.Value = "시험종류";
                dgvSchedDtl.Columns["Rpt_Cnt"].HeaderCell.Value = "반복회수";
                dgvSchedDtl.Columns["Add1"].HeaderCell.Value = "추가1";
                dgvSchedDtl.Columns["Add2"].HeaderCell.Value = "추가2";
                dgvSchedDtl.Columns["Add3"].HeaderCell.Value = "추가3";

                ////컬럼 비지블
                dgvSchedDtl.Columns["Sched_Seq"].Visible = false;
                dgvSchedDtl.Columns["Sched_Dtl_Seq"].Visible = false;

                //컬럼 사이즈
                dgvSchedDtl.Columns["chk"].Width = 60;
                dgvSchedDtl.Columns["Sched_Ord"].Width = 70;
                dgvSchedDtl.Columns["Sched_Loc"].Width = 70;
                dgvSchedDtl.Columns["Test_Type"].Width = 100;
                dgvSchedDtl.Columns["Rpt_Cnt"].Width = 100;
                dgvSchedDtl.Columns["Add1"].Width = 100;
                dgvSchedDtl.Columns["Add2"].Width = 100;
                dgvSchedDtl.Columns["Add3"].Width = 100;

                //컬럼 정렬
                dgvSchedDtl.Columns["Sched_Ord"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedDtl.Columns["Sched_Loc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvSchedDtl.Columns["Test_Type"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvSchedDtl.Columns["Rpt_Cnt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedDtl.Columns["Add1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedDtl.Columns["Add2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvSchedDtl.Columns["Add3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                //읽기전용으로 만들기
                for (int i = 0; i < dgvSchedDtl.Rows.Count; i++)
                {
                    dgvSchedDtl.Rows[i].Cells["Sched_Ord"].ReadOnly = true;
                    dgvSchedDtl.Rows[i].Cells["Sched_Loc"].ReadOnly = true;
                    dgvSchedDtl.Rows[i].Cells["Test_Type"].ReadOnly = true;
                    //dgvSchedDtl.Rows[i].Cells["Rpt_Cnt"].ReadOnly = true;
                    dgvSchedDtl.Rows[i].Cells["Add1"].ReadOnly = true;
                    dgvSchedDtl.Rows[i].Cells["Add2"].ReadOnly = true;
                    dgvSchedDtl.Rows[i].Cells["Add3"].ReadOnly = true;
                }

                //체크박스 모두 체크로
                for (int i = 0; i < dgvSchedDtl.Rows.Count; i++)
                {
                    dgvSchedDtl.Rows[i].Cells["chk"].Value = true;
                }

                //정렬 Disable
                foreach (DataGridViewColumn column in dgvSchedDtl.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR215", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //그리드에 칼럼 콤보박스로 만들어주기
        private void AddComboBoxColumns()
        {
            try
            {
                DataGridViewComboBoxColumn column1 = new DataGridViewComboBoxColumn();
                {
                    column1.DataPropertyName = "Test_Type";
                    column1.MaxDropDownItems = 20;
                    column1.FlatStyle = FlatStyle.Popup;
                }
                column1.DataSource = TestType();
                column1.DisplayMember = "Code_Dtl_Name";
                column1.ValueMember = "Code_Dtl";
                column1.Name = "Test_Type";
                dgvSchedDtl.Columns.Insert(4, column1);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR216", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //스케줄 리스트에 시험유형 콤보박스
        public DataTable TestType()
        {
            DataSet ds = new DataSet();
            string pQueryTemp = string.Empty;
            pQueryTemp = "EXEC _STestTypeQuery '','2'";
            Dblink.AllSelect(pQueryTemp, ds);

            return ds.Tables[0];
        }

       

        string loop_chk = string.Empty;
        int rowcnt = 0;
        byte[] bytestosend_process;
        //작업 진행 로직
        public void ProcessingJob()
        {
            Add_Log("1");
            int chkCnt = 0;
            WorkerLoad(); //2022.12.27

            try
            {
                Add_Log("2");
                if (jobFlag == "OFF") return;
                //보내는 메시지를 위한 byte 배열
                //byte[] bytestosend;
                for (int i = 0; i < dgvSchedDtl.Rows.Count; i++)        //스케줄 돌면서
                {
                    Add_Log("3");
                    //dgvSchedDtl.CurrentCell = dgvSchedDtl.Rows[i].Cells["chk"];
                    //dgvSchedDtl.SelectedRows = i;
                    if (Convert.ToBoolean(dgvSchedDtl.Rows[i].Cells["chk"].Value))        //체크되어있는 줄만 실행
                    {
                        Add_Log("4");
                        dgvSchedDtl.CurrentCell = dgvSchedDtl.Rows[i].Cells["chk"];

                        if (Convert.ToInt32(dgvSchedDtl.Rows[i].Cells["Rpt_Cnt"].Value) > 0)    //반복 횟수가 0이 아니면
                        {
                            Add_Log("5");
                            dgvSchedDtl.Rows[i].DefaultCellStyle.BackColor = Color.White;

                            if (dgvSchedDtl.Rows[i].Cells["Test_Type"].Value.ToString() == "90")    //카운트 클리어
                            {
                                Add_Log("6");
                                //계속 진행시 리턴타이머 재가동
                                serial.ReturnTimerCnt = 0;
                                serial.ReturnTimer.Start();
                                bytestosend_process = CreateMsg.MsgMain(this.mtxtLot.Text.Replace("-", "").Trim(), this.cmbRelayType.SelectedValue.ToString(), this.txtRelayNum.Text.ToString(), dgvSchedDtl.Rows[i].Cells["Test_Type"].Value.ToString(), "", "2", "", "", "1", "1");
                            }
                            else if (dgvSchedDtl.Rows[i].Cells["Test_Type"].Value.ToString() == "98")    //에이징(횟수)일때
                            {
                                Add_Log("7");
                                //계속 진행시 리턴타이머 재가동
                                serial.ReturnTimerCnt = 0;
                                serial.ReturnTimer.Start();
                                bytestosend_process = CreateMsg.MsgMain(this.mtxtLot.Text.Replace("-", "").Trim(), this.cmbRelayType.SelectedValue.ToString(), this.txtRelayNum.Text.ToString(), dgvSchedDtl.Rows[i].Cells["Test_Type"].Value.ToString(), "", "1", "", "", "2", "2");
                            }
                            else if (dgvSchedDtl.Rows[i].Cells["Test_Type"].Value.ToString() == "99")    //에이징(시간)일때
                            {
                                Add_Log("8");
                                serial.ReturnTimer.Stop();
                                serial.AgingTimer.Start();
                                bytestosend_process = CreateMsg.MsgMain(this.mtxtLot.Text.Replace("-", "").Trim(), this.cmbRelayType.SelectedValue.ToString(), this.txtRelayNum.Text.ToString(), dgvSchedDtl.Rows[i].Cells["Test_Type"].Value.ToString(), "", "1", "", "", "3", "3");
                            }
                            else    //에이징 아닐때
                            {
                                Add_Log("9");
                                //계속 진행시 리턴타이머 재가동
                                serial.ReturnTimerCnt = 0;
                                serial.ReturnTimer.Start();
                                CurrentJob(dgvSchedDtl.Rows[i].Cells["Test_Type"].Value.ToString(), dgvSchedDtl.Rows[i].Cells["Sched_Loc"].Value.ToString());
                                if(errorCode != null && errorCode.Contains("open") && errorCode.Contains("down")) //2022.12.27
                                {
                                    Add_Log("10");
                                    //두 줄 인서트
                                    errorCode = "E_OpenCurrent";
                                    bytestosend_process = CreateMsg.MsgMain(this.mtxtLot.Text.Replace("-", "").Trim(), this.cmbRelayType.SelectedValue.ToString(), this.txtRelayNum.Text.ToString(), dgvSchedDtl.Rows[i].Cells["Test_Type"].Value.ToString(), dgvSchedDtl.Rows[i].Cells["Sched_Loc"].Value.ToString(), dgvSchedDtl.Rows[i].Cells["Add1"].Value.ToString(), dgvSchedDtl.Rows[i].Cells["Add2"].Value.ToString(), dgvSchedDtl.Rows[i].Cells["Add3"].Value.ToString(), errorCode, worker);
                                    errorCode = "E_DownCurrent";

                                }
                                else if(errorCode != null && errorCode.Contains("open") && !errorCode.Contains("down")) //2022.12.27추가
                                {
                                    Add_Log("11");
                                    //동작전류에러
                                    errorCode = "E_OpenCurrent";
                                    bytestosend_process = CreateMsg.MsgMain(this.mtxtLot.Text.Replace("-", "").Trim(), this.cmbRelayType.SelectedValue.ToString(), this.txtRelayNum.Text.ToString(), dgvSchedDtl.Rows[i].Cells["Test_Type"].Value.ToString(), dgvSchedDtl.Rows[i].Cells["Sched_Loc"].Value.ToString(), dgvSchedDtl.Rows[i].Cells["Add1"].Value.ToString(), dgvSchedDtl.Rows[i].Cells["Add2"].Value.ToString(), dgvSchedDtl.Rows[i].Cells["Add3"].Value.ToString(), errorCode, worker);
                                }
                                else if (errorCode != null && errorCode.Contains("down") && !errorCode.Contains("open")) //2022.12.27추가
                                {
                                    Add_Log("12");
                                    //낙하전류에러
                                    errorCode = "E_DownCurrent";
                                    bytestosend_process = CreateMsg.MsgMain(this.mtxtLot.Text.Replace("-", "").Trim(), this.cmbRelayType.SelectedValue.ToString(), this.txtRelayNum.Text.ToString(), dgvSchedDtl.Rows[i].Cells["Test_Type"].Value.ToString(), dgvSchedDtl.Rows[i].Cells["Sched_Loc"].Value.ToString(), dgvSchedDtl.Rows[i].Cells["Add1"].Value.ToString(), dgvSchedDtl.Rows[i].Cells["Add2"].Value.ToString(), dgvSchedDtl.Rows[i].Cells["Add3"].Value.ToString(), errorCode, worker);
                                }
                                else //2022.12.27추가
                                {
                                    Add_Log("13");
                                    bytestosend_process = CreateMsg.MsgMain(this.mtxtLot.Text.Replace("-", "").Trim(), this.cmbRelayType.SelectedValue.ToString(), this.txtRelayNum.Text.ToString(), dgvSchedDtl.Rows[i].Cells["Test_Type"].Value.ToString(), dgvSchedDtl.Rows[i].Cells["Sched_Loc"].Value.ToString(), dgvSchedDtl.Rows[i].Cells["Add1"].Value.ToString(), dgvSchedDtl.Rows[i].Cells["Add2"].Value.ToString(), dgvSchedDtl.Rows[i].Cells["Add3"].Value.ToString(), errorCode, worker);
                                    SchedLoc = dgvSchedDtl.Rows[i].Cells["Sched_Loc"].Value.ToString();
                                }
                                    
                            }

                            //로그 찍기 위해 스트링으로 만들어 줌.
                            int intRecSize = bytestosend_process.Length;
                            string[] tempArray = new string[intRecSize];
                            string sendMsg = string.Empty;
                            for (int iTemp = 0; iTemp < intRecSize; iTemp++)
                            {
                                sendMsg += string.Format("{0:X2} ", bytestosend_process[iTemp]);
                            }
                            sendMsg = sendMsg.Substring(0, sendMsg.Length - 1);

                            txtSendMsg.Text = sendMsg;
                            Add_Log("PC -> 계전기 : " + sendMsg);
                            SP1.Write(bytestosend_process, 0, bytestosend_process.Length);
                        }
                        else
                        {
                            Add_Log("14");
                            dgvSchedDtl.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                        }
                        break;
                    }
                    else    //체크안되어 있는줄 완료로 표시
                    {
                        Add_Log("15");
                        dgvSchedDtl.Rows[i].DefaultCellStyle.BackColor = Color.Gainsboro;
                    }
                }

                for (int j = 0; j < dgvSchedDtl.Rows.Count; j++)
                {

                    if (Convert.ToBoolean(dgvSchedDtl.Rows[j].Cells["chk"].Value))
                    {
                        chkCnt++;

                    }
                }

                if (chkCnt > 0)
                {
                    Add_Log("16");
                    if (loop_chk.Length == 0)
                        loop_chk = dgvSchedDtl.Rows[0].Cells[5].Value.ToString();

                    if (loop_chk != dgvSchedDtl.Rows[rowcnt].Cells[5].Value.ToString() || rowcnt == dgvSchedDtl.Rows.Count - 1)
                    {
                        Add_Log("17");
                        loop_chk = dgvSchedDtl.Rows[rowcnt].Cells[5].Value.ToString();
                    }

                    loop_chk = dgvSchedDtl.Rows[rowcnt].Cells[5].Value.ToString();
                    
                }

                //4단위로 체크
                rowcnt++;
                //MessageBox.Show("chkCnt      :        " + chkCnt.ToString());
                //if (chkCnt <= 0 && dgvSchedDtl.Rows.Count == (dgvSchedDtl.CurrentCellAddress.Y) + 1)        //체크되어있는거 없으면 작업종료
                if (chkCnt <= 0)
                {
                    Add_Log("18");
                    Add_Log("[작업]작업이 완료되었습니다.");
                    //MessageBox.Show("작업이 완료되었습니다.", "작업완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    btnJobStop_Click(null, null);
                    rowcnt = 0;
                   
                    if (ErrorCodePop.CheckError(dgvCurrRst, dgvResiRst, dgvRNTRst, dgvNRTRst))   //에러가 있을경우 조치코드 입력창 팝업 (2022.12.27)
                    {
                        Add_Log("19");
                        
                        FormErrorCodePop form = new FormErrorCodePop(this);
                        form.ShowDialog();
                    }
                    
                   // btnJobStop_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Add_Log("end");
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR221", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                rowcnt = 0;
            }

        }

        //결과창에 현재 진행되는 로우 표시
        void CurrentJob(string TestType, string Loc)
        {
            try
            {
                //배경색

                for (int i = 0; i < dgvCurrRst.Rows.Count; i++)
                {
                    dgvCurrRst.Rows[i].Cells[0].Style.BackColor = Color.White;
                }

                for (int i = 0; i < dgvResiRst.Rows.Count; i++)
                {
                    dgvResiRst.Rows[i].Cells[0].Style.BackColor = Color.White;
                }

                for (int i = 0; i < dgvRNTRst.Rows.Count; i++)
                {
                    dgvRNTRst.Rows[i].Cells[0].Style.BackColor = Color.White;
                }

                for (int i = 0; i < dgvNRTRst.Rows.Count; i++)
                {
                    dgvNRTRst.Rows[i].Cells[0].Style.BackColor = Color.White;
                }

                int rowNum = Convert.ToInt32(Loc) - 1;

                switch (TestType)
                {
                    case "01":
                        dgvCurrRst.Rows[rowNum].Cells[0].Style.BackColor = Color.Yellow;

                        //내용
                        dgvCurrRst.Rows[rowNum].Cells[1].Value = "";
                        dgvCurrRst.Rows[rowNum].Cells[2].Value = "";
                        dgvCurrRst.Rows[rowNum].Cells[3].Value = "";
                        dgvCurrRst.Rows[rowNum].Cells[3].Style.BackColor = Color.White;

                        //받은메시지 초기화
                        txtRecvMsg.Text = "";
                        break;

                    case "02":
                        dgvResiRst.Rows[rowNum].Cells[0].Style.BackColor = Color.Yellow;

                        //내용
                        dgvResiRst.Rows[rowNum].Cells[1].Value = "";
                        dgvResiRst.Rows[rowNum].Cells[2].Value = "";
                        dgvResiRst.Rows[rowNum].Cells[2].Style.BackColor = Color.White;

                        //받은메시지 초기화
                        txtRecvMsg.Text = "";

                        break;

                    case "03":
                        dgvRNTRst.Rows[rowNum].Cells[0].Style.BackColor = Color.Yellow;
                        //내용
                        System.Threading.Thread.Sleep(1000);
                        dgvRNTRst.Rows[rowNum].Cells[1].Value = "";
                        dgvRNTRst.Rows[rowNum].Cells[2].Value = "";
                        dgvRNTRst.Rows[rowNum].Cells[2].Style.BackColor = Color.White;

                        //받은메시지 초기화
                        txtRecvMsg.Text = "";
                        TimeResult_Reset(TestType);

                        break;

                    case "04":
                        dgvNRTRst.Rows[rowNum].Cells[0].Style.BackColor = Color.Yellow;

                        //내용
                        System.Threading.Thread.Sleep(1000);
                        dgvNRTRst.Rows[rowNum].Cells[1].Value = "";
                        dgvNRTRst.Rows[rowNum].Cells[2].Value = "";
                        dgvNRTRst.Rows[rowNum].Cells[2].Style.BackColor = Color.White;

                        //받은메시지 초기화
                        txtRecvMsg.Text = "";
                        TimeResult_Reset(TestType);
                        break;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR222", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //동작시간, 복구시간 상세 팝업에 초기화
        void TimeResult_Reset(string testType)
        {
            try
            {
                //작업화면에 뿌려주는 부분
                foreach (Control ctl in this.grbTimeResult.Controls)
                {
                    if (ctl.GetType().ToString().Equals("System.Windows.Forms.Label"))
                    {
                        if (ctl.Name.Substring(0, 7) == "lblTime")
                        {
                            ctl.Text = (ctl.Name.Replace("lblTime", "")).ToString();
                            ctl.BackColor = SystemColors.Control;
                        }
                    }
                }

                //팝업창에 뿌려주는 부분
                if (tr.Created) //팝업이 열려있을때만
                {
                    foreach (Control ctl in tr.Controls)
                    {
                        if (ctl.GetType().ToString().Equals("System.Windows.Forms.Label"))
                        {
                            ctl.Visible = true;
                            if (ctl.Name.Substring(0, 5) != "label")    //번호 표시용 라벨이 아니면, 즉 값 표시 칸이면
                            {
                                ctl.Text = "";
                                ctl.BackColor = SystemColors.Window;
                            }
                            else
                            {
                                if (testType == "03")
                                {
                                    ctl.Text = "N" + (ctl.Name.Replace("label", "")).ToString();
                                }
                                else if (testType == "04")
                                {
                                    ctl.Text = "R" + (ctl.Name.Replace("label", "")).ToString();
                                }
                                ctl.BackColor = SystemColors.Control;
                            }
                        }

                        if (ctl.GetType().ToString().Equals("System.Windows.Forms.PictureBox"))
                        {
                            ctl.BackgroundImage = RelayTester.Properties.Resources.grey;
                        }
                    }

                    if (txtRelayNum.Text == "1")
                    {
                        if (testType == "03")
                        {
                            tr.label5.Visible = false;
                            tr.label6.Visible = false;
                            tr.label9.Visible = false;
                            tr.label10.Visible = false;

                            tr.lblTime5.Visible = false;
                            tr.lblTime6.Visible = false;
                            tr.lblTime9.Visible = false;
                            tr.lblTime10.Visible = false;
                        }
                        else if (testType == "04")
                        {
                            tr.label5.Visible = false;
                            tr.label6.Visible = false;
                            tr.label7.Visible = false;
                            tr.label8.Visible = false;

                            tr.lblTime5.Visible = false;
                            tr.lblTime6.Visible = false;
                            tr.lblTime7.Visible = false;
                            tr.lblTime8.Visible = false;
                        }
                    }
                    else
                    {
                        if (testType == "03")
                        {
                            tr.label9.Visible = false;
                            tr.label10.Visible = false;

                            tr.lblTime9.Visible = false;
                            tr.lblTime10.Visible = false;
                        }
                        else if (testType == "04")
                        {
                            tr.label5.Visible = false;
                            tr.label6.Visible = false;
                            tr.label7.Visible = false;
                            tr.label8.Visible = false;

                            tr.lblTime5.Visible = false;
                            tr.lblTime6.Visible = false;
                            tr.lblTime7.Visible = false;
                            tr.lblTime8.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR223", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //로그찍기
        public void Add_Log(string strTLog)
        {
            string strCurTime = string.Format("{0:D4}-{1:D2}-{2:D2} {3:D2}:{4:D2}:{5:D2}",
                                         DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                                         DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            this.Invoke(new MethodInvoker(
                 delegate ()
                 {
                     if (dgvLog.Rows.Count == 30000)
                     {
                         Save_log();
                     }
                     dgvLog.Rows.Add(strCurTime, strTLog);
                     dgvLog.FirstDisplayedScrollingRowIndex = dgvLog.Rows.Count - 1;
                 }
                 )
            );
        }

        //로그 저장 로직
        private void Save_log()
        {
            try
            {
                string date = System.DateTime.Now.ToString("yyyyMMdd");

                DirectoryInfo di = new DirectoryInfo(System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Relay_LOG\\");
                if (di.Exists == false)
                {
                    di.Create();
                }

                //FileInfo fileinfo = new FileInfo(Application.ExecutablePath);
                FileInfo fileinfo = new FileInfo(System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Relay_LOG\" + ".appref-ms");
                FileStream fs = new FileStream(fileinfo.Directory.FullName + @"\Relay_" + txtRelayNum.Text + "_LOG - " + date + ".txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                int count = this.dgvLog.Rows.Count - 1;
                string rowValueString;

                for (int i = 0; i <= count; i++)
                {
                    rowValueString = string.Empty;
                    foreach (DataGridViewCell cell in dgvLog.Rows[i].Cells)
                    {
                        if (cell.Value == null)
                            rowValueString += " | ";
                        else
                            rowValueString += cell.Value.ToString() + "| ";
                    }
                    rowValueString = rowValueString.Remove(rowValueString.LastIndexOf("| "), 2);
                    sw.WriteLine(rowValueString);
                }
                sw.Close();
                fs.Close(); //스트림은 닫아주셔야 합니다.
                dgvLog.Rows.Clear();
                Add_Log("[SYSTEM] 로그가 저장되었습니다.");
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR226", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
       
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            try
            {
                if (tabControl1.SelectedIndex == 0)
                {
                    if (jobFlag == "ON")
                    {
                        MessageBox.Show("작업을 중지하고 다시 시도하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        tabControl1.SelectedIndex = 1;
                        return;
                    }
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    if (this.mtxtLot.Text.Replace("-", "").Trim() == "")
                    {
                        if (MessageBox.Show("작업 설정에 변동 사항이 있습니다.\r저장 후 작업 진행이 가능합니다.\r저장 하시겠습니까?", "작업 저장", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            btnSave_Click(null, null);
                            return;
                        }
                        else
                        {
                            tabControl1.SelectedIndex = 0;
                            return;
                        }
                    }

                    DataSet tempDS = new DataSet();

                    string pQuery = string.Empty;
                    //if (pRelayId != "" && pRelayId != null)
                    //{
                    pQuery = string.Format("EXEC _SJobMasterCheckQuery '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}', '1'"
                        , this.mtxtLot.Text.Replace("-", "").Trim(), this.txtRelayNum.Text, this.cmbWorker.SelectedValue, this.cmbRelayType.SelectedValue, this.txtRemark.Text, this.btn01.Text, this.btn02.Text, this.btn03.Text, this.btn04.Text, this.btn05.Text, this.btn06.Text, this.btn07.Text, this.btn08.Text, this.btn09.Text, this.btn10.Text, this.btn11.Text, this.btn12.Text, this.btn13.Text, this.btn14.Text, this.btn15.Text, this.btn16.Text, this.btn17.Text, this.btn18.Text, this.btn19.Text, this.btn20.Text, this.btn21.Text, this.btn22.Text, this.btn23.Text, this.btn24.Text, this.btn25.Text, this.btn26.Text, this.btn27.Text, this.btn28.Text, this.btn29.Text, this.btn30.Text);
                    //}
                    Dblink.AllSelect(pQuery, tempDS);

                    int cnt = Convert.ToInt32(tempDS.Tables[0].Rows[0][0]);


                    if (cnt < 1)
                    {
                        if (MessageBox.Show("작업 설정에 변동 사항이 있습니다.\r저장 후 작업 진행이 가능합니다.\r저장 하시겠습니까?", "작업 저장", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            btnSave_Click(null, null);
                        }
                        else
                        {
                            tabControl1.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR231", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        
        

        private void FormRelay_FormClosed(object sender, FormClosedEventArgs e)
        {
            serial.Dispose();
            serial.Disconnection(SP1, btnDisconnect, btnConnect, lblConnectStatus);
            Save_log();
            formMain.btnRelayTester1.BackColor = SystemColors.Control;

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            jobResult.LoadTestNum(cmbRelayType.SelectedValue.ToString());
        }

        public void CallSerialWrite()
        {
            SP1.Write(bytestosend_process, 0, bytestosend_process.Length);

        }

        private void mtxtLot_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }
    }

}

