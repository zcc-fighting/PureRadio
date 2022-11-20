using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using LocalRadioManage.DBBuilder.TableOperate;

namespace LocalRadioManage.DBBuilder.TableObj
{
    public class LocalRadio
    {
        /// <summary>
        /// 本地音频文件表
        /// 记录音频基本信息与本地音频路径
        /// 其外键为节目表，在文件形式上即音频放在节目表文件夹中
        /// </summary>
        public readonly static string TableName = "LocalRadio";
        public readonly static string[] RadioId = { "RadioId", "INTEGER" };
        public readonly static string[] RadioDate = { "RadioDate", "INTEGER" };
        public readonly static string[] ChannalAlbumId = { "ChannalAlbumId", "INTEGER" };
        public readonly static string[] RadioName = { "RadioName", "TEXT NOT NULL" };
        public readonly static string[] RadioDuration = { "RadioDuration", "INTEGER" };
        public readonly static string[] RadioCreateTime = { "RadioCreateTime", "TEXT NOT NULL" };
        public readonly static string[] RadioLocalPath = { "RadioLocalPath", "TEXT NOT NULL" };
        public readonly static string[] Procasters = { "Procasters", "TEXT NOT NULL" };
        public readonly static string[] RadioRemotePath = { "RadioRemotePath", "TEXT NOT NULL" };

        public readonly static string[] PrimaryKey = { RadioId[0], RadioDate[0] };
        public readonly static string[] ForeignKey = { ChannalAlbumId[0], LocalChannalAlbum.TableName, LocalChannalAlbum.ChannalAlbumId[0],TableInform.FOREIGN_CASCADE };
        public readonly static List<string[]> ForeignKey_List = new List<string[]> { ForeignKey };




        public readonly static Dictionary<string[], int> ColLocation = new Dictionary<string[], int>
        {
            { RadioId,0 },
            { RadioDate,1 },
            { ChannalAlbumId,2},
            { RadioName,3 },
            { RadioDuration,4},
            { RadioCreateTime,5 },
            { RadioLocalPath,6 },
            { Procasters,7 },
            { RadioRemotePath,8 },

            
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
