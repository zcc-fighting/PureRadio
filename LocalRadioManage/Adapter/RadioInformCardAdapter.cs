using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using PureRadio.Uwp.Models.Data.User;

namespace PureRadio.LocalRadioManage.Adapter
{
    public static class RadioInformCardAdapter
    {
        public static DataModels.RadioFullAlbum RadioInfoToRadioFullAlum(RadioInfoCard radio)
        {
            RadioFullAlbum album = new RadioFullAlbum();
            album.local_cover = radio.Cover;
            album.id = radio.RadioId;
            album.description = radio.Description;
            album.title = radio.Title;
            album.type = 0;
            return album;
        }

        //内容专辑转换为本地存储
        public static RadioInfoCard RadioFullAlumToRadioInfo(RadioFullAlbum album)
        {
            RadioInfoCard radio = new RadioInfoCard(album.id, album.title, album.local_cover.AbsolutePath, album.description, "","");
            return radio;
        }

    }
}
