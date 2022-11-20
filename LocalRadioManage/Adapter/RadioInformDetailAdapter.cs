using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using PureRadio.Uwp.Models.Data.User;

namespace PureRadio.LocalRadioManage.Adapter
{
    public static class RadioInformDetailAdapter
    {
       public static  RadioInfoDetail RadioFullContentToRadioInfoDetail(RadioFullContent radio)
        {
            RadioInfoDetail detail = new RadioInfoDetail(radio.id, radio.title, null, "", "", "", 0, "", 0, 0, TimeSpan.Zero, radio.start_time, radio.end_time);
            return detail;
        }

        public static RadioInfoDetail RadioFullContentToRadioInfoDetail(RadioFullAlbum album, RadioFullContent radio)
        {
            RadioInfoDetail detail = RadioFullContentToRadioInfoDetail(radio);
            detail.Cover = album.local_cover;
            detail.Description = album.description;
            return detail;
        }

    }
}
