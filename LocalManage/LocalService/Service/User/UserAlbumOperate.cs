using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalManage.LocalService.User;
using PureRadio.LocalManage.LocalService.Service;
using PureRadio.LocalManage.DataModelsL;
using PureRadio.LocalManage.Iterfaces;
using LocalRadioManage.DBBuilder;

namespace PureRadio.LocalManage.LocalService.Service
{
    class UserAlbumOperate:IUserAlbumOperate
    {
        UserAlbumCardOperate userAlbumCardOperate = new UserAlbumCardOperate();
        UserAlbumRadioOperate userAlbumRadioOperate = new UserAlbumRadioOperate();
        UserInformsOperate userInformsOperate = new UserInformsOperate();
        AlbumOperate albumOperate = new AlbumOperate();

        UserInfo current_user = new UserInfo();

        public  UserAlbumOperate(UserInfo user)
        {
            SQLiteConnect.CreateLocalRadioManage();
            userInformsOperate.SaveUserInfo(user);
            userInformsOperate.UpdateUserInfo(user);
            current_user = user;
        }
        public UserAlbumOperate(string userPhoneNumber)
        {
            SQLiteConnect.CreateLocalRadioManage();
            UserInfo user = new UserInfo();
            user.PhoneNumber = userPhoneNumber;
            userInformsOperate.SaveUserInfo(user);
            user = userInformsOperate.SelectUserInfo(user);
            current_user = user;
        }

        public async Task<bool> Download(AlbumCardInfo card)
        {
            await albumOperate.Download(card);
            UserAlbumCardInfo temp = new UserAlbumCardInfo();
            temp.UserNumber = current_user.PhoneNumber;
            temp.State = 0;
            temp.AlbumId = card.ContentId;
            return  userAlbumCardOperate.SaveUserAlbumCardInfo(temp);
        }

        public async Task<bool> Download(AlbumCardInfo card,AlbumRadioInfo radio)
        {
            await Download(card);
            await albumOperate.Download(card,radio);
            UserAlbumRadioInfo temp = new UserAlbumRadioInfo();
            temp.UserNumber = current_user.UserName;
            temp.State = 0;
            temp.ProgramId = radio.ProgramId;
            temp.AlbumId = radio.AlbumId;
            return userAlbumRadioOperate.SaveUserAlbumRadioInfo(temp);
        }

        public bool Fav(AlbumCardInfo card)
        {
            albumOperate.Fav(card);
            UserAlbumCardInfo temp = new UserAlbumCardInfo();
            temp.UserNumber = current_user.PhoneNumber;
            temp.State = 1;
            temp.AlbumId = card.ContentId;
            return userAlbumCardOperate.SaveUserAlbumCardInfo(temp);
        }

        public bool Fav(AlbumCardInfo card, AlbumRadioInfo radio)
        {
            Fav(card);
            albumOperate.Fav(card,radio);
            UserAlbumRadioInfo temp = new UserAlbumRadioInfo();
            temp.UserNumber = current_user.PhoneNumber;
            temp.State = 1;
            temp.AlbumId = card.ContentId;
            temp.ProgramId = radio.ProgramId;
            return userAlbumRadioOperate.SaveUserAlbumRadioInfo(temp);
        }

        //type 0->下载，1->收藏，2->两者
        public List<AlbumCardInfo> Load(int type)
        {
            List<UserAlbumCardInfo> user_cards= userAlbumCardOperate.SelectUserAlbumCardInfo(current_user.PhoneNumber,type);
            if (type != 2)
            {
                List<UserAlbumCardInfo> user_cards_2 = userAlbumCardOperate.SelectUserAlbumCardInfo(current_user.PhoneNumber, 2);
                user_cards.AddRange(user_cards_2);
            }
            List<AlbumCardInfo> cards = new List<AlbumCardInfo>();
            AlbumCardInfo card = new AlbumCardInfo();
            foreach (UserAlbumCardInfo user_card in user_cards)
            {
               card.ContentId = user_card.AlbumId;
               cards.Add(albumOperate.CardOperate.SelectAlbumCardInfo(card));
            }
            return cards;

        }

