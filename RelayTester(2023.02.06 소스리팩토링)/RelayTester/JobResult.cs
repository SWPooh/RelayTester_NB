using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelayTester
{
    public class JobResult //2022.12.27 Create
    {

        DbLink Dblink = new DbLink();
        DataSet ds = new DataSet();
        FormRelay formRelay;
        string date = string.Empty;
        int testNum = 0;
        public JobResult(FormRelay form) 
        {
            formRelay = form;
        }

        public void LoadTestNum(string testType)
        {
            date = DateTime.Now.ToString("yyyyMMdd");

            string TestNumLoadQry = string.Format("EXEC _FJobTestNum '{0}', '{1}' ", testType, date);
            Dblink.AllSelect(TestNumLoadQry, ds);

            testNum = int.Parse(ds.Tables[0].Rows[0][0].ToString());

            formRelay.txtTestNum.Text = testNum.ToString();
        }


        public void IncreaseTestNum()
        {
            try
            {
                testNum = int.Parse(formRelay.txtTestNum.Text);

                testNum++;

                formRelay.txtTestNum.Text = testNum.ToString();
            }
            catch(Exception ex)
            {

            }
            
        }
    }
}
