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
    public partial class FormSetTestValue : Form
    {
        public FormSetTestValueWork codeWork;


        public FormSetTestValue()
        {
            InitializeComponent();
            codeWork = new FormSetTestValueWork(this);
        }

        private void FormRefVal_Load(object sender, EventArgs e)
        {
            // TODO: 이 코드는 데이터를 'relayTesterDBDataSet1.TEmpReg' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            //this.tEmpRegTableAdapter.Fill(this.relayTesterDBDataSet1.TEmpReg);

            //Dblink.ConnCoForm();

            //폼로드할때 자동조회
            codeWork.QueryClick();
           
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            codeWork.QueryClick();
        } //조회버튼 클릭

        private void btnSave_Click(object sender, EventArgs e)
        {
            codeWork.SaveClick();
        } //저장버튼 클릭
    }
}
