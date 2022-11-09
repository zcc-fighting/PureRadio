using Newtonsoft.Json;
using PureRadio.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel.Results
{
    public class SearchProgramRoot
    {
        public SearchProgramData data { get; set; }
    }

    public class SearchProgramData
    {
        public SearchProgramResult searchResultsPage { get; set; }
    }

    public class SearchProgramResult
    {
        public List<SearchProgramResultItem> searchData { get; set; }

        public static List<SearchProgramResultItem> SearchProgram(string query, int page)
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            string listeners = resourceLoader.GetString("PlayCountDisplayProgram");
            string requestBody = "{searchResultsPage(keyword:\\\"" + query + "\\\", page:" + page + ", include:\\\"channel_ondemand\\\" ) {numFound,searchData}}";
            string resultJson = HttpRequest.SendPost("https://webbff.qingting.fm/www", requestBody, true);
            resultJson = resultJson.Replace(",\"playcount\":\"", ",\"playcount\":\"" + listeners);
            SearchProgramRoot result = JsonConvert.DeserializeObject<SearchProgramRoot>(resultJson);
            if (result == null) return new List<SearchProgramResultItem>();
            List<SearchProgramResultItem> searchProgramResults = result.data.searchResultsPage.searchData;
            return searchProgramResults;
        }
    }

    
}
