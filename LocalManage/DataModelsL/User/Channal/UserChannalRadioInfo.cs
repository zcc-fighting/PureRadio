using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalRadioManage.DBBuilder.TableObj;

namespace PureRadio.LocalRadioManage.DataModelsL
{
    class UserChannalRadioInfo
    {
       public string UserNumber { get; set; }
       public int ProgramId { get; set; }
       public int ChannalId { get; set; }
       public int State { get; set; }
       public string Date { get; set; }
       public string StartTime { get; set; }
       public string EndTime { get; set; }
        public UserChannalRadioInfo()
        {

        }

        public UserChannalRadioInfo(List<object> store)
        {
            try { UserNumber = (string)store[UserChannalRadio.ColLocation[UserChannalRadio.UserNumber]]; } catch { }
            try { Date = (string)store[UserChannalRadio.ColLocation[UserChannalRadio.Date]]; } catch { }
            try { EndTime = ((string)store[UserChannalRadio.ColLocation[UserChannalRadio.EndTime]]); } catch { }
         
            ProgramId = (int)(long)store[UserChannalRadio.ColLocation[UserChannalRadio.ProgramId]];
            State = (int)(long)store[UserChannalRadio.ColLocation[UserChannalRadio.State]];

            try { StartTime = ((string)store[UserChannalRadio.ColLocation[UserChannalRadio.StartTime]]); } catch { }
          
            ChannalId = (int)(long)store[UserChannalRadio.ColLocation[UserChannalRadio.ChannalId]];
        }


        public List<object> GetStore(UserChannalRadioInfo info)
        {
            object[] store = new object[UserChannalRadio.ColLocation.Count];
            store[UserChannalRadio.ColLocation[UserChannalRadio.UserNumber]] = info.UserNumber;
            store[UserChannalRadio.ColLocation[UserChannalRadio.ProgramId]] = info.ProgramId;
            store[UserChannalRadio.ColLocation[UserChannalRadio.State]] = info.State;
            try { store[UserChannalRadio.ColLocation[UserChannalRadio.Date]] = info.Date; } catch { }
            try { store[UserChannalRadio.ColLocation[UserChannalRadio.StartTime]] = info.StartTime; } catch { }
            try { store[UserChannalRadio.ColLocation[UserChannalRadio.EndTime]] = info.EndTime; } catch { }
            store[UserChannalRadio.ColLocation[UserChannalRadio.ChannalId]] = ChannalId;
            return store.ToList();
        }


    }
}
