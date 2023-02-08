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
using System.Media;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections;

namespace RelayTester
{
    public partial class FormContectResi : Form
    {
        public string btnflag;
        SoundPlayer _AlertSound = new SoundPlayer(RelayTester.Properties.Resources.Alert);
        public DataSet mainDS = new DataSet();
        public DataSet refValDS = new DataSet();
        CommonRelay commonRelay = new CommonRelay();

        FormRelay formrelay= new FormRelay();

        DbLink Dblink = new DbLink();
        CreateMsg CreateMsg = new CreateMsg();
        ReceiveMsg ReceiveMsg = new ReceiveMsg();


        public ErrorCodePop pop;

        public FormErrorCodePopCon pop2;

        //전송받은 데이터를 위한 큐
        private Queue msg = new Queue();
        private ManualResetEvent Controller = new ManualResetEvent(false);
        private Thread QueueThread = null;
        private Object QueueLockObj = new object();

        public bool IsAlive { get; private set; }

        string jobFlag;
        int msgcnt;
        string msgsum;
        int testNum = 0;
        string seq = string.Empty;

        FormMain formMain;
        public FormContectResi()
        {
            InitializeComponent();
            //pop = new ErrorCodePop(this);
            pop2 = new FormErrorCodePopCon(this);
           
        }
        public FormContectResi(FormMain _form)
        {
            InitializeComponent();
            formMain = _form;
            //pop = new ErrorCodePop(this);
            pop2 = new FormErrorCodePopCon(this);
            
        }

