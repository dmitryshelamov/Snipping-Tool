using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace SnippingTool.View.Converters
{
    public class HotKeyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Key k = value is Key ? (Key)value : Key.None;
            KeyConverter keyConverter = new KeyConverter();
            var result = keyConverter.ConvertToString(k);
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringKey = value as string;
            if (stringKey == null)
                return Key.None;
            KeyConverter keyConverter = new KeyConverter();
            Key result = (Key)keyConverter.ConvertFromString(stringKey);
            return result;
        }
    }
}
