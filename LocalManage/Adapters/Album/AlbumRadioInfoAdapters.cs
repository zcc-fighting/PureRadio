using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureRadio.Uwp.Models.Data.Content;
using PureRadio.LocalRadioManage.DataModelsL;
using PureRadio.Uwp.Models.Data.Constants;
using PureRadio.Uwp.Providers.Interfaces;
using PureRadio.Uwp.Providers;
using PureRadio.Uwp.Models.Enums;

namespace PureRadio.LocalManage.Adapters
{
    class AlbumRadioInfoAdapters
    {
        private static readonly IAccountProvider _accountProvider;
        public static  ContentPlaylistDetail  ToContentPlaylistDetail(AlbumRadioInfo radio)
        {

          
            ContentPlaylistDetail detail = new ContentPlaylistDetail
                (
                radio.Version,
                radio.ProgramId,
                radio.Title,
                0,
                radio.UpdateTime,
                0,
                false,
                null,
                0,
                ""
                );
            return detail;
        }
     public static async Task<AlbumRadioInfo> ToAlbumRadioInfo(int ContentId,ContentPlaylistDetail detail)
        {
            AlbumRadioInfo info = new AlbumRadioInfo();
            info.Version = detail.Version;
            info.ProgramId = detail.ProgramId;
            info.Title = detail.Title;
            info.UpdateTime = detail.UpdateTime;
            info.AlbumId = ContentId;
          
            string url = string.Format(ApiConstants.Content.Play, ContentId, detail.ProgramId);
            var query = await _accountProvider.GenerateAuthorizedQueryStringAsync(url, null, RequestType.PlayContent, true);
            url += $"?{query}";
            info.RemoteUri = new Uri(url);
            return info;
        }


    }
}
