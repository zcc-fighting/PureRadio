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
        /// 文件夹创建
        /// </summary>
        public static async Task<StorageFolder> CreateFolder(StorageFolder root_folder,string folder_name)
        {
            StorageFolder folder=null;
            try
            {
                folder =await GetFolder(root_folder, folder_name);
                if (folder != null)
                {
                    return folder;
                }
                folder = await root_folder.CreateFolderAsync(folder_name, CreationCollisionOption.OpenIfExists);
                return folder;
            }
            catch
            {
                return null;
            }
            
        }
        public static async Task<StorageFolder> CreateFolder(string folder_name)
        {
            StorageFolder root_folder = null;
            try
            {
                root_folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                return await CreateFolder(root_folder, folder_name);
            }
            catch
            {
                return null;
            }

        }
        /// <summary>
        /// 隐藏文件夹创建
        /// </summary>
        public static async Task<StorageFolder> CreateFolder(StorageFolder root_folder, string folder_name,bool is_hidden)
        {
           StorageFolder create_folder=null;
            
            try
            {
                create_folder = await root_folder.CreateFolderAsync(folder_name, CreationCollisionOption.OpenIfExists);
                if (is_hidden)
                {
                    File.SetAttributes(create_folder.Path, System.IO.FileAttributes.Hidden);
                }
                return create_folder;
            }
            catch
            {
                return null;
            }
        }
        public static async Task<StorageFolder> CreateFolder(string folder_name, bool is_hidden)
        {
           
            StorageFolder root_folder = null;
            try
            {
                root_folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                return await CreateFolder(root_folder, folder_name, is_hidden);
            }
            catch
            {
                return null;
            }
        }
        
        /// <summary>
        /// 文件夹列表创建
        /// </summary>
        public static async Task<List<StorageFolder>> CreateFolder(List<string> folder_name)
        {
            StorageFolder root_folder=null;

            try
            {
                root_folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                return await CreateFolder(root_folder,folder_name);
            }
            catch
            {
                return null;
            }
          
        }
        public static async Task<List<StorageFolder>> CreateFolder(StorageFolder root_folder,List<string> folder_name)
        {
            List<StorageFolder> folders = new List<StorageFolder>();
            StorageFolder _folder = null;

            try
            {
                foreach (string folder in folder_name)
                {
                   _folder=await root_folder.CreateFolderAsync(folder, CreationCollisionOption.OpenIfExists);
                    folders.Add(_folder);
                }
                return folders;
            }
            catch
            {
                folders.Clear();
                return null;
            }
           
        }
    }
}
