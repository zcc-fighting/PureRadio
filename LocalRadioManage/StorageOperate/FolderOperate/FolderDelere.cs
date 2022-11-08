using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;


namespace LocalRadioManage.StorageOperate
{
    public static partial class MyFolder
    {
        /// <summary>
        /// 文件夹删除
        /// </summary>
        public static async Task<bool> DeleteFolder(StorageFolder root_folder,string folder_name)
        {
            StorageFolder delete_folder = null;
            IStorageItem item = null;
            try
            {

                item = await root_folder.TryGetItemAsync(folder_name);
                if (item != null && item.IsOfType(StorageItemTypes.Folder))
                {
                    delete_folder = (StorageFolder)item;
                    await delete_folder.DeleteAsync();
                    return true;
                }
            }
            catch
            {

            }
            return false;
        }
        public static async Task<bool> DeleteFolder(string folder_name)
        {
            StorageFolder root_folder = null;
            try
            {
                root_folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                return await DeleteFolder(root_folder, folder_name);
            }
            catch
            {
               
            }
            return false;
        }

        /// <summary>
        /// 文件夹列表删除
        /// </summary>
        public static async Task<bool> DeleteFolder(StorageFolder root_folder, List<string> folder_name)
        {
            StorageFolder delete_folder = null;
            IStorageItem item = null;
            try
            {

                foreach (string folder in folder_name)
                {
                    item = await root_folder.TryGetItemAsync(folder);
                    if (item != null && item.IsOfType(StorageItemTypes.Folder))
                    {
                        delete_folder = (StorageFolder)item;
                        await delete_folder.DeleteAsync();
                    }
                }
                return true;
            }
            catch
            {

            }
            return false;
        }
        public static async Task<bool> DeleteFolder(List<string> folder_name)
        {
            StorageFolder root_folder=null;
            try
            {
                root_folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                return await DeleteFolder(root_folder, folder_name);
            }
            catch
            {

            }
            return false;
        }
    }
}
