using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalRadioManage.DBBuilder.TableObj;

namespace PureRadio.LocalRadioManage.DataModelsL
{
    class UserAlbumCardInfo
    {
       public string UserNumber { get; set; }
       public int AlbumId { get; set; }
       public int State { get; set; }

        public UserAlbumCardInfo()
        {

        }
        public UserAlbumCardInfo(List<object> store)
        {
            try { UserNumber = (string)store[UserAlbumCard.ColLocation[UserAlbumCard.UserNumber]]; } catch { }
      
            AlbumId = (int)(long)store[UserAlbumCard.ColLocation[UserAlbumCard.AlbumId]];
            State = (int)(long)store[UserAlbumCard.ColLocation[UserAlbumCard.State]];
        }


        public List<object> GetStore(UserAlbumCardInfo info)
        {
            object[] store = new object[UserAlbumCard.ColLocation.Count];
            store[UserAlbumCard.ColLocation[UserAlbumCard.UserNumber]] = info.UserNumber;
            store[UserAlbumCard.ColLocation[UserAlbumCard.AlbumId]] = info.AlbumId;
            store[UserAlbumCard.ColLocation[UserAlbumCard.State]] = info.State;
            return store.ToList();
        }


    }
}
