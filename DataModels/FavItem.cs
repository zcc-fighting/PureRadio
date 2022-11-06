using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel
{
    public class FavItem
    {
        /// <summary>
        /// 项目属性(true -> 电台, false -> 节目)
        /// </summary>
        public bool isRadio { get; set; } = false;
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; } = 0;
        /// <summary>
        /// 封面
        /// </summary>
        public string album_cover { get; set; } = "/Assets/Image/DefaultCover.png";
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; } = string.Empty;
        /// <summary>
        /// 分类
        /// </summary>
        public int catid { get; set; } = 0;

        public override bool Equals(object obj)
        {
            return id.Equals((obj as FavItem).id);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public FavItem()
        {
        }

        public FavItem(int id)
        {
            this.id = id;
        }
    }
}
