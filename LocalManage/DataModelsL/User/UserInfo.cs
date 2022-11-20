using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.LocalRadioManage.DataModelsL
{
    class UserInfo
    {

        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        ///// 用户Id
        ///// </summary>
        //public string QingtingId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; } = "default";
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = "default";
        /// <summary>
        /// 账号创建时间
        /// </summary>
        public string CreateTime { get; set; } = "default";
        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; } = "default";
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; } = "default";
        /// <summary>
        /// 地区
        /// </summary>
        public string Location { get; set; } = "default";
        /// <summary>
        /// 简介
        /// </summary>
        public string Signature { get; set; } = "default";
        /// <summary>
        /// 头像
        /// </summary>
        public Uri Avatar { get; set; }

        public Uri LocalAvatar { get; set; }

        public string UserPass { get; set; } = "default";

    }
}
