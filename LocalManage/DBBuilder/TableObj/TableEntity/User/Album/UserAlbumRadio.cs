using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.DBBuilder.TableOperate;
using LocalRadioManage.DBBuilder.TableObj;

namespace PureRadio.LocalManage.DBBuilder.TableObj
{
    class UserAlbumRadio
    {
        public readonly static string TableName = "UserAlbumRadio";
        public readonly static string[] UserNumber = { "UserNumber", "TEXT NOT NULL" };
        public readonly static string[] ProgramId = { "ProgramId", "INTEGER" };
        public readonly static string[] AlbumId = {"AlbumId","INTEGER" };

        //0->收藏，1->下载，2->既收藏又下载
        public readonly static string[] State = { "CardState", "INTEGER" };

        public readonly static string[] PrimaryKey = { UserNumber[0], ProgramId[0] };
        public readonly static string[] ForeignKey_1 = { ProgramId[0], AlbumRadio.TableName, AlbumRadio.ProgramId[0], TableInform.FOREIGN_CASCADE };
        public readonly static string[] ForeignKey_2 = { UserNumber[0], UserInforms.TableName, UserInforms.UserNumber[0], TableInform.FOREIGN_CASCADE };
        public readonly static string[] ForeignKey_3 = {  UserNumber[0],AlbumId[0], UserAlbumCard.TableName, UserAlbumCard.UserNumber[0], UserAlbumCard.AlbumId[0], TableInform.FOREIGN_CASCADE };
        public readonly static List<string[]> ForeignKey_List = new List<string[]> { ForeignKey_1, ForeignKey_2 , ForeignKey_3 };

        public readonly static Dictionary<string[], int> ColLocation = new Dictionary<string[], int>
        {
            {UserNumber,0 },
            {ProgramId,1 },
            {State,2 },
            {AlbumId,3 }
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
