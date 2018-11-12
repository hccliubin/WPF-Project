using HEW.Base.Theme.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace HEW.UserControls.General
{
    /// <summary> 消息输入页 </summary>
    public partial class MessageSingleControl : BaseUserControl
    {
        public MessageSingleControl()
        {
            InitializeComponent();

            this.Showed += MessageSingleControl_Showed;

            this.Hiddened += MessageSingleControl_Hiddened;

        }


        CancellationTokenSource cts = new CancellationTokenSource();

        private void MessageSingleControl_Showed(object sender, RoutedEventArgs e)
        {

            cts = new CancellationTokenSource();

            int result = Count+1;

            Action action = () =>
              {
                  while (true)
                  {

                      if (cts.IsCancellationRequested) return;

                      Application.Current.Dispatcher.Invoke(() =>
                      {
                          result--;

                          this.CountDown = (result).ToString();

                          if (result <= 1)
                          {
                              this.OnCountDownEnd();

                              return;
                          }
                      });

                      Thread.Sleep(1000);

                  }
              };


            Task task = new Task(action, cts.Token);

            task.Start();

        }
        private void MessageSingleControl_Hiddened(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }

        /// <summary> 消息 </summary>
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(MessageSingleControl), new PropertyMetadata(default(string), (d, e) =>
             {
                 MessageSingleControl control = d as MessageSingleControl;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        private int _count;
        /// <summary> 倒计时次数 </summary>
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        /// <summary> 当前倒计时 </summary>
        public string CountDown
        {
            get { return (string)GetValue(CountDownProperty); }
            set { SetValue(CountDownProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CountDownProperty =
            DependencyProperty.Register("CountDown", typeof(string), typeof(MessageSingleControl), new PropertyMetadata("5", (d, e) =>
             {
                 MessageSingleControl control = d as MessageSingleControl;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));



        //声明和注册路由事件
        public static readonly RoutedEvent CountDownEndRoutedEvent =
            EventManager.RegisterRoutedEvent("CountDownEnd", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(MessageSingleControl));
        //CLR事件包装
        public event RoutedEventHandler CountDownEnd
        {
            add { this.AddHandler(CountDownEndRoutedEvent, value); }
            remove { this.RemoveHandler(CountDownEndRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnCountDownEnd()
        {
            RoutedEventArgs args = new RoutedEventArgs(CountDownEndRoutedEvent, this);
            this.RaiseEvent(args);
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.OnCountDownEnd();
        }
    }
}
