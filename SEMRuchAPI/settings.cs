using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SEMRuchAPI
{
    [Serializable()]
    class Settings
    {
        public string APIKey = "";
        public string formula = "";
        public string negWords = "";
        public int locale = 0;
        public int maxLinesPerReport = 1000;

        public  Settings()
        {
            Settings temp = Settings.Read();
            this.APIKey = temp.APIKey;
            this.formula = temp.formula;
            this.negWords = temp.negWords;
            this.locale = temp.locale;
            this.maxLinesPerReport = temp.maxLinesPerReport;
        }

        public Settings(string apikey, string formula, string negwords, int locale,int maxlines)
        {
            this.APIKey = apikey;
            this.formula = formula;
            this.negWords = negwords;
            this.locale = locale;
            this.maxLinesPerReport = maxlines;
        }

        private static Settings Read()
        {
            using (Stream fileStream = File.Open(AppDomain.CurrentDomain.BaseDirectory.ToString() + "settings.dat", FileMode.OpenOrCreate))
            {
                if (fileStream.Length>0)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (Settings)formatter.Deserialize(fileStream);
                }
                else
                {
                    return new Settings("", "", "", 0, 10000);
                }
            }
        }

        public void Write()
        {
            using (Stream filestream = File.Open(AppDomain.CurrentDomain.BaseDirectory.ToString() + "settings.dat", FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(filestream, this);
                filestream.Close();
            }
        }
    }
}
