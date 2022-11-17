using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{

    //用于音频索引存储
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
        /// 当前栏目id
        /// </summary>
        public int program_id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime date { get; set; }


        public string user = "0";
        public Uri radio_uri { get; set; }
    }
    //用于电台索引存储
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
        public Uri cover { get; set; }
        /// <summary>
        /// 介绍
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 当前播放
        /// </summary>
        //public RadioFullNowplaying nowplaying { get; set; }
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
        /// <summary>
        /// 类型 电台->0/专辑->1
        /// </summary>
        public int type { get; set; }

        public string user = "0";

    }


    //用户信息
    public class LocalUserInform
    {
        //此对应用户名
        public string user_true_name = "default";
        //user_name对应蜻蜓id
        public string user_name = "0";
        public string user_pass = "0";
        public Uri user_icon = null;
    }

    public class DownProgressInform
    {
        string file_name ="";
        ulong file_size = 0;
        ulong down_size = 0;
        bool down_end = false;

        public DownProgressInform()
        {

        }
        public DownProgressInform(string _file_name,ulong _file_size,ulong _down_size,bool _down_end)
        {
            file_name = _file_name;
            file_size = _file_size;
            down_size = _down_size;
            down_end = _down_end;
        }
    }

}
