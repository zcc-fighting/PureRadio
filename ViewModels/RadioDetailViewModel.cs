using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Toolkit.Uwp.UI;
using PureRadio.LocalManage.DataModelsL;
using PureRadio.LocalManage.Iterfaces;
using PureRadio.Uwp.Adapters;
using PureRadio.Uwp.Adapters.Interfaces;
using PureRadio.Uwp.Models.Args;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
using PureRadio.Uwp.Providers;
using PureRadio.Uwp.Providers.Interfaces;
using PureRadio.Uwp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.Media.Editing;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using PureRadio.LocalManage.Adapters;
using PureRadio.Uwp.Models.Data.Constants;
using LocalRadioManage.StorageOperate;
using Windows.Storage;

namespace PureRadio.Uwp.ViewModels
{
    public sealed partial class RadioDetailViewModel : ObservableRecipient
    {
        private readonly ISettingsService settings;
        private readonly IChannalOperate radioServ;
        private readonly INavigateService navigate;
        private readonly IPlaybackService playbackService;
        private readonly ILibraryService library;
        private readonly IRadioProvider radioProvider;
        private readonly IPlayerAdapter playerAdapter;
        private readonly DispatcherTimer _refreshTimer;
        private PlaylistDay currentSource;
        private PlayItemSnapshot itemSnapshot;
        private RadioInfoDetail radioDetail;
        private int _topCategoryId;

        private int _radioId;
        public int RadioId
        {
            get => _radioId;
            set
            {
                SetProperty(ref _radioId, value);
                GetRadioFavState();
                GetRadioDetail();
                GetRadioPlaylist();
            }
        }
        static RadioInfoDetail currentDetail;
        private ChannalCardInfo _channalCardInfo;
        public IAsyncRelayCommand ToggleFavCommand { get; }

        [ObservableProperty]
        private bool _isFav;
        [ObservableProperty]
        private bool _isOffline;
        [ObservableProperty]
        private bool _isNotOffline;
        [ObservableProperty]
        private bool _isItemDownload;
        [ObservableProperty]
        private string _title;
        [ObservableProperty]
        private BitmapImage _cover;
        [ObservableProperty]
        private string _description;
        [ObservableProperty]
        private string _audienceCount;
        [ObservableProperty]
        private string _nowplaying;
        [ObservableProperty]
        private string _topCategoryTitle;

        [ObservableProperty]
        private bool _isInfoLoading;

        [ObservableProperty]
        private bool _isPlaylistLoading;

        [ObservableProperty]
        private bool _isShowErrorTips;

        [ObservableProperty]
        private List<RadioPlaylistDetail> _radioPlaylist;

        [ObservableProperty]
        private List<ChannalRadioInfo> _localRadioList = new List<ChannalRadioInfo>();

        private List<RadioPlaylistDetail> PlaylistsBYDay;
        private List<RadioPlaylistDetail> PlaylistsYDay;
        private List<RadioPlaylistDetail> PlaylistsToday;
        private List<RadioPlaylistDetail> PlaylistsTMR;

        public RadioDetailViewModel(
            ISettingsService settings,
            IChannalOperate radioServ,
            INavigateService navigate, 
            IRadioProvider radioProvider,
            IPlaybackService playbackService,
            ILibraryService library,
            IPlayerAdapter playerAdapter)
        {
            this.settings = settings;
            this.radioServ = radioServ;
            this.navigate = navigate;
            this.playbackService = playbackService;
            this.library = library;
            this.radioProvider = radioProvider;
            this.playerAdapter = playerAdapter;
            IsOffline = settings.GetValue<bool>(AppConstants.SettingsKey.IsOffline);
            IsNotOffline = !IsOffline;
            _refreshTimer = new DispatcherTimer
            {
                Interval = TimeSpan.MaxValue,
            };
            Cover = new BitmapImage(new Uri("ms-appx:///Assets/Image/DefaultCover.png"));
            ToggleFavCommand = new AsyncRelayCommand(ToggleFavState);
            IsActive = true;
            
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            _refreshTimer.Tick += OnRefreshTimerTick;
            library.FavItemChanging += Library_FavItemChanging;
        }

        protected override void OnDeactivated()
        {
            if (_refreshTimer.IsEnabled)
                _refreshTimer.Stop();
            _refreshTimer.Tick -= OnRefreshTimerTick;
            library.FavItemChanging -= Library_FavItemChanging;
            base.OnDeactivated();
        }

