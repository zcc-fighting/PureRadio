using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.LocalRadioManage.DataModelsL;

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
    }
}
