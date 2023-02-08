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
        public DataSet mainDS = new DataSet();
        public DataSet dtlDS = new DataSet();
        DbLink Dblink = new DbLink();

        public string sCodeMst;
        public int rowindex;


        public FormSetTester()
        {
            InitializeComponent();
        }

        private void FormRelayReg_Load(object sender, EventArgs e)
        {
            // TODO: 이 코드는 데이터를 'relayTesterDBDataSet1.TEmpReg' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            //this.tEmpRegTableAdapter.Fill(this.relayTesterDBDataSet1.TEmpReg);

            //Dblink.ConnCoForm();

            //폼로드할때 자동조회
            rowindex = 0;
            this.btnQuery_Click(null, null);

           
        }

        private void GridResetMethod()
        {
            //dgvRelayList.DataSource = mainDS.Tables[0];
            ////컬럼명
            dgvRelayList.Columns["RelayCode"].HeaderCell.Value = "계전기코드";
            dgvRelayList.Columns["RelayName"].HeaderCell.Value = "계전기명";

            ////컬럼 비지블

            ////컬럼 인에이블
            dgvRelayList.Columns["RelayCode"].ReadOnly = true;
            dgvRelayList.Columns["RelayName"].ReadOnly = true;

            ////컬럼 사이즈
            dgvRelayList.Columns["RelayCode"].Width = 150;
            dgvRelayList.Columns["RelayName"].Width = 250;
        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            txtCodeName.Text = "";
            mainDS.Clear();
            dtlDS.Clear();
            GridResetMethod();

            //for (int i = 0; i < mainDS.Tables[0].Rows.Count; i++)
            //{
            //    this.mainDS.Tables[0].Rows[i].Delete();
            //}

            foreach (Control ctl in this.grbRelayDtl.Controls)
            {
                if (ctl.GetType().ToString().Equals("System.Windows.Forms.TextBox") || ctl.GetType().ToString().Equals("System.Windows.Forms.MaskedTextBox"))
                {
                    ctl.Text = "";
                }
            }
            //for (int i = 0; i < dtlDS.Tables[0].Rows.Count; i++)
            //{
            //    this.dtlDS.Tables[0].Rows[i].Delete();
            //}

        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string pQuery = string.Empty;
            string pCodeNm = this.txtCodeName.Text;

            pQuery = "EXEC _SRelayQuery '" + pCodeNm + "', '1'";

            mainDS.Clear();
            dgvRelayList.DataSource = null;           

            Dblink.AllSelect(pQuery, mainDS);
            dgvRelayList.DataSource = mainDS.Tables[0];
            if( mainDS.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("조회된 자료가 없습니다.", "조회", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            

            if (mainDS.Tables[0].Rows.Count > 0)
            {
                DataGridViewCellEventArgs cellIndex = new DataGridViewCellEventArgs(0, rowindex);
                dgvCommonMst_CellClick(null, cellIndex);
            }
            GridResetMethod();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //코드 마스터에 마지막로우에 넣기

            mainDS.Tables[0].Rows.Add();
            DataTable dt = mainDS.Tables[0];
            DataRow[] rows = dt.Select();

            dgvRelayList.DataSource = mainDS.Tables[0];

            DataGridViewCellEventArgs cellIndex = new DataGridViewCellEventArgs(0, dgvRelayList.Rows.Count -1);
            dgvCommonMst_CellClick(null, cellIndex);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("선택한 데이터를 삭제 하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)     //저장하고 초기화 안함
            {

                /*foreach (DataGridViewRow dgr in dgvRelayList.SelectedRows)
                {
                    dgvRelayList.Rows.Remove(dgr);
                }*/

                string pQuery = "EXEC _SRelaySave '" + dgvRelayList.SelectedRows[0].Cells[0].Value.ToString() + "','', '', '', '', '', '', 0, 0, 0, '', '3'";
                Dblink.ModifyMethod(pQuery);

                /*foreach (DataRow item in mainDS.Tables[0].Rows)
                {
                    if (item.RowState == DataRowState.Deleted)  //삭제
                    {
                        
                    }
                }*/

                this.btnQuery_Click(null, null);
                MessageBox.Show("삭제가 완료되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //계전기코드 필수체크
            if (txtRelayCode.Text.Trim() == null || txtRelayCode.Text.Trim() == "")
            {
                MessageBox.Show("계전기코드는 필수 항목 입니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRelayCode.Select();
                return;
            }
            //계전기명 필수체크
            if (txtRelayName.Text.Trim() == null || txtRelayName.Text.Trim() == "")
            {
                MessageBox.Show("계전기명은 필수 항목 입니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRelayName.Select();
                return;
            }
            //관련규격 필수체크
            if (txtRelatedSpec.Text.Trim() == null || txtRelatedSpec.Text.Trim() == "")
            {
                MessageBox.Show("관련규격은 필수 항목 입니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRelatedSpec.Select();
                return;
            }
            //접점형태U 필수체크
            if (txtContectU.Text.Trim() == null || txtContectU.Text.Trim() == "")
            {
                MessageBox.Show("접점형태-U는 필수 항목 입니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtContectU.Select();
                return;
            }
            //접점형태D 필수체크
            if (txtContectD.Text.Trim() == null || txtContectD.Text.Trim() == "")
            {
                MessageBox.Show("접점형태-D는 필수 항목 입니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtContectD.Select();
                return;
            }
            // 치수A 필수체크
            if (txtSizeA.Text.Trim() == null || txtSizeA.Text.Trim() == "")
            {
                MessageBox.Show("치수A는 필수 항목 입니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSizeA.Select();
                return;
            }
            // 치수B 필수체크
            if (txtSizeB.Text.Trim() == null || txtSizeB.Text.Trim() == "")
            {
                MessageBox.Show("치수B는 필수 항목 입니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSizeB.Select();
                return;
            }
            // 치수C 필수체크
            if (txtSizeC.Text.Trim() == null || txtSizeC.Text.Trim() == "")
            {
                MessageBox.Show("치수C는 필수 항목 입니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSizeC.Select();
                return;
            }
            // 치수D 필수체크
            if (txtSizeD.Text.Trim() == null || txtSizeD.Text.Trim() == "")
            {
                MessageBox.Show("치수D는 필수 항목 입니다.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtSizeD.Select();
                return;
            }

            if (dgvRelayList.Rows[dgvRelayList.CurrentCellAddress.Y].Cells["RelayCode"].Value.ToString().Trim() == null || dgvRelayList.Rows[dgvRelayList.CurrentCellAddress.Y].Cells["RelayCode"].Value.ToString().Trim() == "")
            {
                //마스터 저장 쿼리
                string pQuery1 = "EXEC _SRelaySave '" + txtRelayCode.Text + "', '" + txtRelayName.Text + "', '" + txtReportSpec.Text + "', '" + txtRelatedSpec.Text + "', '" + txtContectU.Text + "', '" + txtContectD.Text + "', " + txtSizeA.Text + ", " + txtSizeB.Text + ", " + txtSizeC.Text + ", " + txtSizeD.Text + ", '" + txtRemark.Text + "', '1'";
                Dblink.ModifyMethod(pQuery1);
            }
            else
            {
                string pQuery1 = "EXEC _SRelaySave '" + dgvRelayList.Rows[dgvRelayList.CurrentCellAddress.Y].Cells["RelayCode"].Value.ToString().Trim() + "', '" + txtRelayName.Text + "', '" + txtReportSpec.Text + "', '" + txtRelatedSpec.Text + "', '" + txtContectU.Text + "', '" + txtContectD.Text + "', " + txtSizeA.Text + ", " + txtSizeB.Text + ", " + txtSizeC.Text + ", " + txtSizeD.Text + ", '" + txtRemark.Text + "', '2'";
                Dblink.ModifyMethod(pQuery1);
            }
            
            this.btnQuery_Click(null, null);
            MessageBox.Show("저장이 완료되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtCodeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                rowindex = 0;
                this.btnQuery_Click(null, null);
            }
        }

        private void dgvCommonMst_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    foreach (Control ctl in this.grbRelayDtl.Controls)
                    {
                        if (ctl.GetType().ToString().Equals("System.Windows.Forms.TextBox") || ctl.GetType().ToString().Equals("System.Windows.Forms.MaskedTextBox"))
                        {
                            ctl.Text = "";
                        }
                    }
                    dtlDS.Clear();

                    pictureBox1.Image = null;
                    pictureBox2.Image = null;
                    pictureBox3.Image = null;

                    rowindex = e.RowIndex;

                    string pQuery2 = string.Empty;
                    sCodeMst = string.Empty;
                    sCodeMst = dgvRelayList.Rows[e.RowIndex].Cells[0].Value.ToString();

                    pQuery2 = "EXEC _SRelayQuery '" + sCodeMst + "', '2'";

                    Dblink.AllSelect(pQuery2, dtlDS);

                    for (int i = 0; i < dtlDS.Tables[0].Rows.Count; i++)
                    {
                        txtRelayCode.Text = dtlDS.Tables[0].Rows[i]["RelayCode"].ToString();
                        txtRelayName.Text = dtlDS.Tables[0].Rows[i]["RelayName"].ToString();
                        txtReportSpec.Text = dtlDS.Tables[0].Rows[i]["ReportSpec"].ToString();
                        txtRelatedSpec.Text = dtlDS.Tables[0].Rows[i]["RelatedSpec"].ToString();
                        txtContectU.Text = dtlDS.Tables[0].Rows[i]["ContectU"].ToString();
                        txtContectD.Text = dtlDS.Tables[0].Rows[i]["ContectD"].ToString();
                        txtSizeA.Text = dtlDS.Tables[0].Rows[i]["SizeA"].ToString();
                        txtSizeB.Text = dtlDS.Tables[0].Rows[i]["SizeB"].ToString();
                        txtSizeC.Text = dtlDS.Tables[0].Rows[i]["SizeC"].ToString();
                        txtSizeD.Text = dtlDS.Tables[0].Rows[i]["SizeD"].ToString();
                        txtImg1.Text = dtlDS.Tables[0].Rows[i]["Img1Name"].ToString();
                        txtImg2.Text = dtlDS.Tables[0].Rows[i]["Img2Name"].ToString();
                        txtImg3.Text = dtlDS.Tables[0].Rows[i]["Img3Name"].ToString();
                        txtRemark.Text = dtlDS.Tables[0].Rows[i]["Remark"].ToString();

                        if (dtlDS.Tables[0].Rows[i]["Img1Name"].ToString() != null && dtlDS.Tables[0].Rows[i]["Img1Name"].ToString() != "")
                        {
                            pictureBox1.Image = ByteArrayToImage((Byte[])dtlDS.Tables[0].Rows[i]["Img1"]);
                            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        if (dtlDS.Tables[0].Rows[i]["Img2Name"].ToString() != null && dtlDS.Tables[0].Rows[i]["Img2Name"].ToString() != "")
                        {
                            pictureBox2.Image = ByteArrayToImage((Byte[])dtlDS.Tables[0].Rows[i]["Img2"]);
                            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        if (dtlDS.Tables[0].Rows[i]["Img3Name"].ToString() != null && dtlDS.Tables[0].Rows[i]["Img3Name"].ToString() != "")
                        {
                            pictureBox3.Image = ByteArrayToImage((Byte[])dtlDS.Tables[0].Rows[i]["Img3"]);
                            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                        }                   
                    }

                    dgvRelayList.CurrentCell = dgvRelayList.Rows[rowindex].Cells[e.ColumnIndex];

                    GridResetMethod();
                }
            }
            catch(Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }

        Bitmap ByteArrayToImage(byte[] b)
        {
            MemoryStream ms = new MemoryStream();
            byte[] pData = b;
            ms.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(ms, false);
            Bitmap NewImg = new Bitmap(bm);
            ms.Dispose();

            return bm;
        }

        private void btnAddDtl_Click(object sender, EventArgs e)
        {
      
        }

        private void btnDelDtl_Click(object sender, EventArgs e)
        {

        }

        private void btnImg1Add_Click(object sender, EventArgs e)
        {
            if (dgvRelayList.Rows[dgvRelayList.CurrentCellAddress.Y].Cells["RelayCode"].Value.ToString().Trim() == null || dgvRelayList.Rows[dgvRelayList.CurrentCellAddress.Y].Cells["RelayCode"].Value.ToString().Trim() == "")
            {
                MessageBox.Show("계전기 상세 정보 저장후 이미지를 등록 할 수 있습니다.", "이미지 등록", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "이미지 파일 선택";
            fDialog.Filter = "JPG Files|*.jpg|JPEG Files|*.jpeg";
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                txtImg1.Text = fDialog.FileName.ToString();

                string fname = txtRelayCode.Text + "_1" + txtImg1.Text.Substring(txtImg1.Text.LastIndexOf("."), txtImg1.Text.Length - txtImg1.Text.LastIndexOf("."));

                FileInfo fileInfo = new FileInfo(txtImg1.Text);
                FileStream fileStream = new FileStream(txtImg1.Text, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[fileInfo.Length];
                fileStream.Read(buffer, 0, Convert.ToInt32(fileInfo.Length));

                string pQuery = "EXEC _SRelayImageSave '" + txtRelayCode.Text + "', '" + fname + "',@Img,'1'";
                SqlCommand _cmd = new SqlCommand(pQuery);
                SqlParameter param = _cmd.Parameters.Add("@Img", SqlDbType.VarBinary);
                param.Value = buffer;
                Dblink.ImageModifyMethod(_cmd);


                DataGridViewCellEventArgs cellIndex = new DataGridViewCellEventArgs(0, rowindex);
                dgvCommonMst_CellClick(null, cellIndex);

                MessageBox.Show("이미지 등록이 완료 되었습니다.", "이미지 등록", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }
        private void btnImg2Add_Click(object sender, EventArgs e)
        {
            if (dgvRelayList.Rows[dgvRelayList.CurrentCellAddress.Y].Cells["RelayCode"].Value.ToString().Trim() == null || dgvRelayList.Rows[dgvRelayList.CurrentCellAddress.Y].Cells["RelayCode"].Value.ToString().Trim() == "")
            {
                MessageBox.Show("계전기 상세 정보 저장후 이미지를 등록 할 수 있습니다.", "이미지 등록", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "이미지 파일 선택";
            fDialog.Filter = "JPG Files|*.jpg|JPEG Files|*.jpeg";
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                txtImg1.Text = fDialog.FileName.ToString();

                string fname = txtRelayCode.Text + "_2" + txtImg1.Text.Substring(txtImg1.Text.LastIndexOf("."), txtImg1.Text.Length - txtImg1.Text.LastIndexOf("."));

                FileInfo fileInfo = new FileInfo(txtImg1.Text);
                FileStream fileStream = new FileStream(txtImg1.Text, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[fileInfo.Length];
                fileStream.Read(buffer, 0, Convert.ToInt32(fileInfo.Length));

                string pQuery = "EXEC _SRelayImageSave '" + txtRelayCode.Text + "', '" + fname + "',@Img,'2'";
                SqlCommand _cmd = new SqlCommand(pQuery);
                SqlParameter param = _cmd.Parameters.Add("@Img", SqlDbType.VarBinary);
                param.Value = buffer;
                Dblink.ImageModifyMethod(_cmd);


                DataGridViewCellEventArgs cellIndex = new DataGridViewCellEventArgs(0, rowindex);
                dgvCommonMst_CellClick(null, cellIndex);

                MessageBox.Show("이미지 등록이 완료 되었습니다.", "이미지 등록", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void btnImg3Add_Click(object sender, EventArgs e)
        {
            if (dgvRelayList.Rows[dgvRelayList.CurrentCellAddress.Y].Cells["RelayCode"].Value.ToString().Trim() == null || dgvRelayList.Rows[dgvRelayList.CurrentCellAddress.Y].Cells["RelayCode"].Value.ToString().Trim() == "")
            {
                MessageBox.Show("계전기 상세 정보 저장후 이미지를 등록 할 수 있습니다.", "이미지 등록", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "이미지 파일 선택";
            fDialog.Filter = "JPG Files|*.jpg|JPEG Files|*.jpeg";
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                txtImg1.Text = fDialog.FileName.ToString();

                string fname = txtRelayCode.Text + "_3" + txtImg1.Text.Substring(txtImg1.Text.LastIndexOf("."), txtImg1.Text.Length - txtImg1.Text.LastIndexOf("."));

                FileInfo fileInfo = new FileInfo(txtImg1.Text);
                FileStream fileStream = new FileStream(txtImg1.Text, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[fileInfo.Length];
                fileStream.Read(buffer, 0, Convert.ToInt32(fileInfo.Length));

                string pQuery = "EXEC _SRelayImageSave '" + txtRelayCode.Text + "', '" + fname + "',@Img,'3'";
                SqlCommand _cmd = new SqlCommand(pQuery);
                SqlParameter param = _cmd.Parameters.Add("@Img", SqlDbType.VarBinary);
                param.Value = buffer;
                Dblink.ImageModifyMethod(_cmd);


                DataGridViewCellEventArgs cellIndex = new DataGridViewCellEventArgs(0, rowindex);
                dgvCommonMst_CellClick(null, cellIndex);

                MessageBox.Show("이미지 등록이 완료 되었습니다.", "이미지 등록", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btn_Color_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                //panel1.BackColor = colorDialog1.Color;

                txtA.Text = "100";
                txtR.Text = colorDialog1.Color.R.ToString();
                txtG.Text = colorDialog1.Color.G.ToString();
                txtB.Text = colorDialog1.Color.B.ToString();

                

            }
        }
    }
}
