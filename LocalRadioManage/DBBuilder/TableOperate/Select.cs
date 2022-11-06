using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using LocalRadioManage;

namespace LocalRadioManage.DBBuilder.TableOperate
{
    class Select
    {
       
        public static string GetSelectQuery(string table_name, List<string> selected_col,string expression)
        {
            if (selected_col == null||selected_col.Count==0)
            {
                return null;
            }
            string sql_str = "select " + selected_col[0];
            
            for(int i = 1; i < selected_col.Count; i++)
            {
                sql_str += "," + selected_col[i];
            }
            sql_str += " " + "from " + table_name;
            if (expression != "")
            {
                sql_str += " where " + expression;
            }  
           
            return sql_str;
        }

        public static bool SelectData(string sql,List<string> selected_col,ref List<List<object>> data)
        {
            if (sql == null)
            {
                return false;
            }
            SQLiteConnect.Connect();
            SQLiteCommand cmd = new SQLiteCommand(sql,SQLiteConnect.db_connect);
            
            cmd.CommandText = sql;
            cmd.Prepare();
            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader == null)
            {
                SQLiteConnect.Disconnect();
                return false;
            }

            try
            {
                while (reader.Read())
                {
                    List<object> record = new List<object>();
                    for (int i = 0; i < selected_col.Count; i++)
                    {
                        
                        record.Add(reader[selected_col[i]]);
                    }
                    data.Add(record);
                }
            }
            catch
            {
                data = null;
                reader.Close();
                SQLiteConnect.Disconnect();
                return false;
            }
            SQLiteConnect.Disconnect();
            return true;
        }
    }
}
