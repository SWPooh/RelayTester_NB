using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelayTester.FormMainWorking
{
    public class FormMainWork
    {
        public FormMain FORM_MAIN;
        public FormRelay formRelay;
        string ClosingFlag = string.Empty;
        public FormMainWork(FormMain form) 
        { 
            FORM_MAIN = form;
        }
        public void FormLoadWork()
        {
            string cmdLn = "";

            foreach (string arg in Environment.GetCommandLineArgs())
            {
                cmdLn += arg;

            }

            if (cmdLn.Substring(cmdLn.Length - 7) != "Updated")
            {
                string strFile = Application.StartupPath + @"\AppStart.exe";
                FileInfo fileInfo = new FileInfo(strFile);
                if (fileInfo.Exists)
                {
                    ClosingFlag = "None";   //이경우에는 종료하시겠습니까 메시지 안뜨게 하기위해
                    Process.Start(strFile);
                    Application.Exit();
                }

            }
            PingInOut();
            FORM_MAIN.cmbIOGbn.Enabled = false;
        } // 폼 로드이벤트, 업데이트 체크
        private void PingInOut()
        {
            try
            {
                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send("192.168.11.253");

                if (reply.Status == IPStatus.Success) //핑이 제대로 들어가고 있을 경우
                {
                    FORM_MAIN.cmbIOGbn.SelectedIndex = 0;
                    Global.globalInOut = "192.168.11.253";
                }
                else //핑이 제대로 들어가지 않고 있을 경우 
                {
                    FORM_MAIN.cmbIOGbn.SelectedIndex = 1;
                    Global.globalInOut = "211.198.148.133";
                }

                FORM_MAIN.cmbIOGbn.Enabled = false;
            }
            catch (Exception ex)
            {
                FORM_MAIN.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FM001", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
            }
        } //대아티아이 내,외부 DB 핑테스트 (디폴트는 외부)
        public void ConnectComboChange()
        {
            if (FORM_MAIN.cmbIOGbn.SelectedIndex == 0)
            {
                Global.globalInOut = "192.168.11.253";
            }
            else
            {
                Global.globalInOut = "211.198.148.133";
            }
            FORM_MAIN.cmbIOGbn.Enabled = false;
        } //대아티아이 내,외부 DB 변경 이벤트
        public void ConnectionWork()
        {
            if (FORM_MAIN.cmbIOGbn.Enabled == false)
            {
                FORM_MAIN.cmbIOGbn.Enabled = true;
            }
            else
            {
                FORM_MAIN.cmbIOGbn.Enabled = false;
            }

            if (FORM_MAIN.lbl_mainLot.Visible)
            {
                FORM_MAIN.lbl_mainLot.Visible = false;
                FORM_MAIN.txt_mainLot.Visible = false;
                FORM_MAIN.txt_mainLot.Enabled = false;
            }
            else
            {
                FORM_MAIN.lbl_mainLot.Visible = true;
                FORM_MAIN.txt_mainLot.Visible = true;
                FORM_MAIN.txt_mainLot.Enabled = false;
            }

        } // 연결구분 선택 시 콤보박스 enable true/false
        public void MainLotWork()
        {
            if (FORM_MAIN.txt_mainLot.Enabled == false)
            {
                FORM_MAIN.txt_mainLot.Enabled = true;
            }
            else
            {
                FORM_MAIN.txt_mainLot.Enabled = false;
            }
        } // 연결구분 선택따라 lot조회 조건 enable true/false
        public void MenuItemFormOpen(object sender)
        {
            ToolStripMenuItem selectedItem = (ToolStripMenuItem)sender;
            FormWorking(selectedItem.Name);
        } //메뉴스트립 버튼 데이터 전송
        public void FormWorking(string name)
        {
            foreach (Form openForm in Application.OpenForms) // 열려있는 폼 중복체크 
            {
                if (openForm.Name == "name")
                {
                    if (openForm.WindowState == FormWindowState.Minimized)
                    {
                        openForm.WindowState = FormWindowState.Normal;
                    }
                    openForm.Activate();

                    return;
                }
            }

            string nameSpace = "RelayTester"; //네임스페이스 명
            Assembly cuasm = Assembly.GetExecutingAssembly();
            //string Format 의 따옴표와 마침표 주의!!
            Form frm = (Form)cuasm.CreateInstance(string.Format("{0}.{1}", nameSpace, name));

            frm.MdiParent = FORM_MAIN;
            frm.WindowState = FormWindowState.Maximized;

            frm.Show();
        }  //메뉴스트립 버튼별 이름으로 해당 폼 열기
        public void OpenRelayForm(string relayNum)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "FormRelay" + relayNum)
                {
                    if (openForm.WindowState == FormWindowState.Minimized)
                    {
                        openForm.WindowState = FormWindowState.Normal;
                    }
                    openForm.Activate();
                    return;
                }

            }

            formRelay = new FormRelay(FORM_MAIN);
            formRelay.Name = "FormRelay" + relayNum;
            formRelay.Text = "시험기_" + relayNum;
            formRelay.MdiParent = FORM_MAIN;
            formRelay.WindowState = FormWindowState.Maximized;
            formRelay.txtRelayNum.Text = relayNum;
            formRelay.Show();
        } //버튼으로 1,2,3번 시험기 폼 열기


    }
}
