using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.DBBuilder.TableOperate;



namespace LocalRadioManage.DBBuilder.TableObj
{

    class LocalChannalAlbum
    {
        /// <summary>
        /// 本地节目表
        /// 记录节目Id、Name、CoverPath、LocalPath
        /// Cover存入本地
        /// </summary>
        public readonly static string TableName = "LocalChannalAlbum";
        public readonly static string[] ChannalAlbumId = { "ChannalAlbumId", "INTEGER" };
        public readonly static string[] ChannalAlbumType = { "ChannalAlbumType", "INTEGER" };
        public readonly static string[] ChannalAlbumName = { "ChannalAlbumName", "TEXT NOT NULL" };
        public readonly static string[] ChannalAlbumDesc = { "ChannalAlbumDesc", "TEXT NOT NULL" };
        public readonly static string[] ChannalAlbumCover = { "ChannalAlbumCover", "TEXT NOT NULL" };
        //public readonly static string[] ChannalAlbunLocalPath = { "ChannalAlbunLocalPath", "TEXT NOT NULL"};
        public readonly static string[] PrimaryKey = {ChannalAlbumId[0]};
        public readonly static List<string[]> ForeignKey_List = new List<string[]>();

        public readonly static Dictionary<string[], int> ColLocation = new Dictionary<string[], int>
        {
            { ChannalAlbumId,0 },
            { ChannalAlbumType,1 },
            { ChannalAlbumName,2},
            { ChannalAlbumDesc,3 },
            { ChannalAlbumCover,4},
            //{ ChannalAlbunLocalPath,5 }
        };
     
      
        public TableInform GetTableInform()
        {
            List<string> col_names = new List<string>();
            List<string> col_types = new List<string>();
            List<string> tab_constrain = new List<string>();
            foreach (KeyValuePair<string[],int> pair in ColLocation)
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
