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
    //启动项
    public partial class LocalServ
    {

        /// <summary>
        /// 访问此变量需加锁，用于异步结果读取
        /// </summary>
        private MissionComplete complete = null;
        ServiceUserDown UserDownService = null;
        ServiceUserFav UserFavService = null;
        ServiceUser UserService = null;
        UserInforms.AllLocalDown LocalDownload = null;

        /// <summary>
        /// 启动项设为单例
        /// </summary>
        private static readonly Lazy<LocalServ> lazy = new Lazy<LocalServ>(() => new LocalServ());
        public static LocalServ Instance
        {
            get
            {
                return lazy.Value;
            }
        }
        private LocalServ()
        {
            Start();
            complete = new MissionComplete();
            UserDownService = new ServiceUserDown();
            UserFavService = new ServiceUserFav();
            UserService = new ServiceUser();
            LocalDownload = new UserInforms.AllLocalDown();
        }
        public class MissionComplete
        {
            public bool is_complete = false;
            public bool is_success = false;
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
        public MissionComplete GetMissionComplete()
        {
            lock (complete)
            {
                return complete;
            }
        }
    }

    public partial class LocalServ
    {
        /// <summary>
        /// 本地用户管理
        /// </summary>
        public class LocalUser : LocalServ
        {
            public bool SaveUser(string user_name)
            {
                return UserService.SaveUser(user_name);
            }

            public bool SaveUser(string user_name, string user_pass)
            {
                return UserService.SaveUser(user_name, user_pass);
            }

            public bool SaveUser(LocalUserInform user)
            {
                Task<Task<bool>> task = new Task<Task<bool>>(() => SaveUser_Asyc(user));
                task.Start();
                task.Result.Wait();
                return task.Result.Result;
            }

            public LocalUserInform LoadUser(string user_name, string user_pass, bool is_check)
            {
                if (is_check)
                {
                    if (!UserService.CheckUsr(user_name, user_pass))
                    {
                        return null;
                    }
                }
                List<LocalUserInform> users = UserService.LoadUser(user_name);
                if (users.Count == 1)
                {
                    return users[0];
                }
                else
                {
                    return null;
                }

            }

            public async Task<bool> SaveUser_Asyc(LocalUserInform user)
            {
                return await UserService.SaveUser(user);
            }

            public async Task<bool> RemoveUser_Asyc(string user_name)
            {
                return await UserService.DeleteUser(user_name);
            }

            public bool RemoveUsr(string user_name)
            {
                Task<Task<bool>> task = new Task<Task<bool>>(() => RemoveUser_Asyc(user_name));
                task.Start();
                task.Result.Wait();
                return task.Result.Result;
            }

            public bool CheckUsr(string user_name, string user_pass)
            {

                return UserService.CheckUsr(user_name, user_pass);
            }

            public bool UpdateUsr(string user_name, string old_pass, string new_pass)
            {
                return UserService.UpdateUsr(user_name, old_pass, new_pass);
            }

        }
    }

    public partial class LocalServ
    {
        /// <summary>
        /// 本地下载服务
        /// </summary>
        public class LocalDown : LocalServ
        {
            ExportProgress progress = new ExportProgress();
            //异步下载
            public async Task<bool> Download_Asyc(RadioFullAlbum album, List<RadioFullContent> radio)
            {
                bool flag = false;
                StorageFile img_file = await UserDownService.SaveDownProgram(album);
                flag &= (img_file != null);
                flag &= await UserDownService.DownRadio(radio);
                return flag;
            }
            public async Task<bool> Download_Asyc(RadioFullAlbum album, RadioFullContent radio)
            {
                bool flag = true;
                StorageFile img_file = await UserDownService.SaveDownProgram(album);

                flag &= (img_file != null);
                flag &= await UserDownService.DownRadio(radio);
                return flag;
            }
            //同步下载
            public bool Download(RadioFullAlbum album, List<RadioFullContent> radio)
            {
                Task<Task<bool>> task = new Task<Task<bool>>(() => Download_Asyc(album, radio));
                task.Start();
                task.Result.Wait();
                return task.Result.Result;
            }
            public bool Download(RadioFullAlbum album, RadioFullContent radio)
            {
                Task<Task<bool>> task = new Task<Task<bool>>(() => Download_Asyc(album, radio));
                task.Start();
                task.Result.Wait();
                return task.Result.Result;
            }

            public List<RadioFullAlbum> Load()
            {
                LocalDownload.SetLocalDown();
                return LocalDownload.LoadLocalDownProgram();
            }

            public List<RadioFullAlbum> Load(string user_name)
            {
                return UserDownService.LoadProgram(user_name);
            }
            public List<RadioFullContent> Load(RadioFullAlbum album, bool is_user)
            {
                if (is_user)
                {
                    return UserDownService.LoadRadio(album);
                }
                else
                {
                    LocalDownload.SetLocalDown(album);
                    return LocalDownload.LoadLocalDownRadio();
                }
            }

            public StorageFile Load(RadioFullContent radio)
            {
                Task<Task<StorageFile>> task = new Task<Task<StorageFile>>(() => Load_Asyc(radio));
                task.Start();
                task.Result.Wait();
                StorageFile file = task.Result.Result;
                return file;
            }
            //异步加载
            public async Task<StorageFile> Load_Asyc(RadioFullContent radio)
            {

                radio = LoadRadioContent(radio);
                StorageFolder root_folder = await MyFolder.GetFolder(Default.DefalutStorage.radio_folder, radio.channel_id.ToString());
                //实际音频已不存在，用户依赖删除
                if (!await MyFile.FileExists(root_folder, radio.radio_uri))
                {
                    LocalDownload.SetLocalDown(radio);
                    LocalDownload.DeleteLocalDownRadios(true);
                    await RemoveRadio_Asyc(radio, true);
                }

                StorageFile get_file = await MyFile.GetFile(root_folder, radio.radio_uri);

                return get_file;
            }
            private RadioFullContent LoadRadioContent(RadioFullContent radio)
            {
                LocalDownload.SetLocalDown(radio);

                List<RadioFullContent> radio_list = LocalDownload.LoadLocalDownRadio();
                if (radio_list == null)
                {
                    return radio;
                }
                else
                {
                    return radio_list[0];
                }


            }
            private async Task<StorageFile> LoadAlbumImge(RadioFullAlbum album)
            {
                LocalDownload.SetLocalDown(album);
                List<RadioFullAlbum> albums = LocalDownload.LoadLocalDownProgram();
                if (albums != null)
                {
                    album = albums[0];
                }
                return await MyFile.GetFile(album.cover);
            }

            //异步删除
            public async Task<bool> RemoveProgram_Asyc(string user_name, bool is_constrant)
            {
                return await UserDownService.DeleteProgram(user_name, is_constrant);
            }
            public async Task<bool> RemoveProgram_Asyc(RadioFullAlbum album, bool is_constrant)
            {
                return await UserDownService.DeleteProgram(album, is_constrant);
            }

            //同步删除
            public bool RemoveProgram(string user_name, bool is_constrant)
            {
                Task<Task<bool>> task = new Task<Task<bool>>(() => RemoveProgram_Asyc(user_name, is_constrant));
                task.Start();
                task.Result.Wait();
                return task.Result.Result;
            }
            public bool RemoveProgram(RadioFullAlbum album, bool is_constrant)
            {
                Task<Task<bool>> task = new Task<Task<bool>>(() => RemoveProgram_Asyc(album, is_constrant));
                task.Start();
                task.Result.Wait();
                return task.Result.Result;
            }

            //异步删除
            public async Task<bool> RemoveRadio_Asyc(RadioFullAlbum album, bool is_constrant)
            {
                return await UserDownService.DeleteRadio(album, is_constrant);

            }
            public async Task<bool> RemoveRadio_Asyc(RadioFullContent radio, bool is_constrant)
            {
                return await UserDownService.DeleteRadio(radio, is_constrant);
            }

            //同步删除
            public bool RemoveRadio(RadioFullAlbum album, bool is_constrant)
            {
                Task<Task<bool>> task = new Task<Task<bool>>(() => RemoveRadio_Asyc(album, is_constrant));
                task.Start();
                task.Result.Wait();
                return task.Result.Result;
            }
            public bool RemoveRadio(RadioFullContent radio, bool is_constrant)
            {
                Task<Task<bool>> task = new Task<Task<bool>>(() => RemoveRadio_Asyc(radio, is_constrant));
                task.Start();
                task.Result.Wait();
                return task.Result.Result;
            }

            //导出到本地音频库
            public async Task<bool> Export_Aysc(string user_name, StorageFolder root_folder)
            {
                try
                {
                    if (root_folder == null)
                    {
                        root_folder = Windows.Storage.KnownFolders.MusicLibrary;
                    }
                    List<RadioFullAlbum> albums = Load(user_name);
                    lock (progress)
                    {
                        progress.album_total_counts = albums.Count;
                    }
                    foreach (RadioFullAlbum album in albums)
                    {
                        await Task.Run(() => Export_Aysc(album, true, root_folder));
                    }
                    return true;
                }
                catch
                {
                    return false;
                }

            }
            public async Task<bool> Export_Aysc(RadioFullAlbum album, RadioFullContent radio, StorageFolder root_folder)
            {
                lock (progress)
                {
                    if (progress.current_radio_total_counts == 0)
                    {
                        progress.current_radio_total_counts = 1;
                    }
                }
                StorageFile radio_file = await Load_Asyc(radio);
                StorageFile img_file = await LoadAlbumImge(album);
                lock (progress)
                {
                    progress.current_radio_finish_counts += 1;
                }
                if (root_folder == null)
                {
                    root_folder = Windows.Storage.KnownFolders.MusicLibrary;
                }
                if (await MyFile.CreateRadioFile(root_folder, radio_file, img_file, radio, album))
                {
                    lock (progress)
                    {
                        progress.album_finish_counts += 1;
                        progress.radio_total_success_counts += 1;
                    }
                    return true;
                }
                return false;

            }

            public async Task<bool> Export_Aysc(RadioFullAlbum album, bool is_user, StorageFolder root_folder)
            {
                try
                {
                    lock (progress)
                    {
                        if (progress.album_total_counts == 0)
                        {
                            progress.album_total_counts = 1;
                        }
                    }
                    List<RadioFullContent> radios = Load(album, is_user);
                    lock (progress)
                    {
                        progress.current_radio_total_counts = radios.Count;
                    }
                    if (root_folder == null)
                    {
                        root_folder = Windows.Storage.KnownFolders.MusicLibrary;
                    }
                    root_folder = await MyFolder.CreateFolder(Windows.Storage.KnownFolders.MusicLibrary, album.title);
                    foreach (RadioFullContent radio in radios)
                    {
                        await Task.Run(() => Export_Aysc(album, radio, root_folder));
                    }
                    lock (progress)
                    {
                        progress.album_finish_counts += 1;
                    }
                    return true;
                }
                catch
                {
                    return false;
                }

            }


            //获取多音频下载进度
            public ServiceUserDown.DownProgress GetDownProgress()
            {
                lock (UserDownService.Progress)
                {
                    return UserDownService.Progress;
                }
            }

            public ExportProgress GetExportProgress()
            {
                lock (progress)
                {
                    return progress;
                }
            }

            public class ExportProgress
            {
                public int album_total_counts = 0;
                public int album_finish_counts = 0;
                public int current_radio_total_counts = 0;
                public int current_radio_finish_counts = 0;
                public int radio_total_success_counts = 0;
            }

        }

    }

    public partial class LocalServ
    {
        /// <summary>
        ///本地收藏服务
        /// </summary>
        public class LocalFav : LocalServ
        {
            public bool Favor(RadioFullAlbum album, List<RadioFullContent> radio)
            {
                bool flag = false;
                Task<Task> task = new Task<Task>(() => FavorChannalReplayDownload(album, radio));
                task.Start();

                flag &= UserFavService.SaveFavProgram(album);
                UserFavService.FavRadio(radio);
                return flag;
            }
            public bool Favor(RadioFullAlbum album, RadioFullContent radio)
            {
                bool flag = true;
                Task<Task> task = new Task<Task>(() => FavorChannalReplayDownload(album, radio));
                task.Start();

                flag &= UserFavService.SaveFavProgram(album);
                flag &= UserFavService.FavRadio(radio);
                return flag;
            }

            private async Task FavorChannalReplayDownload(RadioFullAlbum album, List<RadioFullContent> radios)
            {
                if (album.type == 0)
                {
                    LocalDown local_down = new LocalDown();
                    await local_down.Download_Asyc(album, radios);
                }
            }
            private async Task FavorChannalReplayDownload(RadioFullAlbum album, RadioFullContent radio)
            {
                if (album.type == 0)
                {
                    LocalDown local_down = new LocalDown();
                    await local_down.Download_Asyc(album, radio);
                }
            }

            public List<RadioFullAlbum> Load(string user_name)
            {
                return UserFavService.LoadProgram(user_name);
            }
            public List<RadioFullContent> Load(RadioFullAlbum album)
            {
                return UserFavService.LoadRadio(album);
            }

            public bool RemoveProgram(string user_name, bool is_constrant)
            {
                return UserFavService.DeleteProgram(user_name, is_constrant);
            }
            public bool RemoveProgram(RadioFullAlbum album, bool is_constrant)
            {
                return UserFavService.DeleteProgram(album, is_constrant);
            }
            public bool RemoveRadio(RadioFullAlbum album, bool is_constrant)
            {
                return UserFavService.DeleteRadio(album, is_constrant);
            }

            public bool RemoveRadio(RadioFullContent radio, bool is_constrant)
            {
                return UserFavService.DeleteRadio(radio, is_constrant);
            }
        }
    }
}








