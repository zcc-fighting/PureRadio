using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
using PureRadio.Uwp.Providers.Interfaces;
using PureRadio.Uwp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PureRadio.ViewModels
{
    public sealed partial class RadioViewModel: ObservableRecipient
    {
        private readonly INavigateService navigate;
        private readonly IRadioProvider radioProvider;

        [ObservableProperty]
        private bool _isRadioModuleShown;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private bool _isEmpty;

        [ObservableProperty]
        private bool _noResult;

        public ICommand RadioResultCommand { get; }
        public IncrementalLoadingObservableCollection<RadioInfoCard> RadioResult { get; set; }

        public RadioViewModel(
            INavigateService navigate,
            IRadioProvider radioProvider)
        {
            this.navigate = navigate;
            this.radioProvider = radioProvider;

            RadioResult = new IncrementalLoadingObservableCollection<RadioInfoCard>(GetRadioResult);

            RadioResultCommand = new RelayCommand(SetRadioResult);

            IsActive = true;

            IsRadioModuleShown = true;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            RadioResult.OnStartLoading += StartLoading;
            RadioResult.OnEndLoading += EndLoading;        }

        protected override void OnDeactivated()
        {
            RadioResult.OnStartLoading -= StartLoading;
            RadioResult.OnEndLoading -= EndLoading;
            base.OnDeactivated();
        }

        private async Task<IEnumerable<RadioInfoCard>> GetRadioResult(CancellationToken cancelToken)
        {
            var resultSet = await radioProvider.GetRankRadio(cancelToken);
            return resultSet.ToList();
        }

        private void StartLoading()
        {
            IsLoading = true;
        }

        private void EndLoading()
        {
            IsLoading = false;
            if (IsRadioModuleShown)
            {
                IsEmpty = RadioResult.Count == 0;
            }
            else
            {
                IsEmpty = true;
            }
        }

        private void SetRadioResult()
        {
            IsRadioModuleShown = true;
        }


        public void Navigate(PageIds pageId, object parameter = null)
        {
            if (pageId == PageIds.RadioDetail || pageId == PageIds.ContentDetail)
            {
                navigate.NavigateToSecondaryView(pageId, parameter);
            }
        }
    }
}
