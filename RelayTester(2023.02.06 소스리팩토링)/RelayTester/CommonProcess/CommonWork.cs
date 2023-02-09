using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelayTester
{
    public class CommonWork
    {
        public static void TextboxClear(GroupBox groupBox)
        {
            foreach (Control ctl in groupBox.Controls)
            {
                if (ctl.GetType().ToString().Equals("System.Windows.Forms.TextBox") && ctl.Name == "txtRelayTypeCode")
                {
                    ctl.Text = "";
                }
            }
        } //그룹박스에 있는 텍스트 박스 초기화


    }
}
