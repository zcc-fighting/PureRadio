using CommunityToolkit.Mvvm.DependencyInjection;
using PureRadio.LocalManage.DataModelsL;
using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.QingTing.Content;
using PureRadio.Uwp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PureRadio.Uwp.Views.Secondary
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ContentDetailPage : Page
    {
        public ContentDetailViewModel ViewModel => (ContentDetailViewModel)DataContext;

        public ContentDetailPage()
        {
            this.InitializeComponent();

            DataContext = Ioc.Default.GetRequiredService<ContentDetailViewModel>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (ViewModel.IsOffline)
            {
                ViewModel.GetLocalContentDetail((AlbumCardInfo)e.Parameter);
                ViewModel.GetLocalContentDetailList((AlbumCardInfo)e.Parameter);
            }
            else
            {
                ViewModel.ContentId = (int)e.Parameter;
            }

            ConnectedAnimation animation =
                ConnectedAnimationService.GetForCurrentView().GetAnimation("ContentToDetailAni");
            if (animation != null)
            {
                animation.TryStart(Cover);
            }
        }

        private void PlayListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(ViewModel.IsOffline)
            {
                var item = e.ClickedItem as AlbumRadioInfo;
                ViewModel.PlayContent(item.ProgramId);
            }
            else
            {
                var item = e.ClickedItem as ContentPlaylistDetail;
                ViewModel.PlayContent(item.ProgramId);
            }

        }

        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PlayContent();
        }

        private void ContentTag_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ViewModel.IsOffline)
            {
                //
            }
            else
            {
                var item = e.ClickedItem as AttributesItem;
                if (item != null)
                    ViewModel.NavigateToCategory(item);
            }
        }

        private void PlayListView_ItemRightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void DownloadDetailListItem(object sender, RoutedEventArgs e)
        {
            ContentPlaylistDetail contentPlaylistDetail = ((MenuFlyoutItem)sender).DataContext as ContentPlaylistDetail;
            ViewModel.DownloadContentDetailListItem(contentPlaylistDetail);
        }

        private void RadioItemButton_Click(object sender, RoutedEventArgs e)
        {
            ContentPlaylistDetail contentPlaylistDetail = ((MenuFlyoutItem)sender).DataContext as ContentPlaylistDetail;
            ViewModel.RemoveContentDetailListItem(contentPlaylistDetail);
        }
    }
}
