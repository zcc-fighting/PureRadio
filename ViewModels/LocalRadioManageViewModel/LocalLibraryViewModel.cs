using CommunityToolkit.Mvvm;
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
using PureRadio.Uwp.Models.Data.User;
using LocalRadioManage.LocalService;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PureRadio.ViewModels.LocalRadioManageViewModels
{
    class LocalLibraryViewModel : ObservableRecipient
    {
        private LocalServ.LocalDown DownMan = new LocalServ.LocalDown();

        public List<DataModels.RadioFullAlbum> all_down_albums { get; set; } = new List<DataModels.RadioFullAlbum>();

        public List<DataModels.RadioFullContent> album_down_radios { get; set; } = new List<DataModels.RadioFullContent>();

        //获取所有本地专辑
        public void GetAllAlbums()
        {
            all_down_albums = DownMan.Load();
        }
        //获取某一专辑的下载音频
        public void GetRadios(DataModels.RadioFullAlbum album)
        {
            album_down_radios = DownMan.Load(album, false);
        }
        //0-> 电台/1-> 专辑 
        public void SiftAlbum(int type)
        {
            List<DataModels.RadioFullAlbum> albums = new List<DataModels.RadioFullAlbum>();
            foreach (DataModels.RadioFullAlbum album in albums)
            {
                if (album.type == type)
                {
                    albums.Add(album);
                }
            }
        }



    }
}

