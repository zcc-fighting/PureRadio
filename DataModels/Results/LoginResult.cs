using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel.Results
{
    public class LoginResult
    {
        /// <summary>
        /// 错误码(0为无错)
        /// </summary>
        public int errorno { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string errormsg { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public UserProfile data { get; set; }
    }

}
