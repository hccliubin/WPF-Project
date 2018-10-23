using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SureDream.Component.MenuBar
{
    public class MenuRoutedEventArgs : RoutedEventArgs
    {
        public IMenuIconButton MenuSource { get; set; }

        public MenuRoutedEventArgs(RoutedEvent routedEvent, object source, IMenuIconButton menusource) : base(routedEvent, source)
        {
            MenuSource = menusource;
        }


    }


}
