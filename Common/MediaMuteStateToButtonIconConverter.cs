using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace PureRadio.Common
{
    public class MediaMuteStateToButtonIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool)
            {
                var muted = (bool)value;
                var fontIcon = new FontIcon();
                fontIcon.FontFamily = new FontFamily("Segoe Fluent Icons");

                if (muted)
                {

                    fontIcon.Glyph = "\xE74F";
                    return fontIcon;
                }
                else
                {
                    fontIcon.Glyph = "\xE767";
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
