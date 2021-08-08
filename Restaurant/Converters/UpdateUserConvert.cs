using Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Restaurant.Converters
{
    class UpdateUserConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0]!=null
                && String.IsNullOrEmpty(values[1].ToString()) is false
                && String.IsNullOrEmpty(values[2].ToString()) is false
                && String.IsNullOrEmpty(values[3].ToString()) is false
                && String.IsNullOrEmpty(values[4].ToString()) is false)
            {
                var user = values[0] as UserVM;
                return new UserVM()
                {
                    Id=(int)user.Id,
                    Name = values[1].ToString(),
                    Username = values[2].ToString(),
                    Password = values[3].ToString(),
                    IdRole = (int)values[4]
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
            object[] result = new object[5] {user.Id, user.Name, user.Username, user.Password, user.IdRole };
            return result;
        }
    }
}
