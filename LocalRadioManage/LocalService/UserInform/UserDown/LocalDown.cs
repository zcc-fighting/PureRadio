using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
  public partial  class AllLocalDown
    {
        string user_name = "0";
        /// <summary>
        /// 下载节目表参数
        /// </summary>
        private string local_program_name = "";
        private string condition_express_program = "";
        private string program_express_check="";
        List<string> selected_col_program = new List<string>();

        /// <summary>
        /// 用户下载音频表参数
        /// </summary>
        private string local_radio_name = "";
        private string condition_express_radio = "";
        private string radio_express_check="";
        List<string> selected_col_radio = new List<string>();

        /// <summary>
        /// 获取节目表单信息
        /// </summary>
        private List<List<object>> local_down_programs_inform = new List<List<object>>();

        /// <summary>
        /// 获取音频表单信息
        /// </summary>
        private List<List<object>> local_down_radios_inform = new List<List<object>>();

    }

    public partial class AllLocalDown
    {

        public AllLocalDown()
        {
            SetLocalDown();
        }
        public AllLocalDown(RadioFullAlbum album)
        {
            SetLocalDown(album);
        }

        public AllLocalDown(RadioFullContent radio)
        {
            SetLocalDown(radio);
        }

        //所有program/radio
        public void SetLocalDown()
        {
            user_name = "0";

            local_program_name = LocalChannalAlbum.TableName;
            local_radio_name = LocalRadio.TableName;

            selected_col_program = SQLiteConnect.TableHandle.GetColNames(local_program_name).ToList();
            selected_col_radio = SQLiteConnect.TableHandle.GetColNames(local_radio_name).ToList();

            List<string> select_all = new List<string>() { "*" };

            Dictionary<string, List<string>> table_cols_program = new Dictionary<string, List<string>>()
            {
                {local_program_name,select_all},
                {UserDownChannalAlbum.TableName,null}

            };
            //用户节目表中没有与之对应id
            program_express_check =LocalChannalAlbum.TableName+"."+  LocalChannalAlbum.ChannalAlbumId[0]
                + "==" + UserDownChannalAlbum.TableName + "." + UserDownChannalAlbum.ChannalAlbumId[0];
            program_express_check = " not exists( " + Select.GetSelectQuery(table_cols_program, program_express_check) + ")";
            Dictionary<string, List<string>> table_cols_radio = new Dictionary<string, List<string>>()
            {
                {local_radio_name,select_all},
                {UserDownRadio.TableName,null}

            };
           
            //用户音频表没有与之对应的(id,date)
            radio_express_check =LocalRadio.TableName+"."+  LocalRadio.RadioId[0] + "==" + UserDownRadio.TableName + "." + UserDownRadio.RadioId[0]
                 + " and "+ LocalRadio.TableName + "." + LocalRadio.RadioDate[0] + "==" + UserDownRadio.TableName + "." + UserDownRadio.RadioDate[0];
            radio_express_check=" not exists( "+Select.GetSelectQuery(table_cols_radio, radio_express_check)+")";
        }
        //某一program->所有radio
        public void SetLocalDown(RadioFullAlbum album)
        {
            SetLocalDown();
            user_name = album.user;
            condition_express_program =local_program_name+"."+LocalChannalAlbum.ChannalAlbumId[0] + "=" + album.id;
            condition_express_radio =local_radio_name+"."+ LocalRadio.ChannalAlbumId[0] + "=" + album.id;
        }
        //radio->单个radio
        public void SetLocalDown(RadioFullContent radio)
        {
            user_name = radio.user;
            ulong date = DateTransform.DateToInt(DateTransform.GetDateTime(radio.day), radio.start_time, radio.end_time);
            SetLocalDown();
            condition_express_radio =local_radio_name+"."+ LocalRadio.RadioId[0] + "=" + radio.id
                + " and " +local_radio_name+"."+ LocalRadio.RadioDate[0] + "=" + date;
        }
    }

    public partial class AllLocalDown
    {
        public bool SaveLocalDownProgram(RadioFullAlbum album)
        {
            try
            {
                List<object> local_program = ChannalAlbumTransform.Local.ToLocalChannalAlbumStorage(album);
                return SQLiteConnect.TableHandle.AddRecord(local_program_name, local_program);
            }
            catch
            {
                return false;
            }
        }
        public void SaveLocalDownProgram(List<RadioFullAlbum> albums)
        {
            foreach (RadioFullAlbum album in albums)
            {
                 SaveLocalDownProgram(album);
            }
        }

        public bool SaveLocalDownProgram(RadioFullAlbum album, List<RadioFullContent> radios)
        {
            if (!SaveLocalDownProgram(album))
            {
                return false;
            }
            SaveLocalDownRadio(radios);

            return true;
        }
        public bool SaveLocalDownRadio(RadioFullContent radio)
        {
            try
            {
                List<object> local_radio = RadioTransform.Local.ToLocalRadioStorage(radio);
                return SQLiteConnect.TableHandle.AddRecord(local_radio_name, local_radio);
            }
            catch
            {
                return false;
            }
        }

        public void SaveLocalDownRadio(List<RadioFullContent> radios)
        {
            try
            {
                foreach (RadioFullContent radio in radios)
                {
                  SaveLocalDownRadio(radio);
                }
            }
            catch
            {
                return;
            }
        }


        public List<RadioFullAlbum> LoadLocalDownProgram()
        {
            try
            {
                SQLiteConnect.TableHandle.SelectRecords(local_program_name,selected_col_program, condition_express_program, ref local_down_programs_inform);
                
                return GetProgramInforms();
            }
            catch
            {
                return null;
            }
        }
        public List<RadioFullContent> LoadLocalDownRadio()
        {
            try
            {
                SQLiteConnect.TableHandle.SelectRecords(local_radio_name,selected_col_radio, condition_express_radio, ref local_down_radios_inform);
                return GetRadioInforms();
            }
            catch
            {
                return null;
            }
        }

        //加载没有用户依赖的本地节目
        public List<RadioFullAlbum> LoadLocalDownProgram_Check()
        {
          
            SQLiteConnect.TableHandle.SelectRecords(local_program_name,selected_col_program, program_express_check,ref local_down_programs_inform);
            return GetProgramInforms();
        }
        //加载没有用户依赖的本地音频
        public List<RadioFullContent> LoadLocalDownRaio_Check()
        {
      
            SQLiteConnect.TableHandle.SelectRecords(local_radio_name,selected_col_radio, radio_express_check, ref local_down_radios_inform);
            return GetRadioInforms();
        }


        public bool DeleteLocalDownProgram(bool is_constrant)
        {
            try
            {
               return  SQLiteConnect.TableHandle.DeleteRecords(local_program_name, condition_express_program, is_constrant);
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteLocalDownRadios(bool is_constrant)
        {
            try
            {
                return SQLiteConnect.TableHandle.DeleteRecords(local_radio_name, condition_express_radio, is_constrant);
            }
            catch
            {
                return false;
            }

        }

        //删除没有用户依赖的本地节目
         public bool DeleteLocalDownProgram_Check(bool is_constrant)
        {
            try
            {
               List<string> table_name=new List<string>() {local_program_name,UserDownChannalAlbum.TableName };
               return  SQLiteConnect.TableHandle.DeleteRecords(local_program_name,table_name,program_express_check, is_constrant);
            }
            catch
            {
                return false;
            }
        }
        //删除没有用户依赖的本地音频
        public bool DeleteLocalDownRadios_Check(bool is_constrant)
        {
            try
            {
                List<string> table_name=new List<string>() {local_program_name,UserDownRadio.TableName };
                return SQLiteConnect.TableHandle.DeleteRecords(local_radio_name, radio_express_check, is_constrant);
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
            if (local_down_programs_inform.Count > 0)
            {
                return ChannalAlbumTransform.Local.ToRadioFullAlbum(local_down_programs_inform,user_name);
            }
            else
            {
                return null;
            }
        }
        private List<RadioFullContent> GetRadioInforms()
        {
            if (local_down_radios_inform.Count > 0)
            {
                return RadioTransform.Local.ToRadioFullContent(local_down_radios_inform,user_name);
            }
            else
            {
                return null;
            }
        }

    }

    }
