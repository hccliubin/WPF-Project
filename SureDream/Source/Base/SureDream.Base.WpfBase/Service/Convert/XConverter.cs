#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/11/21 18:35:28 
 * 文件名：XConverter 
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
using System.Windows.Controls;

namespace SureDream.Base.WpfBase
{

    /// <summary>
    /// 类型转换
    /// </summary>
    public sealed class XConverter
    {
        /// <summary>
        /// 可见转换
        /// </summary>
        public static BooleanToVisibilityConverter BooleanToVisibilityConverter
        {
            get { return new BooleanToVisibilityConverter(); }
        }

        /// <summary>
        /// 可见转换
        /// </summary>
        public static VisibilityConverter VisibilityConverter
        {
            get { return new VisibilityConverter(); }
        }

        /// <summary>
        /// 边距转换
        /// </summary>
        public static ThicknessToDoubleConverter ThicknessToDoubleConverter
        {
            get { return new ThicknessToDoubleConverter(); }
        }

        /// <summary>
        /// 百分比转换
        /// </summary>
        public static PercentToAngleConverter PercentToAngleConverter
        {
            get { return new PercentToAngleConverter(); }
        }

        /// <summary>
        /// bool转换
        /// </summary>
        public static TrueToFalseConverter TrueToFalseConverter
        {
            get { return new TrueToFalseConverter(); }
        }

    }
}
