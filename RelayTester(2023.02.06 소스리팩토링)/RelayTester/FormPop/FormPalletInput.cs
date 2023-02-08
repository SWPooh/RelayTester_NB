using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace RelayTester
{
    public partial class FormPalletInput : Form
    {
        DbLink Dblink = new DbLink();
        public string btnflag;
        SoundPlayer _AlertSound = new SoundPlayer(RelayTester.Properties.Resources.Alert);
        public FormPalletInput()
        {
            InitializeComponent();
        }

        private void btnPalletInput_Click(object sender, EventArgs e)
        {
            btnflag = "input";
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnPalletInput_Click(null, null);
            }
        }

        private void btnPalletMove_Click(object sender, EventArgs e)
        {
            btnflag = "move";
            this.Close();
        }

        private void btnPalletDel_Click(object sender, EventArgs e)
        {
            btnflag = "del";
            this.Close();
        }


        private void FormPalletInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            //바코드 길이 체크
            if (btnflag != "del" && txtRelayBarcode.Text.Length != 14)
            {
                _AlertSound.Play();
                MessageBox.Show("바코드 인식에 오류가 있습니다. 다시 인식하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRelayBarcode.SelectAll();
                e.Cancel = true;
                return;
            }

            //바코드 내용 체크
            DataSet tempDS = new DataSet();
            string pQuery = string.Empty;

            pQuery = "select RelayCode from TRelayReg where RelayCode = '" + txtRelayBarcode.Text.Substring(0, 5) + "'";
            tempDS.Clear();
            Dblink.AllSelect(pQuery, tempDS);

            if (btnflag != "del" && tempDS.Tables[0].Rows.Count < 1)
            {
                Console.WriteLine(txtRelayBarcode.Text.Substring(0, 5));
                _AlertSound.Play();
                MessageBox.Show("바코드 인식에 오류가 있습니다. 다시 인식하세요.", "확인", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRelayBarcode.SelectAll();
                return;
            }
        }
    }
}

