using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalRadioManage.DataModelsL;
using PureRadio.LocalRadioManage.LocalService.Local;
using PureRadio.LocalRadioManage.DBBuilder.TableObj;
using LocalRadioManage.DBBuilder;

namespace PureRadio.LocalRadioManage.LocalService.User
{
    class UserChannalCardOperate
    {
        public readonly string TableName = UserChannalCard.TableName;
        private List<string> SelectedCol = new List<string>();


        public UserChannalCardOperate()
        {
            SelectedCol = SQLiteConnect.TableHandle.GetColNames(TableName).ToList();

        }

        public bool SaveUserChannalCardInfo(UserChannalCardInfo card)
        {
            UserChannalCardInfo temp = SelectUserChannalCardInfo(card);
            if (temp != null)
            {
                if (temp.State != card.State)
                {
                    temp.State = 2;
                }
                return UpdateUserChannalCardInfo(temp);
            }
            return SQLiteConnect.TableHandle.AddRecord(TableName, card.GetStore(card));
        }

        public bool DeleteUserChannalCardInfo(UserChannalCardInfo card)
        {
            string ConditionExpress = UserChannalCard.ChannalId[0]
                             + "=" + card.ChannalId
                             + " and " + UserChannalCard.UserNumber[0]
                             + "=" + "'" + card.UserNumber + "'";
            return SQLiteConnect.TableHandle.DeleteRecords(TableName, ConditionExpress, true);
        }

        public bool UpdateUserChannalCardInfo(UserChannalCardInfo card)
        {
            List<object> store = card.GetStore(card);
            string ConditionExpress = UserChannalCard.ChannalId[0]
                             + "=" + card.ChannalId
                             + " and " + UserChannalCard.UserNumber[0]
                             + "=" + "'" + card.UserNumber + "'";
            return SQLiteConnect.TableHandle.UpdateRecord(TableName, ConditionExpress, store);
        }

        public List<UserChannalCardInfo> SelectUserChannalCardInfo()
        {
            return SelectUserChannalCardInfo("");
        }

        public List<UserChannalCardInfo> SelectUserChannalCardInfo(string ConditionExpress)
        {
            List<List<object>> cards = null;
            List<UserChannalCardInfo> infos = new List<UserChannalCardInfo>();
            SQLiteConnect.TableHandle.SelectRecords(TableName, SelectedCol, ConditionExpress, ref cards);
            if (cards == null)
            {
                return null;
            }
            foreach (List<object> card in cards)
            {
                infos.Add(new UserChannalCardInfo(card));
            }
            return infos;
        }

        public UserChannalCardInfo SelectUserChannalCardInfo(UserChannalCardInfo card)
        {
            string ConditionExpress = UserChannalCard.ChannalId[0]
                            + "=" + card.ChannalId
                            + " and " +  UserChannalCard.UserNumber[0]
                            + "=" + "'" + card.UserNumber + "'";
            List<UserChannalCardInfo> infos = SelectUserChannalCardInfo(ConditionExpress);
            return infos.Count > 0 ? infos[0] : null;
        }

        public List<UserChannalCardInfo> SelectUserChannalCardInfo(string userNumber,int type)
        {
            string ConditionExpress = UserChannalCard.State[0]
                            + "=" + type
                            + " and " +UserChannalCard.UserNumber[0]
                            + "=" + "'" + userNumber + "'";
            List<UserChannalCardInfo> infos = SelectUserChannalCardInfo(ConditionExpress);
            return infos;
        }
    }
   


}
