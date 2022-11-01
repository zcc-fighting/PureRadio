using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace PureRadio.Common
{
    public class ThemeDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ElementTheme theme = (ElementTheme)value;
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            if (theme == ElementTheme.Light) return resourceLoader.GetString("SettingsThemeModeLight/Content");
            else if (theme == ElementTheme.Dark) return resourceLoader.GetString("SettingsThemeModeDark/Content");
            else return resourceLoader.GetString("SettingsThemeModeSystem/Content");            
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
