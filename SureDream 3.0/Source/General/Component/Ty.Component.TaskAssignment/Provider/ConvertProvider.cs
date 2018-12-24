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
                ObservableCollection<TaskView4CModel> collection = values[2] as ObservableCollection<TaskView4CModel>;


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
                        rods.RemoveRange(startIndex, endIndex - startIndex + 1);
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

                //  Message：上一个杆号
                TyeBasePillarEntity r = values[2] as TyeBasePillarEntity;

                List<TyeBasePillarEntity> lastList = (values[3] as List<TyeBasePillarEntity>);

                if (start == null || end == null) return null;

                if (lastList == null) return null;

                if (r == null) return null;

                if (start.ID != end.ID) return null;

                if (values[4] == null) return null;

                string stationID = start.ID;

                //  Message：当前站所有杆号
                List<TyeBasePillarEntity> allrods = (values[4] as ObservableCollection<TyeBasePillarEntity>).Where(l => l.SiteID == stationID).ToList();

                //  Message：上一个杆号可选择列表
                List<TyeBasePillarEntity> rods = lastList.ToList();


                int index = rods.FindIndex(l => l.ID == r.ID);

                //  Message：从当前选择的开始截取
                rods = rods.Skip(index).ToList();

                for (int i = 0; i < rods.Count; i++)
                {
                    if (i == 0) continue;

                    int lastIndex = allrods.ToList().FindIndex(l => l.ID == rods[i - 1].ID);

                    int currentIndex = allrods.ToList().FindIndex(l => l.ID == rods[i].ID);

                    if ((currentIndex - lastIndex) > 1)
                    {
                        rods = rods.Take(rods.FindIndex(l => l.ID == rods[i - 1].ID) + 1).ToList();
                        break;
                    }
                }

                //if (r == null) return null;

                ////  Message：当前站所有杆号
                //List<TyeBasePillarEntity> rods = (values[4] as ObservableCollection<TyeBasePillarEntity>).ToList();

                ////  Message：任务列表
                //ObservableCollection<TaskViewModel> collection = values[3] as ObservableCollection<TaskViewModel>;

                //var index = rods.FindIndex(l => l.PoleCode == r.PoleCode);

                ////  Message：查找当前站的所有任务信息
                //var finds = collection.ToList().FindAll(l => l.StartSite.ID == stationID && l.EndSite.ID == stationID);


                //if (finds == null)
                //{
                //    return rods.Skip(index);
                //}

                //rods = rods.Skip(index).ToList();

                //foreach (var item in finds)
                //{
                //    if (item.StartPole == null || item.EndPole == null) continue;

                //    var startIndex = rods.FindIndex(l => l.PoleCode == item.StartPole.PoleCode);

                //    var endIndex = rods.FindIndex(l => l.PoleCode == item.EndPole.PoleCode);

                //    if (startIndex < 0 || endIndex < 0) continue;

                //    if (startIndex == endIndex)
                //    {
                //        rods.RemoveAt(startIndex);
                //    }
                //    else
                //    {
                //        rods.RemoveRange(startIndex, rods.Count - startIndex);
                //    }
                //}

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
            if (values.Count() == 3)
            {
                //  Message：所有站列表
                ObservableCollection<TyeBaseSiteEntity> first = values[0] as ObservableCollection<TyeBaseSiteEntity>;

                //  Message：任务列表
                ObservableCollection<TaskView4CModel> second = values[1] as ObservableCollection<TaskView4CModel>;

                if (first == null || second == null) return null;

                List<TyeBaseSiteEntity> stations = first.ToList();

                List<TyeBaseSiteEntity> delete = new List<TyeBaseSiteEntity>();

                Dictionary<string, ObservableCollection<TyeBasePillarEntity>> PilarCache = values[2] as Dictionary<string, ObservableCollection<TyeBasePillarEntity>>;

                //  Message：查找所有不是同一个站的
                {
                    var collection = second.ToList().FindAll(l => l.StartSite.ID != l.EndSite.ID);


                    foreach (var item in collection)
                    {
                        var startIndex = stations.FindIndex(l => l.ID == item.StartSite.ID);

                        var endIndex = stations.FindIndex(l => l.ID == item.EndSite.ID);

                        if (startIndex < 0 || endIndex < 0) continue;

                        delete.AddRange(stations.Skip(startIndex).Take(endIndex - startIndex + 1));

                        ////  Message：如果是最后一个站移除
                        //if (endIndex == stations.Count - 1)
                        //{
                        //    delete.Add(stations.Last());
                        //}
                    }
                }

                //  Message：查找所有同一个站
                {
                    var collection = second.ToList().FindAll(l => l.StartSite.ID == l.EndSite.ID).GroupBy(l => l.StartSite);

                    foreach (var item in collection)
                    {

                        TyeBaseSiteEntity currentSite = item.Key;

                        //  Message：判断选择是否可以选择同一个站
                        var localstation = second.ToList().FindAll(l => l.StartSite.ID == currentSite.ID && currentSite.ID == l.EndSite.ID);

                        if (PilarCache.ContainsKey(currentSite.ID) == false)
                        {
                            Debug.WriteLine("缓存中没找到对应站的杆号信息：" + currentSite.ID);
                            continue;
                        }

                        List<TyeBasePillarEntity> poles = PilarCache[currentSite.ID].ToList();

                        var startToEnd = localstation.Select(l => new Tuple<int, int>(Math.Min(poles.FindIndex(k => k.ID == l.StartPole.ID),
                               poles.FindIndex(k => k.ID == l.EndPole.ID)), Math.Max(poles.FindIndex(k => k.ID == l.StartPole.ID),
                               poles.FindIndex(k => k.ID == l.EndPole.ID)))).OrderBy(l => l.Item1).OrderBy(l => l.Item2).ToList();

                        var exist = poles.ToList().Exists(k => !startToEnd.Exists(l => l.Item1 <= poles.IndexOf(k) && l.Item2 >= poles.IndexOf(k)));

                        //  Message：如果所有杆号都分配了则排除当前
                        if (!exist)
                        {
                            delete.Add(currentSite);
                        }

                    }
                }

                stations.RemoveAll(l => delete.Exists(k => k.ID == l.ID));

                //if (collection != null)
                //{
                //    //  Message：起始站全部删除
                //    stations.RemoveAll(l => collection.Exists(k => l.ID == k.StartSite.ID));



                //    //  Message：结束站如果是最后一个则删除
                //    if(collection.Exists(k => stations.Last().ID == k.EndSite.ID))
                //    {
                //        stations.RemoveAll(l => l.ID == stations.Last().ID);
                //    }
                //}

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
            else if (values.Count() == 5)
            {
                //  Message：所有站列表
                ObservableCollection<TyeBaseSiteEntity> allList = values[0] as ObservableCollection<TyeBaseSiteEntity>;

                //  Message：起始站列表
                List<TyeBaseSiteEntity> startList = values[2] as List<TyeBaseSiteEntity>;

                //  Message：起始站
                TyeBaseSiteEntity startSite = values[1] as TyeBaseSiteEntity;

                //  Message：任务列表
                ObservableCollection<TaskView4CModel> taskList = values[3] as ObservableCollection<TaskView4CModel>;

                //  Message：站杆号选择列表
                Dictionary<string, ObservableCollection<TyeBasePillarEntity>> PilarCache = values[4] as Dictionary<string, ObservableCollection<TyeBasePillarEntity>>;

                if (allList == null) return null;
                if (startList == null) return null;
                if (startSite == null) return null;
                if (taskList == null) return null;
                if (PilarCache == null) return null;

                List<TyeBaseSiteEntity> stations = startList.ToList();


                int index = stations.FindIndex(l => l.ID == startSite.ID);

                //  Message：从当前选择的开始截取
                stations = stations.Skip(stations.FindIndex(l => l.ID == startSite.ID)).ToList();

                for (int i = 0; i < stations.Count; i++)
                {
                    if (i == 0) continue;

                    int lastIndex = allList.ToList().FindIndex(l => l.ID == stations[i - 1].ID);

                    int currentIndex = allList.ToList().FindIndex(l => l.ID == stations[i].ID);

                    if ((currentIndex - lastIndex) > 1)
                    {
                        stations = stations.Take(stations.FindIndex(l => l.ID == stations[i - 1].ID) + 1).ToList();
                        break;
                    }
                }

                //  Message：查找同一个起始和结束站的
                var collection = taskList.ToList().FindAll(l => l.StartSite.ID == l.EndSite.ID).GroupBy(l => l.StartSite);

                foreach (var item in collection)
                {
                    int indexCurrent = stations.FindIndex(l => l.ID == item.Key.ID);

                    if (indexCurrent < 0) continue;

                    if (startSite.ID != item.Key.ID)
                    {
                        //  Message：从当前选择的开始截取
                        stations = stations.Take(indexCurrent).ToList();
                    }
                    else
                    {
                        //  Message：从当前选择的开始截取
                        stations = stations.Take(1).ToList();
                    }

                }

                return stations;
                ////  Message：所有站列表
                //ObservableCollection<TyeBaseSiteEntity> first = values[0] as ObservableCollection<TyeBaseSiteEntity>;

                ////  Message：任务列表
                //ObservableCollection<TaskViewModel> second = values[1] as ObservableCollection<TaskViewModel>;

                ////  Message：起始站值
                //TyeBaseSiteEntity third = values[2] as TyeBaseSiteEntity;


                //if (first == null || second == null || third == null) return null;

                //List<TyeBaseSiteEntity> stations = first.ToList();

                //var index = stations.FindIndex(l => l.ID == third.ID);

                ////  Message：查找所有不是同一个站的
                //var collection = second.ToList().FindAll(l => l.StartSite.ID != l.EndSite.ID);

                //if (collection == null) return stations.Skip(index);

                //List<TyeBaseSiteEntity> delete = new List<TyeBaseSiteEntity>();

                //delete.AddRange(stations.Take(index));

                //foreach (var item in collection)
                //{
                //    var startIndex = stations.FindIndex(l => l.ID == item.StartSite.ID);

                //    var endIndex = stations.FindIndex(l => l.ID == item.EndSite.ID);

                //    if (startIndex < 0 || endIndex < 0) continue;

                //    if (startIndex >= index)
                //    {
                //        delete.AddRange(stations.Skip(startIndex + 1).Take(stations.Count - startIndex - 1));

                //    }
                //    else if (startIndex < index && index < endIndex)
                //    {
                //        stations.Clear();
                //    }
                //    else
                //    {
                //        delete.AddRange(stations.Skip(startIndex).Take(endIndex - startIndex));

                //    }

                //}

                //stations.RemoveAll(l => delete.Exists(k => k.ID == l.ID));

                ////  Message：判断选择是否可以选择同一个站
                //var localstation = second.ToList().FindAll(l => l.StartSite.ID == third.ID && third.ID == l.EndSite.ID);

                //if (PilarCache.ContainsKey(third.ID) == false) return stations;

                //List<TyeBasePillarEntity> poles = PilarCache[third.ID].ToList();

                //var startToEnd = localstation.Select(l => new Tuple<int, int>(Math.Min(poles.IndexOf(l.StartPole),
                //       poles.IndexOf(l.EndPole)), Math.Max(poles.IndexOf(l.StartPole), poles.IndexOf(l.EndPole)))).OrderBy(l => l.Item1).OrderBy(l => l.Item2).ToList();

                //var exist = poles.ToList().Exists(k => !startToEnd.Exists(l => l.Item1 <= poles.IndexOf(k) && l.Item2 >= poles.IndexOf(k)));

                ////  Message：如果所有杆号都分配了则排除当前
                //if (!exist)
                //{
                //    stations.RemoveAll(l => l.ID == third.ID);
                //}

                //return stations;
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
    /// 转换杆号可选项删选
    /// </summary>
    public class ConvertLineConverter : IMultiValueConverter
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
                ObservableCollection<Task2CViewModel> collection = values[1] as ObservableCollection<Task2CViewModel>;

                if (rods == null) return null;

                if (collection == null) return null;

                var result = rods.ToList();

                //  Message：查找当前站的所有任务信息
                var finds = collection.ToList();

                if (finds == null) return result;

                foreach (var item in finds)
                {
                    result.RemoveAll(l => item.Lines.Exists(k => k.ID == l.ID));

                    //var startIndex = result.FindIndex(l => l.ID == item.StartLine.ID);

                    //var endIndex = result.FindIndex(l => l.ID == item.EndLine.ID);

                    //if (startIndex < 0 || endIndex < 0) continue;

                    //if (startIndex == endIndex)
                    //{
                    //    result.RemoveAt(startIndex);
                    //}
                    //else
                    //{
                    //    result.RemoveRange(startIndex, endIndex - startIndex + 1);
                    //}
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
    /// 相加中间用parameter间隔
    /// </summary>
    public class LineToToolTipConverter : IValueConverter
    {

        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return null;

            List<TyeLineEntity> sss = values as List<TyeLineEntity>;

            if (sss == null) return null;

            return sss.Select(l=>l.Name).Aggregate((l, k) => l + ","+ k);
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
