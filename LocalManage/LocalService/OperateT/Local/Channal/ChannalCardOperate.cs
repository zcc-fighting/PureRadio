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
    public class ChannalCardOperate
    {
        public readonly string TableName = ChannalCard.TableName;
        private List<string> SelectedCol = new List<string>();

        public ChannalCardOperate()
        {
            SQLiteConnect.CreateLocalRadioManage();
            SelectedCol = SQLiteConnect.TableHandle.GetColNames(TableName).ToList();
        }

        /// <summary>
        /// 增删查改电台详情页
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool SaveChannalCardInfo(ChannalCardInfo card)
        {
            List<object> store = ChannalCardTransform.InfoToStore(card);
            return SQLiteConnect.TableHandle.AddRecord(TableName, store);
        }
        public bool DeleteChannalCardInfo(ChannalCardInfo card)
        {
            string ConditionExpress = ChannalCard.ChannalId[0]
                             + "=" + card.RadioId;
            return SQLiteConnect.TableHandle.DeleteRecords(TableName, ConditionExpress, true);
        }
        public bool UpdateChannalCardInfo(ChannalCardInfo card)
        {
            List<object> store = ChannalCardTransform.InfoToStore(card);
            string ConditionExpress = ChannalCard.ChannalId[0]
                             + "=" + card.RadioId;
            return SQLiteConnect.TableHandle.UpdateRecord(TableName, ConditionExpress, store);
        }
        public List<ChannalCardInfo> SelectChannalCardInfo()
        {
            return SelectChannalCardInfo("");
        }
        public List<ChannalCardInfo> SelectChannalCardInfo(string ConditionExpress)
        {
            List<List<object>> cards = null;
            List<ChannalCardInfo> infos = new List<ChannalCardInfo>();
            SQLiteConnect.TableHandle.SelectRecords(TableName, SelectedCol, ConditionExpress, ref cards);
            if (cards == null)
            {
                return null;
            }
            foreach (List<object> card in cards)
            {
                infos.Add(ChannalCardTransform.StoreToInfo(card));
            }
            return infos;
        }

        public ChannalCardInfo SelectChannalCardInfo(ChannalCardInfo card)
        {
            string ConditionExpress =  ChannalCard.ChannalId[0]
                             + "=" + card.RadioId;
            List<ChannalCardInfo> infos=SelectChannalCardInfo(ConditionExpress);
            return infos.Count > 0 ? infos[0] : null;
        }
    }
}