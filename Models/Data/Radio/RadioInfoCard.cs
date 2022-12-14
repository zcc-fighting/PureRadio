using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Data.Radio
{
    /// <summary>
    /// 电台结果视图项
    /// </summary>
    public class RadioInfoCard
    {
        public RadioInfoCard(int radioId, int programId,string title, string programTitle,
            string cover,string description, string audienceCount, string nowplaying,
            TimeSpan updateTime)
        {
            RadioId = radioId;
            ProgramId = programId;
            Title = title;
            ProgramTitle = programTitle;
            Cover = new Uri(cover);
            Description = description;
            AudienceCount = audienceCount;
            Nowplaying = nowplaying;
            UpdateTime = updateTime;
        }

        /// <summary>
        /// 电台ID
        /// </summary>
        public int RadioId { get; set; }
        /// <summary>
        /// 节目ID
        /// </summary>
        public int ProgramId { get; set; }
        /// <summary>
        /// 电台标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 电台标题
        /// </summary>
        public string ProgramTitle { get; set; }
        /// <summary>
        /// 电台封面图片(URL)
        /// </summary>
        public Uri Cover { get; set; }
        /// <summary>
        /// 电台简介
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 电台听众计数
        /// </summary>
        public string AudienceCount { get; set; }
        /// <summary>
        /// 正在播放的节目
        /// </summary>
        public string Nowplaying { get; set; }
        /// <summary>
        /// 需更新时间
        /// </summary>
        public TimeSpan UpdateTime { get; set; }
    }
}
