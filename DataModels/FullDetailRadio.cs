using Newtonsoft.Json;
using PureRadio.Common;
using PureRadio.DataModel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.DataModel
{
    public class FullDetailRadio
    {
        public RadioFullAlbum album { get; set; }
        public List<RadioFullContent> ListYesterday { get; set; }
        public List<RadioFullContent> ListToday { get; set; }
        public List<RadioFullContent> ListTomorrow { get; set; }

        public static FullDetailRadio GetRadioDetail(int channel_id)
        {
            
            string resultJson = HttpRequest.SendGet("https://webapi.qingting.fm/api/pc/radio/" + channel_id);
            resultJson = resultJson.Replace("\"1\":", "\"ListSunday\":").Replace("\"2\":", "\"ListMonday\":").Replace("\"3\":", "\"ListTuesday\":").Replace("\"4\":", "\"ListWednesday\":").Replace("\"5\":", "\"ListThursday\":").Replace("\"6\":", "\"ListFriday\":").Replace("\"7\":", "\"ListSaturday\":");
            RadioDetailFull result = JsonConvert.DeserializeObject<RadioDetailFull>(resultJson);
            FullDetailRadio fullDetail = new FullDetailRadio();
            if (result == null) return fullDetail;
            int today = (int)DateTime.Now.DayOfWeek;
            fullDetail.album = result.album;
            switch(today)
            {
                case 0:
                    fullDetail.ListYesterday = result.pList.ListSaturday;
                    fullDetail.ListToday = result.pList.ListSunday;
                    fullDetail.ListTomorrow = result.pList.ListMonday;
                    break;
                case 1:
                    fullDetail.ListYesterday = result.pList.ListSunday;
                    fullDetail.ListToday = result.pList.ListMonday;
                    fullDetail.ListTomorrow = result.pList.ListTuesday;
                    break;
                case 2:
                    fullDetail.ListYesterday = result.pList.ListMonday;
                    fullDetail.ListToday = result.pList.ListTuesday;
                    fullDetail.ListTomorrow = result.pList.ListWednesday;
                    break;
                case 3:
                    fullDetail.ListYesterday = result.pList.ListTuesday;
                    fullDetail.ListToday = result.pList.ListWednesday;
                    fullDetail.ListTomorrow = result.pList.ListThursday;
                    break;
                case 4:
                    fullDetail.ListYesterday = result.pList.ListWednesday;
                    fullDetail.ListToday = result.pList.ListThursday;
                    fullDetail.ListTomorrow = result.pList.ListFriday;
                    break;
                case 5:
                    fullDetail.ListYesterday = result.pList.ListThursday;
                    fullDetail.ListToday = result.pList.ListFriday;
                    fullDetail.ListTomorrow = result.pList.ListSaturday;
                    break;
                case 6:
                    fullDetail.ListYesterday = result.pList.ListFriday;
                    fullDetail.ListToday = result.pList.ListSaturday;
                    fullDetail.ListTomorrow = result.pList.ListSunday;
                    break;
                default:

                    break;
            }
            return fullDetail;
        }
    }
}
