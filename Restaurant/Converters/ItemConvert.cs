using Restaurant.ViewModels;
using System;
using System.Windows.Data;

namespace Restaurant.Converters
{
    class ItemConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (String.IsNullOrEmpty(values[0].ToString()) 
                || String.IsNullOrEmpty(values[1].ToString())
                || String.IsNullOrEmpty(values[2].ToString()))
            {
                return null;
            }
            else
            {
                return new MenuItemVM()
                {
                    Name = values[0].ToString(),
                    Price = Decimal.Parse(values[1].ToString()),
                    IsAvailable = bool.Parse(values[2].ToString())
                };
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            MenuItemVM item = value as MenuItemVM;
            object[] result = new object[3] { item.Name,item.Price,item.IsAvailable};
            return result;
        }
    }
}
