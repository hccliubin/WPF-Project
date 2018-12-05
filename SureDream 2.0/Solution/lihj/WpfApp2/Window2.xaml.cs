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
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        //移动标志
        bool trackingMouseMove = false;

        //鼠标按下去的位置
        Point mousePosition;

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AdjustBigImage();
        }

        /// <summary>
        /// 半透明矩形框鼠标左键按下
        /// </summary>
        private void MoveRect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            mousePosition = e.GetPosition(element);
            trackingMouseMove = true;
            if (null != element)
            {
                //强制获取此元素
                element.CaptureMouse();
                element.Cursor = Cursors.Hand;
            }
        }

        /// <summary>
        /// 半透明矩形框鼠标左键弹起
        /// </summary>
        private void MoveRect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            trackingMouseMove = false;
            element.ReleaseMouseCapture();
            mousePosition.X = mousePosition.Y = 0;
            element.Cursor = null;
        }

        /// <summary>
        /// 半透明矩形框移动
        /// </summary>        
        private void MoveRect_MouseMove(object sender, MouseEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (trackingMouseMove)
            {
                //计算鼠标在X轴的移动距离
                double deltaV = e.GetPosition(element).Y - mousePosition.Y;
                //计算鼠标在Y轴的移动距离
                double deltaH = e.GetPosition(element).X - mousePosition.X;
                //得到图片Top新位置
                double newTop = deltaV + (double)element.GetValue(Canvas.TopProperty);
                //得到图片Left新位置
                double newLeft = deltaH + (double)element.GetValue(Canvas.LeftProperty);

                //边界的判断
                if (newLeft <= 0)
                {
                    newLeft = 0;
                }

                //左侧图片框宽度 - 半透明矩形框宽度
                if (newLeft >= (this.SmallBox.Width - this.MoveRect.Width))
                {
                    newLeft = this.SmallBox.Width - this.MoveRect.Width;
                }

                if (newTop <= 0)
                {
                    newTop = 0;
                }

                //左侧图片框高度度 - 半透明矩形框高度度
                if (newTop >= this.SmallBox.Height - this.MoveRect.Height)
                {
                    newTop = this.SmallBox.Height - this.MoveRect.Height;
                }
                element.SetValue(Canvas.TopProperty, newTop);
                element.SetValue(Canvas.LeftProperty, newLeft);
                AdjustBigImage();
                if (mousePosition.X <= 0 || mousePosition.Y <= 0) { return; }
            }
        }

        /// <summary>
        /// 调整右侧大图的位置
        /// </summary>
        void AdjustBigImage()
        {
            //获取右侧大图框与透明矩形框的尺寸比率
            double n = this.BigBox.Width / this.MoveRect.Width;

            //获取半透明矩形框在左侧小图中的位置
            double left = (double)this.MoveRect.GetValue(Canvas.LeftProperty);
            double top = (double)this.MoveRect.GetValue(Canvas.TopProperty);

            //计算和设置原图在右侧大图框中的Canvas.Left 和 Canvas.Top
            double newLeft = -left * n;
            double newTop = -top * n;
            bigImg.SetValue(Canvas.LeftProperty, newLeft);
            bigImg.SetValue(Canvas.TopProperty, newTop);
        }

    }
}
