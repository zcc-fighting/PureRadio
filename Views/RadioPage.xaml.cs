using PureRadio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using PureRadio.DataModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Mvvm.Messaging;
using PureRadio.DataModel.Parameter;
using System.Diagnostics;

namespace PureRadio.Views
{
    public sealed partial class RadioPage:Page
    {
        private RadioViewModel ViewModel { get; set; } = new RadioViewModel();
        private int _rankRadioColumn;
        //private int _categoriesRadioColumn;

        public RadioPage()
        {
            this.InitializeComponent();
        }
        private void RankRadio_ItemClick(object sender, ItemClickEventArgs e)
        {
            LeaderboardItem radioItem = e.ClickedItem as LeaderboardItem;
            if (radioItem != null)
            {
                WeakReferenceMessenger.Default.Send(new NavToRadioDetailMessage(new RadioShot(radioItem.content_id)));
            }
        }

        private void RankRadio_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            int columnCount = (int)(RankRadioView.ActualWidth / (DynamicBridgeCard.Width + 12));
            if (columnCount != _rankRadioColumn)
            {
                _rankRadioColumn = columnCount;
                if (_rankRadioColumn < 3) ViewModel.UpdateRankRadio(10);
                else if (_rankRadioColumn > 8) ViewModel.UpdateRankRadio(24);
                else ViewModel.UpdateRankRadio(_rankRadioColumn * 3);
            }
        }

        private void CategoriesRadio_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void RadioMoreItemsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MoreRadioPage));
        }
    }
}
