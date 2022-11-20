using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.DBBuilder.TableOperate;
using LocalRadioManage.DBBuilder.TableObj;

namespace PureRadio.LocalRadioManage.DBBuilder.TableObj
{
    class AlbumCard
    {
      

        //专辑详情页记录
        public readonly static string TableName = "AlbumCard";
        public readonly static string[] AlbumId = { "AlbumId", "INTEGER" };
        public readonly static string[] Title = { "Titile", "TEXT" };
        public readonly static string[] Podcaster = { "Podcaster", "TEXT" };
        public readonly static string[] Cover = {"Cover", "TEXT NOT NULL" };
        public readonly static string[] LocalCover = { "LocalCover", "TEXT" };
        public readonly static string[] Description = {"Description", "TEXT" };
        public readonly static string[] Rating = {"Rating","REAL" };
        public readonly static string[] CategoryId = { "CategoryId", "INTEGER" };
        public readonly static string[] ContentType = { "ContentType", "TEXT" };
        public readonly static string[] Version_ = { "Version", "TEXT" };

        public readonly static string[] PrimaryKey = { AlbumId[0] };
        public readonly static List<string[]> ForeignKey_List = new List<string[]> { };

      

        public readonly static Dictionary<string[], int> ColLocation = new Dictionary<string[], int>
        {
            {AlbumId,0 },
            {Title,1 },
            {Podcaster,2 },
            {Cover,3 },
            {LocalCover,4 },
            {Description,5 },
            {Rating,6 },
            {CategoryId,7 },
            {ContentType,8 },
            {Version_,9 },
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
