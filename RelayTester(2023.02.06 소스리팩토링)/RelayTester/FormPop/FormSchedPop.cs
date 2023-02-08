using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RelayTester
{
    public partial class FormSchedPop : Form
    {
        public string sSeq = string.Empty;
        public DataSet mainDS = new DataSet();
        DbLink Dblink = new DbLink();

        public string sSched_Seq;
        public string sSched_Name;
        public string sSched_Type1;
        public string sSched_Type2;

        public FormSchedPop()
        {
            InitializeComponent();
        }

        private void FormSchedPop_Load(object sender, EventArgs e)
        {
            string pQuery = string.Empty;

            if (sSched_Seq != "" && sSched_Seq != null)
            {
               pQuery = "EXEC _SSchedulePopQuery '" + sSched_Seq + "','','','','1'";
            }
            else
            {
                pQuery = "EXEC _SSchedulePopQuery '',  '" + sSched_Name + "', '" + sSched_Type1 + "', '" + sSched_Type2 + "', '2'";
            }
            mainDS.Clear();
            dgvSchedPop.DataSource = null;

            Dblink.AllSelect(pQuery, mainDS);
            dgvSchedPop.DataSource = mainDS.Tables[0];
            GridResetMethod();
        }

        private void GridResetMethod()
        {

            dgvSchedPop.DataSource = mainDS.Tables[0];

            for (int i = 0; i < dgvSchedPop.Rows.Count; i++)
            {
                dgvSchedPop.Rows[i].ReadOnly = true;
            }

            ////컬럼명
            dgvSchedPop.Columns["Sched_Seq"].HeaderCell.Value = "스케줄번호";
            dgvSchedPop.Columns["Sched_Date"].HeaderCell.Value = "등록일자";
            dgvSchedPop.Columns["Sched_Name"].HeaderCell.Value = "스케줄명";
            dgvSchedPop.Columns["Eq_Type"].HeaderCell.Value = "계전기종류";
            dgvSchedPop.Columns["ReportChk"].HeaderCell.Value = "시험종류";
            dgvSchedPop.Columns["Sched_remark"].HeaderCell.Value = "비고";

            ////컬럼 비지블
            //dgvSchedPop.Columns["EmpSeq"].Visible = false;

            ////컬럼 사이즈
           dgvSchedPop.Columns["Sched_Seq"].Width = 200;
           dgvSchedPop.Columns["Sched_Date"].Width = 140;
           dgvSchedPop.Columns["Sched_Name"].Width = 240;
           dgvSchedPop.Columns["Eq_Type"].Width = 150;
           dgvSchedPop.Columns["ReportChk"].Width = 130;
           dgvSchedPop.Columns["Sched_remark"].Width = 300;

            //포멧 넣기
           for (int i = 0; i < dgvSchedPop.Rows.Count; i++)
           {
               dgvSchedPop.Rows[i].Cells["Sched_Seq"].Value = dgvSchedPop.Rows[i].Cells["Sched_Seq"].Value.ToString().Substring(0, 4) + "-" + dgvSchedPop.Rows[i].Cells["Sched_Seq"].Value.ToString().Substring(4, 2) + "-" + dgvSchedPop.Rows[i].Cells["Sched_Seq"].Value.ToString().Substring(6, 2) + "-" + dgvSchedPop.Rows[i].Cells["Sched_Seq"].Value.ToString().Substring(8, 3);
               dgvSchedPop.Rows[i].Cells["Sched_Date"].Value = dgvSchedPop.Rows[i].Cells["Sched_Date"].Value.ToString().Substring(0, 4) + "-" + dgvSchedPop.Rows[i].Cells["Sched_Date"].Value.ToString().Substring(4, 2) + "-" + dgvSchedPop.Rows[i].Cells["Sched_Date"].Value.ToString().Substring(6, 2);


           }
        }    
        
        private void dgvSchedPop_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    sSeq = dgvSchedPop.Rows[e.RowIndex].Cells[0].Value.ToString().Replace("-", "");
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
            catch { }
        }

    }
}
