using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using PureRadio.DataModel;
using PureRadio.DataModel.Message;
using PureRadio.DataModel.Parameter;
using PureRadio.DataModel.Results;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.ViewModels
{
    public class DetailRadioViewModel : ObservableRecipient
    {
        public FullDetailRadio _fullDetailRadio;
        private List<RadioFullContent> PlayListYesterday { get; set; }
        private List<RadioFullContent> PlayListToday { get; set; }
        private List<RadioFullContent> PlayListTomorrow { get; set; }
        private int _radioID;
        private string _cover;
        private string _radioChannel;
        private string _nowPlaying;
        private string _listener;
        private string _description;
        private bool _isFav = false;
        public List<RadioFullContent> _playList = new List<RadioFullContent>();

        private string _lastSelectedDay;

        public int RadioID
        {
            get => _radioID;
            set
            {
                SetProperty(ref _radioID, value);
            }
        }
        public string Cover
        {
            get => _cover;
            set
            {
                SetProperty(ref _cover, value);
            }
        }
        public string RadioChannel
        {
            get => _radioChannel;
            set
            {
                SetProperty(ref _radioChannel, value);
            }
        }
        public string NowPlaying
        {
            get => _nowPlaying;
            set
            {
                SetProperty(ref _nowPlaying, value);
            }
        }
        public string Listener
        {
            get => _listener;
            set
            {
                SetProperty(ref _listener, value);
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                SetProperty(ref _description, value);
            }
        }

        public bool IsFav
        {
            get => _isFav;
            set
            {
                SetProperty(ref _isFav, value);
            }
        }

        public List<RadioFullContent> PlayList
        {
            get => _playList;
            set
            {
                SetProperty(ref _playList, value);
            }
        }

        public DetailRadioViewModel()
        {

        }

        public void SetChannel(RadioShot channel)
        {
            _radioID = channel.channelID;
            _isFav = channel.isFav;
            GetInfo();
        }

        public void GetInfo()
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            var detail = FullDetailRadio.GetRadioDetail(_radioID);
            Cover = detail.album.cover;
            RadioChannel = detail.album.title;
            if (detail.album.nowplaying != null) NowPlaying = resourceLoader.GetString("DetailPageNowPlaying") + detail.album.nowplaying.name;
            Listener = resourceLoader.GetString("PlayCountDisplayRadio") + detail.album.audience_count.ToString();
            Description = detail.album.description;
            //PlayListYesterday = new ObservableCollection<RadioFullContent>(detail.ListYesterday);
            //PlayListToday = new ObservableCollection<RadioFullContent>(detail.ListToday);
            //PlayListTomorrow = new ObservableCollection<RadioFullContent>(detail.ListTomorrow);
            PlayListYesterday = detail.ListYesterday;
            PlayListToday = detail.ListToday;
            PlayListTomorrow = detail.ListTomorrow;
        }

        public void ChangePlayList(string tag)
        {
            if (tag == "today") PlayList = PlayListToday;
            else if (tag == "yesterday") PlayList = PlayListYesterday;
            else if (tag == "tomorrow") PlayList = PlayListTomorrow;
            else PlayList = PlayListToday;
        }

        public void PlayRadio()
        {
            WeakReferenceMessenger.Default.Send(new PlayRadioMessage(RadioID));
        }

        public void PlayRadioContent(int index, string selectedDay)
        {
            bool selected_day = true;
            if (_lastSelectedDay == selectedDay) selected_day = false;

            WeakReferenceMessenger.Default.Send(new PlayRadioContentMessage(new RadioDemandMessage(RadioID, selected_day, index, PlayList)));
            _lastSelectedDay = selectedDay;
        }

        public void ToggleFav()
        {
            if (IsFav)
            {
                Messenger.Send(new FavDelMessage(RadioID));
                IsFav = false;
            }
            else
            {
                FavItem favItem = new FavItem
                {
                    isRadio = true,
                    id = RadioID,
                    album_cover = Cover,
                    name = RadioChannel
                };
                Messenger.Send(new FavAddMessage(favItem));
                IsFav = true;
            }

        }

    }
}
