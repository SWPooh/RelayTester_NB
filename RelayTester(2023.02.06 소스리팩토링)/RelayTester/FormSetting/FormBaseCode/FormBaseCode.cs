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
    public partial class FormBaseCode : Form
    {
        public FormBaseCodeWork codeWork;

        public FormBaseCode()
        {
            InitializeComponent();
            codeWork = new FormBaseCodeWork(this);
        }

        private void FormCommonCode_Load(object sender, EventArgs e)
        {
            // TODO: 이 코드는 데이터를 'relayTesterDBDataSet1.TEmpReg' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            //this.tEmpRegTableAdapter.Fill(this.relayTesterDBDataSet1.TEmpReg);

            //Dblink.ConnCoForm();

            //폼로드할때 자동조회
            codeWork.QueryClick();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            codeWork.NewClick();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            codeWork.QueryClick();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            codeWork.AddClick();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            codeWork.SaveData();
        }

        private void txtCodeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                codeWork.rowindex = 0;
                codeWork.QueryClick();
            }
        }
        
        private void dgvCommonMst_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            codeWork.SearchDetail(sender, e);
        }

        private void btnAddDtl_Click(object sender, EventArgs e)
        {
            codeWork.AddDetailClick();

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            codeWork.DeleteClick();
        }
        private void btnDelDtl_Click(object sender, EventArgs e)
        {
            codeWork.DeleteDetailClick();
        }

    }
}
