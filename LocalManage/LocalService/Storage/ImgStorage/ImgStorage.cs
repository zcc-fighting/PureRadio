using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using LocalRadioManage.StorageOperate;

namespace PureRadio.LocalManage.LocalService.Storage
{
    class ImgStorage
    {
        const string DefaultImageFolder = "PureRadioLocalImage";
        public  StorageFolder ImageFolder = null;
        public static readonly StorageFolder LocalImageLibrary = KnownFolders.PicturesLibrary;

       public  ImgStorage()
        {
                Task<Task<bool>> task = new Task<Task<bool>>(() => Init());
                task.Start();
                task.Result.Wait();
        }

        private async Task<bool> Init()
        {
            ImageFolder = await MyFolder.CreateFolder(DefaultImageFolder);
            return (ImageFolder != null);
        }

        public async Task<StorageFile> DownImage(Uri first_uri,Uri second_uri)
        {
          return await DownImage(DefaultImageFolder, first_uri, second_uri);

        }

        public async Task<StorageFile> DownImage(string ImageFolderNmae,Uri first_uri,Uri second_uri)
        {
            StorageFolder root_folder =await MyFolder.CreateFolder(ImageFolderNmae);
            MyFile.CreateFileProgress create = new MyFile.CreateFileProgress();
            StorageFile file =await create.CreateFile(root_folder, first_uri);
            if (file != null)
            {
                return file;
            }
            file = await create.CreateFile(root_folder, first_uri);
            return file;
        }

        public async Task<bool> RemoveImage(string ImageFolderNmae,Uri ImageName)
        {
            StorageFolder root_folder = await MyFolder.CreateFolder(ImageFolderNmae);
            return await MyFile.DeleteFile(root_folder, ImageName);
        }
        public async Task<bool> RemoveImage(Uri ImageName)
        {
            return await MyFile.DeleteFile(ImageFolder, ImageName);
        }

       

    }
}
