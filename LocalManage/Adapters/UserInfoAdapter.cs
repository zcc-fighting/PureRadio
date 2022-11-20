using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.Uwp.Models.Data.User;
using PureRadio.LocalManage.DataModelsL;

namespace PureRadio.LocalManage.Adapters
{
    class UserInfoAdapter
    {
      public static  AccountInfo ToAccountInfo(UserInfo user )
        {
            AccountInfo account = new AccountInfo();
            account.Avatar = user.Avatar;
            account.Birthday = user.Birthday;
            account.CreateTime = user.CreateTime;
            account.Gender = user.Gender;
            account.Location = user.Location;
            account.NickName = user.NickName;
            account.PhoneNumber = user.PhoneNumber;
            account.Signature = user.Signature;
            account.UserName = user.UserName;
            return account;
        }

        public static UserInfo ToUserInfo(AccountInfo account)
        {
            UserInfo user = new UserInfo();
             user.Avatar=account.Avatar ;
            user.Birthday= account.Birthday ;
            user.CreateTime= account.CreateTime ;
            user.Gender= account.Gender;
           user.Location=  account.Location ;
             user.NickName= account.NickName;
            user.PhoneNumber=  account.PhoneNumber;
            user.Signature = account.Signature;
            user.UserName= account.UserName ;
            return user;
        }
    }
}
