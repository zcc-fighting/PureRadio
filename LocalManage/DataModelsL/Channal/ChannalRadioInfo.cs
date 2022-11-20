using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.LocalManage.DataModelsL
{
   public class ChannalRadioInfo
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; } = "default";
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; } = "default";
        /// <summary>
        /// 节目时长
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 节目所属电台的ID
        /// </summary>
        public int RadioId { get; set; }
        /// <summary>
        /// 节目的ID
        /// </summary>
        public int ProgramId { get; set; }
        /// <summary>
        /// 节目标题
        /// </summary>
        public string Title { get; set; } = "default";
        /// <summary>
        /// 主播(可能有多个)
        /// </summary>
        public string Broadcasters { get; set; } = "default";

        public Uri LocalUri { get; set; }

        public Uri RemoteUri { get; set; }

    }
}
