using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using Windows.Storage;
using Windows.UI.Xaml.Data;
using LocalRadioManage.DBBuilder.TableObj;
using LocalRadioManage.StorageOperate;



namespace LocalRadioManage.DBBuilder
{
    public  partial class SQLiteConnect
    {
       
        private const string default_folder_name = "LocalDatabase";
        private const string default_db_name = "LocalRadioManage.db";
        private static StorageFolder local_folder=null;
        private static StorageFile db_file = null;
        public static SQLiteConnection  db_connect = null;
      
        /// <summary>
        /// 连接数据库
        /// </summary>
        public static bool Connect()
        {
            string connect_str;
            SQLiteCommand cmd;

            //创建或获取数据库文件
            if (db_file == null)
            {
                //等待结果
                Task<Task> taskCreate = new Task<Task>(CreateDatabase);
                taskCreate.Start();
                taskCreate.Result.Wait();

               if (db_file == null)
               {
                    return false;
               }
            }

           //连接数据库并允许外键
            connect_str = "data source= " + db_file.Path;
            db_connect = new SQLiteConnection(connect_str);
            db_connect.Open();
            cmd = new SQLiteCommand("PRAGMA foreign_keys = ON",db_connect);
            cmd.ExecuteNonQuery();
            cmd = new SQLiteCommand("PRAGMA recursive_triggers = true;", db_connect);
            cmd.ExecuteNonQuery();
            return true;
        }

        /// <summary>
        /// 断开与数据库连接
        /// </summary>
        public static bool Disconnect()
        {
            if (db_connect != null)
            {
                db_connect.Close();
                db_connect = null;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 创建数据库文件
        /// </summary>
        private static async Task<bool> CreateDatabase()
        {
            //文件夹创建
            try
            {
                local_folder = await MyFolder.CreateFolder(default_folder_name);
                db_file =await MyFile.CreateFile(local_folder,default_db_name);
                return true;
            }
            catch
            {
                return false;
            }
        }
       

    }

    //应用于此应用的数据模型
   
    public static partial class SQLiteConnect
    {
        public static VTable TableHandle = new VTable();
       
        public static bool CreateLocalRadioManage()
        {
            if (SQLiteConnect.Connect())
            {
                LocalChannalAlbum local_channel_album = new LocalChannalAlbum();
                LocalRadio local_radio = new LocalRadio();
                Users user = new Users();
                UserDownChannalAlbum user_down_channal_album = new UserDownChannalAlbum();
                UserDownRadio user_down_radio = new UserDownRadio();
                UserFavChannalAlbum user_fav_channal_album = new UserFavChannalAlbum();
                UserFavRadio user_fav_radio = new UserFavRadio();

                TableHandle.AddTable(LocalChannalAlbum.TableName, local_channel_album.GetTableInform());
                TableHandle.AddTable(LocalRadio.TableName, local_radio.GetTableInform());
                TableHandle.AddTable(Users.TableName, user.GetTableInform());
                TableHandle.AddTable(UserDownChannalAlbum.TableName, user_down_channal_album.GetTableInform());
                TableHandle.AddTable(UserDownRadio.TableName, user_down_radio.GetTableInform());
                TableHandle.AddTable(UserFavChannalAlbum.TableName, user_fav_channal_album.GetTableInform());
                TableHandle.AddTable(UserFavRadio.TableName, user_fav_radio.GetTableInform());

                
                TableHandle.CreateTable(LocalChannalAlbum.TableName);
                TableHandle.CreateTable(LocalRadio.TableName);
                TableHandle.CreateTable(Users.TableName);
                TableHandle.CreateTable(UserDownChannalAlbum.TableName);
                TableHandle.CreateTable(UserDownRadio.TableName);
                TableHandle.CreateTable(UserFavChannalAlbum.TableName);
                TableHandle.CreateTable(UserFavRadio.TableName);
                SQLiteConnect.Disconnect();
                return true;
            }
            return false;
        }
    }

   
}
