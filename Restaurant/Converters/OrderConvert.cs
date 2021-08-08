using Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Restaurant.Converters
{
    class OrderConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (String.IsNullOrEmpty(values[0].ToString())
                || values[1] is null
                || String.IsNullOrEmpty(values[1].ToString()))
            {
                return null;
            }
            else
            {
                var IdTicket = Int32.Parse(values[0].ToString());
                var IdItem = (int)values[1];
                return new OrderVM()
                {
                    IdTicket = Int32.Parse(values[0].ToString()),
                    IdItem = (int)values[1]
                };
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            OrderVM item = value as OrderVM;
            object[] result = new object[2] {item.IdTicket,item.IdItem };
            return result;
        }
    }
}
