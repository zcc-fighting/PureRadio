using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;
using System.Net;
using System.Web;


namespace LocalRadioManage.StorageOperate
{

 public static partial  class MyFile
    {
        /// <summary>
        /// 文件创建
        /// </summary>
        public static async Task<StorageFile> CreateFile(StorageFolder root_folder, string file_name)
        {
            StorageFile file=null;
            try
            {
                file=await root_folder.CreateFileAsync(file_name, CreationCollisionOption.OpenIfExists);               
                return file;
            }
            catch
            {
                return null;
            }
        }
        public static async Task<StorageFile> CreateFile(string file_name)
        {
            StorageFolder root_folder = null;
            
            try
            {
                root_folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                return await CreateFile(root_folder, file_name);
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// 文件列表创建
        /// </summary>
        public static async Task<List<StorageFile>> CreateFile(List<string> file_name)
        {
            StorageFolder root_folder = null;

            try
            {
                root_folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                return await CreateFile(root_folder, file_name);
            }
            catch
            {

            }
            return null;
        }
        public static async Task<List<StorageFile>> CreateFile(StorageFolder root_folder, List<string> file_name)
        {
            StorageFile file_ = null;
            List<StorageFile> files = new List<StorageFile>();
            try
            {
                foreach (string file in file_name)
                {
                   file_= await root_folder.CreateFileAsync(file, CreationCollisionOption.OpenIfExists);
                   files.Add(file_);
                }
                return files;
            }
            catch
            {
                files.Clear();
                return null;
            }
           
        }

        //文件创建uri
        public static async Task<StorageFile> CreateFile(StorageFolder root_folder,Uri uri)
        {
            StorageFile temp_file = null;
            StorageFile store_file = null;
            string file_name = "";

            try
            {
                if (uri == null)
                {
                    return null;
                }
                file_name = HttpUtility.UrlDecode(uri.Segments.Last());
                store_file = await MyFile.GetFile(root_folder, file_name);
                if (store_file != null)
                {
                    return store_file;
                }
                temp_file = await StorageFile.CreateStreamedFileFromUriAsync(file_name, uri, null);
                store_file = await MyFile.CreateFile(root_folder, file_name);
                await temp_file.CopyAndReplaceAsync(store_file);

                return store_file;
            }
            catch
            {
                return null;
            }
        }
    }
}
