using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel
{
    public class LeaderboardItem
    {
        public int content_id { get; set; }
        public string cover { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public LeaderboardNowplaying nowplaying { get; set; }
        public string audience_count { get; set; }
        public string update_time { get; set; }
    }

    public class LeaderboardNowplaying
    {
        public int duration { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public LeaderboardBroadcaster[] broadcasters { get; set; }
    }

    public class LeaderboardBroadcaster
    {
        public string username { get; set; }
        public string thumb { get; set; }
    }
}
