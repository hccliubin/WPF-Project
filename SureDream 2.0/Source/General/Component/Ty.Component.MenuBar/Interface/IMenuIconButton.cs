using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ty.Component.MenuBar
{
    /// <summary> 静态控件布局接口 </summary>
    public interface IMenuIconButton
    {
        /// <summary> 显示明湖曾 </summary>
        object Content { get; set; }

        /// <summary> 水平对齐方式 </summary>
        LeftRightAlignment LeftRightAlignment { get; set; }

    }

    /// <summary> 选中按钮接口 </summary>
    public interface IMenuToggleButton : IMenuIconButton
    {
        /// <summary> 显示明湖曾 </summary>
        bool? IsChecked { get; set; }

    }


    /// <summary> 对齐方式 </summary>
    public enum LeftRightAlignment
    {
        Left = 0, Right
    }

    /// <summary> 按钮类型 </summary>
    public enum MenuButtonStyle
    {
        /// <summary> 默认按钮类型 </summary>
        Default = 0,

        /// <summary> 矢量图标按钮类型 </summary>
        IconButton,

        /// <summary> 选中矢量图标按钮类型 </summary>
        ToggleButton,

        /// <summary> 图片按钮类型 </summary>
        MenuImageButton,

        /// <summary> 图片选中按钮类型 </summary>
        MenuToggleImageButton
    }
}
