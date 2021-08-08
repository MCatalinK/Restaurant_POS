using Restaurant.ViewModels;
using System;
using System.Windows.Data;

namespace Restaurant.Converters
{
    class TableConvert:IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (String.IsNullOrEmpty(values[0].ToString()) 
                || String.IsNullOrEmpty(values[1].ToString()))
            {
                return null;
            }
            else { 
                return new TableVM()
                {
                   NoOfSeats= Int32.Parse(values[0].ToString()),
                   NoOfSeatsOccupied = 0,
                   UserId = Int32.Parse(values[1].ToString())
                };
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            TableVM table = value as TableVM;
            object[] result = new object[3] { table.NoOfSeats,table.NoOfSeatsOccupied,table.UserId};
            return result;
        }
    }
}
