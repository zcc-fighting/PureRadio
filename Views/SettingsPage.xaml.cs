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
using PureRadio.ViewModels;
using muxc = Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.Core;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.Storage;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PureRadio.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private SettingsViewModel ViewModel { get; set; } = new SettingsViewModel();


        public SettingsPage()
        {
            this.InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            switch (ViewModel.Theme)
            {
                case ElementTheme.Default:
                    ThemeRadioButtons.SelectedItem = ThemeRadioButtonSystem;
                    break;
                case ElementTheme.Light:
                    ThemeRadioButtons.SelectedItem = ThemeRadioButtonLight;
                    break;
                case ElementTheme.Dark:
                    ThemeRadioButtons.SelectedItem = ThemeRadioButtonDark;
                    break;
                default:
                    break;
            }
            switch (ViewModel.Language)
            {
                case "zh-CN":
                    LanguageRadioButtonZHCN.IsChecked = true;
                    break;
                case "en-US":
                    LanguageRadioButtonENUS.IsChecked = true;
                    break;
                default:
                    LanguageRadioButtonSystem.IsChecked = true;
                    break;
            }
            switch(ViewModel.NetworkStatus)
            {
                case true:
                    ConnectNetworkToggleSwitch.IsOn = true;
                    break;
                case false:
                    ConnectNetworkToggleSwitch.IsOn = false;
                    break;
            }
        }

        private void ThemeRadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (e.AddedItems.Any() && e.AddedItems.First() is RadioButton btn)
            {
                var selectTheme = ElementTheme.Default;
                if (btn == ThemeRadioButtonLight)
                {
                    selectTheme = ElementTheme.Light;
                }
                else if (btn == ThemeRadioButtonDark)
                {
                    selectTheme = ElementTheme.Dark;
                }
                ViewModel.Theme = selectTheme;
            }
        }

        private void LanguageRadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Any() && e.AddedItems.First() is RadioButton btn)
            {
                DisplayRestartDialog();
            }
        }

        private async void DisplayRestartDialog()
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            ContentDialog restartDialog = new ContentDialog
            {
                Title = resourceLoader.GetString("SettingsLanguageRestartTitle"),
                Content = resourceLoader.GetString("SettingsLanguageRestartSecondary"),
                PrimaryButtonText = resourceLoader.GetString("SettingsLanguageRestartOK"),
                CloseButtonText = resourceLoader.GetString("SettingsLanguageRestartCancel"),
                DefaultButton = ContentDialogButton.Primary,
                RequestedTheme = ViewModel.Theme
            };
            ContentDialogResult result = await restartDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var btn = LanguageRadioButtons.SelectedItem;
                if (btn == LanguageRadioButtonZHCN)
                {
                    ViewModel.Language = "zh-CN";
                }
                else if (btn == LanguageRadioButtonENUS)
                {
                    ViewModel.Language = "en-US";
                }
                else
                {
                    ViewModel.Language = "auto";
                }
                await CoreApplication.RequestRestartAsync(string.Empty);
            }
            else
            {
                // Do nothing.
            }
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (sender != null)
            {
                if (toggleSwitch.IsOn == true)
                {
                    TimerPickerTitle.Visibility = Visibility.Collapsed;
                    TimerPicker.Visibility = Visibility.Collapsed;
                    TimerDelay.Visibility = Visibility.Visible;
                }
                else
                {
                    TimerPickerTitle.Visibility = Visibility.Visible;
                    TimerPicker.Visibility = Visibility.Visible;
                    TimerDelay.Visibility = Visibility.Collapsed;
                    ViewModel.TimerStatus = false;
                    TimerPicker.SelectedTime = TimeSpan.Zero;
                }
            }
        }

        private void TimerPicker_SelectedTimeChanged(TimePicker sender, TimePickerSelectedValueChangedEventArgs args)
        {
            if (ViewModel.Delay == TimeSpan.Zero)
            {
                TimerToggle.IsEnabled = false;
            }
            else
            {
                TimerToggle.IsEnabled = true;
            }
        }

        private void ConnectNetworkToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            RefreshPages();
        }
        private void RefreshPages()
        {
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();

        }



        private async void ClearCacheButton_Click(object sender, RoutedEventArgs e)
        {
            var cacheFolder = ApplicationData.Current.LocalCacheFolder;
            ClearCacheRing.Visibility = Visibility.Visible;
            ClearCacheButton.IsEnabled = false;

            //var resourceToolkit = ServiceLocator.Instance.GetService<IResourceToolkit>();

            try
            {
                var children = await cacheFolder.GetItemsAsync();
                foreach (var child in children)
                {
                    if (child is StorageFile file)
                    {
                        await file.DeleteAsync();
                    }
                    else if (child is StorageFolder folder)
                    {
                        await folder.DeleteAsync();
                    }
                }

            }
            catch
            {
            }
            finally
            {
                ClearCacheRing.Visibility = Visibility.Collapsed;
                ClearCacheButton.IsEnabled = true;
            }
        }

       
    }
}
