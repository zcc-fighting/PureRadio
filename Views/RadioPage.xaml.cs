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

namespace PureRadio.Views
{
    public sealed partial class RadioPage:Page
    {
        private RadioViewModel ViewModel { get; set; } = new RadioViewModel();
        private int _radioColumn;
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
    }
}
