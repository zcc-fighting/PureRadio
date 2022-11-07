using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace LocalRadioManage.DBBuilder.TableOperate
{
    public static class Delete
    {

      

        public static string GetDeleteQuery(string table_name, string condition_express,bool is_constrant)
        {
            string sql_str = "";
            if (is_constrant)
            {
                sql_str += "PRAGMA foreign_keys=ON;";
            }
            sql_str += "delete from " + table_name + " " +
                          "where " + condition_express+";";
            return sql_str;
        }

        public static string GetDeleteQuery(List<string> need_delete,List<string> table_name, string condition_express, bool is_constrant)
        {

            if (table_name.Count == 0)
            {
                return "";
            }
            string sql_str="";
            if (is_constrant)
            {
                sql_str += "PRAGMA foreign_keys=ON;";
            }
            if (need_delete.Count == table_name.Count|| need_delete == null||need_delete.Count==0)
            {
                sql_str += "delete from " + table_name[0];
            }
            else
            {
                sql_str = "delete "+need_delete[0];
                for(int i = 1; i < need_delete.Count; i++)
                {
                    sql_str += "," + need_delete[i];
                }
                sql_str+=" from " + table_name[0];
            }
            
             for(int i = 1; i < table_name.Count; i++)
            {
                sql_str += " inner join " + table_name[i];
            }
            sql_str += " where " + condition_express;
            return sql_str;
        }

        public static bool DeleteDatas(string sql)
        {

            SQLiteConnect.Connect();
            SQLiteCommand cmd = new SQLiteCommand(SQLiteConnect.db_connect);
            cmd.CommandText = sql;
            cmd.Prepare();
            int result = 0;
            try
            {
                result = cmd.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            SQLiteConnect.Disconnect();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
