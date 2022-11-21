using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalManage.DataModelsL;
using PureRadio.LocalManage.LocalService.Local;
using PureRadio.LocalManage.DBBuilder.TableObj;
using LocalRadioManage.DBBuilder;

namespace PureRadio.LocalManage.LocalService.User
{
    class UserAlbumCardOperate
    {
        public readonly string TableName = UserAlbumCard.TableName;
        private List<string> SelectedCol = new List<string>();

        public UserAlbumCardOperate()
        {
            SQLiteConnect.CreateLocalRadioManage();
            SelectedCol = SQLiteConnect.TableHandle.GetColNames(TableName).ToList();
        }

        public bool SaveUserAlbumCardInfo(UserAlbumCardInfo card)
        {
            UserAlbumCardInfo temp=SelectUserAlbumCardInfo(card);
            if (temp != null)
            {
                if (temp.State != card.State)
                {
                    temp.State = 2;
                }
                return UpdateUserAlbumCardInfo(temp);
            }
            return SQLiteConnect.TableHandle.AddRecord(TableName, card.GetStore(card));
        }

        public bool DeleteUserAlbumCardInfo(string userNumber)
        {
            string ConditionExpress =UserAlbumCard.UserNumber[0]
                             + "=" +"'"+ userNumber + "'";
            return SQLiteConnect.TableHandle.DeleteRecords(TableName, ConditionExpress, true);
        }

        public bool DeleteUserAlbumCardInfo(UserAlbumCardInfo card)
        {
            string ConditionExpress =  UserAlbumCard.AlbumId[0]
                             + "=" + card.AlbumId
                             +" and " +UserAlbumCard.UserNumber[0]
                             +"=" + "'" + card.UserNumber + "'";
            return SQLiteConnect.TableHandle.DeleteRecords(TableName, ConditionExpress, true);
        }

        public bool UpdateUserAlbumCardInfo(UserAlbumCardInfo card)
        {
            List<object> store = card.GetStore(card);
            string ConditionExpress =  UserAlbumCard.AlbumId[0]
                             + "=" + card.AlbumId
                             + " and " +  UserAlbumCard.UserNumber[0]
                             + "=" + "'" + card.UserNumber + "'";
            return SQLiteConnect.TableHandle.UpdateRecord(TableName, ConditionExpress, store);
        }

        public List<UserAlbumCardInfo> SelectUserAlbumCardInfo()
        {
            return SelectUserAlbumCardInfo("");
        }
    
        public List<UserAlbumCardInfo> SelectUserAlbumCardInfo(string ConditionExpress)
        {
            List<List<object>> cards = null;
            List<UserAlbumCardInfo> infos = new List<UserAlbumCardInfo>();
            SQLiteConnect.TableHandle.SelectRecords(TableName, SelectedCol, ConditionExpress, ref cards);
            if (cards == null)
            {
                return null;
            }
            foreach (List<object> card in cards)
            {
                infos.Add(new UserAlbumCardInfo(card));
            }
            return infos;
        }
        public UserAlbumCardInfo SelectUserAlbumCardInfo(UserAlbumCardInfo card)
        {
            string ConditionExpress = UserAlbumCard.AlbumId[0]
                            + "=" + card.AlbumId
                            + " and " +UserAlbumCard.UserNumber[0]
                            + "=" + "'" + card.UserNumber + "'";
            List<UserAlbumCardInfo> infos = SelectUserAlbumCardInfo(ConditionExpress);
            return infos.Count > 0 ? infos[0] : null;
        }
        public List<UserAlbumCardInfo> SelectUserAlbumCardInfo(string UserNumber,int type)
        {
            string ConditionExpress = UserAlbumCard.State[0]
                            + "=" + type
                            +" and "+  UserAlbumCard.UserNumber[0]
                            +"=" + "'" + UserNumber + "'";
            List<UserAlbumCardInfo> infos = SelectUserAlbumCardInfo(ConditionExpress);
            return infos;
        }


    }

    
}
