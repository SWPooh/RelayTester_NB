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
using System.Runtime.InteropServices;
using System.IO;

namespace RelayTester
{
    public partial class FormSetSchedule : Form
    {
        public DataSet mainDS = new DataSet();
        public DataSet dtlDS = new DataSet();
        DbLink Dblink = new DbLink();

        public int rowindex;

        //엑셀 임포트용
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";

        private bool aftersave = false;

        public FormSetSchedule()
        {
            InitializeComponent();
        }

        private void FormSchedule_Load(object sender, EventArgs e)
        {
            try
            {
                //Dblink.ConnCoForm();

                mtxtSchedDate.Text = string.Format("{0:D4}{1:D2}{2:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                //폼로드할때 자동조회
                rowindex = 0;
                string pQuery = "EXEC _SScheduleDetailQuery '0000'";
                dtlDS.Clear();
                dgvSchedDtl.DataSource = null;

                Dblink.AllSelect(pQuery, dtlDS);
                dgvSchedDtl.DataSource = dtlDS.Tables[0];
                GridResetMethod();

                this.cmbSchedType.DataSource = EqType("EXEC _SEquipTypeQuery");
                this.cmbSchedType.DisplayMember = "Code_Dtl_Name";
                this.cmbSchedType.ValueMember = "Code_Dtl";
                this.cmbSchedType.SelectedIndex = 0;

                this.cmbSchedTypeSearch.DataSource = EqType("EXEC _SEquipTypeQuery '2'");
                this.cmbSchedTypeSearch.DisplayMember = "Code_Dtl_Name";
                this.cmbSchedTypeSearch.ValueMember = "Code_Dtl";
                this.cmbSchedTypeSearch.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FS101", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void GridResetMethod()
        {
            //콤보박스 추가
            if (dgvSchedDtl.Columns.Contains("Test_Type") == true)
            {
                dgvSchedDtl.Columns.Remove("Test_Type");
            }
            if (dgvSchedDtl.Columns.Contains("Sched_Loc") == true)
            {
                dgvSchedDtl.Columns.Remove("Sched_Loc");
            }
            AddComboBoxColumns();

            ////컬럼명
            dgvSchedDtl.Columns["Sched_Ord"].HeaderCell.Value = "순서";
            dgvSchedDtl.Columns["Sched_Loc"].HeaderCell.Value = "위치";
            dgvSchedDtl.Columns["Test_Type"].HeaderCell.Value = "시험종류";
            dgvSchedDtl.Columns["Rpt_Cnt"].HeaderCell.Value = "반복회수";
            dgvSchedDtl.Columns["Add1"].HeaderCell.Value = "추가1";
            dgvSchedDtl.Columns["Add2"].HeaderCell.Value = "추가2";
            dgvSchedDtl.Columns["Add3"].HeaderCell.Value = "추가3";

            ////컬럼 비지블
            dgvSchedDtl.Columns["Sched_Seq"].Visible = false;
            dgvSchedDtl.Columns["Sched_Dtl_Seq"].Visible = false;

            ///컬럼 인에이블
            dgvSchedDtl.Columns["Sched_Ord"].ReadOnly = true;    

            //컬럼 사이즈
            dgvSchedDtl.Columns["Sched_Ord"].Width = 60;
            dgvSchedDtl.Columns["Sched_Loc"].Width = 60;
            dgvSchedDtl.Columns["Test_Type"].Width = 100;
            dgvSchedDtl.Columns["Rpt_Cnt"].Width = 80;
            dgvSchedDtl.Columns["Add1"].Width = 100;
            dgvSchedDtl.Columns["Add2"].Width = 100;
            dgvSchedDtl.Columns["Add3"].Width = 100;

            //컬럼 정렬
            dgvSchedDtl.Columns["Sched_Ord"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSchedDtl.Columns["Sched_Loc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSchedDtl.Columns["Test_Type"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSchedDtl.Columns["Rpt_Cnt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSchedDtl.Columns["Add1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSchedDtl.Columns["Add2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSchedDtl.Columns["Add3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                //검색조건 초기화
                foreach (Control ctl in this.grbQuery.Controls)
                {
                    if (ctl.GetType().ToString().Equals("System.Windows.Forms.TextBox") || ctl.GetType().ToString().Equals("System.Windows.Forms.MaskedTextBox"))
                    {
                        ctl.Text = "";
                    }
                }

                //스케줄 마스터 초기화
                foreach (Control ctl in this.grbSchedMst.Controls)
                {
                    if (ctl.GetType().ToString().Equals("System.Windows.Forms.TextBox") || ctl.GetType().ToString().Equals("System.Windows.Forms.MaskedTextBox"))
                    {
                        ctl.Text = "";
                    }
                }

                mtxtSchedDate.Text = string.Format("{0:D4}{1:D2}{2:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                chkReportChk.Checked = false;


                //스케줄 디테일 초기화
                dtlDS.Clear();
                GridResetMethod();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FS102", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                FormSchedPop sp = new FormSchedPop();

                if (aftersave == false)
                {
                    sp.sSched_Seq = mtxtSchedSeqSearch.Text.Replace("-", "").Trim();
                    sp.sSched_Name = txtSchedNmSearch.Text.Trim();

                    sp.sSched_Type1 = cmbSchedTypeSearch.SelectedValue.ToString();
                    sp.sSched_Type2 = "00";

                    if (sp.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string pQuery1 = string.Empty;

                        pQuery1 = "EXEC _SScheduleMasterQuery '" + sp.sSeq + "'";
                        mainDS.Clear();

                        Dblink.AllSelect(pQuery1, mainDS);

                        for (int i = 0; i < mainDS.Tables[0].Rows.Count; i++)
                        {
                            this.mtxtSchedSeq.Text = mainDS.Tables[0].Rows[i]["Sched_Seq"].ToString();
                            this.mtxtSchedDate.Text = mainDS.Tables[0].Rows[i]["Sched_Date"].ToString();
                            this.txtSchedNm.Text = mainDS.Tables[0].Rows[i]["Sched_Name"].ToString();
                            this.cmbSchedType.SelectedValue = mainDS.Tables[0].Rows[i]["Sched_Type"].ToString();
                            this.txtSchedRemark.Text = mainDS.Tables[0].Rows[i]["Sched_Remark"].ToString();
                            this.chkReportChk.Checked = Convert.ToBoolean(mainDS.Tables[0].Rows[i]["ReportChk"]);
                        }
                    }
                }
                else
                {
                    sp.sSeq = mtxtSchedSeq.Text.Replace("-", "").Trim();
                }

                string pQuery2 = string.Empty;

                pQuery2 = "EXEC _SScheduleDetailQuery '" + sp.sSeq + "'";

                dtlDS.Clear();
                dgvSchedDtl.DataSource = null;

                Dblink.AllSelect(pQuery2, dtlDS);
                dgvSchedDtl.DataSource = dtlDS.Tables[0];
                GridResetMethod();
                aftersave = false;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FS103", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string sShedSeq = mtxtSchedSeq.Text.Replace("-", "").Trim();
                if (sShedSeq != "")
                {
                    if (MessageBox.Show("해당 스케줄을 삭제 하시겠습니까?\r삭제된 데이터는 복구할수 없습니다.", "스케줄 삭제", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        string pQuery1 = string.Format("EXEC _SScheduleMasterSave '{0}','','','','',0, '3'", mtxtSchedSeq.Text.Replace("-", ""));
                        Dblink.ModifyMethod(pQuery1);


                        btnNew_Click(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FS104", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        private void btnAddDtl_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow newRow = dtlDS.Tables[0].NewRow();

                newRow["Sched_Dtl_Seq"] = String.Format("{0,3:000}", GetSchedDtlSeq());
                newRow["Rpt_Cnt"] = 1;
                newRow["Add1"] = 0;
                newRow["Add2"] = 0;
                newRow["Add3"] = 0;

                dtlDS.Tables[0].Rows.Add(newRow);
                dgvSchedDtl.DataSource = dtlDS.Tables[0];
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FS105", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private int GetSchedDtlSeq()
        {
            //DataSet tmpDS = new DataSet();
            int max = 0;
            foreach (DataRow item in dtlDS.Tables[0].Rows)
            {
                if (Convert.ToInt32(item["Sched_Dtl_Seq"].ToString()) >= max)
                {
                    max = Int32.Parse(item["Sched_Dtl_Seq"].ToString());
                }
            }

            max++;

            return max;
        }

        private void btnDelDtl_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("해당 스케줄을 삭제 하시겠습니까?\r삭제된 데이터는 복구할수 없습니다.", "스케줄 삭제", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow dgr in dgvSchedDtl.SelectedRows)
                    {
                        string pQuery = string.Format("_SScheduleDetailSave '{0}','{1}',0,'','',0,'','','','3'"
                                         , dgr.Cells["Sched_Seq"].Value.ToString(), dgr.Cells["Sched_Dtl_Seq"].Value.ToString());
                        Dblink.ModifyMethod(pQuery);

                        dgvSchedDtl.Rows.Remove(dgr);
                    }
                    dtlDS.Tables[0].AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FS107", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int chk = 0;
                if (chkReportChk.Checked)
                    chk = 1;

                if (mtxtSchedSeq.Text.Replace("-", "").Trim() == "")
                {
                    //스케줄번호 채번 프로시저
                    DataSet tmpDS = new DataSet();
                    string pQuerySeq = string.Format("EXEC _SScheduleMasterNewSeqQuery '" + mtxtSchedDate.Text.Replace("-", "") + "'");
                    tmpDS.Clear();
                    Dblink.AllSelect(pQuerySeq, tmpDS);

                    for (int s = 0; s < tmpDS.Tables[0].Rows.Count; s++)
                    {
                        this.mtxtSchedSeq.Text = tmpDS.Tables[0].Rows[s]["MaxSeq"].ToString();
                    }

                    //마스터 저장 쿼리
                    string pQuery1 = string.Format("EXEC _SScheduleMasterSave '{0}','{1}','{2}','{3}','{4}',{5}, '1'", mtxtSchedSeq.Text.Replace("-", ""), mtxtSchedDate.Text.Replace("-", ""), txtSchedNm.Text, this.cmbSchedType.SelectedValue, txtSchedRemark.Text, chk);

                    Dblink.ModifyMethod(pQuery1);
                }
                else
                {
                    string pQuery1 = string.Format("EXEC _SScheduleMasterSave '{0}','{1}','{2}','{3}','{4}',{5}, '2'", mtxtSchedSeq.Text.Replace("-", ""), mtxtSchedDate.Text.Replace("-", ""), txtSchedNm.Text, this.cmbSchedType.SelectedValue, txtSchedRemark.Text, chk);
                    Dblink.ModifyMethod(pQuery1);
                }

                int i = 1;

                foreach (DataRow item in dtlDS.Tables[0].Rows)
                {
                    if (item.RowState != DataRowState.Deleted) //삭제가 아니면 채번
                    {
                        item["Sched_Seq"] = mtxtSchedSeq.Text.Replace("-", "").Trim();

                        item["Sched_Ord"] = i++;


                        switch (item["Test_Type"].ToString())
                        {
                            case "01":
                                if (item["Add1"].ToString() == null || item["Add1"].ToString() == "" || item["Add1"].ToString() == "0")
                                {
                                    MessageBox.Show("동작전류 시험항목은 [추가1]값(동작개시전압)이 필수 입니다.\n저장이 취소됩니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                if (item["Add2"].ToString() == null || item["Add2"].ToString() == "" || item["Add2"].ToString() == "0")
                                {
                                    MessageBox.Show("동작전류 시험항목은 [추가2]값(낙하개시전압)이 필수 입니다.\n저장이 취소됩니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }

                                break;

                            case "02":
                                if (item["Add1"].ToString() == null || item["Add1"].ToString() == "" || item["Add1"].ToString() == "0")
                                {
                                    MessageBox.Show("코일저항 시험항목은 [추가1]값(전압)이 필수 입니다.\n저장이 취소됩니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                                break;
                        }
                    }

                    if (item.RowState == DataRowState.Added)  //신규
                    {
                        string pQuery = string.Format("_SScheduleDetailSave '{0}','{1}',{2},'{3}','{4}',{5},'{6}','{7}','{8}','1'"
                                                        , item["Sched_Seq"].ToString(), item["Sched_Dtl_Seq"].ToString(), item["Sched_Ord"], item["Sched_Loc"].ToString(), item["Test_Type"].ToString()
                                                        , item["Rpt_Cnt"], item["Add1"].ToString(), item["Add2"].ToString(), item["Add3"].ToString());
                        Dblink.ModifyMethod(pQuery);
                    }
                    if (item.RowState == DataRowState.Modified)  //수정
                    {
                        string pQuery = string.Format("_SScheduleDetailSave '{0}','{1}',{2},'{3}','{4}',{5},'{6}','{7}','{8}','2'"
                                                        , item["Sched_Seq"].ToString(), item["Sched_Dtl_Seq"].ToString(), item["Sched_Ord"], item["Sched_Loc"].ToString(), item["Test_Type"].ToString()
                                                        , item["Rpt_Cnt"], item["Add1"].ToString(), item["Add2"].ToString(), item["Add3"].ToString());
                        Dblink.ModifyMethod(pQuery);
                    }
                    if (item.RowState == DataRowState.Deleted)  //삭제
                    {

                    }

                }
                aftersave = true;
                this.btnQuery_Click(null, null);
                MessageBox.Show("저장이 완료되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FS108", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void mtxtSchedSeqSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnQuery_Click(null, null);
            }
        }
        
        public DataTable EqType(string sQuery)
        {
            DataSet ds = new DataSet();
            Dblink.AllSelect(sQuery, ds);

            return ds.Tables[0];
        }
    
        public DataTable TestType()
        {
            DataSet ds = new DataSet();
            string pQueryTemp = string.Empty;
            pQueryTemp = "EXEC _STestTypeQuery '','1'";
            Dblink.AllSelect(pQueryTemp, ds);

            return ds.Tables[0];
        }

        public DataTable PalletNum()
        {
            DataSet ds = new DataSet();
            string pQueryTemp = string.Empty;
            pQueryTemp = "EXEC _SPalletNumQuery";
            Dblink.AllSelect(pQueryTemp, ds);

            return ds.Tables[0];
        }


        //그리드에 칼럼 콤보박스로 만들어주기
        private void AddComboBoxColumns()
        {
            try
            {
                //시험종류
                DataGridViewComboBoxColumn column1 = new DataGridViewComboBoxColumn();
                {
                    column1.DataPropertyName = "Test_Type";
                    //column.DropDownWidth = 160;

                    column1.MaxDropDownItems = 20;
                    column1.FlatStyle = FlatStyle.Standard;

                }
                column1.DataSource = TestType();
                column1.DisplayMember = "Code_Dtl_Name";
                column1.ValueMember = "Code_Dtl";
                column1.Name = "Test_Type";
                dgvSchedDtl.Columns.Insert(3, column1);

                //파레트 위치
                DataGridViewComboBoxColumn column2 = new DataGridViewComboBoxColumn();
                {
                    column2.DataPropertyName = "Sched_Loc";
                    column2.MaxDropDownItems = 20;
                    column2.FlatStyle = FlatStyle.Standard;
                }
                column2.DataSource = PalletNum();
                column2.DisplayMember = "Code_Dtl_Name";
                column2.ValueMember = "Code_Dtl";
                column2.Name = "Sched_Loc";
                dgvSchedDtl.Columns.Insert(4, column2);
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FS109", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //기본값 버튼 클릭
        private void btnDefault_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSchedDtl.Rows.Count > 0)
                {
                    for (int i = 0; i < dgvSchedDtl.Rows.Count; i++)
                    {
                        if (dgvSchedDtl.Rows[i].Cells["Test_Type"].Value.ToString() == "01")
                        {
                            dgvSchedDtl.Rows[i].Cells["Add1"].Value = 10;
                            dgvSchedDtl.Rows[i].Cells["Add2"].Value = 20;
                        }
                        else if (dgvSchedDtl.Rows[i].Cells["Test_Type"].Value.ToString() == "02")
                        {
                            dgvSchedDtl.Rows[i].Cells["Add1"].Value = 24;
                            dgvSchedDtl.Rows[i].Cells["Add2"].Value = 0;
                        }
                        else if (dgvSchedDtl.Rows[i].Cells["Test_Type"].Value.ToString() == "03")
                        {
                            dgvSchedDtl.Rows[i].Cells["Add1"].Value = 24;
                            dgvSchedDtl.Rows[i].Cells["Add2"].Value = 0;
                        }
                        else if (dgvSchedDtl.Rows[i].Cells["Test_Type"].Value.ToString() == "04")
                        {
                            dgvSchedDtl.Rows[i].Cells["Add1"].Value = 24;
                            dgvSchedDtl.Rows[i].Cells["Add2"].Value = 0;
                        }
                        else
                        {
                            dgvSchedDtl.Rows[i].Cells["Add1"].Value = 0;
                            dgvSchedDtl.Rows[i].Cells["Add2"].Value = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FS110", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //그리드에 콤보박스 클릭할때 바로 리스트 열리도록
        private void dgvSchedDtl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvSchedDtl.CurrentCell.EditType == typeof(DataGridViewComboBoxEditingControl))
            {
                if (e.RowIndex >= 0)
                {
                    //SendKeys.Send("{F4}");
                }
            }
        }

        //엑셀 업로드 버튼 클릭
        private void btnExcelUpload_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Excel 통합 문서(.xlsx)|";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                DataSet tempDS = new DataSet();
                //tempDS 초기화
                tempDS.Clear();

                string filePath = openFileDialog1.FileName;
                string fileExtension = Path.GetExtension(filePath);
                //string header = rbHeaderYes.Checked ? "Yes" : "No";
                string header = "Yes";
                string connectionString = string.Empty;
                string sheetName = string.Empty;

                //커넥션 스트링을 가져옮
                connectionString = string.Format(Excel07ConString, filePath, header);

                // 첫 번째 시트의 이름을 가져옮
                using (OleDbConnection con = new OleDbConnection(connectionString))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        con.Close();
                    }
                }
                Console.WriteLine("sheetName = " + sheetName);

                // 첫 번째 쉬트의 데이타를 읽어서 datagridview 에 보이게 함.
                using (OleDbConnection con = new OleDbConnection(connectionString))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        using (OleDbDataAdapter oda = new OleDbDataAdapter())
                        {

                            cmd.CommandText = "SELECT * From [" + sheetName + "]";
                            cmd.Connection = con;
                            con.Open();
                            oda.SelectCommand = cmd;
                            oda.Fill(tempDS);
                            con.Close();

                            for (int i = 0; i < tempDS.Tables[0].Rows.Count; i++)
                            {
                                tempDS.Tables[0].Rows[i].SetAdded();

                                DataRow newRow = dtlDS.Tables[0].NewRow();


                                newRow["Sched_Dtl_Seq"] = String.Format("{0,3:000}", GetSchedDtlSeq());
                                newRow["Test_Type"] = tempDS.Tables[0].Rows[i]["시험종류"];
                                newRow["Sched_Loc"] = tempDS.Tables[0].Rows[i]["파레트위치"];
                                newRow["Rpt_Cnt"] = tempDS.Tables[0].Rows[i]["반복회수"];
                                newRow["Add1"] = tempDS.Tables[0].Rows[i]["추가1"];
                                newRow["Add2"] = tempDS.Tables[0].Rows[i]["추가2"];
                                newRow["Add3"] = tempDS.Tables[0].Rows[i]["추가3"];

                                dtlDS.Tables[0].Rows.Add(newRow);
                            }

                            //Populate DataGridView.
                            dgvSchedDtl.DataSource = this.dtlDS.Tables[0];

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FS111", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        } 

    }
}
