using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalRadioManage.DataModelsL;
using PureRadio.LocalRadioManage.DBBuilder.TableObj;

namespace PureRadio.LocalRadioManage.DataModelTransform
{
    class ChannalRadioTransform
    {
        public static List<object> InfoToStore(ChannalRadioInfo info)
        {
            object[] store = new object[ChannalRadio.ColLocation.Count];
            store[ChannalRadio.ColLocation[ChannalRadio.ChannalId]] = info.RadioId;
            store[ChannalRadio.ColLocation[ChannalRadio.Broadcasters]] = info.Broadcasters;
            store[ChannalRadio.ColLocation[ChannalRadio.Date]] = info.Date.ToString("yyyyMMdd",null);
            store[ChannalRadio.ColLocation[ChannalRadio.EndTime]] = info.EndTime;
            if (info.LocalUri != null)
            {
                store[ChannalRadio.ColLocation[ChannalRadio.LocalUri]] = info.LocalUri.AbsoluteUri;
            }
            store[ChannalRadio.ColLocation[ChannalRadio.ProgramId]] = info.ProgramId;
            //store[ChannalRadio.ColLocation[ChannalRadio.QuoteCounts]] = info.QuoteCounts;
            store[ChannalRadio.ColLocation[ChannalRadio.RemoteUri]] = info.RemoteUri.AbsoluteUri;
            store[ChannalRadio.ColLocation[ChannalRadio.StartTime]] = info.StartTime;
            store[ChannalRadio.ColLocation[ChannalRadio.Title]] = info.Title;

            return store.ToList();
        }

        public static ChannalRadioInfo StoreToInfo(List<object> store)
        {
            ChannalRadioInfo info = new ChannalRadioInfo();
           info.RadioId= (int)(long)store[ChannalRadio.ColLocation[ChannalRadio.ChannalId]] ;
            if ((string)store[ChannalRadio.ColLocation[ChannalRadio.Broadcasters]] != null)
            {
                info.Broadcasters = (string)store[ChannalRadio.ColLocation[ChannalRadio.Broadcasters]];
            }
            try
            {
                info.Date = DateTime.ParseExact((string)store[ChannalRadio.ColLocation[ChannalRadio.Date]], "yyyyMMdd", null);
            }
            catch
            {

            }
            try
            {
                info.EndTime = ((string)store[ChannalRadio.ColLocation[ChannalRadio.EndTime]]);
            }
            catch
            {

            }
            if (store[ChannalRadio.ColLocation[ChannalRadio.LocalUri]] != null)
            {
                try
                {
                    info.LocalUri = new Uri((string)store[ChannalRadio.ColLocation[ChannalRadio.LocalUri]]);
                }
                catch
                {

                }
            }
           info.ProgramId=(int)(long)store[ChannalRadio.ColLocation[ChannalRadio.ProgramId]];
            //info.QuoteCounts=(int) store[ChannalRadio.ColLocation[ChannalRadio.QuoteCounts]];
            try
            {
                info.RemoteUri = new Uri((string)store[ChannalRadio.ColLocation[ChannalRadio.RemoteUri]]);
            }
            catch
            {

            }
            try
            {
                info.StartTime = ((string)store[ChannalRadio.ColLocation[ChannalRadio.StartTime]]);
            }
            catch
            {

            }
            if (store[ChannalRadio.ColLocation[ChannalRadio.Title]] != null)
            {
                info.Title = (string)store[ChannalRadio.ColLocation[ChannalRadio.Title]];
            }
            return info;
        }
    }
}
