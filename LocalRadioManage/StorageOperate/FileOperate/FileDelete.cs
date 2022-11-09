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
        /// 文件夹删除
        /// </summary>
        public static async Task<bool> DeleteFile(StorageFolder root_folder, string file_name)
        {
            StorageFile delete_file = null;
            IStorageItem item = null;
            try
            {

                item = await root_folder.TryGetItemAsync(file_name);
                if (item != null && item.IsOfType(StorageItemTypes.File))
                {
                    delete_file = (StorageFile)item;
                    await delete_file.DeleteAsync();
                    return true;
                }
            }
            catch
            {

            }
            return false;
        }
        public static async Task<bool> DeleteFile(string file_name)
        {
            StorageFolder root_folder = null;
            try
            {
                root_folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                return await DeleteFile(root_folder, file_name);
            }
            catch
            {

            }
            return false;
        }

        /// <summary>
        /// 文件夹列表删除
        /// </summary>
        public static async Task<bool> DeleteFile(StorageFolder root_folder, List<string> file_name)
        {
            StorageFile delete_file = null;
            IStorageItem item = null;
            try
            {

                foreach (string file in file_name)
                {
                    item = await root_folder.TryGetItemAsync(file);
                    if (item != null && item.IsOfType(StorageItemTypes.File))
                    {
                        delete_file = (StorageFile)item;
                        await delete_file.DeleteAsync();
                    }
                }
                return true;
            }
            catch
            {

            }
            return false;
        }
        public static async Task<bool> DeleteFile(List<string> file_name)
        {
            StorageFolder root_folder = null;
            try
            {
                root_folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                return await DeleteFile(root_folder, file_name);
            }
            catch
            {

            }
            return false;
        }

        public static async Task<bool> DeleteFile(StorageFolder root_folder, Uri uri)
        {
            string file_name = HttpUtility.UrlDecode(uri.Segments.Last());
            return await DeleteFile(root_folder,file_name);
        }
        public static async Task<List<bool>> DeleteFile(StorageFolder root_folder, List<Uri> uris)
        {
            List<bool> is_files = new List<bool>();
            try
            {
                foreach (Uri uri in uris)
                {
                    try
                    {
                       bool get_file = await DeleteFile(root_folder, uri);
                        is_files.Add(get_file);
                    }
                    catch
                    {
                       is_files.Add(false);
                    }
                }
                return is_files;

            }
            catch
            {

            }
            return null;
        }
    }
}
