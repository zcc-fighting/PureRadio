using Newtonsoft.Json;
using PureRadio.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel.Results
{

    public class LeaderboardRoot
    {
        public List<LeaderboardItem> Data { get; set; }
    }

    public class RadioRank
    {
        public static List<LeaderboardItem> Radios(string region_id)
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            string listeners = resourceLoader.GetString("PlayCountDisplayRadio");
            string resultJson = HttpRequest.SendGet("https://rapi.qtfm.cn/billboards/0/" + region_id + "/channels");
            resultJson = resultJson.Replace(",\"audience_count\":", ",\"audience_count\":\"" + listeners).Replace(",\"update_time\":", "\",\"update_time\":");
            resultJson = resultJson.Replace(",\"liveshow_id\"", "\",\"liveshow_id\"").Replace("\"\",\"update_time", "\",\"update_time");
            LeaderboardRoot result = JsonConvert.DeserializeObject<LeaderboardRoot>(resultJson);
            if (result == null) return new List<LeaderboardItem>();
            List<LeaderboardItem> leaderboardResults = result.Data;
            return leaderboardResults;
        }
    }
}
