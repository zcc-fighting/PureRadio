using Microsoft.Toolkit.Mvvm.ComponentModel;
using PureRadio.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.ViewModels
{
    public sealed partial class RadioViewModel: ObservableRecipient
    {
        public List<LeaderboardItem> _rankRadio = new List<LeaderboardItem>();
        public List<RadioCategoriesItem> _categoriesRadio = new List<RadioCategoriesItem>();
        private List<LeaderboardItem> _rankRadioItems = new List<LeaderboardItem>();
        private List<RadioCategoriesItem> _categoriesRadioItems = new List<RadioCategoriesItem>();
        bool _rankRadioEmptyReplace = true;
        bool _categoryRadioEmptyReplace = true;

        private int _rankRadioTotal = 12;
        private int _categoryRadioTotal = 6;

        public RadioViewModel()
        {
            IsActive = true;
        }

        protected override void OnActivated()
        {
            UpdateRadioInfo();
        }
        public List<LeaderboardItem> RankRadio
        {
            get=> _rankRadio;
            set=> SetProperty(ref _rankRadio, value);
        }
        public List<RadioCategoriesItem> CategoryRadio
        {
            get => _categoriesRadio;
            set => SetProperty(ref _categoriesRadio, value);
        }

        public bool RankRadioEmptyReplace
        {
            get => _rankRadioEmptyReplace;
            set=> SetProperty(ref _rankRadioEmptyReplace, value);
        }

        public bool CategoryRadioEmptyReplace
        {
            get=> _categoryRadioEmptyReplace;
            set=> SetProperty(ref _categoryRadioEmptyReplace, value);
        }

        public void UpdateRadioInfo()
        {
            List<LeaderboardItem> rankRadioItems = new List<LeaderboardItem>();
            List<RadioCategoriesItem> categoriesRadioItems = new List<RadioCategoriesItem>();

            if(rankRadioItems==null||rankRadioItems.Count==0)
            {

            }
            else
            {

            }

            if(categoriesRadioItems==null)
            {

            }
            else
            {

            }
        }

        public void UpdateRankRadio(int total)
        {
            _rankRadioTotal = total;
            _rankRadio = _rankRadioItems.GetRange(0, _rankRadioTotal);
        }

        public void UpdateCategoriesRadio(int total)
        {
            _categoryRadioTotal = total;
            _categoriesRadio = _categoriesRadioItems.GetRange(0, _categoryRadioTotal);
        }
    }
}
