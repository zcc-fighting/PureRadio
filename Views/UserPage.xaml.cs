using Microsoft.Toolkit.Mvvm.Messaging;
using PureRadio.DataModel;
using PureRadio.DataModel.Parameter;
using PureRadio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PureRadio.Views
{
    public sealed partial class UserPage : Page
    {
        private UserViewModel ViewModel { get; set; } = new UserViewModel();

        public UserPage()
        {
            this.InitializeComponent();
        }

        private void revealModeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            passworBox.PasswordRevealMode = PasswordRevealMode.Visible;
        }

        private void revealModeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            passworBox.PasswordRevealMode = PasswordRevealMode.Hidden;
        }

        private async void TryLogin()
        {
            ContentDialogResult result = await LoginDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                if (usernameBox.Text == string.Empty || passworBox.Password == string.Empty)
                {
                    LoginDialogTeachingTip.IsOpen = true;
                }
                else
                {
                    if (!ViewModel.Login(usernameBox.Text, passworBox.Password, "+86")) LoginDialogFailureTip.IsOpen = true;
                    else LoginDialogSuccessTip.IsOpen = true;
                }
            }
            else
            {
                // Do nothing.
            }

        }

        private void AccountLoginButton_Click(object sender, RoutedEventArgs e)
        {
            TryLogin();
        }

        private void AccountLogoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.LogOut()) LogoutDialogSuccessTip.IsOpen = true;
        }

        private void LoginDialog_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args)
        {
            revealModeCheckBox.IsChecked = false;
            usernameBox.Text = string.Empty;
            passworBox.Password = string.Empty;
        }

        private void FavCloud_ItemClick(object sender, ItemClickEventArgs e)
        {
            FavItem favItem = e.ClickedItem as FavItem;
            if (favItem.isRadio)
            {
                WeakReferenceMessenger.Default.Send(new NavToRadioDetailMessage(new RadioShot(favItem.id)));
            }
            else
            {
                WeakReferenceMessenger.Default.Send(new NavToProgramDetailMessage(new ProgramShot(favItem.id)));
            }
        }

        private void FavLocal_ItemClick(object sender, ItemClickEventArgs e)
        {
            FavItem favItem = e.ClickedItem as FavItem;
            if (favItem.isRadio)
            {
                WeakReferenceMessenger.Default.Send(new NavToRadioDetailMessage(new RadioShot(favItem.id)));
            }
            else
            {
                WeakReferenceMessenger.Default.Send(new NavToProgramDetailMessage(new ProgramShot(favItem.id)));
            }
        }
    }
}
