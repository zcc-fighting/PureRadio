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

        //public  static string getDeleteQuery(string table_name, string prikey_name,List<int> id_list)
        //  {
        //      string sql_str = "delete from " + table_name + " " +
        //                      "where " + prikey_name + " " + "in (" + id_list[0];
        //      for(int i = 1; i < id_list.Count; i++)
        //      {
        //          sql_str += "," + id_list[i];
        //      }
        //      sql_str += ")";
        //      return sql_str;
        //  }
        public static string GetDeleteQuery(string table_name, string condition_express)
        {
            string sql_str = "delete from " + table_name + " " +
                          "where " + condition_express;
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
