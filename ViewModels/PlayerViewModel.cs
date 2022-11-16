using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using PureRadio.Common;
using PureRadio.DataModel;
using PureRadio.DataModel.Message;
using PureRadio.DataModel.Parameter;
using PureRadio.DataModel.Results;
using PureRadio.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Media.Streaming.Adaptive;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace PureRadio.ViewModels
{
    /// <summary>
    /// The view model for the player.
    /// </summary>
    /// <remarks>
    /// The view disables the ability to skip during a transition or when
    /// the playback list is empty.
    /// </remarks>
    public class PlayerViewModel : ObservableRecipient//, IDisposable
    {
        //private bool disposed;
        //private MediaPlayer player;
        public PlaybackService playService;

        //private CoreDispatcher dispatcher;
        //private MediaPlaybackList subscribedPlaybackList;
        private MediaPlaybackState _isPlaying;
        private DateTime _updateTime;
        private DispatcherTimer timer;

        private NowPlayingDetail _playingDetail;
        private string _title = string.Empty;
        private string _playing = string.Empty;
        private string _cover = "/Assets/Image/DefaultCover.png";
        private string _duration = string.Empty;
        private double _volume;
        private bool _isMuted;
        private int _mediaTotalSeconds;
        private int _mediaNowPosition;
        private bool _positionLiveDisplay = true;
        private bool _positionDemandDisplay = false;

        public bool _positionChange = true;


        //MediaListViewModel mediaList;
        private bool _canSkipNext;
        private bool _canSkipPrevious;

        //public PlaybackSessionViewModel PlaybackSession { get; private set; }

        public PlayerViewModel()
        {
            IsActive = true;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += TimerUpdateRadioDetial;//每1秒触发这个事件，以刷新当前播放            
            playService = PlaybackService.Instance;            
            playService.Player.MediaOpened += MediaOpened;
            playService.Player.MediaEnded += MediaEnded;
            playService.Player.PlaybackSession.PlaybackStateChanged += PlaybackSession_PlaybackStateChanged;
            //playService.PlaybackList.CurrentItemChanged += ItemChanged;
            //player = PlaybackService.Instance.Player;
        }

        private async void PlaybackSession_PlaybackStateChanged(MediaPlaybackSession sender, object args)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                switch (playService.Player.PlaybackSession.PlaybackState)
                {
                    case MediaPlaybackState.Playing:
                        IsPlaying = MediaPlaybackState.Playing;
                        break;
                    case MediaPlaybackState.Paused:
                        IsPlaying = MediaPlaybackState.Paused;
                        break;
                    default:
                        IsPlaying = MediaPlaybackState.Paused;
                        break;
                }
            });            
        }

        protected override void OnActivated()
        {
            WeakReferenceMessenger.Default.Register<PlayRadioMessage>(this, (r, m) =>
            {
                PlayChannelLive(m.Value);
                Messenger.Send(new PlayStartMessage(m.Value));
            });
            WeakReferenceMessenger.Default.Register<PlayRadioContentMessage>(this, (r, m) =>
            {
                PlayChannelDemand(m.Value);
                Messenger.Send(new PlayStartMessage(m.Value.channe_id));
            });
            WeakReferenceMessenger.Default.Register<PlayProgramMessage>(this, (r, m) =>
            {
                PlayProgram(m.Value);
                Messenger.Send(new PlayStartMessage(m.Value.album_id));
            });
            Messenger.Register<PlayerViewModel, NowPlayingRequestMessage>(this, (r, m) => m.Reply(_playingDetail));
        }

        public double PlayerVolume
        {
            get
            {
                if(!IsMuted) _volume = playService.Player.Volume * 100;
                return _volume;
            }
            set
            {
                SetProperty(ref _volume, value);
                if(!IsMuted) playService.Player.Volume = value/100;
            }
        }

        public string Duration
        {
            get => _duration;
            set
            {
                SetProperty(ref _duration, value);
            }
        }

        public MediaPlaybackState IsPlaying
        {
            get => _isPlaying;
            set
            {
                SetProperty(ref _isPlaying, value);
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                SetProperty(ref _title, value);
            }
        }

        public string Playing
        {
            get => _playing;
            set
            {
                SetProperty(ref _playing, value);
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

        public bool IsMuted
        {
            get => _isMuted;
            set
            {
                SetProperty(ref _isMuted, value);
            }
        }

        public int MediaTotalSeconds
        {
            get => _mediaTotalSeconds;
            set
            {
                SetProperty(ref _mediaTotalSeconds, value);
            }
        }

        public int MediaNowPosition
        {
            get
            {
                return _mediaNowPosition;
            }
            set
            {
                SetProperty(ref _mediaNowPosition, value);
            }
        }

        public bool PositionLiveDisplay
        {
            get => _positionLiveDisplay;
            set
            {
                SetProperty(ref _positionLiveDisplay, value);
            }
        }
        public bool PositionDemandDisplay
        {
            get => _positionDemandDisplay;
            set
            {
                SetProperty(ref _positionDemandDisplay, value);
            }
        }

        public bool CanSkipNext
        {
            get => _canSkipNext;
            set
            {
                SetProperty(ref _canSkipNext, value);
            }
        }
        public bool CanSkipPrevious
        {
            get => _canSkipPrevious;
            set
            {
                SetProperty(ref _canSkipPrevious, value);
            }
        }

        public void SkipPrevious()
        {
            if (playService.PlaybackList.CurrentItemIndex == 0) return;
            playService.PlaybackList.MovePrevious();
        }

        public void TogglePlayPause()
        {
            switch (playService.Player.PlaybackSession.PlaybackState)
            {
                case MediaPlaybackState.Playing:
                    playService.Player.Pause();
                    //IsPlaying = MediaPlaybackState.Paused;
                    break;
                case MediaPlaybackState.Paused:
                    playService.Player.Play();
                    //IsPlaying = MediaPlaybackState.Playing;
                    break;
            }
        }

        public void SkipNext()
        {
            if (playService.PlaybackList.CurrentItemIndex == playService.PlaybackList.Items.Count - 1) return;
            playService.PlaybackList.MoveNext();
        }

        public void Mute()
        {
            if (!IsMuted)
            {
                IsMuted = true;
                playService.Player.Volume = 0;
            }
            else
            {
                IsMuted = false;
                playService.Player.Volume = _volume/100;
            }
        }

        public void Refresh()
        {
            if(_playingDetail != null)
            {
                if(_playingDetail.type == 0)
                {
                    PlayChannelLive(_playingDetail.id);
                }
                else
                {
                    MediaNowPosition = 0;
                    Flyto(0);
                }
            }
        }


        public void PlayChannelLive(int channel_id)
        {
            string url = QTDecoder.PlayChannel_Live(channel_id);
            GetRadioDetail(channel_id);
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            string playing = resourceLoader.GetString("SMTCLive");
            playService.InitializeAdaptiveMediaSourceAsync(new Uri(url), _playingDetail.cover, playing, _playingDetail.title);
            IsPlaying = MediaPlaybackState.Playing;            
        }

        public void PlayChannelDemand(RadioDemandMessage fullContent)
        {
            int channel_id = fullContent.channe_id;
            var channelDetail = NowPlayingDetail.GetNowplayingRadio(channel_id);
            if (_playingDetail == null || _playingDetail.id != channel_id || _playingDetail.type != 1 || fullContent.selected_day_change)
            {
                UpdatePlaybackList(fullContent.PlayList, channelDetail.cover, channelDetail.title);
                playService.Player.Source = playService.PlaybackList;
            }
            _playingDetail = channelDetail;
            _playingDetail.type = 1;            
            uint index = Convert.ToUInt32(fullContent.selected_index);
            playService.PlaybackList.MoveTo(index);
            playService.Player.Play();
            IsPlaying = MediaPlaybackState.Playing;
        }

        public void PlayProgram(ProgramDemandMessage program)
        {
            _playingDetail = NowPlayingDetail.GetNowPlayingProgram(program);
            string url = QTDecoder.PlayContent(program.album_id, program.content_id, program.access_token, program.qingting_id);
            playService.Source = MediaSource.CreateFromUri(new Uri(url));
            playService.PlaybackItem = new MediaPlaybackItem(playService.Source);
            playService.UpdateProperties(_playingDetail.cover, _playingDetail.title, _playingDetail.program_title);
            playService.Player.Source = playService.PlaybackItem;
            playService.Player.Play();
            IsPlaying = MediaPlaybackState.Playing;
        }

        public void GetRadioDetail(int channel_id)
        {
            _playingDetail = NowPlayingDetail.GetNowplayingRadio(channel_id);            
        }
       
        public void UpdateDisplayProperties()
        {
            MediaItemDisplayProperties props = playService.PlaybackItem.GetDisplayProperties();
            props.Type = Windows.Media.MediaPlaybackType.Music;
            props.MusicProperties.Title = Playing;
            props.MusicProperties.Artist = Title;
            props.Thumbnail = Windows.Storage.Streams.RandomAccessStreamReference.CreateFromUri(new Uri(_playingDetail.cover));
            playService.PlaybackItem.ApplyDisplayProperties(props);
            //Debug.Print(props.MusicProperties.Title);
        }

        public void UpdatePlaybackList(List<RadioFullContent> radioFulls, string cover, string radio_channel)
        {
            playService.PlaybackList = new MediaPlaybackList();
            foreach (RadioFullContent radioFull in radioFulls)
            {
                string url = QTDecoder.PlayChannel_Demand(radioFull);
                var mediaPlaybackItem = new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri(url)));
                var props = mediaPlaybackItem.GetDisplayProperties();
                props.Type = Windows.Media.MediaPlaybackType.Music;
                props.MusicProperties.Title = radioFull.title;
                props.MusicProperties.Artist = radio_channel;
                props.Thumbnail = Windows.Storage.Streams.RandomAccessStreamReference.CreateFromUri(new Uri(cover));
                mediaPlaybackItem.ApplyDisplayProperties(props);
                playService.PlaybackList.Items.Add(mediaPlaybackItem);
                //Debug.Print(playService.PlaybackList.Items.Count().ToString());
            }
            playService.PlaybackList.CurrentItemChanged += ItemChanged;
        }
        public async void MediaOpened(MediaPlayer sender, object args)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Cover = _playingDetail.cover;
                Title = _playingDetail.title;
                if (_playingDetail.type == 0)
                {
                    if (timer != null && timer.IsEnabled) timer.Stop();//如果已有Timer，先取消
                    Playing = _playingDetail.radio.name;
                    Duration = _playingDetail.start_time + "-" + _playingDetail.end_time;
                    _updateTime = DateTime.Parse(_playingDetail.end_time);
                    if (_updateTime.Hour == 23 && _updateTime.Minute == 59) _updateTime = _updateTime.AddMinutes(1);
                    MediaTotalSeconds = _playingDetail.duration;
                    MediaNowPosition = (int)(DateTime.Now - DateTime.Parse(_playingDetail.start_time)).TotalSeconds;
                    CanSkipPrevious = CanSkipNext = false;
                    //UpdateDisplayProperties();
                    
                }
                else if (_playingDetail.type == 1)
                {
                    if (timer != null && timer.IsEnabled) timer.Stop();//如果已有Timer，先取消
                    Duration = "00:00:00/" + playService.Player.PlaybackSession.NaturalDuration.ToString(@"hh\:mm\:ss");
                    CanSkipPrevious = CanSkipNext = true;
                }
                else if(_playingDetail.type == 2)
                {
                    if (timer != null && timer.IsEnabled) timer.Stop();//如果已有Timer，先取消
                    Playing = _playingDetail.title;
                    Title = _playingDetail.program_title;
                    Duration = "00:00:00/" + playService.Player.PlaybackSession.NaturalDuration.ToString(@"hh\:mm\:ss");
                    MediaNowPosition = 0;
                    MediaTotalSeconds = (int)playService.Player.PlaybackSession.NaturalDuration.TotalSeconds;
                    CanSkipPrevious = CanSkipNext = false;
                }
                timer.Start();
            });
        }

        public async void MediaEnded(MediaPlayer sender, object args)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                timer.Stop();
                Messenger.Send(new PlayEndMessage(_playingDetail.id));
                Title = string.Empty;
                Playing = string.Empty;
                Cover = "/Assets/Image/DefaultCover.png";
                Duration = string.Empty;
                MediaNowPosition = 0;
                _playingDetail = null;
            });
        }

        public void TimerUpdateRadioDetial(object sender, object e)
        {
            if(_playingDetail.type == 0)
            {
                if (_updateTime <= DateTime.Now)
                {
                    GetRadioDetail(_playingDetail.id);
                    UpdateUIInfo();
                }
                if (IsPlaying == MediaPlaybackState.Playing)
                {
                    MediaNowPosition = MediaNowPosition + 1;
                }
                //Debug.Print(playService.Player.PlaybackSession.Position.TotalSeconds.ToString());
            }
            else
            {                
                TimeSpan now = playService.Player.PlaybackSession.Position;
                TimeSpan all = playService.Player.PlaybackSession.NaturalDuration;
                if (_positionChange)
                {
                    MediaNowPosition = (int)now.TotalSeconds;
                }               
                Duration = now.ToString(@"hh\:mm\:ss") + "/" + all.ToString(@"hh\:mm\:ss");
            }
        }

        public void NavToDetail()
        {
            if (_playingDetail == null) return;
            if(_playingDetail.type == 0|| _playingDetail.type == 1)
            {
                WeakReferenceMessenger.Default.Send(new NavToRadioDetailMessage(new RadioShot(_playingDetail.id)));
            }
            else if(_playingDetail.type == 2)
            {
                WeakReferenceMessenger.Default.Send(new NavToProgramDetailMessage(new ProgramShot(_playingDetail.id)));
            }
        }

        public void Flyto(int time)
        {
            playService.Player.PlaybackSession.Position = TimeSpan.FromSeconds(time);
        }

        public async void ItemChanged(MediaPlaybackList sender, CurrentMediaPlaybackItemChangedEventArgs args)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if(args.NewItem != null)
                {
                    Playing = args.NewItem.GetDisplayProperties().MusicProperties.Title;
                    MediaTotalSeconds = (int)args.NewItem.Source.Duration?.TotalSeconds;
                    MediaNowPosition = 0;
                }
            });
        }

        public void UpdateUIInfo()
        {
            Title = _playingDetail.title;
            Cover = _playingDetail.cover;
            Playing = _playingDetail.radio.name;
            Duration = _playingDetail.start_time + "-" + _playingDetail.end_time;
            _updateTime = DateTime.Parse(_playingDetail.end_time);
            MediaTotalSeconds = _playingDetail.duration;
            MediaNowPosition = (int)(DateTime.Now - DateTime.Parse(_playingDetail.start_time)).TotalSeconds;
            playService.UpdateProperties(_playingDetail.cover, _playingDetail.radio.name, _playingDetail.title);
        }

    }

    public sealed class PlayRadioMessage : ValueChangedMessage<int>
    {
        public PlayRadioMessage(int value) : base(value)
        {
        }
    }
    
    public sealed class PlayRadioContentMessage : ValueChangedMessage<RadioDemandMessage>
    {
        public PlayRadioContentMessage(RadioDemandMessage value) : base(value)
        {
        }
    }

    public sealed class PlayProgramMessage : ValueChangedMessage<ProgramDemandMessage>
    {
        public PlayProgramMessage(ProgramDemandMessage value) : base(value)
        {
        }
    }

    public sealed class UpdatePlaybackListMessage : ValueChangedMessage<int>
    {
        public UpdatePlaybackListMessage(int value) : base(value)
        {
        }
    }
    public sealed class UpdateDisplayPropertiesMessage : ValueChangedMessage<int>
    {
        public UpdateDisplayPropertiesMessage(int value) : base(value)
        {
        }
    }

    
}
