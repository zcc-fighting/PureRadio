using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using LocalRadioManage;
using LocalRadioManage.DBBuilder.TableObj;

namespace LocalRadioManage.DBBuilder.TableOperate
{
   static class TableChange
    {
        public static string GetCreateTabCom(string tab_name,List<string> col_names, List<string> col_types, List<string> tab_constrain)
        {
            string query_str= "CREATE TABLE IF NOT EXISTS " + tab_name + "( " + col_names[0] + " " + col_types[0];
            for (int i = 1; i < col_names.Count; i++)
            {
                query_str += ", " + col_names[i] + " " + col_types[i];
            }
            if (tab_constrain != null)
            {
                for (int i = 0; i < tab_constrain.Count; i++)
                {
                    query_str += ", " + tab_constrain[i] + " ";
                }
            }
            query_str += ")";
            return query_str;
        }
        public static bool CreateTable(string query_str)
        {
            SQLiteConnect.Connect();
            SQLiteCommand cmd = new SQLiteCommand(SQLiteConnect.db_connect);
            cmd.CommandText = query_str;
            cmd.Prepare();
           
            int result = cmd.ExecuteNonQuery();
            SQLiteConnect.Disconnect();
            if (result == 0)
            {
                // Console.WriteLine("yes");
             
                return true;
            }
            else
            {
               // Console.WriteLine("no"+result);
                return false;
            }
        }
        
        public static TableInform getTableInform(string table_name, string[] col_names, string[] col_types, string[] tab_constrain)
        {
            TableInform data = new TableInform();
            data.table_name = table_name;
            //data.prikey = prikey;
            data.col_names = col_names.ToList();
            data.col_types = col_types.ToList();
            if (tab_constrain != null)
            {
                data.constraint = tab_constrain.ToList();
            }
            return data;
        }
    }
}
