using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SureDream.Component.MenuBar
{
    /// <summary> 按钮点击事件参数 </summary>
    public class MenuRoutedEventArgs : RoutedEventArgs
    {
        public IMenuIconButton MenuSource { get; set; }

        public MenuRoutedEventArgs(RoutedEvent routedEvent, object source, IMenuIconButton menusource) : base(routedEvent, source)
        {
            MenuSource = menusource;
        }
    }

    /// <summary> 按钮选中事件参数 </summary>
    public class MenuCheckedRoutedEventArgs : RoutedEventArgs
    {
        public IMenuToggleButton MenuSource { get; set; }

        public MenuCheckedRoutedEventArgs(RoutedEvent routedEvent, object source, IMenuToggleButton menusource) : base(routedEvent, source)
        {
            MenuSource = menusource;
        }


    }

    


}
