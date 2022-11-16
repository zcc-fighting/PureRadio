using PureRadio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using CommunityToolkit.Mvvm.DependencyInjection;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Enums;

namespace PureRadio.Views
{
    public sealed partial class RadioPage:Page
    {
        public RadioViewModel ViewModel => (RadioViewModel)DataContext;
        public RadioPage()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<RadioViewModel>();
        }
        private void RadioGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null && e.ClickedItem is RadioInfoSearch radioInfo)
                ViewModel.Navigate(PageIds.RadioDetail, radioInfo.RadioId);
        }
    }
}
