using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using LocalRadioManage.DBBuilder.TableObj;

namespace LocalRadioManage.DataModelTransform
{
    /// <summary>
    /// 通过DataModel中RadioFullContent转换
    /// </summary>
   public partial class RadioTransform
    {

        public static class Local
        {
            /// <summary>
            /// 对应表LocalRadio
            /// </summary>
            public static List<object> ToLocalRadioStorage(RadioFullContent radio)
            {
                try
                {
                    object[] local_store = new object[LocalRadio.ColLocation.Count];

                    ulong radio_date = DateTransform.DateToInt(DateTransform.GetDateTime(radio.day), radio.start_time, radio.end_time);

                    local_store[LocalRadio.ColLocation[LocalRadio.RadioId]] = radio.id;
                    local_store[LocalRadio.ColLocation[LocalRadio.RadioDate]] = radio_date;
                    local_store[LocalRadio.ColLocation[LocalRadio.ChannalAlbumId]] = radio.channel_id;
                    local_store[LocalRadio.ColLocation[LocalRadio.RadioName]] = radio.title;
                    local_store[LocalRadio.ColLocation[LocalRadio.RadioDuration]] = radio.duration;
                    local_store[LocalRadio.ColLocation[LocalRadio.RadioCreateTime]] = DateTime.Now.ToString();
                    local_store[LocalRadio.ColLocation[LocalRadio.RadioLocalPath]] = radio.radio_uri.ToString();

                    return local_store.ToList();
                }
                catch
                {
                    return null;
                }
            }
            public static List<List<object>> ToLocalRadioStorage(List<RadioFullContent> radios)
            {
                List<List<object>> local_stores = new List<List<object>>();
                try
                {
                    foreach (RadioFullContent radio in radios)
                    {
                        local_stores.Add(ToLocalRadioStorage(radio));
                    }
                    return local_stores;
                }
                catch
                {
                    local_stores.Clear();
                    return null;
                }
            }

            public static List<object> ToUserDownRadio(RadioFullContent radio)
            {
                  object[] local_store = new object[UserDownRadio.ColLocation.Count];

                try
                {
                    ulong radio_date = DateTransform.DateToInt(DateTransform.GetDateTime(radio.day), radio.start_time, radio.end_time);

                    local_store[UserDownRadio.ColLocation[UserDownRadio.UserName]] = radio.user;
                    local_store[UserDownRadio.ColLocation[UserDownRadio.RadioId]] = radio.id;
                    local_store[UserDownRadio.ColLocation[UserDownRadio.RadioDate]] = radio_date;
                    local_store[UserDownRadio.ColLocation[UserDownRadio.ChannalAlbumId]] = radio.channel_id;

                    return local_store.ToList();
                }
                catch
                {
                    return null;
                }
            }
            public static List<List<object>> ToUserDownRadio(List<RadioFullContent> radios)
            {
                List<List<object>> local_stores = new List<List<object>>();
                try
                {
                    foreach (RadioFullContent radio in radios)
                    {
                        local_stores.Add(ToUserDownRadio(radio));
                    }
                    return local_stores;
                }
                catch
                {
                    local_stores.Clear();
                    return null;
                }
            }

