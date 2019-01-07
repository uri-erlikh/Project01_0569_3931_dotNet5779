using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLWPF
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    namespace ValueConverterDemo
    {
        public class ItemToIsEnabledConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                bool boolValue = (bool)value;
                if (boolValue)
                {
                    return null;
                }
                else
                {
                    return Visibility.Visible;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}