using Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Restaurant.Converters
{
    class TicketConvert: IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (String.IsNullOrEmpty(values[0].ToString())
                || String.IsNullOrEmpty(values[1].ToString())
                || String.IsNullOrEmpty(values[2].ToString())
                || String.IsNullOrEmpty(values[3].ToString())
                || values[4] is null
                || String.IsNullOrEmpty(values[4].ToString())
                )
            {
                return null;
            }
            else
            {
                return new TicketVM()
                {
                    Id=Int32.Parse(values[0].ToString()),
                    IdUser=Int32.Parse(values[1].ToString()),
                    IdTable= Int32.Parse(values[2].ToString()),
                    TotalPrice = Decimal.Parse(values[3].ToString()),
                    IdType =Int32.Parse(values[4].ToString())
                };
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            TicketVM item = value as TicketVM;
            object[] result = new object[6] { 
                item.Id,
                item.TotalPrice,
                item.EmissionDate,
                item.IdUser,
                item.IdTable,
                item.IdType 
            };
            return result;
        }
    }
}
