using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Bioskop.Converters
{
    public class SaleBoolConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2)
                return null;

            Dictionary<string, bool> saleBool = values[0] as Dictionary<string, bool>;
            KeyValuePair<string, bool> naziv = (KeyValuePair<string, bool>)values[1];
            return saleBool[naziv.Key];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        
    }
}
