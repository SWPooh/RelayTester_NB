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
    public partial class FormSetContectCorrectValue : Form
    {
        public DataSet mainDS = new DataSet();
        DbLink Dblink = new DbLink();


        public FormSetContectCorrectValue()
        {
            InitializeComponent();
        }

        private void FormContectCali_Load(object sender, EventArgs e)
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


            string pQuery = "EXEC _SCaliQuery '', '2'";

            Dblink.AllSelect(pQuery, mainDS);
            if (mainDS.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("조회된 자료가 없습니다.", "조회", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            for (int i = 0; i < mainDS.Tables[0].Rows.Count; i++)
            {
                this.txtN1VoltA.Text = mainDS.Tables[0].Rows[i]["N1VoltA"].ToString();
                this.txtN2VoltA.Text = mainDS.Tables[0].Rows[i]["N2VoltA"].ToString();
                this.txtN3VoltA.Text = mainDS.Tables[0].Rows[i]["N3VoltA"].ToString();
                this.txtN4VoltA.Text = mainDS.Tables[0].Rows[i]["N4VoltA"].ToString();
                this.txtN5VoltA.Text = mainDS.Tables[0].Rows[i]["N5VoltA"].ToString();
                this.txtN6VoltA.Text = mainDS.Tables[0].Rows[i]["N6VoltA"].ToString();
                this.txtN7VoltA.Text = mainDS.Tables[0].Rows[i]["N7VoltA"].ToString();
                this.txtN8VoltA.Text = mainDS.Tables[0].Rows[i]["N8VoltA"].ToString();
                this.txtN9VoltA.Text = mainDS.Tables[0].Rows[i]["N9VoltA"].ToString();
                this.txtN10VoltA.Text = mainDS.Tables[0].Rows[i]["N10VoltA"].ToString();
                this.txtN1VoltB.Text = mainDS.Tables[0].Rows[i]["N1VoltB"].ToString();
                this.txtN2VoltB.Text = mainDS.Tables[0].Rows[i]["N2VoltB"].ToString();
                this.txtN3VoltB.Text = mainDS.Tables[0].Rows[i]["N3VoltB"].ToString();
                this.txtN4VoltB.Text = mainDS.Tables[0].Rows[i]["N4VoltB"].ToString();
                this.txtN5VoltB.Text = mainDS.Tables[0].Rows[i]["N5VoltB"].ToString();
                this.txtN6VoltB.Text = mainDS.Tables[0].Rows[i]["N6VoltB"].ToString();
                this.txtN7VoltB.Text = mainDS.Tables[0].Rows[i]["N7VoltB"].ToString();
                this.txtN8VoltB.Text = mainDS.Tables[0].Rows[i]["N8VoltB"].ToString();
                this.txtN9VoltB.Text = mainDS.Tables[0].Rows[i]["N9VoltB"].ToString();
                this.txtN10VoltB.Text = mainDS.Tables[0].Rows[i]["N10VoltB"].ToString();

                this.txtR1VoltA.Text = mainDS.Tables[0].Rows[i]["R1VoltA"].ToString();
                this.txtR2VoltA.Text = mainDS.Tables[0].Rows[i]["R2VoltA"].ToString();
                this.txtR3VoltA.Text = mainDS.Tables[0].Rows[i]["R3VoltA"].ToString();
                this.txtR4VoltA.Text = mainDS.Tables[0].Rows[i]["R4VoltA"].ToString();
                this.txtR5VoltA.Text = mainDS.Tables[0].Rows[i]["R5VoltA"].ToString();
                this.txtR6VoltA.Text = mainDS.Tables[0].Rows[i]["R6VoltA"].ToString();
                this.txtR7VoltA.Text = mainDS.Tables[0].Rows[i]["R7VoltA"].ToString();
                this.txtR8VoltA.Text = mainDS.Tables[0].Rows[i]["R8VoltA"].ToString();
                this.txtR9VoltA.Text = mainDS.Tables[0].Rows[i]["R9VoltA"].ToString();
                this.txtR10VoltA.Text = mainDS.Tables[0].Rows[i]["R10VoltA"].ToString();
                this.txtR1VoltB.Text = mainDS.Tables[0].Rows[i]["R1VoltB"].ToString();
                this.txtR2VoltB.Text = mainDS.Tables[0].Rows[i]["R2VoltB"].ToString();
                this.txtR3VoltB.Text = mainDS.Tables[0].Rows[i]["R3VoltB"].ToString();
                this.txtR4VoltB.Text = mainDS.Tables[0].Rows[i]["R4VoltB"].ToString();
                this.txtR5VoltB.Text = mainDS.Tables[0].Rows[i]["R5VoltB"].ToString();
                this.txtR6VoltB.Text = mainDS.Tables[0].Rows[i]["R6VoltB"].ToString();
                this.txtR7VoltB.Text = mainDS.Tables[0].Rows[i]["R7VoltB"].ToString();
                this.txtR8VoltB.Text = mainDS.Tables[0].Rows[i]["R8VoltB"].ToString();
                this.txtR9VoltB.Text = mainDS.Tables[0].Rows[i]["R9VoltB"].ToString();
                this.txtR10VoltB.Text = mainDS.Tables[0].Rows[i]["R10VoltB"].ToString();


               
            }
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            string pQuery = string.Format("EXEC _SCaliSave2 {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40}, '1'"
                           , "0"
                           ,Convert.ToDouble(txtN1VoltA.Text)
                           ,Convert.ToDouble(txtN1VoltB.Text)
                           ,Convert.ToDouble(txtN2VoltA.Text)
                           ,Convert.ToDouble(txtN2VoltB.Text)
                           ,Convert.ToDouble(txtN3VoltA.Text)
                           ,Convert.ToDouble(txtN3VoltB.Text)
                           ,Convert.ToDouble(txtN4VoltA.Text)
                           ,Convert.ToDouble(txtN4VoltB.Text)
                           ,Convert.ToDouble(txtN5VoltA.Text)
                           ,Convert.ToDouble(txtN5VoltB.Text)
                           ,Convert.ToDouble(txtN6VoltA.Text)
                           ,Convert.ToDouble(txtN6VoltB.Text)
                           ,Convert.ToDouble(txtN7VoltA.Text)
                           ,Convert.ToDouble(txtN7VoltB.Text)
                           ,Convert.ToDouble(txtN8VoltA.Text)
                           ,Convert.ToDouble(txtN8VoltB.Text)
                           ,Convert.ToDouble(txtN9VoltA.Text)
                           ,Convert.ToDouble(txtN9VoltB.Text)
                           ,Convert.ToDouble(txtN10VoltA.Text)
                           ,Convert.ToDouble(txtN10VoltB.Text)
                           ,Convert.ToDouble(txtR1VoltA.Text)
                           ,Convert.ToDouble(txtR1VoltB.Text)
                           ,Convert.ToDouble(txtR2VoltA.Text)
                           ,Convert.ToDouble(txtR2VoltB.Text)
                           ,Convert.ToDouble(txtR3VoltA.Text)
                           ,Convert.ToDouble(txtR3VoltB.Text)
                           ,Convert.ToDouble(txtR4VoltA.Text)
                           ,Convert.ToDouble(txtR4VoltB.Text)
                           ,Convert.ToDouble(txtR5VoltA.Text)
                           ,Convert.ToDouble(txtR5VoltB.Text)
                           ,Convert.ToDouble(txtR6VoltA.Text)
                           ,Convert.ToDouble(txtR6VoltB.Text)
                           ,Convert.ToDouble(txtR7VoltA.Text)
                           ,Convert.ToDouble(txtR7VoltB.Text)
                           ,Convert.ToDouble(txtR8VoltA.Text)
                           ,Convert.ToDouble(txtR8VoltB.Text)
                           ,Convert.ToDouble(txtR9VoltA.Text)
                           ,Convert.ToDouble(txtR9VoltB.Text)
                           ,Convert.ToDouble(txtR10VoltA.Text)
                           ,Convert.ToDouble(txtR10VoltB.Text)
                           );

            Dblink.ModifyMethod(pQuery);

            this.btnQuery_Click(null, null);

            MessageBox.Show("저장이 완료되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
