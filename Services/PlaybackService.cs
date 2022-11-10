using Microsoft.Toolkit.Mvvm.Messaging;
using PureRadio.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Media;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Media.Streaming.Adaptive;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media;
using Windows.Web.Http;

namespace PureRadio.Services
{
    /// <summary>
    /// The central authority on playback in the application
    /// providing access to the player and active playlist.
    /// </summary>
    public class PlaybackService
    {
        static PlaybackService instance;

        public static PlaybackService Instance
        {
            get
            {
                if (instance == null)
                    instance = new PlaybackService();

                return instance;
            }
        }

        /// <summary>
        /// This application only requires a single shared MediaPlayer
        /// that all pages have access to. The instance could have 
        /// also been stored in Application.Resources or in an 
        /// application defined data model.
        /// </summary>
        public MediaPlayer Player { get; set; }
        public SystemMediaTransportControls TransportControls { get; set; }
        public MediaPlaybackItem PlaybackItem { get; set; }
        public MediaPlaybackList PlaybackList { get; set; }
        public MediaSource Source { get; set; }
        public AdaptiveMediaSource AdaptiveSource { get; set; }
        public HttpClient CustomHttpClient { get; set; } = new HttpClient();

        /// <summary>
        /// The data model of the active playlist. An application might
        /// have a database of items representing a user's media library,
        /// but here we use a simple list loaded from a JSON asset.
        /// </summary>
        // public MediaList CurrentPlaylist { get; set; }

        public PlaybackService()
        {
            // Create the player instance
            Player = new MediaPlayer();
            Player.AutoPlay = false;
            CustomHttpClient.DefaultRequestHeaders.TryAppendWithoutValidation("sec-fetch-dest", "audio");
            CustomHttpClient.DefaultRequestHeaders.TryAppendWithoutValidation("sec-fetch-mode", "no-cors");
            CustomHttpClient.DefaultRequestHeaders.TryAppendWithoutValidation("sec-fetch-site", "cross-site");
        }

        public async void InitializeAdaptiveMediaSourceAsync(Uri uri, string cover, string radio_content, string radio_channel)
        {
            AdaptiveMediaSourceCreationResult result = await AdaptiveMediaSource.CreateFromUriAsync(uri, CustomHttpClient);
            if (result.Status == AdaptiveMediaSourceCreationStatus.Success)
            {
                if (Player.PlaybackSession.PlaybackState != MediaPlaybackState.None) Player.Pause();
                AdaptiveSource = result.MediaSource;
                Source = MediaSource.CreateFromAdaptiveMediaSource(AdaptiveSource);
                PlaybackItem = new MediaPlaybackItem(Source);
                Player.Source = PlaybackItem;
                Player.Play();
                //WeakReferenceMessenger.Default.Send(new UpdateDisplayPropertiesMessage(1));
                UpdateProperties(cover, radio_content, radio_channel);
                //Debug.Print(props.MusicProperties.Title);

                AdaptiveSource.InitialBitrate = AdaptiveSource.AvailableBitrates.Max<uint>();

                //Register for download requests
                //ams.DownloadRequested += DownloadRequested;

                //Register for download failure and completion events
                //ams.DownloadCompleted += DownloadCompleted;
                //ams.DownloadFailed += DownloadFailed;

                //Register for bitrate change events
                //ams.DownloadBitrateChanged += DownloadBitrateChanged;
                //ams.PlaybackBitrateChanged += PlaybackBitrateChanged;

                //Register for diagnostic event
                //ams.Diagnostics.DiagnosticAvailable += DiagnosticAvailable;
            }
            else
            {
                Debug.Print("InitializeAdaptiveMediaSource Failed");
                // Handle failure to create the adaptive media source
                //MyLogMessageFunction($"Adaptive source creation failed: {uri} - {result.ExtendedError}");
            }
        }

        public void UpdateProperties(string cover, string radio_content, string radio_channel)
        {
            MediaItemDisplayProperties props = PlaybackItem.GetDisplayProperties();
            props.Type = Windows.Media.MediaPlaybackType.Music;
            props.MusicProperties.Title = radio_channel;
            props.MusicProperties.Artist = radio_content;
            props.Thumbnail = RandomAccessStreamReference.CreateFromUri(new Uri(cover));
            PlaybackItem.ApplyDisplayProperties(props);
            /*
            // Get the updater.
            SystemMediaTransportControlsDisplayUpdater updater = SystemMediaTransportControls.GetForCurrentView().DisplayUpdater;

            // Music metadata.
            updater.Type = MediaPlaybackType.Music;
            updater.MusicProperties.Title = radio_content;
            updater.MusicProperties.Artist = radio_channel;            

            // Set the album art thumbnail.
            // RandomAccessStreamReference is defined in Windows.Storage.Streams
            updater.Thumbnail = RandomAccessStreamReference.CreateFromUri(new Uri(cover));

            // Update the system media transport controls.
            updater.Update();
            */
        }



    }


}
