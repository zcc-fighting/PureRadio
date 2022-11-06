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
    class ServiceUserFav
    {
        UserInform user_inform = new UserInform();
        public bool SaveFavProgram(RadioFullAlbum album)
        {
            user_inform.SetUserInform(album.user);
            List<object> fav_program = ChannalAlbumTransform.Local.ToLocalChannalAlbumStorage(album);
            try
            {
                return user_inform.UserFav.SaveUserFavProgram(fav_program);
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
            user_inform.SetUserInform(radio.user);
            List<object> down_radio = RadioTransform.Local.ToLocalRadioStorage(radio);
            try
            {
                return user_inform.UserFav.SaveUserFavRadio(down_radio);
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

            return null;
        }

        public List<RadioFullContent> LoadRadio(RadioFullAlbum album)
        {

            return null;
        }
    }
}
