using Microsoft.Toolkit.Mvvm.ComponentModel;
using PureRadio.DataModel;
using PureRadio.DataModel.Results;
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
        public List<List<RadioCategoriesItem>> _categoriesRadioList = new List<List<RadioCategoriesItem>>();
        private List<LeaderboardItem> _rankRadioItems = new List<LeaderboardItem>();
        public List<RadioCategoriesItem> _categoriesRadioItems = new List<RadioCategoriesItem>();
        private List<List<RadioCategoriesItem>> _categoriesRadioListItems = new List<List<RadioCategoriesItem>>();
        
        private bool _rankRadioEmptyReplace = true;
        private bool _categoryRadioEmptyReplace = true;

        private int _rankRadioTotal = 12;
        private int _categoryRadioTotal = 12;

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
            set=> SetProperty(ref _categoriesRadio, value);
        }
        public List<List<RadioCategoriesItem>> CategoriesRadioList
        {
            get => _categoriesRadioList;
            set => SetProperty(ref _categoriesRadioList, value);
        }

        public bool RankRadioEmptyReplace
        {
            get=>_rankRadioEmptyReplace;
            set=> SetProperty(ref _rankRadioEmptyReplace, value);
        }

        public bool CategoryRadioEmptyReplace
        {
            get=> _categoryRadioEmptyReplace;
            set=> SetProperty(ref _categoryRadioEmptyReplace, value);
        }

        public void UpdateRadioInfo()
        {
            //根据网络状态获取网络或本地数据
            if ((bool)Windows.Storage.ApplicationData.Current.LocalSettings.Values["CurrentNetworkMode"])
            {
                _rankRadioItems = RadioRank.Radios("407");
                
            }
            else
            {
                _rankRadioItems= RadioRank.Radios("409");
            }

            //热门电台数据获取失败
            if (_rankRadioItems == null|| _rankRadioItems.Count==0)
            {
                RankRadioEmptyReplace = true;
            }
            else
            {
                RankRadio = _rankRadioItems.GetRange(0, _rankRadioTotal);
                RankRadioEmptyReplace = false;
            }

            if(_categoriesRadioListItems == null)
            {
                CategoryRadioEmptyReplace = true;
            }
            else
            {
                //CategoriesRadioList = _categoriesRadioList;
            }
        }
        //加载全部数据
        public void UpdateRankRadio()
        {
            RankRadio = _rankRadioItems;
            _rankRadioTotal = _rankRadioItems.Count;
        }
        //加载指定数目的数据
        public void UpdateRankRadio(int total)
        {
            if(_rankRadioEmptyReplace)
            {
                _rankRadioTotal = 0;
            }
            else
            {
                _rankRadioTotal = total;
            }
            RankRadio = _rankRadioItems.GetRange(0, _rankRadioTotal);
        }

        public void UpdateCategoriesRadio(int total)
        {
            _categoryRadioTotal = total;
            _categoriesRadioList = _categoriesRadioListItems.GetRange(0, _categoryRadioTotal);
        }
    }
}
