using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using LocalRadioManage.DBBuilder.TableObj;
using LocalRadioManage.LocalService.UserInforms;
using LocalRadioManage.StorageOperate;
using Windows.Storage;

namespace LocalRadioManage.LocalService
{
    class ServiceUser
    {
        UserInform user_inform = new UserInform();
        //刻意存储用户
        public bool SaveUser(string user_name)
        {
            return user_inform.SaveUser(user_name);
        }
        public bool SaveUser(string user_name, string user_pass)
        {
            List<object> user = new List<object>() { user_name, user_pass, "" };
            return user_inform.SaveUser(user);
        }
        public async Task<bool> SaveUser(LocalUserInform user)
        {
            try
            {
                StorageFile img_file = await MyFile.CreateFile(Default.DefalutStorage.image_folder, user.user_icon);
                user.user_icon = new Uri(img_file.Path);
                return user_inform.SaveUser(user);
            }
            catch
            {
                return false;
            }
        }

        //删除一个用户的所有信息
        public async Task<bool> DeleteUser(string user_name)
        {
            DataModels.LocalUserInform user = LoadUser(user_name);
            if (user == null)
            {
                return false;
            }
            await MyFile.DeleteFile(user.user_icon);

            ServiceUserDown userDown = new ServiceUserDown();
            ServiceUserFav userFav = new ServiceUserFav();
            
            await userDown.DeleteProgram(user_name, true);
            userFav.DeleteProgram(user_name, true);

            user_inform.SetUserInform(user_name);
            return user_inform.DeleteUsr(true);
        }

        public List<LocalUserInform> LoadUser()
        {
            List<List<object>> users = user_inform.LoadUser();
            List<LocalUserInform> users_ret = new List<LocalUserInform>();
            foreach (List<object> user in users)
            {
                LocalUserInform inform = DataModelTransform.LocalUserTransform.ToLocalUserInform(user);
                users_ret.Add(inform);
            }
            return users_ret;
        }

        public LocalUserInform LoadUser(string user_name)
        {
            user_inform.SetUserInform(user_name);
            List<LocalUserInform> users= LoadUser();
            if (users.Count == 1)
            {
                return users[0];
            }
            else
            {
                return null;
            }
        }

        public bool CheckUsr(string user_name, string user_pass)
        {
            user_inform.SetUserInform(user_name, user_pass);
            return (user_inform.LoadUser() != null);
        }

        public bool UpdateUsr(string user_name, string old_pass, string new_pass)
        {
            if (CheckUsr(user_name, old_pass))
            {
                return user_inform.UpdateUsr(user_name, old_pass, new_pass);
            }
            else
            {
                return false;
            }
        }
    }
}

