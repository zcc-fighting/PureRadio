using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using PureRadio.Uwp.Models.Data.Content;

namespace PureRadio.LocalRadioManage.Adapter
{
   public static class ContentInformDetailAdapter
    {
        public static ContentInfoDetail RadioFullContentToContentInfoDetail(RadioFullContent radio)
        {
            ContentInfoDetail detail = new ContentInfoDetail(null, radio.id, "", radio.title, "", 0, "", 0, radio.procasters, 0, "");
            return detail;
        }

        public static ContentInfoDetail RadioFullContentToContentInfoDetail(RadioFullAlbum album,RadioFullContent radio)
        {
            ContentInfoDetail detail = RadioFullContentToContentInfoDetail(radio);
            detail.Cover = album.local_cover;
            detail.Description = album.description;
            return detail;
        }



    }
}
