using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalRadioManage.DataModelsL;
using PureRadio.Uwp.Models.Data.Radio;

namespace PureRadio.LocalManage.Adapters
{
    class ChannalCardInfoAdapters
    {
        public static RadioInfoCard ToRadioInfoCard(ChannalCardInfo channal )
        {
            string Cover = "";
            if (channal.LocalCover != null)
            {
                Cover = channal.LocalCover.AbsoluteUri;
            }
            else
            {
                Cover = channal.Cover.AbsoluteUri;
            }

            RadioInfoCard card = new RadioInfoCard(
                channal.RadioId,
                channal.RadioId,
                channal.Title,
                channal.Title,
                Cover,
                channal.Description,
                "",
                "",
                TimeSpan.Zero
                );

            return null;

        } 

        public static RadioInfoDetail ToRadioInfoDetail(ChannalCardInfo channal)
        {
            string Cover = "";
            if (channal.LocalCover != null)
            {
                Cover = channal.LocalCover.AbsoluteUri;
            }
            else
            {
                Cover = channal.Cover.AbsoluteUri;
            }
            RadioInfoDetail detail = new RadioInfoDetail(
                channal.RadioId,
                channal.Title,
                Cover,
                channal.Description,
                "",
                "",
                channal.TopCategoryId,
                channal.TopCategoryTitle,
                channal.RadioId,
                channal.CityId,
                TimeSpan.Zero,
                "",
                ""
                );
            return detail;
        }
    }
}
