using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalManage.DBBuilder.TableObj;

namespace PureRadio.LocalManage.DataModelsL
{
    class UserChannalCardInfo
    {
      public  string UserNumber { get; set; }
       public int ChannalId { get; set; }
       public int State { get; set; }

        public UserChannalCardInfo()
        {

        }
        public UserChannalCardInfo(List<object> store)
        {
            try { UserNumber = (string)store[UserChannalCard.ColLocation[UserChannalCard.UserNumber]]; } catch { }
           
            ChannalId = (int)(long)store[UserChannalCard.ColLocation[UserChannalCard.ChannalId]];
            State = (int)(long)store[UserChannalCard.ColLocation[UserChannalCard.State]];
        }


        public List<object> GetStore(UserChannalCardInfo info)
        {
            object[] store = new object[UserChannalCard.ColLocation.Count];
            store[UserChannalCard.ColLocation[UserChannalCard.UserNumber]] = info.UserNumber;
            store[UserChannalCard.ColLocation[UserChannalCard.ChannalId]] = info.ChannalId;
            store[UserChannalCard.ColLocation[UserChannalCard.State]] = info.State;
            return store.ToList();
        }


    }
}
