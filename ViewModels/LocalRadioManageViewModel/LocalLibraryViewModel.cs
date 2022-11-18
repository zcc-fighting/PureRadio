using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.System.Threading;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using DataModels;
using LocalRadioManage.LocalService;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Data.Content;

namespace PureRadio.ViewModels.LocalRadioManageViewModels
{
    class LocalLibraryViewModel : ObservableRecipient
    {
        private LocalServ.LocalDown DownMan = new LocalServ.LocalDown();
        private List<DataModels.RadioFullAlbum> all_down_albums { get; set; } = new List<DataModels.RadioFullAlbum>();
        private List<DataModels.RadioFullContent> album_down_radios { get; set; } = new List<DataModels.RadioFullContent>();

        //下载电台详情合集
        public List<RadioInfoCard> down_radio_albums { get; set; } = new List<RadioInfoCard>();
        //下载专辑详情合集
        public List<ContentInfoCard> down_content_albums { get; set; } = new List<ContentInfoCard>();
        //电台音频合集
        public List<RadioInfoDetail> down_radio_details { get; set; } = new List<RadioInfoDetail>();
        //专辑音频合集
        public List<ContentInfoDetail> down_content_details { get; set; } = new List<ContentInfoDetail>();

        //获取所有本地专辑
        public void GetAllAlbums()
        {
            all_down_albums = DownMan.Load();
            down_radio_albums.Clear();
            down_content_albums.Clear();

            foreach (DataModels.RadioFullAlbum album in all_down_albums)
            {
                if (album.type == 0)//电台
                {
                    down_radio_albums.Add(LocalRadioManage.Adapter.RadioInformCardAdapter.RadioFullAlumToRadioInfo(album));
                }
                else if (album.type == 1)//专辑
                {
                    down_content_albums.Add(LocalRadioManage.Adapter.ContentInformCardAdapter.RadioFullAlumToContentInfo(album));
                }
            }

        }
        //获取某一专辑的下载音频
        public void GetRadios(DataModels.RadioFullAlbum album)
        {
            album_down_radios = DownMan.Load(album, false);
            down_radio_details.Clear();
            down_content_details.Clear();

            foreach (DataModels.RadioFullContent radio in album_down_radios)
            {
                if (album.type == 0)//电台
                {
                    down_radio_details.Add(LocalRadioManage.Adapter.RadioInformDetailAdapter.RadioFullContentToRadioInfoDetail(album,radio));
                }
                else if (album.type == 1)//专辑
                {
                    down_content_details.Add(LocalRadioManage.Adapter.ContentInformDetailAdapter.RadioFullContentToContentInfoDetail(album,radio));
                }
            }
        }
       



    }
}

