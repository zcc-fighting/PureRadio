using CommunityToolkit.Mvvm.DependencyInjection;
using PureRadio.LocalManage.DataModelsL;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Views.Secondary;
using PureRadio.ViewModels.LocalRadioManageViewModels;
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
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PureRadio.Uwp.Views.Main
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LocalLibraryPage : Page
    {
        public LocalLibraryViewModel ViewModel => (LocalLibraryViewModel)DataContext;
        public LocalLibraryPage()
        {
            this.InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<LocalLibraryViewModel>();
        }


        private void RadioItemButton_Click(object sender, RoutedEventArgs e)
        {
            ChannalCardInfo channalCardInfo = ((Button)sender).DataContext as ChannalCardInfo;
            ViewModel.RemoveRadio(channalCardInfo);
        }

        private void ContentItemButton_Click(object sender, RoutedEventArgs e)
        {
            AlbumCardInfo albumCardInfo = ((Button)sender).DataContext as AlbumCardInfo;
            ViewModel.RemoveContent(albumCardInfo);
        }

        private void RadioListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null && e.ClickedItem is ChannalCardInfo radio)
            {
                ViewModel.Navigate(PageIds.RadioDetail, radio);
            }
        }

        private void ContentListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null && e.ClickedItem is AlbumCardInfo content)
            {
                ViewModel.Navigate(PageIds.ContentDetail, content);
            }
        }
    }
}
