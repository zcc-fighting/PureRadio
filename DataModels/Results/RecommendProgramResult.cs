using Newtonsoft.Json;
using PureRadio.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel.Results
{
    public class RecoProgramItem
    {
        /// <summary>
        /// 节目id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 封面
        /// </summary>
        public string cover { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 播放量
        /// </summary>
        public string playcount { get; set; }
    }

    public class RecoProgramData
    {
        /// <summary>
        /// 
        /// </summary>
        public List<RecoProgramItem> channels { get; set; }
    }

    public class RecommendProgram
    {
        /// <summary>
        /// 
        /// </summary>
        public RecoProgramData data { get; set; }

        public static List<RecoProgramItem> GetRecoPrograms()
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            string listeners = resourceLoader.GetString("PlayCountDisplayProgram");
            List<RecoProgramItem> list = new List<RecoProgramItem>();
            string requestBody = "category=545&attrs=0&curpage=1";
            string resultJson = HttpRequest.SendGet("https://i.qingting.fm/capi/neo-channel-filter", requestBody);
            resultJson = resultJson.Replace(",\"playcount\":\"", ",\"playcount\":\"" + listeners);
            RecommendProgram result = JsonConvert.DeserializeObject<RecommendProgram>(resultJson);
            if (result == null) return list;
            list = result.data.channels;
            return list;
        }
    }

}
