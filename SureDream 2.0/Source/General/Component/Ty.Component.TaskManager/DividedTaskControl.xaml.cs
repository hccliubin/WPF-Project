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

namespace Ty.Component.TaskManager
{
    /// <summary>
    /// DividedTaskControl.xaml 的交互逻辑
    /// </summary>
    public partial class DividedTaskControl : UserControl
    {
        public DividedTaskControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


        //声明和注册路由事件
        public static readonly RoutedEvent SaveClickRoutedEvent =
            EventManager.RegisterRoutedEvent("SaveClick", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(DividedTaskControl));
        //CLR事件包装
        public event RoutedEventHandler SaveClick
        {
            add { this.AddHandler(SaveClickRoutedEvent, value); }
            remove { this.RemoveHandler(SaveClickRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnSaveClick()
        {
            RoutedEventArgs args = new RoutedEventArgs(SaveClickRoutedEvent, this);
            this.RaiseEvent(args);
        }

    }
}
