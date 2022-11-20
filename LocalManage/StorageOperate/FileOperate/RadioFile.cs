using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using Windows.Storage;
using PureRadio.LocalRadioManage.DataModelsL;
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

        public static async Task<bool> CreateRadioFile_Channal(StorageFolder root_folder, StorageFile radio_file, StorageFile image_file, ChannalRadioInfo radio, ChannalCardInfo channal)
        {

            StorageFile exception_mp3 = null;
            StorageFile mp3 = null;

            try
            {
                if (image_file == null)
                {
                    image_file = await MyFile.CreateFile("default.jepg");
                }

                //创建新文件转码为MP3文件
                string new_radio_name = radio.Title + ".mp3";
                await MyFile.DeleteFile(new_radio_name);
                mp3 = await MyFile.CreateFile(new_radio_name);
                exception_mp3 = mp3;
                MediaTranscoder transcoder = new MediaTranscoder();
                var p = MediaEncodingProfile.CreateMp3(AudioEncodingQuality.High);

                try
                {
                    if (radio_file == null)
                    {
                        return false;
                    }
                    var preparedTranscodeResult = await transcoder.PrepareFileTranscodeAsync(radio_file, mp3, p);
                    //var progress = new Progress<double>();
                    await preparedTranscodeResult.TranscodeAsync();
                }
                catch
                {
                    return false;
                }
                TagLib.File new_radio_file = TagLib.File.Create(mp3.Path);
                new_radio_file.Tag.Album = channal.Title;
                new_radio_file.Tag.AlbumArtists = new[] { channal.Title };
                new_radio_file.Tag.Title = radio.Title;
                new_radio_file.Tag.Description = channal.Description;
                new_radio_file.Tag.Copyright = "QingTingFM";
                new_radio_file.Tag.Subtitle = radio.StartTime + "-" + radio.EndTime;
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
                musicProperties.Title = radio.Title;
                musicProperties.Publisher = channal.Title;
                musicProperties.Album = channal.Title;
                musicProperties.AlbumArtist = radio.Broadcasters;
                musicProperties.Subtitle = radio.RadioId.ToString();
                await musicProperties.SavePropertiesAsync();

                await mp3.MoveAsync(root_folder);

                return true;
            }
            catch
            {

                if (exception_mp3 != null)
                {
                    await exception_mp3.DeleteAsync();
                }
                return false;
            }
        }

        public static async Task<bool> CreateRadioFile_Album(StorageFolder root_folder, StorageFile radio_file, StorageFile image_file, AlbumRadioInfo radio, AlbumCardInfo album)
        {

            StorageFile exception_mp3 = null;
            StorageFile mp3 = null;

            try
            {
                if (image_file == null)
                {
                    image_file = await MyFile.CreateFile("default.jepg");
                }



                //创建新文件转码为MP3文件
                string new_radio_name = radio.Title + ".mp3";
                mp3 = await MyFile.CreateFile(new_radio_name);
                exception_mp3 = mp3;
                MediaTranscoder transcoder = new MediaTranscoder();
                var p = MediaEncodingProfile.CreateMp3(AudioEncodingQuality.High);

                try
                {
                    if (radio_file == null)
                    {
                        return false;
                    }
                    var preparedTranscodeResult = await transcoder.PrepareFileTranscodeAsync(radio_file, mp3, p);
                    //var progress = new Progress<double>();
                    await preparedTranscodeResult.TranscodeAsync();
                }
                catch
                {
                    return false;
                }
                TagLib.File new_radio_file = TagLib.File.Create(mp3.Path);
                new_radio_file.Tag.Album = album.Title;
                new_radio_file.Tag.AlbumArtists = new[] { album.Title };
                new_radio_file.Tag.Title = radio.Title;
                new_radio_file.Tag.Description = album.Description;
                new_radio_file.Tag.Copyright = "QingTingFM";
                new_radio_file.Tag.Subtitle = radio.Version;
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
                musicProperties.Title = radio.Title;
                musicProperties.Publisher = album.Title;
                musicProperties.Album = album.Title;
                musicProperties.AlbumArtist = album.Podcasters;
                musicProperties.Subtitle = album.Title;
                await musicProperties.SavePropertiesAsync();

                await mp3.MoveAsync(root_folder);

                return true;
            }
            catch
            {

                if (exception_mp3 != null)
                {
                    await exception_mp3.DeleteAsync();
                }
                return false;
            }
        }
    } 
}

