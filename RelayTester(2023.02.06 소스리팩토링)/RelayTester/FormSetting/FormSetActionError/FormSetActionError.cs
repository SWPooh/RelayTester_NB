using RelayTester.CommonProcess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace RelayTester
{
    public partial class FormSetActionError : Form
    {
        public FormSetActionErrorWork codeWork;

        public FormSetActionError()
        {
            InitializeComponent();
            codeWork = new FormSetActionErrorWork(this);
        }

        private void FormActionError_Load(object sender, EventArgs e)
        {
            codeWork.ErrorDataLoad();
        } //화면이 생기기 전 폼 데이터 로드 단계에서 실행

        private void FormActionError_Shown(object sender, EventArgs e)
        {
            cmb_ErrorCode.SelectedIndex = 0;
        } //폼이 로드되고 난 후 화면이 나타나고 나서 실행

        private void btnQuery_Click(object sender, EventArgs e)
        {
            codeWork.QueryClick();
        } //조회버튼 클릭

        private void dgv_ret_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            CellPaint.CellPainting(e, dgv_ret);
        } //같은 셀 내용 사이의 테두리 삭제

        private void dgv_ret_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            CellPaint.CellFormating(e, dgv_ret);
        } //같은 셀 데이터를 1개만 남기고 삭제

        private void btnNew_Click(object sender, EventArgs e)
        {
            codeWork.NewClick();
        } //추가버튼 클릭

        private void btnDelete_Click(object sender, EventArgs e)
        {
            codeWork.DeleteClick();
        } //삭제버튼 클릭
    }
}