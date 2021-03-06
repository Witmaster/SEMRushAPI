﻿using System;
using System.Collections;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace SEMRuchAPI
{
    public partial class SemRushAPI : Form
    {
        Settings settings = new Settings();
        private static System.Collections.Generic.List<string> competitorsList  = new System.Collections.Generic.List<string>();
        ExcelHandler excel;
        public SemRushAPI()
        {
            InitializeComponent();
            
            Locale.SelectedIndex = settings.locale;
            NegWords.Text = settings.negWords;
            MaxLines.Value = settings.maxLinesPerReport;
           // //using (FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Apikey.dat", FileMode.OpenOrCreate)) { };
           // //using (StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Apikey.dat"))
           // //{
           ////     APIKey = reader.ReadLine();
           // //}
           // //using (StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory.ToString() + "formula.dat"))
           // {
           //     formula = reader.ReadLine();
           // }
           // using (FileStream reader = new FileStream(AppDomain.CurrentDomain.BaseDirectory.ToString() + "filter.dat", FileMode.OpenOrCreate, FileAccess.Read))
           // {
           //     byte[] buffer = new byte[reader.Length];
           //     reader.Read(buffer, 0, (int)reader.Length);
           //     foreach (byte b in buffer)
           //     {
           //         NegWords.Text += Convert.ToChar(b);
           //     }
           // }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (BaseKeywords.Text.Length > 0)
            {
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    excel = new ExcelHandler(saveFile.FileName, BaseKeywords.Text.Split(new string[] { ";", ",", Environment.NewLine },
            StringSplitOptions.RemoveEmptyEntries).Length, settings.formula);
                    if (excel.isOK)
                    {
                        BeginParsing();
                    }
                }
            }
        }

        private void BeginParsing()
        {
            if (settings.APIKey != "" || settings.APIKey != null)
            {
                button1.Enabled = false;
                progressBar.Visible = true;
                progressBar.Value = 0;
                string filter = "";
                if (NegWords.Text != "")
                {
                    var negWords = NegWords.Text.Split(new String[] { ",", ";", Environment.NewLine },
                        StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < 25 && i < negWords.Length; i++)
                    {
                        filter += "&display_filter=-|Ph|Co|";
                        filter += negWords[i].Trim();
                    }
                }
                Parser handle = new Parser(settings.APIKey, Locale.SelectedItem.ToString(), (int)MaxLines.Value, filter);
                var baseKeywords = BaseKeywords.Text.Split(new string[] { ";", ",", Environment.NewLine },
        StringSplitOptions.RemoveEmptyEntries);
                if (baseKeywords.Length != 0)
                {
                    int sheetNum = 0;
                    for (int i = 0; i < baseKeywords.Length; i++)
                    {
                        float progress = ((float)(i + 1) / (float)baseKeywords.Length) * 100;
                        progressBar.Value = (int)progress;
                        if (baseKeywords[i] != " ")
                        {
                            sheetNum++;
                            if (baseKeywords[i].Contains("|"))
                            {
                                string response = "";
                                System.Collections.Generic.Dictionary<string, string> errors = new System.Collections.Generic.Dictionary<string, string>();
                                var subSet = baseKeywords[i].Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string subKeyword in subSet)
                                {
                                    string temp = handle.GetPhraseMatch(subKeyword);
                                    if (temp.Contains("::"))
                                    {
                                        errors.Add(subKeyword, temp);
                                    }
                                    else
                                    {
                                        {
                                            if (response.Length > 0)
                                            {
                                                response += temp.Substring(37);
                                            }
                                        }
                                    }
                                }
                                if (errors.Count > 0)
                                {
                                    int rowIndex = excel.WriteError(errors, subSet[0], sheetNum);
                                    excel.WriteToTab(response, subSet[0], sheetNum, rowIndex);
                                }
                                else
                                {
                                    excel.WriteToTab(response, subSet[0], sheetNum, 0);
                                }
                            }
                            else
                            if (baseKeywords[i].Contains("http://"))
                            {
                                competitorsList.Add(baseKeywords[i].Substring(7));
                                sheetNum--;
                            }
                            else
                            {
                                string response = handle.GetPhraseMatch(baseKeywords[i]);
                                if (response.Length > 0)
                                {
                                    if (response.Contains("::"))
                                    {
                                        excel.WriteError(new System.Collections.Generic.Dictionary<string, string>() { { baseKeywords[i], response } },
                                            baseKeywords[i], sheetNum);
                                    }
                                    else
                                    {
                                        excel.WriteToTab(response.Substring(37), baseKeywords[i], sheetNum, 0);
                                    }
                                }
                            }
                        }
                    }
                  if (competitorsList.Count > 0) { excel.WriteCompetition(handle.GetCompetition(competitorsList), sheetNum); }
                }
            }
            progressBar.Visible = false;
            button1.Enabled = true;
            excel.Dispose();
        }

        private void addChangeApiKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeApiKey(ref settings.APIKey);
        }

        private static DialogResult ChangeApiKey(ref string input)
        {
            System.Drawing.Size windowSize = new System.Drawing.Size(200, 60);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = windowSize;
            inputBox.Text = "Enter or edit API key";
            inputBox.StartPosition = FormStartPosition.CenterParent;

            System.Windows.Forms.TextBox keyField = new TextBox();
            keyField.Text = input;
            keyField.Size = new System.Drawing.Size(windowSize.Width - 10, 23);
            keyField.Location = new System.Drawing.Point(5, 2);
            inputBox.Controls.Add(keyField);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "OK";
            okButton.Location = new System.Drawing.Point(windowSize.Width / 2 - 37, 30);
            inputBox.Controls.Add(okButton);

            inputBox.AcceptButton = okButton;
            DialogResult result = inputBox.ShowDialog();
            input = keyField.Text;
            return result;
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelp();
        }

        private DialogResult ShowHelp()
        {
        System.Drawing.Size windowSize = new System.Drawing.Size(250, 400);
        Form inputBox = new Form();

        inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        inputBox.ClientSize = windowSize;
        inputBox.Text = "Справка";
        inputBox.StartPosition = FormStartPosition.CenterParent;

        System.Windows.Forms.TextBox helpField = new TextBox();
            helpField.Enabled = true;
            helpField.Multiline = true;
            helpField.WordWrap = true;
            helpField.ScrollBars = ScrollBars.Vertical;
            helpField.Text = "Программа предназначена для быстрого сбора ключевиков во фразовом соответствии из SemRush. " +
                    "Формат выходного файла соответствует формату файла keyword-research-<sitename>-RAW.xlsx."
                    + Environment.NewLine + "Поле \"Базовые ключевики\" - поле под базовые ключевики, разделенные запятой, "
                    + "точкой с запятой, переводом строки, либо их комбинациями. Разделенные знаком \" | \" ключевые слова "
                    + "будут собраны на одну вкладку." + Environment.NewLine + "Поле \"Стоп - слова\" - список из максимум "
                    + "25 стоп-слов, разделенных запятыми, точками с запятыми или переводом строки. "
                    + "Рекомендую вбивать туда города, отличные от того, на который ведется таргетинг." +
                    Environment.NewLine + "ВНИМАНИЕ" + Environment.NewLine + "Стоп-слова сохранятся только, если нажать кнопку "
                    + "\"настройки\"->\"сохранить настройки\". Это касается и всех остальных настроек, как-то ключ АПИ," 
                    +"формула Advisability и т.п." + Environment.NewLine + "Поле \"максимум строк\" - максимум строк, "
                    + "которые будут выгружены для одного ключевика." + Environment.NewLine + "Список \"База данных\" - список баз "
                    + "регионального поиска Google, Bing, etc." + Environment.NewLine + "Выберите нужную." + Environment.NewLine
                    +"Также программа собирает данные о конкурентах: топ-20 ключевиков с топ-20 позиций для"+Environment.NewLine
                    +"каждого, чтобы собрать данные о конкурентах, добавьте их в список ключевых слов."
                    +"УРЛ конкурентов обязательно должны начинаться с \"http://\" Если смешать конкурентов и обычные ключевые слова,"+
                    " сначала будут собраны данные по ключевикам, потом все конкуренты будут собраны на одну последнюю вкладку";
        helpField.Select(helpField.Text.Length, 0);
        helpField.Size = new System.Drawing.Size(windowSize.Width, windowSize.Height);
        helpField.Location = new System.Drawing.Point(5, 2);
        inputBox.Controls.Add(helpField);

        Button okButton = new Button();
        okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
        okButton.Name = "okButton";
        okButton.Size = new System.Drawing.Size(75, 23);
        okButton.Text = "OK";
        okButton.Location = new System.Drawing.Point(windowSize.Width/2-37, 30);
        inputBox.Controls.Add(okButton);

        inputBox.AcceptButton = okButton;
        DialogResult result = inputBox.ShowDialog();
        return result;
        }

        private void формулаAdvisabilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeAdvisabilityFormula(ref settings.formula);
            
        }

        private static DialogResult ChangeAdvisabilityFormula(ref string input)
        {
            System.Drawing.Size windowSize = new System.Drawing.Size(200, 60);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = windowSize;
            inputBox.Text = "Введите формулу в формате excel c букво-цифровой нотацией, заменяя цифровую составляющую знаком \"|\"";
            inputBox.StartPosition = FormStartPosition.CenterParent;

            System.Windows.Forms.TextBox keyField = new TextBox();
            keyField.Text = input;
            keyField.Size = new System.Drawing.Size(windowSize.Width - 10, 23);
            keyField.Location = new System.Drawing.Point(5, 2);
            inputBox.Controls.Add(keyField);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "OK";
            okButton.Location = new System.Drawing.Point(windowSize.Width / 2 - 37, 30);
            inputBox.Controls.Add(okButton);

            inputBox.AcceptButton = okButton;
            DialogResult result = inputBox.ShowDialog();
            input = keyField.Text;
            return result;
        }

        private void сохранитьНастройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.negWords = NegWords.Text;
            settings.locale = Locale.SelectedIndex;
            settings.maxLinesPerReport = (int)MaxLines.Value;
            settings.Write();
        }

        private void NegWords_TextChanged(object sender, EventArgs e)
        {
            string[] negs = NegWords.Text.Split(new string[] { Environment.NewLine, ",", ";" }, StringSplitOptions.RemoveEmptyEntries);
            int count = negs.Length;
            negWordsCount.Text = negs.Length.ToString() + "/25";
            if (negs.Length>25)
            {
                int lastIndex = NegWords.Text.LastIndexOfAny(new char[] { ',', ';' });
                int lastIndexNL = NegWords.Text.LastIndexOfAny(Environment.NewLine.ToCharArray());
                if(lastIndex>lastIndexNL)
                {
                    NegWords.Text= NegWords.Text.Remove(lastIndex);
                }
                else if (lastIndexNL!=-1)
                {
                    NegWords.Text=NegWords.Text.Remove(lastIndexNL);
                }
                NegWords.Select(NegWords.Text.Length, 0);
            }
            if (negs.Length==25)
            {
                negWordsCount.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                negWordsCount.ForeColor = System.Drawing.Color.Black;
            }
        }
    }
}
