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
    class UserChannalRadioOperate
    {
        public readonly string TableName = UserChannalRadio.TableName;
        private List<string> SelectedCol = new List<string>();

        public UserChannalRadioOperate()
        {
            SelectedCol = SQLiteConnect.TableHandle.GetColNames(TableName).ToList();

        }

        public bool SaveUserChannalRadioInfo(UserChannalRadioInfo radio)
        {
            UserChannalRadioInfo temp = SelectUserChannalRadioInfo(radio);
            if (temp != null)
            {
                if (temp.State != radio.State)
                {
                    temp.State = 2;
                }
                return UpdateUserChannalRadioInfo(temp);
            }
            return SQLiteConnect.TableHandle.AddRecord(TableName, radio.GetStore(radio));
        }
        public bool DeleteUserChannalRadioInfo(UserChannalCardInfo card)
        {
            string ConditionExpress =  UserChannalRadio.UserNumber[0]
                + "=" + "'" + card.UserNumber + "'"
                + " and " +  UserChannalRadio.ChannalId[0]
                + "=" + card.ChannalId;
            return SQLiteConnect.TableHandle.DeleteRecords(TableName, ConditionExpress, true);
        }

        public bool DeleteUserChannalRadioInfo(UserChannalRadioInfo radio)
        {
            if (radio.Date == null || radio.StartTime == null || radio.EndTime == null)
            {
                return false;
            }
            string ConditionExpress =UserChannalRadio.UserNumber[0]
                + "=" + "'" + radio.UserNumber + "'"
                + " and " + UserChannalRadio.ProgramId[0]
                + "=" + radio.ProgramId
                   + " and " + UserChannalRadio.Date[0]
              + "=" + "'" + radio.Date + "'"
               + " and " + UserChannalRadio.StartTime[0]
              + "=" + "'" + radio.StartTime + "'"
               + " and " +  UserChannalRadio.EndTime[0]
              + "=" + "'" + radio.EndTime + "'"; ;
            return SQLiteConnect.TableHandle.DeleteRecords(TableName, ConditionExpress, true);
        }

        public bool UpdateUserChannalRadioInfo(UserChannalRadioInfo radio)
        {
            string ConditionExpress = UserChannalRadio.UserNumber[0]
               + "=" + "'" + radio.UserNumber + "'"
               + " and " + UserChannalRadio.ProgramId[0]
               + "=" + radio.ProgramId
               + " and " +  UserChannalRadio.Date[0]
              + "=" + "'" + radio.Date + "'"
               + " and " +UserChannalRadio.StartTime[0]
              + "=" + "'" + radio.StartTime + "'"
               + " and " + UserChannalRadio.EndTime[0]
              + "=" + "'" + radio.EndTime + "'";
            return SQLiteConnect.TableHandle.UpdateRecord(TableName, ConditionExpress, radio.GetStore(radio));
        }


        public List<UserChannalRadioInfo> SelectUserChannalRadioInfo()
        {
            return SelectUserChannalRadioInfo("");
        }

        public List<UserChannalRadioInfo> SelectUserChannalRadioInfo(UserChannalCardInfo card)
        {
            string ConditionExpress =  UserChannalRadio.UserNumber[0]
              + "=" + "'" + card.UserNumber + "'"
              + " and " +  UserChannalRadio.ChannalId[0]
              + "=" + card.ChannalId;
            return SelectUserChannalRadioInfo(ConditionExpress);
        }
        public List<UserChannalRadioInfo> SelectUserChannalRadioInfo(string ConditionExpress)
        {
            List<List<object>> radios = null;
            List<UserChannalRadioInfo> infos = new List<UserChannalRadioInfo>();
            SQLiteConnect.TableHandle.SelectRecords(TableName, SelectedCol, ConditionExpress, ref radios);
            if (radios == null)
            {
                return null;
            }
            foreach (List<object> radio in radios)
            {
                infos.Add(new UserChannalRadioInfo(radio));
            }
            return infos;
        }

        public UserChannalRadioInfo SelectUserChannalRadioInfo(UserChannalRadioInfo radio)
        {
            if (radio.Date == null || radio.StartTime == null || radio.EndTime == null)
            {
                return null;
            }
            string ConditionExpress =  UserChannalRadio.UserNumber[0]
              + "=" + "'" + radio.UserNumber + "'"
              + " and " + UserChannalRadio.ProgramId[0]
              + "=" + radio.ProgramId
              +" and "+UserChannalRadio.Date[0]
              +"=" + "'" + radio.Date + "'"
               + " and " + UserChannalRadio.StartTime[0]
              + "=" + "'" +  radio.StartTime + "'"
               + " and " +  UserChannalRadio.EndTime[0]
              + "=" + "'" +  radio.EndTime + "'";
            List<UserChannalRadioInfo> infos = SelectUserChannalRadioInfo(ConditionExpress);
            return infos.Count > 0 ? infos[0] : null;
        }
        public List<UserChannalRadioInfo> SelectUserChannalRadioInfo(string userNumber,int ChannalId,int type)
        {
            string ConditionExpress = UserChannalRadio.UserNumber[0]
             + "=" + "'" + userNumber + "'"
             + " and " +  UserChannalRadio.ChannalId[0]
             + "=" + ChannalId
               + " and " + UserChannalRadio.State[0]
             + "=" + type;
            return SelectUserChannalRadioInfo(ConditionExpress);
        }

    }
}

   

