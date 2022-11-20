using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using Windows.Storage;
using Windows.UI.Xaml.Data;
using PureRadio.LocalRadioManage.DBBuilder.TableObj;
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
        private static SQLiteConnection  db_connect = new SQLiteConnection();
        private static bool db_connect_used = false;
      
        /// <summary>
        /// 连接数据库
        /// </summary>
        public static SQLiteConnection Connect()
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
                    return null;
               }
            }

           //连接数据库并允许外键
            connect_str = "data source= " + db_file.Path;

            SQLiteConnection get_db_connect= GetDbConnect();
            get_db_connect = new SQLiteConnection(connect_str);
            get_db_connect.Open();
            cmd = new SQLiteCommand("PRAGMA foreign_keys = ON", get_db_connect);
            cmd.ExecuteNonQuery();
            cmd = new SQLiteCommand("PRAGMA recursive_triggers = true;", get_db_connect);
            cmd.ExecuteNonQuery();
            return get_db_connect;
        }

        /// <summary>
        /// 断开与数据库连接
        /// </summary>
        public static bool Disconnect()
        {
           try
             {
                CloseDbConnect();
                 return true;
              }
          catch
               {
                CloseDbConnect();
                return false;
                }
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
       
        //对dbconnect访问加锁
        private static SQLiteConnection GetDbConnect()
        {
            uint counts = 1000000;
            while (db_connect_used)
            {
                counts--;
                if (counts == 1)
                {
                    throw new Exception("may somewhere dont disconnect");
                }
            } ;
            lock (db_connect)
            {
                db_connect_used = true;
                return db_connect;
            }
        }
        private static void CloseDbConnect()
        {
            lock (db_connect)
            {
                db_connect.Close();
                db_connect_used = false;
            }
        }


    }

    //应用于此应用的数据模型
    public static partial class SQLiteConnect
    {
        public static VTable TableHandle = new VTable();
        private static bool only_one_create = true;
       
        public static bool CreateLocalRadioManage()
        {
            if ((SQLiteConnect.Connect()!=null)&&only_one_create)
            {
                SQLiteConnect.Disconnect();
                only_one_create = false;
                AlbumCard albumCard = new AlbumCard();
                AlbumRadio albumRadio = new AlbumRadio();
                UserAlbumCard userAlbumCard = new UserAlbumCard();
                UserAlbumRadio userAlbumRadio = new UserAlbumRadio();
                ChannalCard channalCard = new ChannalCard();
                ChannalRadio channalRadio = new ChannalRadio();
                UserChannalCard userChannalCard = new UserChannalCard();
                UserChannalRadio userChannalRadio = new UserChannalRadio();
                UserInforms userInforms = new UserInforms();

                TableHandle.AddTable(AlbumCard.TableName, albumCard.GetTableInform());
                TableHandle.AddTable(AlbumRadio.TableName, albumRadio.GetTableInform());
                TableHandle.AddTable(UserAlbumCard.TableName, userAlbumCard.GetTableInform());
                TableHandle.AddTable(UserAlbumRadio.TableName, userAlbumRadio.GetTableInform());
                TableHandle.AddTable(ChannalCard.TableName, channalCard.GetTableInform());
                TableHandle.AddTable(ChannalRadio.TableName, channalRadio.GetTableInform());
                TableHandle.AddTable(UserChannalCard.TableName, userChannalCard.GetTableInform());
                TableHandle.AddTable(UserChannalRadio.TableName, userChannalRadio.GetTableInform());
                TableHandle.AddTable(UserInforms.TableName, userInforms.GetTableInform());

                TableHandle.CreateTable(AlbumCard.TableName);
                TableHandle.CreateTable(AlbumRadio.TableName);
                TableHandle.CreateTable(ChannalCard.TableName);
                TableHandle.CreateTable(ChannalRadio.TableName);
                TableHandle.CreateTable(UserInforms.TableName);
                TableHandle.CreateTable(UserAlbumCard.TableName);
                TableHandle.CreateTable(UserAlbumRadio.TableName);
                TableHandle.CreateTable(UserChannalCard.TableName);
                TableHandle.CreateTable(UserChannalRadio.TableName);


                return true;
            }
            SQLiteConnect.Disconnect();
            return false;
        }
    }

   
}
