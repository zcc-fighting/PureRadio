using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalManage.DataModelsL;
using PureRadio.LocalManage.DataModelTransform;
using PureRadio.LocalManage.DBBuilder.TableObj;
using PureRadio.LocalManage.Iterfaces;
using PureRadio.LocalManage.LocalService.Storage;
using LocalRadioManage.DBBuilder;
using Windows.Storage;

namespace PureRadio.LocalManage.LocalService.Service
{
  public class UserInformsOperate:IUserInformOperate
    {
        public readonly string TableName = UserInforms.TableName;
        private List<string> SelectedCol = new List<string>();
        ImgStorage ImageS = new ImgStorage();
        public UserInformsOperate()
        {
            SQLiteConnect.CreateLocalRadioManage();
            SelectedCol = SQLiteConnect.TableHandle.GetColNames(TableName).ToList();
        }

        public bool SaveUserInfo(UserInfo user)
        {
            
            Task<Task<StorageFile>> task=new Task<Task<StorageFile>>(()=> ImageS.DownImage(user.Avatar, user.Avatar));
            task.Start();
            task.Result.Wait();
            StorageFile img = task.Result.Result;
            user.LocalAvatar = new Uri(img.Path);
            List<object> store = UserTransform.InfoToStore(user);
            UpdateUserInfo(user);
            return SQLiteConnect.TableHandle.AddRecord(TableName, store);
        }

        public bool DeleteUserInfo(UserInfo user)
        {
            string ConditionExpress =UserInforms.UserNumber[0]
                + "=" + "'" + user.PhoneNumber + "'";
            if (user.LocalAvatar != null)
            {
                Task<Task<bool>> task = new Task<Task<bool>>(() => ImageS.RemoveImage(user.LocalAvatar));
                task.Start();
            }
            return SQLiteConnect.TableHandle.DeleteRecords(TableName, ConditionExpress, true);
        }

        public bool UpdateUserInfo(UserInfo user)
        {
            string ConditionExpress =  UserInforms.UserNumber[0]
                + "=" + "'" + user.PhoneNumber + "'";
            return SQLiteConnect.TableHandle.UpdateRecord(TableName, ConditionExpress, UserTransform.InfoToStore(user));
        }

        public List<UserInfo> SelectUserInfo()
        {
            return SelectUserInfo("");
        }

        public List<UserInfo> SelectUserInfo(string user_name,bool no_need)
        {
            string ConditionExpress =UserInforms.UserNumber[0]
              + "=" + "'" + user_name + "'";
            return SelectUserInfo(ConditionExpress);
        }
        public List<UserInfo> SelectUserInfo(string user_name, string user_pass)
        {
            string ConditionExpress =  UserInforms.UserNumber[0]
              + "=" + "'" + user_name + "'"
              +  UserInforms.UserPass[0]
              +"=" + "'" + user_pass + "'";
            return SelectUserInfo(ConditionExpress);
        }

        public UserInfo SelectUserInfo(UserInfo user)
        {
            string ConditionExpress = UserInforms.UserNumber[0]
                + "=" +"'"+ user.PhoneNumber+"'";
            List<UserInfo> infos = SelectUserInfo(ConditionExpress);
            return infos.Count > 0? infos[0] :null;
        }

        public List<UserInfo> SelectUserInfo(string ConditionExpress)
        {
            List<List<object>> users = null;
            List<UserInfo> infos = new List<UserInfo>();
            SQLiteConnect.TableHandle.SelectRecords(TableName, SelectedCol, ConditionExpress, ref users);
            if (users == null)
            {
                return null;
            }
            foreach (List<object> user in users)
            {
                infos.Add(UserTransform.StoreToInfo(user));
            }
            return infos;
        }
    }
}
