using CommunityToolkit.Mvvm;
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
using PureRadio.Uwp.Models.Data.User;
using CommunityToolkit.Mvvm.ComponentModel;
using PureRadio.Uwp.Services.Interfaces;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using PureRadio.Uwp.Models.Database;
using PureRadio.LocalManage.DataModelsL;
using PureRadio.LocalManage.Iterfaces;
using PureRadio.Uwp.Models.Enums;
using Windows.UI.Xaml.Media.Animation;

namespace PureRadio.ViewModels.LocalRadioManageViewModels
{
    public sealed partial class LocalLibraryViewModel : ObservableRecipient
    {
        private readonly INavigateService navigate;
        private readonly IChannalOperate radioServ;
        private readonly IAlbumOpreate contentServ;


        [ObservableProperty]
        private bool _isDownloadRadioShown;
        [ObservableProperty]
        private bool _isDownloadContentShown;

        [ObservableProperty]
        private bool _isLoading;
        [ObservableProperty]
        private bool _isItemsLoading;
        [ObservableProperty]
        private bool _isEmpty;

        public ICommand DdRadioCommand { get; }
        public ICommand DdContentCommand { get; }

        public ObservableCollection<ChannalCardInfo> RadioResults;
        public ObservableCollection<AlbumCardInfo> ContentResults;
        public ObservableCollection<ChannalRadioInfo> RadioDetailResults;
        public ObservableCollection<AlbumRadioInfo> ContentDetailResults;

        public LocalLibraryViewModel(
            INavigateService navigate,
            IChannalOperate radioServ,
            IAlbumOpreate contentServ)
        {
            this.navigate = navigate;
            this.radioServ = radioServ;
            this.contentServ = contentServ;

            RadioResults = new ObservableCollection<ChannalCardInfo>();
            ContentResults = new ObservableCollection<AlbumCardInfo>();
            RadioDetailResults = new ObservableCollection<ChannalRadioInfo>();
            ContentDetailResults = new ObservableCollection<AlbumRadioInfo>();

            DdRadioCommand = new RelayCommand(SetRadioResult);
            DdContentCommand = new RelayCommand(SetContentResult);

            GetDownload();

            IsActive = true;

            IsDownloadRadioShown = true;
            IsDownloadContentShown = false;
        }

        public void Navigate(PageIds pageId, object parameter = null)
        {
            if (pageId == PageIds.RadioDetail || pageId == PageIds.ContentDetail)
            {
                navigate.NavigateToSecondaryView(pageId, new EntranceNavigationTransitionInfo(), parameter);
            }
        }

        private void SetRadioResult()
        {
            if (IsDownloadContentShown)
            {
                IsDownloadContentShown = false;
            }
            IsDownloadRadioShown = true;
            IsEmpty = RadioResults.Count == 0;
        }

        private void SetContentResult()
        {
            if (IsDownloadRadioShown)
            {
                IsDownloadRadioShown = false;
            }
            IsDownloadContentShown = true;
            IsEmpty = ContentResults.Count == 0;
        }

        private void GetDownload()
        {
            RadioResults.Clear();
            var radios =  radioServ.Load();
            foreach (var radio in radios)
                RadioResults.Insert(0, radio);

            ContentResults.Clear();
            var contents = contentServ.Load();
            foreach (var content in contents)
                ContentResults.Insert(0, content);

            if (IsDownloadRadioShown)
                IsEmpty = RadioResults.Count == 0;
            else if (IsDownloadContentShown)
                IsEmpty = ContentResults.Count == 0;
        }

        public void GetDownloadRadioDetail(ChannalCardInfo channalCardInfo)
        {
            RadioDetailResults.Clear();
            var radioItems = radioServ.Load(channalCardInfo);
            foreach (var item in radioItems)
                RadioDetailResults.Insert(0, item);
        }

        public void GetDownloadContentDetail(AlbumCardInfo albumCardInfo)
        {
            ContentDetailResults.Clear();
            var contentItems = contentServ.Load(albumCardInfo);
            foreach (var item in contentItems)
                ContentDetailResults.Insert(0, item);
        }

        public void RemoveRadio(ChannalCardInfo channalCardInfo)
        {
            radioServ.Remove(channalCardInfo);
        } 

        public void RemoveRadioDetail(ChannalRadioInfo channalRadioInfo)
        {
            radioServ.Remove(channalRadioInfo);
        }

        public void RemoveContent(AlbumCardInfo albumCardInfo)
        {
            contentServ.Remove(albumCardInfo);
        } 

        public void RemoveContentDetail(AlbumRadioInfo albumRadioInfo)
        {
            contentServ.Remove(albumRadioInfo);
        }

        
    }
}

