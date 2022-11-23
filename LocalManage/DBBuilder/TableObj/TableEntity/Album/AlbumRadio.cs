using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.DBBuilder.TableOperate;
using LocalRadioManage.DBBuilder.TableObj;

namespace PureRadio.LocalManage.DBBuilder.TableObj
{
    class AlbumRadio
    {
        //专辑详情页记录
        public readonly static string TableName = "AlbumRadio";
        public readonly static string[] AlbumId = { "AlbumId", "INTEGER" };
        public readonly static string[] ProgramId = { "ProgramId", "INTEGER" };
        public readonly static string[] Title = { "Titile", "TEXT" };
        public readonly static string[] UpdateTime = { "UpdateTime", "TEXT" };
        public readonly static string[] ContentType = { "ContentType", "TEXT" };
        public readonly static string[] Version_ = { "Version", "TEXT" };
        public readonly static string[] LocalUri = { "LocalUri", "TEXT" };
        public readonly static string[] RemoteUri = { "RemoteUri", "TEXT NOT NULL" };
        public readonly static string[] Duration = { "Duration", "INTEGER" };


        public readonly static string[] PrimaryKey = { ProgramId[0] };
        public readonly static string[] ForeignKey_1 = { AlbumId[0], AlbumCard.TableName, AlbumCard.AlbumId[0], TableInform.FOREIGN_CASCADE };
        public readonly static List<string[]> ForeignKey_List = new List<string[]> { ForeignKey_1 };

        public readonly static Dictionary<string[], int> ColLocation = new Dictionary<string[], int>
        {
            {AlbumId,0 },
            {Title,1 },
            {ProgramId,2 },
            {UpdateTime,3 },
            {ContentType,4 },
            {Version_,5 },
            {LocalUri,6 },
            {RemoteUri,7 },
            {Duration,8 },
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
