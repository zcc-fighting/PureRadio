using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.DBBuilder;
using LocalRadioManage.DBBuilder.TableObj;
using DataModels;
using Windows.Storage;
using LocalRadioManage.StorageOperate;

namespace LocalRadioManage.LocalService
{
   public partial class LocalService
    {
        /// <summary>
        /// 访问此变量需加锁，用于异步结果读取
        /// </summary>
        public MissionComplete complete = null;
        ServiceUserDown UserDownService = null;
        ServiceUserFav UserFavService = null;
        public class MissionComplete
        {
            public bool is_complete = false;
            public bool is_success = false;
        }

        public LocalService()
        {
            Start();
            complete = new MissionComplete();
            UserDownService = new ServiceUserDown();
            UserFavService = new ServiceUserFav();
        }
        private bool Start()
        {
            //暂有数据库的创建/获取->默认用户设置->默认存放文件夹
            if (SQLiteConnect.CreateLocalRadioManage())
            {
                Default.DefalutUser.SetDefaultUser();
                Default.DefalutStorage.SetDefaultStorage();
                return true;
            }
            return false;
        }
        public async void Start_Async()
        {
            lock (complete)
            {
                complete.is_complete = false;
            }
            if (await Task.Run(SQLiteConnect.CreateLocalRadioManage))
            {
                await Task.Run(Default.DefalutUser.SetDefaultUser);
                await Task.Run(Default.DefalutStorage.SetDefaultStorage);
                lock (complete)
                {
                    complete.is_complete = true;
                    complete.is_success = true;
                }

            }
            else
            {
                lock (complete)
                {
                    complete.is_complete = true;
                    complete.is_success = false;
                }
            }
        }
    }

    /// <summary>
    /// 本地下载服务
    /// </summary>
    public partial class LocalService
    {
       
     public async Task<bool> Download(RadioFullAlbum album,List<RadioFullContent> radio)
        {
         bool flag = false;
         flag &= await  UserDownService.SaveDownProgram(album);
         flag &= await UserDownService.DownRadio(radio);
         return flag;
        }
     public async Task<bool> Download(RadioFullAlbum album, RadioFullContent radio)
        {
            bool flag = true;
            flag &= await UserDownService.SaveDownProgram(album);
            flag &= await UserDownService.DownRadio(radio);
            return flag;
        }

     public List<RadioFullAlbum> Load(string user_name)
        {
          return  UserDownService.LoadProgram(user_name);
        }
     public List<RadioFullContent> Load(RadioFullAlbum album)
        {
            return UserDownService.LoadRadio(album);
        }
     public StorageFile Load(RadioFullContent radio)
        {
            Task<Task<StorageFile>> task = new Task<Task<StorageFile>>(()=> Load(radio,false));
            task.Start();
            task.Result.Wait();
            StorageFile file =task.Result.Result;
            return file;
        }
     private async Task<StorageFile> Load(RadioFullContent radio,bool is_null)
        {
          return await MyFile.GetFile(Default.DefalutStorage.radio_folder, radio.radio_uri.ToString());
        }

     public ServiceUserDown.DownProgress GetDownProgress()
        {
            lock (UserDownService.Progress)
            {
                return UserDownService.Progress;
            }
        }
        
    }

    /// <summary>
    ///远端收藏服务
    /// </summary>
    public partial class LocalService
    {
       
    }
}








