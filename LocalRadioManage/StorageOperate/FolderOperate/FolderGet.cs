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
        /// 获取文件夹
        /// </summary>
        public static async Task<StorageFolder> GetFolder(StorageFolder root_folder,string folder_name)
        {
            IStorageItem item = null;
            try
            {
                item = await root_folder.TryGetItemAsync(folder_name);
                if (item != null && item.IsOfType(StorageItemTypes.Folder))
                {
                    return (StorageFolder)item;
                }
            }
            catch
            {

            }
            return null;
        }
        public static async Task<StorageFolder> GetFolder(string folder_name)
        {
            StorageFolder root_folder=null;
            
            try
            {
                
                root_folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                return await GetFolder(root_folder,folder_name);
            }
            catch
            {

            }
            return null;
        }
        /// <summary>
        /// 获取文件夹列表
        /// </summary>
        public static async Task<List<StorageFolder>> GetFolders(StorageFolder root_folder, List<string> folder_name)
        {
            List<StorageFolder> folders=new List<StorageFolder>();
            try
            {
                foreach(string folder in folder_name)
                {
                    try
                    {
                        StorageFolder get_folder = await GetFolder(root_folder, folder);
                        folders.Add(get_folder);
                    }
                    catch
                    {
                        folders.Add(null);
                    }
                }
                return folders;
               
            }
            catch
            {

            }
            return null;
        }
        public static async Task<List<StorageFolder>> GetFolders(List<string> folder_name)
        {
            StorageFolder root_folder = null;

            try
            {
                root_folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                return await GetFolders(root_folder, folder_name);
            }
            catch
            {

            }
            return null;

        }

       

    }
}