        private void Library_FavItemChanging(object sender, FavItemChangedEventArgs e)
        {
            if(e.ItemType == MediaPlayType.RadioLive && e.MainId == RadioId)
            {
                IsFav = e.Action switch
                {
                    LibraryItemAction.Add => true,
                    LibraryItemAction.Remove => false,
                    LibraryItemAction.Update => true,
                    _ => false,
                };
            }
        }

        private async void GetRadioFavState()
        {
            if (RadioId != 0)
            {
                IsFav = await library.IsFavItem(MediaPlayType.RadioLive, RadioId);
            }
        }
        private async void GetRadioDetail()
        {
            if(RadioId != 0)
            {
                IsInfoLoading = true;
                var result = await radioProvider.GetRadioDetailInfo(RadioId, CancellationToken.None);
                Title = result.Title;
                Cover = await ImageCache.Instance.GetFromCacheAsync(result.Cover);
                Cover.DecodePixelHeight = Cover.DecodePixelWidth = 200;
                Cover.DecodePixelType = DecodePixelType.Logical;
                Description = result.Description;
                AudienceCount = result.AudienceCount;
                Nowplaying = result.Nowplaying;
                TopCategoryTitle = result.TopCategoryTitle;
                _topCategoryId = result.TopCategoryId;
                if (result.UpdateTime != TimeSpan.Zero)
                {
                    if (_refreshTimer.IsEnabled) 
                        _refreshTimer.Stop();
                    _refreshTimer.Interval = result.UpdateTime.Add(TimeSpan.FromSeconds(1));
                    if (!_refreshTimer.IsEnabled)
                        _refreshTimer.Start();
                }
                else
                {
                    if (_refreshTimer.IsEnabled)
                        _refreshTimer.Stop();
                }
                itemSnapshot = playerAdapter.ConvertToPlayItemSnapshot(result);
                radioDetail = result;
                IsInfoLoading = false;
            }
        }

        private async void GetRadioPlaylist()
        {
            if(RadioId != 0)
            {
                IsPlaylistLoading = true;
                var result = await radioProvider.GetRadioPlaylistDetail(RadioId, CancellationToken.None);
                if(result != null)
                {
                    PlaylistsBYDay = result?.BYday ?? new List<RadioPlaylistDetail>();
                    PlaylistsYDay = result?.Yday ?? new List<RadioPlaylistDetail>();
                    PlaylistsToday = result?.Today ?? new List<RadioPlaylistDetail>();
                    PlaylistsTMR = result?.Tmr ?? new List<RadioPlaylistDetail>();
                }
                else
                {
                    PlaylistsBYDay = new List<RadioPlaylistDetail>();
                    PlaylistsYDay = new List<RadioPlaylistDetail>();
                    PlaylistsToday = new List<RadioPlaylistDetail>();
                    PlaylistsTMR = new List<RadioPlaylistDetail>();
                }
                SwitchPlaylistSource(currentSource);
                IsPlaylistLoading = false;
            }
        }

        public void SwitchPlaylistSource(PlaylistDay target)
        {
            RadioPlaylist = target switch
            {
                PlaylistDay.BeforeYesterday => PlaylistsBYDay,
                PlaylistDay.Yesterday => PlaylistsYDay,
                PlaylistDay.Tomorrow => PlaylistsTMR,
                _ => PlaylistsToday
            };
            currentSource = target;
        }

        private void OnRefreshTimerTick(object sender, object e)
        {
            _refreshTimer.Stop();
            UpdateRadioLiveInfo();
        }

        public void PlayRadioLive()
        {
            if (IsOffline)
                playbackService.PlayRadioLive(_channalCardInfo.RadioId, itemSnapshot, currentDetail);
            else if (itemSnapshot.Duration != 0)
            {
                playbackService.PlayRadioLive(RadioId, itemSnapshot,currentDetail);
            }
        }

        public void PlayRadioDemand(int index)
        {
            if (IsOffline&& _channalCardInfo.RadioId != 0 && index >= 0 && index < LocalRadioList.Count())
            {
                var playList = playerAdapter.ConvertToLocalPlayItemSnapshotList(radioDetail, LocalRadioList);
                playbackService.PlayRadioDemand(_channalCardInfo.RadioId, index, playList);
            }
            else if (RadioId != 0 && index >= 0 && index < RadioPlaylist.Count())
                switch (currentSource)
                {
                    case PlaylistDay.Tomorrow:
                        IsShowErrorTips = true;
                        break;
                    case PlaylistDay.Today:
                        if (DateTime.TryParse(RadioPlaylist[index].EndTime, out DateTime _endDateTime))
                        {
                            if (DateTime.Now > _endDateTime)
                            {
                                var playList = playerAdapter.ConvertToPlayItemSnapshotList(radioDetail, RadioPlaylist);
                                playbackService.PlayRadioDemand(RadioId, index, playList);
                            }
                            else
                            {
                                IsShowErrorTips = true;
                            }
                        }
                        break;
                    default:
                        var snapshotList = playerAdapter.ConvertToPlayItemSnapshotList(radioDetail, RadioPlaylist);
                        playbackService.PlayRadioDemand(RadioId, index, snapshotList);
                        break;
                }

        }

