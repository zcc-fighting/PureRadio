using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.Service;
using LocalRadioManage.DBBuilder;
using LocalRadioManage.DBBuilder.TableObj;


namespace LocalRadioManage.Service.UserInform
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
            table_name = Users.TableName;
            selected_col =  SQLiteConnect.TableHandle.GetColNames(table_name).ToList();
            condition_express = "";
        }
      public UserInform(List<object> user)
        {
            try
            {
                table_name = Users.TableName;
                selected_col = SQLiteConnect.TableHandle.GetColNames(table_name).ToList();
                condition_express = Users.UserName[0] + "=" + (string)user.ElementAt(Users.ColLocation[Users.UserName]);
            }
            catch
            {
                Exception mess = new Exception("UserInform Get incorrct paras");
                throw mess;
            }
        }


        /// <summary>
        /// 对用户表的访问控制
        /// </summary>
        /// <param name="user">"UserName","UserPass"</param>
        public bool SaveUser(List<object> user) 
        {
         ;
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
        public bool DeleteUsr()
        { 
            try
            {
              return SQLiteConnect.TableHandle.DeleteRecords(table_name,condition_express);
            }
            catch
            {
              return false;
            }
        }

        //保留自定义查询的权力
        public void SetConditionExpress(string express)
        {
            condition_express = express;
        }

        private List<List<object>> GetUserInforms()
        {
            return user_informs;
        }

        
    }
}
