using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SureDream.Component.MenuBar
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
        Default = 0, IconButton, ToggleButton
    }
}
