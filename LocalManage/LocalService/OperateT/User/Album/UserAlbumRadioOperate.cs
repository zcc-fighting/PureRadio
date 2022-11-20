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
    class UserAlbumRadioOperate
    {
        public readonly string TableName = UserAlbumRadio.TableName;
        private List<string> SelectedCol = new List<string>();

        public UserAlbumRadioOperate()
        {
            SelectedCol = SQLiteConnect.TableHandle.GetColNames(TableName).ToList();
        }

        public bool SaveUserAlbumRadioInfo(UserAlbumRadioInfo radio)
        {
            UserAlbumRadioInfo temp = SelectUserAlbumRadioInfo(radio);
            if (temp != null)
            {
                if (temp.State != radio.State)
                {
                    temp.State = 2;
                }
                return UpdateUserAlbumRadioInfo(temp);
            }
            return SQLiteConnect.TableHandle.AddRecord(TableName,radio.GetStore(radio));
        }
        public bool DeleteUserAlbumRadioInfo(UserAlbumCardInfo card)
        {
            string ConditionExpress = UserAlbumRadio.TableName + "." + UserAlbumRadio.UserNumber[0]
                + "=" + "'" + card.UserNumber + "'"
                + " and " + UserAlbumRadio.TableName + "." + UserAlbumRadio.AlbumId[0]
                + "=" + card.AlbumId;
            return SQLiteConnect.TableHandle.DeleteRecords(TableName, ConditionExpress, true);
        }

        public bool DeleteUserAlbumRadioInfo(UserAlbumRadioInfo radio)
        {
            string ConditionExpress = UserAlbumRadio.TableName + "." + UserAlbumRadio.UserNumber[0]
                + "=" + "'" + radio.UserNumber + "'"
                + " and " + UserAlbumRadio.TableName + "." + UserAlbumRadio.ProgramId[0]
                + "=" + radio.ProgramId;
            return SQLiteConnect.TableHandle.DeleteRecords(TableName, ConditionExpress, true);
        }

        public bool UpdateUserAlbumRadioInfo(UserAlbumRadioInfo radio)
        {
            string ConditionExpress = UserAlbumRadio.TableName + "." + UserAlbumRadio.UserNumber[0]
               + "=" + "'" + radio.UserNumber + "'"
               + " and " + UserAlbumRadio.TableName+ "." + UserAlbumRadio.ProgramId[0]
               + "=" + radio.ProgramId;
            return SQLiteConnect.TableHandle.UpdateRecord(TableName, ConditionExpress,radio.GetStore(radio));
        }


        public List<UserAlbumRadioInfo> SelectUserAlbumRadioInfo()
        {
            return SelectUserAlbumRadioInfo("");
        }

        public List<UserAlbumRadioInfo> SelectUserAlbumRadioInfo(UserAlbumCardInfo card)
        {
            string ConditionExpress = UserAlbumRadio.TableName + "." + UserAlbumRadio.UserNumber[0]
              + "=" + "'" + card.UserNumber + "'"
              + " and " + UserAlbumRadio.TableName + "." + UserAlbumRadio.AlbumId[0]
              + "=" + card.AlbumId;
            return SelectUserAlbumRadioInfo(ConditionExpress);
        }

        public List<UserAlbumRadioInfo> SelectUserAlbumRadioInfo(string userNumber,int AlbumId,int type)
        {
            string ConditionExpress = UserAlbumRadio.TableName + "." + UserAlbumRadio.State[0]
              + "=" + type
              + " and " +UserAlbumRadio.TableName + "." + UserAlbumRadio.UserNumber[0]
              +"=" + "'" + userNumber + "'"
              + " and " + UserAlbumRadio.TableName + "." + UserAlbumRadio.AlbumId[0]
              +"="+AlbumId;
            return SelectUserAlbumRadioInfo(ConditionExpress);
        }

        public List<UserAlbumRadioInfo> SelectUserAlbumRadioInfo(string ConditionExpress)
        {
            List<List<object>> radios = null;
            List<UserAlbumRadioInfo> infos = new List<UserAlbumRadioInfo>();
            SQLiteConnect.TableHandle.SelectRecords(TableName, SelectedCol, ConditionExpress, ref radios);
            if (radios == null)
            {
                return null;
            }
            foreach (List<object> radio in radios)
            {
                infos.Add(new UserAlbumRadioInfo(radio));
            }
            return infos;
        }

        public UserAlbumRadioInfo SelectUserAlbumRadioInfo(UserAlbumRadioInfo radio)
        {
            string ConditionExpress = UserAlbumRadio.UserNumber[0]
              + "=" + "'" + radio.UserNumber + "'"
              + " and " +  UserAlbumRadio.ProgramId[0]
              + "=" + radio.ProgramId;
            List<UserAlbumRadioInfo> infos = SelectUserAlbumRadioInfo(ConditionExpress);
            return infos.Count > 0 ? infos[0] : null;
        }


    }


  
}
