using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using PureRadio.Uwp.Models.Data.Radio;

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
            detail.Cover = album.cover;
            detail.Description = album.description;
            return detail;
        }

        public static RadioFullContent RadioInfoDetailToRadioFullContent(RadioInfoDetail detail)
        {
            RadioFullContent radio = new RadioFullContent();
            radio.channel_id = detail.RadioId;
            radio.start_time = detail.StartTIme;
            radio.end_time = detail.EndTime;
            //radio.date=detail.
            return radio;
        }
    }
}
