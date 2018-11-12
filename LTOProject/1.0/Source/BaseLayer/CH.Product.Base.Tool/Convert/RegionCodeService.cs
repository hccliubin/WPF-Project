#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/23 10:17:09 
 * 文件名：RegionCodeService 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{

    /// <summary> 民族代码服务类 </summary>
    public class NationCodeService : BaseFactory<NationCodeService>
    {
        // Todo ：对照表 
        string str = @"
1	汉族
2	蒙古族
3	回族
4	藏族
5	维吾尔族
6	苗族
7	彝族
8	壮族
9	布依族
10	朝鲜族
11	满族
12	侗族
13	瑶族
14	白族
15	土家族
16	哈尼族
17	哈萨克族
18	傣族
19	黎族
20	傈僳族
21	佤族
22	畲族
23	高山族
24	拉祜族
25	水族
26	东乡族
27	纳西族
28	景颇族
29	柯尔克孜族
30	土族
31	达斡尔族
32	仫佬族
33	羌族
34	布朗族
35	撒拉族
36	毛难族
37	仡佬族
38	锡伯族
39	阿昌族
40	普米族
41	塔吉克族
42	怒族
43	乌孜别克族
44	俄罗斯族
45	鄂温克族
46	崩龙族
47	保安族
48	裕固族
49	京族
50	塔塔尔族
51	独龙族
52	鄂伦春族
53	赫哲族
54	门巴族
55	珞巴族
56	基诺族
97	其他
98	外国血统";

        private Dictionary<string, string> _dic = new Dictionary<string, string>();
        /// <summary> 区划对照表 </summary>
        public Dictionary<string, string> Dic
        {
            get
            {

                // Todo ：初始化 
                if (_dic.Count == 0)
                {
                    Init();
                }
                return _dic;
            }
        }


        /// <summary> 初始化方法 </summary>
        void Init()
        {
            string[] collection = str.Split('\r');

            _dic.Clear();

            foreach (var item in collection)
            {
                string[] temp = item.Trim('\n').Split('\t');

                if (temp == null || temp.Length != 2) continue;
                _dic.Add(temp[0], temp[1]);
            }
        }


        /// <summary> 获取区划名称 </summary>
        public string GetName(string code)
        {
            if (code == "0") return "汉族";
            if (code == "99") return "其他";
            if (string.IsNullOrEmpty(code)) return string.Empty;

            string outstring = string.Empty;
            Dic.TryGetValue(code.TrimStart('0'), out outstring);

            return outstring;
        }


        /// <summary> 获取性别  1 男 2 女 其他 </summary>
        public string GetGender(string code)
        {
            if (code == "1") return "男";

            if (code == "2") return "女";

            return "其他";
        }

        /// <summary> 获取年龄 </summary>
        public string GetAge(string birthDay)
        {
            if (string.IsNullOrEmpty(birthDay)) return null;

            if (birthDay.Length < 10) return null;

            DateTime time = birthDay.Substring(0, 10).ToDateTime("yyyy-MM-dd");

            return (DateTime.Now.Year - time.Year - 1).ToString();
        }

        /// <summary> 获取检查项是否正常 </summary>
        public string GetNormal(string codeString)
        {
            if (string.IsNullOrEmpty(codeString) || codeString.StartsWith("0|")) return null;

            return codeString.StartsWith("1") ? codeString.Replace("1", "正常").Trim('|') : codeString.StartsWith("2") ? codeString.Replace("2", "异常").Trim('|') : codeString.Trim('/');
        }

        /// <summary> 获取检查项阳性还是阴性 </summary>
        public string GetResult(string codeString)
        {
            if (string.IsNullOrEmpty(codeString)) return null;

            return codeString.StartsWith("1") ? "阳性" : codeString.StartsWith("2") ? "阴性" : codeString;
        }


    }
}
