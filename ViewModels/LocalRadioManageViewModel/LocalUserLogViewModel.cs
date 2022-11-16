using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using PureRadio.DataModel;
using PureRadio.DataModel.Parameter;
using PureRadio.DataModel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.System.Threading;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using DataModels;
using LocalRadioManage.LocalService;

namespace PureRadio.ViewModels.LocalRadioManageViewModels
{
    class LocalUserLogViewModel : ObservableRecipient
    {
        private LocalServ.LocalUser UserMan = new LocalServ.LocalUser();
        private LocalServ.LocalDown DownMan = new LocalServ.LocalDown();
        private LocalServ.LocalFav FavMan = new LocalServ.LocalFav();

        //用户账号/密码/图标信息
        private LocalUserInform user_local { get; set; } = new LocalUserInform();
        public UserProfile user_view { get; set; }

        //用户所有下载专辑
        private List<DataModels.RadioFullAlbum> user_down_albums { get; set; } = new List<DataModels.RadioFullAlbum>();
        //用户所有收藏专辑
        private List<DataModels.RadioFullAlbum> user_fav_albums { get; set; } = new List<DataModels.RadioFullAlbum>();


        //登录用户
        public bool LoginUser()
        {
            user_local = UserMan.LoadUser("0", "0", false);
            user_view = LocalRadioManage.Adapter.UserProfileAdapter.UserInformToUserProfile(user_local);
            GetUserAlbums();
            return UserMan.CheckUsr(user_local.user_name, user_local.user_pass);
        }
        public bool LoginUser(string user_name, string user_pass)
        {
            user_local = UserMan.LoadUser(user_name, user_pass, false);
            user_view = LocalRadioManage.Adapter.UserProfileAdapter.UserInformToUserProfile(user_local);
            GetUserAlbums();
            return UserMan.CheckUsr(user_local.user_name, user_local.user_pass);
        }
        //注册用户
        public bool CreateUser(string user_name, string user_pass)
        {
            bool flag = UserMan.SaveUser(user_name, user_pass);
            if (!flag)
            {
                return false;
            }
            LoginUser(user_name, user_pass);
            return flag;
        }
        public bool CreateUser(UserProfile _user)
        {
            LocalUserInform user = LocalRadioManage.Adapter.UserProfileAdapter.UserProfileToUserInform(_user);
            bool flag = UserMan.SaveUser(user);
            if (!flag)
            {
                return false;
            }
            LoginUser(user.user_name, user.user_pass);
            return flag;
        }

        //获取用户专辑
        public void GetUserAlbums()
        {
            user_down_albums = DownMan.Load(user_local.user_name);
            user_fav_albums = FavMan.Load(user_local.user_name);
        }


    }
}
