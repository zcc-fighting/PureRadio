using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.DBBuilder.TableOperate;
using LocalRadioManage.DBBuilder.TableObj;

namespace PureRadio.LocalRadioManage.DBBuilder.TableObj
{
    class ChannalCard
    {
        //电台详情页记录
        public readonly static string TableName = "ChannalCard";
        public readonly static string[] ChannalId = { "ChannalId", "INTEGER" };
        public readonly static string[] Title = { "Titile", "TEXT" };
        public readonly static string[] Cover = { "Cover", "TEXT NOT NULL" };
        public readonly static string[] LocalCover = { "LocalCover", "TEXT" };
        public readonly static string[] Description = { "Description", "TEXT" };
        public readonly static string[] CityId = { "CityId", "INTEGER" };
        public readonly static string[] RegionId = { "RegionId", "INTEGER" };
        public readonly static string[] TopCategoryId = { "TopCategoryId", "INTEGER" };
        public readonly static string[] TopCategoryTitle = { "TopCategoryTitle", "TEXT" };
        public readonly static string[] UpdateTime = { "UpdateTime", "TEXT" };


        public readonly static string[] PrimaryKey = { ChannalId[0] };
       
        public readonly static List<string[]> ForeignKey_List = new List<string[]> {  };

        public readonly static Dictionary<string[], int> ColLocation = new Dictionary<string[], int>
        {
            {ChannalId,0 },
            {Title,1 },
            {Cover,2 },
            {LocalCover,3 },
            {Description,4 },
            {CityId,5 },
            {RegionId,6 },
            {TopCategoryId,7 },
            {TopCategoryTitle,8 },
            {UpdateTime,9 },
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
