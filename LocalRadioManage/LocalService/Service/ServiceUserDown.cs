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
        //用于多音频进度
        public  DownProgress Progress = new DownProgress();
        //用于单音频进度
        public MyFile.CreateFileProgress.Progress RadioProgress = new MyFile.CreateFileProgress.Progress();
       
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
                StorageFile img_flie = await MyFile.CreateFile(album_folder, album.remote_cover);
                album.local_cover = new Uri(img_flie.Path);
           
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
                MyFile.CreateFileProgress create = new MyFile.CreateFileProgress();
                lock (create.progress)
                {
                    if (radio.remote_radio_uri == null)
                    {
                        return false;
                    }
                    create.progress.file_name = System.Web.HttpUtility.UrlDecode(radio.remote_radio_uri.Segments.Last());
                    lock (Progress.FileDownProgress)
                    {
                        Progress.FileDownProgress.Add(create.progress);
                       
                    }
                    lock (Progress.FileDownProgressMap)
                    {
                        Progress.FileDownProgressMap.Add(create.progress.file_name, create.progress);
                    }
                }
                lock (Progress)
                {
                    Progress.down_counts = (Progress.down_counts+1)%Int16.MaxValue;
                }
                StorageFile radio_file = await create.CreateFile(album_folder, radio.remote_radio_uri);
                lock (Progress)
                {
                    Progress.down_progress = (Progress.down_progress+1)% Int16.MaxValue;
                }
                if (radio_file == null)
                {
                    return false;
                }
               
                radio.local_radio_uri = new Uri(radio_file.Path);
                return user_inform.UserDown.SaveUserDownRadio(radio);
            }
            catch
            {
                return false; 
            }

        }
        public async Task<bool> DownRadio(List<RadioFullContent> radios)
        {
        
          

            try
            {
             
                foreach (RadioFullContent radio in radios)
                {
                    bool flag= await DownRadio(radio);
               
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
               
                //记录删除失败的专辑/音频
                List<RadioFullAlbum> fail_delete_albums = new List<RadioFullAlbum>();
                List<RadioFullContent> fail_delete_radios = new List<RadioFullContent>();

                //删除对应音频
                if (lonely_radios != null)
                {
                    foreach (RadioFullContent radio in lonely_radios)
                    {
                        if (!await MyFile.DeleteFile(radio.local_radio_uri))
                        {
                            fail_delete_radios.Add(radio);
                        }
                    }
                }

                //删除对应专辑
                if (lonely_albums!= null)
                {
                    foreach (RadioFullAlbum album in lonely_albums)
                    {
                        if (!await MyFolder.DeleteFolder(Default.DefalutStorage.radio_folder, album.id.ToString()))
                        {
                            fail_delete_albums.Add(album);
                        }
                    }
                }

               

                try
                {
                    user_inform.UserDown.LocalDown.DeleteLocalDownProgram_Check(true);
                    user_inform.UserDown.LocalDown.DeleteLocalDownRadios_Check(true);
                    user_inform.UserDown.LocalDown.SaveLocalDownProgram(fail_delete_albums);
                    user_inform.UserDown.LocalDown.SaveLocalDownRadio(fail_delete_radios);
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

                List<RadioFullContent> fail_delete_radios = new List<RadioFullContent>();
               

                if (lonely_radios != null)
                {
                   

                    foreach (RadioFullContent radio in lonely_radios)
                    {
                        if(! await MyFile.DeleteFile(radio.local_radio_uri))
                        {
                            fail_delete_radios.Add(radio);
                        }
                    }
                }
                

                try
                {
                    //删除表中数据
                    user_inform.UserDown.LocalDown.DeleteLocalDownRadios_Check(true);
                    user_inform.UserDown.LocalDown.SaveLocalDownRadio(fail_delete_radios);
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
          public  int down_progress =0;
          public List<MyFile.CreateFileProgress.Progress> FileDownProgress = new List<MyFile.CreateFileProgress.Progress>();
        }
    }
}
