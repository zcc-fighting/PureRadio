using Microsoft.Toolkit.Mvvm.Messaging;
using PureRadio.DataModel;
using PureRadio.DataModel.Parameter;
using PureRadio.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PureRadio.Views
{
    public sealed partial class MoreRadioPage:Page
    {
        private RadioViewModel ViewModel { get; set; } = new RadioViewModel();

        public MoreRadioPage()
        {
            InitializeComponent();
            ViewModel.UpdateRankRadio();
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
