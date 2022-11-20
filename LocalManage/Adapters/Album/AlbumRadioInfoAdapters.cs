using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.Uwp.Models.Data.Content;
using PureRadio.LocalRadioManage.DataModelsL;

namespace PureRadio.LocalManage.Adapters
{
    class AlbumRadioInfoAdapters
    {
     public static  ContentPlaylistDetail  ToContentPlaylistDetail(AlbumRadioInfo radio)
        {
            ContentPlaylistDetail detail = new ContentPlaylistDetail
                (
                radio.Version,
                radio.ProgramId,
                radio.Title,
                0,
                radio.UpdateTime,
                0,
                false,
                null,
                0,
                ""
                );
            return detail;
        }

    }
}
