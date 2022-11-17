using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalRadioManage.LocalService;
using System.IO;
using System.Net;
using System.Web;
using Windows.Storage;
using LocalRadioManage.StorageOperate;
using LocalRadioManage.DBBuilder;
using LocalRadioManage.DBBuilder.TableObj;
using LocalRadioManage.DataModelTransform;
using LocalRadioManage.DBBuilder.TableOperate;
using DataModels;


namespace LocalRadioManage.LocalService.UserInforms
{
    public partial class UserDown
    {

       public AllLocalDown LocalDown = new AllLocalDown();

        private string user_name = "0";
        /// <summary>
        /// 用户下载节目表参数
        /// </summary>
        private string user_program_name = "";
        private string local_program_name = "";
        private string condition_express_program = "";
        private Dictionary<string, List<string>> table_cols_program = null;

        /// <summary>
        /// 用户下载音频表参数
        /// </summary>
        private string user_radio_name = "";
        private string local_radio_name = "";
        private string condition_express_radio = "";
        private Dictionary<string, List<string>> table_cols_radio = null;

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
       ///初始化
     public UserDown()
        {
          SetUserDown();

        }
     public UserDown(string user_name)
        {
            SetUserDown(user_name);
        }
     public UserDown(RadioFullAlbum album)
        {
          
            SetUserDown(album);
        }
     public UserDown(RadioFullContent radio)
        {
           
            SetUserDown(radio);
        }
    
     private void SetUserDown()
        {
            user_name = "0";
            user_program_name = UserDownChannalAlbum.TableName;
            local_program_name = LocalChannalAlbum.TableName;
            user_radio_name = UserDownRadio.TableName;
            local_radio_name = LocalRadio.TableName;
 
            List<string> selected_col_program = SQLiteConnect.TableHandle.GetColNames(local_program_name).ToList();
            List<string> selected_col_radio = SQLiteConnect.TableHandle.GetColNames(local_radio_name).ToList();

            table_cols_program = new Dictionary<string, List<string>>() {
                    { local_program_name,selected_col_program },
                    { user_program_name,null}
                };

          
            table_cols_radio = new Dictionary<string, List<string>>() {
                    { local_radio_name,selected_col_radio },
                    { user_radio_name,null}
                };

        }
        //某一用户->所有program
     public void SetUserDown(string user_name)
        {    
            SetUserDown();
            this.user_name = user_name;
            condition_express_program =user_program_name+"."+UserDownChannalAlbum.UserName[0] + "=" + user_name;//找到用户
            condition_express_program += " and " + user_program_name + "." + UserDownChannalAlbum.ChannalAlbumId[0] 
                + "=" + local_program_name + "." + LocalChannalAlbum.ChannalAlbumId[0];//匹配用户所有专辑
        }
     //某一program->所有radio
     public bool SetUserDown(RadioFullAlbum album)
        {
            try
            {
                LocalDown.SetLocalDown(album);
                SetUserDown(album.user);
                user_name = album.user;
                condition_express_radio = user_radio_name + "." + UserDownRadio.UserName[0] + "=" + album.user;//找到用户
                condition_express_radio += " and " + user_radio_name + "." + UserDownRadio.ChannalAlbumId[0] + "=" + album.id;//匹配某一专辑
                condition_express_radio += " and " + local_radio_name + "." + LocalRadio.RadioId[0]
                    + "=" + user_radio_name + "." + UserDownRadio.RadioId[0];
                condition_express_radio += " and " + local_radio_name + "." + LocalRadio.RadioDate[0]
                    + "=" + user_radio_name + "." + UserDownRadio.RadioDate[0];//匹配专辑内音频
                return true;
            }
            catch
            {
                return false;
            }
        }

        //radio->单个radio
     public bool SetUserDown(RadioFullContent radio)
        {
            try
            {
                LocalDown.SetLocalDown(radio);
                SetUserDown(radio.user);
                user_name = radio.user;
                condition_express_radio = user_radio_name + "." + UserDownRadio.UserName[0] + "=" + radio.user;//找到用户
                condition_express_radio += " and " + user_radio_name + "." + UserDownRadio.RadioId[0] + "=" + radio.id;
                ulong date = DateTransform.DateToInt(radio.date, radio.start_time, radio.end_time);
                condition_express_radio += " and " + user_radio_name + "." + UserDownRadio.RadioDate[0] + "=" + date;//匹配某一radio
                return true;
            }
            catch
            {
                return false;
            }
        }
      
    }

   public partial class UserDown
    {
    
        public bool SaveUserDownProgram(RadioFullAlbum album)
        {
            try
            {
                List<object> user_program = ChannalAlbumTransform.Local.ToUserDownChannalAlubum(album);
                LocalDown.SaveLocalDownProgram(album);
                return SQLiteConnect.TableHandle.AddRecord(this.user_program_name, user_program);
            }
            catch
            {
                return false;
            }
        }
        public bool SaveUserDownRadio(RadioFullContent radio)
        {
            try
            {
                List<object> user_radio= RadioTransform.Local.ToUserDownRadio(radio);
                LocalDown.SaveLocalDownRadio(radio);
                return SQLiteConnect.TableHandle.AddRecord(user_radio_name, user_radio);
            }
            catch
            {
                return false;
            }
        }
        public bool SaveUserDownRadio(List<RadioFullContent> radios)
        {
            try
            {
                foreach (RadioFullContent radio in radios)
                {
                    SaveUserDownRadio(radio);
                }
                return true;
            }
            catch
            {
                return false; ;
            }
        }

        public List<RadioFullAlbum> LoadUserDownProgram()
        {
            try
            {
                SQLiteConnect.TableHandle.SelectRecords(table_cols_program, condition_express_program,ref user_down_programs_inform);
                return GetProgramInforms();
            }
            catch
            {
                return null;
            }
        }
        public List<RadioFullContent> LoadUserDownRadio()
        {
            try
            {
                SQLiteConnect.TableHandle.SelectRecords(table_cols_radio, condition_express_radio, ref user_down_radios_inform);
                return GetRadioInforms();
            }
            catch
            {
                return null;
            }
        }

        //删除user_program
        public bool DeleteUsrDownProgram(bool is_constrant)
        {
            try
            {
            
              List<string> table_name =new List<string>(){user_program_name,local_program_name };
             return  SQLiteConnect.TableHandle.DeleteRecords(user_program_name,table_name, condition_express_program, is_constrant);
      
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteUsrDownRadios(bool is_constrant)
        {
            try
            {
                List<string> table_name = new List<string>() { user_radio_name, local_radio_name };
                return SQLiteConnect.TableHandle.DeleteRecords(user_radio_name,table_name,condition_express_radio, is_constrant);
            }
            catch
            {
                return false;
            }

        }


        public void SetSelectedCols_Program(List<string> program_cols)
        {
            table_cols_program[local_program_name] = program_cols;
        }
        public void SetSelectedCols_Radio(List<string> radio_cols)
        {
            table_cols_radio[local_radio_name] = radio_cols;
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
            if (user_down_programs_inform.Count > 0)
            {
                return ChannalAlbumTransform.Local.ToRadioFullAlbum(user_down_programs_inform,user_name);
            }
            else
            {
                return null;
            }
        }
        private List<RadioFullContent> GetRadioInforms()
        {
            if (user_down_radios_inform.Count > 0)
            {
                return RadioTransform.Local.ToRadioFullContent(user_down_radios_inform,user_name);
            }
            else
            {
                return null;
            }
        }

    }
}
