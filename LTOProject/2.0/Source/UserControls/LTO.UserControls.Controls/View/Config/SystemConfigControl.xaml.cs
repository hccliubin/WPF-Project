
using LTO.Base.Theme.Style;
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

namespace LTO.UserControls.Controls
{
    /// <summary>
    /// SystemConfigControl.xaml 的交互逻辑
    /// </summary>
    public partial class SystemConfigControl : BaseUserControl
    {
        public SystemConfigControl()
        {
            InitializeComponent();
        }


        //声明和注册路由事件
        public static readonly RoutedEvent CancelClickedRoutedEvent =
            EventManager.RegisterRoutedEvent("CancelClicked", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(SystemConfigControl));
        //CLR事件包装
        public event RoutedEventHandler CancelClicked
        {
            add { this.AddHandler(CancelClickedRoutedEvent, value); }
            remove { this.RemoveHandler(CancelClickedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnCancelClicked()
        {
            RoutedEventArgs args = new RoutedEventArgs(CancelClickedRoutedEvent, this);
            this.RaiseEvent(args);
        }


        private void btn_goback_Click(object sender, RoutedEventArgs e)
        {
            this.OnCancelClicked();
        }
    }
}
