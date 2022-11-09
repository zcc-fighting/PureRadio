using PureRadio.DataModel.Results;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace PureRadio.Common
{
    public class QTDecoder
    {
        //static string radioKey = "Lwrpu$K5oP";
        static string contentKey = "fpMn12&38f_2e";

        public static string PlayChannel_Live(int channel_id)
        {
            /*
            string path = "/live/" + channel_id.ToString() + "/64k.mp3";
            int time = (int)(DateTime.Now.AddHours(1).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            string ts = time.ToString("X").ToLower();
            string urlParams = "app_id=" + WebUtility.UrlEncode("web") + "&path=" + WebUtility.UrlEncode(path) + "&ts=" + WebUtility.UrlEncode(ts);
            string hmac = HmacMD5(urlParams, radioKey);
            return "https://lhttp.qtfm.cn" + path + "?app_id=web&ts=" + ts + "&sign=" + WebUtility.UrlEncode(hmac);
            */
            // http://ls.qingting.fm/live/1739/64k.m3u8
            return "http://ls.qingting.fm/live/" + channel_id + "/64k.m3u8";
        }

        public static string PlayChannel_Demand(RadioFullContent fullContent)
        {
            // https://lcache.qtfm.cn/cache/20220531/1739/1739_20220531_000000_060000_24_0.aac
            int channel_id = fullContent.channel_id;
            int content_day = fullContent.day;
            int today = (int)DateTime.Now.DayOfWeek + 1;
            if (content_day == 7 && today == 1) content_day = 0;
            string date = DateTime.Now.AddDays(content_day - today).ToString("yyyyMMdd");
            string file = date + "/" + channel_id.ToString() + "/" + channel_id.ToString() + "_" + date + "_" + fullContent.start_time.Replace(":", "") + "_" + fullContent.end_time.Replace(":", "") + "_24_0.aac";
            return "https://lcache.qtfm.cn/cache/" + file;
        }

        public static string PlayContent(int album_id, int content_id, string access_token, string qingting_id)
        {
            string path = "/audiostream/redirect/" + album_id.ToString() + "/" + content_id.ToString();
            int time = (int)(DateTime.Now.AddHours(1).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            string ts = time.ToString("X").ToLower();
            string urlToEncode = path + "?access_token=" + access_token + "&device_id=MOBILESITE&qingting_id=" + qingting_id + "&t=" + ts;
            string hmac = HmacMD5(urlToEncode, contentKey);
            return "https://audio.qtfm.cn" + path
                + "?access_token=" + access_token
                + "&device_id=MOBILESITE&qingting_id=" + qingting_id
                + "&t=" + ts
                + "&sign=" + hmac;
        }

        public static string HmacMD5(string source, string key)
        {
            HMACMD5 hMACMD5 = new HMACMD5(Encoding.Default.GetBytes(key));
            byte[] bytes = hMACMD5.ComputeHash(Encoding.Default.GetBytes(source));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                stringBuilder.Append(bytes[i].ToString("X2").ToLower());
            }
            hMACMD5.Clear();
            return stringBuilder.ToString();
        }
    }
}
