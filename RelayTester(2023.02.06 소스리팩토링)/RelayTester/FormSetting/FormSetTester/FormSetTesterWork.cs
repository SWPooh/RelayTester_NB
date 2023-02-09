using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelayTester
{
    public class FormSetTesterWork
    {
        public FormSetTester Form_SetTester;

        public DataSet mainDS = new DataSet();
        public DataSet dtlDS = new DataSet();
        DbLink Dblink = new DbLink();

        public string sCodeMst;
        public int rowindex = 0;

        public FormSetTesterWork(FormSetTester form) 
        {
            Form_SetTester= form;
        }

        public void FormLoad()
        {
            rowindex = 0;
            QueryClick();
        } //폼 로드 이벤트

        public void QueryClick()
        {
            CommonWork.TextboxClear(Form_SetTester.grbRelayDtl);

            string pQuery = string.Empty;
            string pCodeNm = Form_SetTester.txtCodeName.Text;

            pQuery = "EXEC _SRelayQuery '" + pCodeNm + "', '1'";

            mainDS.Clear();
            Form_SetTester.dgvRelayList.DataSource = null;

            Dblink.AllSelect(pQuery, mainDS);
            Form_SetTester.dgvRelayList.DataSource = mainDS.Tables[0];
            if (mainDS.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("조회된 자료가 없습니다.", "조회", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Form_SetTester.dgvRelayList.Rows[0].Selected = true;
            }


            

            GridResetMethod();
        } // 계전기 리스트 조회, 조회버튼 클릭 이벤트

        private void GridResetMethod()
        {
            //dgvRelayList.DataSource = mainDS.Tables[0];
            ////컬럼명
            Form_SetTester.dgvRelayList.Columns["RelayCode"].HeaderCell.Value = "계전기코드";
            Form_SetTester.dgvRelayList.Columns["RelayName"].HeaderCell.Value = "계전기명";

            ////컬럼 비지블

            ////컬럼 인에이블
            Form_SetTester.dgvRelayList.Columns["RelayCode"].ReadOnly = true;
            Form_SetTester.dgvRelayList.Columns["RelayName"].ReadOnly = true;

            ////컬럼 사이즈
            Form_SetTester.dgvRelayList.Columns["RelayCode"].Width = 150;
            Form_SetTester.dgvRelayList.Columns["RelayName"].Width = 250;
        } // 좌측 그리드뷰 헤더명 설정

        public void NewClick()
        {
            Form_SetTester.txtCodeName.Text = "";
            mainDS.Clear();
            dtlDS.Clear();
            GridResetMethod();

            foreach (Control ctl in Form_SetTester.grbRelayDtl.Controls)
            {
                if (ctl.GetType().ToString().Equals("System.Windows.Forms.TextBox") || ctl.GetType().ToString().Equals("System.Windows.Forms.MaskedTextBox"))
                {
                    ctl.Text = "";
                }
            }
        } //신규버튼 클릭 이벤트

        public void AddClick()
        {
            CommonWork.TextboxClear(Form_SetTester.grbRelayDtl);

            //코드 마스터에 마지막로우에 넣기

            DataSet SeqDs = new DataSet();

            mainDS.Tables[0].Rows.Add();
            DataTable dt = mainDS.Tables[0];
            DataRow[] rows = dt.Select();

            Form_SetTester.dgvRelayList.DataSource = mainDS.Tables[0];
            Form_SetTester.dgvRelayList.Rows[Form_SetTester.dgvRelayList.Rows.Count - 1].Selected= true;

            string query = "EXEC _SRelaySeqQuery";
            Dblink.AllSelect(query, SeqDs);

            Form_SetTester.txtRelayTypeCode.Text = SeqDs.Tables[0].Rows[0][0].ToString();

        } //추가버튼 클릭 이벤트

        public void DeleteClick()
        {
            if (MessageBox.Show("선택한 데이터를 삭제 하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)     //저장하고 초기화 안함
            {
                string pQuery = "EXEC _SRelaySave '" + 
                                 Form_SetTester.dgvRelayList.SelectedRows[0].Cells[0].Value.ToString() + "'," +
                                 "'', " +
                                 "'', " +
                                 "'', " +
                                 "'', " +
                                 "'', " +
                                 "'', " +
                                 "0, " +
                                 "0, " +
                                 "0, " +
                                 "'', " +
                                 "''," +
                                 "''," + //RGB A
                                 "''," + //RGB R
                                 "''," + //RGB G
                                 "''," + //RGB B
                                 "'3'";
                Dblink.ModifyMethod(pQuery);

                QueryClick();
                MessageBox.Show("삭제가 완료되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        } //삭제버튼 클릭 이벤트

        public void SaveClick()
        {
            //계전기코드 필수체크
            if (Form_SetTester.txtRelayCode.Text.Trim() == null || Form_SetTester.txtRelayCode.Text.Trim() == "")
            {
                MessageBox.Show("계전기코드는 필수 항목 입니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Form_SetTester.txtRelayCode.Select();
                return;
            }
            //계전기명 필수체크
            if (Form_SetTester.txtRelayName.Text.Trim() == null || Form_SetTester.txtRelayName.Text.Trim() == "")
            {
                MessageBox.Show("계전기명은 필수 항목 입니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Form_SetTester.txtRelayName.Select();
                return;
            }
            //관련규격 필수체크
            if (Form_SetTester.txtRelatedSpec.Text.Trim() == null || Form_SetTester.txtRelatedSpec.Text.Trim() == "")
            {
                MessageBox.Show("관련규격은 필수 항목 입니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Form_SetTester.txtRelatedSpec.Select();
                return;
            }
            //접점형태U 필수체크
            if (Form_SetTester.txtContectU.Text.Trim() == null || Form_SetTester.txtContectU.Text.Trim() == "")
            {
                MessageBox.Show("접점형태-U는 필수 항목 입니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Form_SetTester.txtContectU.Select();
                return;
            }
            //접점형태D 필수체크
            if (Form_SetTester.txtContectD.Text.Trim() == null || Form_SetTester.txtContectD.Text.Trim() == "")
            {
                MessageBox.Show("접점형태-D는 필수 항목 입니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Form_SetTester.txtContectD.Select();
                return;
            }
            // 치수A 필수체크
            if (Form_SetTester.txtSizeA.Text.Trim() == null || Form_SetTester.txtSizeA.Text.Trim() == "")
            {
                MessageBox.Show("치수A는 필수 항목 입니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Form_SetTester.txtSizeA.Select();
                return;
            }
            // 치수B 필수체크
            if (Form_SetTester.txtSizeB.Text.Trim() == null || Form_SetTester.txtSizeB.Text.Trim() == "")
            {
                MessageBox.Show("치수B는 필수 항목 입니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Form_SetTester.txtSizeB.Select();
                return;
            }
            // 치수C 필수체크
            if (Form_SetTester.txtSizeC.Text.Trim() == null || Form_SetTester.txtSizeC.Text.Trim() == "")
            {
                MessageBox.Show("치수C는 필수 항목 입니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Form_SetTester.txtSizeC.Select();
                return;
            }
            // 치수D 필수체크
            if (Form_SetTester.txtSizeD.Text.Trim() == null || Form_SetTester.txtSizeD.Text.Trim() == "")
            {
                MessageBox.Show("치수D는 필수 항목 입니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Form_SetTester.txtSizeD.Select();
                return;
            }

            if (Form_SetTester.dgvRelayList.SelectedRows[0].Cells[0].Value.ToString().Trim() == null || Form_SetTester.dgvRelayList.SelectedRows[0].Cells[0].Value.ToString().Trim() == "")
            {
                //마스터 저장 쿼리
                string pQuery1 = "EXEC _SRelaySave '" + 
                                Form_SetTester.txtRelayCode.Text + "', '" + 
                                Form_SetTester.txtRelayName.Text + "', '" + 
                                Form_SetTester.txtReportSpec.Text + "', '" + 
                                Form_SetTester.txtRelatedSpec.Text + "', '" + 
                                Form_SetTester.txtContectU.Text + "', '" + 
                                Form_SetTester.txtContectD.Text + "', " + 
                                Form_SetTester.txtSizeA.Text + ", " + 
                                Form_SetTester.txtSizeB.Text + ", " + 
                                Form_SetTester.txtSizeC.Text + ", " + 
                                Form_SetTester.txtSizeD.Text + ", '" + 
                                Form_SetTester.txtRemark.Text + "','" + 
                                Form_SetTester.txtRelaySimpleName.Text + "','" +
                                Form_SetTester.txtA.Text + "','" +
                                Form_SetTester.txtR.Text + "','" +
                                Form_SetTester.txtG.Text + "','" +
                                Form_SetTester.txtB.Text + "','1'";

                Dblink.ModifyMethod(pQuery1);
            }
            else
            {
                string pQuery1 = "EXEC _SRelaySave '" + 
                                 Form_SetTester.dgvRelayList.SelectedRows[0].Cells[0].Value.ToString().Trim() + "', '" + 
                                 Form_SetTester.txtRelayName.Text + "', '" + 
                                 Form_SetTester.txtReportSpec.Text + "', '" + 
                                 Form_SetTester.txtRelatedSpec.Text + "', '" + 
                                 Form_SetTester.txtContectU.Text + "', '" + 
                                 Form_SetTester.txtContectD.Text + "', " + 
                                 Form_SetTester.txtSizeA.Text + ", " + 
                                 Form_SetTester.txtSizeB.Text + ", " + 
                                 Form_SetTester.txtSizeC.Text + ", " + 
                                 Form_SetTester.txtSizeD.Text + ", '" + 
                                 Form_SetTester.txtRemark.Text + "','" + 
                                 Form_SetTester.txtRelaySimpleName.Text + "', " +
                                 "''," + //RGB A
                                 "''," + //RGB R
                                 "''," + //RGB G
                                 "''," + //RGB B
                                 "'2'";
                Dblink.ModifyMethod(pQuery1);
            }

            QueryClick();
            MessageBox.Show("저장이 완료되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
        } //저장버튼 클릭 이벤트

        public void CellClick(DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    foreach (Control ctl in Form_SetTester.grbRelayDtl.Controls)
                    {
                        if (ctl.GetType().ToString().Equals("System.Windows.Forms.TextBox") || ctl.GetType().ToString().Equals("System.Windows.Forms.MaskedTextBox"))
                        {
                            ctl.Text = "";
                        }
                    }
                    dtlDS.Clear();

                    Form_SetTester.pictureBox1.Image = null;
                    Form_SetTester.pictureBox2.Image = null;
                    Form_SetTester.pictureBox3.Image = null;

                    rowindex = e.RowIndex;

                    string pQuery2 = string.Empty;
                    sCodeMst = string.Empty;
                    sCodeMst = Form_SetTester.dgvRelayList.Rows[e.RowIndex].Cells[0].Value.ToString();

                    pQuery2 = "EXEC _SRelayQuery '" + sCodeMst + "', '2'";

                    Dblink.AllSelect(pQuery2, dtlDS);

                    for (int i = 0; i < dtlDS.Tables[0].Rows.Count; i++)
                    {
                        Form_SetTester.txtRelayCode.Text = dtlDS.Tables[0].Rows[i][0].ToString();       //"RelayCode"
                        Form_SetTester.txtRelayName.Text = dtlDS.Tables[0].Rows[i][1].ToString();       //"RelayName"
                        Form_SetTester.txtReportSpec.Text = dtlDS.Tables[0].Rows[i][2].ToString();      //"ReportSpec"
                        Form_SetTester.txtRelatedSpec.Text = dtlDS.Tables[0].Rows[i][3].ToString();     //"RelatedSpec"
                        Form_SetTester.txtContectU.Text = dtlDS.Tables[0].Rows[i][4].ToString();        //"ContectU"
                        Form_SetTester.txtContectD.Text = dtlDS.Tables[0].Rows[i][5].ToString();        //"ContectD"
                        Form_SetTester.txtSizeA.Text = dtlDS.Tables[0].Rows[i][6].ToString();           //"SizeA"
                        Form_SetTester.txtSizeB.Text = dtlDS.Tables[0].Rows[i][7].ToString();           //"SizeB"
                        Form_SetTester.txtSizeC.Text = dtlDS.Tables[0].Rows[i][8].ToString();           //"SizeC"
                        Form_SetTester.txtSizeD.Text = dtlDS.Tables[0].Rows[i][9].ToString();           //"SizeD"
                        Form_SetTester.txtImg1.Text = dtlDS.Tables[0].Rows[i][10].ToString();           //"Img1Name"
                        Form_SetTester.txtImg2.Text = dtlDS.Tables[0].Rows[i][11].ToString();           //"Img2Name"
                        Form_SetTester.txtImg3.Text = dtlDS.Tables[0].Rows[i][12].ToString();           //"Img3Name"
                        Form_SetTester.txtRemark.Text = dtlDS.Tables[0].Rows[i][13].ToString();         //"Remark"
                        Form_SetTester.txtRelaySimpleName.Text = dtlDS.Tables[0].Rows[i][17].ToString();         //"RelaySimpleName"
                        Form_SetTester.txtRelayTypeCode.Text = dtlDS.Tables[0].Rows[i][18].ToString();         //"RelayType"
                        Form_SetTester.txtA.Text = dtlDS.Tables[0].Rows[i][19].ToString();         //"RGB/A" 투명도
                        Form_SetTester.txtR.Text = dtlDS.Tables[0].Rows[i][20].ToString();         //"RGB/R" RED
                        Form_SetTester.txtG.Text = dtlDS.Tables[0].Rows[i][21].ToString();         //"RGB/G" GREEN
                        Form_SetTester.txtB.Text = dtlDS.Tables[0].Rows[i][22].ToString();         //"RGB/B" BLUE

                        if (dtlDS.Tables[0].Rows[i]["Img1Name"].ToString() != null && dtlDS.Tables[0].Rows[i]["Img1Name"].ToString() != "")
                        {
                            //Form_SetTester.pictureBox1.Image = ByteArrayToImage((Byte[])dtlDS.Tables[0].Rows[i]["Img1"]);
                            Form_SetTester.pictureBox1.Load(dtlDS.Tables[0].Rows[i]["Img1"].ToString());
                            Form_SetTester.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        if (dtlDS.Tables[0].Rows[i]["Img2Name"].ToString() != null && dtlDS.Tables[0].Rows[i]["Img2Name"].ToString() != "")
                        {
                            //Form_SetTester.pictureBox2.Image = ByteArrayToImage((Byte[])dtlDS.Tables[0].Rows[i]["Img2"]);
                            Form_SetTester.pictureBox2.Load(dtlDS.Tables[0].Rows[i]["Img2"].ToString());
                            Form_SetTester.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        if (dtlDS.Tables[0].Rows[i]["Img3Name"].ToString() != null && dtlDS.Tables[0].Rows[i]["Img3Name"].ToString() != "")
                        {
                            //Form_SetTester.pictureBox3.Image = ByteArrayToImage((Byte[])dtlDS.Tables[0].Rows[i]["Img3"]);
                            Form_SetTester.pictureBox3.Load(dtlDS.Tables[0].Rows[i]["Img3"].ToString());
                            Form_SetTester.pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                    }

                    Form_SetTester.dgvRelayList.CurrentCell = Form_SetTester.dgvRelayList.Rows[rowindex].Cells[e.ColumnIndex];

                    GridResetMethod();

                    ColorLoad();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        } //좌측 그리드뷰 셀 클릭 이벤트
        
        public void ColorClick()
        {
            if (Form_SetTester.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                //panel1.BackColor = colorDialog1.Color;

                Form_SetTester.txtA.Text = "100";
                Form_SetTester.txtR.Text = Form_SetTester.colorDialog1.Color.R.ToString();
                Form_SetTester.txtG.Text = Form_SetTester.colorDialog1.Color.G.ToString();
                Form_SetTester.txtB.Text = Form_SetTester.colorDialog1.Color.B.ToString();

                Form_SetTester.pnl_color.BackColor = Form_SetTester.colorDialog1.Color;

            }
        } //색상 등록버튼 클릭 이벤트

        public void ColorLoad()
        {
            
            int a = int.Parse(Form_SetTester.txtA.Text);
            int r = int.Parse(Form_SetTester.txtR.Text);
            int g = int.Parse(Form_SetTester.txtG.Text);
            int b = int.Parse(Form_SetTester.txtB.Text);
            if (a.ToString().Length < 1)
            {
                a = 100;
                r = 0;
                g = 0;
                b = 0;
            }
            else
            {
                Form_SetTester.pnl_color.BackColor = Color.FromArgb(a, r, g, b);
                Form_SetTester.pnl_color.ForeColor = Color.FromArgb(a, r, g, b);
            }

                
        } //TEXT BOX 값에 따라 패널에 색 표시

        public void ImageSave(TextBox txtImg)
        {
            
            int txtImgCnt = int.Parse(txtImg.Name.Substring(6, 1).ToString());
            try
            {
                OpenFileDialog fDialog = new OpenFileDialog();
                if (fDialog.ShowDialog() == DialogResult.OK)
                {
                    txtImg.Text = fDialog.FileName.ToString();

                    string FilePath = txtImg.Text;
                    string fname = Form_SetTester.txtRelayCode.Text + "_" + txtImgCnt + txtImg.Text.Substring(txtImg.Text.LastIndexOf("."), txtImg.Text.Length - txtImg.Text.LastIndexOf("."));

                    string pQuery = "EXEC _SRelayImageSave '" + Form_SetTester.txtRelayCode.Text + "', '" + fname + "','" + FilePath + "','" + txtImgCnt + "'";
                    
                    Dblink.ModifyMethod(pQuery);

                    DataGridViewCellEventArgs cellIndex = new DataGridViewCellEventArgs(0, rowindex);
                    CellClick(cellIndex);

                    MessageBox.Show("이미지 등록이 완료 되었습니다.", "이미지 등록", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                //img =
            }
            catch (Exception ex)
            {

            }
        } //이미지 등록버튼 클릭 이벤트


    }
}
