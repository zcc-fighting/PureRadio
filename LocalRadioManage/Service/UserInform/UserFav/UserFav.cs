using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.Service;
using LocalRadioManage.DBBuilder;
using LocalRadioManage.DBBuilder.TableObj;

namespace LocalRadioManage.Service.UserInform
{
    public partial class UserFav
    {
        /// <summary>
        /// 收藏节目表参数
        /// </summary>
        private string table_name_program = "";
        private List<string> selected_col_program = null;
        private string condition_express_program = "";

        /// <summary>
        /// 收藏音频表参数
        /// </summary>
        private string table_name_radio = "";
        private List<string> selected_col_radio = null;
        private string condition_express_radio = "";

        /// <summary>
        /// 获取收藏节目表单信息
        /// </summary>
        private List<List<object>> user_fav_programs_inform = new List<List<object>>();

        /// <summary>
        /// 获取收藏音频表单信息
        /// </summary>
        private List<List<object>> user_fav_radios_inform = new List<List<object>>();
    }

  public partial  class UserFav
    {

        public UserFav()
        {
            table_name_program = UserFavChannalAlbum.TableName;
            selected_col_program = SQLiteConnect.TableHandle.GetColNames(table_name_program).ToList();
            table_name_radio = UserFavRadio.TableName;
            selected_col_radio = SQLiteConnect.TableHandle.GetColNames(table_name_radio).ToList();
        }
        public UserFav(List<object> user_down_program)
        {
            table_name_program = UserFavChannalAlbum.TableName;
            selected_col_program = SQLiteConnect.TableHandle.GetColNames(table_name_program).ToList();
            table_name_radio = UserFavRadio.TableName;
            selected_col_radio = SQLiteConnect.TableHandle.GetColNames(table_name_radio).ToList();

            int location = UserFavChannalAlbum.ColLocation[UserFavChannalAlbum.ChannalAlbumId];
            condition_express_program = UserFavChannalAlbum.UserName[0] + "=" + UserFavChannalAlbum.ColLocation[UserFavChannalAlbum.UserName];
            condition_express_radio = UserFavChannalAlbum.ChannalAlbumId[0] + "=" + user_down_program[location];//获取某一节目表下所有音频的表达式
        }
        /// <summary>
        /// 对用户收藏节目表/音频表的访问控制
        /// </summary>
        /// <param name="user_fav"></param>
        public bool SaveUserDownProgram(List<object> user_fav_program)
        {
            try
            {
                return SQLiteConnect.TableHandle.AddRecord(table_name_program, user_fav_program);
            }
            catch
            {
                return false;
            }
        }
        public bool SaveUserDownRadio(List<object> user_fav_radio)
        {
            try
            {
                return SQLiteConnect.TableHandle.AddRecord(table_name_radio, user_fav_radio);
            }
            catch
            {
                return false;
            }
        }
        public bool SaveUserDownRadio(List<List<object>> user_fav_radios)
        {
            try
            {
                foreach (List<object> user_down in user_fav_radios)
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
                 SQLiteConnect.TableHandle.SelectRecords(table_name_program, selected_col_program, condition_express_program, ref user_fav_programs_inform);
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
                SQLiteConnect.TableHandle.SelectRecords(table_name_radio, selected_col_radio, condition_express_radio, ref user_fav_radios_inform);
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
            return user_fav_programs_inform;
        }

        private List<List<object>> GetRadioInforms()
        {
            return user_fav_radios_inform;
        }
    }
}
