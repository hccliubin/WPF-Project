using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{

    /// <summary> 当前实例的反射方法 </summary>
    public static class InstanceHelper
    {
        /// <summary> 检验当前指定类型属性是否都满足指定条件 </summary>
        public static bool IsPropertyMatch<T>(this object obj, Predicate<T> match) where T : class
        {
            var ps = obj.GetType().GetProperties();
            // Todo ：检查所有类型为String的属性 如果有一个为空则返回false 
            foreach (var item in ps)
            {
                if (item.PropertyType.FullName==typeof(T).FullName)
                {
                    if (!item.CanRead) continue;

                    T s = item.GetValue(obj) as T;

                    if (match(s)) return false;
                }
            }

            return true;
        }



    }
}
