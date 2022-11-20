using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.LocalManage.DataModelsL
{
   public class AlbumRadioInfo
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; } = "default";
        /// <summary>
        /// 节目Id
        /// </summary>
        public int ProgramId { get; set; }

        public int AlbumId { get; set; }
        /// <summary>
        /// 节目标题
        /// </summary>
        public string Title { get; set; } = "default";

        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpdateTime { get; set; } = "default";

        /// <summary>
        /// 节目类型(未知)
        /// </summary>
        public string ContentType { get; set; } = "default";

        public Uri LocalUri { get; set; }

        public Uri RemoteUri { get; set; }
    }
}
