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

            if (values.Where(l => l == null).Count() > 0 || values.Where(l => l.ToString() == "{DependencyProperty.UnsetValue}").Count() > 0) return parameter.ToString();


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
            if (values.Length == 4)
            {
                //  Message：起始站
                TyeBaseSiteEntity start = values[0] as TyeBaseSiteEntity;

                //  Message：结束站
                TyeBaseSiteEntity end = values[1] as TyeBaseSiteEntity;

                if (start == null || end == null) return null;

                if (start.ID != end.ID) return null;

                string stationID = start.ID;

                //  Message：当前站杆号列表
                List<TyeBasePillarEntity> rods = (values[3] as ObservableCollection<TyeBasePillarEntity>).ToList();

                //  Message：任务列表哦
                ObservableCollection<TaskViewModel> collection = values[2] as ObservableCollection<TaskViewModel>;


                //  Message：查找当前站的所有任务信息
                var finds = collection.ToList().FindAll(l => l.StartSite.ID == stationID && l.EndSite.ID == stationID);

                if (finds == null) return rods;

                foreach (var item in finds)
                {
                    if (item.StartPole == null || item.EndPole == null) continue;

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

            //  Message：结束杆号可选项删选
            else if (values.Length == 5)
            {
                //  Message：起始站
                TyeBaseSiteEntity start = values[0] as TyeBaseSiteEntity;

                //  Message：结束站
                TyeBaseSiteEntity end = values[1] as TyeBaseSiteEntity;

                if (start == null || end == null) return null;

                if (start.ID != end.ID) return null;

                string stationID = start.ID;

                //  Message：上一个杆号
                TyeBasePillarEntity r = values[2] as TyeBasePillarEntity;

                if (r == null) return null;

                //  Message：当前站所有杆号
                List<TyeBasePillarEntity> rods = (values[4] as ObservableCollection<TyeBasePillarEntity>).ToList();

                //  Message：任务列表
                ObservableCollection<TaskViewModel> collection = values[3] as ObservableCollection<TaskViewModel>;

                var index = rods.FindIndex(l => l.PoleCode == r.PoleCode);

                //  Message：查找当前站的所有任务信息
                var finds = collection.ToList().FindAll(l => l.StartSite.ID == stationID && l.EndSite.ID == stationID);

                if (finds == null)
                {
                    return rods.Skip(index);
                }

                rods = rods.Skip(index).ToList();

                foreach (var item in finds)
                {
                    if (item.StartPole == null || item.EndPole == null) continue;

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

    /// <summary>
    /// 站可选值删选
    /// </summary>
    public class StationDataSourceValuesConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return null;

            //  Message：起始站可选值
            if (values.Count() == 2)
            {
                //  Message：所有站列表
                ObservableCollection<TyeBaseSiteEntity> first = values[0] as ObservableCollection<TyeBaseSiteEntity>;

                //  Message：任务列表
                ObservableCollection<TaskViewModel> second = values[1] as ObservableCollection<TaskViewModel>;

                if (first == null || second == null) return null;

                List<TyeBaseSiteEntity> stations = first.ToList();

                ////  Message：查找所有不是同一个站的
                //var collection = second.ToList().FindAll(l => l.StartSite.ID != l.EndSite.ID);

                //if (collection == null) return stations;

                //foreach (var item in collection)
                //{
                //    var startIndex = stations.FindIndex(l => l.ID == item.StartSite.ID);
                //    var endIndex = stations.FindIndex(l => l.ID == item.EndSite.ID);

                //    if (startIndex < 0 || endIndex < 0) continue;

                //    stations.RemoveRange(startIndex + 1, stations.Count - startIndex - 1);
                //}

                return stations;

            }

            //  Message：结束站可选值
            else if (values.Count() == 4)
            {
                //  Message：所有站列表
                ObservableCollection<TyeBaseSiteEntity> first = values[0] as ObservableCollection<TyeBaseSiteEntity>;

                //  Message：任务列表
                ObservableCollection<TaskViewModel> second = values[1] as ObservableCollection<TaskViewModel>;

                //  Message：起始站值
                TyeBaseSiteEntity third = values[2] as TyeBaseSiteEntity;

                //  Message：站杆号选择列表
                Dictionary<string, ObservableCollection<TyeBasePillarEntity>> PilarCache = values[3] as Dictionary<string, ObservableCollection<TyeBasePillarEntity>>;

                if (first == null || second == null || third == null) return null;

                List<TyeBaseSiteEntity> stations = first.ToList();

                var index = stations.FindIndex(l => l.ID == third.ID);

                //  Message：查找所有不是同一个站的
                var collection = second.ToList().FindAll(l => l.StartSite.ID != l.EndSite.ID);

                if (collection == null) return stations.Skip(index);

                List<TyeBaseSiteEntity> delete = new List<TyeBaseSiteEntity>();

                delete.AddRange(stations.Take(index));

                foreach (var item in collection)
                {
                    var startIndex = stations.FindIndex(l => l.ID == item.StartSite.ID);

                    var endIndex = stations.FindIndex(l => l.ID == item.EndSite.ID);

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

                }

                stations.RemoveAll(l => delete.Exists(k => k.ID == l.ID));

                //  Message：判断选择是否可以选择同一个站
                var localstation = second.ToList().FindAll(l => l.StartSite.ID == third.ID && third.ID == l.EndSite.ID);

                if (PilarCache.ContainsKey(third.ID) == false) return stations;

                List<TyeBasePillarEntity> poles = PilarCache[third.ID].ToList();

                var startToEnd = localstation.Select(l => new Tuple<int, int>(Math.Min(poles.IndexOf(l.StartPole),
                       poles.IndexOf(l.EndPole)), Math.Max(poles.IndexOf(l.StartPole), poles.IndexOf(l.EndPole)))).OrderBy(l => l.Item1).OrderBy(l => l.Item2).ToList();

                var exist = poles.ToList().Exists(k => !startToEnd.Exists(l => l.Item1 <= poles.IndexOf(k) && l.Item2 >= poles.IndexOf(k)));

                //  Message：如果所有杆号都分配了则排除当前
                if (!exist)
                {
                    stations.RemoveAll(l => l.ID == third.ID);
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
