using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using PureRadio.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.System.Threading;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace PureRadio.ViewModels
{
    public class SettingsViewModel : ObservableRecipient
    {
        private ElementTheme _theme;
        private string _language;
        private bool _timerStatus = false;
        private TimeSpan _delay;
        private string _closeTime;
        private bool _networkStatus = false;


        public SettingsViewModel()
        {
            IsActive = true;                       
        }

        protected override void OnActivated()
        {
            _language = (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["CurrentLanguage"];
            _theme = App.RootTheme;
            TimerStatus timerStatus = Messenger.Send<TimerRequestMessage>();
            _timerStatus = timerStatus.isOn;
            _delay = timerStatus.delay;
            _closeTime = timerStatus.closeTime;
            _networkStatus = Windows.Storage.ApplicationData.Current.LocalSettings.Values["CurrentNetworkMode"] == null ? true : (bool)Windows.Storage.ApplicationData.Current.LocalSettings.Values["CurrentNetworkMode"];
        }

        public ElementTheme Theme
        {
            get => _theme;
            set
            {
                SetProperty(ref _theme, value);
                SendThemeMessage();
                SaveTheme();
            }
        }

        public string Language
        {
            get => _language;
            set
            {
                SetProperty(ref _language, value);
                SaveLanguage();
            }
        }

        public bool TimerStatus
        {
            get => _timerStatus;
            set
            {
                SetProperty(ref _timerStatus, value);
                CloseTime = DateTime.Now.AddMilliseconds(_delay.TotalMilliseconds).ToString("g");
                SendTimerMessage();
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

        public string CloseTime
        {
            get
            {
                var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
                return resourceLoader.GetString("SettingsTimerDelay") + _closeTime;
            }
            set
            {
                SetProperty(ref _closeTime, value);
            }
        }

        public bool NetworkStatus
        {
            get => _networkStatus;
            set
            {
                SetProperty(ref _networkStatus, value);
                SaveNetwork();
            }
        }

        public void SaveNetwork()
        {
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["CurrentNetworkMode"] = NetworkStatus;
        }


        public void SendThemeMessage()
        {
            Messenger.Send(new ThemeChangedMessage(Theme));
        }

        public void SendTimerMessage()
        {
            Messenger.Send(new TimerChangedMessage(new TimerStatus(Delay, TimerStatus, _closeTime)));
        }

        private void SaveLanguage()
        {
            Windows.Storage.ApplicationData.Current.LocalSettings.Values["CurrentLanguage"] = Language;
        }

        private void SaveTheme()
        {
            if(Theme == ElementTheme.Light)
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["CurrentTheme"] = "Light";
            }
            else if(Theme == ElementTheme.Dark)
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["CurrentTheme"] = "Dark";
            }
            else
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["CurrentTheme"] = "Default";
            }
        }

    }
}
