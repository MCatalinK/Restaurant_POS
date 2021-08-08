using Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Restaurant.Converters
{
    class WaiterUpdateTableConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] is null
                || String.IsNullOrEmpty(values[1].ToString()))
            {
                return null;
            }
            else
            {
                var table = values[0] as TableVM;
                return new TableVM()
                {
                    Id = (int)table.Id,
                    NoOfSeats = (int)table.NoOfSeats,
                    NoOfSeatsOccupied = Int32.Parse(values[1].ToString()),
                    UserId = (int)table.UserId
                };
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            TableVM table = value as TableVM;
            object[] result = new object[3] { table.NoOfSeats, table.NoOfSeatsOccupied, table.UserId};
            return result;
        }
    }
}
