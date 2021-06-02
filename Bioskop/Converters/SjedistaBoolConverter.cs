using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Bioskop.Converters
{
    public class SjedistaBoolConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2)
                return null;

            Dictionary<int, bool> sjedisteBool = values[0] as Dictionary<int, bool>;
            KeyValuePair<int, bool> broj = (KeyValuePair<int, bool>)values[1];
            return sjedisteBool[broj.Key];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}
