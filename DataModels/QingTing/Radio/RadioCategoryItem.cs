using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.QingTing.Radio
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RadioCategoryItem
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "content_id", Required = Required.Default)]
        public int content_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "content_type", Required = Required.Default)]
        public string content_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "cover", Required = Required.Default)]
        public string cover { get; set; }
        /// <summary>
        /// 怀集音乐之声
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "title", Required = Required.Default)]
        public string title { get; set; }
        /// <summary>
        /// 怀集音乐之声是一个旨在宣传怀集,服务怀集,建设怀集为宗旨
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description", Required = Required.Default)]
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "nowplaying", Required = Required.Default)]
        public Nowplaying nowplaying { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "audience_count", Required = Required.Default)]
        public int audience_count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "update_time", Required = Required.Default)]
        public string update_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "categories", Required = Required.Default)]
        public List<CategoriesItem> categories { get; set; }
    }


    //public class BroadcastersItem
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public int id { get; set; }
    //    /// <summary>
    //    /// 怀集音乐之声（肇庆站）
    //    /// </summary>
    //    public string username { get; set; }
    //}

    public class Nowplaying
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string start_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string end_time { get; set; }
        /// <summary>
        /// 最爱经典金曲
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 最爱经典金曲
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int playbill_rid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int playbill_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<BroadcastersItem> broadcasters { get; set; }
    }

    public class CategoriesItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 音乐台
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pid { get; set; }
    }

}
