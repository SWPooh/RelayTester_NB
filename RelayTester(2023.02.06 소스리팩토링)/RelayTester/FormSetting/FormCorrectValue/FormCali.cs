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
    public partial class FormCali : Form
    {
        public DataSet mainDS = new DataSet();
        DbLink Dblink = new DbLink();


        public FormCali()
        {
            InitializeComponent();
        }

        private void FormCali_Load(object sender, EventArgs e)
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


            string pQuery = "EXEC _SCaliQuery '" + groupBox3.Text.Substring(0,1) + "', '1'";

            Dblink.AllSelect(pQuery, mainDS);
            if (mainDS.Tables[0].Rows.Count < 1)
            {
                MessageBox.Show("조회된 자료가 없습니다.", "조회", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            for (int i = 0; i < mainDS.Tables[0].Rows.Count; i++)
            {
                this.txtOperA.Text = mainDS.Tables[0].Rows[i]["OperA"].ToString();
                this.txtOperB.Text = mainDS.Tables[0].Rows[i]["OperB"].ToString();
                this.txtDropA.Text = mainDS.Tables[0].Rows[i]["DropA"].ToString();
                this.txtDropB.Text = mainDS.Tables[0].Rows[i]["DropB"].ToString();
                this.txtCVoltA.Text = mainDS.Tables[0].Rows[i]["CVoltA"].ToString();
                this.txtCCurrA.Text = mainDS.Tables[0].Rows[i]["CCurrA"].ToString();
                this.txtCResiB.Text = mainDS.Tables[0].Rows[i]["CResiB"].ToString();              
            }
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (Control ctl in this.groupBox3.Controls)
            {
                if (ctl.GetType().ToString().Equals("System.Windows.Forms.TextBox"))
                {
                    if (ctl.Text =="")
                    {
                        ctl.Text = "0";
                    }
                }
            }

            string pQuery = string.Format("EXEC _SCaliSave {0},{1},{2},{3},{4},{5},{6},{7}, '1'"
                           , groupBox3.Text.Substring(0, 1)
                           , Convert.ToDouble(txtOperA.Text) 
                           , Convert.ToDouble(txtOperB.Text) 
                           , Convert.ToDouble(txtDropA.Text)
                           , Convert.ToDouble(txtDropB.Text)
                           , Convert.ToDouble(txtCVoltA.Text)
                           , Convert.ToDouble(txtCCurrA.Text)
                           , Convert.ToDouble(txtCResiB.Text)
                           );

            Dblink.ModifyMethod(pQuery);

            this.btnQuery_Click(null, null);

            MessageBox.Show("저장이 완료되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
