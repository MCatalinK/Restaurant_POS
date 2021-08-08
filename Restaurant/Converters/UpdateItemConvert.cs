using Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Restaurant.Converters
{
    class UpdateItemConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] is null
                ||String.IsNullOrEmpty(values[0].ToString())
                || String.IsNullOrEmpty(values[1].ToString())
                || String.IsNullOrEmpty(values[2].ToString())
                || String.IsNullOrEmpty(values[3].ToString()))
            {
                return null;
            }
            else
            {
                var item = values[0] as MenuItemVM;
                var Name = values[1].ToString();
                 var   Price = Decimal.Parse(values[2].ToString());
                var IsAvailable = bool.Parse(values[3].ToString());
                return new MenuItemVM()
                {
                    Id=(int)item.Id,
                    Name = values[1].ToString(),
                    Price = Decimal.Parse(values[2].ToString()),
                    IsAvailable = bool.Parse(values[3].ToString())
                };
            }

        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            MenuItemVM item = value as MenuItemVM;
            object[] result = new object[3] { item.Name, item.Price, item.IsAvailable };
            return result;
        }
    }
}
