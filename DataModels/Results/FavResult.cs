using Newtonsoft.Json;
using PureRadio.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace PureRadio.DataModel.Results
{
    public class FavResult
    {
        /// <summary>
        /// 收藏电台
        /// </summary>
        public List<FavItem> favRadio { get; set; }
        /// <summary>
        /// 收藏节目
        /// </summary>
        public List<FavItem> favProgram { get; set; }

        public FavResult(List<FavItem> favRadio, List<FavItem> favProgram)
        {
            this.favRadio = favRadio;
            this.favProgram = favProgram;
        }

        /// <summary>
        /// 获取云端收藏
        /// </summary>
        public static List<FavItem> GetFav(string qingting_id, string access_token)
        {
            string requestBody = $"qingting_id={qingting_id}&access_token={access_token}";
            string resultJson = HttpRequest.SendGet("https://webbff.qingting.fm/www/favchannel", requestBody);
            FavResult result = JsonConvert.DeserializeObject<FavResult>(resultJson);
            List<FavItem> favCloud = new List<FavItem>();
            foreach (var radio in result.favRadio)
            {
                radio.isRadio = true;
                favCloud.Add(radio);
            }
            foreach(var program in result.favProgram)
            {
                program.isRadio = false;
                favCloud.Add(program);
            }
            return favCloud;
        }

        /// <summary>
        /// 获取本地收藏
        /// </summary>
        public static async Task<List<FavItem>> GetFav()
        {
            List<FavItem> favLocal = new List<FavItem>();
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile FavFile = (StorageFile)await storageFolder.TryGetItemAsync("FavResult.json");
            if (FavFile == null) return favLocal;
            string text = await FileIO.ReadTextAsync(FavFile);
            List<FavItem> result = JsonConvert.DeserializeObject<List<FavItem>>(text);
            if (result == null) return favLocal;
            favLocal = result;
            return favLocal;
        }

        public static async void SaveFav(List<FavItem> favItems)
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile FavFile = await storageFolder.CreateFileAsync("FavResult.json", Windows.Storage.CreationCollisionOption.ReplaceExisting);
            //if (FavFile == null) FavFile = (StorageFile)await storageFolder.CreateFileAsync("FavResult.json");
            string text = JsonConvert.SerializeObject(favItems);
            await Windows.Storage.FileIO.WriteTextAsync(FavFile, text);
        }

    }

}
