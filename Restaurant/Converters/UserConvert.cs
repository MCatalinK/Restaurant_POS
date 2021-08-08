using Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Restaurant.Converters
{
    class UserConvert:IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (String.IsNullOrEmpty(values[0].ToString()) is false 
                && String.IsNullOrEmpty(values[1].ToString()) is false 
                && String.IsNullOrEmpty(values[2].ToString()) is false 
                && String.IsNullOrEmpty(values[3].ToString()) is false)
            {
                return new UserVM()
                {
                    Name = values[0].ToString(),
                    Username = values[1].ToString(),
                    Password = values[2].ToString(),
                    IdRole = (int)values[3]
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
