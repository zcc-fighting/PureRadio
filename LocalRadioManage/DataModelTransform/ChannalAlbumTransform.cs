using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace LocalRadioManage.DataModelTransform
{
    class ChannalAlbumTransform
    {
        /// <summary>
        /// 对应表LocalChannalAlbum
        /// 加入顺序
        /// "ChannalAlbumId", "ChannalAlbumType", "ChannalAlbumName","ChannalAlbumDesc", "ChannalAlbumCover"
        /// </summary>
        public static List<object> ToLocalChannalAlbumStorage(RadioFullAlbum radio_album)
        {
            List<object> local_store=new List<object>();
            try
            {
                local_store.Add(radio_album.id);
                local_store.Add(0);
                local_store.Add(radio_album.title);
                local_store.Add(radio_album.description);
                local_store.Add(radio_album.cover);
                return local_store;
            }
            catch
            {
                return null;
            }
        }

        public static List<List<object>> ToLocalChannalAlbumStorage(List<RadioFullAlbum> radio_album)
        {
            List<List<object>> local_stores = new List<List<object>>();
            try
            {
                foreach (RadioFullAlbum album in radio_album)
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

        public static RadioFullAlbum ToRadioFullAlbum(List<object> local_store)
        {
            RadioFullAlbum album = new RadioFullAlbum();
            try 
            { 
                album.id = (int)local_store[0];
                album.title = (string)local_store[2];
                album.description = (string)local_store[3];
                album.cover = (string)local_store[4];
                return album;
            }
            catch
            {
                return null;
            }
        }

        public static List<RadioFullAlbum> ToRadioFullAlbum(List<List<object>> local_store)
        {
            List<RadioFullAlbum> albums = new List<RadioFullAlbum>();
            try
            {
                foreach (List<object> store in local_store)
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
