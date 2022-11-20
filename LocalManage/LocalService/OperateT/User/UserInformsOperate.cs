using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalRadioManage.DataModelsL;
using PureRadio.LocalRadioManage.DataModelTransform;
using PureRadio.LocalRadioManage.DBBuilder.TableObj;
using LocalRadioManage.DBBuilder;

namespace PureRadio.LocalRadioManage.LocalService.Service
{
    class UserInformsOperate
    {
        public readonly string TableName = UserInforms.TableName;
        private List<string> SelectedCol = new List<string>();

        public UserInformsOperate()
        {
            SelectedCol = SQLiteConnect.TableHandle.GetColNames(TableName).ToList();
        }

        public bool SaveUserInfo(UserInfo user)
        {
            List<object> store = UserTransform.InfoToStore(user);
            return SQLiteConnect.TableHandle.AddRecord(TableName, store);
        }

        public bool DeleteUserInfo(UserInfo user)
        {
            string ConditionExpress =UserInforms.UserNumber[0]
                + "=" + "'" + user.PhoneNumber + "'";
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
