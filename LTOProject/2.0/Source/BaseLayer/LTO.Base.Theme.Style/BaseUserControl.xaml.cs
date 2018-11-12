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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LTO.Base.Theme.Style
{
    /// <summary> 主页面控件基类 </summary>
    public partial class BaseUserControl : UserControl
    {

        static BaseUserControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BaseUserControl), new FrameworkPropertyMetadata(typeof(BaseUserControl)));
        }

        public bool IsShow
        {
            get { return (bool)GetValue(IsShowProperty); }
            set { SetValue(IsShowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowProperty =
            DependencyProperty.Register("IsShow", typeof(bool), typeof(BaseUserControl), new PropertyMetadata(true, (d, e) =>
             {
                 BaseUserControl control = d as BaseUserControl;

                 if (control == null) return;

                 bool config = (bool)e.NewValue;

                 bool old = (bool)e.OldValue;

                 if(old==false&&config==true)
                 {
                     control.OnShowed();
                 }

                 if (old == true && config == false)
                 {
                     control.OnHiddened();
                 }

                 //if (config)
                 //{
                 //    control.OnShowed();

                 //    //Storyboard board = control.FindResource("Story_LoadOpacity") Storyboard;
                 //}
                 //else
                 //{
                 //    control.OnHidden();
                 //}

             }));



        //声明和注册路由事件
        public static readonly RoutedEvent ShowedRoutedEvent =
            EventManager.RegisterRoutedEvent("Showed", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(BaseUserControl));
        //CLR事件包装
        public event RoutedEventHandler Showed
        {
            add { this.AddHandler(ShowedRoutedEvent, value); }
            remove { this.RemoveHandler(ShowedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnShowed()
        {
            RoutedEventArgs args = new RoutedEventArgs(ShowedRoutedEvent, this);
            this.RaiseEvent(args);
        }



        //声明和注册路由事件
        public static readonly RoutedEvent HiddenedRoutedEvent =
            EventManager.RegisterRoutedEvent("Hiddened", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(BaseUserControl));
        //CLR事件包装
        public event RoutedEventHandler Hiddened
        {
            add { this.AddHandler(HiddenedRoutedEvent, value); }
            remove { this.RemoveHandler(HiddenedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnHiddened()
        {
            RoutedEventArgs args = new RoutedEventArgs(HiddenedRoutedEvent, this);
            this.RaiseEvent(args);
        }


    }
}
