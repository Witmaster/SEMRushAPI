namespace SEMRuchAPI
{
    partial class SemRushAPI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BaseKeywords = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addChangeApiKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.формулаAdvisabilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьСписокСтопсловToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьНастройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NegWords = new System.Windows.Forms.TextBox();
            this.Locale = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.saveFile = new System.Windows.Forms.SaveFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.MaxLines = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.negWordsCount = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxLines)).BeginInit();
            this.SuspendLayout();
            // 
            // BaseKeywords
            // 
            this.BaseKeywords.Location = new System.Drawing.Point(12, 74);
            this.BaseKeywords.Multiline = true;
            this.BaseKeywords.Name = "BaseKeywords";
            this.BaseKeywords.Size = new System.Drawing.Size(177, 177);
            this.BaseKeywords.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(404, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addChangeApiKeyToolStripMenuItem,
            this.формулаAdvisabilityToolStripMenuItem,
            this.сохранитьСписокСтопсловToolStripMenuItem,
            this.сохранитьНастройкиToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.fileToolStripMenuItem.Text = "Настройки";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // addChangeApiKeyToolStripMenuItem
            // 
            this.addChangeApiKeyToolStripMenuItem.Name = "addChangeApiKeyToolStripMenuItem";
            this.addChangeApiKeyToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.addChangeApiKeyToolStripMenuItem.Text = "Ключ API";
            this.addChangeApiKeyToolStripMenuItem.Click += new System.EventHandler(this.addChangeApiKeyToolStripMenuItem_Click);
            // 
            // формулаAdvisabilityToolStripMenuItem
            // 
            this.формулаAdvisabilityToolStripMenuItem.Name = "формулаAdvisabilityToolStripMenuItem";
            this.формулаAdvisabilityToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.формулаAdvisabilityToolStripMenuItem.Text = "Формула Advisability";
            this.формулаAdvisabilityToolStripMenuItem.Click += new System.EventHandler(this.формулаAdvisabilityToolStripMenuItem_Click);
            // 
            // сохранитьСписокСтопсловToolStripMenuItem
            // 
            this.сохранитьСписокСтопсловToolStripMenuItem.Name = "сохранитьСписокСтопсловToolStripMenuItem";
            this.сохранитьСписокСтопсловToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.сохранитьСписокСтопсловToolStripMenuItem.Text = "Сохранить список стоп-слов";
            this.сохранитьСписокСтопсловToolStripMenuItem.Click += new System.EventHandler(this.сохранитьСписокСтопсловToolStripMenuItem_Click);
            // 
            // сохранитьНастройкиToolStripMenuItem
            // 
            this.сохранитьНастройкиToolStripMenuItem.Name = "сохранитьНастройкиToolStripMenuItem";
            this.сохранитьНастройкиToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.сохранитьНастройкиToolStripMenuItem.Text = "Сохранить настройки";
            this.сохранитьНастройкиToolStripMenuItem.Click += new System.EventHandler(this.сохранитьНастройкиToolStripMenuItem_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            this.справкаToolStripMenuItem.Click += new System.EventHandler(this.справкаToolStripMenuItem_Click);
            // 
            // NegWords
            // 
            this.NegWords.Location = new System.Drawing.Point(209, 75);
            this.NegWords.Multiline = true;
            this.NegWords.Name = "NegWords";
            this.NegWords.Size = new System.Drawing.Size(183, 177);
            this.NegWords.TabIndex = 2;
            this.NegWords.TextChanged += new System.EventHandler(this.NegWords_TextChanged);
            // 
            // Locale
            // 
            this.Locale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Locale.FormattingEnabled = true;
            this.Locale.Items.AddRange(new object[] {
            "au",
            "us",
            "uk",
            "ca",
            "ru",
            "de",
            "fr",
            "es",
            "it",
            "br",
            "bing-us",
            "ar",
            "be",
            "ch",
            "dk",
            "fi",
            "hk",
            "ie",
            "il",
            "mx",
            "nl",
            "no",
            "pl",
            "se",
            "sg",
            "tr",
            "mobile-us",
            "jp",
            "in"});
            this.Locale.Location = new System.Drawing.Point(329, 29);
            this.Locale.Name = "Locale";
            this.Locale.Size = new System.Drawing.Size(63, 21);
            this.Locale.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Базовые ключевые слова:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(207, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Стоп-слова:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(317, 256);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Старт";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(10, 257);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(299, 23);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 7;
            this.progressBar.Visible = false;
            // 
            // saveFile
            // 
            this.saveFile.DefaultExt = "*.xlsx";
            this.saveFile.FileName = "RAW-keyword-research-for-_.xlsx";
            this.saveFile.Filter = "*.xlsx|*.xls";
            this.saveFile.Title = "Куда писать?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(250, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "База данных";
            // 
            // MaxLines
            // 
            this.MaxLines.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.MaxLines.Location = new System.Drawing.Point(112, 30);
            this.MaxLines.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.MaxLines.Name = "MaxLines";
            this.MaxLines.Size = new System.Drawing.Size(120, 20);
            this.MaxLines.TabIndex = 9;
            this.MaxLines.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Максимум строк:";
            // 
            // negWordsCount
            // 
            this.negWordsCount.AutoSize = true;
            this.negWordsCount.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.negWordsCount.Location = new System.Drawing.Point(356, 59);
            this.negWordsCount.Name = "negWordsCount";
            this.negWordsCount.Size = new System.Drawing.Size(30, 13);
            this.negWordsCount.TabIndex = 11;
            this.negWordsCount.Text = "0/25";
            // 
            // SemRushAPI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 286);
            this.Controls.Add(this.negWordsCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.MaxLines);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Locale);
            this.Controls.Add(this.NegWords);
            this.Controls.Add(this.BaseKeywords);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "SemRushAPI";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxLines)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox BaseKeywords;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TextBox NegWords;
        private System.Windows.Forms.ComboBox Locale;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addChangeApiKeyToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown MaxLines;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem сохранитьСписокСтопсловToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem формулаAdvisabilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьНастройкиToolStripMenuItem;
        private System.Windows.Forms.Label negWordsCount;
    }
}

