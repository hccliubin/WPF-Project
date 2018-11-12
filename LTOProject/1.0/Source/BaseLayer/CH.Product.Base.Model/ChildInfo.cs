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

namespace CH.Product.Base.Model
{


    /// <summary> 儿童信息 </summary>
    public class ChildInfo
    {

        //private string _id;
        ///// <summary> 说明 </summary>
        //public string ID
        //{
        //    get { return _id; }
        //    set { _id = value; }
        //}

        private string _hzxm;
        /// <summary> 姓名 </summary>
        public string hzxm
        {
            get { return _hzxm; }
            set { _hzxm = value; }
        }

        private string _xb;

        /// <summary> 性别 </summary>
        public string xb
        {
            get { return _xb; }
            set { _xb = value; }
        }


        private string _yfjzsj;

        /// <summary> 留观结束时间 </summary>
        public string yfjzsj
        {
            get { return _yfjzsj; }
            set { _yfjzsj = value; }
        }

        private string _lgId;

        /// <summary> 编号 </summary>
        public string lgId
        {
            get { return _lgId; }
            set { _lgId = value; }
        }


        private string _lgzt;

        /// <summary> 留观状态 </summary>
        public string lgzt
        {
            get { return _lgzt; }
            set { _lgzt = value; }
        }

        private int _second;
        /// <summary> 倒计时 </summary>
        public int second
        {
            get { return _second; }
            set { _second = value; }
        }


    }
}
