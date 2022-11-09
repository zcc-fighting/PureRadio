using Newtonsoft.Json;
using PureRadio.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel.Results
{

    public class DataItem
    {
        /// <summary>
        /// 1233 新疆蒙古语广播
        /// </summary>
        public string title { get; set; }
    }

    public class SearchSuggestResult
    {
        /// <summary>
        /// 
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DataItem> data { get; set; }

        public static List<string> GetSuggest(string _queryText)
        {
            string requestBody = "k=" + WebUtility.UrlEncode(_queryText);
            string resultJson = HttpRequest.SendGet("https://search.qtfm.cn/v3/suggest", requestBody);
            SearchSuggestResult result = JsonConvert.DeserializeObject<SearchSuggestResult>(resultJson);
            if (result == null) return new List<string>();
            List<string> suggestions = new List<string>();
            foreach (var item in result.data)
            {
                suggestions.Add(item.title);
            }
            return suggestions;
        }
    }

}
