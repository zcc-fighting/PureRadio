using System;
using System.Collections.Generic;
using DataModels;
using LocalRadioManage.LocalService;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PureRadio.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class DownloadPage : Page
    {
        public DownloadPage()
        {
            this.InitializeComponent();
        }

        private static int _clicks = 0;
        private void RepeatButton_Click_1(object sender, RoutedEventArgs e)
        {
            _clicks += 1;
            // progressBar1.Value = _clicks;
            LocalServ.LocalDown localDown = new LocalServ.LocalDown();

            RadioFullContent radio = new RadioFullContent();
            radio.channel_id = 468;
            radio.id = 2571788;
            radio.start_time = "00:00:00";
            radio.end_time = "01:00:00";
            radio.duration = 3600;
            radio.day = 1;
            radio.title = "小说连播";
            radio.radio_uri = new Uri("https://lcache.qtfm.cn/cache/20221030/468/468_20221030_000000_010000_24_0.aac");
            radio.user = "0";
            radio.procasters = "广东";
            int int_date_time = 20221030;
            radio.date = DateTime.ParseExact(int_date_time.ToString(), "yyyyMMdd", null);
            RadioFullContent radio1 = new RadioFullContent();
            radio1.channel_id = 468;
            radio1.id = 2571788;
            radio1.start_time = "00:00:00";
            radio1.end_time = "01:00:00";
            radio1.duration = 3600;
            radio1.day = 1;
            radio1.title = "小说连播";
            radio1.radio_uri = new Uri("https://lcache.qtfm.cn/cache/20221106/468/468_20221106_000000_010000_24_0.aac");
            radio1.user = "0";
            radio1.procasters = "广东";
            int_date_time = 20221106;
            radio.date = DateTime.ParseExact(int_date_time.ToString(), "yyyyMMdd", null);

            RadioFullAlbum album = new RadioFullAlbum();
            album.user = "0";
            album.id = 468;
            album.title = "广东南方生活直播";
            album.cover = new Uri("http://pic.qtfm.cn/2021/0519/20210519030711.jpeg");
            album.description = "阿巴阿巴阿巴";

            progressBar1.Maximum = 100;
            progressBar2.Maximum = 100;
            if (_clicks == 1)
            {
                List<DataModels.RadioFullContent>  radios= new List<DataModels.RadioFullContent>() { radio,radio1};
                localDown.RemoveProgram(album, true);
             
                foreach(RadioFullContent _radio in radios)
                {
                    localDown.Download(album, radio);
                }
              
                List<DownProgressInform> progressInforms = localDown.GetDownProgressInform();
                //progressBar1.Name = progressInforms[0].file_name;

                progressBar1.Value = 0;
                progressBar2.Value = 0;
                bool flag = false;
                while (!flag)
                {
                      flag = false;
                      progressInforms = localDown.GetDownProgressInform();
                    if (progressInforms.Count >= 1)
                    {
                        if (progressInforms[0].file_size != 0)
                        {
                            progressBar1.Value = (progressInforms[0].down_size * 100) / progressInforms[0].file_size;
                        }
                    }
                    if (progressInforms.Count >= 2)
                    {
                        if (progressInforms[1].file_size != 0)
                        {
                            progressBar2.Value = (progressInforms[1].down_size * 100) / progressInforms[1].file_size;
                        }
                        flag = progressInforms[0].down_end && progressInforms[1].down_end;
                    }
                  
                }
            }




        }
    }
}
