using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelayTester
{
    public class FormCorrectValueWork
    {
        public FormSetCorrectValue correctValue;

        public DataSet mainDS = new DataSet();
        public DataSet dtlDS = new DataSet();
        DbLink Dblink = new DbLink();

        public FormCorrectValueWork(FormSetCorrectValue form) 
        {
            correctValue = form;
        }

        public void FormLoad()
        {
            correctValue.dgvCaliList.Rows.Clear();

            mainDS = new DataSet();
            string query = "EXEC _SCaliMstQuery";
            Dblink.AllSelect(query, mainDS);

            for(int i=0; i < mainDS.Tables[0].Rows.Count; i++)
            {
                correctValue.dgvCaliList.Rows.Add();
                correctValue.dgvCaliList.Rows[i].Cells[0].Value = mainDS.Tables[0].Rows[i][0].ToString();
                correctValue.dgvCaliList.Rows[i].Cells[1].Value = mainDS.Tables[0].Rows[i][1].ToString();
            }
            correctValue.dgvCaliList.Rows.Add();
            correctValue.dgvCaliList.Rows[mainDS.Tables[0].Rows.Count].Cells[0].Value = "00";
            correctValue.dgvCaliList.Rows[mainDS.Tables[0].Rows.Count].Cells[1].Value = "접촉저항";

            correctValue.dgvCaliList.Sort(correctValue.dgvCaliList.Columns[0], System.ComponentModel.ListSortDirection.Ascending);


        } // 폼 로드시 그리드뷰 데이터 로드

        public void CellClick() 
        {
            try
            {
                string pQuery = string.Empty;
                dtlDS.Clear();

                if (correctValue.dgvCaliList.SelectedRows[0].Cells[0].Value.ToString() == "00")       //접촉저항
                {
                    TextBoxClear("접촉저항");
                    pQuery = "EXEC _SCaliQuery '', '2'";
                    Dblink.AllSelect(pQuery, dtlDS);
                    for (int i = 0; i < dtlDS.Tables[0].Rows.Count; i++)
                    {
                        correctValue.txtContactSeq.Text = correctValue.dgvCaliList.SelectedRows[0].Cells[0].Value.ToString();
                        correctValue.txtN1VoltA.Text = dtlDS.Tables[0].Rows[i]["N1VoltA"].ToString();
                        correctValue.txtN2VoltA.Text = dtlDS.Tables[0].Rows[i]["N2VoltA"].ToString();
                        correctValue.txtN3VoltA.Text = dtlDS.Tables[0].Rows[i]["N3VoltA"].ToString();
                        correctValue.txtN4VoltA.Text = dtlDS.Tables[0].Rows[i]["N4VoltA"].ToString();
                        correctValue.txtN5VoltA.Text = dtlDS.Tables[0].Rows[i]["N5VoltA"].ToString();
                        correctValue.txtN6VoltA.Text = dtlDS.Tables[0].Rows[i]["N6VoltA"].ToString();
                        correctValue.txtN7VoltA.Text = dtlDS.Tables[0].Rows[i]["N7VoltA"].ToString();
                        correctValue.txtN8VoltA.Text = dtlDS.Tables[0].Rows[i]["N8VoltA"].ToString();
                        correctValue.txtN9VoltA.Text = dtlDS.Tables[0].Rows[i]["N9VoltA"].ToString();
                        correctValue.txtN10VoltA.Text = dtlDS.Tables[0].Rows[i]["N10VoltA"].ToString();
                        correctValue.txtN1VoltB.Text = dtlDS.Tables[0].Rows[i]["N1VoltB"].ToString();
                        correctValue.txtN2VoltB.Text = dtlDS.Tables[0].Rows[i]["N2VoltB"].ToString();
                        correctValue.txtN3VoltB.Text = dtlDS.Tables[0].Rows[i]["N3VoltB"].ToString();
                        correctValue.txtN4VoltB.Text = dtlDS.Tables[0].Rows[i]["N4VoltB"].ToString();
                        correctValue.txtN5VoltB.Text = dtlDS.Tables[0].Rows[i]["N5VoltB"].ToString();
                        correctValue.txtN6VoltB.Text = dtlDS.Tables[0].Rows[i]["N6VoltB"].ToString();
                        correctValue.txtN7VoltB.Text = dtlDS.Tables[0].Rows[i]["N7VoltB"].ToString();
                        correctValue.txtN8VoltB.Text = dtlDS.Tables[0].Rows[i]["N8VoltB"].ToString();
                        correctValue.txtN9VoltB.Text = dtlDS.Tables[0].Rows[i]["N9VoltB"].ToString();
                        correctValue.txtN10VoltB.Text = dtlDS.Tables[0].Rows[i]["N10VoltB"].ToString();

                        correctValue.txtR1VoltA.Text = dtlDS.Tables[0].Rows[i]["R1VoltA"].ToString();
                        correctValue.txtR2VoltA.Text = dtlDS.Tables[0].Rows[i]["R2VoltA"].ToString();
                        correctValue.txtR3VoltA.Text = dtlDS.Tables[0].Rows[i]["R3VoltA"].ToString();
                        correctValue.txtR4VoltA.Text = dtlDS.Tables[0].Rows[i]["R4VoltA"].ToString();
                        correctValue.txtR5VoltA.Text = dtlDS.Tables[0].Rows[i]["R5VoltA"].ToString();
                        correctValue.txtR6VoltA.Text = dtlDS.Tables[0].Rows[i]["R6VoltA"].ToString();
                        correctValue.txtR7VoltA.Text = dtlDS.Tables[0].Rows[i]["R7VoltA"].ToString();
                        correctValue.txtR8VoltA.Text = dtlDS.Tables[0].Rows[i]["R8VoltA"].ToString();
                        correctValue.txtR9VoltA.Text = dtlDS.Tables[0].Rows[i]["R9VoltA"].ToString();
                        correctValue.txtR10VoltA.Text = dtlDS.Tables[0].Rows[i]["R10VoltA"].ToString();
                        correctValue.txtR1VoltB.Text = dtlDS.Tables[0].Rows[i]["R1VoltB"].ToString();
                        correctValue.txtR2VoltB.Text = dtlDS.Tables[0].Rows[i]["R2VoltB"].ToString();
                        correctValue.txtR3VoltB.Text = dtlDS.Tables[0].Rows[i]["R3VoltB"].ToString();
                        correctValue.txtR4VoltB.Text = dtlDS.Tables[0].Rows[i]["R4VoltB"].ToString();
                        correctValue.txtR5VoltB.Text = dtlDS.Tables[0].Rows[i]["R5VoltB"].ToString();
                        correctValue.txtR6VoltB.Text = dtlDS.Tables[0].Rows[i]["R6VoltB"].ToString();
                        correctValue.txtR7VoltB.Text = dtlDS.Tables[0].Rows[i]["R7VoltB"].ToString();
                        correctValue.txtR8VoltB.Text = dtlDS.Tables[0].Rows[i]["R8VoltB"].ToString();
                        correctValue.txtR9VoltB.Text = dtlDS.Tables[0].Rows[i]["R9VoltB"].ToString();
                        correctValue.txtR10VoltB.Text = dtlDS.Tables[0].Rows[i]["R10VoltB"].ToString();
                    }
                } // 접촉저항 데이터 조회
                else                                                                            
                {
                    TextBoxClear("시험기");
                   pQuery = "EXEC _SCaliQuery '" + correctValue.dgvCaliList.SelectedRows[0].Cells[0].Value.ToString() + "', '1'";
                    Dblink.AllSelect(pQuery, dtlDS);
                    for (int i = 0; i < dtlDS.Tables[0].Rows.Count; i++)
                    {
                        correctValue.txtCaliSeq.Text = correctValue.dgvCaliList.SelectedRows[0].Cells[0].Value.ToString();
                        correctValue.txtOperA.Text = dtlDS.Tables[0].Rows[i]["OperA"].ToString();
                        correctValue.txtOperB.Text = dtlDS.Tables[0].Rows[i]["OperB"].ToString();
                        correctValue.txtDropA.Text = dtlDS.Tables[0].Rows[i]["DropA"].ToString();
                        correctValue.txtDropB.Text = dtlDS.Tables[0].Rows[i]["DropB"].ToString();
                        correctValue.txtCVoltA.Text = dtlDS.Tables[0].Rows[i]["CVoltA"].ToString();
                        correctValue.txtCCurrA.Text = dtlDS.Tables[0].Rows[i]["CCurrA"].ToString();
                        correctValue.txtCResiB.Text = dtlDS.Tables[0].Rows[i]["CResiB"].ToString();
                    }
                } // 동작전류, 낙하전류, 동작시간, 복귀시간 데이터 조회
            }
            catch (Exception ex)
            {
                correctValue.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCM102", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        } // 좌측 그리드뷰 셀 클릭 이벤트

        public void TextBoxClear(string str)
        {
            if(str == "접촉저항")
            {
                correctValue.grbContact.Visible = true;
                correctValue.grbTester.Visible = false;
                correctValue.grbContact.Dock = DockStyle.Left;
                correctValue.grbTester.Dock = DockStyle.Right;
                foreach (Control ControlTextBoxClear in correctValue.grbTester.Controls)
                {

                    if (typeof(TextBox) == ControlTextBoxClear.GetType())
                    {
                        (ControlTextBoxClear as TextBox).Text = "";
                    }

                }
            }
            else
            {
                correctValue.grbContact.Visible = false;
                correctValue.grbTester.Visible = true;
                correctValue.grbContact.Dock = DockStyle.Right;
                correctValue.grbTester.Dock = DockStyle.Left;
                foreach (Control ControlTextBoxClear in correctValue.grbContact.Controls)
                {

                    if (typeof(TextBox) == ControlTextBoxClear.GetType())
                    {
                        (ControlTextBoxClear as TextBox).Text = "";
                    }

                }
            }
        }

        public void SaveClick()
        {
            try
            {
                if (correctValue.dgvCaliList.SelectedRows[0].Cells[0].Value.ToString() == "00")       //접촉저항
                {
                    if (correctValue.txtContactSeq.Text != "")
                    {
                        MessageBox.Show("새로운 보정값을 추가하려면,\n\"추가\"버튼을 클릭하여 보정값 입력후 저장하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        string pQuery = string.Format("EXEC _SCaliSave2 {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40}, '1'"
                               , "00"
                               , Convert.ToDouble(correctValue.txtN1VoltA.Text)
                               , Convert.ToDouble(correctValue.txtN1VoltB.Text)
                               , Convert.ToDouble(correctValue.txtN2VoltA.Text)
                               , Convert.ToDouble(correctValue.txtN2VoltB.Text)
                               , Convert.ToDouble(correctValue.txtN3VoltA.Text)
                               , Convert.ToDouble(correctValue.txtN3VoltB.Text)
                               , Convert.ToDouble(correctValue.txtN4VoltA.Text)
                               , Convert.ToDouble(correctValue.txtN4VoltB.Text)
                               , Convert.ToDouble(correctValue.txtN5VoltA.Text)
                               , Convert.ToDouble(correctValue.txtN5VoltB.Text)
                               , Convert.ToDouble(correctValue.txtN6VoltA.Text)
                               , Convert.ToDouble(correctValue.txtN6VoltB.Text)
                               , Convert.ToDouble(correctValue.txtN7VoltA.Text)
                               , Convert.ToDouble(correctValue.txtN7VoltB.Text)
                               , Convert.ToDouble(correctValue.txtN8VoltA.Text)
                               , Convert.ToDouble(correctValue.txtN8VoltB.Text)
                               , Convert.ToDouble(correctValue.txtN9VoltA.Text)
                               , Convert.ToDouble(correctValue.txtN9VoltB.Text)
                               , Convert.ToDouble(correctValue.txtN10VoltA.Text)
                               , Convert.ToDouble(correctValue.txtN10VoltB.Text)
                               , Convert.ToDouble(correctValue.txtR1VoltA.Text)
                               , Convert.ToDouble(correctValue.txtR1VoltB.Text)
                               , Convert.ToDouble(correctValue.txtR2VoltA.Text)
                               , Convert.ToDouble(correctValue.txtR2VoltB.Text)
                               , Convert.ToDouble(correctValue.txtR3VoltA.Text)
                               , Convert.ToDouble(correctValue.txtR3VoltB.Text)
                               , Convert.ToDouble(correctValue.txtR4VoltA.Text)
                               , Convert.ToDouble(correctValue.txtR4VoltB.Text)
                               , Convert.ToDouble(correctValue.txtR5VoltA.Text)
                               , Convert.ToDouble(correctValue.txtR5VoltB.Text)
                               , Convert.ToDouble(correctValue.txtR6VoltA.Text)
                               , Convert.ToDouble(correctValue.txtR6VoltB.Text)
                               , Convert.ToDouble(correctValue.txtR7VoltA.Text)
                               , Convert.ToDouble(correctValue.txtR7VoltB.Text)
                               , Convert.ToDouble(correctValue.txtR8VoltA.Text)
                               , Convert.ToDouble(correctValue.txtR8VoltB.Text)
                               , Convert.ToDouble(correctValue.txtR9VoltA.Text)
                               , Convert.ToDouble(correctValue.txtR9VoltB.Text)
                               , Convert.ToDouble(correctValue.txtR10VoltA.Text)
                               , Convert.ToDouble(correctValue.txtR10VoltB.Text)
                               );

                        Dblink.ModifyMethod(pQuery);
                    }
                }
                else
                {
                    if (correctValue.txtCaliSeq.Text != "")
                    {
                        MessageBox.Show("새로운 보정값을 추가하려면,\n\"추가\"버튼을 클릭하여 보정값 입력후 저장하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        string pQuery = string.Format("EXEC _SCaliSave {0},{1},{2},{3},{4},{5},{6},{7}, '1'"
                               , correctValue.dgvCaliList.SelectedRows[0].Cells[0].Value.ToString()
                               , Convert.ToDouble(correctValue.txtOperA.Text)
                               , Convert.ToDouble(correctValue.txtOperB.Text)
                               , Convert.ToDouble(correctValue.txtDropA.Text)
                               , Convert.ToDouble(correctValue.txtDropB.Text)
                               , Convert.ToDouble(correctValue.txtCVoltA.Text)
                               , Convert.ToDouble(correctValue.txtCCurrA.Text)
                               , Convert.ToDouble(correctValue.txtCResiB.Text)
                               );

                        Dblink.ModifyMethod(pQuery);
                    }

                }

                MessageBox.Show("저장이 완료되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                correctValue.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCM101", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        } // 저장버튼 클릭 이벤트
    }
}
