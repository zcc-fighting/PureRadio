using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.LocalRadioManage.DataModelsL
{
  public  class AlbumCardInfo
    {
        /// <summary>
        /// 内容(专辑)封面图片(URL)
        /// </summary>
        public Uri Cover { get; set; }
        /// <summary>
        /// 内容(专辑)Id
        /// </summary>
        public int ContentId { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; } = "default";
        /// <summary>
        /// 内容(专辑)标题
        /// </summary>
        public string Title { get; set; } = "default";
        /// <summary>
        /// 内容(专辑)简介
        /// </summary>
        public string Description { get; set; } = "default";
        /// <summary>
        /// 内容(专辑)评分 (0 ~ 5)
        /// </summary>
        public float Rating { get; set; } = 0;
        /// <summary>
        /// 主播(可能有多个)
        /// </summary>
        public string Podcasters { get; set; } = "default";
        /// <summary>
        /// 所属分类Id
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 内容(专辑)分类("free" -> 免费, "channel-sale" -> 付费, "program-sale" -> 付费 )
        /// </summary>
        public string ContentType { get; set; } = "default";
        /// <summary>
        /// 内容(专辑)的属性
        /// </summary>


        public Uri LocalCover { get; set; }

       

    }
}
