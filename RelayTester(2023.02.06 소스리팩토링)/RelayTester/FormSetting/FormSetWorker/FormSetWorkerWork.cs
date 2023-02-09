using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelayTester
{
    public class FormSetWorkerWork
    {
        public FormSetWorker Form_SetWorker;

        public DataSet mainDS = new DataSet();
        DbLink Dblink = new DbLink();

        public FormSetWorkerWork(FormSetWorker form) 
        {
            Form_SetWorker= form;
        }

        public void QueryClick()
        {
            string pQuery = string.Empty;
            string pEmpNm = Form_SetWorker.txtEmpName.Text;

            pQuery = "EXEC _SEmpQuery '" + pEmpNm + "', '1'";

            mainDS.Clear();
            Form_SetWorker.dgvEmp.DataSource = null;

            Dblink.AllSelect(pQuery, mainDS);
            if (mainDS.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("조회된 자료가 없습니다.", "조회", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Form_SetWorker.dgvEmp.DataSource = mainDS.Tables[0];
            GridResetMethod();
        } //조회버튼 클릭 이벤트

        public void NewClick()
        {
            Form_SetWorker.txtEmpName.Text = "";
            mainDS.Clear();
            GridResetMethod();
        } //신규버튼 클릭 이벤트

        public void AddClick()
        {
            mainDS.Tables[0].Rows.Add();
            Form_SetWorker.dgvEmp.DataSource = mainDS.Tables[0];
            Form_SetWorker.dgvEmp.Rows[Form_SetWorker.dgvEmp.Rows.Count - 1].Selected = true;
        } //추가버튼 클릭 이벤트

        public void DeleteClick()
        {
            try
            {
                if (Form_SetWorker.dgvEmp.SelectedRows.Count > 0)
                {
                    string EmpSeq = Form_SetWorker.dgvEmp.SelectedRows[0].Cells[0].Value.ToString();
                    //DeleteDetailClick();
                    Dblink.ModifyMethod("EXEC _SEmpSave '" + EmpSeq + "', '','','3'");

                    QueryClick();
                }

            }
            catch (Exception ex)
            {

            }
        } //삭제버튼 클릭 이벤트

        public void SaveClick()
        {
            DataSet Seqds = new DataSet();

            bool flag = false;
            string empSeq = Form_SetWorker.dgvEmp.SelectedRows[0].Cells[0].Value.ToString();
            string empName = Form_SetWorker.dgvEmp.SelectedRows[0].Cells[1].Value.ToString();
            string empRemark = Form_SetWorker.dgvEmp.SelectedRows[0].Cells[2].Value.ToString();

            string MaxSeq = string.Empty;
            string SeqQuery = string.Empty;
            if (empSeq.Length <= 0)  //신규인경우 seq max 값 조회
            {
                SeqQuery = "EXEC _SEmpQuery '', '2'";
                Dblink.AllSelect(SeqQuery, Seqds);
                MaxSeq = Seqds.Tables[0].Rows[0][0].ToString();
                flag = true;
            }

            string pQuery = string.Empty;

            if (flag) //신규
            {
                pQuery = "EXEC _SEmpSave " + MaxSeq + ", '" + empName + "', '" + empRemark + "', '1'";
                Dblink.ModifyMethod(pQuery);
            }
            else //수정
            {
                pQuery = "EXEC _SEmpSave " + empSeq + ", '" + empName + "', '" + empRemark + "', '2'";
                Dblink.ModifyMethod(pQuery);
            }
           
            QueryClick();
            MessageBox.Show("저장이 완료되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
        } //저장버튼 클릭 이벤트

        private void GridResetMethod()
        {
            //for (int i = 0; i < mainDS.Tables[0].Rows.Count; i++)
            //{
            //    dgvEmp.Rows.Add(mainDS.Tables[0].Rows[i]["EmpSeq"].ToString(), mainDS.Tables[0].Rows[i]["EmpName"].ToString(), mainDS.Tables[0].Rows[i]["Remark"].ToString());
            //}
            Form_SetWorker.dgvEmp.DataSource = mainDS.Tables[0];
            //컬럼명
            Form_SetWorker.dgvEmp.Columns["EmpSeq"].HeaderCell.Value = "순번";
            Form_SetWorker.dgvEmp.Columns["EmpName"].HeaderCell.Value = "작업자이름";
            //dgvEmp.Columns["UseGbn"].HeaderCell.Value = "사용구분";
            Form_SetWorker.dgvEmp.Columns["Remark"].HeaderCell.Value = "비고";

            //컬럼 비지블
            Form_SetWorker.dgvEmp.Columns["EmpSeq"].Visible = false;

            //컬럼 사이즈
            Form_SetWorker.dgvEmp.Columns["EmpName"].Width = 200;
            //dgvEmp.Columns["UseGbn"].Width = 100;
            Form_SetWorker.dgvEmp.Columns["Remark"].Width = 500;

        } //그리드뷰 헤더명 설정
    }
}
