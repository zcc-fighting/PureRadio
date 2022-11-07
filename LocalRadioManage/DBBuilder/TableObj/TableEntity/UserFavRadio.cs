using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.DBBuilder.TableOperate;

namespace LocalRadioManage.DBBuilder.TableObj
{
    class UserFavRadio
    {
        /// <summary>
        /// 用户收藏音频表
        /// 特定用户所收藏的远端音频Id
        /// </summary>
        public readonly static string TableName = "UserFavRadio";
        public readonly static string[] UserName = { "UserName", "TEXT NOT NULL" };
        public readonly static string[] RadioId = { "RadioId", "INTEGER" };
        public readonly static string[] RadioDate = { "RadioDate", "INTEGER" };
        public readonly static string[] ChannalAlbumId = { "ChannalAlbumId", "INTEGER" };
        public readonly static string[] RadioName = { "RadioName", "TEXT NOT NULL" };
        public readonly static string[] RadioDuration = { "RadioDuration", "INTEGER" };
        public readonly static string[] RadioCreateTime = { "RadioCreateTime", "TEXT NOT NULL" };
        public readonly static string[] RadioRemotePath = { "RadioRemotePath ", "TEXT NOT NULL" };

        public readonly static string[] PrimaryKey = { UserName[0], RadioId[0], RadioDate[0] };
        public readonly static string[] ForeignKey_0 = { UserName[0], Users.TableName, Users.UserName[0] };
        public readonly static string[] ForeignKey_1 = { ChannalAlbumId[0], UserFavChannalAlbum.TableName, UserFavChannalAlbum.ChannalAlbumId[0] };
        public readonly static List<string[]> ForeignKey_List = new List<string[]> { ForeignKey_0, ForeignKey_1 };


        public readonly static Dictionary<string[], int> ColLocation = new Dictionary<string[], int>
        {
            { UserName,0 },
            { RadioId,1 },
            { RadioDate,2 },
            { ChannalAlbumId,3},
            { RadioName,4 },
            { RadioDuration,5},
            { RadioCreateTime,6},
            {RadioRemotePath,7 }
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
