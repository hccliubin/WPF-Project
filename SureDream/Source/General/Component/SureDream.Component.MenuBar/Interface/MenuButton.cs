using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SureDream.Component.MenuBar
{
    /// <summary> 动态添加控件接口 </summary>
    public class MenuButton : IMenuIconButton
    {
        /// <summary> 显示的名称 </summary>
        public object Content { get; set; }

        /// <summary> 显示的图标 </summary>
        public string IconFont { get; set; }

        /// <summary> 水平对齐方式 </summary>
        public LeftRightAlignment LeftRightAlignment { get; set; }

        /// <summary> 控件的显示形式 </summary>
        public MenuButtonStyle MenuButtonStyle { get; set; }
    }

}
