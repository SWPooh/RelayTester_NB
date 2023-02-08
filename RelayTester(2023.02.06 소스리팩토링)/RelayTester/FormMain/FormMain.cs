using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.IO;
using System.Diagnostics;
using RelayTester;
using System.Reflection;
using RelayTester.FormMainWorking;

namespace RelayTester
{
    public partial class FormMain : Form
    {
        public FormMainWork mainWork = null;
        public FormMain()
        {
            InitializeComponent();
            mainWork = new FormMainWork(this);
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            mainWork.FormLoadWork();
        } // 프로그램 시작 시 업데이트 체크
        private void cmbIOGbn_SelectedIndexChanged(object sender, EventArgs e)
        {
            mainWork.ConnectComboChange();
        } //대아티아이 내,외부 DB 변경 이벤트
        private void ConnectOption_Click(object sender, EventArgs e)
        {
            mainWork.ConnectionWork();
                
        } // 연결구분 선택 시 콤보박스 enable true/false
        private void lbl_mainLot_Click(object sender, EventArgs e)
        {
            mainWork.MainLotWork();
        } // 연결구분 선택따라 lot조회 조건 enable true/false
        private void FormSelectResult_Click(object sender, EventArgs e)
        {
            mainWork.MenuItemFormOpen(sender);
        } //성적서 - 성적서용 결과값 선택 페이지 이동
        private void FormReportPrint_Click(object sender, EventArgs e)
        {
            mainWork.MenuItemFormOpen(sender);
        } //성적서 - 시험성적서 출력페이지 이동
        private void FormErrorReport_Click(object sender, EventArgs e)
        {
            mainWork.MenuItemFormOpen(sender);
        } //성적서 - 오류조치결과 페이지 이동
        private void FormBaseCode_Click(object sender, EventArgs e)
        {
            mainWork.MenuItemFormOpen(sender);
        } //설정 - 기초코드관리 페이지 이동
        private void FormSetCorrectValue_Click(object sender, EventArgs e)
        {
            mainWork.MenuItemFormOpen(sender);
        } //설정 - 보정값 관리 페이지 이동
        private void FormSetTester_Click(object sender, EventArgs e)
        {
            mainWork.MenuItemFormOpen(sender);
        } //설정 - 계전기등록 페이지 이동
        private void FormSetSchedule_Click(object sender, EventArgs e)
        {
            mainWork.MenuItemFormOpen(sender);
        } //설정 - 스케줄등록 페이지 이동
        private void FormSetTestValue_Click(object sender, EventArgs e)
        {
            mainWork.MenuItemFormOpen(sender);
        } //설정 - 시험기준값 등록 페이지 이동
        private void FormSetErrorCode_Click(object sender, EventArgs e)
        {
            mainWork.MenuItemFormOpen(sender);
        } //설정 - 에러코드 등록 페이지 이동
        private void FormSetActionError_Click(object sender, EventArgs e)
        {
            mainWork.MenuItemFormOpen(sender);
        } //설정 - 조치코드 등록 페이지 이동
        private void FormSetWorker_Click(object sender, EventArgs e)
        {
            mainWork.MenuItemFormOpen(sender);
        } //설정 - 작업자 등록 페이지 이동
        private void FormSetAdminPW_Click(object sender, EventArgs e)
        {
            mainWork.MenuItemFormOpen(sender);
        } //설정 - 관리자 비밀번호 변경 페이지 이동

        private void btnRelayTester1_Click(object sender, EventArgs e)
        {
            mainWork.OpenRelayForm("01");
        } // 1번시험기 페이지 이동

        private void btnRelayTester2_Click(object sender, EventArgs e)
        {
            mainWork.OpenRelayForm("02");
        } // 2번 시험기 페이지 이동

        private void btnRelayTester3_Click(object sender, EventArgs e)
        {
            mainWork.OpenRelayForm("03");
        } // 3번 시험기 페이지 이동
    }

}
