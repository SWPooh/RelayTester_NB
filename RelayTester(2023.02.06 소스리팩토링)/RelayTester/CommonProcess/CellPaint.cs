using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RelayTester.CommonProcess
{
    public static class CellPaint
    {
        static bool IsTheSameCellValue(int column, int row, DataGridView gridView)
        {
            DataGridViewCell cell1 = gridView[column, row];
            DataGridViewCell cell2 = gridView[column, row - 1];

            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }

            return cell1.Value.ToString() == cell2.Value.ToString();
        } //셀 합치기 관련 코드

        public static void CellPainting(DataGridViewCellPaintingEventArgs e, DataGridView gridView)
        {
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;

            if (e.RowIndex < 1 || e.ColumnIndex < 0)
            {
                return;
            }

            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex, gridView))
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Top = gridView.AdvancedCellBorderStyle.Top;
            }
        } //같은 셀 내용 사이의 테두리 지우는 함수

        public static void CellFormating(DataGridViewCellFormattingEventArgs e, DataGridView gridView)
        {
            if (e.RowIndex == 0)
                return;

            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex, gridView))
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        } //같은 셀 내용 지우는 함수
    }
}
