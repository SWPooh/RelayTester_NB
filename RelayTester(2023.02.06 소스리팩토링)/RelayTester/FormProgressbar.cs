using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace RelayTester
{
    public partial class FormProgressbar : Form
    {

        FormReportPrint reportNew = null;

        public FormProgressbar()
        {
            InitializeComponent();
        }

        public FormProgressbar(string strMsg)
        {
            InitializeComponent();
            label_prbText.Text = strMsg;
        }

        public FormProgressbar(FormReportPrint report)
        {
            reportNew = report;
        }

        private void FormProgressbar_Load(object sender, EventArgs e)
        {
            ClearProgressbar();
        }

        public void CloseProgressBar()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(delegate ()
                    {
                        Thread.Sleep(1500);
                        this.Close();
                    }));
                }
            }

            catch
            {

            }
        }

        public void ChangeProgressBarLabel(string text)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(delegate ()
                    {
                        label_prbText.Text = text;
                    }));
                }
            }

            catch
            {

            }
        }

        public void StartProgressbar(int rowNum, int rowCount)
        {
            reportNew = new FormReportPrint();

            bool checkComplete = false;

            int rowValue = 0;
            float rowRemainder = 0;
            float tmpRowResult = 0;
            int rowResult = 0;

            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(delegate ()
                    {
                        rowValue = 100 / rowNum;
                        rowRemainder = 100 % rowNum;

                        if (rowRemainder > 0)
                        {
                            if (rowCount == rowNum)
                            {
                                rowResult = 100;
                                label_prbText.Text = "작업이 완료되었습니다.";
                                
                            }
                            else
                                rowResult = rowCount * rowValue;

                        }
                        else
                            rowResult = rowCount * rowValue;

                        prbLoad.Value = rowResult;
                        lb_LoadCount.Text = prbLoad.Value.ToString();
                    }));
                }
                
            }

            catch
            {

            }
        }

        public void ClearProgressbar()
        {
            prbLoad.Value = 0;
            lb_LoadCount.Text = 0.ToString();
        }

    }
}

