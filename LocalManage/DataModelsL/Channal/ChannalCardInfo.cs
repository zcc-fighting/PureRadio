using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.LocalRadioManage.DataModelsL
{
   public class ChannalCardInfo
    {
        /// <summary>
        /// 电台ID
        /// </summary>
        public int RadioId { get; set; }
        /// <summary>
        /// 电台标题
        /// </summary>
        public string Title { get; set; } = "default";
        /// <summary>
        /// 电台封面图片(URL)
        /// </summary>
        public Uri Cover { get; set; }
        /// <summary>
        /// 电台简介
        /// </summary>
        public string Description { get; set; } = "default";
        /// <summary>
        /// 所属分类Id
        /// </summary>
        public int TopCategoryId { get; set; }
        /// <summary>
        /// 所属分类标题
        /// </summary>
        public string TopCategoryTitle { get; set; } = "default";
        /// <summary>
        /// 所属地区Id
        /// </summary>
        public int RegionId { get; set; }
        /// <summary>
        /// 所属城市Id
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// 需更新时间
        /// </summary>
        public string UpdateTime { get; set; } = "default";

        public Uri LocalCover { get; set; }


    }
}
