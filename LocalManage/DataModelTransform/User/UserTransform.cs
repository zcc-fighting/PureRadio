using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalRadioManage.DataModelsL;
using PureRadio.LocalRadioManage.DBBuilder.TableObj;

namespace PureRadio.LocalRadioManage.DataModelTransform
{
    class UserTransform
    {
        public static List<object> InfoToStore(UserInfo info)
        {
            object[] store = new object[UserInforms.ColLocation.Count];
            store[UserInforms.ColLocation[UserInforms.UserName]] = info.UserName;
            store[UserInforms.ColLocation[UserInforms.Birthday]] = info.Birthday;
            store[UserInforms.ColLocation[UserInforms.CreateTime]] = info.CreateTime;
            store[UserInforms.ColLocation[UserInforms.Gender]] = info.Gender;
            store[UserInforms.ColLocation[UserInforms.Location]] = info.Location;
            store[UserInforms.ColLocation[UserInforms.Signature]] = info.Signature;
            if (info.Avatar != null)
            {
                store[UserInforms.ColLocation[UserInforms.UserIcon]] = info.Avatar.AbsolutePath;
            }
            store[UserInforms.ColLocation[UserInforms.UserNickName]] = info.NickName;
            store[UserInforms.ColLocation[UserInforms.UserNumber]] = info.PhoneNumber;
            store[UserInforms.ColLocation[UserInforms.UserPass]] = info.UserPass;
            if (info.LocalAvatar != null)
            {
                store[UserInforms.ColLocation[UserInforms.LocalUserIcon]] = info.LocalAvatar.AbsolutePath;
            }
            return store.ToList();
        }

        public static UserInfo StoreToInfo(List<object> store)
        {
            UserInfo info = new UserInfo();
            try
            {
                info.UserName = (string)store[UserInforms.ColLocation[UserInforms.UserName]];
            }
            catch{}
            try
            {
                info.Birthday = (string)store[UserInforms.ColLocation[UserInforms.Birthday]];
            }
            catch{}
            try
            {
                info.CreateTime = (string)store[UserInforms.ColLocation[UserInforms.CreateTime]];
            }
            catch { }
            try
            {
                info.Gender = (string)store[UserInforms.ColLocation[UserInforms.Gender]];
            }
            catch { }
            try
            {
                info.Location = (string)store[UserInforms.ColLocation[UserInforms.Location]];
            }
            catch { }
            try
            {
                info.Signature = (string)store[UserInforms.ColLocation[UserInforms.Signature]];
            }
            catch { }
            try
            {
                info.Avatar = new Uri((string)store[UserInforms.ColLocation[UserInforms.UserIcon]]);
            }
            catch { }
            try
            {
                info.NickName = (string)store[UserInforms.ColLocation[UserInforms.UserNickName]];
            }
            catch { }
            try
            {
                info.PhoneNumber = (string)store[UserInforms.ColLocation[UserInforms.UserNumber]];
            }
            catch { }
            try
            {
                info.UserPass = (string)store[UserInforms.ColLocation[UserInforms.UserPass]];
            }
            catch { }
            try
            {
                if ((string)store[UserInforms.ColLocation[UserInforms.LocalUserIcon]] != null)
                {
                    info.LocalAvatar = new Uri((string)store[UserInforms.ColLocation[UserInforms.LocalUserIcon]]);
                }
            }
            catch { }
            return info;
        }
    }
}
