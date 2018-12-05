using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Ty.Component.TaskManager
{
    /// <summary>
    /// 所有制都相等
    /// </summary>
    public class TrueForAllEqulsConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return null;

            return values.ToList().TrueForAll(l => l == values.First());
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 相加中间用parameter间隔
    /// </summary>
    public class AppendToStringConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return null;

            var sss = values.ToList();

            if (values.Where(l => l == null).Count() > 0) return parameter.ToString();

            return values.ToList().Aggregate((l, k) => l.ToString() + parameter.ToString() + k.ToString());
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 相加中间用parameter间隔
    /// </summary>
    public class ConvertPoleFromSiteConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return null;

            if (values.Length != 2) return null;

            Site start = values[0] as Site;
            Site end = values[1] as Site;

            if (start == null || end == null) return null;

            if (start.ID == end.ID) return start.Poles;

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 相加中间用parameter间隔
    /// </summary>
    public class IsEqualValueConverter : IValueConverter
    {

        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return null;

            if (parameter == null) return null;



            return values.ToString() == parameter.ToString();
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return parameter;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
