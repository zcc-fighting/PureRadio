using PureRadio.DataModel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.Common;
using Newtonsoft.Json;
using Windows.Storage;

namespace PureRadio.DataModel
{
    public class UserProfile
    {
        /// <summary>
        /// 是否为本地账户
        /// </summary>
        public bool localAccount { get; set; }
        /// <summary>
        /// 账号id
        /// </summary>
        public string qingting_id { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string nick_name { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string phone_number { get; set; }
        /// <summary>
        /// 国际区号
        /// </summary>
        public string area_code { get; set; }
        /// <summary>
        /// 性别(m -> 男 f-> 女 u-> 保密)
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        /// 个性签名
        /// </summary>
        public string signature { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// 设备id
        /// </summary>
        public string device_id { get; set; }
        /// <summary>
        /// 访问token
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 刷新token
        /// </summary>
        public string refresh_token { get; set; }
        /// <summary>
        /// token超时时间
        /// </summary>
        public int expires_in { get; set; }

        public static UserProfile GetAccount()
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values["AccountState"] != null && localSettings.Values["UserName"] != null && localSettings.Values["Password"] != null)
            {
                string accountLocal = localSettings.Values["AccountState"].ToString();
                if(accountLocal == "Online")
                {
                    string phoneNum = localSettings.Values["UserName"].ToString();
                    string pwd = localSettings.Values["Password"].ToString();
                    string area = "+86";
                    UserProfile user = Login(phoneNum, pwd, area);
                    if (user != null) return user;
                    else return Login();
                }                
            }
            return Login();
        }

        public static void SaveAccount(bool accountLocal, string phoneNum, string pwd)
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["AccountState"] = accountLocal ? "Local" : "Online";
            localSettings.Values["UserName"] = phoneNum;
            localSettings.Values["Password"] = pwd;
        }

        public static UserProfile Login(string phoneNum, string pwd, string area)
        {            
            string requestBody = "{\"account_type\": \"5\",\"device_id\": \"web\",\"user_id\": \"" + phoneNum + "\",\"password\": \"" + pwd + "\",\"area_code\": \"" + area + "\"}";
            string resultJson = HttpRequest.SendPost("https://user.qingting.fm/u2/api/v4/user/login", requestBody);
            LoginResult result = JsonConvert.DeserializeObject<LoginResult>(resultJson);
            if( result == null || result.errorno != 0 || result.data == null) return null;
            result.data.localAccount = false;
            SaveAccount(false, phoneNum, pwd);
            return result.data;
        }

        public static UserProfile Login()
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            UserProfile localProfile = new UserProfile();
            localProfile.localAccount = true;
            localProfile.nick_name = resourceLoader.GetString("UserDefaultName");
            localProfile.signature = resourceLoader.GetString("UserDefaultSignature");
            localProfile.device_id = "web";
            localProfile.avatar = "/Assets/Image/DefaultAvatar.png";
            localProfile.userName = localProfile.nick_name;
            localProfile.area_code = "+86";
            localProfile.gender = "u";
            localProfile.expires_in = 0;
            localProfile.qingting_id = localProfile.phone_number = localProfile.access_token = localProfile.refresh_token = string.Empty;
            return localProfile;
        }

        public static UserProfile LogOut()
        {
            SaveAccount(true, string.Empty, string.Empty);
            return Login();
        }

    }
}
