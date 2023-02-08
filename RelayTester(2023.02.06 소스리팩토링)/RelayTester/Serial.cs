#define _NON_TIMEOUT_
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelayTester
{
    internal class Serial //2022.12.28 Create
    {
        FormRelay formRelay;
        DbLink Dblink = new DbLink();
        private Queue msg = new Queue();
        private ManualResetEvent Controller = new ManualResetEvent(false);
        public bool IsAlive { get; private set; }

        private Thread QueueThread = null;

        public int ReturnTimerCnt = 0;
        public System.Timers.Timer ReturnTimer = new System.Timers.Timer();

        public System.Timers.Timer AgingTimer = new System.Timers.Timer();

        delegate void TimerEventFiredDelegate();

        public Serial() {}

        public Serial(FormRelay _form)
        {
            formRelay = _form;

            this.QueueThread = new Thread(this.ExecuteMessage)
            {
                IsBackground = true,
                Name = "MessageQueue"
            };
            this.IsAlive = true;
            this.QueueThread.Start();
#if _NON_TIMEOUT_
            AgingTimer.Interval = 1000000; // 1분
#else
            AgingTimer.Interval = 6000; // 1분
#endif

            AgingTimer.Elapsed += new System.Timers.ElapsedEventHandler(AgingTimer_Elapsed);

            ReturnTimer.Interval = 1000;
            ReturnTimer.Elapsed += new System.Timers.ElapsedEventHandler(ReturnTimer_Elapsed);
              

        }

       

        public void GetComPort()
        {
            DataSet ds = new DataSet();
            string port;
            string pQueryTemp = string.Empty;

            pQueryTemp = ("EXEC _SCommonCodeQuery 'CM004', '" + formRelay.txtRelayNum.Text + "', '1'");
            
            Dblink.AllSelect(pQueryTemp, ds);

            port = ds.Tables[0].Rows[0]["Remark"].ToString();

            formRelay.lblPortNum.Text = port;
        }

        public void Connection(SerialPort port, string portNum, Button btnDisconnect, Button btnConnect, Label lblConnectStatus)
        {
            try
            {
                if (portNum != "" && portNum != null)
                {
                    if (!port.IsOpen)
                    {
                        port.PortName = portNum;
                        port.BaudRate = 38400;
                        port.DataBits = (int)8;
                        port.Parity = Parity.None;
                        port.StopBits = StopBits.One;
                        port.Encoding = System.Text.Encoding.GetEncoding(1252);
                        port.ReadTimeout = (int)5000;
                        port.WriteTimeout = (int)5000;
                        port.Open();

                        if (port.IsOpen)
                        {
                            btnDisconnect.Enabled = true;
                            btnConnect.Enabled = false;

                            lblConnectStatus.Text = "CONNECTED";
                            lblConnectStatus.BackColor = Color.GreenYellow;
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
                formRelay.Add_Log("[SYSTEM] " + ex.Message);
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        public void Disconnection(SerialPort port, Button btnDisconnect, Button btnConnect, Label lblConnectStatus)
        {
            try
            {
                port.Close();

                btnDisconnect.Enabled = false;
                btnConnect.Enabled = true;

                lblConnectStatus.Text = "DISCONNECTED";
                lblConnectStatus.BackColor = Color.Red;
            }
            catch (Exception ex)
            {
                //Add_Log("[SYSTEM] " + ex.Message);
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        public void ReadData(object sender, SerialDataReceivedEventArgs e, SerialPort port)
        {
            try
            {
                
                System.Threading.Thread.Sleep(50);
                if (port.IsOpen)
                {
                    int receive = port.BytesToRead;
                    string readData;

                    if (receive != 0)
                    {
                        readData = string.Empty;
                        string sbuf = string.Empty;
                        sbuf = port.ReadExisting();
                        
                        /*if(errormsg) //에러메세지 수신시 재수신 요청 (23.01.20)
                        {
                            port.DiscardInBuffer();
                            formRelay.CallSerialWrite();
                        }*/

                        byte[] toBytes = Encoding.GetEncoding(1252).GetBytes(sbuf);

                        for (int iTemp = 0; iTemp < toBytes.Length; iTemp++)
                        {
                            readData += string.Format("{0:X2} ", toBytes[iTemp]);
                        }
                        readData = readData.Substring(0, readData.Length - 1);

                        formRelay.txtRecvMsg.Text = readData;

                        //Add_Log("계전기 -> PC : " + strRecData);

                        readData = readData.Replace("10 10", "10"); //데이터중에 10이 있으면 10 10 구조로 송신됨, So, 10하나 짤라줌
                                                                    //작업 시작 버튼이 눌려 있을떄만
                                                                    //if (jobFlag == "ON")
                                                                    //Check_msg(readData); //2023.01.29
                        formRelay.Add_Log("Receive_Que");
                        Receive_Que(readData); //메시지 수신시 큐로

                    }
                }
            }
            catch (Exception ex)
            {
                this.formRelay.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR209", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //01.09//
        public void Receive_Que(string que_data)
        {
            // Queue 에 Command 를 Enqueue

            this.msg.Enqueue(que_data);

            // 대기상태에 빠져 있을지 모르니, Thread 를 동작하게 만듦
            this.Controller.Set();
        }

        private bool errormsg = false;

        public void Check_msg(string msg)
        {
            formRelay.Add_Log("Check_msg");
            try
            {
                string remsg = "";
                int istx = 0;
                int ietx = 0;

                istx = Convert.ToInt32(msg.IndexOf("10 02"));
                ietx = Convert.ToInt32(msg.IndexOf("10 03"));

                if (istx < 0 || ietx < 0)
                {
                    errormsg = true;
                    //MessageBox.Show("수신된 메시지에 오류가 있습니다.\r수신된 메시지 : " + msg, "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    formRelay.Add_Log("수신된 메시지에 오류가 있어 통신을 재시작합니다. 수신된 메시지 : " + msg);

                    return;
                }

                //붙어서 온거 처리
                if (msg.Length != ietx + 5)
                {
                    remsg = msg.Substring(ietx + 5);
                    msg = msg.Remove(ietx + 5);
                    Receive_Que(msg);
                    Receive_Que(remsg);
                }
                else
                {
                    if (msg.Substring(6, 2) != String.Format("{0:00}", Convert.ToInt32(formRelay.txtRelayNum.Text)))
                    {
                        formRelay.ErrorProcess(); //에러발생시 처리

                        //MessageBox.Show(this, "해당 장비에 대한 리턴값이 아닙니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        if (MessageBox.Show( "-" + formRelay.txtRelayNum.Text + "번 시험기-\n해당 장비에 대한 리턴값이 아닙니다.\n계속 진행하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            //계속 진행시 리턴타이머 재가동
                            ReturnTimerCnt = 0;
                            ReturnTimer.Start();
                            return;
                        }
                        else
                        {
                            //계속 진행 안할경우 작업 종료
                            formRelay.btnJobStop_Click(null, null);
                            return;
                        }
                    }

                    if (msg.Substring(9, 2) != (Convert.ToInt32(formRelay.txtSendMsg.Text.Substring(9, 2)) + 20).ToString() || msg.Substring(12, 2) != (formRelay.txtSendMsg.Text.Substring(12, 2)))
                    {
                        formRelay.ErrorProcess(); //에러발생시 처리

                        //MessageBox.Show(this, "해당 명령에 대한 리턴값이 아닙니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        if (MessageBox.Show( "-" + formRelay.txtRelayNum.Text + "번 시험기-\n해당 명령에 대한 리턴값이 아닙니다.\n계속 진행하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            //계속 진행시 리턴타이머 재가동
                            ReturnTimerCnt = 0;
                            ReturnTimer.Start();
                            return;
                        }
                        else
                        {
                            //계속 진행 안할경우 작업 종료
                            formRelay.btnJobStop_Click(null, null);
                            return;
                        }
                    }
                    formRelay.ReceiveData(msg);
                }
            }
            catch (Exception ex)
            {
                this.formRelay.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR210", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        public int roopCnt = 0; //2023.01.29
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
                    
                   /* if(roopCnt == 3) //2023.01.29
                    {
                        roopCnt= 0;
                        // 다 수행하고 나면, 대기상태로 진입
                        this.Controller.Reset();
                        this.Controller.WaitOne(Timeout.Infinite);
                    }*/
                    
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

        void AgingTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)    // 매 1분마다 Tick 이벤트 핸들러 실행(에이징 시간 체크용)
        {
            formRelay.BeginInvoke(new TimerEventFiredDelegate(AgingTimerWork));
        }

        void ReturnTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            formRelay.BeginInvoke(new TimerEventFiredDelegate(ReturnTimerWork));

            /*if (errormsg && roopCnt < 3) //에러메세지 수신시 재수신 요청 (23.01.20)
            {
                //formRelay.SP1.DiscardInBuffer();
                formRelay.CallSerialWrite();
                roopCnt++;
            }
            else if(errormsg && roopCnt == 3)
            {
                formRelay.Add_Log("수신된 메시지에 오류가 있어 통신에 실패하였습니다.");
                roopCnt++;
            }*/
            
        }

        private void AgingTimerWork()
        {
            try
            {
                formRelay.dgvSchedDtl.Rows[formRelay.dgvSchedDtl.CurrentCellAddress.Y].Cells["Rpt_Cnt"].Value = (Convert.ToInt32(formRelay.dgvSchedDtl.Rows[formRelay.dgvSchedDtl.CurrentCellAddress.Y].Cells["Rpt_Cnt"].Value) - 1).ToString();

                if (Convert.ToInt32(formRelay.dgvSchedDtl.Rows[formRelay.dgvSchedDtl.CurrentCellAddress.Y].Cells["Rpt_Cnt"].Value) <= 0)
                {
                    formRelay.dgvSchedDtl.Rows[formRelay.dgvSchedDtl.CurrentCellAddress.Y].Cells["chk"].Value = false;
                    formRelay.btnJobStop_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                this.formRelay.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR124", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void ReturnTimerWork()
        {
            try
            {
                ReturnTimerCnt++;

                if (ReturnTimerCnt > 60)
                {
                    formRelay.dgvSchedDtl.Rows[formRelay.dgvSchedDtl.CurrentCellAddress.Y].Cells["chk"].Value = false;

                    int rowNum = Convert.ToInt32(formRelay.dgvSchedDtl.Rows[formRelay.dgvSchedDtl.CurrentCellAddress.Y].Cells["Sched_Loc"].Value.ToString()) - 1;

                    switch (formRelay.dgvSchedDtl.Rows[formRelay.dgvSchedDtl.CurrentCellAddress.Y].Cells["Test_Type"].Value.ToString())
                    {
                        case "01":
                            formRelay.dgvCurrRst.Rows[rowNum].Cells[3].Style.BackColor = Color.DeepSkyBlue;
                            formRelay.dgvCurrRst.Rows[rowNum].Cells[3].Value = "T/O";
                            break;

                        case "02":
                            formRelay.dgvResiRst.Rows[rowNum].Cells[2].Style.BackColor = Color.DeepSkyBlue;
                            formRelay.dgvResiRst.Rows[rowNum].Cells[2].Value = "T/O";
                            break;

                        case "03":
                            formRelay.dgvRNTRst.Rows[rowNum].Cells[2].Style.BackColor = Color.DeepSkyBlue;
                            formRelay.dgvRNTRst.Rows[rowNum].Cells[2].Value = "T/O";
                            break;

                        case "04":
                            formRelay.dgvNRTRst.Rows[rowNum].Cells[2].Style.BackColor = Color.DeepSkyBlue;
                            formRelay.dgvNRTRst.Rows[rowNum].Cells[2].Value = "T/O";
                            break;
                    }
                    ReturnTimerCnt = 0;
                    formRelay.ProcessingJob();
                }
            }
            catch (Exception ex)
            {
                this.formRelay.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FR125", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}
