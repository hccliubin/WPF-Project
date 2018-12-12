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

            //UnsetValue

            foreach (var item in values)
            {

                Debug.WriteLine(item.ToString());

            }


            if (values.Where(l => l == null).Count() > 0 || values.Where(l => l.ToString() == "{DependencyProperty.UnsetValue}").Count() > 0) return parameter.ToString();


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

            if (values.Length == 3)
            {
                Station start = values[0] as Station;
                Station end = values[1] as Station;

                if (start == null || end == null) return null;

                if (start.ID != end.ID) return null;

                string stationName = start.StationName;

                List<Rod> rods = start.Rods.ToList();

                ObservableCollection<TaskViewModel> collection = values[2] as ObservableCollection<TaskViewModel>;


                //  Message：查找当前站的所有任务信息
                var finds = collection.ToList().FindAll(l => l.StartSite.StationName == stationName && l.EndSite.StationName == stationName);

                if (finds == null) return rods;

                foreach (var item in finds)
                {
                    var startIndex = rods.FindIndex(l => l.RodName == item.StartPole.RodName);
                    var endIndex = rods.FindIndex(l => l.RodName == item.EndPole.RodName);

                    if (startIndex < 0 || endIndex < 0) continue;

                    if (startIndex == endIndex)
                    {
                        rods.RemoveAt(startIndex);
                    }
                    else
                    {
                        rods.RemoveRange(startIndex, endIndex - startIndex);

                    }

                }

                return rods;
            }
            else if (values.Length == 4)
            {
                Station start = values[0] as Station;
                Station end = values[1] as Station;

                if (start == null || end == null) return null;

                if (start.ID != end.ID) return null;

                string stationName = start.StationName;

                Rod r = values[2] as Rod;

                if (r == null) return null;

                List<Rod> rods = start.Rods.ToList();

                ObservableCollection<TaskViewModel> collection = values[3] as ObservableCollection<TaskViewModel>;

                var index = rods.FindIndex(l => l.RodName == r.RodName);

                //  Message：查找当前站的所有任务信息
                var finds = collection.ToList().FindAll(l => l.StartSite.StationName == stationName && l.EndSite.StationName == stationName);

                if (finds == null)
                {
                    return rods.Skip(index);
                }

                rods = rods.Skip(index).ToList();

                foreach (var item in finds)
                {
                    var startIndex = rods.FindIndex(l => l.RodName == item.StartPole.RodName);
                    var endIndex = rods.FindIndex(l => l.RodName == item.EndPole.RodName);

                    if (startIndex < 0 || endIndex < 0) continue;

                    if (startIndex == endIndex)
                    {
                        rods.RemoveAt(startIndex);
                    }
                    else
                    {
                        rods.RemoveRange(startIndex, rods.Count - startIndex);

                    }

                }

                return rods;

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

    /// <summary>
    /// 相加中间用parameter间隔
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
    /// 相加中间用parameter间隔
    /// </summary>
    public class ProgressToColorConverter : IValueConverter
    {

        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return null;

            var v = double.Parse(values.ToString());

            if (v < 3)
            {
                return System.Windows.Media.Brushes.Red;
            }
            else if (v >= 3 && v <= 6)
            {
                return System.Windows.Media.Brushes.Orange;
            }
            else
            {
                return System.Windows.Media.Brushes.Green;
            }
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new Exception();
        }
    }

    /// <summary>
    /// 相加中间用parameter间隔
    /// </summary>
    public class StationDataSourceValuesConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return null;

            if (values.Count() == 2)
            {
                ObservableCollection<Station> first = values[0] as ObservableCollection<Station>;

                ObservableCollection<TaskViewModel> second = values[1] as ObservableCollection<TaskViewModel>;

                if (first == null || second == null) return null;

                List<Station> stations = first.ToList();

                //  Message：查找所有不是同一个站的
                var collection = second.ToList().FindAll(l => l.StartSite.StationName != l.EndSite.StationName);

                if (collection == null) return stations;

                foreach (var item in collection)
                {
                    var startIndex = stations.FindIndex(l => l.StationName == item.StartSite.StationName);
                    var endIndex = stations.FindIndex(l => l.StationName == item.EndSite.StationName);

                    if (startIndex < 0 || endIndex < 0) continue;

                    stations.RemoveRange(startIndex + 1, stations.Count - startIndex - 1);
                }

                return stations;

            }
            else if (values.Count() == 3)
            {
                ObservableCollection<Station> first = values[0] as ObservableCollection<Station>;

                ObservableCollection<TaskViewModel> second = values[1] as ObservableCollection<TaskViewModel>;

                Station third = values[2] as Station;

                if (first == null || second == null || third == null) return null;

                List<Station> stations = first.ToList();

                var index = stations.FindIndex(l => l.StationName == third.StationName);

                //  Message：查找所有不是同一个站的
                var collection = second.ToList().FindAll(l => l.StartSite.StationName != l.EndSite.StationName);

                if (collection == null) return stations.Skip(index);

                List<Station> delete = new List<Station>();

                delete.AddRange(stations.Take(index));

                foreach (var item in collection)
                {
                    var startIndex = stations.FindIndex(l => l.StationName == item.StartSite.StationName);

                    var endIndex = stations.FindIndex(l => l.StationName == item.EndSite.StationName);

                    if (startIndex < 0 || endIndex < 0) continue;

                    if (startIndex >= index)
                    {
                        delete.AddRange(stations.Skip(startIndex + 1).Take(stations.Count - startIndex - 1));

                    }
                    else if (startIndex < index && index < endIndex)
                    {
                        stations.Clear();

                    }
                    else
                    {
                        delete.AddRange(stations.Skip(startIndex).Take(endIndex - startIndex));

                    }


                    //stations.RemoveRange(startIndex, stations.Count - startIndex);

                }

                stations.RemoveAll(l => delete.Exists(k => k.StationName == l.StationName));

                //  Message：判断选择是否可以选择同一个站
                var localstation = second.ToList().FindAll(l => l.StartSite.StationName==third.StationName&& third.StationName == l.EndSite.StationName);

                var startToEnd = localstation.Select(l => new Tuple<int, int>(Math.Min(third.Rods.IndexOf(l.StartPole),
                       third.Rods.IndexOf(l.EndPole)), Math.Max(third.Rods.IndexOf(l.StartPole), third.Rods.IndexOf(l.EndPole)))).OrderBy(l => l.Item1).OrderBy(l => l.Item2).ToList();

                var exist = third.Rods.ToList().Exists(k => !startToEnd.Exists(l => l.Item1 <= third.Rods.IndexOf(k) && l.Item2 >= third.Rods.IndexOf(k)));

                //  Message：如果所有杆号都分配了则排除当前
                if(!exist)
                {
                    stations.RemoveAll(l=>l.StationName==third.StationName);
                }
                //for (int i = 0; i < third.Rods.Count; i++)
                //{
                //    if (!startToEnd.Exists(l => l.Item1 <= i && l.Item2 >= i))
                //    {

                //    }
                //}

                return stations;
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

}
