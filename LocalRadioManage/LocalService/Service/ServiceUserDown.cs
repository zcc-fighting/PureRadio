using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using LocalRadioManage.DBBuilder.TableObj;
using LocalRadioManage.LocalService.UserInforms;
using LocalRadioManage.StorageOperate;
using Windows.Storage;


namespace LocalRadioManage.LocalService
{
  public  class ServiceUserDown
    {
        UserInform user_inform = new UserInform();
        public  DownProgress Progress = new DownProgress();
       
        //保存节目->节目表+文件(图片)
        public async Task<StorageFile> SaveDownProgram(RadioFullAlbum album)
        {
            user_inform.SaveUser(album.user);
            user_inform.SetUserInform(album.user);
            try
            {
               user_inform.UserDown.SetUserDown(album);
                //用户已存在该节目则返回
                if (user_inform.UserDown.LoadUserDownProgram()!= null)
                {
                    return null;
                }
              
                user_inform.UserDown.SetUserDown(album.user);
                //修改图片本地名称
                StorageFolder album_folder = await MyFolder.CreateFolder(Default.DefalutStorage.radio_folder, album.id.ToString());
                StorageFile img_flie = await MyFile.CreateFile(album_folder, album.cover);
                album.cover = new Uri(img_flie.Path);
           
                user_inform.UserDown.SaveUserDownProgram(album);

                return img_flie;
            }
            catch
            {
                return null;
            }
            
        }
        public async void SaveDownProgram(List<RadioFullAlbum> albums)
        {
            foreach(RadioFullAlbum album in albums)
            {
                await SaveDownProgram(album);
            }
        }
        public async Task<bool> SaveDownProgram(RadioFullAlbum album, List<RadioFullContent> radios)
        {
            StorageFile img_file = await SaveDownProgram(album);

            if (img_file == null)
            {
                return false;
            }
            await DownRadio(radios);

            return true;
        }
        //保存音频->音频表+文件(音频)
        public async Task<bool> DownRadio(RadioFullContent radio)
        {
            user_inform.SaveUser(radio.user);
            user_inform.SetUserInform(radio.user);
          
            try
            {
                user_inform.UserDown.SetUserDown(radio);
                //用户已存在该音频则返回
                if (user_inform.UserDown.LoadUserDownRadio() !=null)
                {
                    return true;
                }
                user_inform.UserDown.SetUserDown(radio.user);
                //修改音频本地名称
                StorageFolder album_folder =await MyFolder.CreateFolder(Default.DefalutStorage.radio_folder,radio.channel_id.ToString());
                StorageFile radio_file = await MyFile.CreateFile(album_folder, radio.radio_uri);
                radio.radio_uri = new Uri(radio_file.Path);
                return user_inform.UserDown.SaveUserDownRadio(radio);
            }
            catch
            {
                return false; 
            }

        }
        public async Task<bool> DownRadio(List<RadioFullContent> radios)
        {
            lock(Progress)
            {
                Progress.down_counts = radios.Count;
                Progress.down_progress = -1;
                Progress.down_situation.Clear();
            }

            try
            {
                foreach (RadioFullContent radio in radios)
                {
                  bool flag= await DownRadio(radio);
                    lock (Progress)
                    {
                        Progress.down_progress += 1;
                        Progress.down_situation.Add(flag);
                    }

                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        //根据用户/专辑->载入节目/音频
        public List<RadioFullAlbum> LoadProgram(string user_name)
        {
            try
            {
                user_inform.UserDown.SetUserDown(user_name);
                return user_inform.UserDown.LoadUserDownProgram();
            }
            catch
            {
                return null;
            }
           
        }
        public List<RadioFullContent> LoadRadio(RadioFullAlbum album)
        {
            try
            {
                user_inform.UserDown.SetUserDown(album);
                return user_inform.UserDown.LoadUserDownRadio();
            }
            catch
            {
                return null;
            }
           
        }

        //删除节目->节目表+文件(图片)
        public async Task<bool> DeleteProgram(string user_name,bool is_constrant)
        {
            try
            {
               user_inform.UserDown.SetUserDown(user_name);
               return await DeleteProgram(is_constrant);
                
            }
            catch
            {
                return false;
            }
           
        }
        public async Task<bool> DeleteProgram(RadioFullAlbum album, bool is_constrant)
        {
            try
            {
                user_inform.UserDown.SetUserDown(album);
                return await DeleteProgram(is_constrant);
            }
            catch
            {
                return false;
            }
        }
        private async Task<bool> DeleteProgram(bool is_constrant)
        {
            if (user_inform.UserDown.DeleteUsrDownProgram(is_constrant))
            {
                //获取已无用户依赖的本地节目/音频
                List<RadioFullAlbum> lonely_albums = user_inform.UserDown.LocalDown.LoadLocalDownProgram_Check();
                List<RadioFullContent> lonely_radios = user_inform.UserDown.LocalDown.LoadLocalDownRaio_Check();

                List<string> album_folders = new List<string>();
                List<Uri> img_uris = new List<Uri>();
                List<Uri> radio_uris = new List<Uri>();
              

                //删除对应专辑
                if (lonely_albums!= null)
                {
                    user_inform.UserDown.LocalDown.DeleteLocalDownProgram_Check(true);
                    foreach (RadioFullAlbum album in lonely_albums)
                    {
                        img_uris.Add(album.cover);
                        album_folders.Add(album.id.ToString());
                    }
                }

                //删除对应音频
                if (lonely_radios != null)
                {
                    user_inform.UserDown.LocalDown.DeleteLocalDownRadios_Check(true);
                    foreach (RadioFullContent radio in lonely_radios)
                    {
                        radio_uris.Add(radio.radio_uri);
                    }
                }
                 

                try
                {
                    await MyFile.DeleteFile(img_uris);
                    await MyFile.DeleteFile(radio_uris);
                    await MyFolder.DeleteFolder(Default.DefalutStorage.radio_folder, album_folders);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        //删除音频->音频表+文件(音频)
        public async Task<bool> DeleteRadio(RadioFullAlbum album,bool is_constrant)
        {
            try
            {
                user_inform.UserDown.SetUserDown(album);
                return await DeleteRadio(is_constrant);
            }
            catch
            {
                return false;
            }
        }
        public async  Task<bool> DeleteRadio(RadioFullContent radio,bool is_constrant)
        {

            try
            {
                user_inform.UserDown.SetUserDown(radio);
                return await DeleteRadio(is_constrant);
            }
            catch
            {
                return false;
            }
        }
        private async Task<bool> DeleteRadio(bool is_constrant)
        {
            if (user_inform.UserDown.DeleteUsrDownRadios(is_constrant))
            {

                //获取已无用户依赖的音频
                List<RadioFullContent> lonely_radios = user_inform.UserDown.LocalDown.LoadLocalDownRaio_Check();

                //删除本地对应音频
                List<Uri> radio_uris = new List<Uri>();

                if (lonely_radios != null)
                {
                    //删除表中数据
                    user_inform.UserDown.LocalDown.DeleteLocalDownRadios_Check(true);

                    foreach (RadioFullContent radio in lonely_radios)
                    {
                        radio_uris.Add(radio.radio_uri);
                    }
                }
                

                try
                {
                    await MyFile.DeleteFile(radio_uris);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public class DownProgress
        {
          public  int down_counts = 0;
          public  int down_progress = -1;
          public  List<bool> down_situation = new List<bool>();
        }
    }
}
