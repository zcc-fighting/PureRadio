using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalManage.DataModelsL;
using PureRadio.LocalManage.DBBuilder.TableObj;

namespace PureRadio.LocalManage.DataModelTransform
{
    class AlbumRadioTransform
    {
        public static List<object> InfoToStore(AlbumRadioInfo info)
        {
            object[] store = new object[AlbumRadio.ColLocation.Count];
            store[AlbumRadio.ColLocation[AlbumRadio.AlbumId]] = info.AlbumId;
            store[AlbumRadio.ColLocation[AlbumRadio.ContentType]] = info.ContentType;
            if (info.LocalUri != null)
            {
                store[AlbumRadio.ColLocation[AlbumRadio.LocalUri]] = info.LocalUri.AbsoluteUri;
            }
            store[AlbumRadio.ColLocation[AlbumRadio.ProgramId]] = info.ProgramId;
           // store[AlbumRadio.ColLocation[AlbumRadio.QuoteCounts]] = info.QuoteCounts;
            store[AlbumRadio.ColLocation[AlbumRadio.RemoteUri]] = info.RemoteUri.AbsoluteUri;
            store[AlbumRadio.ColLocation[AlbumRadio.Title]] = info.Title;
            store[AlbumRadio.ColLocation[AlbumRadio.UpdateTime]] = info.UpdateTime;
            store[AlbumRadio.ColLocation[AlbumRadio.Version_]] = info.Version;
            store[AlbumRadio.ColLocation[AlbumRadio.Duration]] = info.Duration;
            return store.ToList();
        }

        public static AlbumRadioInfo StoreToInfo(List<object> store)
        {
            AlbumRadioInfo info = new AlbumRadioInfo();
            info.AlbumId =(int)(long)store[AlbumRadio.ColLocation[AlbumRadio.AlbumId]] ;
            if (store[AlbumRadio.ColLocation[AlbumRadio.ContentType]] != null)
            {
                info.ContentType = (string)store[AlbumRadio.ColLocation[AlbumRadio.ContentType]];
            }
            if (store[AlbumRadio.ColLocation[AlbumRadio.LocalUri]] != null){
                try
                {
                    info.LocalUri = new Uri((string)store[AlbumRadio.ColLocation[AlbumRadio.LocalUri]]);
                }
                catch
                {

                }
            }
            info.ProgramId =(int)(long)store[AlbumRadio.ColLocation[AlbumRadio.ProgramId]]  ;
            //info.QuoteCounts = (int)store[AlbumRadio.ColLocation[AlbumRadio.QuoteCounts]]  ;
            if (store[AlbumRadio.ColLocation[AlbumRadio.RemoteUri]] != null)
            {
                try
                {
                    info.RemoteUri = new Uri((string)store[AlbumRadio.ColLocation[AlbumRadio.RemoteUri]]);
                }
                catch
                {

                }
            }
            if (store[AlbumRadio.ColLocation[AlbumRadio.Title]] != null)
            {
                info.Title = (string)store[AlbumRadio.ColLocation[AlbumRadio.Title]];
            }
            if (store[AlbumRadio.ColLocation[AlbumRadio.UpdateTime]] != null)
            {
                info.UpdateTime = (string)store[AlbumRadio.ColLocation[AlbumRadio.UpdateTime]];
            }
            if (store[AlbumRadio.ColLocation[AlbumRadio.Version_]] != null)
            {
                info.Version = (string)store[AlbumRadio.ColLocation[AlbumRadio.Version_]];
            }
            try
            {
                info.Duration = (int)(long)store[AlbumRadio.ColLocation[AlbumRadio.Duration]];
            }
            catch
            {

            }
            return info;
        }
    }
}