        private void FormContectResi_Load(object sender, EventArgs e)
        {
            try
            {
                TestNum_Load();
                this.cmbRelayType.DataSource = EqType("EXEC _SEquipTypeQuery '2'");
                this.cmbRelayType.DisplayMember = "Code_Dtl_Name";
                this.cmbRelayType.ValueMember = "Code_Dtl";
                this.cmbRelayType.SelectedIndex = 0;

                GetComPort();
                Connecting();

                //메시지 수신을 위한 큐 스레드
                this.QueueThread = new Thread(this.ExecuteMessage)
                {
                    IsBackground = true,
                    Name = "MessageQueue"
                };
                this.IsAlive = true;
                this.QueueThread.Start();

                if (txtRelayBarcodeSearch.Text != null && txtRelayBarcodeSearch.Text != "")
                {
                    QueryEvent();
                }
                else
                {
                    this.ActiveControl = txtRelayBarcodeSearch;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCR101", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Connecting();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            Disconnecting();
        }

        public void btnJobStart_Click(object sender, EventArgs e)
        {
            try
            {
                TestNum_Increase();

                //작업 저장여부 확인
                if (mtxtLot.Text.Replace("-", "").Trim() == "")
                {
                    MessageBox.Show("LOT 번호가 등록되지 않았습니다. 바코드를 다시 읽은 후 시도해 주세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.txtRelayBarcodeSearch.SelectAll();
                    return;
                }
                //시리얼 통신 연결 확인
                if (!SP1.IsOpen)
                {
                    MessageBox.Show("장비와 연결이 끊어졌습니다.\r장비와 연결 후 다시 시도하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.txtRelayBarcodeSearch.SelectAll();
                    return;
                }
                //계전기 종류이 있는지 확인
                if (txtRelayBarcode.Text == null || txtRelayBarcode.Text == "")
                {
                    MessageBox.Show("계전기가 등록되지 않았습니다. 바코드를 다시 읽은 후 시도해 주세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.txtRelayBarcodeSearch.Select();
                    return;
                }
                //계전기 종류이 있는지 확인
                if (cmbRelayType.SelectedValue.ToString() == null || cmbRelayType.SelectedValue.ToString() == "")
                {
                    MessageBox.Show("계전기 종류가 등록되지 않았습니다. 바코드를 다시 읽은 후 시도해 주세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.txtRelayBarcodeSearch.Select();
                    return;
                }
                //접점형태 있는지 확인
                if (txtContectN.Text == null || txtContectN.Text == "")
                {
                    MessageBox.Show("접점형태가 입력되지 않았습니다. 바코드를 다시 읽은 후 시도해 주세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.txtRelayBarcodeSearch.Select();
                    return;
                }

                btnJobStart.Enabled = false;
                btnJobStop.Enabled = true;
                btnBarcode.Enabled = false;
                jobFlag = "ON";

                //시험 기준값 가져오기
                string pQueryTemp = string.Empty;
                pQueryTemp = "EXEC _SRefValQuery";
                Dblink.AllSelect(pQueryTemp, refValDS);

                Add_Log("[작업]작업이 시작되었습니다.");

                foreach (Control ctl in this.grbResult.Controls)
                {
                    if (ctl.GetType().ToString().Equals("System.Windows.Forms.TextBox"))
                    {
                        ctl.Text = "";
                        ctl.BackColor = Color.White;
                    }
                }

                //작업진행
                lblJobStatus.Text = "시험중";
                lblJobStatus.BackColor = Color.GreenYellow;
                ProcessingJob();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCR103", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnJobStop_Click(object sender, EventArgs e)
        {
            try
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    lblJobStatus.Text = "시험대기";
                    lblJobStatus.BackColor = Color.Orange;
                    Add_Log("[작업]작업을 정지하였습니다.");
                    btnJobStart.Enabled = true;
                    btnJobStop.Enabled = false;
                    btnBarcode.Enabled = true;
                    jobFlag = "OFF";
                    this.btnPalletInput.Focus();
                }
                    )
                );
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCR111", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtRelayBarcodeSearch_Click(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void btnPalletInput_Click(object sender, EventArgs e)
        {
            btnflag = "input";
            this.Close();
        }

        private void btnPalletMove_Click(object sender, EventArgs e)
        {
            btnflag = "move";
            this.Close();
        }

        private void btnPalletDel_Click(object sender, EventArgs e)
        {
            btnflag = "del";
            this.Close();
        }

        private void btnBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                /*접점시험기 변경으로 바코드 인식로직도 변경(2022.08.22)
                 
                ////보내는 메시지를 위한 byte 배열
                //byte[] bytestosend = { 0x10, 0x02, 0x00, 0x4A, 0x04, 0x10, 0x03 };

                ////로그 찍기 위해 스트링으로 만들어 줌.
                //int intRecSize = bytestosend.Length;
                //string[] tempArray = new string[intRecSize];
                //string sendMsg = string.Empty;
                //for (int iTemp = 0; iTemp < intRecSize; iTemp++)
                //{
                //    sendMsg += string.Format("{0:X2} ", bytestosend[iTemp]);
                //}
                //sendMsg = sendMsg.Substring(0, sendMsg.Length - 1);

                //txtSendMsg.Text = sendMsg;
                //Add_Log("PC -> 계전기 : " + sendMsg);

                //txtRelayBarcodeSearch.SelectAll();
                //txtRelayBarcodeSearch.Focus();

                //SP1.Write(bytestosend, 0, bytestosend.Length);
                //SP1.Write("\r\n");
                */

                txtRelayBarcodeSearch.SelectAll();
                txtRelayBarcodeSearch.Focus();

                string sendMsg = string.Empty;
                sendMsg = "IO:OUTP 004" + "\r\n";        //트리거 방아쇠 당김
                txtSendMsg.Text = sendMsg;
                Add_Log("PC -> 계전기 : " + sendMsg);
                SP1.Write(sendMsg);

                System.Threading.Thread.Sleep(1000);     //바코드 읽는 시간을 주기 위해 500ms 대기

                sendMsg = string.Empty;
                sendMsg = "IO:OUTP 000" + "\r\n";       //트리거 방아쇠 복구
                txtSendMsg.Text = sendMsg;
                Add_Log("PC -> 계전기 : " + sendMsg);
                SP1.Write(sendMsg);

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCR113", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnCali_Click(object sender, EventArgs e)
        {
            FormAdminLogin al = new FormAdminLogin();

            if (al.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FormSetContectCorrectValue cc = new FormSetContectCorrectValue();
                cc.ShowDialog();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();  //객체생성
            if (ofd.ShowDialog() == DialogResult.OK)  //다이얼로그 창 뜨고 선택
            {
                dgvSetCmd.Rows.Clear();
                dgvSetCmd.Refresh();

                StreamReader rd = new StreamReader(ofd.FileName);

                // 마지막이 될 때까지 루프
                while (!rd.EndOfStream)
                {
                    // 한 라인을 읽는다
                    string line = rd.ReadLine();

                    // 한 라인에 각 컬럼의 데이타를 순서대로 넣는다
                    dgvSetCmd.Rows.Add(line);
                }
                // StreamReader는 사용 후 반드시 닫는다
                rd.Close();
            }
        }

        private void btnSetCmd_Click(object sender, EventArgs e)    //접점저항 장비 세팅용 명령어
        {
            if (jobFlag != "ON")    //작업중이 아닐때만 가능하도록
            {
                for (int i = 0; i < dgvSetCmd.Rows.Count; i++)    //한줄씩 실행
                {
                    string sendMsg = string.Empty;
                    sendMsg = dgvSetCmd.Rows[i].Cells["cmd"].Value.ToString();

                    Add_Log("PC -> 계전기 : " + sendMsg);
                    SP1.Write(sendMsg + "\r\n");
                    System.Threading.Thread.Sleep(100);  //명령어 붙어서 가는걸 방지하기 위해 딜레이를 줌
                }
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            Add_Log("PC -> 계전기 : " + txtSet.Text);
            SP1.Write(txtSet.Text + "\r\n");
            System.Threading.Thread.Sleep(100);  //명령어 붙어서 가는걸 방지하기 위해 딜레이를 줌
        }

        ////////////////////////////////클릭 이벤트///////////////////////////////

        public DataTable EqType(string sQuery)
        {
            DataSet ds = new DataSet();
            Dblink.AllSelect(sQuery, ds);
            return ds.Tables[0];
        }

        private void txtRelayBarcodeSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryEvent();
            }
        }

        private void QueryEvent()
        {
            try
            {
                //초기화
                foreach (Control ctl in this.grbContectResiMst.Controls)
                {
                    if (ctl.GetType().ToString().Equals("System.Windows.Forms.TextBox"))
                    {
                        ctl.Text = "";
                    }
                }
                foreach (Control ctl in this.grbResult.Controls)
                {
                    if (ctl.GetType().ToString().Equals("System.Windows.Forms.TextBox"))
                    {
                        ctl.Text = "";
                    }
                }

                //바코드 길이 체크
                if (txtRelayBarcodeSearch.Text.Length != 14)
                {
                    _AlertSound.Play();
                    MessageBox.Show("바코드 인식에 오류가 있습니다. 다시 인식하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRelayBarcodeSearch.SelectAll();
                    return;
                }
                //바코드 내용 체크
                DataSet tempDS = new DataSet();
                string pQuery = string.Empty;

                pQuery = "EXEC _SRelayQuery '" + txtRelayBarcodeSearch.Text.Substring(0, 5) + "', '1'";
                tempDS.Clear();
                Dblink.AllSelect(pQuery, tempDS);

                if (tempDS.Tables[0].Rows.Count < 1)
                {
                    Console.WriteLine(txtRelayBarcodeSearch.Text.Substring(0, 5));
                    _AlertSound.Play();
                    MessageBox.Show("바코드 인식에 오류가 있습니다. 다시 인식하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRelayBarcodeSearch.SelectAll();
                    return;
                }

                //마스터에 입력
                //mtxtLot.Text = txtRelayBarcodeSearch.Text.Substring(5, 4);  //LOT번호
                txtRelayBarcode.Text = txtRelayBarcodeSearch.Text;  //바코드번호

                DataSet RelayDS = new DataSet();
                string pQueryRelay = string.Empty;

                pQueryRelay = string.Format("EXEC _SRelayQuery '" + txtRelayBarcodeSearch.Text.Substring(0, 5) + "', '3'");
                RelayDS.Clear();
                Dblink.AllSelect(pQueryRelay, RelayDS);

                for (int i = 0; i < RelayDS.Tables[0].Rows.Count; i++)
                {
                    cmbRelayType.SelectedValue = RelayDS.Tables[0].Rows[i]["Code_Dtl"].ToString();  //계전기 종류
                    txtContectN.Text = RelayDS.Tables[0].Rows[i]["ContectU"].ToString();  //접점형태N
                    txtContectR.Text = RelayDS.Tables[0].Rows[i]["ContectD"].ToString();  //접점형태R

                    if (RelayDS.Tables[0].Rows[i]["Img3Name"].ToString() != null && RelayDS.Tables[0].Rows[i]["Img3Name"].ToString() != "")
                    {
                        pictureBox1.Image = ByteArrayToImage((Byte[])RelayDS.Tables[0].Rows[i]["Img3"]);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    
                    Boolean[] rtn = commonRelay.ContactResiPoint(RelayDS.Tables[0].Rows[i]["RelayCode"].ToString());     //공통로직 CommonRelay로 뺌(2022.10.26)

                    txtN1.Visible = rtn[0];
                    txtN2.Visible = rtn[1];
                    txtN3.Visible = rtn[2];
                    txtN4.Visible = rtn[3];
                    txtN5.Visible = rtn[4];
                    txtN6.Visible = rtn[5];
                    txtN7.Visible = rtn[6];
                    txtN8.Visible = rtn[7];
                    txtR1.Visible = rtn[8];
                    txtR2.Visible = rtn[9];
                    txtR3.Visible = rtn[10];
                    txtR4.Visible = rtn[11];
                    txtR9.Visible = rtn[12];
                    txtR10.Visible = rtn[13];

                   
                }

                txtRelayBarcodeSearch.Text = string.Empty;
                btnJobStart.Focus();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCR102", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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

        private void Connecting()
        {
            try
            {
                if (lblPortNum.Text != "" && lblPortNum.Text != null)
                {
                    if (!SP1.IsOpen)
                    {
                        SP1 = new SerialPort();
                        SP1.DataReceived += new SerialDataReceivedEventHandler(SP1_DataReceived);

                        SP1.PortName = lblPortNum.Text;
                        SP1.BaudRate = 9600;
                        SP1.DataBits = (int)8;
                        SP1.Parity = Parity.None;
                        SP1.StopBits = StopBits.One;
                        SP1.Encoding = System.Text.Encoding.GetEncoding(1252);
                        SP1.ReadTimeout = (int)50000;
                        SP1.WriteTimeout = (int)5000;
                        SP1.Open();

                        if (SP1.IsOpen)
                        {
                            btnDisconnect.Enabled = true;
                            btnConnect.Enabled = false;

                            lblConnectStatus.Text = "CONNECTED";
                            lblConnectStatus.BackColor = Color.GreenYellow;
                            //MessageBox.Show("[" + SP1.PortName.ToString() + "] CONNECTED");
                        }
                    }
                    else
                    {
                        MessageBox.Show("이미 연결되어 있습니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("PORT 지정이 되어있지 않습니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCR105", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void Disconnecting()
        {
            try
            {
                SP1.Close();

                btnDisconnect.Enabled = false;
                btnConnect.Enabled = true;

                lblConnectStatus.Text = "DISCONNECTED";
                lblConnectStatus.BackColor = Color.Red;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCR106", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void GetComPort()
        {
            try
            {
                string rtnPort;
                DataSet ds = new DataSet();
                string pQueryTemp = string.Empty;
                pQueryTemp = "EXEC _SCommonCodeQuery 'CM004', '04', '1'";
                Dblink.AllSelect(pQueryTemp, ds);

                rtnPort = ds.Tables[0].Rows[0]["Remark"].ToString();

                lblPortNum.Text = rtnPort;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCR107", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void ProcessingJob()    //접촉저항, 접촉저항기 시힘기 변경으로 인해 개별 로직 타도록 로직 수정함(2022.02.16)
        {
            try
            {
                msgcnt = 0;
                msgsum = "";
                //보내는 메시지를 위한 byte 배열
                byte[] bytestosend;
                bytestosend = CreateMsg.MsgMain(this.mtxtLot.Text.Replace("-", "").Trim(), this.cmbRelayType.SelectedValue.ToString(), "00", "05", "00","","",txtRelayBarcode.Text, "", tb_worker.Text);


                string sendMsg = string.Empty;
                SP1.Write("\r\n");
                sendMsg = "IO:OUTP 001" + "\r\n";       //N접점 시험 명령어
                txtSendMsg.Text = sendMsg;
                Add_Log("PC -> 계전기 : " + sendMsg);
                SP1.Write(sendMsg);

                System.Threading.Thread.Sleep(100);     //준비시간을 주기 위해 100ms 대기

                //N접점 포트만 열어주기
                SP1.Write("ch:stat on,2" + "\r\n");
                SP1.Write("ch:stat on,3" + "\r\n");
                SP1.Write("ch:stat on,4" + "\r\n");
                SP1.Write("ch:stat on,5" + "\r\n");
                SP1.Write("ch:stat on,6" + "\r\n");
                SP1.Write("ch:stat on,7" + "\r\n");
                SP1.Write("ch:stat on,8" + "\r\n");
                SP1.Write("ch:stat on,9" + "\r\n");
                SP1.Write("ch:stat on,10" + "\r\n");
                SP1.Write("ch:stat on,11" + "\r\n");
                SP1.Write("ch:stat off,12" + "\r\n");
                SP1.Write("ch:stat off,13" + "\r\n");
                SP1.Write("ch:stat off,14" + "\r\n");
                SP1.Write("ch:stat off,15" + "\r\n");
                SP1.Write("ch:stat off,16" + "\r\n");
                SP1.Write("ch:stat off,17" + "\r\n");
                SP1.Write("ch:stat off,18" + "\r\n");
                SP1.Write("ch:stat off,19" + "\r\n");
                SP1.Write("ch:stat off,20" + "\r\n");
                SP1.Write("ch:stat off,21" + "\r\n");

                sendMsg = string.Empty;
                sendMsg = "READ?" + "\r\n";     //시험 명령
                txtSendMsg.Text = sendMsg;
                Add_Log("PC -> 계전기 : " + sendMsg);
                SP1.Write(sendMsg);


            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCR104", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        } 

        //PORT1에서 받는 데이터
        private void SP1_DataReceived(object sender, SerialDataReceivedEventArgs e) //접촉저항, 접촉저항기 시힘기 변경으로 인해 개별 로직 타도록 로직 수정함(2022.02.16)
        {
            try
            {
                System.Threading.Thread.Sleep(50);
                if (SP1.IsOpen)
                {
                    int intRecSize = SP1.BytesToRead;
                    string strRecData;

                    if (intRecSize != 0)
                    {
                        System.Threading.Thread.Sleep(500); //끊어 읽어오는 증상때문에 전체 다 올떄까지 기다림
                        strRecData = string.Empty;
                        strRecData = SP1.ReadExisting();
   
                        this.Invoke(new MethodInvoker(delegate()
                                {
                                    txtRecvMsg.Text = strRecData;
                                }
                           )
                        );

                        Add_Log("계전기 -> PC : " + strRecData);
                        
                        if (jobFlag == "ON")
                        {
                            Receive_Que(strRecData); //메시지 수신시 큐로
                        }
                        
                    }

                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCR108", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        #region 큐큐큐큐큐
        public void Receive_Que(string que_data)
        {
            // Queue 에 Command 를 Enqueue

            this.msg.Enqueue(que_data);

            // 대기상태에 빠져 있을지 모르니, Thread 를 동작하게 만듦
            this.Controller.Set();
        }

        private void ExecuteMessage()
        {
            try
            {
                while (IsAlive)
                {
                    // Queue 에 있는 모든 Command 를 수행

                    while (this.msg.Count > 0)
                    {
                        string command = this.msg.Dequeue().ToString();
                        //ReceiveData(command);
                        Check_msg(command);
                    }
                    // 다 수행하고 나면, 대기상태로 진입
                    this.Controller.Reset();
                    this.Controller.WaitOne(Timeout.Infinite);
                }
            }
            catch (ThreadAbortException) { }
        }

        public void Dispose()
        {
            this.IsAlive = false;
            this.Controller.Set();
            this.QueueThread.Abort();
        }
        #endregion

        public void Check_msg(string msg)   //접촉저항, 접촉저항기 시힘기 변경으로 인해 개별 로직 타도록 로직 수정함(2022.02.16)
        {
            try
            {
                msgcnt++;       //N접점 or R접점 시험 끝나면 cnt 1증가
                msgsum += msg;  //N접점 시험에 R접점 시험 결과값 합치기
                if (msgcnt < 2)     //N시험만 끝난상태에서는 msgcnt가 1이기 때문에 아래 진입
                {
                    msgsum += ", "; //구분자 추가

                    string sendMsg = string.Empty;
                    if(txtRelayBarcode.Text.Substring(0, 5) != "DR300")     //자기유지가 아니면(무극, 유극)
                        sendMsg = "IO:OUTP 000" + "\r\n";       //R접점 시험 명령어
                    else                                                    //자기유지이면
                        sendMsg = "IO:OUTP 002" + "\r\n";       //R접점 시험 명령어
                    txtSendMsg.Text = sendMsg;
                    Add_Log("PC -> 계전기 : " + sendMsg);
                    SP1.Write(sendMsg);

                    System.Threading.Thread.Sleep(300);     //준비시간을 주기 위해 300ms 대기(자기유지 대기시간이랑 맞추기위해 전부다 300ms으로 변경)

                    //R접점 포트만 열어주기
                    SP1.Write("ch:stat off,2" + "\r\n");
                    SP1.Write("ch:stat off,3" + "\r\n");
                    SP1.Write("ch:stat off,4" + "\r\n");
                    SP1.Write("ch:stat off,5" + "\r\n");
                    SP1.Write("ch:stat off,6" + "\r\n");
                    SP1.Write("ch:stat off,7" + "\r\n");
                    SP1.Write("ch:stat off,8" + "\r\n");
                    SP1.Write("ch:stat off,9" + "\r\n");
                    SP1.Write("ch:stat off,10" + "\r\n");
                    SP1.Write("ch:stat off,11" + "\r\n");
                    SP1.Write("ch:stat on,12" + "\r\n");
                    SP1.Write("ch:stat on,13" + "\r\n");
                    SP1.Write("ch:stat on,14" + "\r\n");
                    SP1.Write("ch:stat on,15" + "\r\n");
                    SP1.Write("ch:stat on,16" + "\r\n");
                    SP1.Write("ch:stat on,17" + "\r\n");
                    SP1.Write("ch:stat on,18" + "\r\n");
                    SP1.Write("ch:stat on,19" + "\r\n");
                    SP1.Write("ch:stat on,20" + "\r\n");
                    SP1.Write("ch:stat on,21" + "\r\n");

                    sendMsg = string.Empty;
                    sendMsg = "READ?" + "\r\n";     //시험 명령
                    txtSendMsg.Text = sendMsg;
                    Add_Log("PC -> 계전기 : " + sendMsg);
                    SP1.Write(sendMsg);
                }
                else        //두번째 결과값 까지 받으면 진행
                {
                    ReceiveData(msgsum);
                    Add_Log("receivedata");
                } 
                   
            }
            catch (Exception ex)
            {
                Add_Log("error");
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCR109", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        public void ReceiveData(string msg) //접촉저항, 접촉저항기 시힘기 변경으로 인해 개별 로직 타도록 로직 수정함(2022.02.16)
        {
            msg = "10 02 00 72" + msg + " 10 03";       //stx, etx 붙여줌

            String[] rtnArray = ReceiveMsg.RMsgMain(this.mtxtLot.Text.Replace("-", "").Trim(), msg, true,txtTestNum.Text, tb_testerType.Text, cmbRelayType.SelectedValue.ToString());
            Add_Log("rsgmain");
            ResultInput(rtnArray);
            Add_Log("resultinput");
        }

        private void ResultInput(String[] rtnArray)
        {
            try { 
                
                //무극, 유극, 자기유지에 따른 결과값을 가져오기 위해
                string minName1 = string.Empty;
                string maxName1 = string.Empty;
                string minName2 = string.Empty;
                string maxName2 = string.Empty;


                //무극, 유극, 자기유지에 따른 결과값을 가져오기 위해
                string[] rtn5 = commonRelay.ResultColName5(txtRelayBarcode.Text.Substring(0, 5));     //공통로직 CommonRelay로 뺌(2022.10.26)
                minName1 = rtn5[0];
                maxName1 = rtn5[1];

                
                this.Invoke(new MethodInvoker(delegate ()
                        {
                            txtN1.Text = Math.Round(Convert.ToDouble(rtnArray[3])).ToString();
                            txtN2.Text = Math.Round(Convert.ToDouble(rtnArray[4])).ToString();
                            txtN3.Text = Math.Round(Convert.ToDouble(rtnArray[5])).ToString();
                            txtN4.Text = Math.Round(Convert.ToDouble(rtnArray[6])).ToString();
                            txtN5.Text = Math.Round(Convert.ToDouble(rtnArray[7])).ToString();
                            txtN6.Text = Math.Round(Convert.ToDouble(rtnArray[8])).ToString();
                            txtN7.Text = Math.Round(Convert.ToDouble(rtnArray[9])).ToString();
                            txtN8.Text = Math.Round(Convert.ToDouble(rtnArray[10])).ToString();
                            //txtN9.Text = rtnArray[11];
                            //txtN10.Text = rtnArray[12];
                            txtR1.Text = Math.Round(Convert.ToDouble(rtnArray[13])).ToString();
                            txtR2.Text = Math.Round(Convert.ToDouble(rtnArray[14])).ToString();
                            txtR3.Text = Math.Round(Convert.ToDouble(rtnArray[15])).ToString();
                            txtR4.Text = Math.Round(Convert.ToDouble(rtnArray[16])).ToString();
                            //txtR5.Text = rtnArray[17];
                            //txtR6.Text = rtnArray[18];
                            //txtR7.Text = rtnArray[19];
                            //txtR8.Text = rtnArray[20];
                            txtR9.Text = Math.Round(Convert.ToDouble(rtnArray[21])).ToString();
                            txtR10.Text = Math.Round(Convert.ToDouble(rtnArray[22])).ToString();

                            foreach (Control ctl in this.grbResult.Controls)
                            {
                                if (ctl.GetType().ToString().Equals("System.Windows.Forms.TextBox"))
                                {
                                    if (Convert.ToDouble(ctl.Text) >= Convert.ToDouble(refValDS.Tables[0].Rows[0][minName1]) && Convert.ToDouble(ctl.Text) <= Convert.ToDouble(refValDS.Tables[0].Rows[0][maxName1]))
                                    {
                                        ctl.BackColor = Color.GreenYellow;
                                    }
                                    else
                                    {
                                        ctl.BackColor = Color.Red;
                                    }
                                }
                            }

                            seq = ReceiveMsg.ErrorChkSeq;
                            //여기서 에러체크하고 폼 띄움
                            //ErrorCheck2()

                           
                        }
                    )
                );

                this.Invoke(new MethodInvoker(delegate ()
                        {
                            Add_Log("[작업]작업이 완료되었습니다.");
                        //MessageBox.Show(this, "작업이 완료되었습니다.", "작업완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                   )
                );

                btnJobStop_Click(null, null);

                UpadteErrorCode();
                
                pop = new ErrorCodePop(this);
                pop.CheckErrorContectResis(seq);

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCR110", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        public void UpadteErrorCode() //2022.12.27추가
        {
            try
            {
                string ErrorUpateQuery = string.Empty;

                string errorCode = "E_ConResistance";

                ErrorUpateQuery = string.Format("EXEC _FJobResult3 '{0}','{1}' ", ReceiveMsg.ErrorChkSeq, errorCode);
                Dblink.ModifyMethod(ErrorUpateQuery);
                //seq 찾아서 에러코드만 업데이트하는 프로시져 실행


            }
            catch (Exception ex)
            {
                Add_Log("ErrorCodeUpdate Fail!! \n" + ex.Message);
            }
        }

        //로그찍기
        public void Add_Log(string strTLog)
        {
            string strCurTime = string.Format("{0:D4}-{1:D2}-{2:D2} {3:D2}:{4:D2}:{5:D2}",
                                         DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                                         DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            this.Invoke(new MethodInvoker(
                 delegate()
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
                FileStream fs = new FileStream(fileinfo.Directory.FullName + @"\Relay_0_LOG - " + date + ".txt", FileMode.Append);
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
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCR112", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //버튼 포커스 인 공통
        private void btnFocus_Enter(object sender, EventArgs e)
        {
            Button sendbT = (Button)sender;

            sendbT.BackColor = Color.Yellow;
        }

        //버튼 포커스 아웃 공통
        private void btnFocus_Leave(object sender, EventArgs e)
        {
            Button sendbT = (Button)sender;

            sendbT.BackColor = Color.Transparent;
        }

        private void FormContectResi_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
            Disconnecting();
            Save_log();
        }

        public void TestNum_Load()
        {
            //DB에서 셀렉트해서 테스트넘버 로드할 것.

            DataSet ds = new DataSet();

            int testNum = 0;
            string testType = "06";

            string date = DateTime.Now.ToString("yyyyMMdd");

            string TestNumLoadQry = string.Format("EXEC _FJobTestNum '{0}', '{1}' ", testType, date);
            Dblink.AllSelect(TestNumLoadQry, ds);

            testNum = int.Parse(ds.Tables[0].Rows[0][0].ToString());

            txtTestNum.Text = testNum.ToString();

        }

        public void TestNum_Increase()
        {
            try
            {
                testNum = int.Parse(txtTestNum.Text);

                testNum++;

                txtTestNum.Text = testNum.ToString();
            }
            catch(Exception ex)
            {

            }
           
            
            
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            
        }
    }
}
