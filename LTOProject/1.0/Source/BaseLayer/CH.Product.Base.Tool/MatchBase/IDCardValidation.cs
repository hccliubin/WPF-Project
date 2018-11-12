using System;
using System.Collections.Generic;
using System.Text;

namespace System.Linq
{
    /// <summary> 验证身份证号码类 </summary>
    public static class IDCardHelper
    {
        /// <summary>  验证身份证合理性 </summary> 
        public static bool IsIDCard(this string idNumber)  
        {

            if(string.IsNullOrEmpty(idNumber))
            {
                return false;
            }

            if(idNumber.Length == 18)  
 
            {  
                bool check = CheckIDCard18(idNumber);  
                return check;  
            }  
            else if (idNumber.Length == 15)  
            {  
                bool check = CheckIDCard15(idNumber);  
                return check;  
            }  
            else  
            {  
                return false;  
 
            }  
        }

        /// <summary> 根据身份证号获取生日 </summary>
        public static string getBirthdayByIdCard(string idNumber)
        {
            string birth = "";
            if (idNumber.Length == 18)
            {
                birth = idNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");  
            }
            else if (idNumber.Length == 15)
            {
                birth = idNumber.Substring(6, 6).Insert(4, "-").Insert(2, "-");  
            }
            return birth;
        }

        /// <summary> 根据身份证号获取性别 </summary>
        public static string getSexByIdCard(string idNumber) {
            string sex = "";
            if (idNumber.Length == 18)
            {
                sex = idNumber.Substring(16, 1);
            }
            else if (idNumber.Length == 15)
            {
                sex = idNumber.Substring(3, 1);
            }
            if (Convert.ToInt32(sex) % 2 == 0)
            {
                sex = "2";
            }
            else {
                sex = "1";
            }
            return sex;
        }

        /// <summary> 根据身份证号获取年龄  </summary>
        public static int getAgeByIdCard(string idNumber)
        {
            string birthday = getBirthdayByIdCard(idNumber);
            TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(birthday));
            return ts.Days / 365;
        }
   
   
        /// <summary> 18位身份证号码验证 </summary>  
        private static bool CheckIDCard18(this string idNumber)  
        {  
            long n = 0;  
            if (long.TryParse(idNumber.Remove(17), out n) == false   
 
                || n < Math.Pow(10, 16) || long.TryParse(idNumber.Replace('x', '0').Replace('X', '0'), out n) == false)  
 
            {  
                return false;//数字验证  
            }  
 
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";  
 
            if (address.IndexOf(idNumber.Remove(2)) == -1)  
            {  
                return false;//省份验证  
            }  
            string birth = idNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");  
 
            DateTime time = new DateTime();  
            if (DateTime.TryParse(birth, out time) == false)  
            {  
 
                return false;//生日验证  
            }  
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');  
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');  
 
            char[] Ai = idNumber.Remove(17).ToCharArray();  
            int sum = 0;  
            for (int i = 0; i < 17; i++)  
            {  
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());  
 
            }  
            int y = -1;  
            Math.DivRem(sum, 11, out y);  
            if (arrVarifyCode[y] != idNumber.Substring(17, 1).ToLower())  
            {  
                return false;//校验码验证  
 
            }  
            return true;//符合GB11643-1999标准  
        }  
   
   
        /// <summary> 16位身份证号码验证 </summary>  
        private static bool CheckIDCard15(this string idNumber)  
        {  
            long n = 0;  
            if (long.TryParse(idNumber, out n) == false || n < Math.Pow(10, 14))  
 
            {  
                return false;//数字验证  
            }  
 
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";  
 
            if (address.IndexOf(idNumber.Remove(2)) == -1)  
            {  
                return false;//省份验证  
            }  
            string birth = idNumber.Substring(6, 6).Insert(4, "-").Insert(2, "-");  
 
            DateTime time = new DateTime();  
            if (DateTime.TryParse(birth, out time) == false)  
            {  
                return false;//生日验证  
            }  
            return true;  
        } 
    }
}
