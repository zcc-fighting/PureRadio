using Newtonsoft.Json;
using PureRadio.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel.Results
{
    public class RadioFullNowplaying
    {
        /// <summary>
        /// 当前id
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
        /// 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
    }

    public class RadioFullAlbum
    {
        /// <summary>
        /// 电台id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 电台名
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 封面
        /// </summary>
        public string cover { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 当前播放
        /// </summary>
        public RadioFullNowplaying nowplaying { get; set; }
        /// <summary>
        /// 收听人数
        /// </summary>
        public int audience_count { get; set; }
        /// <summary>
        /// 首要分类id
        /// </summary>
        public int top_category_id { get; set; }
        /// <summary>
        /// 首要分类名称
        /// </summary>
        public string top_category_title { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public string update_time { get; set; }
    }

    public class RadioFullContent
    {
        /// <summary>
        /// 内容id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string start_time { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string end_time { get; set; }
        /// <summary>
        /// 时长
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// 一周第几天(周日开始)
        /// </summary>
        public int day { get; set; }
        /// <summary>
        /// 归属电台id
        /// </summary>
        public int channel_id { get; set; }
        /// <summary>
        /// 当前内容id
        /// </summary>
        public int program_id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
    }

    public class RadioFullPList
    {
        /// <summary>
        /// 
        /// </summary>
        public List<RadioFullContent> ListSunday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RadioFullContent> ListMonday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RadioFullContent> ListTuesday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RadioFullContent> ListWednesday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RadioFullContent> ListThursday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RadioFullContent> ListFriday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RadioFullContent> ListSaturday { get; set; }
    }

    public class RadioDetailFull
    {
        /// <summary>
        /// 
        /// </summary>
        public RadioFullAlbum album { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public RadioFullPList pList { get; set; }

        
    }




}
