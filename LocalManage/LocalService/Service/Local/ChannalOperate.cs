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
    class ChannalOperate:IChannalOperate
    {
        public ChannalCardOperate CardOperate = new ChannalCardOperate();
        public ChannalRadioOperate RadioOperate = new ChannalRadioOperate();
        ImgStorage ImageS = new ImgStorage();
        RadioStorage RadioS = new RadioStorage();

        public ChannalOperate()
        {
            SQLiteConnect.CreateLocalRadioManage();
        }
        public async Task<bool> Download(ChannalCardInfo album)
        {
            StorageFile image = await ImageS.DownImage(album.RadioId.ToString(), album.Cover, album.Cover);
            if (image == null)
            {
                return false;
            }
            album.LocalCover = new Uri(image.Path);
            return Fav(album);
        }
        public async Task<bool> Download(ChannalCardInfo album, ChannalRadioInfo radio)
        {
            if (!await Download(album))
            {
                return false;
            }
            StorageFile music = await RadioS.DownRadio(radio.RadioId.ToString(), radio.RemoteUri, radio.RemoteUri);
            if (music == null)
            {
                return false;
            }
            radio.LocalUri = new Uri(music.Path);
            return Fav(album, radio);
        }

        public bool Fav(ChannalCardInfo album)
        {
            CardOperate.SaveChannalCardInfo(album);
            ChannalCardInfo temp = CardOperate.SelectChannalCardInfo(album);
            if (temp.LocalCover == null)
            {
                temp.LocalCover = album.LocalCover;
            }

            return CardOperate.UpdateChannalCardInfo(temp);
        }

        public bool Fav(ChannalCardInfo album, ChannalRadioInfo radio)
        {
            if (!Fav(album))
            {
                return false;
            }
            RadioOperate.SaveChannalRadioInfo(radio);
            ChannalRadioInfo temp = RadioOperate.SelectChannalRadioInfo(radio);
            if (temp.LocalUri == null)
            {
                temp.LocalUri = radio.LocalUri;
            }
            return RadioOperate.UpdateChannalRadioInfo(temp);
        }

        public int Fav(ChannalCardInfo album, List<ChannalRadioInfo> radios)
        {
            CardOperate.SaveChannalCardInfo(album);
            int i = 0;
            foreach (ChannalRadioInfo radio in radios)
            {
                if (Fav(album, radio))
                {
                    i++;
                }
            }
            return i;
        }


        public async Task<bool> Remove(ChannalCardInfo album)
        {
            await MyFolder.DeleteFolder(album.RadioId.ToString());
            return CardOperate.DeleteChannalCardInfo(album);

        }

        public async Task<bool> Remove(ChannalRadioInfo radio)
        {
            await RadioS.RemoveRadio(radio.RadioId.ToString(), radio.RemoteUri);
            return RadioOperate.DeleteChannalRadioInfo(radio);
        }

        public bool Update(ChannalCardInfo album)
        {
            return CardOperate.UpdateChannalCardInfo(album);
        }

        public bool Update(ChannalRadioInfo radio)
        {
            return RadioOperate.UpdateChannalRadioInfo(radio);
        }

        public List<ChannalCardInfo> Load()
        {
            return CardOperate.SelectChannalCardInfo();
        }

        public List<ChannalRadioInfo> Load(ChannalCardInfo album)
        {
            return RadioOperate.SelectChannalRadioInfo(album);
        }

        public async Task<bool> Export(ChannalCardInfo album, ChannalRadioInfo radio)
        {
            StorageFolder folder = await MyFolder.CreateFolder(album.RadioId.ToString());
            StorageFile image = await ImageS.DownImage(album.RadioId.ToString(), album.LocalCover, album.Cover);
            StorageFile music = await RadioS.DownRadio(radio.RadioId.ToString(), radio.LocalUri, radio.RemoteUri);
            if (image == null || music == null)
            {
                return false;
            }
            return await MyFile.CreateRadioFile_Channal(folder, music, image, radio, album);
        }
    }
}
