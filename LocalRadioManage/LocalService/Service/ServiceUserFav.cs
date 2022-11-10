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
    class ServiceUserFav
    {
        UserInform user_inform = new UserInform();

        //保存收藏节目
        public bool SaveFavProgram(RadioFullAlbum album)
        {
            user_inform.SaveUser(album.user);
            user_inform.SetUserInform(album.user);
            try
            {
                return user_inform.UserFav.SaveUserFavProgram(album);
            }
            catch
            {
                return false;
            }
        }
        public void SaveFavProgram(List<RadioFullAlbum> albums)
        {
            foreach (RadioFullAlbum album in albums)
            {
                SaveFavProgram(album);
            }
        }
        public bool SaveFavProgram(RadioFullAlbum album, List<RadioFullContent> radios)
        {
            if (!SaveFavProgram(album))
            {
                return false;
            }
            FavRadio(radios);
            return true;
        }

        public bool FavRadio(RadioFullContent radio)
        {
            user_inform.SaveUser(radio.user);
            user_inform.SetUserInform(radio.user);
            try
            {
                return user_inform.UserFav.SaveUserFavRadio(radio);
            }
            catch
            {
                return false;
            }
        }
        public void FavRadio(List<RadioFullContent> radios)
        {
            foreach (RadioFullContent radio in radios)
            {
                FavRadio(radio);
            }
        }

        public List<RadioFullAlbum> LoadProgram(string user_name)
        {
            try
            {
                user_inform.UserFav.SetUserFav(user_name);
                return user_inform.UserFav.LoadUserFavProgram();
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
                user_inform.UserFav.SetUserFav(album);
                return user_inform.UserFav.LoadUserFavRadio();
            }
            catch
            {
                return null;
            }
        }

        public bool DeleteProgram(string user_name, bool is_constrant)
        {
            try
            {
                user_inform.UserFav.SetUserFav(user_name);
                return user_inform.UserFav.DeleteUserFavProgram(is_constrant);
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteProgram(RadioFullAlbum album, bool is_constrant)
        {
            try
            {
                user_inform.UserFav.SetUserFav(album);
                return user_inform.UserFav.DeleteUserFavProgram(is_constrant);
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteRadio(RadioFullAlbum album, bool is_constrant)
        {
            try
            {
                user_inform.UserFav.SetUserFav(album);
                return user_inform.UserFav.DeleteUserFavRadios(is_constrant);
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteRadio(RadioFullContent radio, bool is_constrant)
        {
            try
            {
                user_inform.UserFav.SetUserFav(radio);
                return user_inform.UserFav.DeleteUserFavRadios(is_constrant);
            }
            catch
            {
                return false;
            }
        }
    }
}
