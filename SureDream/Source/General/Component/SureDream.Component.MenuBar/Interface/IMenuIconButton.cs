using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SureDream.Component.MenuBar
{
    public interface IMenuIconButton
    {
        /// <summary> 水平对齐方式 </summary>
        LeftRightAlignment LeftRightAlignment { get; set; }

    }

    public enum LeftRightAlignment
    {
        Left = 0, Right
    }

    public enum MenuButtonStyle
    {
        Default=0,IconButton,ToggleButton
    }
}
