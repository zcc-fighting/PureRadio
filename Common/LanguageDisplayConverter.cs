using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace PureRadio.Common
{
    public class LanguageDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string lang = (string)value;
            var resourceLoader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            if (lang == "zh-CN") return resourceLoader.GetString("SettingsLanguageZhCN/Content");
            else if (lang == "en-US") return resourceLoader.GetString("SettingsLanguageEnUS/Content");
            else return resourceLoader.GetString("SettingsLanguageSystem/Content");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
