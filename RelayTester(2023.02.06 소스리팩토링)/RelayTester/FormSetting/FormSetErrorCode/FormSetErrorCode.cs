using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelayTester
{
    public partial class FormSetErrorCode : Form
    {
        public FormSetErrorCodeWork codeWork;

        public FormSetErrorCode()
        {
            InitializeComponent();
            codeWork = new FormSetErrorCodeWork(this);

        }

        private void FormErrorCode_Load(object sender, EventArgs e)
        {
            codeWork.QueryClick();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            codeWork.QueryClick();
        } //조회버튼 클릭

        private void btnNew_Click(object sender, EventArgs e)
        {
            codeWork.NewClick();
            
        } //추가버튼 클릭

        private void btnDelete_Click(object sender, EventArgs e)
        {
            codeWork.DeleteClick();
        } //삭제버튼 클릭
        
    }
}
