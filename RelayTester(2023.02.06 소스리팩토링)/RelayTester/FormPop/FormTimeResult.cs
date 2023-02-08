using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RelayTester
{
    public partial class FormTimeResult : Form
    {

        public FormTimeResult()
        {
            InitializeComponent();
        }

        private void FormTimeResult_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Control ctl in this.Controls)
            {
                if (ctl.GetType().ToString().Equals("System.Windows.Forms.Label"))
                {
                    ctl.Visible = true;
                    ctl.Text = "";
                    ctl.BackColor = SystemColors.Control;
                    if (ctl.Name.Substring(0, 5) == "label")    //번호 표시용 라벨이면
                    {
                        ctl.Text = ctl.Name.Replace("label", "");
                    }
                }
                if (ctl.GetType().ToString().Equals("System.Windows.Forms.PictureBox"))
                {
                    ctl.BackgroundImage = RelayTester.Properties.Resources.grey;
                }
            }
        }

   
    }
}

