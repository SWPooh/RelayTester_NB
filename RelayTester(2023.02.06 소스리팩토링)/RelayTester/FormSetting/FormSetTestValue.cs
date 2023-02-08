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

namespace RelayTester
{
    public partial class FormSetTestValue : Form
    {
        public DataSet mainDS = new DataSet();
        DbLink Dblink = new DbLink();


        public FormSetTestValue()
        {
            InitializeComponent();
        }

        private void FormRefVal_Load(object sender, EventArgs e)
        {
            // TODO: 이 코드는 데이터를 'relayTesterDBDataSet1.TEmpReg' 테이블에 로드합니다. 필요한 경우 이 코드를 이동하거나 제거할 수 있습니다.
            //this.tEmpRegTableAdapter.Fill(this.relayTesterDBDataSet1.TEmpReg);

            //Dblink.ConnCoForm();

            //폼로드할때 자동조회
            this.btnQuery_Click(null, null);
           
        }



        private void btnQuery_Click(object sender, EventArgs e)
        {
            foreach(Control ctl in this.Controls)
            {
                if(ctl.GetType().ToString().Equals("System.Windows.Forms.TextBox"))
                {
                    ctl.Text = "";
                }
            }


            string pQuery = "EXEC _SRefValQuery";

            Dblink.AllSelect(pQuery, mainDS);
            if (mainDS.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("조회된 자료가 없습니다.", "조회", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            for (int i = 0; i < mainDS.Tables[0].Rows.Count; i++)
            {
                ////
                //동작시간-무극
                this.txtTNRNMin.Text = mainDS.Tables[0].Rows[i]["TNRNMin"].ToString();
                this.txtTNRNMax.Text = mainDS.Tables[0].Rows[i]["TNRNMax"].ToString();
                this.txtTNNRMin.Text = mainDS.Tables[0].Rows[i]["TNNRMin"].ToString();
                this.txtTNNRMax.Text = mainDS.Tables[0].Rows[i]["TNNRMax"].ToString();

                //동작시간-유극
                this.txtTPRNMin.Text = mainDS.Tables[0].Rows[i]["TPRNMin"].ToString();
                this.txtTPRNMax.Text = mainDS.Tables[0].Rows[i]["TPRNMax"].ToString();
                this.txtTPNRMin.Text = mainDS.Tables[0].Rows[i]["TPNRMin"].ToString();
                this.txtTPNRMax.Text = mainDS.Tables[0].Rows[i]["TPNRMax"].ToString();

                //동작시간-유극 저전류
                this.txtTPRNLMin.Text = mainDS.Tables[0].Rows[i]["TPRNLMin"].ToString();
                this.txtTPRNLMax.Text = mainDS.Tables[0].Rows[i]["TPRNLMax"].ToString();
                this.txtTPNRLMin.Text = mainDS.Tables[0].Rows[i]["TPNRLMin"].ToString();
                this.txtTPNRLMax.Text = mainDS.Tables[0].Rows[i]["TPNRLMax"].ToString();

                //동작시간-자기유지
                this.txtTSRNMin.Text = mainDS.Tables[0].Rows[i]["TSRNMin"].ToString();
                this.txtTSRNMax.Text = mainDS.Tables[0].Rows[i]["TSRNMax"].ToString();
                this.txtTSNRMin.Text = mainDS.Tables[0].Rows[i]["TSNRMin"].ToString();
                this.txtTSNRMax.Text = mainDS.Tables[0].Rows[i]["TSNRMax"].ToString();

                ////
                //코일전류-무극
                this.txtCNOMin.Text = mainDS.Tables[0].Rows[i]["CNOMin"].ToString();
                this.txtCNOMax.Text = mainDS.Tables[0].Rows[i]["CNOMax"].ToString();
                this.txtCNDMin.Text = mainDS.Tables[0].Rows[i]["CNDMin"].ToString();
                this.txtCNDMax.Text = mainDS.Tables[0].Rows[i]["CNDMax"].ToString();

                //코일전류-유극
                this.txtCPOMin.Text = mainDS.Tables[0].Rows[i]["CPOMin"].ToString();
                this.txtCPOMax.Text = mainDS.Tables[0].Rows[i]["CPOMax"].ToString();
                this.txtCPDMin.Text = mainDS.Tables[0].Rows[i]["CPDMin"].ToString();
                this.txtCPDMax.Text = mainDS.Tables[0].Rows[i]["CPDMax"].ToString();

                //코일전류-유극 저전류
                this.txtCPOLMin.Text = mainDS.Tables[0].Rows[i]["CPOLMin"].ToString();
                this.txtCPOLMax.Text = mainDS.Tables[0].Rows[i]["CPOLMax"].ToString();
                this.txtCPDLMin.Text = mainDS.Tables[0].Rows[i]["CPDLMin"].ToString();
                this.txtCPDLMax.Text = mainDS.Tables[0].Rows[i]["CPDLMax"].ToString();

                //코일전류-자기유지
                this.txtCSOMin.Text = mainDS.Tables[0].Rows[i]["CSOMin"].ToString();
                this.txtCSOMax.Text = mainDS.Tables[0].Rows[i]["CSOMax"].ToString();
                this.txtCSDMin.Text = mainDS.Tables[0].Rows[i]["CSDMin"].ToString();
                this.txtCSDMax.Text = mainDS.Tables[0].Rows[i]["CSDMax"].ToString();

                ////
                //코일저항-무극
                this.txtRNMin.Text = mainDS.Tables[0].Rows[i]["RNMin"].ToString();
                this.txtRNMax.Text = mainDS.Tables[0].Rows[i]["RNMax"].ToString();

                //코일저항-유극
                this.txtRPMin.Text = mainDS.Tables[0].Rows[i]["RPMin"].ToString();
                this.txtRPMax.Text = mainDS.Tables[0].Rows[i]["RPMax"].ToString();

                //코일저항-유극 저전류
                this.txtRPLMin.Text = mainDS.Tables[0].Rows[i]["RPLMin"].ToString();
                this.txtRPLMax.Text = mainDS.Tables[0].Rows[i]["RPLMax"].ToString();

                //코일저항-자기유지
                this.txtRSMin.Text = mainDS.Tables[0].Rows[i]["RSMin"].ToString();
                this.txtRSMax.Text = mainDS.Tables[0].Rows[i]["RSMax"].ToString();

                ////
                //접촉저항-무극
                this.txtCRNMin.Text = mainDS.Tables[0].Rows[i]["CRNMin"].ToString();
                this.txtCRNMax.Text = mainDS.Tables[0].Rows[i]["CRNMax"].ToString();

                //접촉저항-유극
                this.txtCRPMin.Text = mainDS.Tables[0].Rows[i]["CRPMin"].ToString();
                this.txtCRPMax.Text = mainDS.Tables[0].Rows[i]["CRPMax"].ToString();

                //접촉저항-유극 저전류
                this.txtCRPLMin.Text = mainDS.Tables[0].Rows[i]["CRPLMin"].ToString();
                this.txtCRPLMax.Text = mainDS.Tables[0].Rows[i]["CRPLMax"].ToString();

                //접촉저항-자기유지
                this.txtCRSMin.Text = mainDS.Tables[0].Rows[i]["CRSMin"].ToString();
                this.txtCRSMax.Text = mainDS.Tables[0].Rows[i]["CRSMax"].ToString();
            }
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            string pQuery1 = string.Format("delete from _TRefVal");
            Dblink.ModifyMethod(pQuery1);


            string pQuery2 = string.Format("EXEC _SRefValSAVE {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40},{41},{42},{43},{44},{45},{46},{47}"
                           , this.txtTNRNMin.Text, this.txtTNRNMax.Text, this.txtTNNRMin.Text, this.txtTNNRMax.Text
                           , this.txtTPRNMin.Text, this.txtTPRNMax.Text, this.txtTPNRMin.Text, this.txtTPNRMax.Text
                           , this.txtTSRNMin.Text, this.txtTSRNMax.Text, this.txtTSNRMin.Text, this.txtTSNRMax.Text
                           , this.txtCNOMin.Text, this.txtCNOMax.Text, this.txtCNDMin.Text, this.txtCNDMax.Text
                           , this.txtCPOMin.Text, this.txtCPOMax.Text, this.txtCPDMin.Text, this.txtCPDMax.Text
                           , this.txtCSOMin.Text, this.txtCSOMax.Text, this.txtCSDMin.Text, this.txtCSDMax.Text
                           , this.txtRNMin.Text, this.txtRNMax.Text
                           , this.txtRPMin.Text, this.txtRPMax.Text
                           , this.txtRSMin.Text, this.txtRSMax.Text
                           , this.txtCRNMin.Text, this.txtCRNMax.Text
                           , this.txtCRPMin.Text, this.txtCRPMax.Text
                           , this.txtCRSMin.Text, this.txtCRSMax.Text
                           , this.txtTPRNLMin.Text, this.txtTPRNLMax.Text, this.txtTPNRLMin.Text, this.txtTPNRLMax.Text
                           , this.txtCPOLMin.Text, this.txtCPOLMax.Text, this.txtCPDLMin.Text, this.txtCPDLMax.Text
                           , this.txtRPLMin.Text, this.txtRPLMax.Text
                           , this.txtCRPLMin.Text, this.txtCRPLMax.Text
                           );

            Dblink.ModifyMethod(pQuery2);

            this.btnQuery_Click(null, null);

            MessageBox.Show("저장이 완료되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
