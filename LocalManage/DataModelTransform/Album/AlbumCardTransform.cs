using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalRadioManage.DataModelsL;
using PureRadio.LocalRadioManage.DBBuilder.TableObj;

namespace PureRadio.LocalRadioManage.DataModelTransform
{
    class AlbumCardTransform
    {
        public static List<object> InfoToStore(AlbumCardInfo info)
        {
            object[] store = new object[AlbumCard.ColLocation.Count];
            store[AlbumCard.ColLocation[AlbumCard.AlbumId]] = info.ContentId;
            store[AlbumCard.ColLocation[AlbumCard.CategoryId]] = info.CategoryId;
            store[AlbumCard.ColLocation[AlbumCard.ContentType]] = info.ContentType;
            if (info.Cover != null)
            {
                store[AlbumCard.ColLocation[AlbumCard.Cover]] = info.Cover.AbsoluteUri;
            }
            store[AlbumCard.ColLocation[AlbumCard.Description]] = info.Description;
            if (info.LocalCover != null)
            {
                store[AlbumCard.ColLocation[AlbumCard.LocalCover]] = info.LocalCover.AbsoluteUri;
            }
            store[AlbumCard.ColLocation[AlbumCard.Podcaster]] = info.Podcasters;
            //store[AlbumCard.ColLocation[AlbumCard.QuoteCounts]] = info.QuoteCounts;
            store[AlbumCard.ColLocation[AlbumCard.Rating]] = info.Rating;
            store[AlbumCard.ColLocation[AlbumCard.Title]] = info.Title;
            store[AlbumCard.ColLocation[AlbumCard.Version_]] = info.Version;
            return store.ToList();
        }

        public static AlbumCardInfo StoreToInfo(List<object> store)
        {
            AlbumCardInfo info = new AlbumCardInfo();
            info.ContentId = (int)(long)store[AlbumCard.ColLocation[AlbumCard.AlbumId]];
            info.CategoryId = (int)(long)store[AlbumCard.ColLocation[AlbumCard.CategoryId]];
            if (store[AlbumCard.ColLocation[AlbumCard.ContentType]].ToString() != null)
            {
                info.ContentType = (string)store[AlbumCard.ColLocation[AlbumCard.ContentType]];
            }
            if (store[AlbumCard.ColLocation[AlbumCard.Cover]] != null)
            {
                try
                {
                    info.Cover = new Uri((string)store[AlbumCard.ColLocation[AlbumCard.Cover]]);
                }
                catch
                {

                }
            }
            if (store[AlbumCard.ColLocation[AlbumCard.Description]] != null)
            {
                info.Description = (string)store[AlbumCard.ColLocation[AlbumCard.Description]];
            }
            if (store[AlbumCard.ColLocation[AlbumCard.LocalCover]] != null)
            {
                try
                {
                    info.LocalCover = new Uri((string)store[AlbumCard.ColLocation[AlbumCard.LocalCover]]);
                }
                catch
                {

                }
            }
            if (store[AlbumCard.ColLocation[AlbumCard.Podcaster]] != null)
            {
                info.Podcasters = (string)store[AlbumCard.ColLocation[AlbumCard.Podcaster]];
            }
            //info.QuoteCounts =(int)store[AlbumCard.ColLocation[AlbumCard.QuoteCounts]];
            try
            {
                info.Rating = (float)(double)store[AlbumCard.ColLocation[AlbumCard.Rating]];
            }
            catch
            {

            }
        
            if (store[AlbumCard.ColLocation[AlbumCard.Title]] != null)
            {
                info.Title = (string)store[AlbumCard.ColLocation[AlbumCard.Title]];
            }
            if (store[AlbumCard.ColLocation[AlbumCard.Version_]] != null)
            {
                info.Version = (string)store[AlbumCard.ColLocation[AlbumCard.Version_]];
            }

            return info;
        }


    }
}
