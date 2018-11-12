
using LTO.Base.Theme.Style;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// PassWordControl.xaml 的交互逻辑
    /// </summary>
    public partial class PassWordControl : BaseUserControl
    {
        public PassWordControl()
        {
            InitializeComponent();
        }



        //声明和注册路由事件
        public static readonly RoutedEvent CancelClickedRoutedEvent =
            EventManager.RegisterRoutedEvent("CancelClicked", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(PassWordControl));
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


        //声明和注册路由事件
        public static readonly RoutedEvent loginClickedRoutedEvent =
            EventManager.RegisterRoutedEvent("loginClicked", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(PassWordControl));
        //CLR事件包装
        public event RoutedEventHandler loginClicked
        {
            add { this.AddHandler(loginClickedRoutedEvent, value); }
            remove { this.RemoveHandler(loginClickedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnloginClicked()
        {
            RoutedEventArgs args = new RoutedEventArgs(loginClickedRoutedEvent, this);
            this.RaiseEvent(args);
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.OnCancelClicked();
        }

        private void btn_sumit_Click(object sender, RoutedEventArgs e)
        {
            this.OnloginClicked();
        }



        //声明和注册路由事件
        public static readonly RoutedEvent PassWordGotFocusRoutedEvent =
            EventManager.RegisterRoutedEvent("PassWordGotFocus", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(PassWordControl));
        //CLR事件包装
        public event RoutedEventHandler PassWordGotFocus
        {
            add { this.AddHandler(PassWordGotFocusRoutedEvent, value); }
            remove { this.RemoveHandler(PassWordGotFocusRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnPassWordGotFocus()
        {
            RoutedEventArgs args = new RoutedEventArgs(PassWordGotFocusRoutedEvent, this);
            this.RaiseEvent(args);
        }


        private void BindPassWordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("BindPassWordBox_IsKeyboardFocusedChanged");

            this.OnPassWordGotFocus();
        }
    }
}
