using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using LocalRadioManage;

namespace LocalRadioManage.DBBuilder.TableOperate
{
   public static class Update
    {
        //public static string getUpdateQuery(string table_name, string[] col_name,int prikey)
        //{
        //    string str_sql = "update " + table_name + " set " + col_name[0] + "=@" + col_name[0];
        //    for(int i = 1; i < col_name.Length; i++)
        //    {
        //        str_sql+=","+col_name[i]+ "=@" + col_name[i];
        //    }
        //    str_sql += " where " + col_name[prikey] + "=@" + col_name[prikey];

        //    return str_sql;
        //}

        public static string GetUpdateQuery(string table_name, string[] col_name, string condition_express)
        {
            string str_sql = "update " + table_name + " set " + col_name[0] + "=@" + col_name[0];
            for (int i = 1; i < col_name.Length; i++)
            {
                str_sql += "," + col_name[i] + "=@" + col_name[i];
            }
            if (condition_express != "")
            {
                str_sql += " where " + condition_express;
            }
            return str_sql;
        }

        public static bool UpdateData(string sql, SQLiteParameter[] parameters)
        {
            SQLiteConnect.Connect();
            SQLiteCommand cmd = new SQLiteCommand(SQLiteConnect.db_connect);
            cmd.CommandText = sql;
            cmd.Prepare();
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
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
