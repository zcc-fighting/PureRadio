using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalManage.DataModelsL;

namespace PureRadio.LocalManage.Iterfaces
{
    interface IUserInformOperate
    {
        /// <summary>
        /// 存入用户信息
        /// </summary>
        bool SaveUserInfo(UserInfo user);
        /// <summary>
        /// 删除用户信息
        /// </summary>
        bool DeleteUserInfo(UserInfo user);
        /// <summary>
        /// 更新用户信息
        /// </summary>
        bool UpdateUserInfo(UserInfo user);
        /// <summary>
        /// 选取所有用户信息
        /// </summary>
         List<UserInfo> SelectUserInfo();
        /// <summary>
        /// 按电话号码选取用户信息
        /// </summary>
         List<UserInfo> SelectUserInfo(string UserPhoneNumer, bool no_need);
        /// <summary>
        /// 按电话号码与密码
        /// </summary>
         List<UserInfo> SelectUserInfo(string user_name, string user_pass);

         UserInfo SelectUserInfo(UserInfo user);
        /// <summary>
        /// 构建条件表达式选取
        /// </summary>
          List<UserInfo> SelectUserInfo(string ConditionExpress);
    }

   

}

