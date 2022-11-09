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

namespace PureRadio.ViewModels
{
    public class AppViewModel : ObservableRecipient
    {
        private ElementTheme _theme;
        private ThreadPoolTimer _timer;
        private TimeSpan _delay;
        private bool _timerOn = false;
        private string _closeTime;
        private int _idNP;
        private bool _isFavNP;
        private bool _haveNP;

        public UserProfile UserInfo { get; set; }
        private List<FavItem> FavCloud { get; set; }
        private List<FavItem> FavLocal { get; set; }
        //private List<FavItem> RecentListen { get; set; }

        public AppViewModel()
        {
            _theme = App.RootTheme;
            //UserInfo = UserProfile.GetAccount();
            UserProfile user = UserProfile.GetAccount();
            if (user == null) UserInfo = UserProfile.Login();
            else UserInfo = user;
            UpdateFavAsync();
            IsActive = true;            
        }

        public ElementTheme Theme
        {
            get => _theme;
            set
            {
                SetProperty(ref _theme, value);
                UpdateTheme();
            }
                
        }

        public TimeSpan Delay
        {
            get => _delay;
            set
            {
                SetProperty(ref _delay, value);
            }
        }

        public bool TimerOn
        {
            get => _timerOn;
            set
            {
                SetProperty(ref _timerOn, value);                
            }
        }

        public string CloseTime
        {
            get
            {
                return  _closeTime;
            }
            set
            {
                SetProperty(ref _closeTime, value);
            }
        }

        public bool IsFavNP
        {
            get => _isFavNP;
            set
            {
                SetProperty(ref _isFavNP, value);
            }
        }

        protected override void OnActivated()
        {            
            Messenger.Register<AppViewModel, ThemeChangedMessage>(this, (r, m) => r.Theme= m.Value);
            Messenger.Register<AppViewModel, TimerChangedMessage>(this, (r, m) =>
            {
                r._delay = m.Value.delay;
                r.TimerOn = m.Value.isOn;
                r.CloseTime = m.Value.closeTime;
                CheckTimer();
            });
            Messenger.Register<AppViewModel, UserChangedMessage>(this, (r, m) =>
            {
                r.UserInfo = m.Value;
                UpdateFavAsync();
            });
            Messenger.Register<AppViewModel, FavAddMessage>(this, (r, m) =>
            {
                AddFav(m.Value);
            });
            Messenger.Register<AppViewModel, FavDelMessage>(this, (r, m) =>
            {
                DelFav(m.Value);
            });
            Messenger.Register<AppViewModel, PlayStartMessage>(this, (r, m) =>
            {
                _haveNP = true;
                r._idNP = m.Value;
                IsFavNP = r.CheckFav(r._idNP);
            });
            Messenger.Register<AppViewModel, PlayEndMessage>(this, (r, m) =>
            {
                _haveNP = false;
                r._idNP = 0;
                IsFavNP = false;
            });
            Messenger.Register<AppViewModel, TimerRequestMessage>(this, (r, m) => m.Reply(new TimerStatus(Delay, TimerOn, CloseTime)));
            Messenger.Register<AppViewModel, UserRequestMessage>(this, (r, m) => m.Reply(UserInfo));
            Messenger.Register<AppViewModel, FavCloudRequestMessage>(this, (r, m) => m.Reply(FavCloud));
            Messenger.Register<AppViewModel, FavLocalRequestMessage>(this, (r, m) => m.Reply(FavLocal));
            //Messenger.Register<AppViewModel, RecentRequestMessage>(this, (r, m) => m.Reply(RecentListen));
        }

        private void UpdateTheme()
        {
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            App.RootTheme = Theme;
            if (Theme == ElementTheme.Dark)
            {
                titleBar.ButtonForegroundColor = Colors.White;
            }
            else if (Theme == ElementTheme.Light)
            {
                titleBar.ButtonForegroundColor = Colors.Black;
            }
            else
            {
                if (Application.Current.RequestedTheme == ApplicationTheme.Dark)
                {
                    titleBar.ButtonForegroundColor = Colors.White;
                }
                else
                {
                    titleBar.ButtonForegroundColor = Colors.Black;
                }
            }
        }

