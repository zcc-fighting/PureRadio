using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using LocalRadioManage;


namespace LocalRadioManage.DBBuilder.TableOperate
{
   public static class Insert
    {
        public static string GetInsertQuery(string table_name, string[] col_name)
        {
            string str_sql = "insert into " + table_name + "("+col_name[0];
            for(int i = 1; i < col_name.Length; i++)
            {
                str_sql += ","+col_name[i] ;
            }
            str_sql += ") values("+"@"+col_name[0];
            for (int i = 1; i < col_name.Length; i++)
            {
                str_sql += ","+"@" +col_name[i];
            }
            str_sql += ")";

            return str_sql;
        }
        public static bool InsertData(string sql, SQLiteParameter[] parameters)
        {
            SQLiteConnect.Connect();
            SQLiteCommand cmd = new SQLiteCommand(SQLiteConnect.db_connect);
            cmd.CommandText = sql;
            cmd.Prepare();
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
                cmd.CommandTimeout = 1000;
            }
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
