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
                sql_str += "PRAGMA recursive_triggers = true;";
            }
            else
            {
                sql_str += "PRAGMA foreign_keys=OFF;";
                sql_str += "PRAGMA recursive_triggers = false;";
            }
            sql_str += "delete from " + table_name + " " +
                          "where " + condition_express+";";
            return sql_str;
        }

        //单表的连接删除(不支持多表删除)
        public static string GetDeleteQuery(string need_delete,List<string> table_name, string condition_express, bool is_constrant)
        {

            if (table_name.Count == 0)
            {
                return "";
            }
            string sql_str="";
            if (is_constrant)
            {
                sql_str += "PRAGMA foreign_keys=ON;";
                sql_str += "PRAGMA recursive_triggers = true;";
            }
            else
            {
                sql_str += "PRAGMA foreign_keys=OFF;";
                sql_str += "PRAGMA recursive_triggers = false;";
            }
           
            sql_str += "delete from " + need_delete;
           
           
            List<string> selected_col =new List<string>{ "*"};
            Dictionary<string,List<string>> col_tables = new Dictionary<string, List<string>>();
            foreach(string str in table_name)
            {
                if (str == need_delete)
                {
                 
                }
                else
                {
                    col_tables.Add(str, selected_col);
                }
            }
            string select_str = Select.GetSelectQuery(col_tables, condition_express);

            sql_str += " where exists(" + select_str + ");";
            return sql_str;
        }

        public static bool DeleteDatas(string sql)
        {

            SQLiteConnection get_db_connect= SQLiteConnect.Connect();
            if (get_db_connect == null)
            {
                SQLiteConnect.Disconnect();
                return false;
            }
            SQLiteCommand cmd = new SQLiteCommand(get_db_connect);
            cmd.CommandText = sql;
            cmd.Prepare();
            int result = 0;
            try
            {
                result = cmd.ExecuteNonQuery();
                SQLiteConnect.Disconnect();
            }
            catch
            {
                SQLiteConnect.Disconnect();
                return false;
            }
           
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
