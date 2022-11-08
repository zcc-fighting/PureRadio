using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage;
using LocalRadioManage.DBBuilder;
using LocalRadioManage.DataModelTransform;
using LocalRadioManage.LocalService;
using DataModels;

namespace LocalRadioManage.test
{
  class LocalRadioTest
    {
        public async static void TestServiceStart()
        {
            LocalService.LocalService service = new LocalService.LocalService();
            RadioFullContent radio = new RadioFullContent();
            radio.channel_id = 468;
            radio.id = 2571788;
            radio.start_time = "00:00:00";
            radio.end_time = "01:00:00";
            radio.duration = 3600;
            radio.day = 1;
            radio.title = "小说连播";
            radio.radio_uri = new Uri("https://lcache.qtfm.cn/cache/20221106/468/468_20221106_000000_010000_24_0.aac");
            radio.user = "0";

            RadioFullAlbum album = new RadioFullAlbum();
            album.user = "0";
            album.id = 468;
            album.title = "广东南方生活直播";
            album.cover=new Uri("http://pic.qtfm.cn/2021/0519/20210519030711.jpeg");
            album.description = "阿巴阿巴阿巴";

      
            await service.Download(album, radio);
            service.Load(radio);
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
            album.cover =new Uri(Default.DefalutStorage.image_folder.Path+"\\温暖四季.jpg");

          radio_store=  RadioTransform.Local.ToLocalRadioStorage(radio);
          album_store=  ChannalAlbumTransform.Local.ToLocalChannalAlbumStorage(album);

          radio = RadioTransform.Local.ToRadioFullContent(radio_store);
          album = ChannalAlbumTransform.Local.ToRadioFullAlbum(album_store);

        }

      
    }
}
