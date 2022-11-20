using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using LocalRadioManage.StorageOperate;

namespace PureRadio.LocalManage.LocalService.Storage
{
    class RadioStorage
    {
        const string DefaultRadioFolder = "PureRadioLocalRadio";
        public static StorageFolder RadioFolder = null;
        public static readonly StorageFolder LocalMusicLibrary = KnownFolders.MusicLibrary;

        public RadioStorage()
        {
            Task<Task<bool>> task = new Task<Task<bool>>(() => Init());
            task.Start();
            task.Result.Wait();
        }

        private async Task<bool> Init()
        {
            RadioFolder = await MyFolder.CreateFolder(DefaultRadioFolder);
            return (RadioFolder != null);
        }

        public async Task<StorageFile> DownRadio(Uri first_uri, Uri second_uri)
        {
            return await DownRadio(DefaultRadioFolder, first_uri, second_uri);
        }
        public async Task<StorageFile> DownRadio(string RadioFolderName, Uri first_uri, Uri second_uri)
        {
            StorageFolder root_folder = await MyFolder.CreateFolder(RadioFolderName);
            StorageFile file = await MyFile.CreateFile(root_folder, first_uri);
            if (file != null)
            {
                return file;
            }
            file = await MyFile.CreateFile(root_folder, second_uri);
            return file;
        }

        public async Task<bool> RemoveRadio(string RadioFolderNmae, Uri RadioName)
        {
            StorageFolder root_folder = await MyFolder.CreateFolder(RadioFolderNmae);
            return await MyFile.DeleteFile(root_folder, RadioName);
        }
        public async Task<bool> RemoveRadio(Uri RadioName)
        {
            return await MyFile.DeleteFile(RadioFolder, RadioName);
        }
    }
}
