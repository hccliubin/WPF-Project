using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace CH.Product.UserControls.Service
{
    /// <summary>
    /// ObserverListUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class ObserverListUserControl : UserControl
    {

        LeaveToObserveEngineViewModel _viewModel = new LeaveToObserveEngineViewModel();

        public ObserverListUserControl()
        {

            LeaveToObserveEngine.Instance.RunDemo();

            if (LeaveToObserveEngine.Instance.Collection == null) return;

            ObservableCollection<LeaveToObserveItemViewModel> items = new ObservableCollection<LeaveToObserveItemViewModel>();

            foreach (var item in LeaveToObserveEngine.Instance.Collection)
            {
                items.Add(item);
            }

            _viewModel.Collection.Clear();

            foreach (var item in items)
            {
                _viewModel.AddItem(item);
            }

            this.DataContext = _viewModel;

            InitializeComponent();

        }

        #region - 滚动条功能 -

        private void scrolls_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;

            this.ScrollMove(e.Source);

        }

        private void scrolls_PreviewTouchMove(object sender, TouchEventArgs e)
        {
            this.ScrollMove(e.Source);
        }

        Point last;
        void ScrollMove(object source)
        {
            Point pp = Mouse.GetPosition(source as FrameworkElement);

            Point temp = (source as FrameworkElement).PointToScreen(pp);

            double y = temp.Y - last.Y;

            this.scrolls.ScrollToVerticalOffset(this.scrolls.VerticalOffset - y);

            last = temp;

            this.CheckPosition();

        }

        void GetLast(object source)
        {
            Point pp = Mouse.GetPosition(source as FrameworkElement);

            Point temp = (source as FrameworkElement).PointToScreen(pp);

            last = temp;
        }

        private void scrolls_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            this.GetLast(e.Source);
        }

        private void scrolls_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.GetLast(e.Source);
        }

        private void up_Click(object sender, RoutedEventArgs e)
        {
            //this.scrolls.LineUp();

            this.scrolls.ScrollToVerticalOffset(this.scrolls.VerticalOffset - 120);

            this.CheckPosition();
        }

        private void down_Click(object sender, RoutedEventArgs e)
        {
            //this.scrolls.LineDown();

            this.scrolls.ScrollToVerticalOffset(this.scrolls.VerticalOffset + 120);

            this.CheckPosition();
        }

        void CheckPosition()
        {
            if (this.scrolls.VerticalOffset == 0)
            {
                //this.up.Visibility = Visibility.Hidden;
            }

            else if (IsVerticalScrollBarAtButtom(this.scrolls))
            {
                //this.down.Visibility = Visibility.Hidden;
            }
            else
            {
                //this.up.Visibility = Visibility.Visible;

                //this.down.Visibility = Visibility.Visible;
            }

        }

        public bool IsVerticalScrollBarAtButtom(ScrollViewer s)
        {
            bool isAtButtom = false;
            double dVer = s.VerticalOffset;
            double dViewport = s.ViewportHeight;
            double dExtent = s.ExtentHeight;
            if (dVer != 0)
            {
                if (dVer + dViewport == dExtent)
                {
                    isAtButtom = true;
                }
                else
                {
                    isAtButtom = false;
                }
            }
            else
            {
                isAtButtom = false;
            }

            return isAtButtom;
        }


        #endregion




    }
}
