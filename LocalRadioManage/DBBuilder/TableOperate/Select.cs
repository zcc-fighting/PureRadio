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

        //构建多表查询
        public static string GetSelectQuery(Dictionary<string,List<string>> table_cols, string expression)
        {
            if (table_cols == null || table_cols.Count == 0)
            {
                return "";
            }
            string sql_str = "select ";
            int first = 0;
          
            foreach(KeyValuePair<string,List<string>> table_col in table_cols)
            {

                if (table_col.Value != null && table_col.Value.Count != 0)
                {
                    if (first == 0)
                    {
                        if (table_col.Value[0] == "*")
                        {
                            sql_str += table_col.Value[0];
                            break;
                        }
                        sql_str += table_col.Key + "." + table_col.Value[0];
                        first++;
                        for (int i = 1; i < table_col.Value.Count; i++)
                        {
                            sql_str += "," + table_col.Key + "." + table_col.Value[i];
                        }
                    }
                    else
                    {
                        for (int i = 0; i < table_col.Value.Count; i++)
                        {
                            sql_str += "," + table_col.Key + "." + table_col.Value[i];
                        }
                    }
                }
            }
            sql_str += " from ";
            first = 0;
            foreach (KeyValuePair<string, List<string>> table_col in table_cols)
            {
                if (first == 0)
                {
                    first++;
                    sql_str += table_col.Key;
                }
                else
                {
                    sql_str += " inner join " + table_col.Key;
                }
            }
            
            if (expression != "")
            {
                sql_str += " where " + expression;
            }
            return sql_str;
        }


        public static bool SelectData(string sql,List<string> selected_col,ref List<List<object>> data)
        {
            data = new List<List<object>>();
            if (sql == null||sql=="")
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
