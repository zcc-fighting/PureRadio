using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalRadioManage.DataModelTransform
{
    class DateTransform
    {
        //为方便索引，将日期存储为int形式
        //8 6 6
        public static ulong DateToInt(DateTime date,string start_time,string end_time)
        {
            ulong store_date = 0;
            ulong result = 0;
            start_time=start_time.Replace(":", "");
            end_time=end_time.Replace(":", "");
            

            if(ulong.TryParse(end_time,out result))
            {
                store_date += result;
            }
            if(ulong.TryParse(start_time,out result))
            {
                store_date += result * 1000_000;
            }
            if (ulong.TryParse(date.ToString("yyyyMMdd"), out result))
            {
                result -= 2000_0000;
                store_date += result * 1000_000 * 1000_000;
            }  
            
            return store_date;
        }
        public static void IntToDate(ulong store_date,ref DateTime date,ref string start_time,ref string end_time )
        {
           uint int_end_time=0;
           uint int_start_time = 0;
           uint int_date_time = 0;
            int temp_len = 0; 

           int_end_time =(uint)(store_date % 1000_000);
           store_date /= 1000_000;
           int_start_time = (uint)(store_date % 1000_000);
           store_date /= 1000_000;
           int_date_time = (uint)store_date+2000_0000;

           start_time += (int_start_time).ToString();
           end_time += (int_end_time).ToString();

            temp_len = 6 - start_time.Length;
            for (int i = 0; i <temp_len ; i++)
            {
                start_time=start_time.Insert(0,"0");
            }
            start_time = start_time.Insert(4, ":");
            start_time = start_time.Insert(2, ":");
           

            temp_len = 6 - end_time.Length;
            for (int i = 0; i <temp_len; i++)
            {
                end_time = end_time.Insert(0, "0");
            }
            end_time = end_time.Insert(4, ":");
            end_time = end_time.Insert(2, ":");
          

            date = DateTime.ParseExact(int_date_time.ToString(), "yyyyMMdd",null);
        }

        public static DateTime GetDateTime(int day)
        {
            int content_day = day;
            int today = (int)DateTime.Now.DayOfWeek + 1;
            if (content_day == 7 && today == 1)
            {
                content_day = 0;
            }
            DateTime date = DateTime.Now.AddDays(content_day - today);
            return date; 
        }

       
    }
}
