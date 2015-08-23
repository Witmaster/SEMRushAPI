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
    }
}
