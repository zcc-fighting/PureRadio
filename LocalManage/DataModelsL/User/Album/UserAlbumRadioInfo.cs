using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalManage.DBBuilder.TableObj;

namespace PureRadio.LocalManage.DataModelsL
{
    class UserAlbumRadioInfo
    {
     public string UserNumber { get; set; }
     public int ProgramId { get; set; }
     public int AlbumId { get; set; }
     public int State { get; set; }

        public UserAlbumRadioInfo()
        {

        }
        public UserAlbumRadioInfo(List<object> store)
        {
            try { UserNumber = (string)store[UserAlbumRadio.ColLocation[UserAlbumRadio.UserNumber]]; } catch { }
          
            ProgramId = (int)(long)store[UserAlbumRadio.ColLocation[UserAlbumRadio.ProgramId]];
            State = (int)(long)store[UserAlbumRadio.ColLocation[UserAlbumRadio.State]];
            AlbumId=(int)(long)store[UserAlbumRadio.ColLocation[UserAlbumRadio.AlbumId]];
        }


        public List<object> GetStore(UserAlbumRadioInfo info)
        {
            object[] store = new object[UserAlbumRadio.ColLocation.Count];
            store[UserAlbumRadio.ColLocation[UserAlbumRadio.UserNumber]] = info.UserNumber;
            store[UserAlbumRadio.ColLocation[UserAlbumRadio.ProgramId]] = info.ProgramId;
            store[UserAlbumRadio.ColLocation[UserAlbumRadio.State]] = info.State;
            store[UserAlbumRadio.ColLocation[UserAlbumRadio.AlbumId]]=info.AlbumId;
            return store.ToList();
        }


    }
}
