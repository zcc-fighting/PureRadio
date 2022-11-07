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
        /// <summary>
        /// 下载节目表参数
        /// </summary>
        private string user_program_name = "";
        private string local_program_name = "";
        private string condition_express_program = "";
        private Dictionary<string, List<string>> table_cols_program = null;

        /// <summary>
        /// 下载音频表参数
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
            user_program_name = UserDownChannalAlbum.TableName;
            local_program_name = LocalChannalAlbum.TableName;
            user_radio_name = UserDownRadio.TableName;
            local_radio_name = LocalRadio.TableName;
        

            List<string> selected_col_program = SQLiteConnect.TableHandle.GetColNames(local_program_name).ToList();
            table_cols_program = new Dictionary<string, List<string>>() {
                    { local_program_name,selected_col_program },
                    { user_program_name,null}
                };

            List<string> selected_col_radio = SQLiteConnect.TableHandle.GetColNames(local_radio_name).ToList();
            table_cols_radio = new Dictionary<string, List<string>>() {
                    { local_radio_name,selected_col_radio },
                    { user_radio_name,null}
                };

        }
        //某一用户->所有program
     public void SetUserDown(string user_name)
        {
            SetUserDown();
            condition_express_program =user_program_name+"."+UserDownChannalAlbum.UserName[0] + "=" + user_name;//找到用户
            condition_express_program += " and " + user_program_name + "." + UserDownChannalAlbum.ChannalAlbumId[0] 
                + "=" + local_program_name + "." + LocalChannalAlbum.ChannalAlbumId[0];//匹配用户所有专辑
        }
     //某一program->所有radio
     public bool SetUserDown(RadioFullAlbum album)
        {
            try
            {
                SetUserDown(album.user);
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
                SetUserDown(radio.user);
                condition_express_radio = user_radio_name + "." + UserDownRadio.UserName[0] + "=" + radio.user;//找到用户
                condition_express_radio += " and " + user_radio_name + "." + UserDownRadio.RadioId[0] + "=" + radio.id;
                ulong date = DateTransform.DateToInt(DateTransform.GetDateTime(radio.day), radio.start_time, radio.end_time);
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
                List<object> local_program = ChannalAlbumTransform.Local.ToLocalChannalAlbumStorage(album);
                SQLiteConnect.TableHandle.AddRecord(this.local_program_name, local_program);
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
                List<object> local_radio = RadioTransform.Local.ToLocalRadioStorage(radio);
                SQLiteConnect.TableHandle.AddRecord(local_radio_name, local_radio);
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

        //删除user_program,因为设置了外键local_program是否能删除取决于是否被引用
        public bool DeleteUsrDownProgram(bool is_constrant)
        {
            try
            {
            
              List<string> table_name =new List<string>(){user_program_name,local_program_name };
             return  SQLiteConnect.TableHandle.DeleteRecords(table_name,table_name, condition_express_program, is_constrant);

              //逆天
              //string check_express = local_program_name + "." + LocalChannalAlbum.ChannalAlbumId[0] + " not in (";
              //List<string> temp = new List<string>() { UserDownChannalAlbum.ChannalAlbumId[0] };
              //check_express += Select.GetSelectQuery(user_program_name, temp, "");
              //check_express += ")";
              //SQLiteConnect.TableHandle.SelectRecords(local_program_name, table_cols_program[local_program_name], check_express, ref user_down_programs_inform);
            
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
                return SQLiteConnect.TableHandle.DeleteRecords(table_name,table_name,condition_express_radio, is_constrant);
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
                return ChannalAlbumTransform.Local.ToRadioFullAlbum(user_down_programs_inform);
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
                return RadioTransform.Local.ToRadioFullContent(user_down_radios_inform);
            }
            else
            {
                return null;
            }
        }

    }
}
