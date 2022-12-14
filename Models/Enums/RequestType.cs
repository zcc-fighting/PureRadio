using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Enums
{
    /// <summary>
    /// Http请求类型
    /// </summary>
    public enum RequestType
    {
        /// <summary>
        /// 默认方法
        /// </summary>
        Default,
        /// <summary>
        /// 登录(Post)
        /// </summary>
        Login,
        /// <summary>
        /// 认证(刷新Token)(Post)
        /// </summary>
        Auth,
        /// <summary>
        /// 搜索(Post GraphQL)
        /// </summary>
        Search,
        /// <summary>
        /// 电台推荐(Post GraphQL)
        /// </summary>
        RecommendRadio,
        /// <summary>
        /// 专辑分类推荐(Post GraphQL)
        /// </summary>
        RecommendContent,
        /// <summary>
        /// 播放点播内容(专辑内的节目)
        /// </summary>
        PlayContent,
        /// <summary>
        /// 主页电台推荐(Post GraphQL)
        /// </summary>
        HomeRadioRecommend,
    }
}
