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
        public FormSetCorrectValue Form_CorrectValue;

        public DataSet mainDS = new DataSet();
        public DataSet dtlDS = new DataSet();
        DbLink Dblink = new DbLink();

        public FormCorrectValueWork(FormSetCorrectValue form) 
        {
            Form_CorrectValue = form;
        }

        public void FormLoad()
        {
            Form_CorrectValue.dgvCaliList.Rows.Clear();

            mainDS = new DataSet();
            string query = "EXEC _SCaliMstQuery";
            Dblink.AllSelect(query, mainDS);

            for(int i=0; i < mainDS.Tables[0].Rows.Count; i++)
            {
                Form_CorrectValue.dgvCaliList.Rows.Add();
                Form_CorrectValue.dgvCaliList.Rows[i].Cells[0].Value = mainDS.Tables[0].Rows[i][0].ToString();
                Form_CorrectValue.dgvCaliList.Rows[i].Cells[1].Value = mainDS.Tables[0].Rows[i][1].ToString();
            }
            Form_CorrectValue.dgvCaliList.Rows.Add();
            Form_CorrectValue.dgvCaliList.Rows[mainDS.Tables[0].Rows.Count].Cells[0].Value = "00";
            Form_CorrectValue.dgvCaliList.Rows[mainDS.Tables[0].Rows.Count].Cells[1].Value = "접촉저항";

            Form_CorrectValue.dgvCaliList.Sort(Form_CorrectValue.dgvCaliList.Columns[0], System.ComponentModel.ListSortDirection.Ascending);


        } // 폼 로드시 그리드뷰 데이터 로드

        public void CellClick() 
        {
            try
            {
                string pQuery = string.Empty;
                dtlDS.Clear();

                if (Form_CorrectValue.dgvCaliList.SelectedRows[0].Cells[0].Value.ToString() == "00")       //접촉저항
                {
                    TextBoxClear("접촉저항");
                    pQuery = "EXEC _SCaliQuery '', '2'";
                    Dblink.AllSelect(pQuery, dtlDS);
                    for (int i = 0; i < dtlDS.Tables[0].Rows.Count; i++)
                    {
                        Form_CorrectValue.txtContactSeq.Text = Form_CorrectValue.dgvCaliList.SelectedRows[0].Cells[0].Value.ToString();
                        Form_CorrectValue.txtN1VoltA.Text = dtlDS.Tables[0].Rows[i]["N1VoltA"].ToString();
                        Form_CorrectValue.txtN2VoltA.Text = dtlDS.Tables[0].Rows[i]["N2VoltA"].ToString();
                        Form_CorrectValue.txtN3VoltA.Text = dtlDS.Tables[0].Rows[i]["N3VoltA"].ToString();
                        Form_CorrectValue.txtN4VoltA.Text = dtlDS.Tables[0].Rows[i]["N4VoltA"].ToString();
                        Form_CorrectValue.txtN5VoltA.Text = dtlDS.Tables[0].Rows[i]["N5VoltA"].ToString();
                        Form_CorrectValue.txtN6VoltA.Text = dtlDS.Tables[0].Rows[i]["N6VoltA"].ToString();
                        Form_CorrectValue.txtN7VoltA.Text = dtlDS.Tables[0].Rows[i]["N7VoltA"].ToString();
                        Form_CorrectValue.txtN8VoltA.Text = dtlDS.Tables[0].Rows[i]["N8VoltA"].ToString();
                        Form_CorrectValue.txtN9VoltA.Text = dtlDS.Tables[0].Rows[i]["N9VoltA"].ToString();
                        Form_CorrectValue.txtN10VoltA.Text = dtlDS.Tables[0].Rows[i]["N10VoltA"].ToString();
                        Form_CorrectValue.txtN1VoltB.Text = dtlDS.Tables[0].Rows[i]["N1VoltB"].ToString();
                        Form_CorrectValue.txtN2VoltB.Text = dtlDS.Tables[0].Rows[i]["N2VoltB"].ToString();
                        Form_CorrectValue.txtN3VoltB.Text = dtlDS.Tables[0].Rows[i]["N3VoltB"].ToString();
                        Form_CorrectValue.txtN4VoltB.Text = dtlDS.Tables[0].Rows[i]["N4VoltB"].ToString();
                        Form_CorrectValue.txtN5VoltB.Text = dtlDS.Tables[0].Rows[i]["N5VoltB"].ToString();
                        Form_CorrectValue.txtN6VoltB.Text = dtlDS.Tables[0].Rows[i]["N6VoltB"].ToString();
                        Form_CorrectValue.txtN7VoltB.Text = dtlDS.Tables[0].Rows[i]["N7VoltB"].ToString();
                        Form_CorrectValue.txtN8VoltB.Text = dtlDS.Tables[0].Rows[i]["N8VoltB"].ToString();
                        Form_CorrectValue.txtN9VoltB.Text = dtlDS.Tables[0].Rows[i]["N9VoltB"].ToString();
                        Form_CorrectValue.txtN10VoltB.Text = dtlDS.Tables[0].Rows[i]["N10VoltB"].ToString();

                        Form_CorrectValue.txtR1VoltA.Text = dtlDS.Tables[0].Rows[i]["R1VoltA"].ToString();
                        Form_CorrectValue.txtR2VoltA.Text = dtlDS.Tables[0].Rows[i]["R2VoltA"].ToString();
                        Form_CorrectValue.txtR3VoltA.Text = dtlDS.Tables[0].Rows[i]["R3VoltA"].ToString();
                        Form_CorrectValue.txtR4VoltA.Text = dtlDS.Tables[0].Rows[i]["R4VoltA"].ToString();
                        Form_CorrectValue.txtR5VoltA.Text = dtlDS.Tables[0].Rows[i]["R5VoltA"].ToString();
                        Form_CorrectValue.txtR6VoltA.Text = dtlDS.Tables[0].Rows[i]["R6VoltA"].ToString();
                        Form_CorrectValue.txtR7VoltA.Text = dtlDS.Tables[0].Rows[i]["R7VoltA"].ToString();
                        Form_CorrectValue.txtR8VoltA.Text = dtlDS.Tables[0].Rows[i]["R8VoltA"].ToString();
                        Form_CorrectValue.txtR9VoltA.Text = dtlDS.Tables[0].Rows[i]["R9VoltA"].ToString();
                        Form_CorrectValue.txtR10VoltA.Text = dtlDS.Tables[0].Rows[i]["R10VoltA"].ToString();
                        Form_CorrectValue.txtR1VoltB.Text = dtlDS.Tables[0].Rows[i]["R1VoltB"].ToString();
                        Form_CorrectValue.txtR2VoltB.Text = dtlDS.Tables[0].Rows[i]["R2VoltB"].ToString();
                        Form_CorrectValue.txtR3VoltB.Text = dtlDS.Tables[0].Rows[i]["R3VoltB"].ToString();
                        Form_CorrectValue.txtR4VoltB.Text = dtlDS.Tables[0].Rows[i]["R4VoltB"].ToString();
                        Form_CorrectValue.txtR5VoltB.Text = dtlDS.Tables[0].Rows[i]["R5VoltB"].ToString();
                        Form_CorrectValue.txtR6VoltB.Text = dtlDS.Tables[0].Rows[i]["R6VoltB"].ToString();
                        Form_CorrectValue.txtR7VoltB.Text = dtlDS.Tables[0].Rows[i]["R7VoltB"].ToString();
                        Form_CorrectValue.txtR8VoltB.Text = dtlDS.Tables[0].Rows[i]["R8VoltB"].ToString();
                        Form_CorrectValue.txtR9VoltB.Text = dtlDS.Tables[0].Rows[i]["R9VoltB"].ToString();
                        Form_CorrectValue.txtR10VoltB.Text = dtlDS.Tables[0].Rows[i]["R10VoltB"].ToString();
                    }
                } // 접촉저항 데이터 조회
                else                                                                            
                {
                    TextBoxClear("시험기");
                   pQuery = "EXEC _SCaliQuery '" + Form_CorrectValue.dgvCaliList.SelectedRows[0].Cells[0].Value.ToString() + "', '1'";
                    Dblink.AllSelect(pQuery, dtlDS);
                    for (int i = 0; i < dtlDS.Tables[0].Rows.Count; i++)
                    {
                        Form_CorrectValue.txtCaliSeq.Text = Form_CorrectValue.dgvCaliList.SelectedRows[0].Cells[0].Value.ToString();
                        Form_CorrectValue.txtOperA.Text = dtlDS.Tables[0].Rows[i]["OperA"].ToString();
                        Form_CorrectValue.txtOperB.Text = dtlDS.Tables[0].Rows[i]["OperB"].ToString();
                        Form_CorrectValue.txtDropA.Text = dtlDS.Tables[0].Rows[i]["DropA"].ToString();
                        Form_CorrectValue.txtDropB.Text = dtlDS.Tables[0].Rows[i]["DropB"].ToString();
                        Form_CorrectValue.txtCVoltA.Text = dtlDS.Tables[0].Rows[i]["CVoltA"].ToString();
                        Form_CorrectValue.txtCCurrA.Text = dtlDS.Tables[0].Rows[i]["CCurrA"].ToString();
                        Form_CorrectValue.txtCResiB.Text = dtlDS.Tables[0].Rows[i]["CResiB"].ToString();
                    }
                } // 동작전류, 낙하전류, 동작시간, 복귀시간 데이터 조회
            }
            catch (Exception ex)
            {
                Form_CorrectValue.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCM102", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        } // 좌측 그리드뷰 셀 클릭 이벤트

        public void TextBoxClear(string str)
        {
            if(str == "접촉저항")
            {
                Form_CorrectValue.grbContact.Visible = true;
                Form_CorrectValue.grbTester.Visible = false;
                Form_CorrectValue.grbContact.Dock = DockStyle.Left;
                Form_CorrectValue.grbTester.Dock = DockStyle.Right;
                foreach (Control ControlTextBoxClear in Form_CorrectValue.grbTester.Controls)
                {

                    if (typeof(TextBox) == ControlTextBoxClear.GetType())
                    {
                        (ControlTextBoxClear as TextBox).Text = "";
                    }

                }
            }
            else
            {
                Form_CorrectValue.grbContact.Visible = false;
                Form_CorrectValue.grbTester.Visible = true;
                Form_CorrectValue.grbContact.Dock = DockStyle.Right;
                Form_CorrectValue.grbTester.Dock = DockStyle.Left;
                foreach (Control ControlTextBoxClear in Form_CorrectValue.grbContact.Controls)
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
                if (Form_CorrectValue.dgvCaliList.SelectedRows[0].Cells[0].Value.ToString() == "00")       //접촉저항
                {
                    if (Form_CorrectValue.txtContactSeq.Text != "")
                    {
                        MessageBox.Show("새로운 보정값을 추가하려면,\n\"추가\"버튼을 클릭하여 보정값 입력후 저장하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        string pQuery = string.Format("EXEC _SCaliSave2 {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40}, '1'"
                               , "00"
                               , Convert.ToDouble(Form_CorrectValue.txtN1VoltA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtN1VoltB.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtN2VoltA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtN2VoltB.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtN3VoltA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtN3VoltB.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtN4VoltA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtN4VoltB.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtN5VoltA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtN5VoltB.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtN6VoltA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtN6VoltB.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtN7VoltA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtN7VoltB.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtN8VoltA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtN8VoltB.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtN9VoltA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtN9VoltB.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtN10VoltA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtN10VoltB.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtR1VoltA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtR1VoltB.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtR2VoltA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtR2VoltB.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtR3VoltA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtR3VoltB.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtR4VoltA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtR4VoltB.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtR5VoltA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtR5VoltB.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtR6VoltA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtR6VoltB.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtR7VoltA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtR7VoltB.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtR8VoltA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtR8VoltB.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtR9VoltA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtR9VoltB.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtR10VoltA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtR10VoltB.Text)
                               );

                        Dblink.ModifyMethod(pQuery);
                    }
                }
                else
                {
                    if (Form_CorrectValue.txtCaliSeq.Text != "")
                    {
                        MessageBox.Show("새로운 보정값을 추가하려면,\n\"추가\"버튼을 클릭하여 보정값 입력후 저장하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        string pQuery = string.Format("EXEC _SCaliSave {0},{1},{2},{3},{4},{5},{6},{7}, '1'"
                               , Form_CorrectValue.dgvCaliList.SelectedRows[0].Cells[0].Value.ToString()
                               , Convert.ToDouble(Form_CorrectValue.txtOperA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtOperB.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtDropA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtDropB.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtCVoltA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtCCurrA.Text)
                               , Convert.ToDouble(Form_CorrectValue.txtCResiB.Text)
                               );

                        Dblink.ModifyMethod(pQuery);
                    }

                }

                MessageBox.Show("저장이 완료되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Form_CorrectValue.Cursor = Cursors.Default;
                MessageBox.Show("작업중 오류가 발생했습니다.\n\n" + ex.Message + "\n\n Error Code : FCM101", "에러", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        } // 저장버튼 클릭 이벤트
    }
}
