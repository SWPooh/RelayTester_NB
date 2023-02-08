using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelayTester
{
    public partial class FormErrorCodePopCon : Form
    {
        ErrorCodePop pop;
        FormContectResi contResi;
        //public Form2Relay FORM_Relay2 = null;

        public FormErrorCodePopCon(FormContectResi form)
        {
            InitializeComponent();
            contResi = form;
        }
        
        public FormErrorCodePopCon(ErrorCodePop form)
        {
            pop = form;
        }
        

        private void FormErrorCodeCon_Load(object sender, EventArgs e)
        {
            //contResi = new FormContectResi();
            string testNum = contResi.txtTestNum.Text;
            contResi.pop.LoadErroCodeContectResis();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool flag = true;
            try
            {
                for (int i = 0; i < dgv_error.Rows.Count; i++)
                {
                    if (dgv_error.Rows[i].Cells[2].Value == null || dgv_error.Rows[i].Cells[2].Value.ToString().Length < 1)
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    contResi.pop.UpdateActCodeContectResis();
                }
                else
                    MessageBox.Show("조치코드가 입력되지않았습니다. \n 다시 확인해주세요!!");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("저장에 실패했습니다. \n 다시 시도해주세요! \n 같은 문제가 반복되면 문의바랍니다." + ex.Message);
            }
            finally
            {
                if(flag)
                    Close();
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
        private void dgv_error_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) //Row 추가시 에러 예외처리
        {

        }
        private void dgv_error_DataError(object sender, DataGridViewDataErrorEventArgs e) //Row 추가시 에러 예외처리
        {

        }
        private void dgv_error_CellPainting(object sender, DataGridViewCellPaintingEventArgs e) // 동일한 내용시 표 합치기
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

        private void dgv_error_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) // 동일한 내용시 표 합치기
        {
            if (e.RowIndex == 0)
                return;

            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }

        bool IsTheSameCellValue(int column, int row) // 동일한 내용시 표 합치기
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