        public void NavigateToCategory()
        {
            if(RadioId != 0&& _topCategoryId != 0)
            {
                navigate.NavigateToSecondaryView(PageIds.RadioCategory, new EntranceNavigationTransitionInfo(), _topCategoryId);
            }
        }

        public async Task ToggleFavState()
        {
            if (RadioId != 0 && itemSnapshot != null)
            {
                if (IsFav)
                {
                    _ = await library.RemoveFromFav(MediaPlayType.RadioLive, RadioId);
                }
                else
                {
                    _ = await library.AddToFav(itemSnapshot);
                }
            }
        }

        private async void UpdateRadioLiveInfo()
        {
            RadioInfoDetail detail;
            if (IsOffline)
                 detail = ChannalCardInfoAdapters.ToRadioInfoDetail(_channalCardInfo);
            else detail = await radioProvider.GetRadioDetailInfo(RadioId, CancellationToken.None);
            int count = 0;
            while (detail.RadioId == RadioId && detail.EndTime == radioDetail.EndTime && detail.UpdateTime != TimeSpan.Zero)
            {
                if (count < 4)
                    await Task.Delay(5000);
                else if (count < 10)
                    await Task.Delay(10000);
                else
                {
                    return;
                }
                detail = await radioProvider.GetRadioDetailInfo(RadioId, CancellationToken.None);
                count++;
            }
            IsInfoLoading = true;
            Title = detail.Title;
            Cover = new BitmapImage(detail.Cover);
            Description = detail.Description;
            AudienceCount = detail.AudienceCount;
            Nowplaying = detail.Nowplaying;
            TopCategoryTitle = detail.TopCategoryTitle;
            _topCategoryId = detail.TopCategoryId;
            if (detail.UpdateTime != TimeSpan.Zero)
            {
                if (_refreshTimer.IsEnabled)
                    _refreshTimer.Stop();
                _refreshTimer.Interval = detail.UpdateTime.Add(TimeSpan.FromSeconds(1));
                if (!_refreshTimer.IsEnabled)
                    _refreshTimer.Start();
            }
            else
            {
                if (_refreshTimer.IsEnabled)
                    _refreshTimer.Stop();
            }
            itemSnapshot = playerAdapter.ConvertToPlayItemSnapshot(detail);
            radioDetail = detail;
            IsInfoLoading = false;
            if (DateTime.Now.Hour == 0 && DateTime.Now.Minute < 2)
                if (IsNotOffline)
                    GetRadioPlaylist();
                else
                    GetLocalRadioDetailList(_channalCardInfo);
        }

        public async void DownloadRadioDetailListItem(RadioPlaylistDetail radioPlaylistDetail)
        {
            await radioServ.Download(ChannalCardInfoAdapters.ToChanalCardInfo(radioDetail),ChannalRadioInfoAdapters.ToChannalRadioInfo(radioPlaylistDetail));
        }

        public void GetLocalRadioDetail(ChannalCardInfo channalCardInfo)
        {
            IsInfoLoading = true;
            _channalCardInfo = channalCardInfo;
            var result = ChannalCardInfoAdapters.ToRadioInfoDetail(channalCardInfo);
            currentDetail = result;
            Title = result.Title;
            Cover = new BitmapImage(result.Cover);
            Description = result.Description;
            AudienceCount = result.AudienceCount;
            Nowplaying = result.Nowplaying;
            TopCategoryTitle = result.TopCategoryTitle;
            _topCategoryId = result.TopCategoryId;
            radioDetail = result;
            itemSnapshot = playerAdapter.ConvertToPlayItemSnapshot(result);
            IsInfoLoading = false;
        }
        public void GetLocalRadioDetailList(ChannalCardInfo channalCardInfo)
        {
            IsPlaylistLoading = true;
            _channalCardInfo = channalCardInfo;

            LocalRadioList = radioServ.Load(channalCardInfo);
            IsPlaylistLoading = false;
        }
        public static RadioInfoDetail GetRadioInfoDetail()
        {
            return currentDetail;
        }
    }
}
