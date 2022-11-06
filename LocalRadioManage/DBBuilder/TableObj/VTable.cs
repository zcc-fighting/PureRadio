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
    //数据存放
    public partial class VTable
    {
        Dictionary<string, TableInform> table_list = new Dictionary<string, TableInform>();
    }

    //工具类
   public partial class VTable
    {
        static  public SQLiteParameter[] GetParameters(List<string> col_names, List<object> record)
        {
            List<SQLiteParameter> parameters;
            SQLiteParameter para;

            parameters = new List<SQLiteParameter>();
            try
            {
                for (int i = 0; i < col_names.Count; i++)
                {
                    para = new SQLiteParameter("@" + col_names[i], record[i]);
                    parameters.Add(para);
                }
            }
            catch
            {
                return null;
            }
            return parameters.ToArray();

        }
        public void AddTable(string table_name,TableInform data)
        {
            table_list.Add(table_name, data);
        }
        public bool CreateTable(string table_name)
        {
            string sql;
            TableInform data;

            data = table_list[table_name];
            if (data == null)
            {
                return false;
            }
            sql = TableChange.GetCreateTabCom(table_name, data.col_names, data.col_types, data.constraint);
            return TableChange.CreateTable(sql);
        }
        public string[] GetColNames(string table_name)
        {

            TableInform data;
            try
            {
                data = table_list[table_name];
            }
            catch
            {
                return null;
            }
            return data.col_names.ToArray();
        }

        //public string getPrikeyName(string table_name)
        //{
        //    TableInform data;
        //    try
        //    {
        //        data = table_list[table_name];
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //    if (data == null)
        //    {
        //        return null;
        //    }
        //    return data.col_names[data.prikey];
        //}
    }

    //增删改查
    public partial class VTable
    {
        public bool AddRecord(string table_name,List<object> record) 
        {
            string sql;
            TableInform data;
            SQLiteParameter[] parameters;

            data = table_list[table_name];
            if (data == null)
            {
                return false;
            }
            sql = Insert.GetInsertQuery(data.table_name,data.col_names.ToArray());
            parameters = GetParameters(data.col_names,record);
       
            return Insert.InsertData(sql, parameters);
        }
        public bool UpdateRecord(string table_name, string condition_express, List<object> record)
        {
            string sql;
            TableInform data;
            SQLiteParameter[] parameters;

            data = table_list[table_name];
            if (data == null)
            {
                return false;
            }
            sql = Update.GetUpdateQuery(data.table_name, data.col_names.ToArray(), condition_express);
            parameters = GetParameters(data.col_names, record);

            return Update.UpdateData(sql, parameters);
        }
        public bool UpdateRecord(string table_name, string condition_express, List<object> record, List<string> selected_col)
        {
            string sql;
            TableInform data;
            SQLiteParameter[] parameters;
            data = table_list[table_name];
            if (data == null)
            {
                return false;
            }
            sql = Update.GetUpdateQuery(data.table_name, selected_col.ToArray(), condition_express);
            parameters = GetParameters(selected_col, record);

            return Update.UpdateData(sql, parameters);
        }
        public bool DeleteRecords(string table_name, string condition_express)
        {
            string sql;
            TableInform data;
            //SQLiteParameter[] parameters;

            data = table_list[table_name];
            if (data == null || condition_express == "")
            {
                return false;
            }

            sql = Delete.GetDeleteQuery(data.table_name, condition_express);

            return Delete.DeleteDatas(sql);
        }
        public bool SelectRecords(string table_name, List<string> selected_col,string condition_express, ref List<List<object>> data_list_out)
        {
            TableInform data;
            string sql;
            try
            {
                data = table_list[table_name];
            }
            catch
            {
                return false;
            }
            if (data == null)
            {
                return false;
            }
            if (selected_col == null)
            {
                return false;
            }
            sql = Select.GetSelectQuery(data.table_name, selected_col, condition_express);
            return Select.SelectData(sql, selected_col, ref data_list_out);
        }
    }

     

    
}
