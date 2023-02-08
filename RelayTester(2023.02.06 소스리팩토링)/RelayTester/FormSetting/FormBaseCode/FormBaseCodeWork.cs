using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelayTester
{
    public class FormBaseCodeWork
    {
        public FormBaseCode formBase;

        public DataSet mainDS = new DataSet();
        public DataSet dtlDS = new DataSet();
        DbLink Dblink = new DbLink();

        public string sCodeMst;
        public int rowindex = 0;

        public FormBaseCodeWork(FormBaseCode form) 
        { 
            formBase= form;
        }

        public void GridResetMethod()
        {
            formBase.dgvCommonMst.DataSource = mainDS.Tables[0];
            //컬럼명
            formBase.dgvCommonMst.Columns["Code_Mst"].HeaderCell.Value = "코드";
            formBase.dgvCommonMst.Columns["Code_Mst_Name"].HeaderCell.Value = "코드명";

            formBase.dgvCommonDtl.Columns["Code_Dtl"].HeaderCell.Value = "세부코드";
            formBase.dgvCommonDtl.Columns["Code_Dtl_Name"].HeaderCell.Value = "세부코드명";
            formBase.dgvCommonDtl.Columns["Remark"].HeaderCell.Value = "비고";

            //컬럼 비지블
            formBase.dgvCommonDtl.Columns["Code_Mst"].Visible = false;

            //컬럼 인에이블
            formBase.dgvCommonMst.Columns["Code_Mst"].ReadOnly = true;
            formBase.dgvCommonDtl.Columns["Code_Dtl"].ReadOnly = true;

            //컬럼 사이즈
            formBase.dgvCommonMst.Columns["Code_Mst"].Width = 100;
            formBase.dgvCommonMst.Columns["Code_Mst_Name"].Width = 150;
            formBase.dgvCommonDtl.Columns["Code_Dtl"].Width = 150;
            formBase.dgvCommonDtl.Columns["Code_Dtl_Name"].Width = 150;
            formBase.dgvCommonDtl.Columns["Remark"].Width = 300;

        } //DATAGRIDVIEW DATASOURCE 바인딩 후 헤더값 설정

        public void NewClick()
        {
            formBase.txtCodeName.Text = "";
            mainDS.Clear();
            dtlDS.Clear();
            GridResetMethod();
        } //신큐버튼 클릭 이벤트

        public void QueryClick()
        {
            try
            {
                string pQuery = string.Empty;
                string pCodeNm = formBase.txtCodeName.Text;

                pQuery = "EXEC _SCommonMasterQuery '" + pCodeNm + "'";

                mainDS.Clear();
                formBase.dgvCommonMst.DataSource = null;

                Dblink.AllSelect(pQuery, mainDS);
                formBase.dgvCommonMst.DataSource = mainDS.Tables[0];

                if (mainDS.Tables[0].Rows.Count > 1)
                {
                    DataGridViewCellEventArgs cellIndex = new DataGridViewCellEventArgs(0, rowindex);
                    SearchDetail(null, cellIndex);
                }

                GridResetMethod();
            }
            catch (Exception ex)
            {
                formBase.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCC101", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        } // 조회버튼 클릭 이벤트

        public void SearchDetail(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                if (e.RowIndex >= 0)
                {
                    formBase.txtCodeName.Text = formBase.dgvCommonMst.SelectedRows[0].Cells[0].Value.ToString();

                    rowindex = e.RowIndex;

                    string pQuery2 = string.Empty;
                    sCodeMst = string.Empty;
                    sCodeMst = formBase.dgvCommonMst.Rows[e.RowIndex].Cells[0].Value.ToString();

                    pQuery2 = "EXEC _SCommonDetailQuery '" + sCodeMst + "'";

                    dtlDS.Clear();
                    formBase.dgvCommonDtl.DataSource = null;

                    Dblink.AllSelect(pQuery2, dtlDS);
                    formBase.dgvCommonDtl.DataSource = dtlDS.Tables[0];

                    //dgvCommonMst.Rows[rowindex].Selected = true;
                    formBase.dgvCommonMst.CurrentCell = formBase.dgvCommonMst.Rows[rowindex].Cells[e.ColumnIndex];

                    GridResetMethod();
                }
            }
            catch (Exception ex)
            {
                formBase.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCC103", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        } // 마스트 그리드뷰 셀 선택 시 우측 그리드뷰 데이터 조회

        public void SaveData()
        {
            try
            {
                foreach (DataRow item in mainDS.Tables[0].Rows)
                {
                    if (item.RowState == DataRowState.Added)  //신규
                    {
                        string pQuery = string.Format("EXEC _SCommonMasterSave '{0}','{1}', '1'", item["Code_Mst"].ToString(), item["Code_Mst_Name"].ToString());
                        Dblink.ModifyMethod(pQuery);
                    }
                    if (item.RowState == DataRowState.Modified)  //수정
                    {
                        string pQuery = string.Format("EXEC _SCommonMasterSave '{0}','{1}', '2'", item["Code_Mst"].ToString(), item["Code_Mst_Name"].ToString());
                        Dblink.ModifyMethod(pQuery);
                    }
                    if (item.RowState == DataRowState.Deleted)  //삭제
                    {
                        string pQuery = string.Format("EXEC _SCommonMasterSave '{0}','', '3'", item["Code_Mst", DataRowVersion.Original].ToString());
                        Dblink.ModifyMethod(pQuery);
                    }
                }

                foreach (DataRow item in dtlDS.Tables[0].Rows)
                {
                    if (item.RowState == DataRowState.Added)  //신규
                    {
                        string pQuery = string.Format("EXEC _SCommonDetailSave '{0}','{1}','{2}','{3}', '1'", item["Code_Mst"].ToString(), item["Code_Dtl"].ToString(), item["Code_Dtl_Name"].ToString(), item["Remark"].ToString());
                        Dblink.ModifyMethod(pQuery);
                    }
                    if (item.RowState == DataRowState.Modified)  //수정
                    {
                        string pQuery = string.Format("EXEC _SCommonDetailSave '{0}','{1}','{2}','{3}', '2'", item["Code_Mst"].ToString(), item["Code_Dtl"].ToString(), item["Code_Dtl_Name"].ToString(), item["Remark"].ToString());
                        Dblink.ModifyMethod(pQuery);
                    }
                    if (item.RowState == DataRowState.Deleted)  //삭제
                    {
                        string pQuery = string.Format("EXEC _SCommonDetailSave '{0}', '{1}','','','3'", item["Code_Mst", DataRowVersion.Original].ToString(), item["Code_Dtl", DataRowVersion.Original].ToString());
                        Dblink.ModifyMethod(pQuery);
                    }
                }
                formBase.txtCodeName.Text = "";
                QueryClick();
                MessageBox.Show("저장이 완료되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                formBase.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCC102", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        } //저장버튼 클릭 이벤트 , 그리드뷰 데이터 저장

        public void AddClick()
        {
            try
            {
                //코드 마스터에 최대값 가져와서 증분
                DataSet tmpDS = new DataSet();

                //MessageBox.Show(mainDS.Tables[0].Rows.Count.ToString());
                string pQueryTemp = string.Empty;
                pQueryTemp = "EXEC _SCommonMasterNewSeqQuery";
                tmpDS.Clear();
                Dblink.AllSelect(pQueryTemp, tmpDS);

                DataTable dtmax = tmpDS.Tables[0];
                DataRow[] rowsmax = dtmax.Select();

                int max = Int32.Parse(rowsmax[0]["Code_Max"].ToString());
                max++;



                //코드 마스터에 마지막로우에 넣기

                mainDS.Tables[0].Rows.Add();
                DataTable dt = mainDS.Tables[0];
                DataRow[] rows = dt.Select();

                rows[mainDS.Tables[0].Rows.Count - 1]["Code_Mst"] = "CM" + String.Format("{0,3:000}", max);

                formBase.dgvCommonMst.DataSource = mainDS.Tables[0];

                DataGridViewCellEventArgs cellIndex = new DataGridViewCellEventArgs(0, max - 1);
                SearchDetail(null, cellIndex);
            }
            catch (Exception ex)
            {
                formBase.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCC102", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        } //추가버튼 클릭 이벤트

        public void AddDetailClick()
        {
            try
            {
                //코드 디테일에서 최대값 가져와서 증분
                DataSet tmpDS = new DataSet();

                int max = 0;

                string pQueryTemp = string.Empty;
                //에이징,카운트클리어 빼기위해 99, 98는 제외
                pQueryTemp = "EXEC _SCommonDetailNewSeqQuery '" + sCodeMst + "'";
                tmpDS.Clear();
                Dblink.AllSelect(pQueryTemp, tmpDS);

                DataTable dtmax = tmpDS.Tables[0];
                DataRow[] rowsmax = dtmax.Select();

                if (rowsmax[0]["Code_Max"].ToString() != "")
                {
                    max = Int32.Parse(rowsmax[0]["Code_Max"].ToString());
                }
                max++;


                if (dtlDS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in dtlDS.Tables[0].Rows)
                    {
                        //에이징,카운트클리어 빼기위해 99, 98는 제외
                        if (Int32.Parse(item["Code_Dtl"].ToString()) != 99 && Int32.Parse(item["Code_Dtl"].ToString()) != 98 && Int32.Parse(item["Code_Dtl"].ToString()) != 90 && Int32.Parse(item["Code_Dtl"].ToString()) >= max)
                        {
                            max = Int32.Parse(item["Code_Dtl"].ToString()) + 1;
                        }
                    }
                }

                //코드 디테일에 마지막로우에 넣기

                dtlDS.Tables[0].Rows.Add();
                DataTable dt = dtlDS.Tables[0];
                DataRow[] rows = dt.Select();

                rows[dtlDS.Tables[0].Rows.Count - 1]["Code_Mst"] = sCodeMst;
                rows[dtlDS.Tables[0].Rows.Count - 1]["Code_Dtl"] = String.Format("{0,2:00}", max);

                formBase.dgvCommonDtl.DataSource = dtlDS.Tables[0];

            }
            catch (Exception ex)
            {
                formBase.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCC104", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        } //디테일 추가버튼 클릭 이벤트

        public void DeleteClick()
        {
            try
            {
                if (formBase.dgvCommonMst.SelectedRows.Count > 0)
                {
                    string Mst_code = formBase.dgvCommonMst.SelectedRows[0].Cells[0].Value.ToString();
                    //DeleteDetailClick();
                    Dblink.ModifyMethod("EXEC _SCommonMasterDeleteQuery '" + Mst_code + "', '','MstDelete'");
                    formBase.txtCodeName.Text = "";
                    rowindex = 0;
                    QueryClick();
                }

            }
            catch (Exception ex)
            {

            }
        } //삭제버튼 클릭 이벤트

        public void DeleteDetailClick()
        {
            try
            {
                if (formBase.dgvCommonDtl.SelectedRows.Count > 0)
                {
                    string Mst_code = formBase.dgvCommonMst.SelectedRows[0].Cells[0].Value.ToString();
                    string Mst_dtl_code = formBase.dgvCommonDtl.SelectedRows[0].Cells[1].Value.ToString();

                    Dblink.ModifyMethod("EXEC _SCommonMasterDeleteQuery '" + Mst_code + "', '" + Mst_dtl_code + "', 'MstDetailDelete'");
                    formBase.txtCodeName.Text = "";
                    rowindex = 0;
                    QueryClick();
                }

            }
            catch (Exception ex)
            {

            }
        } //디테일 삭제버튼 클릭 이벤트
    }
}
