using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.Service;
using System.IO;
using System.Net;
using System.Web;
using Windows.Storage;
using LocalRadioManage.StorageOperate;
using LocalRadioManage.DBBuilder;
using LocalRadioManage.DBBuilder.TableObj;


namespace LocalRadioManage.Service.UserInform
{
    public partial class UserDown
    {
        /// <summary>
        /// 下载节目表参数
        /// </summary>
        private string table_name_program = "";
        private List<string> selected_col_program = null;
        private string condition_express_program = "";

        /// <summary>
        /// 下载音频表参数
        /// </summary>
        private string table_name_radio = "";
        private List<string> selected_col_radio = null;
        private string condition_express_radio = "";

        /// <summary>
        /// 获取节目表单信息
        /// </summary>
        private  List<List<object>> user_down_programs_inform = new List<List<object>>();
       
        /// <summary>
        /// 获取音频表单信息
        /// </summary>
        private List<List<object>> user_down_radios_inform = new List<List<object>>();
    }

   public partial class UserDown
    {
     public UserDown()
        {
            table_name_program = UserDownChannalAlbum.TableName;
            selected_col_program = SQLiteConnect.TableHandle.GetColNames(table_name_program).ToList();
            table_name_radio = UserDownRadio.TableName;
            selected_col_radio= SQLiteConnect.TableHandle.GetColNames(table_name_radio).ToList();
        }
     public UserDown(List<object> user_down_program)
        {
            table_name_program = UserDownChannalAlbum.TableName;
            selected_col_program = SQLiteConnect.TableHandle.GetColNames(table_name_program).ToList();
            table_name_radio = UserDownRadio.TableName;
            selected_col_radio = SQLiteConnect.TableHandle.GetColNames(table_name_radio).ToList();

            int location = UserDownChannalAlbum.ColLocation[UserDownChannalAlbum.ChannalAlbumId];
            condition_express_program = UserDownChannalAlbum.UserName[0] + "=" + UserDownChannalAlbum.ColLocation[UserDownChannalAlbum.UserName];
            condition_express_radio = UserDownChannalAlbum.ChannalAlbumId[0] + "=" + user_down_program[location];//获取某一节目表下所有音频的表达式
        }

        public bool SaveUserDownProgram(List<object> user_down_program)
        {
            try
            {
               return  SQLiteConnect.TableHandle.AddRecord(table_name_program, user_down_program);
            }
            catch
            {
                return false;
            }
        }
        public bool SaveUserDownRadio( List<object> user_down)
        {
            try
            {
               return SQLiteConnect.TableHandle.AddRecord(table_name_radio, user_down);
            }
            catch
            {
               return false;
            }
        }
        public bool SaveUserDownRadio(List<List<object>> user_downs)
        {
            try
            {
                foreach(List<object> user_down in user_downs)
                {
                    SaveUserDownRadio(user_down);
                }
                return true;
            }
            catch
            {
                return false; ;
            }
        }

        public List<List<object>> LoadUserDownProgram()
        {
            try
            {
                SQLiteConnect.TableHandle.SelectRecords(table_name_program, selected_col_program, condition_express_program,ref user_down_programs_inform);
                return GetProgramInforms();
            }
            catch
            {
                return null;
            }
        }

        public List<List<object>> LoadUserDownRadio()
        {
            try
            {
                SQLiteConnect.TableHandle.SelectRecords(table_name_radio, selected_col_radio, condition_express_radio, ref user_down_radios_inform);
                return GetRadioInforms();
            }
            catch
            {
                return null;
            }
        }

        public bool DeleteUsrDownProgram()
        {
            try
            {
              return SQLiteConnect.TableHandle.DeleteRecords(table_name_program, condition_express_program);
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteUsrDownRadios()
        {
            try
            {
                return SQLiteConnect.TableHandle.DeleteRecords(table_name_radio, condition_express_radio);
            }
            catch
            {
                return false;
            }

        }

        public void SetConditionExpress_Program(string express)
        {
            condition_express_program = express;
        }
        public void SetConditionExpress_Radio(string express)
        {
            condition_express_radio = express;
        }

        private List<List<object>> GetProgramInforms()
        {
            return user_down_programs_inform;
        }

        private List<List<object>> GetRadioInforms()
        {
            return user_down_radios_inform;
        }

    }
}
