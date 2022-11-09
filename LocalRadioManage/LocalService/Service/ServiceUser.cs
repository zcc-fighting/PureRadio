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
            List<object> user = new List<object>() { user_name, user_pass };
            return user_inform.SaveUser(user);
        }

        //删除一个用户的所有信息
        public async Task<bool> DeleteUser(string user_name)
        {
            ServiceUserDown userDown = new ServiceUserDown();
            ServiceUserFav userFav = new ServiceUserFav();
            await userDown.DeleteProgram(user_name, true);
            userFav.DeleteProgram(user_name, true);
            user_inform.SaveUser(user_name);
            return user_inform.DeleteUsr(true);
        }

        public List<List<object>> LoadUser()
        {
          return  user_inform.LoadUser();
        }
    }
}
