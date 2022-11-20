using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalRadioManage.LocalService.User;
using PureRadio.LocalRadioManage.LocalService.Service;
using PureRadio.LocalRadioManage.DataModelsL;

namespace PureRadio.LocalRadioManage.LocalService.Service
{
    class UserChannalOperate 
    {
        UserChannalCardOperate userChannalCardOperate = new UserChannalCardOperate();
        UserChannalRadioOperate userChannalRadioOperate = new UserChannalRadioOperate();
        UserInformsOperate userInformsOperate = new UserInformsOperate();
        ChannalOperate channalOperate = new ChannalOperate();

        UserInfo current_user = new UserInfo();

        public UserChannalOperate(UserInfo user)
        {
            userInformsOperate.SaveUserInfo(user);
            userInformsOperate.UpdateUserInfo(user);
            current_user = user;
        }
        public UserChannalOperate(string userPhoneNumber)
        {
            UserInfo user = new UserInfo();
            user.PhoneNumber = userPhoneNumber;
            userInformsOperate.SaveUserInfo(user);
            user = userInformsOperate.SelectUserInfo(user);
            current_user = user;
        }

        public async Task<bool> Download(ChannalCardInfo card)
        {
            await channalOperate.Download(card);
            UserChannalCardInfo temp = new UserChannalCardInfo();
            temp.UserNumber = current_user.PhoneNumber;
            temp.State = 0;
            temp.ChannalId = card.RadioId;
            return userChannalCardOperate.SaveUserChannalCardInfo(temp);
        }

        public async Task<bool> Download(ChannalCardInfo card, ChannalRadioInfo radio)
        {
            await Download(card);
            await channalOperate.Download(card, radio);
            UserChannalRadioInfo temp = new UserChannalRadioInfo();
            temp.UserNumber = current_user.UserName;
            temp.State = 0;
            temp.ProgramId = radio.ProgramId;
            temp.ChannalId = radio.RadioId;
            temp.StartTime = radio.StartTime;
            temp.EndTime = radio.EndTime;
            temp.Date = radio.Date.ToString("yyyyMMdd", null);
            return userChannalRadioOperate.SaveUserChannalRadioInfo(temp);
        }

        public bool Fav(ChannalCardInfo card)
        {
            channalOperate.Fav(card);
            UserChannalCardInfo temp = new UserChannalCardInfo();
            temp.UserNumber = current_user.PhoneNumber;
            temp.State = 1;
            temp.ChannalId = card.RadioId;
            return userChannalCardOperate.SaveUserChannalCardInfo(temp);
        }

        public bool Fav(ChannalCardInfo card, ChannalRadioInfo radio)
        {
            Fav(card);
            channalOperate.Fav(card, radio);
            UserChannalRadioInfo temp = new UserChannalRadioInfo();
            temp.UserNumber = current_user.PhoneNumber;
            temp.State = 1;
            temp.ChannalId = card.RadioId;
            temp.ProgramId = radio.ProgramId;
            temp.StartTime = radio.StartTime;
            temp.EndTime = radio.EndTime;
            temp.Date = radio.Date.ToString("yyyyMMdd", null);
            return userChannalRadioOperate.SaveUserChannalRadioInfo(temp);
        }

        //type 0->下载，1->收藏，2->两者
        public List<ChannalCardInfo> Load(int type)
        {
            List<UserChannalCardInfo> user_cards = userChannalCardOperate.SelectUserChannalCardInfo(current_user.PhoneNumber, type);
            if (type != 2)
            {
                List<UserChannalCardInfo> user_cards_2= userChannalCardOperate.SelectUserChannalCardInfo(current_user.PhoneNumber, 2);
                user_cards.AddRange(user_cards_2);
            }

            List<ChannalCardInfo> cards = new List<ChannalCardInfo>();
            ChannalCardInfo card = new ChannalCardInfo();
            foreach (UserChannalCardInfo user_card in user_cards)
            {
                card.RadioId = user_card.ChannalId;
                cards.Add(channalOperate.CardOperate.SelectChannalCardInfo(card));
            }
            return cards;

        }

