using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.LocalManage.DataModelsL;
using PureRadio.Uwp.Models.Data.Constants;

namespace PureRadio.LocalManage.Adapters
{
    class ChannalRadioInfoAdapters
    {
       

        public static RadioPlaylistDetail ToRadioPlaylistDetail(ChannalRadioInfo info)
        {
            RadioPlaylistDetail detail = new RadioPlaylistDetail(
                info.StartTime,
                info.EndTime,
                0,
                info.Date.Day,
                info.RadioId,
                info.ProgramId,
                info.Title,
                info.Broadcasters
                );
            return detail;
        }

        public static ChannalRadioInfo ToChannalRadioInfo(RadioPlaylistDetail detail)
        {
            ChannalRadioInfo info = new ChannalRadioInfo();
            info.StartTime = detail.StartTime;
            info.EndTime = detail.EndTime;
          

            int today = (int)DateTime.Today.DayOfWeek + 1;
            if (detail.Day - today > 0) today += 7;
            int offset = detail.Day - today;
            DayOfWeek dayOfWeek = (DayOfWeek)(detail.Day - 1);
            info.Date = DateTime.Today.AddDays(offset);

            info.RadioId = detail.RadioId;
            info.ProgramId = detail.ProgramId;
            info.Title = detail.Title;
            info.Broadcasters = detail.Broadcasters;
            info.RemoteUri = new(String.Format(ApiConstants.Radio.OnDemand, info.Date.ToString("yyyyMMdd",null), info.RadioId, info.RadioId, info.Date.ToString("yyyyMMdd", null)
                , info.StartTime.Replace(":", string.Empty), info.EndTime.Replace(":", string.Empty)));
            return info;
        }
    }
}
