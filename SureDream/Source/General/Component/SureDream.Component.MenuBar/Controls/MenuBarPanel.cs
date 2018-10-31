using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SureDream.Component.MenuBar
{ 
    /// <summary>
    /// 工具栏容器
    /// </summary>
    public class MenuBarPanel : ItemsControl
    {
        static MenuBarPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuBarPanel), new FrameworkPropertyMetadata(typeof(MenuBarPanel)));
        }
    }
}
