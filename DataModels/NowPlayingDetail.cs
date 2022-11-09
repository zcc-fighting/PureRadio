using Newtonsoft.Json;
using PureRadio.Common;
using PureRadio.DataModel.Message;
using PureRadio.DataModel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel
{
    public class NowPlayingDetail
    {
        /// <summary>
        /// 电台id/专辑id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 电台名称/节目名称
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 封面
        /// </summary>
        public string cover { get; set; }
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
        /// 类型(0 -> 电台直播/1 -> 电台回放/2 -> 点播节目)
        /// </summary>
        public int type { get; set; }

        public NowplayingRadio radio { get; set; }

        public string program_title { get; set; }

        public static NowPlayingDetail GetNowplayingRadio(int id)
        {
            string resultJson = HttpRequest.SendGet("http://rapi.qingting.fm/channels/" + id);
            RadioDetailSmallResult result = JsonConvert.DeserializeObject<RadioDetailSmallResult>(resultJson);
            if (result == null || result.Data == null) return null;
            NowPlayingDetail playingDetail = new NowPlayingDetail();
            playingDetail.radio = result.Data.nowplaying;
            playingDetail.id = result.Data.id;
            playingDetail.title = result.Data.title;
            playingDetail.cover = result.Data.cover;
            playingDetail.duration = result.Data.nowplaying.duration;
            playingDetail.start_time = result.Data.nowplaying.start_time;
            playingDetail.end_time = result.Data.nowplaying.end_time;
            playingDetail.type = 0;
            return playingDetail;
        }
        
        public static NowPlayingDetail GetNowPlayingProgram(ProgramDemandMessage program)
        {
            NowPlayingDetail playingDetail = new NowPlayingDetail();
            playingDetail.program_title = program.title;
            playingDetail.id = program.album_id;
            playingDetail.title = program.album;
            playingDetail.cover = program.cover;
            playingDetail.type = 2;
            return playingDetail;
        }
    }
}
