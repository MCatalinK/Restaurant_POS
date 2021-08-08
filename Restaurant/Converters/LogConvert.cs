using Restaurant.ViewModels;
using System;
using System.Windows.Data;

namespace Restaurant.Converters
{
    class LogConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] != null && values[1] != null)
            {
                return new UserVM()
                {
                    Username = values[0].ToString(),
                    Password = values[1].ToString(),
                    IdRole = 0
                };
            }
            else
            {
                return null;
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            UserVM user = value as UserVM;
            object[] result = new object[4] {user.Name,user.Username,user.Password,user.IdRole };
            return result;
        }
    }
}
