using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.DBBuilder;
using LocalRadioManage.DBBuilder.TableObj;
using LocalRadioManage.StorageOperate;
using Windows.Storage;



namespace LocalRadioManage.LocalService
{
    /// <summary>
    /// 对于在用户未登录情况下的数据操作
    /// 由此处创建的默认用户"0"所代理
    /// </summary>
  public static class Default
    {
        public static class DefalutUser
        {
           
            //"UserName", "UserPass"
            public static readonly  object[] default_user =  {  "0", "0" };
            // "UserName", "ChannalAlbumId" 
            public static readonly object[] default_local_down = { "0", 0 };
            //"UserName", "ChannalAlbumId", "ChannalAlbumType","ChannalAlbumDesc", "ChannalAlbumCover"
            public static readonly object[] default_local_fav = { "0", 0, 0, "本地默认用户", "defalut.jpg" };

            public static void SetDefaultUser()
            {
                SQLiteConnect.TableHandle.AddRecord(Users.TableName, default_user.ToList());
                SQLiteConnect.TableHandle.AddRecord(UserDownChannalAlbum.TableName, default_local_down.ToList());
                SQLiteConnect.TableHandle.AddRecord(UserFavChannalAlbum.TableName, default_local_fav.ToList());
            }
        }

        public static class DefalutStorage
        {
            const string default_radio_folder = "PureRadioLocalRadio";
            const string default_image_folder = "PureRadioLocalImage";
            public static StorageFolder radio_folder=null;
            public static StorageFolder image_folder = null;

            public static void SetDefaultStorage()
            {
                Task<Task> task_set_default = new Task<Task>(_SetDefaultStorage);
                task_set_default.Start();
                task_set_default.Result.Wait();
            }

            private async static Task<bool> _SetDefaultStorage()
            {
                try
                {
                    radio_folder = await StorageOperate.MyFolder.CreateFolder(KnownFolders.MusicLibrary, default_radio_folder);
                    image_folder = await StorageOperate.MyFolder.CreateFolder(KnownFolders.PicturesLibrary, default_image_folder);
                    return true;
                }
                catch
                {
                    radio_folder = await StorageOperate.MyFolder.CreateFolder(default_radio_folder);
                    image_folder = await StorageOperate.MyFolder.CreateFolder(default_image_folder);
                    return false;
                }
            }
        }

    }
}
