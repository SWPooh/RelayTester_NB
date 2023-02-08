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
    public partial class FormLotPop : Form
    {
        public string sSeq = string.Empty;
        public string sType = string.Empty;
        public string sRelayNum = string.Empty;
        public string sTester = string.Empty;

        public DataSet mainDS = new DataSet();
        DbLink Dblink = new DbLink();

        public FormLotPop()
        {
            InitializeComponent();
        }

        private void FormLotPop_Load(object sender, EventArgs e)
        {
           

            string pQuery = string.Empty;
            
            pQuery = "EXEC _SLotQuery'" + sRelayNum + "', '1' ";

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
            dgvJobPop.Columns["EmpName"].HeaderCell.Value = "작업자명";
            dgvJobPop.Columns["EqType"].HeaderCell.Value = "계전기종류";
            dgvJobPop.Columns["RelayNum"].HeaderCell.Value = "시험기번호";
            dgvJobPop.Columns["remark"].HeaderCell.Value = "비고";

            //컬럼 비지블
            dgvJobPop.Columns["RelayType"].Visible = false;

            // //컬럼 사이즈
            dgvJobPop.Columns["Lot"].Width = 150;
            dgvJobPop.Columns["EmpName"].Width = 150;
            dgvJobPop.Columns["EqType"].Width = 150;
            dgvJobPop.Columns["RelayNum"].Width = 150;
            dgvJobPop.Columns["remark"].Width = 300;

            // 포멧 넣기
            
        }
        
        private void dgvSchedPop_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    sSeq = dgvJobPop.Rows[e.RowIndex].Cells["Lot"].Value.ToString().Replace("-", "");
                    sType = dgvJobPop.Rows[e.RowIndex].Cells["RelayType"].Value.ToString().Replace("-", "");
                    sRelayNum = dgvJobPop.Rows[e.RowIndex].Cells["RelayNum"].Value.ToString();
                    sTester = dgvJobPop.Rows[e.RowIndex].Cells["EmpName"].Value.ToString();
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
            catch { }
        }

    }
}
