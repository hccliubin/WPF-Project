#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/4/4 9:54:01 
 * 文件名：ChildInfo 
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


    /// <summary> 儿童信息 </summary>
    class ChildInfo
    {

        private string _id;
        /// <summary> 说明 </summary>
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name;
        /// <summary> 说明 </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }



    }
}
