using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.DBBuilder.TableOperate;
using LocalRadioManage.DBBuilder.TableObj;

namespace PureRadio.LocalRadioManage.DBBuilder.TableObj
{
    class ChannalRadio
    {

        //电台详情页记录
        public readonly static string TableName = "ChannalRadio";
        public readonly static string[] ProgramId = { "ProgramId", "INTEGER" };
        public readonly static string[] Date = { "Date", "TEXT NOT NULL" };
        public readonly static string[] StartTime = { "StartTime", "TEXT NOT NULL" };
        public readonly static string[] EndTime = { "EndTime", "TEXT NOT NULL" };
        public readonly static string[] ChannalId = { "ChannalId", "INTEGER" };
        public readonly static string[] Title = { "Titile", "TEXT" };
        public readonly static string[] Broadcasters = { "Broadcasters", "TEXT" };
        public readonly static string[] LocalUri = { "LocalUri", "TEXT" };
        public readonly static string[] RemoteUri = { "RemoteUri", "TEXT NOT NULL" };

        public readonly static string[] PrimaryKey = { ProgramId[0], Date[0], StartTime[0], EndTime[0] };

        public readonly static string[] ForeignKey_1 = { ChannalId[0], ChannalCard.TableName, ChannalCard.ChannalId[0], TableInform.FOREIGN_CASCADE };
        public readonly static List<string[]> ForeignKey_List = new List<string[]> { ForeignKey_1 };

        public readonly static Dictionary<string[], int> ColLocation = new Dictionary<string[], int>
        {
            {ChannalId,0 },
            {Title,1 },
            {Date,2 },
            {StartTime,3 },
            {EndTime,4 },
            {ProgramId,5 },
            {Broadcasters,6},
            {LocalUri,7 },
            {RemoteUri,8 },
        };

        public TableInform GetTableInform()
        {
            List<string> col_names = new List<string>();
            List<string> col_types = new List<string>();
            List<string> tab_constrain = new List<string>();
            foreach (KeyValuePair<string[], int> pair in ColLocation)
            {
                col_names.Add(pair.Key[0]);
                col_types.Add(pair.Key[1]);
            }
            tab_constrain = TableInform.CreateConstraint(PrimaryKey, ForeignKey_List);
            TableInform data = TableChange.GetTableInform(TableName, col_names.ToArray(), col_types.ToArray(), tab_constrain.ToArray());
            return data;
        }
    }
}
