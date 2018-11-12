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
    /// ConfigMessageControl.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigMessageControl : BaseUserControl
    {
        public ConfigMessageControl()
        {
            InitializeComponent();
        }


        //声明和注册路由事件
        public static readonly RoutedEvent SetClickedRoutedEvent =
            EventManager.RegisterRoutedEvent("SetClicked", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ConfigMessageControl));
        //CLR事件包装
        public event RoutedEventHandler SetClicked
        {
            add { this.AddHandler(SetClickedRoutedEvent, value); }
            remove { this.RemoveHandler(SetClickedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnSetClicked()
        {
            RoutedEventArgs args = new RoutedEventArgs(SetClickedRoutedEvent, this);
            this.RaiseEvent(args);
        }


        //声明和注册路由事件
        public static readonly RoutedEvent CloseClickedRoutedEvent =
            EventManager.RegisterRoutedEvent("CloseClicked", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ConfigMessageControl));
        //CLR事件包装
        public event RoutedEventHandler CloseClicked
        {
            add { this.AddHandler(CloseClickedRoutedEvent, value); }
            remove { this.RemoveHandler(CloseClickedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnCloseClicked()
        {
            RoutedEventArgs args = new RoutedEventArgs(CloseClickedRoutedEvent, this);
            this.RaiseEvent(args);
        }



        //声明和注册路由事件
        public static readonly RoutedEvent SetDownClickedRoutedEvent =
            EventManager.RegisterRoutedEvent("SetDownClicked", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ConfigMessageControl));
        //CLR事件包装
        public event RoutedEventHandler SetDownClicked
        {
            add { this.AddHandler(SetDownClickedRoutedEvent, value); }
            remove { this.RemoveHandler(SetDownClickedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnSetDownClicked()
        {
            RoutedEventArgs args = new RoutedEventArgs(SetDownClickedRoutedEvent, this);
            this.RaiseEvent(args);
        }



        //声明和注册路由事件
        public static readonly RoutedEvent ShutDownAppRoutedEvent =
            EventManager.RegisterRoutedEvent("ShutDownApp", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ConfigMessageControl));
        //CLR事件包装
        public event RoutedEventHandler ShutDownApp
        {
            add { this.AddHandler(ShutDownAppRoutedEvent, value); }
            remove { this.RemoveHandler(ShutDownAppRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnShutDownApp()
        {
            RoutedEventArgs args = new RoutedEventArgs(ShutDownAppRoutedEvent, this);
            this.RaiseEvent(args);
        }


        private void btn_set_Click(object sender, RoutedEventArgs e)
        {
            this.OnSetClicked();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.OnCloseClicked();
        }

        private void btn_shutdown_Click(object sender, RoutedEventArgs e)
        {
            this.OnSetDownClicked();
        }

        private void btn_closeApp_Click(object sender, RoutedEventArgs e)
        {
            this.OnShutDownApp();
        }


    }
}
