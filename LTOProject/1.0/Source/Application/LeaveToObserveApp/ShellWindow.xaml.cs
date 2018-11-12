using HeBianGu.General.WpfControlLib;
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
using System.Windows.Shapes;

namespace CH.Product.LeaveToObserveApp
{
    /// <summary>
    /// ShellWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShellWindow : Window
    {
        public ShellWindow()
        {
            InitializeComponent();
        }

        #region - 关闭功能 -

        DateTime _last;

        bool isSetDown;

        void CloseMethod()
        {
            // Todo ：提示的按钮和按钮的事件 
            Tuple<string, Action<MessageWindow>> item1 = new Tuple<string, Action<MessageWindow>>("关机", l => l.Result = false);

            // Todo ：提示的按钮和按钮的事件 
            Tuple<string, Action<MessageWindow>> item2 = new Tuple<string, Action<MessageWindow>>("确定", l => l.Result = true);

            // Todo ：提示的按钮和按钮的事件 
            Tuple<string, Action<MessageWindow>> item3 = new Tuple<string, Action<MessageWindow>>("取消", l => l.Result = false);

            var r = MessageWindow.ShowDialogWith("确定要退出系统", "温馨提示！", item1, item2, item3);

            if (r == 0)
            {

                // Todo ：提示的按钮和按钮的事件 
                var result = MessageWindow.ShowDialog("确定要关机", "温馨提示！");

                if (result)
                {
                    // Todo ：关机 
                    WinAPIServer.Instance.PowerOff();
                }

                return;

            }

            if (r == 1)
            {
                // Todo ：动画完成后关闭程序 
                Action completedAction = () =>
                {
                    try
                    {

                        this.Dispatcher.Invoke(() =>
                        {
                            //this.Dispose();
                            //Application.Current.Shutdown();
                            Environment.Exit(0);

                        });
                    }
                    catch
                    {

                    }
                };

                // Todo ：确定 

                try
                {
                    //this.Dispatcher.Invoke(() => StoryProvider.DownToUpOpsClose(this, completedAction));

                    this.Close();

                    //this.Dispose();

                    //Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    this.Dispatcher.Invoke(() => MessageWindow.ShowSumit(ex.Message));
                }


            }

            if (r == 2)
            {
                // Todo ：取消 
                return;
            }
        }

        private void titleBorder_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isSetDown = false;
        }

        private void titleBorder_TouchDown(object sender, TouchEventArgs e)
        {

        }

        private void titleBorder_TouchUp(object sender, TouchEventArgs e)
        {
            isSetDown = false;
        }


        private void titleBorder_TouchLeave(object sender, TouchEventArgs e)
        {
            isSetDown = false;
        }


        private void titleBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.RectangleFuntion();

        }

        /// <summary> 弹出设置窗口 </summary>
        void RectangleFuntion()
        {
            isSetDown = true;

            _last = DateTime.Now;

            Func<bool> match = () =>
            {
                return (DateTime.Now - _last).TotalSeconds > 2;
            };

            Func<bool> stopMatch = () =>
            {
                return isSetDown == false;
            };

            Action act = () =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    try
                    {
                        this.CloseMethod();
                    }
                    catch (Exception ex)
                    {
                        MessageWindow.ShowSumit(ex.Message);
                    }
                });
            };


            TimerSplitService.Instance.WaitThread(match, act, stopMatch);
        }

        #endregion
    }
}
