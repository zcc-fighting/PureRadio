using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel
{

    public class RadioCategoriesItem
    {
        public int content_id { get; set; }
        public string cover { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public RadioCategoriesNowplayingItem nowplaying { get; set; }
        public string audience_count { get; set; }
        public string update_time { get; set; }
    }

    public class RadioCategoriesNowplayingItem
    {
        public int id { get; set; }
        public int duration { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string title { get; set; }
        public List<RadioCategoriesBroadcaster> broadcasters { get; set; }
    }

    public class RadioCategoriesBroadcaster
    {
        public int id { get; set; }
        public string username { get; set; }
        public string thumb { get; set; }
    }
}
