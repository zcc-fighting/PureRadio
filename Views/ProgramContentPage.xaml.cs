using Microsoft.Toolkit.Mvvm.Messaging;
using PureRadio.DataModel;
using PureRadio.DataModel.Parameter;
using PureRadio.DataModel.Results;
using PureRadio.ViewModels;
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
    public sealed partial class ProgramContentPage : Page
    {
        private ContentIncrementalLoading<ContentItem> _programs;
        private string content_id;
        public ProgramContentPage()
        {
            this.InitializeComponent();
            this.Loaded += ContentPage_Loaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            content_id = e.Parameter?.ToString();
        }

        private void ContentPage_Loaded(object sender, RoutedEventArgs e)
        {
            if ((bool)Windows.Storage.ApplicationData.Current.LocalSettings.Values["CurrentNetworkMode"])
            {
                ProgramResult.IncrementalLoadingTrigger = IncrementalLoadingTrigger.Edge;
                ProgramResult.DataFetchSize = 4.0;
                ProgramResult.IncrementalLoadingThreshold = 2.0;
                _programs = new ContentIncrementalLoading<ContentItem>(2000, (startIndex, count) =>
                {
                    //Debug.WriteLine("从索引 {0} 处开始获取 {1} 条数据", startIndex, count);
                    return ContentResult.Content(content_id, startIndex);
                });
                ProgramResult.ItemsSource = _programs;
            }
            else
            {
                ProgramResult.ItemsSource = null;
                this.Frame.Navigate(typeof(NULLPage));
            }
        }

        private void ProgramResult_ItemClick(object sender, ItemClickEventArgs e)
        {
            ContentItem content = e.ClickedItem as ContentItem;
            if (content != null) WeakReferenceMessenger.Default.Send(new NavToProgramDetailMessage(new ProgramShot(content.id)));
        }
    }
}
