using System;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace SEMRuchAPI
{
    public partial class SemRushAPI : Form
    {
        private static string APIKey = "";
        ExcelHandler excel;
        public SemRushAPI()
        {
            InitializeComponent();
            Locale.SelectedIndex = 0;
            using (FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Apikey.dat", FileMode.OpenOrCreate)) { };
            using (StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Apikey.dat"))
            {
                APIKey = reader.ReadLine();
            }
            using (FileStream reader = new FileStream(AppDomain.CurrentDomain.BaseDirectory.ToString() + "filter.dat", FileMode.OpenOrCreate, FileAccess.Read))
            {
                byte[] buffer = new byte[reader.Length];
                reader.Read(buffer, 0, (int)reader.Length);
                foreach (byte b in buffer)
                {
                    NegWords.Text += Convert.ToChar(b);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                excel = new ExcelHandler(saveFile.FileName, BaseKeywords.Text.Split(new string[] { ";", ",", Environment.NewLine },
        StringSplitOptions.RemoveEmptyEntries).Length);
                if (excel.isOK)
                {
                    BeginParsing();
                }
            }
        }

        private void BeginParsing()
        {
            if (APIKey != "" || APIKey != null)
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
                        filter += negWords[i];
                    }
                }
                Parser handle = new Parser(APIKey, Locale.SelectedItem.ToString(), (int)MaxLines.Value, filter);
                var baseKeywords = BaseKeywords.Text.Split(new string[] { ";", ",", Environment.NewLine },
        StringSplitOptions.RemoveEmptyEntries);
                if (baseKeywords.Length != 0)
                {
                    for (int i = 0; i < baseKeywords.Length; i++)
                    {
                        float progress = ((float)(i + 1) / (float)baseKeywords.Length) * 100;
                        progressBar.Value = (int)progress;
                        Thread.Sleep(500);
                        if (baseKeywords[i].Contains("|"))
                        {
                            string response = "";
                            var subSet = baseKeywords[i].Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string subKeyword in subSet)
                            {
                                response += handle.GetPhraseMatch(subKeyword);
                                excel.WriteToTab(response, subSet[0]);
                            }
                        }
                        else
                        {
                            string response = handle.GetPhraseMatch(baseKeywords[i]);
                            excel.WriteToTab(response, baseKeywords[i]);
                        }
                    }
                }
            }
            progressBar.Visible = false;
            button1.Enabled = true;
            excel.Dispose();
        }

        private void addChangeApiKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeApiKey(ref APIKey);
            using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory.ToString() + "APIKey.dat"))
            {
                writer.WriteLine(APIKey);
            }
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

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void сохранитьСписокСтопсловToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory.ToString() + "filter.dat"))
            {
                writer.Write(NegWords.Text);
            }
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
            helpField.Enabled = false;
            helpField.Multiline = true;
            helpField.WordWrap = true;
        helpField.Text = "Программа предназначена для быстрого сбора ключевиков во фразовом соответствии из SemRush. "+
                "Формат выходного файла соответствует формату файла keyword-research-<sitename>-RAW.xlsx."
                +Environment.NewLine +"Поле \"Базовые ключевики\" - поле под базовые ключевики, разделенные запятой, "
                +"точкой с запятой, переводом строки, либо их комбинациями. Разделенные знаком \" | \" ключевые слова "
                +"будут собраны на одну вкладку."+ Environment.NewLine +"Поле \"Стоп - слова\" - список из максимум "
                +"25 стоп-слов, разделенных запятыми, точками с запятыми или переводом строки. "
                +"Рекомендую вбивать туда города, отличные от того, на который ведется таргетинг."+
                Environment.NewLine+"ВНИМАНИЕ"+Environment.NewLine+"Стоп-слова сохранятся только, если нажать кнопку "
                +"\"настройки\"->\"сохранить стоп-слова\"."+Environment.NewLine+"Поле \"максимум строк\" - максимум строк, "
                +"которые будут выгружены для одного ключевика."+Environment.NewLine+"Список \"База данных\" - список баз "
                +"регионального поиска Google, Bing, etc."+Environment.NewLine+"Выберите нужную.";
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
    }
}
