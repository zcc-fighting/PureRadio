using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalManage.DataModelsL;
using PureRadio.LocalManage.DBBuilder.TableObj;
using PureRadio.LocalManage.DataModelTransform;
using LocalRadioManage.DBBuilder;

namespace PureRadio.LocalManage.LocalService.Local
{
    class AlbumRadioOperate
    {
        public readonly string TableName = AlbumRadio.TableName;
        private List<string> SelectedCol = new List<string>();

        public AlbumRadioOperate()
        {
            SQLiteConnect.CreateLocalRadioManage();
            SelectedCol = SQLiteConnect.TableHandle.GetColNames(TableName).ToList();
        }

        /// <summary>
        /// 增删查改专辑内音频
        /// </summary>
        /// <param name="radio"></param>
        /// <returns></returns>
        public bool SaveAlbumRadioInfo(AlbumRadioInfo radio)
        {
            List<object> store = AlbumRadioTransform.InfoToStore(radio);
            return SQLiteConnect.TableHandle.AddRecord(TableName, store);
        }
        public bool DeleteAlbumRadioInfo(AlbumRadioInfo radio)
        {
            string ConditionExpress = AlbumRadio.ProgramId[0]
                             + "=" + radio.ProgramId;
            return SQLiteConnect.TableHandle.DeleteRecords(TableName, ConditionExpress, true);
        }
        public bool DeleteAlbumRadioInfo(AlbumCardInfo card)
        {
            string ConditionExpress = AlbumRadio.AlbumId[0]
                + "=" + card.ContentId;
            return SQLiteConnect.TableHandle.DeleteRecords(TableName, ConditionExpress, true);
        }
        public bool UpdateAlbumRadioInfo(AlbumRadioInfo radio)
        {
            List<object> store = AlbumRadioTransform.InfoToStore(radio);
            string ConditionExpress = AlbumRadio.ProgramId[0]
                                + "=" + radio.ProgramId;
            return SQLiteConnect.TableHandle.UpdateRecord(TableName, ConditionExpress, store);
        }

        public List<AlbumRadioInfo> SelectAlbumRadioInfo()
        {
            return SelectAlbumRadioInfo("");
        }

        public List<AlbumRadioInfo> SelectAlbumRadioInfo(AlbumCardInfo card)
        {
            string ConditionExpress = AlbumRadio.AlbumId[0]
               + "=" + card.ContentId;
           return SelectAlbumRadioInfo(ConditionExpress);
        }
        public AlbumRadioInfo SelectAlbumRadioInfo(AlbumRadioInfo radio)
        {
            string ConditionExpress = AlbumRadio.AlbumId[0]
               + "=" + radio.AlbumId
             +" and "+ AlbumRadio.ProgramId[0]
             +"="+radio.ProgramId;
            List<AlbumRadioInfo> infos= SelectAlbumRadioInfo(ConditionExpress);
            return infos.Count > 0 ? infos[0] : null;
        }
        public List<AlbumRadioInfo> SelectAlbumRadioInfo(string ConditionExpress)
        {
            List<List<object>> radios = null;
            List<AlbumRadioInfo> infos = new List<AlbumRadioInfo>();
            SQLiteConnect.TableHandle.SelectRecords(TableName, SelectedCol, ConditionExpress, ref radios);
            if (radios == null)
            {
                return null;
            }
            foreach (List<object> radio in radios)
            {
                infos.Add(AlbumRadioTransform.StoreToInfo(radio));
            }
            return infos;
        }

    }
}
