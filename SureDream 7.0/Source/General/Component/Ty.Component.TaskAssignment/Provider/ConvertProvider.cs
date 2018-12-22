using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Ty.Component.TaskAssignment
{
    /// <summary>
    /// 所有值都相等
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

            if (values.Where(l => l == null).Count() > 0 || values.Where(l => l.ToString() == "{DependencyProperty.UnsetValue}").Count() > 0) return "全部";


            return values.ToList().Aggregate((l, k) => l.ToString() + parameter.ToString() + k.ToString());
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 转换杆号可选项删选
    /// </summary>
    public class ConvertPoleFromSiteConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return null;

            //  Message：起始杆号可选项删选
            if (values.Length == 2)
            {
                //  Message：当前站杆号列表
                ObservableCollection<TyeLineEntity> rods = (values[0] as ObservableCollection<TyeLineEntity>);

                //  Message：任务列表哦
                ObservableCollection<TaskViewModel> collection = values[1] as ObservableCollection<TaskViewModel>;

                if (rods == null) return null;

                if (collection == null) return null;

                var result = rods.ToList();

                //  Message：查找当前站的所有任务信息
                var finds = collection.ToList();


                if (finds == null) return result;

                foreach (var item in finds)
                {
                    var startIndex = result.FindIndex(l => l.ID == item.StartLine.ID);

                    var endIndex = result.FindIndex(l => l.ID == item.EndLine.ID);

                    if (startIndex < 0 || endIndex < 0) continue;

                    if (startIndex == endIndex)
                    {
                        result.RemoveAt(startIndex);
                    }
                    else
                    {
                        result.RemoveRange(startIndex, endIndex - startIndex + 1);
                    }
                }

                return result;
            }

            //  Message：结束杆号可选项删选
            else if (values.Length == 3)
            {
                ////  Message：起始站
                //TyeBaseSiteEntity start = values[0] as TyeBaseSiteEntity;

                ////  Message：结束站
                //TyeBaseSiteEntity end = values[1] as TyeBaseSiteEntity;

                //  Message：上一个杆号
                TyeLineEntity lastselect = values[0] as TyeLineEntity;

                List<TyeLineEntity> lastList = (values[1] as List<TyeLineEntity>);

                ObservableCollection<TyeLineEntity> allLine = (values[2] as ObservableCollection<TyeLineEntity>);


                if (lastselect == null || lastList == null || allLine == null) return null;

                var result = lastList.ToList();

                int index = result.FindIndex(l => l.ID == lastselect.ID);

                //  Message：从当前选择的开始截取
                result = result.Skip(index).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    if (i == 0) continue;

                    int lastIndex = allLine.ToList().FindIndex(l => l.ID == result[i - 1].ID);

                    int currentIndex = allLine.ToList().FindIndex(l => l.ID == result[i].ID);

                    if ((currentIndex - lastIndex) > 1)
                    {
                        result = result.Take(result.FindIndex(l => l.ID == result[i - 1].ID) + 1).ToList();
                        break;
                    }
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 任务类型切换转换
    /// </summary>
    public class IsEqualValueConverter : IValueConverter
    {

        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return null;

            if (parameter == null) return null;

            bool r = ((int)values).ToString() == parameter.ToString();

            return r;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                TaskTypeEnum taskTypeEnum = (TaskTypeEnum)int.Parse(parameter.ToString());

                return taskTypeEnum;
            }
            else
            {
                return string.Empty;
            }
        }
    }

    /// <summary>
    /// 百分比值转换
    /// </summary>
    public class ProgressToValueConverter : IValueConverter
    {

        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return null;

            return ((double.Parse(values.ToString()) / 10) * 100).ToString() + "%";
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new Exception();
        }
    }

    /// <summary>
    /// 值对应颜色转换
    /// </summary>
    public class ProgressToColorConverter : IValueConverter
    {

        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return null;

            var v = double.Parse(values.ToString());

            BrushConverter brushConverter = new BrushConverter();

            if (v <= 1)
            {
                return (System.Windows.Media.Brush)brushConverter.ConvertFromString("#ea6884");
            }
            else if (v > 1 && v < 9)
            {
                return (System.Windows.Media.Brush)brushConverter.ConvertFromString("#f1b086");
            }
            else
            {
                return (System.Windows.Media.Brush)brushConverter.ConvertFromString("#95cb83");
            }
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new Exception();
        }
    }
}
