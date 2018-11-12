using LTO.Base.Theme.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace LTO.UserControls.Controls
{
    /// <summary> 配置页面 </summary>
    public partial class HomeConfigControl : BaseUserControl
    {

        HomeConfigNotifyClass _vm = new HomeConfigNotifyClass();

        public HomeConfigControl()
        {
            InitializeComponent();

            this.DataContext = _vm;
        }
    }



  

}
