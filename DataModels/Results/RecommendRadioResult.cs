using Newtonsoft.Json;
using PureRadio.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PureRadio.DataModel.Results
{
    internal class RecommendRadioResult
    {
    }

    public class RecoRadioItem
    {
        /// <summary>
        /// 封面
        /// </summary>
        public string cover { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string channelTitle { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 播放量
        /// </summary>
        public int playcount { get; set; }
        /// <summary>
        /// id
        /// </summary>
        public int channel_id { get; set; }
        /// <summary>
        /// 正在播放
        /// </summary>
        public string desc { get; set; }
    }

    public class RecoRadioPage
    {
        /// <summary>
        /// 
        /// </summary>
        public List<RecoRadioItem> radioPlaying { get; set; }
    }

    public class RecoRadioData
    {
        /// <summary>
        /// 
        /// </summary>
        public RecoRadioPage radioPage { get; set; }
    }

    public class RecommendRadio
    {
        /// <summary>
        /// 
        /// </summary>
        public RecoRadioData data { get; set; }

        public static List<RecoRadioItem> GetRecoRadio()
        {
            //string result = Regex.Replace(input, pattern, replacement);
            //var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            //string listeners = resourceLoader.GetString("PlayCountDisplayProgram");
            List<RecoRadioItem> list = new List<RecoRadioItem>();
            string query = "{radioPage{radioPlaying}}";
            string resultJson = HttpRequest.SendPost("https://webbff.qingting.fm/www", query, true);
            resultJson = resultJson.Replace("\"to\":\"/radios/", "\"channel_id\":\"").Replace("\"imgUrl\":\"", "\"cover\":\"http:");
            RecommendRadio result = JsonConvert.DeserializeObject<RecommendRadio>(resultJson);
            if (result == null) return list;
            list = result.data.radioPage.radioPlaying;
            return list;
        }
    }

}
