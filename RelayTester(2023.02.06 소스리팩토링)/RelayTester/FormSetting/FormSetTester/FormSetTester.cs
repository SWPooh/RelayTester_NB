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
using System.IO;

namespace RelayTester
{
    public partial class FormSetTester : Form
    {
        public FormSetTesterWork codeWork;

        public FormSetTester()
        {
            InitializeComponent();
            codeWork = new FormSetTesterWork(this);
        }

        private void FormRelayReg_Load(object sender, EventArgs e)
        {
            // TODO: 이 코드는 데이터를 'relayTesterDBDataSet1.TEmpReg' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            //this.tEmpRegTableAdapter.Fill(this.relayTesterDBDataSet1.TEmpReg);

            //Dblink.ConnCoForm();

            //폼로드할때 자동조회
            codeWork.FormLoad();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            codeWork.NewClick();
        }  //신규버튼 클릭

        private void btnQuery_Click(object sender, EventArgs e)
        {
            codeWork.QueryClick();
        } //계전기 리스트 조회, 조회버튼 클릭

        private void btnAdd_Click(object sender, EventArgs e)
        {
            codeWork.AddClick();
        } //추가버튼 클릭

        private void btnDelete_Click(object sender, EventArgs e)
        {
            codeWork.DeleteClick();
        } //삭제버튼 클릭

        private void btnSave_Click(object sender, EventArgs e)
        {
            codeWork.SaveClick();
        } //저장버튼 클릭

        private void dgvCommonMst_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            codeWork.CellClick(e);
        } //좌측 그리드뷰 셀 클릭

        private void btnImg1Add_Click(object sender, EventArgs e)
        {
            codeWork.ImageSave(txtImg1);
        } //이미지 등록버튼 클릭
        private void btnImg2Add_Click(object sender, EventArgs e)
        {
            codeWork.ImageSave(txtImg2);
        } //이미지 등록버튼 클릭
        private void btnImg3Add_Click(object sender, EventArgs e)
        {
            codeWork.ImageSave(txtImg3);
        } //이미지 등록버튼 클릭

        private void btn_Color_Click(object sender, EventArgs e)
        {
            codeWork.ColorClick();
        } //색상 등록버튼 클릭
    }
}
