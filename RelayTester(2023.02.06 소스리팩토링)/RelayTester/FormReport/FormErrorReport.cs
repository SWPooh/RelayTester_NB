using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelayTester
{
    public partial class FormErrorReport : Form
    {
        ErrorReport errorReport;

        public FormErrorReport()
        {
            InitializeComponent();
            errorReport = new ErrorReport(this);
        }

        private void FormErrorReport_Load(object sender, EventArgs e)
        {
            errorReport.LoadTesterCmb();
            errorReport.LoadRelayCmb();
            errorReport.LoadTestNum();

            dgvQueryResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void rbt_all_CheckedChanged(object sender, EventArgs e)
        {
            errorReport.RadioButtonAllChk();
        }

        private void rbt_one_CheckedChanged(object sender, EventArgs e)
        {
            errorReport.RadioButtonOneChk();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            errorReport.LoadSelectData();
        }

        private void btnExcelSave_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string date = dt.ToString("yyyy-MM-dd");


            Excell.SaveExcel(date + "_에러리포트",dgvQueryResult);
        }


        private void btnLotQuery_Click(object sender, EventArgs e)
        {
            errorReport.LoadLotData();
        }

        private void cmbTester_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorReport.SelectResult();
        }

        private void cmbRelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorReport.SelectResult();
        }

        private void dtpdate_ValueChanged(object sender, EventArgs e)
        {
            errorReport.LoadTestNum();
        }

        private void chkTestNum_CheckedChanged(object sender, EventArgs e)
        {
            errorReport.LoadTestNum();
        }

        private void chkLot_CheckedChanged(object sender, EventArgs e)
        {
            //errorReport.TestNumLoad();
            errorReport.SelectResult();
        }

        private void rbt_AllDate_CheckedChanged(object sender, EventArgs e)
        {
            //dtpdate2.Enabled = true;
            chkTestNum.Enabled = false;
            cmbTestNum.Enabled = false;
            chkTestNum.Checked = false;
        }

        private void rbt_OneDate_CheckedChanged(object sender, EventArgs e)
        {
            //dtpdate2.Enabled = false;
            chkTestNum.Enabled = true;
            cmbTestNum.Enabled = true;
        }

        private void mtxtLot_TextChanged(object sender, EventArgs e)
        {
            errorReport.SelectResult();
        }

        private void dgvQueryResult_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void dgvQueryResult_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
        }

        bool IsTheSameCellValue(int column, int row)
        {
            
            return true;
        }
    }
}
