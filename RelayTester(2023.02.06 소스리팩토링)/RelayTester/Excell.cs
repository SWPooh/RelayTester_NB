using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace RelayTester
{
    internal class Excell //2023.01.02 Create
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint IpdwProcessId);
        
        public Excell() { }

        private static void copyAlltoCilboard(DataGridView dgv_ret)
        {
            dgv_ret.SelectAll();
            
            DataObject dataObject = dgv_ret.GetClipboardContent();
            if (dataObject != null)
                Clipboard.SetDataObject(dataObject);
        }


        public static void SaveExcel(string str, DataGridView dgv_ret) //엑셀저장 버튼 이벤트
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save as Excel File";
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = str;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                copyAlltoCilboard(dgv_ret);

                Object misValue = System.Reflection.Missing.Value;
                Excel.Application xlexcel = new Excel.Application();

                xlexcel.DisplayAlerts = false;
                Excel.Workbook xlWorkBook = xlexcel.Workbooks.Add(misValue);
                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                try
                {
                    Excel.Range rng = xlWorkSheet.get_Range("D:D").Cells;
                    rng.NumberFormat = "@";
                    Excel.Range CR = (Excel.Range)xlWorkSheet.Cells[1, 1];
                    CR.Select();

                    xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
                    xlWorkBook.SaveAs(sfd.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlexcel.DisplayAlerts = true;
                    xlWorkBook.Close(true, misValue, misValue);

                    uint processId = 0;
                    GetWindowThreadProcessId(new IntPtr(xlexcel.Hwnd), out processId);
                    xlexcel.Quit();

                    if (processId != 0)
                    {
                        System.Diagnostics.Process excelProcess = System.Diagnostics.Process.GetProcessById((int)processId);
                        excelProcess.CloseMainWindow();
                        excelProcess.Refresh();
                        excelProcess.Kill();
                    }
                    MessageBox.Show("엑셀 파일 저장이 완료되었습니다.");

                }
                catch (Exception exc)
                {
                    xlWorkBook.Close();
                    uint processId = 0;
                    GetWindowThreadProcessId(new IntPtr(xlexcel.Hwnd), out processId);
                    xlexcel.Quit();
                    if (processId != 0)
                    {
                        System.Diagnostics.Process excelProcess = System.Diagnostics.Process.GetProcessById((int)processId);
                        excelProcess.CloseMainWindow();
                        excelProcess.Refresh();
                        excelProcess.Kill();
                    }
                }

                //dgv_ret.ClearSelection();
            }

        }
    }
}
