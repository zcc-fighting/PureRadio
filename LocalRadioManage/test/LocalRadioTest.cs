using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage;
using LocalRadioManage.DBBuilder;
using LocalRadioManage.LocalService;
using Windows.Storage;
using LocalRadioManage.DataModelTransform;
using DataModels;



namespace LocalRadioManage.test
{
  class LocalRadioTest
    {
        public async static void TestServiceStart()
        {
            LocalService.LocalServ.LocalDown down_service = new LocalService.LocalServ.LocalDown();
            LocalServ.LocalUser user = new LocalServ.LocalUser();
            RadioFullContent radio = new RadioFullContent();
            radio.channel_id = 468;
            radio.id = 2571788;
            radio.start_time = "00:00:00";
            radio.end_time = "01:00:00";
            radio.duration = 3600;
            radio.day = 1;
            radio.title = "小说连播";
            radio.local_radio_uri = new Uri("https://lcache.qtfm.cn/cache/20221030/468/468_20221030_000000_010000_24_0.aac");
            radio.remote_radio_uri= new Uri("https://lcache.qtfm.cn/cache/20221030/468/468_20221030_000000_010000_24_0.aac");
            radio.user = "0";
            int int_date_time = 20221030;
            radio.date =DateTime.ParseExact(int_date_time.ToString(), "yyyyMMdd", null);

            RadioFullAlbum album = new RadioFullAlbum();
            album.user = "0";
            album.id = 468;
            album.title = "广东南方生活直播";
            album.remote_cover=new Uri("http://pic.qtfm.cn/2021/0519/20210519030711.jpeg");
            album.remote_cover= new Uri("http://pic.qtfm.cn/2021/0519/20210519030711.jpeg");
            album.description = "阿巴阿巴阿巴";

      
           await down_service.Download_Asyc(album, radio);
            album.user = "523523523";
            radio.user = "523523523";
           await down_service.Download_Asyc(album, radio);
            bool ss = user.CheckUsr("523523523", "0");
            ss = user.UpdateUsr("523523523", "0", "123456");
            ss = user.CheckUsr("523523523", "123456");
            List<RadioFullAlbum>  albums= down_service.Load(album.user);
           List<RadioFullContent> radios = down_service.Load(album,true);
           StorageFile radio_file= down_service.Load(radio);
            bool a = down_service.RemoveProgram(album, true);
            bool b = down_service.RemoveRadio(album, true);
            album.user = "0";
            Task<Task<bool>> task = new Task<Task<bool>>(() => down_service.Export_Aysc(album, radio,null));
            
            task.Start();
            task.Result.Wait();
           LocalServ.LocalDown.ExportProgress progress=  down_service.GetExportProgress();
            

            albums = down_service.Load(album.user);
            radios = down_service.Load(album,true);
            a = down_service.RemoveProgram(album, true);
            b = down_service.RemoveRadio(album, true);
            
            bool c = down_service.RemoveProgram(album, true);
 
        }

        public static void TestDataModelTrans()
        {
            RadioFullContent radio = new RadioFullContent();
            RadioFullAlbum album = new RadioFullAlbum();
            List<object> radio_store=null;
            List<object> album_store = null;

            radio.channel_id = 114;
            radio.id = 514;
            radio.title = "听我说";
            radio.start_time = "00:00:00";
            radio.end_time = "01:00:00";
            radio.duration = 3600;
            radio.day = 7;

            album.id = 15926;
            album.title = "谢谢你";
            album.description = "因为有你";
            album.remote_cover =new Uri(Default.DefalutStorage.image_folder.Path+"\\温暖四季.jpg");

          radio_store=  RadioTransform.Local.ToLocalRadioStorage(radio);
          album_store=  ChannalAlbumTransform.Local.ToLocalChannalAlbumStorage(album);

          radio = RadioTransform.Local.ToRadioFullContent(radio_store);
          album = ChannalAlbumTransform.Local.ToRadioFullAlbum(album_store);

        }

      
    }
}
