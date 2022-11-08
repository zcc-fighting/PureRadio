using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.DBBuilder.TableOperate;

namespace LocalRadioManage.DBBuilder.TableObj
{
    class UserDownChannalAlbum
    {
        /// <summary>
        /// 用户下载节目表
        /// 特定用户所下载的本地节目的映射
        /// </summary>
       

        public readonly static string TableName = "UserDownChannalAlbum";
        public readonly static string[] UserName = { "UserName", "TEXT NOT NULL" };
        public readonly static string[] ChannalAlbumId = { "ChannalAlbumId", "INTEGER" };

        public readonly static string[] PrimaryKey = { UserName[0], ChannalAlbumId[0] };
        public readonly static string[] ForeignKey_0 = { UserName[0],Users.TableName, Users.UserName[0] };
        public readonly static string[] ForeignKey_1 = {ChannalAlbumId[0],LocalChannalAlbum.TableName, LocalChannalAlbum.ChannalAlbumId[0]};
        public readonly static List<string[]> ForeignKey_List = new List<string[]> { ForeignKey_0,ForeignKey_1};
       

        public readonly static Dictionary<string[], int> ColLocation = new Dictionary<string[], int>
        {
            { UserName,0 },
            { ChannalAlbumId,1},
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
