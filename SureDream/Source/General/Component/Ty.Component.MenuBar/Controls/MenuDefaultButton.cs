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

namespace Ty.Component.MenuBar
{
    /// <summary>
    /// 默认的按钮
    /// </summary>
    public class MenuDefaultButton : MenuIconButton
    {
        static MenuDefaultButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuDefaultButton), new FrameworkPropertyMetadata(typeof(MenuDefaultButton)));
        }
    }
}
