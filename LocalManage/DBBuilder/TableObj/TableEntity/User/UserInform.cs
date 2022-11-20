using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.DBBuilder.TableOperate;
using LocalRadioManage.DBBuilder.TableObj;

namespace PureRadio.LocalRadioManage.DBBuilder.TableObj
{
    class UserInforms
    {

        public readonly static string TableName = "UserInforms";
        public readonly static string[] UserNumber = { "UserNumber", "TEXT NOT NULL" };
        public readonly static string[] UserName = { "UserName", "TEXT " };
        public readonly static string[] UserNickName = { "UserNickName", "TEXT " };
        public readonly static string[] UserPass = { "UserPass", "TEXT" };
        public readonly static string[] UserIcon = { "UserIcon", "TEXT " };
        public readonly static string[] Gender = { "Gender", "TEXT" };
        public readonly static string[] Signature = { "Signature", "TEXT " };
        public readonly static string[] Location = { "Location", "TEXT " };
        public readonly static string[] Birthday = { "Birthday", "TEXT " };
        public readonly static string[] CreateTime = { "CreateTime", "TEXT " };
        public readonly static string[] LocalUserIcon = { "LocalUserIcon", "TEXT " };

        public readonly static string[] PrimaryKey = { UserNumber[0] };
        public readonly static List<string[]> ForeignKey_List = new List<string[]> { };

        public readonly static Dictionary<string[], int> ColLocation = new Dictionary<string[], int>
        {
            {UserNumber,0 },
            {UserName,1 },
            {UserNickName,2 },
            {UserPass,3 },
            {UserIcon,4 },
            {Gender,5 },
            {Signature,6 },
            {Location,7 },
            {Birthday,8 },
            {CreateTime,9 },
            {LocalUserIcon,10 }
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
