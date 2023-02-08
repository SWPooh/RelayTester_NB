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
    public partial class FormReportLotPop : Form
    {
        public string sSeq = string.Empty;
        public string sType = string.Empty;
        public string sRelayType = string.Empty;

        public DataSet mainDS = new DataSet();
        DbLink Dblink = new DbLink();

        public FormReportLotPop()
        {
            InitializeComponent();
        }

        private void FormReportLotPop_Load(object sender, EventArgs e)
        {
            string pQuery = string.Empty;
            //pQuery = "select distinct left(A.Lot,2) + '-' + right(A.Lot,2) as Lot, B.Code_Dtl_Name AS EqType, A.RelayType from TJobDtl A  Left Outer Join TCodeDtl B On A.Relaytype = B.Code_Dtl where B.Code_Mst = 'CM002' and A.RelayType = '" + sRelayType + "' order by left(A.Lot,2) + '-' + right(A.Lot,2) desc";
            pQuery = "EXEC _SLotQuery '" + sRelayType + "','0'";
            mainDS.Clear();
            dgvJobPop.DataSource = null;


            Dblink.AllSelect(pQuery, mainDS);
            dgvJobPop.DataSource = mainDS.Tables[0];
            GridResetMethod();
        }

        private void GridResetMethod()
        {

            dgvJobPop.DataSource = mainDS.Tables[0];

            for (int i = 0; i < dgvJobPop.Rows.Count; i++)
            {
                dgvJobPop.Rows[i].ReadOnly = true;
            }

            //컬럼명
            dgvJobPop.Columns["Lot"].HeaderCell.Value = "LOT번호";
            dgvJobPop.Columns["EqType"].HeaderCell.Value = "계전기종류";

            //컬럼 비지블
            dgvJobPop.Columns["RelayType"].Visible = false;

            // //컬럼 사이즈
            dgvJobPop.Columns["Lot"].Width = 150;
            dgvJobPop.Columns["EqType"].Width = 150;
        }
        
        private void dgvSchedPop_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    sSeq = dgvJobPop.Rows[e.RowIndex].Cells["Lot"].Value.ToString().Replace("-", "");
                    sType = dgvJobPop.Rows[e.RowIndex].Cells["RelayType"].Value.ToString().Replace("-", "");
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
            catch { }
        }

    }
}
