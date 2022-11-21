using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.LocalManage.DataModelsL;
using PureRadio.LocalManage.DataModelTransform;
using PureRadio.LocalManage.DBBuilder.TableObj;
using LocalRadioManage.DBBuilder;

namespace PureRadio.LocalManage.LocalService.Local
{
    class AlbumCardOperate
    {
        public readonly string TableName = AlbumCard.TableName;
        private List<string> SelectedCol = new List<string>();

        public AlbumCardOperate()
        {
            SQLiteConnect.CreateLocalRadioManage();
            SelectedCol = SQLiteConnect.TableHandle.GetColNames(TableName).ToList();

        }

        /// <summary>
        /// 增删查改专辑详情页
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool SaveAlbumCardInfo(AlbumCardInfo card)
        {
            List<object> store = AlbumCardTransform.InfoToStore(card);
            return SQLiteConnect.TableHandle.AddRecord(TableName, store);
        }

        public bool DeleteAlbumCardInfo(AlbumCardInfo card)
        {
            string ConditionExpress = AlbumCard.AlbumId[0]
                             + "=" + card.ContentId;
            return SQLiteConnect.TableHandle.DeleteRecords(TableName, ConditionExpress, true);
        }

        public bool UpdateAlbumCardInfo(AlbumCardInfo card)
        {
            List<object> store = AlbumCardTransform.InfoToStore(card);
            string ConditionExpress = AlbumCard.AlbumId[0]
                             + "=" + card.ContentId;
            return SQLiteConnect.TableHandle.UpdateRecord(TableName, ConditionExpress, store);
        }

        public List<AlbumCardInfo> SelectAlbumCardInfo()
        {
            return SelectAlbumCardInfo("");
        }

        public AlbumCardInfo SelectAlbumCardInfo(AlbumCardInfo card)
        {
            string ConditionExpress = AlbumCard.AlbumId[0]
                              + "=" + card.ContentId;
            List<AlbumCardInfo> infos=SelectAlbumCardInfo(ConditionExpress);
            return   infos.Count > 0 ? infos[0] : null;
        }
        public List<AlbumCardInfo> SelectAlbumCardInfo(string ConditionExpress)
        {
            List<List<object>> cards = null;
            List<AlbumCardInfo> infos = new List<AlbumCardInfo>();
            SQLiteConnect.TableHandle.SelectRecords(TableName, SelectedCol, ConditionExpress, ref cards);
            if (cards == null)
            {
                return null;
            }
            foreach (List<object> card in cards)
            {
                infos.Add(AlbumCardTransform.StoreToInfo(card));
            }
            return infos;
        }

    }

    
}
