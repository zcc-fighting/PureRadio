using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalRadioManage.DataModelsL;
using PureRadio.LocalRadioManage.DBBuilder.TableObj;

namespace PureRadio.LocalRadioManage.DataModelTransform
{
    class ChannalCardTransform
    {
        public static List<object> InfoToStore(ChannalCardInfo info)
        {
            object[] store = new object[ChannalCard.ColLocation.Count];
            store[ChannalCard.ColLocation[ChannalCard.ChannalId]] = info.RadioId;
            store[ChannalCard.ColLocation[ChannalCard.CityId]] = info.CityId;
            if (info.Cover != null)
            {
                store[ChannalCard.ColLocation[ChannalCard.Cover]] = info.Cover.AbsoluteUri;
            }
            store[ChannalCard.ColLocation[ChannalCard.Description]] = info.Description;
            if (info.LocalCover != null)
            {
                store[ChannalCard.ColLocation[ChannalCard.LocalCover]] = info.LocalCover.AbsoluteUri;
            }
            //store[ChannalCard.ColLocation[ChannalCard.QuoteCounts]] = info.QuoteCounts;
            store[ChannalCard.ColLocation[ChannalCard.RegionId]] = info.RegionId;
            store[ChannalCard.ColLocation[ChannalCard.Title]] = info.Title;
            store[ChannalCard.ColLocation[ChannalCard.TopCategoryId]] = info.TopCategoryId;
            store[ChannalCard.ColLocation[ChannalCard.TopCategoryTitle]] = info.TopCategoryTitle;
            store[ChannalCard.ColLocation[ChannalCard.UpdateTime]] = info.UpdateTime;

            return store.ToList();
        }

        public static ChannalCardInfo StoreToInfo(List<object> store)
        {
            ChannalCardInfo info = new ChannalCardInfo();
             info.RadioId=(int)(long)store[ChannalCard.ColLocation[ChannalCard.ChannalId]];
            info.CityId= (int)(long)store[ChannalCard.ColLocation[ChannalCard.CityId]] ;
            if (store[ChannalCard.ColLocation[ChannalCard.Cover]] != null)
            {
                try
                {
                    info.Cover = new Uri((string)store[ChannalCard.ColLocation[ChannalCard.Cover]]);
                }
                catch
                {

                }
            }
            if (store[ChannalCard.ColLocation[ChannalCard.Description]] != null)
            {
                info.Description = (string)store[ChannalCard.ColLocation[ChannalCard.Description]];
            }
            if (store[ChannalCard.ColLocation[ChannalCard.LocalCover]] != null)
            {
                try
                {
                    info.LocalCover = new Uri((string)store[ChannalCard.ColLocation[ChannalCard.LocalCover]]);
                }
                catch
                {

                }
            }
           // info.QuoteCounts=(int)store[ChannalCard.ColLocation[ChannalCard.QuoteCounts]];
            info.RegionId=(int)(long)store[ChannalCard.ColLocation[ChannalCard.RegionId]] ;
            if (store[ChannalCard.ColLocation[ChannalCard.Title]] != null)
            {
                info.Title = (string)store[ChannalCard.ColLocation[ChannalCard.Title]];
            }
            info.TopCategoryId=(int)(long)store[ChannalCard.ColLocation[ChannalCard.TopCategoryId]] ;
            if (store[ChannalCard.ColLocation[ChannalCard.TopCategoryTitle]] != null)
            {
                info.TopCategoryTitle = (string)store[ChannalCard.ColLocation[ChannalCard.TopCategoryTitle]];
            }
            if (store[ChannalCard.ColLocation[ChannalCard.UpdateTime]] != null)
            {
                info.UpdateTime = (string)store[ChannalCard.ColLocation[ChannalCard.UpdateTime]];
            }
            return info;
        }
    }
}
