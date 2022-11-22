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
    public class ChannalRadioOperate
    {
        public readonly string TableName = ChannalRadio.TableName;
       
        private List<string> SelectedCol = new List<string>();

        public ChannalRadioOperate()
        {
            SQLiteConnect.CreateLocalRadioManage();
            SelectedCol = SQLiteConnect.TableHandle.GetColNames(TableName).ToList();
        }


        /// <summary>
        /// 增删查改电台内音频
        /// </summary>
        /// <param name="radio"></param>
        /// <returns></returns>
        public bool SaveChannalRadioInfo(ChannalRadioInfo radio)
        {
            List<object> store = ChannalRadioTransform.InfoToStore(radio);
            return SQLiteConnect.TableHandle.AddRecord(TableName, store);
        }
        public bool DeleteChannalRadioInfo(ChannalRadioInfo radio)
        {
            string ConditionExpress = ChannalRadio.ProgramId[0]
                             + "=" + radio.ProgramId
                              + " and " +  ChannalRadio.Date[0]
               + "=" + radio.Date.ToString("yyyyMMdd", null)
                + " and " +  ChannalRadio.StartTime[0]
               + "=" + "'" + radio.StartTime+ "'"
                + " and " + ChannalRadio.EndTime[0]
               + "=" + "'" + radio.EndTime + "'"; ;
            return SQLiteConnect.TableHandle.DeleteRecords(TableName, ConditionExpress, true);
        }
        public bool DeleteChannalRadioInfo(ChannalCardInfo card)
        {
            string ConditionExpress =   ChannalRadio.ChannalId[0]
                + "=" + card.RadioId;
            return SQLiteConnect.TableHandle.DeleteRecords(TableName, ConditionExpress, true);
        }
        public bool UpdateChannalRadioInfo(ChannalRadioInfo radio)
        {
            List<object> store = ChannalRadioTransform.InfoToStore(radio);
            string ConditionExpress = ChannalRadio.ProgramId[0]
                                + "=" + radio.ProgramId + " and " +  ChannalRadio.Date[0]
                                + "=" + radio.Date.ToString("yyyyMMdd", null)
                                  + " and " +ChannalRadio.StartTime[0]
                                + "=" + "'" + radio.StartTime+ "'"
                               + " and " + ChannalRadio.EndTime[0]
                                + "=" + "'" + radio.EndTime + "'";
            return SQLiteConnect.TableHandle.UpdateRecord(TableName, ConditionExpress, store);
        }

        public List<ChannalRadioInfo> SelectChannalRadioInfo()
        {
            return SelectChannalRadioInfo("");
        }
        public ChannalRadioInfo SelectChannalRadioInfo(ChannalRadioInfo radio)
        {
            string ConditionExpress =ChannalRadio.ProgramId[0]
               + "=" + radio.ProgramId
               +" and " +  ChannalRadio.Date[0]
               + "=" + radio.Date.ToString("yyyyMMdd",null)
                + " and " + ChannalRadio.StartTime[0]
               + "=" +"'"+ radio.StartTime+"'"
                + " and " +  ChannalRadio.EndTime[0]
               + "=" +"'"+ radio.EndTime+"'";
            List<ChannalRadioInfo> infos=SelectChannalRadioInfo(ConditionExpress);
            return infos.Count > 0 ? infos[0] : null;
        }

        public List<ChannalRadioInfo> SelectChannalRadioInfo(ChannalCardInfo card)
        {
            string ConditionExpress =ChannalRadio.ChannalId[0]
               + "=" + card.RadioId;
            return SelectChannalRadioInfo(ConditionExpress);
        }
        public List<ChannalRadioInfo> SelectChannalRadioInfo(string ConditionExpress)
        {
            List<List<object>> radios = null;
            List<ChannalRadioInfo> infos = new List<ChannalRadioInfo>();
            SQLiteConnect.TableHandle.SelectRecords(TableName, SelectedCol, ConditionExpress, ref radios);
            if (radios == null)
            {
                return null;
            }
            foreach (List<object> radio in radios)
            {
                infos.Add(ChannalRadioTransform.StoreToInfo(radio));
            }
            return infos;
        }
    }
}
