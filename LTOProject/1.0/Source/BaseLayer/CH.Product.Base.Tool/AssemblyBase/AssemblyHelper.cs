using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{

    /// <summary> 程序集反射帮助类 </summary>
    public class AssemblyHelper : BaseFactory<AssemblyHelper>
    {
        /// <summary> 获取程序集中继承T类型的所有实例 </summary>
        public List<T> GetAssemblyClass<T>(Assembly ass) where T : class, new()
        {
            List<T> ls = new List<T>();

            Type[] classes = ass.GetTypes();

            classes = classes.ToList().FindAll(l => typeof(T).IsAssignableFrom(l)).ToArray();

            if (classes == null || classes.Length == 0)
            {
                return null;
            }

            classes.OrderBy(l => l.Name);


            foreach (Type t in classes)
            {
                T output = Activator.CreateInstance(t) as T;
                ls.Add(output);
            }

            return ls;
        }

        /// <summary> 从程序集中加载匹配项的所有实例 </summary>
        public List<T> AssGetTypeInstance<T>(Assembly ass, Predicate<Type> match) where T : class, new()
        {

            List<T> ls = new List<T>();

            var types = ass.GetTypes();

            foreach (var item in types)
            {
                if (!match(item)) continue;

                if (!item.IsSubclassOf(typeof(T))) continue;

                T ctr = Activator.CreateInstance(item) as T;

                if (ctr == null) continue;

                ls.Add(ctr);

            }

            return ls;
        }
    }
}
