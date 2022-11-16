using PureRadio.DataModel;
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

namespace PureRadio.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ProgramContentJumpPage : Page
    {
        private List<ProgramJumpItem> _programs;
        public ProgramContentJumpPage()
        {
            this.InitializeComponent();
            this.Loaded += ProgramContentJumpPage_Loaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private void ProgramContentJumpPage_Loaded(object sender, RoutedEventArgs e)
        {
            _programs = ProgramJump.ProgramJumpResult();
            ProgramResult.ItemsSource = _programs;
        }

        private void ProgramResult_ItemClick(object sender, ItemClickEventArgs e)
        {
            ProgramJumpItem item = e.ClickedItem as ProgramJumpItem;
            ProgramJumpPage.Navigate(typeof(ProgramContentPage), item.Id);
        }
    }
}
