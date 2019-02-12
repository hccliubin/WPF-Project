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

namespace WpfScaleTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TransformGroup group = new TransformGroup();
            ScaleTransform st = new ScaleTransform();
            TranslateTransform tt = new TranslateTransform();
            group.Children.Add(st);
            group.Children.Add(tt);

            ZoomGrid.RenderTransform = group;
            ZoomGrid.RenderTransformOrigin = new Point(0.0, 0.0);

            ZoomGrid.MouseWheel += child_MouseWheel;
            ZoomGrid.MouseLeftButtonDown += child_MouseLeftButtonDown;
            ZoomGrid.MouseLeftButtonUp += child_MouseLeftButtonUp;
            ZoomGrid.MouseMove += child_MouseMove;

            ZoomCanvas.MouseLeftButtonDown += ZoomCanvas_MouseLeftButtonDown;
        }
        private void ZoomCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                this.Reset();
            }
        }
        public void Reset()
        {
            // reset zoom
            var st = GetScaleTransform(ZoomGrid);
            st.ScaleX = 1.0;
            st.ScaleY = 1.0;

            // reset pan
            var tt = GetTranslateTransform(ZoomGrid);
            tt.X = 0.0;
            tt.Y = 0.0;
        }
        private bool IsMoveRectangle;
        /// <summary>
        /// Ctrl状态
        /// </summary>
        private bool CtrlDown;
        private Point origin;
        private Point start;
        private void child_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsMoveRectangle)
            {
                if (CtrlDown)
                {
                    Point drawPoint = (Point)e.GetPosition(ZoomGrid);
                    //if (CurrentRectangle != null & e.LeftButton == MouseButtonState.Pressed)
                    //{
                    //    CurrentRectangle.Width = drawPoint.X - CurrentRectangle.Margin.Left > 0 ? drawPoint.X - CurrentRectangle.Margin.Left : 0;
                    //    CurrentRectangle.Height = drawPoint.Y - CurrentRectangle.Margin.Top > 0 ? drawPoint.Y - CurrentRectangle.Margin.Top : 0;
                    //    //CurrentRectangle.Width = drawPoint.X - CurrentRectangle.Margin.Left < 0 ? Width = CurrentRectangle.Width : drawPoint.X - CurrentRectangle.Margin.Left;
                    //    //CurrentRectangle.Height = drawPoint.Y - CurrentRectangle.Margin.Top < 0 ? CurrentRectangle.Height : drawPoint.Y - CurrentRectangle.Margin.Top;
                    //}
                }
                else
                {
                    if (ZoomGrid.IsMouseCaptured)
                    {
                        var tt = GetTranslateTransform(ZoomGrid);
                        Vector v = start - e.GetPosition(ZoomCanvas);
                        tt.X = origin.X - v.X;
                        tt.Y = origin.Y - v.Y;
                    }
                }
            }
        }
        private void child_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsMoveRectangle)
            {
                ZoomGrid.ReleaseMouseCapture();
                ZoomCanvas.Cursor = Cursors.Arrow;
            }
        }
        private void child_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsMoveRectangle)
            {
                if (CtrlDown)
                {
                    Point clickPoint = (Point)e.GetPosition(ZoomGrid);
                    //CurrentRectangle.Visibility = Visibility.Visible;
                    //CurrentRectangle.Margin = new Thickness(clickPoint.X, clickPoint.Y, 0, 0);
                }
                else
                {
                    var tt = GetTranslateTransform(ZoomGrid);
                    start = e.GetPosition(ZoomCanvas);
                    origin = new Point(tt.X, tt.Y);
                    ZoomCanvas.Cursor = Cursors.Hand;
                    ZoomGrid.CaptureMouse();
                }
            }
        }
        private void child_MouseWheel(object sender, MouseWheelEventArgs e)
        {

            var st = GetScaleTransform(ZoomGrid);
            var tt = GetTranslateTransform(ZoomGrid);

            double zoom = e.Delta > 0 ? .2 : -.2;
            if (!(e.Delta > 0) && (st.ScaleX < .4 || st.ScaleY < .4))
                return;
            if (e.Delta > 0 && (st.ScaleX > 1.4 || st.ScaleY > 1.4))
                return;

            Point relative = e.GetPosition(ZoomGrid);
            double abosuluteX;
            double abosuluteY;

            abosuluteX = relative.X * st.ScaleX + tt.X;
            abosuluteY = relative.Y * st.ScaleY + tt.Y;

            st.ScaleX += zoom;
            st.ScaleY += zoom;

            tt.X = abosuluteX - relative.X * st.ScaleX;
            tt.Y = abosuluteY - relative.Y * st.ScaleY;
        }
        /// <summary>
        /// true放大
        /// </summary>
        /// <param name="isMaxZoom"></param>
        private void MaxZoomHandle()
        {

            TransformGroup tg = ZoomGrid.RenderTransform as TransformGroup;
            var tgnew = tg.CloneCurrentValue();
            if (tgnew != null)
            {
                ScaleTransform st = tgnew.Children[0] as ScaleTransform;
                ZoomGrid.RenderTransformOrigin = new Point(0.5, 0.5);
                if (st.ScaleX > 0 && st.ScaleX <= 2.0)
                {
                    st.ScaleX += 0.05;
                    st.ScaleY += 0.05;
                }
                else if (st.ScaleX < 0 && st.ScaleX >= -2.0)
                {
                    st.ScaleX -= 0.05;
                    st.ScaleY += 0.05;
                }
            }

            // 重新给图像赋值Transform变换属性
            ZoomGrid.RenderTransform = tgnew;

        }
        private void MinZoomHandle()
        {
            TransformGroup tg = ZoomGrid.RenderTransform as TransformGroup;
            var tgnew = tg.CloneCurrentValue();
            if (tgnew != null)
            {
                ScaleTransform st = tgnew.Children[0] as ScaleTransform;
                ZoomGrid.RenderTransformOrigin = new Point(0.5, 0.5);
                if (st.ScaleX >= 0.2)
                {
                    st.ScaleX -= 0.05;
                    st.ScaleY -= 0.05;
                }
                else if (st.ScaleX <= -0.2)
                {
                    st.ScaleX += 0.05;
                    st.ScaleY -= 0.05;
                }
            }

            // 重新给图像赋值Transform变换属性
            ZoomGrid.RenderTransform = tgnew;

        }
        private ScaleTransform GetScaleTransform(UIElement element)
        {
            return (ScaleTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is ScaleTransform);
        }
        private TranslateTransform GetTranslateTransform(UIElement element)
        {
            return (TranslateTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is TranslateTransform);
        }

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog =
                 new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "图片|*.jpg;*.png;*.gif;*.jpeg;*.bmp";
            if (dialog.ShowDialog() == true)
            {
                img.ImageSource =new BitmapImage(new Uri(dialog.FileName, UriKind.Absolute));
            }
        }
    }
}
