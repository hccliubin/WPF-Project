using LTO.Base.Theme.Style;
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

namespace LTO.Base.Theme.Style
{
    /// <summary> 消息输入页 </summary>
    public partial class MessageSingleControl : BaseUserControl
    {

        public static MessageSingleControl Instance;


        private MessageResult _result = MessageResult.Cancel;
        /// <summary> 倒计时结束返回-9 点击取消返回1 </summary>
        public MessageResult Result
        {
            get { return _result; }
            set { _result = value; }
        }

        /// <summary> 显示提示信息 可设置倒计时和注册退出事件 </summary>
        public static void Show(string message, int count = 3, Action<MessageResult> ResultAction = null)
        {
            if (Instance != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                 {
                     Instance.Title = message;

                     Instance.Message = string.Empty;

                     Instance.txt_message.Visibility = Visibility.Collapsed;

                     Instance.image.Source = new BitmapImage(new Uri("Image/打印失败.png", UriKind.Relative));

                     Instance.Count = count;

                     Instance.OnResult = ResultAction;

                     Instance.stack_buttons.Children.Clear();

                     Instance.cts.Cancel();

                     Instance.IsShow = true;
                 });
            }
        }

        /// <summary> 显示提示信息 可设置倒计时和注册退出事件 </summary>
        public static void ShowWithError(string tiltle, string message, int count = 3, Action<MessageResult> ResultAction = null)
        {
            if (Instance != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Instance.Message = message;

                    Instance.txt_message.Visibility = Visibility.Visible;

                    Instance.image.Source = new BitmapImage(new Uri("Image/打印失败.png", UriKind.Relative));

                    Instance.Count = count;

                    Instance.Title = tiltle;

                    Instance.OnResult = ResultAction;

                    Instance.stack_buttons.Children.Clear();

                    Instance.cts.Cancel();

                    Instance.IsShow = true;
                });
            }
        }

        /// <summary> 显示提示信息 可设置倒计时和注册退出事件 </summary>
        public static void ShowWithSuccess(string tiltle, string message, int count = 3, Action<MessageResult> ResultAction = null)
        {
            if (Instance != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Instance.Message = message;

                    Instance.Count = count;

                    Instance.Title = tiltle;

                    Instance.txt_message.Visibility = Visibility.Visible;

                    Instance.image.Source = new BitmapImage(new Uri("Image/查询成功.png", UriKind.Relative));

                    Instance.OnResult = ResultAction;

                    Instance.stack_buttons.Children.Clear();

                    Instance.cts.Cancel();

                    Instance.IsShow = true;
                });
            }
        }

        /// <summary> 带有取消按钮的提示框 </summary>
        public static void ShowWithCancel(string message, int count = 5, Action<MessageResult> ResultAction = null)
        {
            Action<MessageSingleControl> btn = l =>
            {
                l.Result = MessageResult.Cancel;
                l.IsShow = false;
                //if (ResultAction != null)
                //{
                //    ResultAction(MessageResult.Cancel);
                //}
            };

            Tuple<string, Action<MessageSingleControl>> buttuon = new Tuple<string, Action<MessageSingleControl>>("取消", btn);

            ShowWith(message, count, ResultAction, buttuon);
        }

        /// <summary> 带有取消按钮的提示框 </summary>
        public static void ShowWithSumit(string message, int count = 5, Action<MessageResult> ResultAction = null)
        {
            Action<MessageSingleControl> btn = l =>
            {
                l.Result = MessageResult.Cancel;
                l.IsShow = false;
                //if (ResultAction != null)
                //{
                //    ResultAction(MessageResult.Cancel);
                //}
            };

            Tuple<string, Action<MessageSingleControl>> buttuon = new Tuple<string, Action<MessageSingleControl>>("确定", btn);

            ShowWith(message, count, ResultAction, buttuon);
        }

        /// <summary> 带有取消按钮的提示框 </summary>
        public static void ShowWithCancelAndSumit(string message, int count = 5, Action<MessageResult> ResultAction = null)
        {
            Action<MessageSingleControl> btn = l =>
            {
                l.Result = MessageResult.Cancel;
                l.IsShow = false;
            };

            Tuple<string, Action<MessageSingleControl>> buttuon = new Tuple<string, Action<MessageSingleControl>>("取消", btn);

            Action<MessageSingleControl> btn1 = l =>
            {
                l.Result = MessageResult.Sumit;
                l.IsShow = false;

            };

            Tuple<string, Action<MessageSingleControl>> buttuon1 = new Tuple<string, Action<MessageSingleControl>>("开始", btn1);

            ShowWith(message, count, ResultAction, buttuon, buttuon1);
        }

        /// <summary> 显示提示框 可以自定义按钮 </summary>
        public static void ShowWith(string message, int count = 5, Action<MessageResult> ResultAction = null, params Tuple<string, Action<MessageSingleControl>>[] buttons)
        {
            if (Instance != null)
            {
                Instance.IsShow = false;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Instance.Title = message;

                    Instance.Message = string.Empty;

                    Instance.txt_message.Visibility = Visibility.Collapsed;

                    Instance.image.Source = new BitmapImage(new Uri("Image/打印失败.png", UriKind.Relative));

                    Instance.Count = count;

                    if (count > 0)
                    {
                        Instance.border.MouseUp += Instance.Border_MouseUp;
                    }
                    else
                    {
                        Instance.border.MouseUp -= Instance.Border_MouseUp;
                        Instance.CountDown = string.Empty;
                    }

                    Instance.OnResult = ResultAction;

                    Instance.InitButtons(buttons);

                    Instance.cts.Cancel();

                    Instance.IsShow = true;
                });
            }
        }

        public Action<MessageResult> OnResult;

        public MessageSingleControl()
        {
            InitializeComponent();

            this.Showed += MessageSingleControl_Showed;

            this.Hiddened += MessageSingleControl_Hiddened;

        }

        void InitButtons(params Tuple<string, Action<MessageSingleControl>>[] buttons)
        {
            stack_buttons.Children.Clear();

            foreach (var item in buttons)
            {
                Button button = new Button();
                button.Content = item.Item1;
                button.Height = 60;
                button.Margin = new Thickness(10, 5, 10, 5);
                button.Click += (l, k) =>
              {
                  item.Item2(this);
              };

                if (item.Item1 == "开始" || item.Item1 == "确定")
                {
                    button.Style = this.FindResource("NoticeButtonStyle") as System.Windows.Style;
                }
                else
                {
                    button.Style = this.FindResource("DefultButtonStyle") as System.Windows.Style;
                }


                stack_buttons.Children.Add(button);
            }

        }


        CancellationTokenSource cts = new CancellationTokenSource();

        object locker = new object();
        private void MessageSingleControl_Showed(object sender, RoutedEventArgs e)
        {
            lock (locker)
            {
                cts.Cancel();

                cts = new CancellationTokenSource();

                int result = Count + 1;

                if (Count < 0) return;

                Action action = () =>
                  {
                      while (true)
                      {
                          System.Diagnostics.Debug.WriteLine(cts.IsCancellationRequested);

                          if (cts.IsCancellationRequested) return;

                          Application.Current.Dispatcher.Invoke(() =>
                          {
                              result--;

                              this.CountDown = (result).ToString();

                              if (result <= 1)
                              {
                                  this.Result = MessageResult.TimeOver;

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
        }

        [Obsolete]
        void BegionDownCount()
        {
            cts.Cancel();
            cts = new CancellationTokenSource();

            int result = Count + 1;

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

                            this.Result = MessageResult.TimeOver;

                            return;
                        }
                    });

                    Thread.Sleep(1000);

                }
            };


            Task task = new Task(action, cts.Token);

            task.Start();
        }
        [Obsolete]
        void AsyncDownCount()
        {
            cts.Cancel();
            cts = new CancellationTokenSource();

            int result = Count + 1;

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

                            this.Result = MessageResult.TimeOver;

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

            if (OnResult != null)
            {
                OnResult(this.Result);
            }

            lock (locker)
            {
                cts.Cancel();
            }
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



        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(MessageSingleControl), new PropertyMetadata(default(string), (d, e) =>
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
            this.Result = MessageResult.Cancel;
            this.OnCountDownEnd();
        }
    }


    public enum MessageResult
    {
        Cancel = 0,
        TimeOver = -9,
        Sumit = 1
    }
}
