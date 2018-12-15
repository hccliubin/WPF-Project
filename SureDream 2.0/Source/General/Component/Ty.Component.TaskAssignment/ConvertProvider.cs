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

            if (values.Length == 4)
            {
                TyeBaseSiteEntity start = values[0] as TyeBaseSiteEntity;
                TyeBaseSiteEntity end = values[1] as TyeBaseSiteEntity;

                if (start == null || end == null) return null;

                if (start.ID != end.ID) return null;

                string stationName = start.SiteName;

                List<TyeBasePillarEntity> rods = (values[3] as ObservableCollection<TyeBasePillarEntity>).ToList();

                ObservableCollection<TaskModel> collection = values[2] as ObservableCollection<TaskModel>;


                //  Message：查找当前站的所有任务信息
                var finds = collection.ToList().FindAll(l => l.StartSite.SiteName == stationName && l.EndSite.SiteName == stationName);

                if (finds == null) return rods;

                foreach (var item in finds)
                {
                    var startIndex = rods.FindIndex(l => l.PoleCode == item.StartPole.PoleCode);

                    var endIndex = rods.FindIndex(l => l.PoleCode == item.EndPole.PoleCode);

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
            else if (values.Length == 5)
            {
                TyeBaseSiteEntity start = values[0] as TyeBaseSiteEntity;
                TyeBaseSiteEntity end = values[1] as TyeBaseSiteEntity;

                if (start == null || end == null) return null;

                if (start.ID != end.ID) return null;

                string stationName = start.SiteName;

                TyeBasePillarEntity r = values[2] as TyeBasePillarEntity;

                if (r == null) return null;

                List<TyeBasePillarEntity> rods = (values[4] as ObservableCollection<TyeBasePillarEntity>).ToList();

                ObservableCollection<TaskModel> collection = values[3] as ObservableCollection<TaskModel>;

                var index = rods.FindIndex(l => l.PoleCode == r.PoleCode);

                //  Message：查找当前站的所有任务信息
                var finds = collection.ToList().FindAll(l => l.StartSite.SiteName == stationName && l.EndSite.SiteName == stationName);

                if (finds == null)
                {
                    return rods.Skip(index);
                }

                rods = rods.Skip(index).ToList();

                foreach (var item in finds)
                {
                    var startIndex = rods.FindIndex(l => l.PoleCode == item.StartPole.PoleCode);
                    var endIndex = rods.FindIndex(l => l.PoleCode == item.EndPole.PoleCode);

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
                ObservableCollection<TyeBaseSiteEntity> first = values[0] as ObservableCollection<TyeBaseSiteEntity>;

                ObservableCollection<TaskModel> second = values[1] as ObservableCollection<TaskModel>;

                if (first == null || second == null) return null;

                List<TyeBaseSiteEntity> stations = first.ToList();

                //  Message：查找所有不是同一个站的
                var collection = second.ToList().FindAll(l => l.StartSite.SiteName != l.EndSite.SiteName);

                if (collection == null) return stations;

                foreach (var item in collection)
                {
                    var startIndex = stations.FindIndex(l => l.SiteName == item.StartSite.SiteName);
                    var endIndex = stations.FindIndex(l => l.SiteName == item.EndSite.SiteName);

                    if (startIndex < 0 || endIndex < 0) continue;

                    stations.RemoveRange(startIndex + 1, stations.Count - startIndex - 1);
                }

                return stations;

            }
            else if (values.Count() == 4)
            {
                ObservableCollection<TyeBaseSiteEntity> first = values[0] as ObservableCollection<TyeBaseSiteEntity>;

                ObservableCollection<TaskModel> second = values[1] as ObservableCollection<TaskModel>;

                TyeBaseSiteEntity third = values[2] as TyeBaseSiteEntity;

                Dictionary<string, ObservableCollection<TyeBasePillarEntity>> PilarCache = values[3] as Dictionary<string, ObservableCollection<TyeBasePillarEntity>>;

                if (first == null || second == null || third == null) return null;

                List<TyeBaseSiteEntity> stations = first.ToList();

                var index = stations.FindIndex(l => l.SiteName == third.SiteName);

                //  Message：查找所有不是同一个站的
                var collection = second.ToList().FindAll(l => l.StartSite.SiteName != l.EndSite.SiteName);

                if (collection == null) return stations.Skip(index);

                List<TyeBaseSiteEntity> delete = new List<TyeBaseSiteEntity>();

                delete.AddRange(stations.Take(index));

                foreach (var item in collection)
                {
                    var startIndex = stations.FindIndex(l => l.SiteName == item.StartSite.SiteName);

                    var endIndex = stations.FindIndex(l => l.SiteName == item.EndSite.SiteName);

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

                stations.RemoveAll(l => delete.Exists(k => k.SiteName == l.SiteName));


                //  Message：判断选择是否可以选择同一个站
                var localstation = second.ToList().FindAll(l => l.StartSite.SiteName == third.SiteName && third.SiteName == l.EndSite.SiteName);

                if (PilarCache.ContainsKey(third.SiteName) == false) return stations;


                List<TyeBasePillarEntity> poles = PilarCache[third.SiteName].ToList();

                var startToEnd = localstation.Select(l => new Tuple<int, int>(Math.Min(poles.IndexOf(l.StartPole),
                       poles.IndexOf(l.EndPole)), Math.Max(poles.IndexOf(l.StartPole), poles.IndexOf(l.EndPole)))).OrderBy(l => l.Item1).OrderBy(l => l.Item2).ToList();

                var exist = poles.ToList().Exists(k => !startToEnd.Exists(l => l.Item1 <= poles.IndexOf(k) && l.Item2 >= poles.IndexOf(k)));

                //  Message：如果所有杆号都分配了则排除当前
                if (!exist)
                {
                    stations.RemoveAll(l => l.SiteName == third.SiteName);
                }

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
