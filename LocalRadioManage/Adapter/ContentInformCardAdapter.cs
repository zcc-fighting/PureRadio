using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using PureRadio.Uwp.Models.Data.User;

namespace PureRadio.LocalRadioManage.Adapter
{
   public static class ContentInformCardAdapter
    {
        public static DataModels.RadioFullAlbum ContentInfoToRadioFullAlum(ContentInfoCard content)
        {
            RadioFullAlbum album = new RadioFullAlbum();
            album.remote_cover = content.Cover;
            album.id = content.ContentId;
            album.description = content.Description;
            album.title = content.Title;
            album.type = 1;
            return album;
        }

        //内容专辑转换为本地存储
        public static ContentInfoCard RadioFullAlumToContentInfo(RadioFullAlbum album)
        {
            ContentInfoCard content = new ContentInfoCard(album.id, album.title,"", album.local_cover.AbsolutePath, album.description,"");
            return content;
        }

    }
}
