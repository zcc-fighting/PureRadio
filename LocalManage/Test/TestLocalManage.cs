using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using PureRadio.LocalRadioManage.LocalService.Local;
using PureRadio.LocalRadioManage.LocalService.Service;
using PureRadio.LocalRadioManage.DataModelsL;
using LocalRadioManage.DBBuilder;

namespace PureRadioR.LocalManage.Test
{
    class TestLocalManage
    {
        //测试下载音频/专辑
        public async static Task<bool> TestServiceStart()
        {

            SQLiteConnect.CreateLocalRadioManage();

            AlbumOperate operateAlbum = new AlbumOperate();
            ChannalOperate operateChannal = new ChannalOperate();
            UserAlbumOperate userAlbum = new UserAlbumOperate("default");
            UserChannalOperate userChannal= new UserChannalOperate("default");
            UserInformsOperate userInforms = new UserInformsOperate();

            AlbumCardInfo album_card = new AlbumCardInfo();
            AlbumRadioInfo album_radio = new AlbumRadioInfo();

            ChannalCardInfo channal_card = new ChannalCardInfo();
            ChannalRadioInfo channal_radio = new ChannalRadioInfo();

            album_card.Cover = new Uri("http://pic.qtfm.cn/2021/0519/20210519030711.jpeg");
            album_card.ContentId = 468;

            channal_card.Cover= new Uri("http://pic.qtfm.cn/2021/0519/20210519030711.jpeg");
            channal_card.RadioId = 468;

            channal_radio.RadioId = 468;
            channal_radio.StartTime = "00:00:00"; ;
            channal_radio.EndTime = "01:00:00"; ;
            int int_date_time = 20221030;
            channal_radio.Date= DateTime.ParseExact(int_date_time.ToString(), "yyyyMMdd", null);
            channal_radio.RemoteUri= new Uri("https://lcache.qtfm.cn/cache/20221030/468/468_20221030_000000_010000_24_0.aac");
            channal_radio.ProgramId= 2571788;
            

            album_radio.ProgramId = 2571788;
            album_radio.RemoteUri= new Uri("https://lcache.qtfm.cn/cache/20221030/468/468_20221030_000000_010000_24_0.aac");
            album_radio.AlbumId = 468;

            List<AlbumCardInfo> albums = operateAlbum.Load();
            List<AlbumRadioInfo> radios = operateAlbum.Load(album_card);
            List<ChannalCardInfo> Channals = operateChannal.Load();
            List<ChannalRadioInfo> Channalradios = operateChannal.Load(channal_card);

            //await operateAlbum.Download(album_card,album_radio);
            //await operateChannal.Download(channal_card, channal_radio);

            // //await operateAlbum.Export(album_card, album_radio);
            // await operateAlbum.Remove(album_card);
            // await operateChannal.Remove(channal_card);
            // operateAlbum.Fav(album_card, album_radio);
            // operateChannal.Fav(channal_card, channal_radio);
            // albums = operateAlbum.Load();
            // radios = operateAlbum.Load(album_card);
            //Channals = operateChannal.Load();
            //Channalradios = operateChannal.Load(channal_card);
            // operateChannal.Fav(album);

          List<UserInfo> list= userInforms.SelectUserInfo();


            userAlbum.Fav(album_card, album_radio);
            userChannal.Fav(channal_card, channal_radio);
          
            albums = userAlbum.Load(1);
            Channals = userChannal.Load(1);
            radios = userAlbum.Load(album_card, 1);
            Channalradios = userChannal.Load(channal_card, 1);

            await userAlbum.Download(album_card, album_radio);
            await userChannal.Download(channal_card, channal_radio);

          userAlbum.Fav(album_card,album_radio);
          userChannal.Fav(channal_card,channal_radio);
          albums = userAlbum.Load(0);
          Channals = userChannal.Load(0);
          radios = userAlbum.Load(album_card, 0);
          Channalradios = userChannal.Load(channal_card, 0);

            userAlbum.Remove(2);
            userChannal.Remove(2);
            albums = userAlbum.Load(0);
            Channals = userChannal.Load(0);
            radios = userAlbum.Load(album_card, 0);
            Channalradios = userChannal.Load(channal_card, 0);
            albums = userAlbum.Load(1);
            Channals = userChannal.Load(1);
            radios = userAlbum.Load(album_card, 1);
            Channalradios = userChannal.Load(channal_card, 1);

            userAlbum.Remove(0);
            userChannal.Remove(0);
            albums = userAlbum.Load(1);
            Channals = userChannal.Load(1);
            radios = userAlbum.Load(album_card, 1);
            Channalradios = userChannal.Load(channal_card, 1);


            return false;
        }
    }
}
