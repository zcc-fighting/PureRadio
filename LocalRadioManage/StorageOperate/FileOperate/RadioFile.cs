using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using Windows.Storage;
using DataModels;
using TagLib;
using System.IO;
using Windows.Media;
using Windows.Media.Transcoding;
using Windows.Media.MediaProperties;
///using Windows.Media.

namespace LocalRadioManage.StorageOperate
{
    public static partial class MyFile
    {

        public static async Task<bool> CreateRadioFile(StorageFolder root_folder, StorageFile radio_file, StorageFile image_file, RadioFullContent radio, RadioFullAlbum album)
        {
            StorageFile exception_mp3 = null;
            try
            {
                //创建新文件转码为MP3文件
                string new_radio_name = radio.title + ".mp3";
                StorageFile mp3 = await MyFile.CreateFile(new_radio_name);
                exception_mp3 = mp3;
                MediaTranscoder transcoder = new MediaTranscoder();
                var p = MediaEncodingProfile.CreateMp3(AudioEncodingQuality.High);

                try
                {
                    var preparedTranscodeResult = await transcoder.PrepareFileTranscodeAsync(radio_file, mp3, p);
                    var progress = new Progress<double>();
                    await preparedTranscodeResult.TranscodeAsync().AsTask(progress);
                }
                catch 
                {
                    return false;
                }

              
                TagLib.File new_radio_file =  TagLib.File.Create(mp3.Path);
               
                new_radio_file.Tag.Album = album.title;
                new_radio_file.Tag.AlbumArtists = new[] {album.title };
                new_radio_file.Tag.Title = radio.title;
                new_radio_file.Tag.Description = album.description;
                new_radio_file.Tag.Copyright = "QingTingFM";
                new_radio_file.Tag.Subtitle = radio.start_time + "-" + radio.end_time;

                TagLib.Picture pic = new TagLib.Picture
                {
                    Type = TagLib.PictureType.FrontCover,
                    Description = "Cover",
                    MimeType = System.Net.Mime.MediaTypeNames.Image.Jpeg
                };

                Picture picture = new Picture(image_file.Path);
                new_radio_file.Tag.Pictures = new IPicture[1] { picture };

                new_radio_file.Save();


                Windows.Storage.FileProperties.MusicProperties musicProperties = await mp3.Properties.GetMusicPropertiesAsync();

                musicProperties.Title = radio.title;
                musicProperties.Publisher = radio.title;
                musicProperties.Album = album.title;
                musicProperties.AlbumArtist= album.title;
                musicProperties.Subtitle = radio.channel_id.ToString();
                await  musicProperties.SavePropertiesAsync();

                await  mp3.MoveAsync(root_folder);
                return true;
            }
            catch
            {
                if (exception_mp3 != null)
                {
                  await Task.Run(()=>exception_mp3.DeleteAsync());
                }                
                return false;
            }


        }
       
    }
       
}
