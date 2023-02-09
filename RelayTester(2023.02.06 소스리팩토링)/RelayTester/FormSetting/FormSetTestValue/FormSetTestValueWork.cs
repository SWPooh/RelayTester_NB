using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelayTester
{
    public class FormSetTestValueWork
    {
        public FormSetTestValue Form_TestValue;

        public DataSet mainDS = new DataSet();
        DbLink Dblink = new DbLink();

        public FormSetTestValueWork(FormSetTestValue form) 
        {
            Form_TestValue= form;
        }

        public void QueryClick()
        {
            foreach (Control ctl in Form_TestValue.Controls)
            {
                if (ctl.GetType().ToString().Equals("System.Windows.Forms.TextBox"))
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
                Form_TestValue.txtTNRNMin.Text = mainDS.Tables[0].Rows[i]["TNRNMin"].ToString();
                Form_TestValue.txtTNRNMax.Text = mainDS.Tables[0].Rows[i]["TNRNMax"].ToString();
                Form_TestValue.txtTNNRMin.Text = mainDS.Tables[0].Rows[i]["TNNRMin"].ToString();
                Form_TestValue.txtTNNRMax.Text = mainDS.Tables[0].Rows[i]["TNNRMax"].ToString();

                //동작시간-유극
                Form_TestValue.txtTPRNMin.Text = mainDS.Tables[0].Rows[i]["TPRNMin"].ToString();
                Form_TestValue.txtTPRNMax.Text = mainDS.Tables[0].Rows[i]["TPRNMax"].ToString();
                Form_TestValue.txtTPNRMin.Text = mainDS.Tables[0].Rows[i]["TPNRMin"].ToString();
                Form_TestValue.txtTPNRMax.Text = mainDS.Tables[0].Rows[i]["TPNRMax"].ToString();

                //동작시간-유극 저전류
                Form_TestValue.txtTPRNLMin.Text = mainDS.Tables[0].Rows[i]["TPRNLMin"].ToString();
                Form_TestValue.txtTPRNLMax.Text = mainDS.Tables[0].Rows[i]["TPRNLMax"].ToString();
                Form_TestValue.txtTPNRLMin.Text = mainDS.Tables[0].Rows[i]["TPNRLMin"].ToString();
                Form_TestValue.txtTPNRLMax.Text = mainDS.Tables[0].Rows[i]["TPNRLMax"].ToString();

                //동작시간-자기유지
                Form_TestValue.txtTSRNMin.Text = mainDS.Tables[0].Rows[i]["TSRNMin"].ToString();
                Form_TestValue.txtTSRNMax.Text = mainDS.Tables[0].Rows[i]["TSRNMax"].ToString();
                Form_TestValue.txtTSNRMin.Text = mainDS.Tables[0].Rows[i]["TSNRMin"].ToString();
                Form_TestValue.txtTSNRMax.Text = mainDS.Tables[0].Rows[i]["TSNRMax"].ToString();

                ////
                //코일전류-무극
                Form_TestValue.txtCNOMin.Text = mainDS.Tables[0].Rows[i]["CNOMin"].ToString();
                Form_TestValue.txtCNOMax.Text = mainDS.Tables[0].Rows[i]["CNOMax"].ToString();
                Form_TestValue.txtCNDMin.Text = mainDS.Tables[0].Rows[i]["CNDMin"].ToString();
                Form_TestValue.txtCNDMax.Text = mainDS.Tables[0].Rows[i]["CNDMax"].ToString();

                //코일전류-유극
                Form_TestValue.txtCPOMin.Text = mainDS.Tables[0].Rows[i]["CPOMin"].ToString();
                Form_TestValue.txtCPOMax.Text = mainDS.Tables[0].Rows[i]["CPOMax"].ToString();
                Form_TestValue.txtCPDMin.Text = mainDS.Tables[0].Rows[i]["CPDMin"].ToString();
                Form_TestValue.txtCPDMax.Text = mainDS.Tables[0].Rows[i]["CPDMax"].ToString();

                //코일전류-유극 저전류
                Form_TestValue.txtCPOLMin.Text = mainDS.Tables[0].Rows[i]["CPOLMin"].ToString();
                Form_TestValue.txtCPOLMax.Text = mainDS.Tables[0].Rows[i]["CPOLMax"].ToString();
                Form_TestValue.txtCPDLMin.Text = mainDS.Tables[0].Rows[i]["CPDLMin"].ToString();
                Form_TestValue.txtCPDLMax.Text = mainDS.Tables[0].Rows[i]["CPDLMax"].ToString();

                //코일전류-자기유지
                Form_TestValue.txtCSOMin.Text = mainDS.Tables[0].Rows[i]["CSOMin"].ToString();
                Form_TestValue.txtCSOMax.Text = mainDS.Tables[0].Rows[i]["CSOMax"].ToString();
                Form_TestValue.txtCSDMin.Text = mainDS.Tables[0].Rows[i]["CSDMin"].ToString();
                Form_TestValue.txtCSDMax.Text = mainDS.Tables[0].Rows[i]["CSDMax"].ToString();

                ////
                //코일저항-무극
                Form_TestValue.txtRNMin.Text = mainDS.Tables[0].Rows[i]["RNMin"].ToString();
                Form_TestValue.txtRNMax.Text = mainDS.Tables[0].Rows[i]["RNMax"].ToString();

                //코일저항-유극
                Form_TestValue.txtRPMin.Text = mainDS.Tables[0].Rows[i]["RPMin"].ToString();
                Form_TestValue.txtRPMax.Text = mainDS.Tables[0].Rows[i]["RPMax"].ToString();

                //코일저항-유극 저전류
                Form_TestValue.txtRPLMin.Text = mainDS.Tables[0].Rows[i]["RPLMin"].ToString();
                Form_TestValue.txtRPLMax.Text = mainDS.Tables[0].Rows[i]["RPLMax"].ToString();

                //코일저항-자기유지
                Form_TestValue.txtRSMin.Text = mainDS.Tables[0].Rows[i]["RSMin"].ToString();
                Form_TestValue.txtRSMax.Text = mainDS.Tables[0].Rows[i]["RSMax"].ToString();

                ////
                //접촉저항-무극
                Form_TestValue.txtCRNMin.Text = mainDS.Tables[0].Rows[i]["CRNMin"].ToString();
                Form_TestValue.txtCRNMax.Text = mainDS.Tables[0].Rows[i]["CRNMax"].ToString();

                //접촉저항-유극
                Form_TestValue.txtCRPMin.Text = mainDS.Tables[0].Rows[i]["CRPMin"].ToString();
                Form_TestValue.txtCRPMax.Text = mainDS.Tables[0].Rows[i]["CRPMax"].ToString();

                //접촉저항-유극 저전류
                Form_TestValue.txtCRPLMin.Text = mainDS.Tables[0].Rows[i]["CRPLMin"].ToString();
                Form_TestValue.txtCRPLMax.Text = mainDS.Tables[0].Rows[i]["CRPLMax"].ToString();

                //접촉저항-자기유지
                Form_TestValue.txtCRSMin.Text = mainDS.Tables[0].Rows[i]["CRSMin"].ToString();
                Form_TestValue.txtCRSMax.Text = mainDS.Tables[0].Rows[i]["CRSMax"].ToString();
            }
        }  //조회버튼 클릭 이벤트

        public void SaveClick()
        {
            string pQuery1 = string.Format("delete from _TRefVal");
            Dblink.ModifyMethod(pQuery1);


            string pQuery2 = string.Format("EXEC _SRefValSAVE {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40},{41},{42},{43},{44},{45},{46},{47}"
                           , Form_TestValue.txtTNRNMin.Text, Form_TestValue.txtTNRNMax.Text, Form_TestValue.txtTNNRMin.Text, Form_TestValue.txtTNNRMax.Text
                           , Form_TestValue.txtTPRNMin.Text, Form_TestValue.txtTPRNMax.Text, Form_TestValue.txtTPNRMin.Text, Form_TestValue.txtTPNRMax.Text
                           , Form_TestValue.txtTSRNMin.Text, Form_TestValue.txtTSRNMax.Text, Form_TestValue.txtTSNRMin.Text, Form_TestValue.txtTSNRMax.Text
                           , Form_TestValue.txtCNOMin.Text, Form_TestValue.txtCNOMax.Text, Form_TestValue.txtCNDMin.Text, Form_TestValue.txtCNDMax.Text
                           , Form_TestValue.txtCPOMin.Text, Form_TestValue.txtCPOMax.Text, Form_TestValue.txtCPDMin.Text, Form_TestValue.txtCPDMax.Text
                           , Form_TestValue.txtCSOMin.Text, Form_TestValue.txtCSOMax.Text, Form_TestValue.txtCSDMin.Text, Form_TestValue.txtCSDMax.Text
                           , Form_TestValue.txtRNMin.Text, Form_TestValue.txtRNMax.Text
                           , Form_TestValue.txtRPMin.Text, Form_TestValue.txtRPMax.Text
                           , Form_TestValue.txtRSMin.Text, Form_TestValue.txtRSMax.Text
                           , Form_TestValue.txtCRNMin.Text, Form_TestValue.txtCRNMax.Text
                           , Form_TestValue.txtCRPMin.Text, Form_TestValue.txtCRPMax.Text
                           , Form_TestValue.txtCRSMin.Text, Form_TestValue.txtCRSMax.Text
                           , Form_TestValue.txtTPRNLMin.Text, Form_TestValue.txtTPRNLMax.Text, Form_TestValue.txtTPNRLMin.Text, Form_TestValue.txtTPNRLMax.Text
                           , Form_TestValue.txtCPOLMin.Text, Form_TestValue.txtCPOLMax.Text, Form_TestValue.txtCPDLMin.Text, Form_TestValue.txtCPDLMax.Text
                           , Form_TestValue.txtRPLMin.Text, Form_TestValue.txtRPLMax.Text
                           , Form_TestValue.txtCRPLMin.Text, Form_TestValue.txtCRPLMax.Text
                           );

            Dblink.ModifyMethod(pQuery2);

            QueryClick();

            MessageBox.Show("저장이 완료되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
        } //저장버튼 클릭 이벤트
    }
}
