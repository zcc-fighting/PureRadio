using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage;
using LocalRadioManage.DBBuilder;
using LocalRadioManage.DataModelTransform;
using LocalRadioManage.Service;
using DataModel;

namespace LocalRadioManage.test
{
  class LocalRadioTest
    {
        public static void TestServiceStart()
        {
            myService service = new myService();
            service.Start();
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
            album.cover = "温暖四季.jpg";

          radio_store=  RadioTransform.ToLocalRadioStorage(radio);
          album_store=  ChannalAlbumTransform.ToLocalChannalAlbumStorage(album);

          radio = RadioTransform.ToRadioFullContent(radio_store);
          album = ChannalAlbumTransform.ToRadioFullAlbum(album_store);

        }

      
    }
}
