using Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Restaurant.Converters
{
    class WUpdateConvert: IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] is null
                || String.IsNullOrEmpty(values[1].ToString()))
            
                return null;
            else
            {
                var ticket = values[0] as TicketVM;
                return new TicketVM()
                {
                    Id = (int)ticket.Id,
                    EmissionDate = (DateTime)ticket.EmissionDate,
                    IdUser = ticket.IdUser,
                    IdTable = ticket.IdTable,
                    IdType = Int32.Parse(values[1].ToString())
                };
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            TicketVM item = value as TicketVM;
            object[] result = new object[5] {
                item.Id,
                item.TotalPrice,
                item.IdUser,
                item.IdTable,
                item.IdType
            };
            return result;
        }
    }
}
