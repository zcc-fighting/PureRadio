using System;
using System.Collections.Generic;
using DataModels;
using LocalRadioManage.LocalService;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System.Linq;
using PureRadio.ViewModels;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PureRadio.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>




        public sealed partial class DownloadPage : Page
    {
        DownloadViewModel DownloadView = new DownloadViewModel();

        //DownProgressInform 

        LocalServ.LocalDown Download = new LocalServ.LocalDown();

        

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
            radio.remote_radio_uri = new Uri("https://lcache.qtfm.cn/cache/20221030/468/468_20221030_000000_010000_24_0.aac");
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
            radio1.remote_radio_uri = new Uri("https://lcache.qtfm.cn/cache/20221106/468/468_20221106_000000_010000_24_0.aac");
            radio1.user = "0";
            radio1.procasters = "广东";
            int_date_time = 20221106;
            radio1.date = DateTime.ParseExact(int_date_time.ToString(), "yyyyMMdd", null);

            RadioFullAlbum album = new RadioFullAlbum();
            album.user = "0";
            album.id = 468;
            album.title = "广东南方生活直播";
            album.remote_cover = new Uri("http://pic.qtfm.cn/2021/0519/20210519030711.jpeg");
            album.description = "阿巴阿巴阿巴";

            progressBar1.Maximum = 100;
            progressBar2.Maximum = 100;
            if (_clicks == 1)
            {
                List<DataModels.RadioFullContent>  radios= new List<DataModels.RadioFullContent>() { radio,radio1};
                localDown.RemoveProgram(album, true);
             
                foreach(RadioFullContent _radio in radios)
                {
                    localDown.Download(album, _radio);
                }
              
                progressBar1.Value = 0;
                progressBar2.Value = 0;
                List<ProgressBar> bars = new List<ProgressBar>() { progressBar1, progressBar2 };
               
                Dictionary<string, ProgressBar> pair = new Dictionary<string, ProgressBar>();
                string file_name_1= System.Web.HttpUtility.UrlDecode(radio.remote_radio_uri.Segments.Last());
                string file_name_2= System.Web.HttpUtility.UrlDecode(radio1.remote_radio_uri.Segments.Last());
                _clicks = 0;

                List<string> names = new List<string> { file_name_1, file_name_2 };
          


                begintask(ref progressBar1, localDown, file_name_1);
                begintask(ref progressBar2, localDown, file_name_2);
                
                
                //AsycLoadProgressBar asyc1 = new AsycLoadProgressBar(TestAsycLoadProgressBar);
                //asyc1.Invoke(bars, localDown, names);
                // AsycLoadProgressBar asyc2 = new AsycLoadProgressBar(TestAsycLoadProgressBar);
                //asyc2.Invoke(progressBar2, localDown, file_name_2);
                
            }
           

        }


        public delegate void AsycLoadProgressBar(ref ProgressBar bar, LocalServ.LocalDown down, string name);
       
        // public delegate void AsycLoadProgressBar(List<ProgressBar> bar, LocalServ.LocalDown down, List<string> names);

        public void begintask(ref ProgressBar bar, LocalServ.LocalDown down, string name)
        {
            
            AsycLoadProgressBar asyc1 = new AsycLoadProgressBar(TestAsycLoadProgressBar);
            asyc1.Invoke(ref bar, down, name);
        }
        private static void TestAsycLoadProgressBar(ref ProgressBar bar,LocalServ.LocalDown down,string name)
        {
            DownProgressInform inform =null;
            while (inform == null)
            {
                inform = down.GetDownProgressInform(name);
            }
            while ((inform!=null)&&!inform.down_end)
            {
                if (inform.file_size != 0)
                {
                    bar.Value = inform.down_size * 100 / inform.file_size;
                }
                inform = down.GetDownProgressInform(name);
            }
        }
        private void TestAsycLoadProgressBar(List<ProgressBar> bar, LocalServ.LocalDown down, List<string> names)
        {
            for (int i=0;i<names.Count;i++)
            {
                DownProgressInform inform = null;
                while (inform == null)
                {
                    inform = down.GetDownProgressInform(names[i]);
                }
                while ((inform != null) && !inform.down_end)
                {
                    if (inform.file_size != 0)
                    {
                        bar[i].Value = inform.down_size * 100 / inform.file_size;
                    }
                    inform = down.GetDownProgressInform(names[i]);
                }

            }
        }
    }
}
