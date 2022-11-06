using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.LocalService;
using LocalRadioManage.DBBuilder;
using LocalRadioManage.DBBuilder.TableObj;

namespace LocalRadioManage.LocalService.UserInforms
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

     public partial class UserFav
    {
       ///初始化
     public UserFav()
        {
          SetUserFav();

        }
     public UserFav(string user_name)
        {
            SetUserFav(user_name);
        }
     public UserFav(List<object> user_down_program)
        {
            SetUserFav(user_down_program);
        }


     private void SetUserFav()
        {
            table_name_program = UserFavChannalAlbum.TableName;
            selected_col_program = SQLiteConnect.TableHandle.GetColNames(table_name_program).ToList();
            table_name_radio = UserFavRadio.TableName;
            selected_col_radio= SQLiteConnect.TableHandle.GetColNames(table_name_radio).ToList();
        }
        //用户->所有program
     public void SetUserFav(string user_name)
        {
            SetUserFav();
            condition_express_program = UserFavChannalAlbum.UserName[0] + "=" + user_name;
        }
        //program->所有radio
     public bool SetUserFav(List<object> user_fav_program)
        {
            int location= UserFavChannalAlbum.ColLocation[UserFavChannalAlbum.UserName];
            string user_name=(string)user_fav_program[location];
            SetUserFav(user_name);
            location= UserFavChannalAlbum.ColLocation[UserFavChannalAlbum.ChannalAlbumId];
            try
            {
                condition_express_radio = UserFavChannalAlbum.ChannalAlbumId[0] + "=" + user_fav_program.ElementAt(location);
            }
            catch
            {
                return false;
            }
            return true;
        }
        //radio->单个radio
        public bool SetUserFav(List<object> user_fav_radio, bool is_radio)
        {
            int location = UserFavRadio.ColLocation[UserFavRadio.UserName];
            string user_name = (string)user_fav_radio[location];
            SetUserFav(user_name);

            try
            {
                location = UserFavRadio.ColLocation[UserFavRadio.RadioId];
                condition_express_radio = UserFavRadio.RadioId[0] + "=" + (int)user_fav_radio[location];
                location = UserFavRadio.ColLocation[UserFavRadio.RadioDate];
                condition_express_radio = " and " + UserFavRadio.RadioDate[0] + "=" + (ulong)user_fav_radio[location];
            }
            catch
            {
                return false;
            }
            return true;

        }

    }

  public partial  class UserFav
    {

        /// <summary>
        /// 对用户收藏节目表/音频表的访问控制
        /// </summary>
        /// <param name="user_fav"></param>
        public bool SaveUserFavProgram(List<object> user_fav_program)
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
        public bool SaveUserFavRadio(List<object> user_fav_radio)
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
        public bool SaveUserFavRadio(List<List<object>> user_fav_radios)
        {
            try
            {
                foreach (List<object> user_down in user_fav_radios)
                {
                    SaveUserFavRadio(user_down);
                }
                return true;
            }
            catch
            {
                return false; ;
            }
        }

        public List<List<object>> LoadUserFavProgram()
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

        public List<List<object>> LoadUserFavRadio()
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

        public bool DeleteUserFavProgram()
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

        public bool DeleteUserFavRadios()
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
            if (user_fav_programs_inform.Count > 0)
            {
                return user_fav_programs_inform;
            }
            else
            {
                return null;
            }
           
        }

        private List<List<object>> GetRadioInforms()
        {
            if (user_fav_radios_inform.Count > 0)
            {
                return user_fav_radios_inform;
            }
            else
            {
                return null;
            }
           
        }
    }
}
