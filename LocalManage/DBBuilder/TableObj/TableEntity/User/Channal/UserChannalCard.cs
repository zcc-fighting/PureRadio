using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.DBBuilder.TableOperate;
using LocalRadioManage.DBBuilder.TableObj;

namespace PureRadio.LocalRadioManage.DBBuilder.TableObj
{
    class UserChannalCard
    {
        public readonly static string TableName = "UserChannalCard";
        public readonly static string[] UserNumber = { "UserNumber", "TEXT NOT NULL" };
        public readonly static string[] ChannalId = { "ChannalId", "INTEGER" };


        //0->收藏，1->下载，2->既收藏又下载
        public readonly static string[] State = { "CardState", "INTEGER" };

        public readonly static string[] PrimaryKey = { UserNumber[0], ChannalId[0] };
        public readonly static string[] ForeignKey_1 = { ChannalId[0], ChannalCard.TableName, ChannalCard.ChannalId[0], TableInform.FOREIGN_CASCADE };
        public readonly static string[] ForeignKey_2 = { UserNumber[0], UserInforms.TableName, UserInforms.UserNumber[0], TableInform.FOREIGN_CASCADE };
        public readonly static List<string[]> ForeignKey_List = new List<string[]> { ForeignKey_1, ForeignKey_2 };

        public readonly static Dictionary<string[], int> ColLocation = new Dictionary<string[], int>
        {
            {UserNumber,0 },
            {ChannalId,1 },
            {State,2 },
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