        public void CreateTimer()
        {
            _timer = ThreadPoolTimer.CreateTimer(
                (timer) =>
                {
                    CoreApplication.Exit();
                },
                Delay);
            
        }

        public void CancelTimer()
        {
            _timer.Cancel();
        }

        public void CheckTimer()
        {
            if (_timer != null) CancelTimer();
            if (TimerOn == true)
            {
                CreateTimer();
            }
        }

        public async void UpdateFavAsync()
        {
            FavLocal = await FavResult.GetFav();
            if (!UserInfo.localAccount)
            {
                FavCloud = FavResult.GetFav(UserInfo.qingting_id, UserInfo.access_token);                
            }
            else
            {
                FavCloud = new List<FavItem>();
            }
            Messenger.Send(new FavUpdateMessage(true));
        }

        public bool CheckFav(int channelID)
        {
            return FavLocal.Contains(new FavItem(channelID));
        }

        public void AddFav(FavItem favItem)
        {
            if(!FavLocal.Contains(favItem)) FavLocal.Add(favItem);
            if (favItem.id == _idNP) IsFavNP = true;
            FavResult.SaveFav(FavLocal);
        }

        public void DelFav(int favItemID)
        {
            if (FavLocal.Remove(new FavItem(favItemID)))
            {
                FavResult.SaveFav(FavLocal);
                if (favItemID == _idNP) IsFavNP = false;
            }              
        }

        public void ToggleFav()
        {
            if (!_haveNP) return;
            if (IsFavNP)
            {
                DelFav(_idNP);
                //IsFavNP = false;
            }
            else
            {
                NowPlayingDetail nowPlaying = Messenger.Send<NowPlayingRequestMessage>();
                FavItem favItem = new FavItem()
                {
                    id = _idNP,
                    isRadio = nowPlaying.type == 2 ? false : true,
                    album_cover = nowPlaying.cover,
                    name = nowPlaying.title
                };
                AddFav(favItem);
                //IsFavNP = true;
            }
        }

    }

    public sealed class ThemeChangedMessage : ValueChangedMessage<ElementTheme>
    {
        public ThemeChangedMessage(ElementTheme value) : base(value)
        {
        }
    }

    public sealed class TimerChangedMessage : ValueChangedMessage<TimerStatus>
    {
        public TimerChangedMessage(TimerStatus value) : base(value)
        {
        }
    }
    public sealed class UserChangedMessage : ValueChangedMessage<UserProfile>
    {
        public UserChangedMessage(UserProfile value) : base(value)
        {
        }
    }

    public sealed class NavToRadioDetailMessage : ValueChangedMessage<RadioShot>
    {
        public NavToRadioDetailMessage(RadioShot value) : base(value)
        {
        }
    }

    public sealed class NavToProgramDetailMessage : ValueChangedMessage<ProgramShot>
    {
        public NavToProgramDetailMessage(ProgramShot value) : base(value)
        {
        }
    }

    public sealed class FavAddMessage : ValueChangedMessage<FavItem>
    {
        public FavAddMessage(FavItem value) : base(value)
        {
        }
    }
    public sealed class FavDelMessage : ValueChangedMessage<int>
    {
        public FavDelMessage(int value) : base(value)
        {
        }
    }
    public sealed class FavUpdateMessage : ValueChangedMessage<bool>
    {
        public FavUpdateMessage(bool value) : base(value)
        {
        }
    }
    public sealed class PlayStartMessage : ValueChangedMessage<int>
    {
        public PlayStartMessage(int value) : base(value)
        {
        }
    }

    public sealed class PlayEndMessage : ValueChangedMessage<int>
    {
        public PlayEndMessage(int value) : base(value)
        {
        }
    }



    public sealed class TimerRequestMessage : RequestMessage<TimerStatus>
    {
    }

    public sealed class UserRequestMessage : RequestMessage<UserProfile>
    {
    }

    public sealed class FavCloudRequestMessage : RequestMessage<List<FavItem>>
    {
    }
    public sealed class FavLocalRequestMessage : RequestMessage<List<FavItem>>
    {
    }
    /*
    public sealed class RecentRequestMessage : RequestMessage<List<FavItem>>
    {
    }
    */
    public sealed class NowPlayingRequestMessage : RequestMessage<NowPlayingDetail>
    {
    }

}
