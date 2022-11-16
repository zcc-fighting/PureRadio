using DataModels;
using PureRadio.DataModel.Parameter;
using PureRadio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;


namespace PureRadio.Views
{
    public sealed partial class DetailRadioPage : Page
    {
        private DetailRadioViewModel ViewModel { get; set; } = new DetailRadioViewModel();

        private string _nowpagetag;

        public DetailRadioPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null) ViewModel.SetChannel((RadioShot)e.Parameter);
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            // NavView doesn't load any page by default, so load home page.
            NavList.SelectedItem = NavList.MenuItems[1];
            // If navigation occurs on SelectionChanged, this isn't needed.
            // Because we use ItemInvoked to navigate, we need to call Navigate
            // here to load the home page.
            NavView_Navigate("today", new Windows.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());
        }

        private void NavView_Navigate(
            string navItemTag,
            Windows.UI.Xaml.Media.Animation.NavigationTransitionInfo transitionInfo)
        {
            _nowpagetag = navItemTag;
            ViewModel.ChangePlayList(navItemTag);
        }

        private void NavView_ItemInvoked(muxc.NavigationView sender,
                                         muxc.NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                NavView_Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void PlayListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            RadioFullContent fullContent = e.ClickedItem as RadioFullContent;
            int selected_index = PlayListView.Items.IndexOf(fullContent);
            if (fullContent != null)
            {
                //int content_day = fullContent.day;
                string date;
                if (_nowpagetag == "yesterday") date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                else if (_nowpagetag == "today") date = DateTime.Now.ToString("yyyy-MM-dd");
                else date = DateTime.Now.AddDays(+1).ToString("yyyy-MM-dd");
                DateTime endTime = DateTime.Parse(date + " " + fullContent.end_time);
                DateTime now = DateTime.Now;
                if (endTime < now)
                {
                    ViewModel.PlayRadioContent(selected_index, _nowpagetag);
                }
                else
                {
                    CantPlayTip.IsOpen = true;
                    //Debug.Print("Not availab");
                }
            }
        }
    }

}
