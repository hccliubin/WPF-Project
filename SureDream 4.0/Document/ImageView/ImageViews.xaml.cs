using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ImageView
{
    /// <summary>
    /// ImageView.xaml 的交互逻辑
    /// </summary>
    public partial class ImageViews : UserControl
    {
        int thumbWidth;
        int thumbHeight;
        double scale = 1;
        double imgWidth;
        double imgHeight;
        double hOffSetRate = 0;//滚动条横向位置横向百分比
        double vOffSetRate = 0;//滚动条位置纵向百分比

        Storyboard sb_ShowTools;
        Storyboard sb_HideTools;
        Storyboard sb_Tip;
        TransformGroup tfGroup;

        #region 依赖属性
        public static readonly DependencyProperty SourceProperty =
         DependencyProperty.Register("Source", typeof(ImageSource),
         typeof(ImageViews),
         new PropertyMetadata(new PropertyChangedCallback(OnSourcePropertyChanged)));

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }

            set { SetValue(SourceProperty, value); }
        }

        static void OnSourcePropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            if (sender != null && sender is ImageViews)
            {
                ImageViews view = (ImageViews)sender;
                if (args != null && args.NewValue != null)
                {
                    ImageSource imgSource = args.NewValue as ImageSource;
                    view.imgBig.Source = imgSource;
                    view.imgless.Source = imgSource;
                    view.GetImageWidthHeight();

                    view.SetFullImage();
                }
            }
        }
        #endregion



        public ImageViews()
        {
            InitializeComponent();

            sb_ShowTools = this.FindResource("Sb_ShowTools") as Storyboard;
            sb_HideTools = this.FindResource("Sb_HideTools") as Storyboard;
            sb_Tip = this.FindResource("sb_Tips") as Storyboard;
            tfGroup = this.FindResource("TfGroup") as TransformGroup;
            this.Loaded += MainWindow_Loaded;
            this.MouseEnter += ImageViews_MouseEnter;
            this.MouseLeave += ImageViews_MouseLeave;

            svImg.ScrollChanged += svImg_ScrollChanged;
            gridMouse.MouseWheel += svImg_MouseWheel;
            gridMouse.MouseLeftButtonDown += control_MouseLeftButtonDown;
            gridMouse.MouseLeftButtonUp += control_MouseLeftButtonUp;
            gridMouse.MouseMove += control_MouseMove;
            btnActualsize.Click += btnActualsize_Click;
            btnEnlarge.Click += btnEnlarge_Click;
            btnNarrow.Click += btnNarrow_Click;
            btnRotate.Click += btnRotate_Click;

            this.mask.LoationChanged += (l, k) =>
              {
                  //var position = e.GetPosition(img);
                  //double X = mouseXY.X - k.Left;
                  //double Y = mouseXY.Y - k.Top;
                  //mouseXY = position;
                  //if (X != 0)

                  _loationFlag = true;

                  svImg.ScrollToHorizontalOffset(k.Left * svImg.ScrollableWidth);
                  //if (Y != 0)
                  svImg.ScrollToVerticalOffset(k.Top * svImg.ScrollableHeight);


                  System.Diagnostics.Debug.WriteLine(k.Left);
                  System.Diagnostics.Debug.WriteLine(svImg.ScrollableWidth);
                  System.Diagnostics.Debug.WriteLine(k.Left * svImg.ScrollableWidth);
                  System.Diagnostics.Debug.WriteLine("说明");


                  //GetOffSetRate();

                  _loationFlag = false;

              };


            SetbtnActualsizeEnable();
        }

        bool _loationFlag = false;

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetImageWidthHeight();
        }

        public void GetImageWidthHeight()
        {
            this.UpdateLayout();

            if (imgWidth == 0.0 || imgHeight == 0.0)
            {
                imgWidth = control.ActualWidth;
                imgHeight = control.ActualHeight;
            }
        }

        void btnRotate_Click(object sender, RoutedEventArgs e)
        {
            if (imgWidth == 0 || imgHeight == 0)
                return;
            scale = 1;
            this.txtZoom.Text = ((int)(scale * 100)).ToString() + "%";
            if (sb_Tip != null)
                sb_Tip.Begin();
            SetbtnActualsizeEnable();
            btnNarrow.IsEnabled = true;
            btnEnlarge.IsEnabled = true;
            hOffSetRate = 0;
            vOffSetRate = 0;
            imgBig.Width = imgWidth;
            imgBig.Height = imgHeight;

            if (tfGroup != null)
            {
                var rotate = tfGroup.Children[2] as RotateTransform;
                rotate.Angle += 90;
            }

        }

        void btnNarrow_Click(object sender, RoutedEventArgs e)
        {
            if (imgWidth == 0 || imgHeight == 0)
                return;

            btnEnlarge.IsEnabled = true;
            if (scale < 0.15)
                return;
            scale = scale * (1 / 1.2);

            SetbtnActualsizeEnable();
            if (scale < 0.15)
            {
                btnNarrow.IsEnabled = false;
            }
            this.txtZoom.Text = ((int)(scale * 100)).ToString() + "%";
            if (sb_Tip != null)
                sb_Tip.Begin();
            SetImageByScale();

        }

        /// <summary> 设置初始图片为平铺整个控件 </summary>
        void SetFullImage()
        {
            if (imgWidth == 0 || imgHeight == 0)
                return;

            scale = 0.14;

            SetbtnActualsizeEnable();

            btnNarrow.IsEnabled = false;

            this.txtZoom.Text = ((int)(scale * 100)).ToString() + "%";

            if (sb_Tip != null)
                sb_Tip.Begin();
            SetImageByScale();
        }

        void btnEnlarge_Click(object sender, RoutedEventArgs e)
        {
            if (imgWidth == 0 || imgHeight == 0)
                return;

            btnNarrow.IsEnabled = true;
            if (scale > 16)
                return;
            scale = scale * 1.2;
            SetbtnActualsizeEnable();

            if (scale > 16)
            {
                btnEnlarge.IsEnabled = false;
            }
            this.txtZoom.Text = ((int)(scale * 100)).ToString() + "%";
            if (sb_Tip != null)
                sb_Tip.Begin();
            SetImageByScale();

        }

        void btnActualsize_Click(object sender, RoutedEventArgs e)
        {
            if (imgWidth == 0 || imgHeight == 0)
                return;

            scale = 1;
            imgBig.Width = imgWidth;
            imgBig.Height = imgHeight;
            SetbtnActualsizeEnable();
        }

        void ImageViews_MouseLeave(object sender, MouseEventArgs e)
        {
            if (imgWidth == 0 || imgHeight == 0)
                return;

            if (sb_ShowTools != null)
                sb_ShowTools.Pause();
            if (sb_HideTools != null)
                sb_HideTools.Begin();

        }

        void ImageViews_MouseEnter(object sender, MouseEventArgs e)
        {
            if (imgWidth == 0 || imgHeight == 0)
                return;

            if (sb_HideTools != null)
                sb_HideTools.Pause();
            if (sb_ShowTools != null)
                sb_ShowTools.Begin();
        }

        private bool mouseDown;
        private Point mouseXY;

        void control_MouseMove(object sender, MouseEventArgs e)
        {
            if (imgWidth == 0 || imgHeight == 0)
                return;

            var img = sender as Grid;
            if (img == null)
            {
                return;
            }
            if (mouseDown)
            {
                Domousemove(img, e);
            }
        }

        private void Domousemove(Grid img, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }

            var position = e.GetPosition(img);
            double X = mouseXY.X - position.X;
            double Y = mouseXY.Y - position.Y;
            mouseXY = position;
            if (X != 0)
                svImg.ScrollToHorizontalOffset(svImg.HorizontalOffset + X);
            if (Y != 0)
                svImg.ScrollToVerticalOffset(svImg.VerticalOffset + Y);

            GetOffSetRate();
        }
        void control_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var img = sender as Grid;
            if (img == null)
            {
                return;
            }
            img.ReleaseMouseCapture();
            mouseDown = false;
        }

        void control_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var img = sender as Grid;
            if (img == null)
            {
                return;
            }
            img.CaptureMouse();
            mouseDown = true;
            mouseXY = e.GetPosition(img);
        }

        void svImg_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (imgWidth == 0 || imgHeight == 0)
                return;

            if (scale < 0.15 && e.Delta < 0)
                return;
            if (scale > 16 && e.Delta > 0)
                return;
            scale = scale * (e.Delta > 0 ? 1.2 : 1 / 1.2);

            SetbtnActualsizeEnable();
            btnNarrow.IsEnabled = scale > 0.15;
            btnEnlarge.IsEnabled = scale < 16;

            this.txtZoom.Text = ((int)(scale * 100)).ToString() + "%";
            if (sb_Tip != null)
                sb_Tip.Begin();
            SetImageByScale();
        }

        private void SetImageByScale()
        {
            GetOffSetRate();
            imgBig.Width = scale * imgWidth;
            imgBig.Height = scale * imgHeight;
            SetOffSetByRate();
        }

        /// <summary>
        /// 通过滚动条位置百分比在放大缩小时设置滚动条位置
        /// </summary>
        private void SetOffSetByRate()
        {
            this.UpdateLayout();
            if (svImg.ScrollableWidth > 0)
            {
                double hOffSet = hOffSetRate * svImg.ScrollableWidth;
                svImg.ScrollToHorizontalOffset(hOffSet);
            }
            if (svImg.ScrollableHeight > 0)
            {
                double vOffSet = vOffSetRate * svImg.ScrollableHeight;
                svImg.ScrollToVerticalOffset(vOffSet);
            }
        }

        /// <summary>
        /// 获取滚动条滚动位置百分比
        /// </summary>
        private void GetOffSetRate()
        {
            if (svImg.ScrollableWidth > 0)
            {
                if (svImg.HorizontalOffset != 0)
                    hOffSetRate = svImg.HorizontalOffset / svImg.ScrollableWidth;
            }
            if (svImg.ScrollableHeight > 0)
            {
                if (svImg.VerticalOffset != 0)
                    vOffSetRate = svImg.VerticalOffset / svImg.ScrollableHeight;
            }
        }

        void svImg_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {

            if (imgWidth == 0 || imgHeight == 0)
                return;

            thumbWidth = (int)mask.ActualWidth;
            thumbHeight = (int)mask.ActualHeight;

            double timeH = svImg.ViewportHeight / (svImg.ViewportHeight + svImg.ScrollableHeight);
            double timeW = svImg.ViewportWidth / (svImg.ViewportWidth + svImg.ScrollableWidth);

            double w = thumbWidth * timeW;
            double h = thumbHeight * timeH;

            double offsetx = 0;
            double offsety = 0;
            if (svImg.ScrollableWidth == 0)
            {
                offsetx = 0;
            }
            else
            {
                offsetx = (w - thumbWidth) / svImg.ScrollableWidth * svImg.HorizontalOffset;
            }

            if (svImg.ScrollableHeight == 0)
            {
                offsety = 0;
            }
            else
            {
                offsety = (h - thumbHeight) / svImg.ScrollableHeight * svImg.VerticalOffset;
            }


            Rect rect = new Rect(-offsetx, -offsety, w, h);

            mask.UpdateSelectionRegion(rect);
            //throw new NotImplementedException();
        }

        #region 设置工具栏按钮可用
        /// <summary>
        /// 设置1:1按钮可用
        /// </summary>
        private void SetbtnActualsizeEnable()
        {
            btnActualsize.IsEnabled = (int)(scale * 100) != 100;
        }
        #endregion
    }
}
