using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ty.Base.WpfBase
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
