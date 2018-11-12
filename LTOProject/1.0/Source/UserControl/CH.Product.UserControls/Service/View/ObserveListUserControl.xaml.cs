using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CH.Product.UserControls.Service.View
{
    /// <summary>
    /// ObserveListUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class ObserveListUserControl : UserControl
    {

        LeaveToObserveEngineViewModel _viewModel = new LeaveToObserveEngineViewModel();

        public ObserveListUserControl()
        {

            InitializeComponent();

            this.Loaded += ObserverListUserControl_Loaded;
        }

        private void ObserverListUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _viewModel;

            //var collection= ServiceProvider.Instance.GetList();

            //if (collection == null) return;

            //ObservableCollection<LeaveToObserveItemViewModel> items = new ObservableCollection<LeaveToObserveItemViewModel>();

            //foreach (var item in collection)
            //{
            //    items.Add(item);
            //}

            //_viewModel.Clear();

            //foreach (var item in items)
            //{
            //    _viewModel.AddItem(item);
            //}

            //_viewModel.RefreshData();
        }

        #region - 滚动条功能 -

        private void scrolls_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;

            this.ScrollMove(e.Source, sender as ScrollViewer);

        }

        private void scrolls_PreviewTouchMove(object sender, TouchEventArgs e)
        {
            this.ScrollMove(e.Source, sender as ScrollViewer);
        }

        Point last;
        void ScrollMove(object source, ScrollViewer scroll)
        {
            Point pp = Mouse.GetPosition(source as FrameworkElement);

            Point temp = (source as FrameworkElement).PointToScreen(pp);

            double y = temp.Y - last.Y;

            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - y);

            last = temp;

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

        //private void FTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    _viewModel.Instance_CallBackScanning(((TextBox)sender).Text);
        //}
    }
}
