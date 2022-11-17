using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using LocalRadioManage.DBBuilder.TableObj;

namespace LocalRadioManage.DataModelTransform
{
    public static class LocalUserTransform
    {
        public static List<object> ToLocalUserStorage(LocalUserInform user)
        {
            object[] local_store = new object[Users.ColLocation.Count];
            try
            {
                local_store[Users.ColLocation[Users.UserName]] = user.user_name;
                local_store[Users.ColLocation[Users.UserPass]] = user.user_pass;
                local_store[Users.ColLocation[Users.UserIcon]] = user.user_icon.ToString();
                local_store[Users.ColLocation[Users.UserTrueName]] = user.user_true_name;
            }
            catch
            {
                return null;
            }

            return local_store.ToList();
        }

        public static LocalUserInform ToLocalUserInform(List<object> local_store)
        {
            try
            {
                LocalUserInform user = new LocalUserInform();
                user.user_name = (string)local_store[Users.ColLocation[Users.UserName]];
                user.user_pass = (string)local_store[Users.ColLocation[Users.UserPass]];
                user.user_icon = new Uri((string)local_store[Users.ColLocation[Users.UserIcon]]);
                user.user_true_name = (string)local_store[Users.ColLocation[Users.UserTrueName]];
                return user;
            }
            catch
            {
                return null;
            }
        }
    }
}
