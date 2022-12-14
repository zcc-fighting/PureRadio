using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using PureRadio.LocalManage.DataModelsL;
using PureRadio.Uwp.Adapters.Interfaces;
using PureRadio.Uwp.Models.Data.Constants;
using PureRadio.Uwp.Models.Data.Content;
using PureRadio.Uwp.Models.Data.Radio;
using PureRadio.Uwp.Models.Enums;
using PureRadio.Uwp.Models.Local;
using PureRadio.Uwp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace PureRadio.Uwp.Adapters
{
    public class PlayerAdapter : IPlayerAdapter
    {
        public MediaPlaybackItem ConvertToMediaPlaybackItem(PlayItemSnapshot item)
        {
            var mediaPlaybackItem = new MediaPlaybackItem(MediaSource.CreateFromUri(item.SourceUri));
            var props = mediaPlaybackItem.GetDisplayProperties();
            props.Type = Windows.Media.MediaPlaybackType.Music;
            props.MusicProperties.Title = item.Title;
            props.MusicProperties.Artist = item.SubTitle;
            props.Thumbnail = Windows.Storage.Streams.RandomAccessStreamReference.CreateFromUri(item.Cover);
            mediaPlaybackItem.ApplyDisplayProperties(props);
            return mediaPlaybackItem;
        }

        public PlayItemSnapshot ConvertToPlayItemSnapshot(RadioInfoDetail item)
        {
            Uri sourceUri = new(string.Format(ApiConstants.Radio.Live, item.RadioId));
            int duration = 0;
            if(TimeSpan.TryParse(item.StartTIme, out TimeSpan startSpan) && TimeSpan.TryParse(item.EndTime, out TimeSpan endSpan))
            {
                duration = (int)(endSpan - startSpan).TotalSeconds;
                if (item.EndTime == "23:59:00") duration += 60;
            }
            return new PlayItemSnapshot(
                MediaPlayType.RadioLive, sourceUri, item.Cover, item.Nowplaying, item.Title, item.RadioId, 0, duration, item.StartTIme, item.EndTime);
        }

        public PlayItemSnapshot ConvertToPlayItemSnapshot(ContentInfoDetail item)
        {
            return new PlayItemSnapshot(
                MediaPlayType.ContentDemand, null, item.Cover, item.Title, item.Title, item.ContentId, 0, 0);
        }

        private PlayItemSnapshot ConvertToPlayItemSnapshot(RadioPlaylistDetail item, Uri cover, string radioTitle)
        {
            int today = (int)DateTime.Today.DayOfWeek + 1;
            if (item.Day - today > 0) today += 7;
            int offset = item.Day - today;
            DayOfWeek dayOfWeek = (DayOfWeek)(item.Day - 1);
            string targetDay = DateTime.Today.AddDays(offset).ToString("yyyyMMdd");
            Uri sourceUri = new(String.Format(ApiConstants.Radio.OnDemand, targetDay, item.RadioId, item.RadioId, targetDay, item.StartTime.Replace(":", string.Empty), item.EndTime.Replace(":", string.Empty)));
            return new PlayItemSnapshot(
                MediaPlayType.RadioDemand, sourceUri, cover, item.Title, radioTitle, item.RadioId, item.ProgramId, item.Duration, item.StartTime, item.EndTime, dayOfWeek);
        }
        private PlayItemSnapshot ConvertToLocalPlayItemSnapshot(ChannalRadioInfo item, Uri cover, string radioTitle)
        {
            int duration = 0;
            if (TimeSpan.TryParse(item.StartTime, out TimeSpan startSpan) && TimeSpan.TryParse(item.EndTime, out TimeSpan endSpan))
            {
                duration = (int)(endSpan - startSpan).TotalSeconds;
                if (item.EndTime == "23:59:00") duration += 60;
            }
            string targetDay = item.Date.ToString("yyyyMMdd");
            Uri sourceUri = new(String.Format(ApiConstants.Radio.OnLocalDemand, item.RadioId, item.RadioId, targetDay, item.StartTime.Replace(":", string.Empty), item.EndTime.Replace(":", string.Empty)));
            return new PlayItemSnapshot(
                MediaPlayType.RadioDemand, sourceUri, cover, item.Title, radioTitle, item.RadioId, item.ProgramId, duration, item.StartTime, item.EndTime, DayOfWeek.Sunday);
        }
        public List<PlayItemSnapshot> ConvertToPlayItemSnapshotList(RadioInfoDetail detail, List<RadioPlaylistDetail> playlist)
        {
            return playlist.Select(p => ConvertToPlayItemSnapshot(p, detail.Cover, detail.Title)).ToList();
        }

        public List<PlayItemSnapshot> ConvertToLocalPlayItemSnapshotList(RadioInfoDetail detail, List<ChannalRadioInfo> playlist)
        {
            return playlist.Select(p => ConvertToLocalPlayItemSnapshot(p, detail.Cover, detail.Title)).ToList();
        }

        private PlayItemSnapshot ConvertToPlayItemSnapshot(ContentPlaylistDetail item, int contentId, string contentTitle)
        {
            return new PlayItemSnapshot(
                MediaPlayType.ContentDemand, null, item.Cover, item.Title, contentTitle, contentId, item.ProgramId, item.Duration);
        }
        private PlayItemSnapshot ConvertToLocalPlayItemSnapshot(AlbumRadioInfo item, Uri cover, int contentId, string contentTitle)
        {
            int duration = item.Duration;
            //Uri sourceUri = new(String.Format(ApiConstants.Radio.OnLocalDemand, item.RadioId, item.RadioId, targetDay, item.StartTime.Replace(":", string.Empty), item.EndTime.Replace(":", string.Empty)));

            return new PlayItemSnapshot(
                MediaPlayType.ContentDemand, null, cover, item.Title, contentTitle, contentId, item.ProgramId, item.Duration);
        }

        public List<PlayItemSnapshot> ConvertToPlayItemSnapshotList(ContentInfoDetail detail, List<ContentPlaylistDetail> playlist)
        {
            return playlist.Select(p => ConvertToPlayItemSnapshot(p, detail.ContentId, detail.Title)).ToList();
        }
        public List<PlayItemSnapshot> ConvertToLocalPlayItemSnapshotList(ContentInfoDetail detail, List<AlbumRadioInfo> playlist)
        {
            return playlist.Select(p => ConvertToLocalPlayItemSnapshot(p,detail.Cover, detail.ContentId, detail.Title)).ToList();
        }
    }
}
