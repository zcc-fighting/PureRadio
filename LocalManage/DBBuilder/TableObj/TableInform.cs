using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalRadioManage.DBBuilder.TableObj
{
    /// <summary>
    /// 表实体信息转换格式
    /// </summary>
        public class TableInform
        {
            public string table_name = "";
            //public int prikey = 0;
            public List<string> col_names = new List<string>();
            public List<string> col_types = new List<string>();
            public List<string> constraint = new List<string>();


        public static readonly string FOREIGN_CASCADE =" ON DELETE CASCADE";
        //public static readonly string FOREIGN_CASCADE = "";
        public static List<string> CreateConstraint(string[] primary_keys,List<string[]> foreign_keys )
        {
            List<string> constraint = new List<string>();

            if (primary_keys!=null&&primary_keys.Length > 0)
            {
                string primary = "PRIMARY KEY(" + primary_keys[0];
                for(int i=1;i<primary_keys.Length;i++)
                {
                    primary += "," + primary_keys[i];
                }
                primary += ")";
                constraint.Add(primary);
            }

            if (foreign_keys!=null&&foreign_keys.Count > 0)
            {  
                foreach(string[] foregin in foreign_keys)
                {
                   
                    if (foregin.Length > 0)
                    {
                        int refer_count = (foregin.Length-1)/2;
                        string temp = "FOREIGN KEY(" + foregin[0];
                        for(int i = 1; i < refer_count; i++)
                        {
                            temp += "," + foregin[i];
                        }
                        temp += ") REFERENCES "+foregin[refer_count]+"("+foregin[refer_count+1];
                        for (int i = refer_count+2; i < foregin.Length-1; i++)
                        {
                            temp += "," + foregin[i];
                        }
                        temp += ")"+foregin[foregin.Length-1];
                        constraint.Add(temp);
                    }
                }
            }
            return constraint;
            
        }
        }
    
}
