using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.LocalService;
using LocalRadioManage.DBBuilder;
using LocalRadioManage.DBBuilder.TableObj;
using LocalRadioManage.DataModelTransform;
using DataModels;

namespace LocalRadioManage.LocalService.UserInforms
{
    public partial class UserFav
    {

        private string user_name = "0";
        /// <summary>
        /// 收藏节目表参数
        /// </summary>
        private string user_program_name = "";
        private List<string> selected_col_program = null;
        private string condition_express_program = "";

        /// <summary>
        /// 收藏音频表参数
        /// </summary>
        private string user_radio_name = "";
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
     public UserFav(RadioFullAlbum album)
        {
            SetUserFav( album);
        }
        public UserFav(RadioFullContent radio)
        {
            SetUserFav(radio);
        }


        private void SetUserFav()
        {
            user_name = "0";
            user_program_name = UserFavChannalAlbum.TableName;
            selected_col_program = SQLiteConnect.TableHandle.GetColNames(user_program_name).ToList();
            user_radio_name = UserFavRadio.TableName;
            selected_col_radio= SQLiteConnect.TableHandle.GetColNames(user_radio_name).ToList();
        }
        //某一用户->所有program
     public void SetUserFav(string user_name)
        {
            SetUserFav();
            this.user_name = user_name;
            condition_express_program = UserFavChannalAlbum.UserName[0] + "=" + user_name;
        }
        
        //某一program->所有radio
     public bool SetUserFav(RadioFullAlbum album)
        {
            try
            {
                SetUserFav(album.user);
                user_name = album.user;
                condition_express_program += UserFavChannalAlbum.ChannalAlbumId[0] + "=" + album.id;
                return true;
            }
            catch
            {
                return false;
            }
        }
        //radio->单个radio
     public bool SetUserFav(RadioFullContent radio)
        {
            SetUserFav(radio.user);
            try
            {
                user_name = radio.user;
                condition_express_radio = UserFavRadio.RadioId[0] + "=" + radio.id;
                ulong date = DateTransform.DateToInt(DateTransform.GetDateTime(radio.day), radio.start_time, radio.end_time);
                condition_express_radio = " and " + UserFavRadio.RadioDate[0] + "=" + date;
                return true;
            }
            catch
            {
                return false;
            }
           
        }

    }

  public partial  class UserFav
    {

        /// <summary>
        /// 对用户收藏节目表/音频表的访问控制
        /// </summary>
        /// <param name="user_fav"></param>
        public bool SaveUserFavProgram(RadioFullAlbum album)
        {
            try
            {
                List<object> user_fav_program= ChannalAlbumTransform.Remote.ToUserFavChannalAlbumStorage(album);
                return SQLiteConnect.TableHandle.AddRecord(user_program_name, user_fav_program);
            }
            catch
            {
                return false;
            }
        }
        public bool SaveUserFavRadio(RadioFullContent radio)
        {
            try
            {
                List<object> user_fav_radio = RadioTransform.Remote.ToRemoteRadioFav(radio);
                return SQLiteConnect.TableHandle.AddRecord(user_radio_name, user_fav_radio);
            }
            catch
            {
                return false;
            }
        }
        public bool SaveUserFavRadio(List<RadioFullContent> radios)
        {
            try
            {
                foreach (RadioFullContent radio in radios)
                {
                    SaveUserFavRadio(radio);
                }
                return true;
            }
            catch
            {
                return false; ;
            }
        }

        public List<RadioFullAlbum> LoadUserFavProgram()
        {
            try
            {
                 SQLiteConnect.TableHandle.SelectRecords(user_program_name, selected_col_program, condition_express_program, ref user_fav_programs_inform);
                return GetProgramInforms();

            }
            catch
            {
                return null;
            }
        }

        public List<RadioFullContent> LoadUserFavRadio()
        {
            try
            {
                SQLiteConnect.TableHandle.SelectRecords(user_radio_name, selected_col_radio, condition_express_radio, ref user_fav_radios_inform);
                return GetRadioInforms();
            }
            catch
            {
                return null;
            }
        }

        public bool DeleteUserFavProgram(bool is_constraint)
        {
            try
            {
                return SQLiteConnect.TableHandle.DeleteRecords(user_program_name, condition_express_program,is_constraint);
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteUserFavRadios(bool is_constraint)
        {
            try
            {
                return SQLiteConnect.TableHandle.DeleteRecords(user_radio_name, condition_express_radio,is_constraint);
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

        private List<RadioFullAlbum> GetProgramInforms()
        {
            if (user_fav_programs_inform.Count > 0)
            {
               return ChannalAlbumTransform.Remote.ToRadioFullAlbum(user_fav_programs_inform);
            }
            else
            {
                return null;
            }
           
        }

        private List<RadioFullContent> GetRadioInforms()
        {
            if (user_fav_radios_inform.Count > 0)
            {
               return RadioTransform.Remote.ToRadioFullContent(user_fav_radios_inform);
            }
            else
            {
                return null;
            }
           
        }
    }
}
