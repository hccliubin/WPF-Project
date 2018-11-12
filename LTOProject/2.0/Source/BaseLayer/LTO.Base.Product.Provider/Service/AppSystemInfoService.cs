#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/4/12 11:23:08 
 * 文件名：AppSystemInfoService 
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

namespace LTO.Base.Product.Provider
{
   public class AppSystemInfoService
    {

        public static AppSystemInfoService Instance = new AppSystemInfoService();

        /// <summary> 初始化系统文件 </summary>
        public void Init(string binPath,string exeName)
        {
            AppSystemInfo.Instance.Init(binPath,exeName);

            StringResourceService.Instance.Init();
        }

        
    }
}
