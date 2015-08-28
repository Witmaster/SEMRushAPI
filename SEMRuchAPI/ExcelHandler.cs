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

        public void WriteToTab(string data, string name, int sheet, int offset)
        {
            offset++;
            currentSheet = xlWorkBook.Worksheets.get_Item(sheet+1);
            currentSheet.Name = name;
            currentSheet.Cells[offset, 1] = "Keyword";
            currentSheet.Cells[offset, 2] = "Search volume";
            currentSheet.Cells[offset, 3] = "Competition";
            currentSheet.Cells[offset, 4] = "CPC";
            currentSheet.Cells[offset, 5] = "Advisability";
            currentSheet.Cells[offset, 6] = "Root";
            var dataarray = data.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            string[] splitline = new string[6];
            for (int i = 1; i < dataarray.Length; i++)
            {
                splitline = dataarray[i].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < splitline.Length; j++)
                {
                    currentSheet.Cells[i + offset, j + 1] = splitline[j];
                    currentSheet.Cells[i+offset, 5] = "формула"; //not implemented yet
                    currentSheet.Cells[i+offset, 6] = currentSheet.Name;
                }
            }
            xlWorkBook.Save();
        }

        public int WriteError(System.Collections.Generic.Dictionary<string,string> errors, string name, int sheet)
        {
            int rowNum = 1;
            currentSheet = xlWorkBook.Worksheets.get_Item(sheet + 1);
            currentSheet.Name = name;
            foreach (System.Collections.Generic.KeyValuePair<string, string> item in errors)
            {
                string[] splitline = item.Value.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                string errormessage = splitline[0] + " " + splitline[1] + ": ";
                switch (splitline[1])
                {
                    case "50":
                        {
                            errormessage += "По запросу \"" + item.Key + "\" ничего не найдено";
                            break;
                        }
                    case "120":
                        {
                            errormessage += "указан неправильный ключ API";
                            break;
                        }
                    case "130":
                        {
                            errormessage += "ваш план подписки не допускает использования API.";
                            break;
                        }
                    case "131":
                        {
                            errormessage += "Превышен лимит запросов к API";
                            break;
                        }
                    case "132":
                        {
                            errormessage += "баланс API подошел к концу. Пополните счет API";
                            break;
                        }
                    case "133":
                        {
                            errormessage += "запрашиваемая база данных недоступна";
                            break;
                        }
                    case "134":
                        {
                            errormessage += "Превышен лимит запросов к API";
                            break;
                        }
                    case "135":
                        {
                            errormessage += "запрашиваемый вид отчета сейчас недоступен";
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                currentSheet.Cells[rowNum, 1] = errormessage;
                rowNum++;
            }
            xlWorkBook.Save();
            return rowNum;
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
