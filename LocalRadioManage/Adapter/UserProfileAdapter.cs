using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using PureRadio.DataModel;

namespace PureRadio.LocalRadioManage.Adapter
{
    public static class UserProfileAdapter
    {
        public static DataModels.LocalUserInform UserProfileToUserInform(UserProfile user_pro)
        {
            DataModels.LocalUserInform user_inform = new LocalUserInform();
            user_inform.user_name = user_pro.phone_number;
            user_inform.user_true_name = user_pro.userName;
            user_inform.user_pass = "0";//没有密码
            user_inform.user_icon = new Uri(user_pro.avatar);
            return user_inform;
        }

        public static UserProfile UserInformToUserProfile(DataModels.LocalUserInform local)
        {
            UserProfile user_pro = new UserProfile();
            user_pro.userName = local.user_true_name;
            user_pro.phone_number = local.user_name;
            user_pro.avatar = local.user_icon.AbsolutePath;
            return user_pro;
        }

    }
}