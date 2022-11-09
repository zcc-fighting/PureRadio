using Newtonsoft.Json;
using PureRadio.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel.Results
{
    public class SearchRadioRoot
    {
        public SearchRadioData data { get; set; }
    }

    public class SearchRadioData
    {
        public SearchRadioResult searchResultsPage { get; set; }
    }

    public class SearchRadioResult
    {
        public List<SearchRadioResultItem> searchData { get; set; }


        public static List<SearchRadioResultItem> SearchRadio(string query , int page)
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            string listeners = resourceLoader.GetString("PlayCountDisplayRadio");
            string requestBody = "{searchResultsPage(keyword:\\\"" + query + "\\\", page:" + page + ", include:\\\"channel_live\\\" ) {numFound,searchData}}";
            string resultJson = HttpRequest.SendPost("https://webbff.qingting.fm/www", requestBody, true);
            resultJson = resultJson.Replace(",\"playcount\":\"", ",\"playcount\":\"" + listeners);
            SearchRadioRoot result = JsonConvert.DeserializeObject<SearchRadioRoot>(resultJson);
            if(result == null) return new List<SearchRadioResultItem>();
            List<SearchRadioResultItem> searchRadioResults = result.data.searchResultsPage.searchData;
            return searchRadioResults;
        }

    }
       
}
