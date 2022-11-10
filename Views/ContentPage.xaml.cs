using PureRadio.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PureRadio.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ContentPage : Page
    {
        public ContentPage()
        {
            this.InitializeComponent();
        }

        private readonly List<(string Tag, Type Page)> _pages = new List<(string Tag, Type Page)>
        {
            ("545", typeof(ProgramContentPage)),
            ("0", typeof(ProgramContentJumpPage)),
        };

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private void ContentTypeNavView_Loaded(object sender, RoutedEventArgs e)
        {
            ContentTypeNavView.SelectedItem = ContentTypeNavView.MenuItems[0];
            ContentTypeNavView_Navigate("545", new Windows.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());
        }

        private void ContentTypeNavView_ItemInvoked(muxc.NavigationView sender, muxc.NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                ContentTypeNavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void ContentTypeNavView_Navigate(
            string navItemTag,
            Windows.UI.Xaml.Media.Animation.NavigationTransitionInfo transitionInfo)
        {
            Type _page = null;
            var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
            _page = item.Page;
            string categories_id = item.Tag;
            // Get the page type before navigation so you can prevent duplicate
            // entries in the backstack.
            var preNavPageType = ContentView.CurrentSourcePageType;
            // Only navigate if the selected page isn't currently loaded.
            if (!(_page is null))
            {
                ContentView.Navigate(_page, categories_id, transitionInfo);
            }
        }
    }
}
