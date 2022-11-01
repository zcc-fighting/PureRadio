using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace PureRadio.Common
{
    public class MediaPlaybackStateToButtonIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is MediaPlaybackState)
            {
                var state = (MediaPlaybackState)value;
                var fontIcon = new FontIcon();
                fontIcon.FontFamily = new FontFamily("Segoe Fluent Icons");

                if (state == MediaPlaybackState.Playing)
                {
                    
                    fontIcon.Glyph = "\xF8AE";
                    return fontIcon;
                }
                else
                {
                    fontIcon.Glyph = "\xF5B0";
                    return fontIcon;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
