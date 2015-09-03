using System;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace SEMRuchAPI
{
    class ExcelHandler
    {
        public bool isOK = false;
        private static Microsoft.Office.Interop.Excel.Application xlApp;
        private static Workbook xlWorkBook;
        private static Worksheet currentSheet;
        private static string advisabilityFormula = "";
        public ExcelHandler(string filename, int tabcount, string formula)
        {
            xlApp = new Microsoft.Office.Interop.Excel.Application();
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
                string[] temp = formula.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                advisabilityFormula = formula;
            }
        }

        public void WriteToTab(string data, string name, int sheet, int offset)
        {
            offset++;
            currentSheet = xlWorkBook.Worksheets.get_Item(sheet);
            if (name.Length > 30) { currentSheet.Name = name.Remove(30); } else { currentSheet.Name = name; }
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
                }
                string temp = advisabilityFormula;
                temp = temp.Replace("|", (i+ offset).ToString());
                currentSheet.Cells[i + offset, 5] = temp;
                currentSheet.Cells[i + offset, 6] = currentSheet.Name;
            }
            ColorScale reqColorScale = (ColorScale)(currentSheet.get_Range("e2:e" + dataarray.Length.ToString()+1, Type.Missing).FormatConditions.AddColorScale(3));
            reqColorScale.ColorScaleCriteria[1].FormatColor.Color = 0x006b69f8;
            reqColorScale.ColorScaleCriteria[2].FormatColor.Color = 0x00ffffff;
            reqColorScale.ColorScaleCriteria[3].FormatColor.Color = 0x007bbe63;
            xlWorkBook.Save();
        }

        public int WriteError(System.Collections.Generic.Dictionary<string,string> errors, string name, int sheet)
        {
            int rowNum = 1;
            currentSheet = xlWorkBook.Worksheets.get_Item(sheet);
            if (name.Length > 30) { currentSheet.Name = name.Remove(30); } else { currentSheet.Name = name; }
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

        public void WriteCompetition(string data, int sheet)
        {
            sheet++;
            currentSheet = xlWorkBook.Worksheets.get_Item(sheet);
            currentSheet.Name = "Competitors";
            currentSheet.Cells[1, 1] = "Keyword/domain";
            currentSheet.Cells[1, 2] = "Position";
            currentSheet.Cells[1, 3] = "Queries per month";
            string[] temp = data.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < temp.Length; i++)
            {
                string[] buffer = temp[i].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < buffer.Length; j++)
                {
                    currentSheet.Cells[i + 2, j+1] = buffer[j].ToString();
                }
            }
            int lastSheet = xlWorkBook.Worksheets.Count;
            xlApp.DisplayAlerts = false;
            while (lastSheet>sheet)
            {
                xlWorkBook.Worksheets.get_Item(lastSheet).Delete();
                lastSheet--;
            }
            xlApp.DisplayAlerts = true;
        }

    }
}
