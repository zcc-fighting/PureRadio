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
    public static partial class MyFile
    {
        /// <summary>
        /// 获取文件夹
        /// </summary>
        public static async Task<StorageFile> GetFile(StorageFolder root_folder, string file_name)
        {
            IStorageItem item = null;
            try
            {
                item = await root_folder.TryGetItemAsync(file_name);
                if (item != null && item.IsOfType(StorageItemTypes.File))
                {
                
                    return (StorageFile)item;
                }
            }
            catch
            {

            }
            return null;
        }
        public static async Task<StorageFile> GetFile(string file_name)
        {
            StorageFolder root_folder = null;

            try
            {
                root_folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                return await GetFile(root_folder, file_name);
            }
            catch
            {

            }
            return null;
        }
        /// <summary>
        /// 获取文件夹列表
        /// </summary>
        public static async Task<List<StorageFile>> GetFiles(StorageFolder root_folder, List<string> file_name)
        {
            List<StorageFile> files = new List<StorageFile>();
            try
            {
                foreach (string file in file_name)
                {
                    try
                    {
                        StorageFile get_file = await GetFile(root_folder, file);
                        files.Add(get_file);
                    }
                    catch
                    {
                        files.Add(null);
                    }
                }
                return files;

            }
            catch
            {

            }
            return null;
        }
        public static async Task<List<StorageFile>> GetFiles(List<string> file_name)
        {
            StorageFolder root_folder = null;

            try
            {
                root_folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                return await GetFiles(root_folder, file_name);
            }
            catch
            {

            }
            return null;

        }


        public static async Task<StorageFile> GetFile(StorageFolder root_folder, Uri uri)
        {
           StorageFile file = null;
           string file_name = HttpUtility.UrlDecode(uri.Segments.Last());
          
           file= await GetFile(root_folder,file_name);

            if (file == null)
            {
              return await  GetFile(uri);
            }
            return file;
        }
        public static async Task<StorageFile> GetFile( Uri uri)
        {
            try
            {
                return await StorageFile.GetFileFromPathAsync(uri.ToString());
            }
            catch
            {
                return null;
            }
        }

        public static async Task<List<StorageFile>> GetFile(List<Uri> uris)
        {
            List<StorageFile> files = new List<StorageFile>();
            foreach (Uri uri in uris)
            {
                files.Add(await GetFile(uri));
            }
            return files;
        }
        public static async Task<List<StorageFile>> GetFile(StorageFolder root_folder, List<Uri> uris)
        {
            List<StorageFile> files = new List<StorageFile>();
            try
            {
                foreach (Uri uri in uris)
                {
                    try
                    {
                        StorageFile get_file = await GetFile(root_folder, uri);
                        files.Add(get_file);
                    }
                    catch
                    {
                        files.Add(null);
                    }
                }
                return files;

            }
            catch
            {

            }
            return null;
        }

        public static async Task<bool> FileExists(StorageFolder root_folder,Uri uri)
        {
            try
            {
               string file_name = HttpUtility.UrlDecode(uri.Segments.Last());
               
               Windows.Storage.IStorageItem item=await root_folder.TryGetItemAsync(file_name);

                List<string> folders = new List<string>();
                string path = uri.AbsolutePath.Replace("/" + file_name, "");
                

                if (item == null || !item.IsOfType(StorageItemTypes.File))
                {
                    return false;
                }
            
                return true;
            }
            catch
            {
                return true;
            }


        }


    }
}
