using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    /// <summary> 字符串转换 </summary>
    public static class ConvertExtention
    {
        /// <summary> 转换为Double类型 </summary>
        public static double ToDouble(this string s)
        {
            return Convert.ToDouble(s);
        }

        /// <summary> 转换为Int类型 </summary>
        public static int ToInt(this string s)
        {
            return Convert.ToInt32(s);
        }

        /// <summary> 转换为Double类型 </summary>
        public static double? ToDoubleNull(this string s)
        {
            if (string.IsNullOrEmpty(s)) return null;

            return Convert.ToDouble(s);
        }

        /// <summary> 转换为Int类型 </summary>
        public static int? ToIntNull(this string s)
        {
            if (string.IsNullOrEmpty(s)) return null;

            return Convert.ToInt32(s);
        }

        /// <summary> 转换为string 空则返回空 </summary>
        public static string ToStringNull(this double? s)
        {
            return s == null ? null : s.Value.ToString();
        }

        /// <summary> 转换为Double类型 </summary>
        public static bool IsDouble(this string s)
        {
            double d;
            return double.TryParse(s, out d);
        }

        /// <summary> 转换为Int类型 </summary>
        public static bool IsInt(this string s)
        {
            int i;
            return int.TryParse(s, out i);
        }

        /// <summary> 字符串转换成日期 string format = "yyyy-MM-dd hh:mi:ss"; </summary>
        public static DateTime ToDateTime(this string str,string format= "yyyy-MM-dd HH:mm:ss")
        {
            DateTime ss = DateTime.ParseExact(str, format, CultureInfo.CurrentCulture);

            return ss;
        }

        /// <summary> 时间转换成字符串 string format = "yyyy-MM-dd hh:mi:ss"; </summary>
        public static string ToDateTimeString(this DateTime time, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return time.ToString(format);
        }


        /// <summary> 截取小数点 </summary>
        public static string Round(this string str, int len)
        {
            return Math.Round(str.ToDouble(), len).ToString();
        }

        /// <summary>        
        /// 时间戳转为C#格式时间        
        /// </summary>        
        /// <param name=”timeStamp”></param>        
        /// <returns></returns>        
        public static DateTime ToTimeStamp(this string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string ToTimeStamp(this System.DateTime time)
        {
            long ts = ToIntTimeStamp(time);
            return ts.ToString();
        }
        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        public static long ToIntTimeStamp(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }


        /// <summary> 转换为Int类型 </summary>
        public static bool IsLong(this string s)
        {
            long i;
            return long.TryParse(s, out i);
        }


        /// <summary> 转换为Int类型 </summary>
        public static long ToLong(this string s)
        {
            long i;
            long.TryParse(s, out i);

            return i;
        }
    }
}
