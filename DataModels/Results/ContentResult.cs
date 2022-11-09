using Newtonsoft.Json;
using PureRadio.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel.Results
{
    public class ContentRoot
    {
        public ContentData data { get; set; }
    }

    public class ContentData
    {
        public List<ContentItem> channels { get; set; }
    }

    public class ContentResult
    {
        public static List<ContentItem> Content(string content_id, int page)
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            string listeners = resourceLoader.GetString("PlayCountDisplayProgram");
            string resultJson = HttpRequest.SendGet("https://i.qingting.fm/capi/neo-channel-filter?category=" + content_id + "&curpage=" + page);
            resultJson = resultJson.Replace(",\"playcount\":\"", ",\"playcount\":\"" + listeners);
            ContentRoot result = JsonConvert.DeserializeObject<ContentRoot>(resultJson);
            if (result == null) return new List<ContentItem>();
            List<ContentItem> ContentResults = result.data.channels;
            return ContentResults;
        }
    }
}
