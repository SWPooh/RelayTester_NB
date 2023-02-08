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
    public partial class FormSetWorker : Form
    {
        public DataSet mainDS = new DataSet();
        DbLink Dblink = new DbLink();

        public FormSetWorker()
        {
            InitializeComponent();
        }

        private void FormEmpReg_Load(object sender, EventArgs e)
        {
            // TODO: 이 코드는 데이터를 'relayTesterDBDataSet1.TEmpReg' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            //this.tEmpRegTableAdapter.Fill(this.relayTesterDBDataSet1.TEmpReg);

            //Dblink.ConnCoForm();

            //폼로드할때 자동조회
            this.btnQuery_Click(null, null);

           
        }

        private void GridResetMethod()
        {
            //for (int i = 0; i < mainDS.Tables[0].Rows.Count; i++)
            //{
            //    dgvEmp.Rows.Add(mainDS.Tables[0].Rows[i]["EmpSeq"].ToString(), mainDS.Tables[0].Rows[i]["EmpName"].ToString(), mainDS.Tables[0].Rows[i]["Remark"].ToString());
            //}
            dgvEmp.DataSource = mainDS.Tables[0];
            //컬럼명
            dgvEmp.Columns["EmpSeq"].HeaderCell.Value = "순번";
            dgvEmp.Columns["EmpName"].HeaderCell.Value = "작업자이름";
            //dgvEmp.Columns["UseGbn"].HeaderCell.Value = "사용구분";
            dgvEmp.Columns["Remark"].HeaderCell.Value = "비고";

            //컬럼 비지블
            dgvEmp.Columns["EmpSeq"].Visible = false;

            //컬럼 사이즈
            dgvEmp.Columns["EmpName"].Width = 200;
            //dgvEmp.Columns["UseGbn"].Width = 100;
            dgvEmp.Columns["Remark"].Width = 500;

        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            txtEmpName.Text = "";
            mainDS.Clear();
            GridResetMethod();

            //for (int i = 0; i < mainDS.Tables[0].Rows.Count; i++)
            //{
            //    this.mainDS.Tables[0].Rows[i].Delete();
            //}

        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string pQuery = string.Empty;
            string pEmpNm = this.txtEmpName.Text;

            pQuery = "EXEC _SEmpQuery '" + pEmpNm + "'";

            mainDS.Clear();
            dgvEmp.DataSource = null;

            Dblink.AllSelect(pQuery, mainDS);
            if (mainDS.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("조회된 자료가 없습니다.", "조회", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dgvEmp.DataSource = mainDS.Tables[0];
            GridResetMethod();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //dgvEmp.Rows.Add();
            mainDS.Tables[0].Rows.Add();
            dgvEmp.DataSource = mainDS.Tables[0];
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow dgr in dgvEmp.SelectedRows)
            {
                dgvEmp.Rows.Remove(dgr);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (DataRow item in mainDS.Tables[0].Rows)
            {
                if (item.RowState == DataRowState.Added)  //신규
                {
                  string pQuery = "EXEC _SEmpSave 0, '" + item["EmpName"].ToString() + "', '" + item["Remark"].ToString() + "', '1'";
                  Dblink.ModifyMethod(pQuery);
                }
                if(item.RowState == DataRowState.Modified)  //수정
                {
                    string pQuery = "EXEC _SEmpSave "+ item["EmpSeq"].ToString() + ", '" + item["EmpName"].ToString() + "', '" + item["Remark"].ToString() + "', '2'";
                    Dblink.ModifyMethod(pQuery);
                }
                if (item.RowState == DataRowState.Deleted)  //삭제
                {
                    string pQuery = "EXEC _SEmpSave " + item["EmpSeq",DataRowVersion.Original].ToString() + ",'','','3'";
                    Dblink.ModifyMethod(pQuery);
                }
            }
            this.btnQuery_Click(null, null);
            MessageBox.Show("저장이 완료되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtEmpName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnQuery_Click(null, null);
            }
        }


    }
}
