using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using LocalRadioManage.DBBuilder.TableObj;
using System.Net;
using System.Web;


namespace LocalRadioManage.DataModelTransform
{
   public partial class ChannalAlbumTransform
    {
        public static class Local
        {
            /// <summary>
            /// 对应表LocalChannalAlbum
            /// </summary>
            public static List<object> ToLocalChannalAlbumStorage(RadioFullAlbum radio_album)
            {

                object[] local_store = new object[LocalChannalAlbum.ColLocation.Count];
                try
                {
                    local_store[LocalChannalAlbum.ColLocation[LocalChannalAlbum.ChannalAlbumId]] = radio_album.id;
                    local_store[LocalChannalAlbum.ColLocation[LocalChannalAlbum.ChannalAlbumType]] = 0;
                    local_store[LocalChannalAlbum.ColLocation[LocalChannalAlbum.ChannalAlbumName]] = radio_album.title;
                    local_store[LocalChannalAlbum.ColLocation[LocalChannalAlbum.ChannalAlbumDesc]] = radio_album.description;
                    local_store[LocalChannalAlbum.ColLocation[LocalChannalAlbum.ChannalAlbumCover]] = radio_album.cover.ToString();

                    return local_store.ToList();
                }
                catch
                {
                    return null;
                }
            }
            public static List<List<object>> ToLocalChannalAlbumStorage(List<RadioFullAlbum> radio_albums)
            {
                List<List<object>> local_stores = new List<List<object>>();
                try
                {
                    foreach (RadioFullAlbum album in radio_albums)
                    {
                        local_stores.Add(ToLocalChannalAlbumStorage(album));
                    }
                    return local_stores;
                }
                catch
                {
                    return null;
                }

            }
            public static RadioFullAlbum ToRadioFullAlbum(List<object> store)
            {
                RadioFullAlbum album = new RadioFullAlbum();
                try
                {
                    album.id = (int)store[LocalChannalAlbum.ColLocation[LocalChannalAlbum.ChannalAlbumId]];
                    album.title = (string)store[LocalChannalAlbum.ColLocation[LocalChannalAlbum.ChannalAlbumName]];
                    album.description = (string)store[LocalChannalAlbum.ColLocation[LocalChannalAlbum.ChannalAlbumDesc]];
                    album.cover =new Uri((string)store[LocalChannalAlbum.ColLocation[LocalChannalAlbum.ChannalAlbumCover]]);
                    return album;
                }
                catch
                {
                    return null;
                }
            }
            public static List<RadioFullAlbum> ToRadioFullAlbum(List<List<object>> stores)
            {
                List<RadioFullAlbum> albums = new List<RadioFullAlbum>();
                try
                {
                    foreach (List<object> store in stores)
                    {
                        albums.Add(ToRadioFullAlbum(store));
                    }
                    return albums;
                }
                catch
                {
                    return null;
                }

            }
        }

    }

    public partial class ChannalAlbumTransform
    {
        public static class Remote
        {
            /// <summary>
            /// 对应表UserFavChannalAlbum
            /// </summary>
            public static List<object> ToUserFavChannalAlbumStorage(RadioFullAlbum radio_album)
            {

                object[] local_store = new object[UserFavChannalAlbum.ColLocation.Count];
                try
                {
                    local_store[UserFavChannalAlbum.ColLocation[UserFavChannalAlbum.ChannalAlbumId]] = radio_album.id;
                    local_store[UserFavChannalAlbum.ColLocation[UserFavChannalAlbum.ChannalAlbumType]] = 0;
                    local_store[UserFavChannalAlbum.ColLocation[UserFavChannalAlbum.ChannalAlbumName]] = radio_album.title;
                    local_store[UserFavChannalAlbum.ColLocation[UserFavChannalAlbum.ChannalAlbumDesc]] = radio_album.description;
                    local_store[UserFavChannalAlbum.ColLocation[UserFavChannalAlbum.ChannalAlbumCover]] = radio_album.cover;

                    return local_store.ToList();
                }
                catch
                {
                    return null;
                }
            }
            public static List<List<object>> ToUserFavChannalAlbumStorage(List<RadioFullAlbum> radio_albums)
            {
                List<List<object>> local_stores = new List<List<object>>();
                try
                {
                    foreach (RadioFullAlbum album in radio_albums)
                    {
                        local_stores.Add(ToUserFavChannalAlbumStorage(album));
                    }
                    return local_stores;
                }
                catch
                {
                    return null;
                }

            }
            public static RadioFullAlbum ToRadioFullAlbum(List<object> store)
            {
                RadioFullAlbum album = new RadioFullAlbum();
                try
                {
                    album.user = (string)store[UserFavChannalAlbum.ColLocation[UserFavChannalAlbum.UserName]];
                    album.id = (int)store[UserFavChannalAlbum.ColLocation[UserFavChannalAlbum.ChannalAlbumId]];
                    album.title = (string)store[UserFavChannalAlbum.ColLocation[UserFavChannalAlbum.ChannalAlbumName]];
                    album.description = (string)store[UserFavChannalAlbum.ColLocation[UserFavChannalAlbum.ChannalAlbumDesc]];
                    album.cover =new Uri((string)store[UserFavChannalAlbum.ColLocation[UserFavChannalAlbum.ChannalAlbumCover]]);
                
                    return album;
                }
                catch
                {
                    return null;
                }
            }
            public static List<RadioFullAlbum> ToRadioFullAlbum(List<List<object>> stores)
            {
                List<RadioFullAlbum> albums = new List<RadioFullAlbum>();
                try
                {
                    foreach (List<object> store in stores)
                    {
                        albums.Add(ToRadioFullAlbum(store));
                    }
                    return albums;
                }
                catch
                {
                    return null;
                }

            }
        }

    }
    }