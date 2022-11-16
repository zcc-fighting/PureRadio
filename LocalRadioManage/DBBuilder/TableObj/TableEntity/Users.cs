using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.DBBuilder.TableOperate;

namespace LocalRadioManage.DBBuilder.TableObj
{
    class Users
    {
        /// <summary>
        /// 在该客户端上的用户信息的记录
        /// </summary>
        public readonly static string TableName = "Users";
        public readonly static string[] UserName = { "UserName", "TEXT NOT NULL" };
        public readonly static string[] UserPass = { "UserPass", "TEXT NOT NULL" };
        public readonly static string[] UserIcon = { "UserIcon", "TEXT " };
        public readonly static string[] UserTrueName = { "UserIcon", "TEXT " };

        public readonly static string[] PrimaryKey = { UserName[0] };
        public readonly static List<string[]> ForeignKey_List = new List<string[]> {  };

        public readonly static Dictionary<string[], int> ColLocation = new Dictionary<string[], int>
        {
            { UserName,0 },
            { UserPass,1 },
            { UserIcon,2 },
            { UserTrueName,3 }
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
