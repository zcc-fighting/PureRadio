using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using LocalRadioManage.DBBuilder.TableObj;
using LocalRadioManage.LocalService.UserInforms;
using LocalRadioManage.DataModelTransform;
using LocalRadioManage.StorageOperate;
using Windows.Storage;


namespace LocalRadioManage.LocalService
{
    class ServiceUserDown
    {
        UserInform user_inform = new UserInform();
        public  DownProgress Progress = new DownProgress();
       
        public async Task<bool> SaveDownProgram(RadioFullAlbum album)
        {
            user_inform.SetUserInform(album.user);
            List<object> down_program= ChannalAlbumTransform.Local.ToLocalChannalAlbumStorage(album);
            try
            {
               user_inform.UserDown.SetUserDown(down_program);
                //已存在则返回
                if (user_inform.UserDown.LoadUserDownProgram() != null)
                {
                    return true;
                }
               user_inform.UserDown.SetUserDown(album.user);
                //修改图片本地名称
              StorageFile img_flie=await MyFile.CreateFile(Default.DefalutStorage.image_folder, album.cover);
              down_program[LocalChannalAlbum.ColLocation[LocalChannalAlbum.ChannalAlbunLocalPath]] = img_flie.Path;
              return  user_inform.UserDown.SaveUserDownProgram(down_program);
            }
            catch
            {
                return false;
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
           if(!await SaveDownProgram(album))
            {
                return false;
            }
            await DownRadio(radios);

            return true;
        }

        public async Task<bool> DownRadio(RadioFullContent radio)
        {
            user_inform.SetUserInform(radio.user);
            List<object> down_radio = RadioTransform.Local.ToLocalRadioStorage(radio);
            try
            {
                user_inform.UserDown.SetUserDown(down_radio,true);
                //已存在则返回
                if (user_inform.UserDown.LoadUserDownRadio() != null)
                {
                    return true;
                }
                user_inform.UserDown.SetUserDown(radio.user);
                //修改音频本地名称
                StorageFile radio_file = await MyFile.CreateFile(Default.DefalutStorage.image_folder, radio.radio_uri);
                down_radio[LocalRadio.ColLocation[LocalRadio.RadioLocalPath]] = radio_file.Path;
                return user_inform.UserDown.SaveUserDownRadio(down_radio);
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

        public List<RadioFullAlbum> LoadProgram(string user_name)
        {

            return null;
        }

        public List<RadioFullContent> LoadRadio(RadioFullAlbum album)
        {

            return null;
        }



        public class DownProgress
        {
          public  int down_counts = 0;
          public  int down_progress = -1;
          public  List<bool> down_situation = new List<bool>();
        }
    }
}
