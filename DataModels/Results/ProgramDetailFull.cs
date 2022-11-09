using Newtonsoft.Json;
using PureRadio.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel.Results
{

    public class PodcastersItem
    {
        /// <summary>
        /// 蜻蜓头条
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string img_url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string user_id { get; set; }
    }

    public class User_relevance
    {
        /// <summary>
        /// 是否购买
        /// </summary>
        public string sale_status { get; set; }
        /// <summary>
        /// 已付费集数
        /// </summary>
        public List<string> paid_program_ids { get; set; }
    }

    public class ProgramAlbum
    {
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public string update_time { get; set; }
        /// <summary>
        /// 播放量
        /// </summary>
        public string playcount { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string desc { get; set; }
        /// <summary>
        /// 总集数
        /// </summary>
        public int program_count { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 封面
        /// </summary>
        public string img_url { get; set; }
        /// <summary>
        /// 评分
        /// </summary>
        public int score { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 主持人
        /// </summary>
        public List<PodcastersItem> podcasters { get; set; }
        /// <summary>
        /// 是否收费
        /// </summary>
        public string isCharged { get; set; }
        /// <summary>
        /// 用户关系
        /// </summary>
        public User_relevance user_relevance { get; set; }
    }

    public class ProgramlistItem
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
        /// 时长
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public string update_time { get; set; }
        /// <summary>
        /// 顺序
        /// </summary>
        public int sequence { get; set; }
        /// <summary>
        /// 是否免费
        /// </summary>
        public string isfree { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public List<int> file_size { get; set; }
        /// <summary>
        /// 封面
        /// </summary>
        public string cover { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int content_type { get; set; }
        /// <summary>
        /// 播放量
        /// </summary>
        public string playcount { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
    }

    public class ChannelPage
    {
        /// <summary>
        /// 
        /// </summary>
        public ProgramAlbum album { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ProgramlistItem> plist { get; set; }
    }

    public class ProgramDetailFullData
    {
        /// <summary>
        /// 
        /// </summary>
        public ChannelPage channelPage { get; set; }
    }

    public class ProgramDetailFullRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public ProgramDetailFullData data { get; set; }

        public static ProgramAlbum GetAlbum(int cid, string qingting_id)
        {
            // {channelPage(cid:293411,page:1,order:\"asc\",qtId:\"a028518b208b447d9d8704cca240c8d4\"){plist}}
            string query = "{channelPage(cid: " + cid.ToString() + ", page: 1, order:\\\"asc\\\",qtId:\\\"" + qingting_id + "\\\"){album}}";
            string resultJson = HttpRequest.SendPost("https://webbff.qingting.fm/www", query, true);
            ProgramDetailFullRoot result = JsonConvert.DeserializeObject<ProgramDetailFullRoot>(resultJson);
            ProgramAlbum album = new ProgramAlbum();
            if (result == null) return album;
            album = result.data.channelPage.album;
            return album;
        } 

        public static List<ProgramlistItem> GetLists(int cid, string qingting_id, int page)
        {
            string query = "{channelPage(cid: " + cid.ToString() + ", page: " + page.ToString() + ", order:\\\"asc\\\",qtId:\\\"" + qingting_id + "\\\"){plist}}";
            string resultJson = HttpRequest.SendPost("https://webbff.qingting.fm/www", query, true);
            ProgramDetailFullRoot result = JsonConvert.DeserializeObject<ProgramDetailFullRoot>(resultJson);
            List<ProgramlistItem> list = new List<ProgramlistItem>();
            if (result == null) return list;
            list = result.data.channelPage.plist;
            return list;
        }
    }

}
