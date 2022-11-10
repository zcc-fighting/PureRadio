using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.DBBuilder.TableOperate;

namespace LocalRadioManage.DBBuilder.TableObj
{
    class UserDownRadio
    {
        /// <summary>
        /// 用户下载音频表
        /// 特定用户所下载的本地音频的映射
        /// </summary>
        public readonly static string TableName = "UserDownRadio";
        public readonly static string[] UserName = { "UserName", "TEXT NOT NULL" };
        public readonly static string[] RadioId = { "RadioId", "INTEGER" };
        public readonly static string[] RadioDate = { "RadioDate", "INTEGER" };
        public readonly static string[] ChannalAlbumId = { "ChannalAlbumId", "INTEGER" };

        public readonly static string[] PrimaryKey = { UserName[0], RadioId[0],RadioDate[0] };
        public readonly static string[] ForeignKey_0 = { UserName[0], Users.TableName, Users.UserName[0], TableInform.FOREIGN_CASCADE };
        public readonly static string[] ForeignKey_1 = { RadioId[0],RadioDate[0], LocalRadio.TableName, LocalRadio.RadioId[0],LocalRadio.RadioDate[0], TableInform.FOREIGN_CASCADE };
        public readonly static string[] ForeignKey_2 = {UserName[0],ChannalAlbumId[0],UserDownChannalAlbum.TableName, UserDownChannalAlbum.UserName[0],UserDownChannalAlbum.ChannalAlbumId[0], TableInform.FOREIGN_CASCADE };
        public readonly static List<string[]> ForeignKey_List = new List<string[]> { ForeignKey_0, ForeignKey_1,ForeignKey_2 };
        

        public readonly static Dictionary<string[], int> ColLocation = new Dictionary<string[], int>
        {
            {UserName,0 },
            {RadioId,1},
            {RadioDate,2 },
            {ChannalAlbumId,3}
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
