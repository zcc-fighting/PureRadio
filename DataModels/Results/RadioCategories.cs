using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PureRadio.Common;

namespace PureRadio.DataModel.RadioCategories
{
    public class RadioCategoriesRoot
    {
        public List<RadioCategoriesItem> Data { get; set; }
    }

    public class RadioCategories
    {
        public List<RadioCategoriesItem> radioData { get; set; }
        public static List<RadioCategoriesItem> Radios(string content_id,int page) 
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            string listeners = resourceLoader.GetString("PlayCountDisplayRadio");
            string resultJson = HttpRequest.SendGet("http://rapi.qingting.fm/channels?category_id="+content_id+"&page="+page+"&pagesize=15");
            RadioCategoriesRoot result = JsonConvert.DeserializeObject<RadioCategoriesRoot>(resultJson);
            if (result == null) return new List<RadioCategoriesItem>();
            foreach(RadioCategoriesItem item in result.Data)
            {
                item.audience_count = listeners + item.audience_count;
            }
            List<RadioCategoriesItem> radioCategoriesResults = result.Data;
            return radioCategoriesResults;
        }
    }
}
