using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;
using System.Net;
using System.Web;
using Windows.Web.Http;



namespace LocalRadioManage.StorageOperate 
{
    public static partial class MyFile
    {

        public class CreateFileProgress
        {
            public class Progress {
                public ulong file_size = 0;
                public ulong progress_size = 0;
                public string file_name = "";
                public StorageFile store_file=null;
                public bool is_end = false;

               public Progress()
                {

                }
                public Progress(ulong _file_size,ulong _progress_size,string _file_name,StorageFile _file)
                {
                    file_size = _file_size;
                    progress_size = _progress_size;
                    file_name = _file_name;
                    store_file = _file;
                }
            }
           public Progress progress = new Progress();

           public CreateFileProgress()
            {

            }

            public  async Task<StorageFile> CreateFile(StorageFolder root_folder, Uri uri)
            {
                StorageFile temp_file = null;
                StorageFile store_file = null;
                string file_name = "";

                try
                {
                    file_name = HttpUtility.UrlDecode(uri.Segments.Last());
                    store_file = await MyFile.GetFile(root_folder, file_name);
                    if (store_file != null)
                    {
                        return store_file;
                    }

                    temp_file = await StorageFile.CreateStreamedFileFromUriAsync(file_name, uri, null);
                    //获取文件大小
                    Windows.Storage.FileProperties.BasicProperties basic = await temp_file.GetBasicPropertiesAsync();
                    store_file = await MyFile.CreateFile(root_folder, file_name);
                    lock (progress)
                    {
                        progress.store_file = store_file;
                        progress.file_size = basic.Size;
                    }
                    await CopyFile(temp_file, store_file);
                  
                   if(await CheckDeleteFile(progress)){
                        return null;
                    }
                    store_file = await MyFile.GetFile(root_folder, file_name);
                    return store_file;
                }
                catch
                {
                    await CheckDeleteFile(progress);
                    return null;
                }
            }
            private  async Task<bool> CopyFile(StorageFile source, StorageFile destination)
            {
                try
                {
                    ulong begin = 0;
                    byte[] buffer = new byte[16 * 1024 * 1024];
                    int true_size = 0;
                    Stream read = await source.OpenStreamForReadAsync();
                    Stream write = await destination.OpenStreamForWriteAsync();

                    while ((true_size = read.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        begin += (uint)true_size;
                        lock (progress)
                        {
                            progress.progress_size = begin;
                        }
                        write.Write(buffer, 0, true_size);
                    }
                    read.Close();
                    write.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            private async Task<bool> CheckDeleteFile(Progress temp)
            {
                try
                {
                    lock (temp)
                    {
                        temp.is_end = true;
                        if (temp.file_size == temp.progress_size&&temp.file_size!=0)
                        {
                            return false;
                        }
                    }
                    await temp.store_file.DeleteAsync();
                    return true;
                   
                   
                    
                }
                catch
                {
                    return false;
                }
            }

        }

       
    }

}

