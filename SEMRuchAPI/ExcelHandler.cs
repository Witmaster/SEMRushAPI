using System;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace SEMRuchAPI
{
    class ExcelHandler
    {
        public bool isOK = false;
        private static Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        private static Workbook xlWorkBook;
        private static Worksheet currentSheet;
        private static int currentSheetNumber = 0 ;

        public ExcelHandler(string filename, int tabcount)
        {
            if (xlApp == null)
            {
                isOK = false;
                MessageBox.Show("Microsoft Excel не установлен или установлен некорректно");
                //  System.Windows.Forms.Application.Exit()               
            }
            else
            {
                isOK = true;
                xlApp.SheetsInNewWorkbook = tabcount;
                xlWorkBook = xlApp.Workbooks.Add(Type.Missing);
                xlWorkBook.SaveAs(filename);
            }
        }

        public void WriteToTab(string data, string name)
        {
            currentSheetNumber++;
            currentSheet = xlWorkBook.Worksheets.get_Item(currentSheetNumber);
            currentSheet.Name = name;
            var dataarray = data.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            string[] splitline = new string[6];
            for (int i = 0; i < dataarray.Length; i++)
            {
                splitline = dataarray[i].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < splitline.Length; j++)
                {
                    currentSheet.Cells[i+1, j+1] = splitline[j];
                }
                if (i==0)
                {
                    currentSheet.Cells[i+1, 5] = "Advisability";
                    currentSheet.Cells[i+1, 6] = "Root";
                }
                else
                {
                    currentSheet.Cells[i+1, 5] = "формула";
                    currentSheet.Cells[i+1, 6] = currentSheet.Name;
                }
            }
            xlWorkBook.Save();
        }
        public void Dispose()
        {
            xlWorkBook.Save();
            xlWorkBook.Close(true,Type.Missing,Type.Missing);
            xlApp.Quit();
            ReleaseObject(currentSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);

        }
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
