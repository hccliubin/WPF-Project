using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LTO.Base.Frame.MVVM
{
    /// <summary> 匹配字符串 </summary>
    [ValueConversion(typeof(string), typeof(bool))]
    public class IsEqualStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (value == null) return false;

            if (parameter == null) return false;

            if (value.ToString() != parameter.ToString()) return false;

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            bool b = (bool)value;

            return b ? parameter : null;
        }
    }

    public sealed class XConverter
    {
        public static IsEqualStringConverter IsEqualStringConverter
        {
            get { return new IsEqualStringConverter(); }
        } 
    }
}
