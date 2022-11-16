using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.LocalService;
using LocalRadioManage.DBBuilder;
using LocalRadioManage.DBBuilder.TableObj;
using DataModels;
using LocalRadioManage.DataModelTransform;


namespace LocalRadioManage.LocalService.UserInforms
{
    public partial class UserInform
    {
        /// <summary>
        /// 根据数据库表结构,以下两为user的关联表
        /// </summary>
        public UserDown UserDown = new UserDown();
        public UserFav UserFav = new UserFav();

        /// <summary>
        /// 查询用户表参数
        /// </summary>
        private string table_name = "";
        private List<string> selected_col = null;
        private string condition_express = "";

        /// <summary>
        /// 获取(单)用户数据
        /// </summary>
        private List<List<object>> user_informs = new List<List<object>>();
    }

    public partial class UserInform
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public UserInform()
        {
            SetUserInform();
        }
        public UserInform(string user)
        {
            SetUserInform(user);
        }
        public UserInform(List<object> user)
        {
            SetUserInform(user);
        }
        public UserInform(RadioFullAlbum album)
        {
            SetUserInform(album);
        }

        public UserInform(LocalUserInform user)
        {
            SetUserInform(user);
        }


        private void SetUserInform()
        {
            table_name = Users.TableName;
            selected_col = SQLiteConnect.TableHandle.GetColNames(table_name).ToList();
            condition_express = "";
        }
        public bool SetUserInform(string user)
        {
            try
            {
                SetUserInform();
                condition_express = Users.UserName[0] + "=" + user;
                UserDown.SetUserDown(user);
                UserFav.SetUserFav(user);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public bool SetUserInform(string user,string user_pass)
        {
            try
            {
                SetUserInform();
                List<object> user_l = new List<object>() { user, user_pass };
                condition_express = Users.UserName[0] + "=" + user
                    + " and " + Users.UserPass[0] + "=" + user_pass;
                UserDown.SetUserDown(user);
                UserFav.SetUserFav(user);
                return true;
            }
            catch
            {
                return false;
            }
           
        }

        public bool SetUserInform(LocalUserInform user)
        {
            try
            {
                SetUserInform(user.user_name,user.user_pass);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SetUserInform(List<object> user)
        {
            try
            {
                SetUserInform();
                condition_express = Users.UserName[0] + "=" + (string)(user.ElementAt(Users.ColLocation[Users.UserName]))
                 +" and "+Users.UserPass[0]+"="+(string)(user.ElementAt(Users.ColLocation[Users.UserPass]));
                UserDown.SetUserDown((string)user[0]);
                UserFav.SetUserFav((string)user[0]);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public void SetUserInform(RadioFullAlbum album)
        {
            SetUserInform();
            condition_express = Users.UserName[0] + "=" + album.user;
            UserDown.SetUserDown(album);
            UserFav.SetUserFav(album);
        }

    }

    public partial class UserInform
    {
        /// <summary>
        /// 对用户表的访问控制
        /// </summary>
        /// <param name="user">"UserName","UserPass"</param>
        
        public bool SaveUser(LocalUserInform user)
        {
            List<object> store = LocalUserTransform.ToLocalUserStorage(user);
            try
            {
                SQLiteConnect.TableHandle.AddRecord(table_name, store);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SaveUser(List<object> user)
        {
            try
            {
                SQLiteConnect.TableHandle.AddRecord(table_name, user);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool SaveUser(string user_name)
        {
            List<object> user = new List<object>() { user_name, "0","" };
            try
            {
                return SQLiteConnect.TableHandle.AddRecord(table_name, user);
            }
            catch
            {
                return false;
            }
        }
        public List<List<object>> LoadUser()
        {
            try
            {
                SQLiteConnect.TableHandle.SelectRecords(table_name, selected_col, condition_express, ref user_informs);
                return GetUserInforms();
            }
            catch
            {
                return null;
            }

        }
        public bool DeleteUsr(bool is_constraint)
        {
            try
            {
                return SQLiteConnect.TableHandle.DeleteRecords(table_name, condition_express,is_constraint);
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateUsr(string user_name,string old_pass,string new_pass)
        {
            SetUserInform(user_name, old_pass);
            List<object> new_record = new List<object>() { user_name, new_pass };
            return SQLiteConnect.TableHandle.UpdateRecord(table_name,condition_express,new_record);
        }

        //自定义查询
        public void SetConditionExpress(string express)
        {
            condition_express = express;
        }

        private List<List<object>> GetUserInforms()
        {
            if (user_informs.Count > 0)
            {
                return user_informs;
            }
            else
            {
                return null;
            }
        }

    }
}
