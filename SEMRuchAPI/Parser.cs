using System.Net;

namespace SEMRuchAPI
{
    class Parser
    {
        private static string key;
        private static string locale;
        private static string maxlines;
        private static string filter;

        public Parser(string apikey, string database, int limit, string negwords)
        {
            key = apikey;
            locale = database;
            maxlines = limit.ToString();
            filter = negwords;    
        }

        public string GetPhraseMatch(string keyword)
        {
            keyword = keyword.Replace(' ', '+');
            string requestURL = "http://api.semrush.com/?type=phrase_fullsearch&key=" 
                + key + "&phrase=" + keyword + "&database=" + locale
                + "&display_limit=" + maxlines 
                + "&export_columns=Ph,Nq,Co,Cp&display_sort=nq_desc" + filter;
            using (WebClient client = new WebClient())
            {
                return client.DownloadString(requestURL);
            }
        }

        public string GetCompetition(System.Collections.Generic.List<string> competitors)
        {
            string response = "";
            for (int i = 0; i < competitors.Count; i++)
            {
                string requestURL = "http://api.semrush.com/?type=domain_organic&key="
                    + key + "&domain=" +competitors[i] + "&database=" + locale
                    + "&display_limit=" + 20
                    + "&export_columns=Ph,Pp,Nq&display_sort=pp_desc&display_filter=-|Pp|Gt|10";
                using (WebClient client = new WebClient())
                {
                    string webresp = client.DownloadString(requestURL);
                    if (!webresp.Contains("::"))
                    {
                        response +=competitors[i]+System.Environment.NewLine + webresp.Substring(webresp.IndexOf(System.Environment.NewLine))+System.Environment.NewLine;
                    }
                    else { response += "По домену " + competitors[i] + " ничего не найдено" + System.Environment.NewLine; }
                }
            }
            competitors.Clear();
            return response;
        }
    }
}
