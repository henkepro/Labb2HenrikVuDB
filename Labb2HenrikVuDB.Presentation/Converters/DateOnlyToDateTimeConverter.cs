using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Labb2HenrikVuDB.Presentation.Converters
{
    public class DateOnlyToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is DateOnly dateOnly)
            {
                return (DateTime?)dateOnly.ToDateTime(TimeOnly.MinValue);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is DateTime dateTime)
            {
                return DateOnly.FromDateTime(dateTime);
            }
            return null;
        }
    }
}
