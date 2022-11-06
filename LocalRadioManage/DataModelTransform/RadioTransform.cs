using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace LocalRadioManage.DataModelTransform
{
    /// <summary>
    /// 通过DataModel中RadioFullContent转换
    /// </summary>
   public class RadioTransform
    {
        /// <summary>
        /// 对应表LocalRadio
        /// 加入顺序
        /// "RadioId","RadioDate","ChannalAlbumId","RadioName","RadioDuration","RadioCreateTime","RadioLocalPath"
        ///这里未写入localpath，该值通过实际音频文件创建
        /// </summary>
        public static List<object> ToLocalRadioStorage(RadioFullContent full_content)
        {
            try
            {
                List<object> table_insert = new List<object>();
                table_insert.Add(full_content.id);
                ulong date_store = DateTransform.DateToInt(DateTransform.GetDateTime(full_content.day),full_content.start_time,full_content.end_time);
                table_insert.Add(date_store);
                table_insert.Add(full_content.channel_id);
                table_insert.Add(full_content.title);
                table_insert.Add(full_content.duration);
                table_insert.Add(DateTime.Now.ToString());
 
                return table_insert;
            }
            catch
            {
                return null;
            }
        }
        public static List<List<object>> ToLocalRadioStorage(List<RadioFullContent> full_content)
        {
            List<List<object>> table_inserts = new List<List<object>>();
            try
            {
                foreach (RadioFullContent radio in full_content)
                {
                    table_inserts.Add(ToLocalRadioStorage(radio));
                }
                return table_inserts;
            }
            catch
            {
                table_inserts.Clear();
                return null;
            }
        }
        public static RadioFullContent ToRadioFullContent(List<object> local_store)
        {

            RadioFullContent radio = new RadioFullContent();
            radio.id = (int)local_store[0];
            DateTime date=new DateTime();
            string start_time="";
            string end_time = "";
            DateTransform.IntToDate((ulong)local_store[1],ref date,ref start_time,ref end_time);
            radio.day = date.Day;
            radio.start_time = start_time;
            radio.end_time = end_time;
            radio.channel_id = (int)local_store[2];
            radio.title = (string)local_store[3];
            radio.duration = (int)local_store[4];
 
            return radio;
        }
        public static List<RadioFullContent> ToRadioFullContent(List<List<object>> local_store )
        {
            List<RadioFullContent> radios = new List<RadioFullContent>();

            try
            {
                foreach(List<object> store in local_store)
                {
                    radios.Add(ToRadioFullContent(store));
                }
                return radios;
            }
            catch
            {
                return null;
            }
        }
    }
}