        public List<AlbumRadioInfo> Load(AlbumCardInfo card, int type)
        {
            List<UserAlbumRadioInfo> user_radios = userAlbumRadioOperate.SelectUserAlbumRadioInfo(current_user.PhoneNumber,card.ContentId,type);
            if (type != 2)
            {
                List<UserAlbumRadioInfo> user_radios_2 = userAlbumRadioOperate.SelectUserAlbumRadioInfo(current_user.PhoneNumber, card.ContentId, 2);
                user_radios.AddRange(user_radios_2);
            }
            List<AlbumRadioInfo> radios = new List<AlbumRadioInfo>();
            AlbumRadioInfo radio = new AlbumRadioInfo();
            foreach (UserAlbumRadioInfo user_radio in user_radios)
            {
                radio.AlbumId = user_radio.AlbumId;
                radio.ProgramId = user_radio.ProgramId;
                radios.Add(albumOperate.RadioOperate.SelectAlbumRadioInfo(radio));
            }
            return radios;

        }

        //清空用户下载/收藏
        public bool Remove(int type)
        {
            List<AlbumCardInfo> infos= Load(type);
            foreach(AlbumCardInfo info in infos)
            {
                Remove(info, type);
            }
            return true;
        }
        //清空用户某一专辑下载/收藏
        public bool Remove(AlbumCardInfo card,int type)
        {
            //找到card是否存在
            UserAlbumCardInfo user_card = new UserAlbumCardInfo();
            user_card.UserNumber = current_user.PhoneNumber;
            user_card.AlbumId = card.ContentId;
            user_card = userAlbumCardOperate.SelectUserAlbumCardInfo(user_card);
            if (user_card == null)
            {
                return false;
            }
            //判断如何删除
            int delete = 0;
            int update = 0;
            if (type == 2||type==user_card.State)
            {
               return userAlbumCardOperate.DeleteUserAlbumCardInfo(user_card);
            }
            else if (type == 1)
            {
                delete = 1;
                update = 0;
            }
            else if (type == 0)
            {
                delete = 0;
                update = 1;
            }
            //按要求删除
            if (user_card.State == 2)
            {
                user_card.State = update;
                userAlbumCardOperate.UpdateUserAlbumCardInfo(user_card);
            }

            List<UserAlbumRadioInfo> user_fav_radios = userAlbumRadioOperate.SelectUserAlbumRadioInfo(current_user.PhoneNumber, card.ContentId, delete);
            foreach (UserAlbumRadioInfo radio_ in user_fav_radios)
            {
                userAlbumRadioOperate.DeleteUserAlbumRadioInfo(radio_);
            }
            List<UserAlbumRadioInfo> user_all_radios = userAlbumRadioOperate.SelectUserAlbumRadioInfo(current_user.PhoneNumber, card.ContentId, 2);
            foreach (UserAlbumRadioInfo radio_ in user_all_radios)
            {
                radio_.State = update;
                userAlbumRadioOperate.UpdateUserAlbumRadioInfo(radio_);
            }
            return true;
        }
        //清空用户某一音频下载/收藏
        public bool Remove(AlbumRadioInfo radio, int type)
        {
            //找到radio是否存在
            UserAlbumRadioInfo user_radio = new UserAlbumRadioInfo();
            user_radio.UserNumber = current_user.PhoneNumber;
            user_radio.AlbumId = radio.AlbumId;
            user_radio.ProgramId = radio.ProgramId;
            user_radio = userAlbumRadioOperate.SelectUserAlbumRadioInfo(user_radio);
            if (radio == null)
            {
                return false;
            }
            //判断如何删除
            if (type == 2||type==user_radio.State)
            {
                userAlbumRadioOperate.DeleteUserAlbumRadioInfo(user_radio);
            }
            else if (type == 1&&user_radio.State==2)
            {
                user_radio.State = 0;
            }
            else if (type == 0&& user_radio.State == 2)
            {
                user_radio.State = 1;
            }
            return userAlbumRadioOperate.UpdateUserAlbumRadioInfo(user_radio);
        }

    }
}
