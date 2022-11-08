using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PureRadio.Common;

namespace PureRadio.DataModel.Results
{
    public class RadioCategoriesRoot
    {
        public string CategoryName { get; set; }
        public List<RadioCategoriesItem> Data { get; set; }
    }

    public class RadioCategoriesList
    {
        public List<string> CategoryNameList { get; set; }
        public List<List<RadioCategoriesItem>> Data { get; set; }
    }

    public class RadioCategories
    {
        public List<RadioCategoriesItem> radioData { get; set; }
        public static List<RadioCategoriesItem> Radios(string content_id,int page,int total) 
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            string listeners = resourceLoader.GetString("PlayCountDisplayRadio");
            string resultJson = HttpRequest.SendGet("http://rapi.qingting.fm/channels?category_id="+content_id+"&page="+page+"&pagesize="+total);
            RadioCategoriesRoot result = JsonConvert.DeserializeObject<RadioCategoriesRoot>(resultJson);
            if (result == null) return new List<RadioCategoriesItem>();
            foreach(RadioCategoriesItem item in result.Data)
            {
                item.audience_count = listeners + item.audience_count;
            }
            List<RadioCategoriesItem> radioCategoriesResults = result.Data;
            return radioCategoriesResults;
        }


        public List<List<RadioCategoriesItem>> radioListData { get; set; }
        public static List<List<RadioCategoriesItem>> GetRadioCategoriesList(string program_id, int total)
        {
            List<List<RadioCategoriesItem>> result = new List<List<RadioCategoriesItem>>();
            result.Add(Radios(program_id, 1, total));
            return result;
        }
    }
}
