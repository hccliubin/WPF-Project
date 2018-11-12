#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/4/2 16:08:24 
 * 文件名：Class1 
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

namespace CH.Product.UserControls
{
    /// <summary>
    /// 留观项
    /// </summary>
    public class LeaveToObserveItem
    {
        /// <summary>
        /// id 唯一标识符
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 编号 自己处理取后三位
        /// </summary>
        public string seq { get; set; }

        /// <summary>
        /// 病人名字
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }

        public string status { get; set; }

        public int state { get; set; }

        /// <summary>
        /// 倒计时 
        /// </summary>
        public long createTime { get; set; }
        public bool animate { get; set; }

        /// <summary>
        /// 所打的疫苗种类
        /// </summary>
        public string vaccineName { get; set; }

        /// <summary>
        /// 留观结束时间
        /// </summary>
        public string leaveTime { get; set; }
    }
}
