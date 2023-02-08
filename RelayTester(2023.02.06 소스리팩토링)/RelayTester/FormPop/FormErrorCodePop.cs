using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelayTester
{
    public partial class FormErrorCodePop : Form
    {
        ErrorCodePop pop;
        public FormRelay FORM_Relay = null;
        public FormErrorCodePop(FormRelay form)
        {
            InitializeComponent();
            FORM_Relay = form;
            pop = new ErrorCodePop(this);
        }

        private void FormErrorCodePop_Load(object sender, EventArgs e)
        {
            string testNum = FORM_Relay.txtTestNum.Text;
            string testerNum = FORM_Relay.txtRelayNum.Text;
            pop.LoadDataError(testerNum, testNum);   
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool flag = true;
            try
            {
                for(int i = 0; i<dgv_error.Rows.Count; i++)
                {
                    if (dgv_error.Rows[i].Cells[3].Value == null || dgv_error.Rows[i].Cells[3].Value.ToString().Length < 1)
                    {
                        flag = false;
                    }
                }

                if(flag)
                {
                    pop.UpdateActCode();
                }
                else
                    MessageBox.Show("조치코드가 입력되지않았습니다. \n 다시 확인해주세요!!");

            }
            catch(Exception ex)
            {
                MessageBox.Show("저장에 실패했습니다. \n 다시 시도해주세요! \n 같은 문제가 반복되면 문의바랍니다." + ex.Message);
            }
            finally
            {
                if (flag)
                    this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("정말 취소하시겠습니까?", "YesOrNo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Close();
            }

            else
            {

            }

        }

        private void dgv_error_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        private void dgv_error_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dgv_error_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;

            if (e.RowIndex < 1 || e.ColumnIndex < 0)
            {
                return;
            }

            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Top = dgv_error.AdvancedCellBorderStyle.Top;
            }
        }

        private void dgv_error_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0)
                return;

            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }
        
        bool IsTheSameCellValue(int column, int row)
        {
            DataGridViewCell cell1 = dgv_error[column, row];
            DataGridViewCell cell2 = dgv_error[column, row - 1];

            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }

            return cell1.Value.ToString() == cell2.Value.ToString();
        }
    }

}
