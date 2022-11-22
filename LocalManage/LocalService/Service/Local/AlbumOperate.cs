using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalManage.LocalService.Local;
using PureRadio.LocalManage.DataModelsL;
using PureRadio.LocalManage.LocalService.Storage;
using LocalRadioManage.StorageOperate;
using Windows.Storage;
using PureRadio.LocalManage.Iterfaces;
using LocalRadioManage.DBBuilder;

namespace PureRadio.LocalManage.LocalService.Service
{
    public class AlbumOperate : IAlbumOpreate
    {
        public AlbumCardOperate CardOperate = new AlbumCardOperate();
        public AlbumRadioOperate RadioOperate = new AlbumRadioOperate();
        ImgStorage ImageS = new ImgStorage();
        RadioStorage RadioS = new RadioStorage();

        public AlbumOperate()
         {
            SQLiteConnect.CreateLocalRadioManage();
        }

        public async Task<bool> Download(AlbumCardInfo album)
        {
            StorageFile image = await ImageS.DownImage(album.ContentId.ToString(), album.Cover, album.Cover);
            if (image == null)
            {
                return false;
            }
            album.LocalCover =new Uri(image.Path);
            return Fav(album);
        }
        public async Task<bool> Download(AlbumCardInfo album,AlbumRadioInfo radio)
        {
           if(!await Download(album))
            {
                return false;
            }
            StorageFile music = await RadioS.DownRadio(radio.AlbumId.ToString(), radio.RemoteUri, radio.RemoteUri);
            if (music == null)
            {
                return false;
            }
            radio.LocalUri = new Uri(music.Path);
            return Fav(album,radio);
        }

        public bool Fav(AlbumCardInfo album)
        {
            CardOperate.SaveAlbumCardInfo(album);
            AlbumCardInfo temp= CardOperate.SelectAlbumCardInfo(album);
            if (temp.LocalCover == null)
            {
                temp.LocalCover = album.LocalCover;
            }
            return CardOperate.UpdateAlbumCardInfo(temp);
        }

        public bool Fav(AlbumCardInfo album, AlbumRadioInfo radio)
        {
            if (!Fav(album)){
                return false;
            }
             RadioOperate.SaveAlbumRadioInfo(radio);
            AlbumRadioInfo temp = RadioOperate.SelectAlbumRadioInfo(radio);
            if (temp.LocalUri == null)
            {
                temp.LocalUri = radio.LocalUri;
            }
            return  RadioOperate.UpdateAlbumRadioInfo(temp);
        }

        public int  Fav(AlbumCardInfo album, List<AlbumRadioInfo> radios)
        {
            CardOperate.SaveAlbumCardInfo(album);
            int i = 0;
            foreach (AlbumRadioInfo radio in radios)
            {
                if(Fav(album, radio))
                {
                    i++;
                }
            }
            return i;
        }


        public async Task<bool> Remove(AlbumCardInfo album)
        {
            await MyFolder.DeleteFolder(album.ContentId.ToString());
            return CardOperate.DeleteAlbumCardInfo(album);

        }

        public async Task<bool> Remove(AlbumRadioInfo radio)
        {
            await RadioS.RemoveRadio(radio.AlbumId.ToString(), radio.RemoteUri);
            return RadioOperate.DeleteAlbumRadioInfo(radio);
        }

        public bool Update(AlbumCardInfo album)
        {
            return CardOperate.UpdateAlbumCardInfo(album);
        }

        public bool Update(AlbumRadioInfo radio)
        {
            return RadioOperate.UpdateAlbumRadioInfo(radio);
        }

        public List<AlbumCardInfo> Load()
        {
          return  CardOperate.SelectAlbumCardInfo();
        }

        public List<AlbumRadioInfo> Load(AlbumCardInfo album)
        {
            return RadioOperate.SelectAlbumRadioInfo(album);
        }

        public async Task<bool> Export(AlbumCardInfo album, AlbumRadioInfo radio)
        {
          StorageFolder folder =await MyFolder.CreateFolder(album.ContentId.ToString());
          StorageFile image= await ImageS.DownImage(album.ContentId.ToString(), album.LocalCover, album.Cover);
          StorageFile music = await RadioS.DownRadio(radio.AlbumId.ToString(), radio.LocalUri, radio.RemoteUri);
            if (image == null || music == null)
            {
                return false;
            }
          return await MyFile.CreateRadioFile_Album(folder, music, image, radio, album);
        }
    }
}
