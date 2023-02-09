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
        public FormSetWorkerWork codeWork;

        public FormSetWorker()
        {
            InitializeComponent();
            codeWork = new FormSetWorkerWork(this);
        }

        private void FormEmpReg_Load(object sender, EventArgs e)
        {
            // TODO: 이 코드는 데이터를 'relayTesterDBDataSet1.TEmpReg' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            //this.tEmpRegTableAdapter.Fill(this.relayTesterDBDataSet1.TEmpReg);

            //Dblink.ConnCoForm();

            //폼로드할때 자동조회
            codeWork.QueryClick();
        } //폼 로드

        private void btnNew_Click(object sender, EventArgs e)
        {
            codeWork.NewClick();
        } //신규버튼 클릭

        private void btnQuery_Click(object sender, EventArgs e)
        {
            codeWork.QueryClick();
        } //조회버튼 클릭

        private void btnAdd_Click(object sender, EventArgs e)
        {
            codeWork.AddClick();
        } //추가버튼 클릭

        private void btnDelete_Click(object sender, EventArgs e)
        {
            codeWork.DeleteClick();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            codeWork.SaveClick();
        }

        private void txtEmpName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                codeWork.QueryClick();
            }
        }


    }
}
