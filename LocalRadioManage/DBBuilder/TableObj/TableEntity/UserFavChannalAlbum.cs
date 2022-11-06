using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.DBBuilder.TableOperate;

namespace LocalRadioManage.DBBuilder.TableObj
{
    class UserFavChannalAlbum
    {
        /// <summary>
        /// 用户收藏节目表
        /// 特定用户所收藏的远端节目Id
        /// </summary>
        public readonly static string TableName = "UserFavChannalAlbum";
        public readonly static string[] UserName = { "UserName", "TEXT NOT NULL" };
        public readonly static string[] ChannalAlbumId = { "ChannalAlbumId", "INTEGER" };
        public readonly static string[] ChannalAlbumType = { "ChannalAlbumType", "INTEGER" };
        public readonly static string[] ChannalAlbumName = { "ChannalAlbumName", "TEXT NOT NULL" };
        public readonly static string[] ChannalAlbumDesc = { "ChannalAlbumDesc", "TEXT NOT NULL" };
        public readonly static string[] ChannalAlbumCover = { "ChannalAlbumCover", "TEXT NOT NULL" };


      
        public readonly static string[] PrimaryKey = { UserName[0], ChannalAlbumId[0] };
        public readonly static string[] ForeignKey = { UserName[0], Users.TableName,Users.UserName[0] };
        public readonly static List<string[]> ForeignKey_List = new List<string[]> { ForeignKey };
      

        public readonly static Dictionary<string[], int> ColLocation = new Dictionary<string[], int>
        {
            { UserName,0 },
            { ChannalAlbumId,1},
            {ChannalAlbumType,2 },
            { ChannalAlbumName,3},
            { ChannalAlbumDesc,4 },
            { ChannalAlbumCover,5},
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
            TableInform data = TableChange.getTableInform(TableName, col_names.ToArray(), col_types.ToArray(), tab_constrain.ToArray());
            return data;
        }


    }
}
