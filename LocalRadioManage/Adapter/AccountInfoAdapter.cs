using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using PureRadio.Uwp.Models.Data.User;

namespace PureRadio.LocalRadioManage.Adapter
{
    public static class AccountInfoAdapter
    {
        public static DataModels.LocalUserInform AccountInfoToUserInform(AccountInfo user_pro)
        {
            DataModels.LocalUserInform user_inform = new LocalUserInform();
            user_inform.user_name = user_pro.PhoneNumber;
            user_inform.user_true_name = user_pro.UserName;
            user_inform.user_pass = "0";//没有密码
            user_inform.user_icon = user_pro.Avatar;
            return user_inform;
        }

        public static AccountInfo UserInformToAccountInfo(DataModels.LocalUserInform local)
        {
            AccountInfo user_pro = new AccountInfo();
            user_pro.UserName = local.user_true_name;
            user_pro.PhoneNumber = local.user_name;
            user_pro.Avatar = new Uri(local.user_icon.AbsolutePath);
            return user_pro;
        }

    }
}