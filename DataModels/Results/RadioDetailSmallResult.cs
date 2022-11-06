using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel.Results
{
    public class NowplayingRadio
    {
        /// <summary>
        /// 节目id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 时长
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string start_time { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string end_time { get; set; }
        /// <summary>
        /// 节目名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 节目名称
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 列表rid
        /// </summary>
        public int playbill_rid { get; set; }
        /// <summary>
        /// 列表id
        /// </summary>
        public int playbill_id { get; set; }
    }

    public class RadioDetailSmallData
    {
        /// <summary>
        /// 电台id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 厦门音乐广播
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 封面
        /// </summary>
        public string cover { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string content_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int channel_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public NowplayingRadio nowplaying { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int audience_count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int top_category_id { get; set; }
        /// <summary>
        /// 音乐台
        /// </summary>
        public string top_category_title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int top_category_play_w_rank { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int programs_count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string update_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int res_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int region_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int city_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<int> category_ids { get; set; }
    }

    public class RadioDetailSmallResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string Success { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RadioDetailSmallData Data { get; set; }
    }

}