        public List<ChannalRadioInfo> Load(ChannalCardInfo card, int type)
        {
            List<UserChannalRadioInfo> user_radios = userChannalRadioOperate.SelectUserChannalRadioInfo(current_user.PhoneNumber, card.RadioId, type);
            if (type != 2)
            {
                List<UserChannalRadioInfo> user_radios_2 = userChannalRadioOperate.SelectUserChannalRadioInfo(current_user.PhoneNumber, card.RadioId, 2);
                user_radios.AddRange(user_radios_2);
            }
            List<ChannalRadioInfo> radios = new List<ChannalRadioInfo>();
            ChannalRadioInfo radio = new ChannalRadioInfo();
            foreach (UserChannalRadioInfo user_radio in user_radios)
            {
                radio.RadioId = user_radio.ChannalId;
                radio.ProgramId = user_radio.ProgramId;
                radio.Date = DateTime.ParseExact(user_radio.Date, "yyyyMMdd", null);
                radio.StartTime = user_radio.StartTime;
                radio.EndTime = user_radio.EndTime;
                radios.Add(channalOperate.RadioOperate.SelectChannalRadioInfo(radio));
            }
            return radios;
        }

        //清空用户下载/收藏
        public bool Remove(int type)
        {
            List<ChannalCardInfo> infos = Load(type);
            foreach (ChannalCardInfo info in infos)
            {
                Remove(info, type);
            }
            return true;
        }
        //清空用户某一专辑下载/收藏
        public bool Remove(ChannalCardInfo card, int type)
        {
            //找到card是否存在
            UserChannalCardInfo user_card = new UserChannalCardInfo();
            user_card.UserNumber = current_user.PhoneNumber;
            user_card.ChannalId = card.RadioId;
            user_card = userChannalCardOperate.SelectUserChannalCardInfo(user_card);
            if (user_card == null)
            {
                return false;
            }
            //判断如何删除
            int delete = 0;
            int update = 0;
            if (type == 2 || type == user_card.State)
            {
                return userChannalCardOperate.DeleteUserChannalCardInfo(user_card);
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
                userChannalCardOperate.UpdateUserChannalCardInfo(user_card);
            }

            List<UserChannalRadioInfo> user_fav_radios = userChannalRadioOperate.SelectUserChannalRadioInfo(current_user.PhoneNumber, card.RadioId, delete);
            foreach (UserChannalRadioInfo radio_ in user_fav_radios)
            {
                userChannalRadioOperate.DeleteUserChannalRadioInfo(radio_);
            }
            List<UserChannalRadioInfo> user_all_radios = userChannalRadioOperate.SelectUserChannalRadioInfo(current_user.PhoneNumber, card.RadioId, 2);
            foreach (UserChannalRadioInfo radio_ in user_all_radios)
            {
                radio_.State = update;
                userChannalRadioOperate.UpdateUserChannalRadioInfo(radio_);
            }
            return true;
        }
        //清空用户某一音频下载/收藏
        public bool Remove(ChannalRadioInfo radio, int type)
        {
            //找到radio是否存在
            UserChannalRadioInfo user_radio = new UserChannalRadioInfo();
            user_radio.UserNumber = current_user.PhoneNumber;
            user_radio.ChannalId = radio.RadioId;
            user_radio.ProgramId = radio.ProgramId;
            user_radio.StartTime = radio.StartTime;
            user_radio.EndTime = radio.EndTime;
            user_radio.Date = radio.Date.ToString("yyyyMMdd",null);
            user_radio = userChannalRadioOperate.SelectUserChannalRadioInfo(user_radio);
            if (radio == null)
            {
                return false;
            }
            //判断如何删除
            if (type == 2 || type == user_radio.State)
            {
                userChannalRadioOperate.DeleteUserChannalRadioInfo(user_radio);
            }
            else if (type == 1 && user_radio.State == 2)
            {
                user_radio.State = 0;
            }
            else if (type == 0 && user_radio.State == 2)
            {
                user_radio.State = 1;
            }
            return userChannalRadioOperate.UpdateUserChannalRadioInfo(user_radio);
        }

    }
 

   

}