            public static RadioFullContent ToRadioFullContent(List<object> store)
            {

                RadioFullContent radio = new RadioFullContent();
                radio.id = (int)(long)store[LocalRadio.ColLocation[LocalRadio.RadioId]];
                radio.channel_id = (int)(long)store[LocalRadio.ColLocation[LocalRadio.ChannalAlbumId]];
                radio.title = (string)store[LocalRadio.ColLocation[LocalRadio.RadioName]];
                radio.duration = (int)(long)store[LocalRadio.ColLocation[LocalRadio.RadioDuration]];
                radio.radio_uri = new Uri((string)store[LocalRadio.ColLocation[LocalRadio.RadioLocalPath]]);

                //一整个日期的填充
                ulong radio_date = (ulong)(long)store[LocalRadio.ColLocation[LocalRadio.RadioDate]];
                DateTime date = new DateTime();
                string start_time = "";
                string end_time = "";
                DateTransform.IntToDate(radio_date, ref date, ref start_time, ref end_time);
                radio.day = date.Day;
                radio.start_time = start_time;
                radio.end_time = end_time;


                return radio;
            }
            public static List<RadioFullContent> ToRadioFullContent(List<List<object>> stores)
            {
                List<RadioFullContent> radios = new List<RadioFullContent>();

                try
                {
                    foreach (List<object> store in stores)
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

            public static RadioFullContent ToRadioFullContent(List<object> store,string user_name)
            {
                RadioFullContent radio = ToRadioFullContent(store);
                radio.user = user_name;
                return radio;
            }
            public static List<RadioFullContent> ToRadioFullContent(List<List<object>> stores,string user_name)
            {
                List<RadioFullContent> radios = new List<RadioFullContent>();

                try
                {
                    foreach (List<object> store in stores)
                    {
                        radios.Add(ToRadioFullContent(store,user_name));
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


    public partial class RadioTransform
    {
        public static class Remote
        {
            /// <summary>
            /// 对应表UserFavRadio
            /// </summary>
            public static List<object> ToRemoteRadioFav(RadioFullContent radio)
            {
                try
                {
                    object[] reomte_fav = new object[UserFavRadio.ColLocation.Count];

                    ulong radio_date = DateTransform.DateToInt(DateTransform.GetDateTime(radio.day), radio.start_time, radio.end_time);

                    reomte_fav[UserFavRadio.ColLocation[UserFavRadio.UserName]] = radio.user;
                    reomte_fav[UserFavRadio.ColLocation[UserFavRadio.RadioId]] = radio.id;
                    reomte_fav[UserFavRadio.ColLocation[UserFavRadio.RadioDate]] = radio_date;
                    reomte_fav[UserFavRadio.ColLocation[UserFavRadio.ChannalAlbumId]] = radio.channel_id;
                    reomte_fav[UserFavRadio.ColLocation[UserFavRadio.RadioName]] = radio.title;
                    reomte_fav[UserFavRadio.ColLocation[UserFavRadio.RadioDuration]] = radio.duration;
                    reomte_fav[UserFavRadio.ColLocation[UserFavRadio.RadioCreateTime]] = DateTime.Now.ToString();
                    reomte_fav[UserFavRadio.ColLocation[UserFavRadio.RadioRemotePath]] = radio.radio_uri.ToString();

                    return reomte_fav.ToList();
                }
                catch
                {
                    return null;
                }
            }
            public static List<List<object>> ToRemoteRadioFav(List<RadioFullContent> radios)
            {
                List<List<object>> remote_favs = new List<List<object>>();
                try
                {
                    foreach (RadioFullContent radio in radios)
                    {
                        remote_favs.Add(ToRemoteRadioFav(radio));
                    }
                    return remote_favs;
                }
                catch
                {
                    remote_favs.Clear();
                    return null;
                }
            }
            public static RadioFullContent ToRadioFullContent(List<object> store)
            {

                RadioFullContent radio = new RadioFullContent();
                radio.user = (string)store[UserFavRadio.ColLocation[UserFavRadio.UserName]];
                radio.id = (int)store[UserFavRadio.ColLocation[UserFavRadio.RadioId]];
                radio.channel_id = (int)store[UserFavRadio.ColLocation[UserFavRadio.ChannalAlbumId]];
                radio.title = (string)store[UserFavRadio.ColLocation[UserFavRadio.RadioName]];
                radio.duration = (int)store[UserFavRadio.ColLocation[UserFavRadio.RadioDuration]];
                radio.radio_uri = new Uri((string)store[UserFavRadio.ColLocation[UserFavRadio.RadioRemotePath]]);
               

               //一整个日期的填充
               ulong radio_date = (ulong)store[UserFavRadio.ColLocation[UserFavRadio.RadioDate]];
                DateTime date = new DateTime();
                string start_time = "";
                string end_time = "";
                DateTransform.IntToDate(radio_date, ref date, ref start_time, ref end_time);
                radio.day = date.Day;
                radio.start_time = start_time;
                radio.end_time = end_time;
               


                return radio;
            }
            public static List<RadioFullContent> ToRadioFullContent(List<List<object>> stores)
            {
                List<RadioFullContent> radios = new List<RadioFullContent>();

                try
                {
                    foreach (List<object> store in stores)
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
}
