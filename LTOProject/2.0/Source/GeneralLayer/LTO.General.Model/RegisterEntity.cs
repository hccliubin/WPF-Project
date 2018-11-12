using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTO.General.Model
{
    public class RegisterEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> dataList { get; set; }
        /// <summary>
        /// 张浩宇
        /// </summary>
        public string hzxm { get; set; }
        /// <summary>
        /// 儿童：张浩宇已挂号!
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string preRowNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rowNum { get; set; }
    }
}
