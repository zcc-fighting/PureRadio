using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using PureRadio.DataModel;
using PureRadio.DataModel.Results;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.ViewModels
{
    public class UserViewModel : ObservableRecipient
    {
        public UserProfile UserInfo { get; set; }
        public ObservableCollection<FavItem> _favCloud = new ObservableCollection<FavItem>();
        public ObservableCollection<FavItem> _favLocal = new ObservableCollection<FavItem>();
        //public ObservableCollection<FavItem> _recentlyListen = new ObservableCollection<FavItem>();

        private bool _accountLoginButtonState;
        private bool _accountLogoutButtonState;
        private bool _favCloudEmptyReplace;
        private bool _favLocalEmptyReplace;
        //private bool _recentlyListenEmptyReplace;        

        private string _avatar;
        private string _nickname;
        private string _signature;

        public UserViewModel()
        {
            IsActive = true;
        }

        public ObservableCollection<FavItem> FavCloud
        {
            get => _favCloud;
            set
            {
                SetProperty(ref _favCloud, value);
            }
        }
        public ObservableCollection<FavItem> FavLocal
        {
            get => _favLocal;
            set
            {
                SetProperty(ref _favLocal, value);
            }
        }
        /*
        public ObservableCollection<FavItem> RecentlyListen
        {
            get => _recentlyListen;
            set
            {
                SetProperty(ref _recentlyListen, value);
            }
        }
        */

        protected override void OnActivated()
        {
            UserInfo = Messenger.Send<UserRequestMessage>();
            UpdateInfo();
            AccountLoginButtonState = UserInfo.localAccount;
            AccountLogoutButtonState = !AccountLoginButtonState;
        }



        public string Avatar
        {
            get => _avatar;
            set
            {
                SetProperty(ref _avatar, value);
            }
        }

        public string NickName
        {
            get => _nickname;
            set
            {
                SetProperty(ref _nickname, value);
            }
        }

        public string Signature
        {
            get => _signature;
            set
            {
                SetProperty(ref _signature, value);
            }
        }

        public bool AccountLoginButtonState
        {
            get => _accountLoginButtonState;
            set
            {
                SetProperty(ref _accountLoginButtonState, value);
            }
        }

        public bool AccountLogoutButtonState
        {
            get => _accountLogoutButtonState;
            set
            {
                SetProperty(ref _accountLogoutButtonState, value);
            }
        }
        public bool FavCloudEmptyReplace
        {
            get => _favCloudEmptyReplace;
            set
            {
                SetProperty(ref _favCloudEmptyReplace, value);
            }
        }

        public bool FavLocalEmptyReplace
        {
            get => _favLocalEmptyReplace;
            set
            {
                SetProperty(ref _favLocalEmptyReplace, value);
            }
        }

        /*
        public bool RecentlyListenEmptyReplace
        {
            get => _recentlyListenEmptyReplace;
            set
            {
                SetProperty(ref _recentlyListenEmptyReplace, value);
            }
        }
        */

        public bool Login(string phoneNum, string pwd, string area)
        {
            UserProfile user = UserProfile.Login(phoneNum, pwd, area);
            if (user != null)
            {
                UserInfo = user;
                Messenger.Send(new UserChangedMessage(UserInfo));
                UpdateInfo();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool LogOut()
        {
            UserProfile user = UserProfile.LogOut();
            if (user != null)
            {
                UserInfo = user;
                Messenger.Send(new UserChangedMessage(UserInfo));
                //FavCloud.Clear();
                UpdateInfo();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateInfo()
        {
            Avatar = UserInfo.avatar;
            NickName = UserInfo.nick_name;
            Signature = UserInfo.signature;
            AccountLoginButtonState = UserInfo.localAccount;
            AccountLogoutButtonState = !UserInfo.localAccount;
            List<FavItem> cloud = Messenger.Send<FavCloudRequestMessage>();
            List<FavItem> local = Messenger.Send<FavLocalRequestMessage>();
            //List<FavItem> recent = Messenger.Send<RecentRequestMessage>();
            if (cloud == null || cloud.Count == 0)
            {
                FavCloud = new ObservableCollection<FavItem>();
                FavCloudEmptyReplace = true;
            }
            else
            {
                FavCloud = new ObservableCollection<FavItem>(cloud);
                FavCloudEmptyReplace = false;
            }
            if (local == null || local.Count == 0)
            {
                FavLocal = new ObservableCollection<FavItem>();
                FavLocalEmptyReplace = true;
            }
            else
            {
                FavLocal = new ObservableCollection<FavItem>(local);
                FavLocalEmptyReplace = false;
            }
            /*
            if (recent == null || recent.Count == 0)
            {

                RecentlyListenEmptyReplace = true;
            }
            else
            {

                RecentlyListenEmptyReplace= false;
            }
            */
        }

    }
}
