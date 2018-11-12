#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/4/12 11:05:52 
 * 文件名：LogService 
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

namespace LTO.General.Logger
{
    public class LogService
    {

        public static LogService Instance = new LogService();

        private Log4Servcie _log4net=new Log4Servcie();
        /// <summary> 说明 </summary>
        public Log4Servcie Log4Net
        {
            get { return _log4net; }
            set { _log4net = value; }
        }


    }
}
