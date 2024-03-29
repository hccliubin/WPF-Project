﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ty.Component.ImageControl;
using Ty.Component.ImageControl.Provider.Hook;

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

            //this.MouseEnter += ImageViews_MouseEnter;
            //this.MouseLeave += ImageViews_MouseLeave;

            //  ToEdit ：设置工具栏显示的控件
            this.grid_all.MouseEnter += ImageViews_MouseEnter;
            this.grid_all.MouseLeave += ImageViews_MouseLeave;

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

                  //_loationFlag = true;

                  svImg.ScrollToHorizontalOffset(k.Left * svImg.ExtentWidth);
                  svImg.ScrollToVerticalOffset(k.Top * svImg.ExtentHeight);

                  //_loationFlag = false;

              };



            SetbtnActualsizeEnable();

            //  Message：注册鼠标悬停事件，注意删除和新增的时候
            mouseEventHandler = (l, k) =>
            {
                RectangleShape rectangleShape = l as RectangleShape;

                ShowPartWithShape(rectangleShape);
            };


            this.RegisterDefaltApi();
        }

        //bool _loationFlag = false;

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

            this.SetRotateRight();
        }

        void Rotate(double angle)
        {
            if (imgWidth == 0 || imgHeight == 0)
                return;

            if (false)
            {
                //  Message：设置1:1并旋转
                Scale = 1;
                this.txtZoom.Text = ((int)(Scale * 100)).ToString() + "%";
            }
            //  Message：设置平铺并旋转
            this.SetFullImage();

            if (sb_Tip != null)
                sb_Tip.Begin();
            SetbtnActualsizeEnable();
            btnNarrow.IsEnabled = true;
            btnEnlarge.IsEnabled = true;
            hOffSetRate = 0;
            vOffSetRate = 0;

            //imgBig.Width = imgWidth;
            //imgBig.Height = imgHeight;

            //  Message：设置平铺并旋转
            //vb.Width = imgWidth;
            //vb.Height = imgHeight;

            if (tfGroup != null)
            {
                var rotate = tfGroup.Children[2] as RotateTransform;
                rotate.Angle += angle;
            }
        }

        void btnNarrow_Click(object sender, RoutedEventArgs e)
        {
            this.SetNarrow();

        }

        /// <summary> 设置初始图片为平铺整个控件 </summary>
        void SetFullImage()
        {
            if (imgWidth == 0 || imgHeight == 0)
                return;

            Scale = 0.02;

            Scale = svImg.ActualWidth / imgWidth;

            Scale = Math.Min(Scale, svImg.ActualHeight / imgHeight);

            SetbtnActualsizeEnable();

            btnNarrow.IsEnabled = false;

            this.txtZoom.Text = ((int)(Scale * 100)).ToString() + "%";

            if (sb_Tip != null) sb_Tip.Begin();

            SetImageByScale();
        }

        void btnEnlarge_Click(object sender, RoutedEventArgs e)
        {
            this.SetEnlarge();

        }

        void btnActualsize_Click(object sender, RoutedEventArgs e)
        {
            if (imgWidth == 0 || imgHeight == 0)
                return;

            Scale = 1;

            //imgBig.Width = imgWidth;
            //imgBig.Height = imgHeight;

            vb.Width = imgWidth;
            vb.Height = imgHeight;

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

        private System.Windows.Point mouseXY;

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
            if (this.btn_wheel.IsChecked.HasValue && this.btn_wheel.IsChecked.Value)
            {
                if (e.Delta > 0)
                {
                    this.OnNextClick();
                }
                else
                {
                    this.OnLastClicked();
                }
            }
            else
            {
                if (imgWidth == 0 || imgHeight == 0) return;

                if (Scale < 0.15 && e.Delta < 0) return;

                if (Scale > 16 && e.Delta > 0) return;

                Scale = Scale * (e.Delta > 0 ? 1.2 : 1 / 1.2);

                SetbtnActualsizeEnable();

                btnNarrow.IsEnabled = Scale > 0.15;

                btnEnlarge.IsEnabled = Scale < 16;

                this.txtZoom.Text = ((int)(Scale * 100)).ToString() + "%";

                if (sb_Tip != null)
                    sb_Tip.Begin();

                SetImageByScale();

            }
        }

        private void SetImageByScale()
        {
            GetOffSetRate();

            //imgBig.Width = scale * imgWidth;
            //imgBig.Height = scale * imgHeight;


            vb.Width = Scale * imgWidth;
            vb.Height = Scale * imgHeight;

            //vb.Width = Scale * this.control.ActualWidth;
            //vb.Height = Scale * this.control.ActualHeight;

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
            btnActualsize.IsEnabled = (int)(Scale * 100) != 100;
        }
        #endregion


        MarkType _markType;
        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InkCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.ViewModel == null) return;

            if ((this._markType == MarkType.None)) return;

            //if ((this.ImageOprateCtrEntity.MarkType == MarkType.None)) return;
            _dynamic.BegionMatch(true);

            start = e.GetPosition(sender as InkCanvas);


        }

        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InkCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.ViewModel == null) return;

            if ((this._markType == MarkType.None)) return;

            if (e.LeftButton != MouseButtonState.Pressed) return;

            if (this.start.X <= 0) return;

            System.Windows.Point end = e.GetPosition(this.canvas);

            //this._isMatch = Math.Abs(start.X - end.X) > 50 && Math.Abs(start.Y - end.Y) > 50;

            _dynamic.Visibility = Visibility.Visible;

            _dynamic.Refresh(start, end);

        }

        /// <summary>
        /// 鼠标抬起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InkCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if ((this._markType == MarkType.None)) return;

            //  Do：检查选择区域是否可用
            if (!_dynamic.IsMatch())
            {
                _dynamic.Visibility = Visibility.Collapsed;
                return;
            };

            if (this.start.X <= 0) return;

            //  Do：如果是选择局部放大
            //if (this.r_screen.IsChecked.HasValue && this.r_screen.IsChecked.Value)
            if (this._markType == MarkType.Enlarge)
            {

                this.ShowScaleWithRect(this._dynamic.Rect);

                _dynamic.Visibility = Visibility.Collapsed;
            }
            else
            {
                ImgMarkEntity imgMarkEntity = new ImgMarkEntity();

                this.DrawMarkedMouseUp?.Invoke(imgMarkEntity, this._markType);
            }

            //  Do：将数据初始化
            start = new System.Windows.Point(-1, -1);


        }

        void ShowScaleWithRect(Rect rect)
        {

            if (imgWidth == 0 || imgHeight == 0)
                return;

            double percentX = rect.X / this.canvas.ActualWidth;

            double percentY = rect.Y / this.canvas.ActualHeight;

            double timeW = rect.Width / this.canvas.ActualWidth;
            double timeH = rect.Height / this.canvas.ActualHeight;

            double w = mask.ActualWidth * timeW;
            double h = mask.ActualHeight * timeH;


            //  Message：设置缩放比例
            Scale = Math.Min(svImg.ActualWidth / imgWidth, svImg.ActualHeight / imgHeight);

            Scale = Scale / Math.Min(timeW, timeH);

            this.txtZoom.Text = ((int)(Scale * 100)).ToString() + "%";

            if (sb_Tip != null) sb_Tip.Begin();

            SetImageByScale();

            //  Message：更改区域位置
            Rect rectMark = new Rect(percentX * mask.ActualWidth, percentY * mask.ActualHeight, w, h);

            Debug.WriteLine(rectMark.Width);
            Debug.WriteLine(rectMark.Height);

            mask.UpdateSelectionRegion(rectMark, true);

            //svImg.ScrollToHorizontalOffset(percentX * svImg.ExtentWidth);
            //svImg.ScrollToVerticalOffset(percentY * svImg.ExtentHeight);


        }

        #region - 成员变量 -

        /// <summary>
        /// 绑定模型
        /// </summary>
        public ImageControlViewModel ViewModel
        {
            get
            {
                if (this.DataContext is ImageControlViewModel)
                {
                    return (ImageControlViewModel)this.DataContext;
                }

                return null;
            }
            set
            {
                this.DataContext = value;
            }
        }

        System.Windows.Point start;

        #endregion


        //public void Test()
        //{
        //    this.SetMarkType(MarkType.Sample);

        //    this.ViewModel = new ImageControlViewModel(null);
        //}

        //private void Cb_marktype_Checked(object sender, RoutedEventArgs e)
        //{
        //    this.SetMarkType(MarkType.Sample);
        //}

        //private void Cb_marktype_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    this.SetMarkType(MarkType.None);
        //}

        /// <summary>
        /// 加载图片(上一张下一张切换用)
        /// </summary>
        /// <param name="imagePath"> 图片路径 </param>
        public void LoadImage(string imagePath)
        {
            if (imagePath == null) return;

            //this.RefreshPart();

            Application.Current.Dispatcher.Invoke(() =>
            {
                this.RefreshCurrentText();

                if (this.ViewModel == null)
                {
                    this.ViewModel = new ImageControlViewModel(this);
                }

                //ImageControlViewModel viewModel = new ImageControlViewModel(this);

                this.ViewModel.IsBuzy = true;

                try
                {
                    Task.Run(() =>
                    {
                        var p = imagePath;
                        var s = new BitmapImage();
                        s.BeginInit();
                        s.CacheOption = BitmapCacheOption.OnLoad;

                        s.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                        s.EndInit();
                        //这一句很重要，少了UI线程就不认了。
                        s.Freeze();



                        Application.Current.Dispatcher.Invoke(() =>
                        {

                            this.ViewModel.ImageSource = s;

                            this.Source = s;

                            this.ViewModel.IsBuzy = false;
                        });

                    });

                }
                catch (Exception ex)
                {

                    Debug.WriteLine(ex);

                }

            });

        }

        #region - 成员属性 -

        //  Do：所有图片路径集合
        LinkedList<string> _collection = new LinkedList<string>();

        //  Do：当前图片路径
        LinkedListNode<string> current;

        ////  Do：自动播放时间处理
        //Timer timer = new Timer();

        public LinkedListNode<string> Current { get => current; set => current = value; }



        #endregion

        #region - 路由事件 -

        //声明和注册路由事件
        public static readonly RoutedEvent LastClickedRoutedEvent =
            EventManager.RegisterRoutedEvent("LastClicked", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ImageViews));

        /// <summary>
        /// 上一页路由事件
        /// </summary>
        public event RoutedEventHandler LastClicked
        {
            add { this.AddHandler(LastClickedRoutedEvent, value); }
            remove { this.RemoveHandler(LastClickedRoutedEvent, value); }
        }

        /// <summary>
        /// 激发上一页
        /// </summary>
        public void OnLastClicked()
        {
            //this.RefreshPart();

            this.Clear();

            if (Current != null)
            {
                Current = Current.Previous;

                if (Current == null)
                {
                    Current = Collection.Last;
                }


                this.LoadImage(Current.Value);
            }

            //  Do：触发删除事件
            this.PreviousImgEvent?.Invoke();

            Application.Current.Dispatcher.Invoke(() =>
            {
                RoutedEventArgs args = new RoutedEventArgs(LastClickedRoutedEvent, this);
                this.RaiseEvent(args);
            });

        }

        void RefreshCurrentText()
        {
            if (this.Collection == null) return;

            if (this.current == null) return;

            var index = this.Collection.ToList().FindIndex(l => l == this.current.Value);

            this.btn_play_current.Content = $"第{(index+1).ToString()}/{this.Collection?.Count}张";
        }

        //声明和注册路由事件
        public static readonly RoutedEvent NextClickRoutedEvent =
            EventManager.RegisterRoutedEvent("NextClick", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ImageViews));

        /// <summary>
        /// 下一页路由事件
        /// </summary>
        public event RoutedEventHandler NextClick
        {
            add { this.AddHandler(NextClickRoutedEvent, value); }
            remove { this.RemoveHandler(NextClickRoutedEvent, value); }
        }

        /// <summary>
        /// 激发下一页
        /// </summary>
        public void OnNextClick()
        {

            this.Clear();

            if (Current != null)
            {
                Current = Current.Next;

                if (Current == null)
                {
                    Current = Collection.First;
                }

                this.LoadImage(Current.Value);
            }

            //  Do：触发下一页
            this.NextImgEvent?.Invoke();

            Application.Current.Dispatcher.Invoke(() =>
            {
                RoutedEventArgs args = new RoutedEventArgs(NextClickRoutedEvent, this);
                this.RaiseEvent(args);
            });

        }

        #endregion

        #region - 依赖属性 -

        /// <summary>
        /// 所有图片的路径集合
        /// </summary>
        public List<string> ImagePaths
        {
            get { return (List<string>)GetValue(ImagePathsProperty); }
            set { SetValue(ImagePathsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImagePathsProperty =
            DependencyProperty.Register("ImagePaths", typeof(List<string>), typeof(ImageViews), new PropertyMetadata(default(List<string>), (d, e) =>
            {
                ImageViews control = d as ImageViews;

                if (control == null) return;

                List<string> config = e.NewValue as List<string>;

                if (config == null) return;

                if (config.Count == 0) return;

                //if (!File.Exists(config.First())) return;

                control.Collection.Clear();
                //  Do：根据路径加载图片内存集合
                foreach (var item in config)
                {
                    control.Collection.AddLast(item);
                }

                control.Current = control.Collection.First;

                //  Do：加载默认图片
                control.LoadImage(control.Current.Value);


            }));

        /// <summary>
        /// 自动播放模式
        /// </summary>
        public ImgPlayMode ImgPlayMode
        {
            get
            {
                return (ImgPlayMode)GetValue(ImgPlayModeProperty);
            }
            set { SetValue(ImgPlayModeProperty, value); }
        }


        MarkType _tempMarkType;

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImgPlayModeProperty =
            DependencyProperty.Register("ImgPlayMode", typeof(ImgPlayMode), typeof(ImageViews), new PropertyMetadata(ImgPlayMode.停止播放, (d, e) =>
            {
                ImageViews control = d as ImageViews;

                if (control == null) return;

                ImgPlayMode config = (ImgPlayMode)e.NewValue;

                //  Do：设置自动播放模式
                if (config == ImgPlayMode.正序 || config == ImgPlayMode.倒叙)
                {
                    control.Start();

                    control._tempMarkType = control._markType;

                    control.SetMarkType(MarkType.None);
                }
                else if (config == ImgPlayMode.停止播放)
                {
                    control.Stop();

                    control.SetMarkType(control._tempMarkType);
                }

            }));


        /// <summary>
        /// 自动播放速度
        /// </summary>
        public double Speed
        {
            get { return (double)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }

        public LinkedList<string> Collection { get => _collection; set => _collection = value; }
        public double Scale
        {
            get => scale;
            set
            {
                scale = value;
                SetImageByScale();
            }
        }



        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(double), typeof(ImageViews), new PropertyMetadata(1.0, (d, e) =>
            {
                ImageViews control = d as ImageViews;

                if (control == null) return;

            }));

        #endregion

        /// <summary>
        /// 上一页、下一页时清理局部放大还有蒙版等页面
        /// </summary>
        public void Clear()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (this.ViewModel == null) return;

                //  Do：清理动态形状
                this._dynamic.Visibility = Visibility.Collapsed;

                //  Do：清理所有样本形状
                foreach (var sample in this.ViewModel.SampleCollection)
                {
                    foreach (var item in sample.RectangleLayer)
                    {
                        item.Clear(this.canvas);
                    }

                    sample.RectangleLayer.Clear();
                }

                this.ViewModel.SampleCollection.Clear();
            });

            ////  Do：隐藏蒙版
            //this.HideRectangleClip();

        }

        Task task;

        //  Message：取消播放任务
        CancellationTokenSource tokenSource;

        /// <summary> 開始播放 </summary>
        void Start()
        {
            //control.timer.Start(); 

            Action action = null;

            action = () =>
            {
                if (tokenSource.IsCancellationRequested) return;

                //Thread.Sleep(100 * random.Next(10));


                ImgPlayMode playMode = ImgPlayMode.正序;

                double speed = 0;

                bool isBuzy = false;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    playMode = this.ImgPlayMode;
                    speed = this.Speed;
                    isBuzy = this.ViewModel == null ? false : this.ViewModel.IsBuzy;
                });


                if (playMode == ImgPlayMode.正序)
                {
                    if (!isBuzy)
                        this.OnNextClick();
                }
                else if (playMode == ImgPlayMode.倒叙)
                {
                    if (!isBuzy)
                        this.OnLastClicked();
                }

                Task nextTask = Task.Delay(TimeSpan.FromMilliseconds((1000 * speed)), tokenSource.Token);

                nextTask.ContinueWith(l => action());

            };

            tokenSource = new CancellationTokenSource();

            task = new Task(action, tokenSource.Token);

            task.Start();
        }

        /// <summary> 停止播放 </summary>
        void Stop()
        {
            //control.timer.Stop();

            tokenSource.Cancel();
        }

        /// <summary>
        /// 重新刷新绘制所有样本数据
        /// </summary>
        public void RefreshAll()
        {
            foreach (var items in this.ViewModel.SampleCollection)
            {
                foreach (var item in items.RectangleLayer)
                {
                    item.Clear(this.canvas);

                    item.Draw(this.canvas);
                }
            }
        }

        void ShowPartWithShape(RectangleShape rectangle)
        {
            RectangleGeometry rect = new RectangleGeometry(new Rect(0, 0, this.canvas.ActualWidth, this.canvas.ActualHeight));

            //  Do：设置覆盖的蒙版
            var geo = Geometry.Combine(rect, new RectangleGeometry(rectangle.Rect), GeometryCombineMode.Exclude, null);

            DynamicShape shape = new DynamicShape(rectangle);

            ////  Do：设置形状、用来提供给局部放大页面
            //this.DynamicShape = shape;

            ////  Do：设置提供局部放大在全局的范围的视图
            //this.ImageVisual = this.canvas;

            //this.OnBegionShowPartView();

            ////  Do：设置当前蒙版的剪切区域
            //this.rectangle_clip.Clip = geo;

            this.ShowScaleWithRect(rectangle.Rect);

            _dynamic.Visibility = Visibility.Collapsed;
        }

        RectangleShape _currentShap;

        MouseEventHandler mouseEventHandler;

        public void ShowDefaultDefectPart(bool flag)
        {
            if (this.ViewModel == null) return;

            if (this.ViewModel.SampleCollection.Count == 0) return;

            _currentShap = this.ViewModel.SampleCollection.First().RectangleLayer.First() as RectangleShape;


            foreach (var sample in this.ViewModel.SampleCollection)
            {
                foreach (var shape in sample.RectangleLayer)
                {
                    RectangleShape rectangleShape = shape as RectangleShape;

                    if (flag)
                    {
                        rectangleShape.MouseEnter += mouseEventHandler;
                    }
                    else
                    {
                        rectangleShape.MouseEnter -= mouseEventHandler;

                        //  Message：恢复到平铺样式
                        this.SetFullImage();
                    }
                }
            }
        }

        void ShowCurrentShape()
        {
            if (_currentShap == null)
            {
                _currentShap = this.ViewModel.SampleCollection.First().RectangleLayer.First() as RectangleShape;
            }

            this.ShowPartWithShape(_currentShap);
        }



        public void ShowNextShape()
        {
            if (this.ViewModel == null) return;

            if (this.ViewModel.SampleCollection.Count == 0) return;

            var sample = this.ViewModel.SampleCollection.Where(l => l.RectangleLayer.Contains(this._currentShap));

            if (sample == null || sample.Count() == 0) return;

            int index = this.ViewModel.SampleCollection.IndexOf(sample.First());

            //  Message：如果是最后一项则跳转到第一项
            index = this.ViewModel.SampleCollection.Count - 1 == index ? 0 : index + 1;

            RectangleShape shape = this.ViewModel.SampleCollection[index].RectangleLayer.First() as RectangleShape;

            _currentShap = shape;

            this.ShowCurrentShape();
        }

        public void ShowPreShape()
        {
            if (this.ViewModel == null) return;

            if (this.ViewModel.SampleCollection.Count == 0) return;

            var sample = this.ViewModel.SampleCollection.Where(l => l.RectangleLayer.Contains(this._currentShap));

            if (sample == null || sample.Count() == 0) return;

            int index = this.ViewModel.SampleCollection.IndexOf(sample.First());

            //  Message：如果是最后一项则跳转到第一项
            index = 0 == index ? this.ViewModel.SampleCollection.Count - 1 : index - 1;

            RectangleShape shape = this.ViewModel.SampleCollection[index].RectangleLayer.First() as RectangleShape;

            _currentShap = shape;

            this.ShowCurrentShape();
        }

        /// <summary>
        /// 显示全屏
        /// </summary>
        void ShowFullScreen()
        {
            ImageFullScreenWindow window = new ImageFullScreenWindow();

            window.DataContext = this.ViewModel;
            this.ClearToScreen();
            window.CenterContent = this.grid_all;
            //window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //window.Owner = ComponetProvider.Instance.FindVisualParent<Window>(this).First();

            //  Do：触发全屏状态事件
            this.FullScreenChangedEvent?.Invoke(true);

            window.ShowDialog();

            this.RecoverFromScreen();

            //  Do：触发取消全屏状态事件
            this.FullScreenChangedEvent?.Invoke(false);
        }

        /// <summary>
        /// 退出全屏模式清理
        /// </summary>
        void ClearToScreen()
        {
            this.Content = null;

            //this.btn_fullScreen.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// 刷新显示全屏
        /// </summary>
        void RecoverFromScreen()
        {
            this.Content = this.grid_all;
            //this.btn_fullScreen.Visibility = Visibility.Visible;
        }

        private void Btn_next_Click(object sender, RoutedEventArgs e)
        {
            this.OnNextClick();
        }

        private void Btn_preview_Click(object sender, RoutedEventArgs e)
        {
            this.OnLastClicked();
        }

        private void BtnMacthsize_Click(object sender, RoutedEventArgs e)
        {
            this.SetFullImage();
        }

        private void BtnRotate_left_Click(object sender, RoutedEventArgs e)
        {
            this.SetRotateLeft();
        }

        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
            if (this.Current == null) return;

            this.DeleteImgEvent?.Invoke(this.Current.Value);
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.SetFullScreen(true);
        }

        private void UserControl_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            this.SetFullScreen(true);
        }

        /// <summary> 幻灯片播放 </summary>
        private void Btn_play_Click(object sender, RoutedEventArgs e)
        {
            this.tool_normal.Visibility = Visibility.Collapsed;
            this.tool_play.Visibility = Visibility.Visible;

            this.RefreshPlay();
        }

        private void Btn_play_addspeed_Click(object sender, RoutedEventArgs e)
        {
            this.ImgPlaySpeedUp();
        }

        private void Btn_play_mulspeed_Click(object sender, RoutedEventArgs e)
        {
            this.ImgPlaySpeedDown();
        }

        private void Btn_play_start_Click(object sender, RoutedEventArgs e)
        {
            this.RefreshPlay();
        }

        /// <summary> 刷新播放状态 </summary>
        void RefreshPlay()
        {
            if (this.btn_play_start.ToolTip.ToString() == "播放")
            {
                this.PlayStart();
            }
            else
            {
                this.PlayStop();
            }
        }

        /// <summary> 开始播放 </summary>
        void PlayStart()
        {
            this.SetImgPlay(ImgPlayMode.正序);
            this.btn_play_start.ToolTip = "暂停";
            this.path_stat.Visibility = Visibility.Collapsed;
            this.path_stop.Visibility = Visibility.Visible;
        }
        /// <summary> 停止播放 </summary>
        void PlayStop()
        {
            this.SetImgPlay(ImgPlayMode.停止播放);
            this.btn_play_start.ToolTip = "播放";
            this.path_stat.Visibility = Visibility.Visible;
            this.path_stop.Visibility = Visibility.Collapsed;
        }

        /// <summary> 退出播放 </summary>
        private void Btn_play_close_Click(object sender, RoutedEventArgs e)
        {
            this.tool_normal.Visibility = Visibility.Visible;
            this.tool_play.Visibility = Visibility.Collapsed;

            this.PlayStop();
        }
    }


    partial class ImageViews : IImgOperate
    {
        public event ImgMarkHandler ImgMarkOperateEvent;
        public event ImgProcessHandler ImgProcessEvent;
        public event Action PreviousImgEvent;
        public event Action NextImgEvent;
        public event Action<ImgMarkEntity, MarkType> DrawMarkedMouseUp;
        public event Action<string> DeleteImgEvent;
        public event Action<bool> FullScreenChangedEvent;

        /// <summary>
        /// 触发新增事件（此方法）
        /// </summary>
        /// <param name="entity"></param>
        public void OnImgMarkOperateEvent(ImgMarkEntity entity)
        {
            this.ImgMarkOperateEvent?.Invoke(entity);
        }

        public void AddImgFigure(Dictionary<string, string> imgFigures)
        {
            if (this.ViewModel == null)
            {
                Debug.WriteLine("请先加载图片数据，在添加标定信息");
                return;
            }

            this.ViewModel.FigureCollection = imgFigures;
        }

        public void AddMark(ImgMarkEntity imgMarkEntity)
        {
            SampleVieModel sample = new SampleVieModel();

            //sample.Name = imgMarkEntity.Name;

            //sample.Code = imgMarkEntity.PHMCodes;

            sample.Model = imgMarkEntity;

            //  Do：根据选择的样本类型来生成缺陷/样本
            if (this._markType == MarkType.Defect)
            {
                DefectShape resultStroke = new DefectShape(this._dynamic);
                sample.Flag = "\xe688";
                sample.Type = "0";
                sample.Code = imgMarkEntity.PHMCodes;
                resultStroke.Name = sample.Name;
                resultStroke.Code = sample.Code;
                resultStroke.Draw(this.canvas);
                sample.Add(resultStroke);
            }
            else if (this._markType == MarkType.Sample)
            {
                SampleShape resultStroke = new SampleShape(this._dynamic);
                sample.Flag = "\xeaf3";
                sample.Type = "1";
                sample.Code = imgMarkEntity.PHMCodes;
                resultStroke.Name = sample.Name;
                resultStroke.Code = sample.Code;
                resultStroke.Draw(this.canvas);
                sample.Add(resultStroke);
            }

            this.ViewModel.Add(sample);

            //  Do：触发新增事件
            this.ImgMarkOperateEvent?.Invoke(sample.Model);

            //  Do：清除动态框
            _dynamic.BegionMatch(false);
        }

        public Control BuildEntity()
        {
            return this;
        }

        public void CancelAddMark()
        {
            this._dynamic.Visibility = Visibility.Collapsed;
        }

        public void DeleteSelectMark()
        {
            var entity = this.GetSelectMarkEntity();

            entity.markOperateType = ImgMarkOperateType.Delete;

            this.MarkOperate(entity);
        }

        public ImgMarkEntity GetSelectMarkEntity()
        {
            if (this.ViewModel == null) return null;

            var result = this.ViewModel.SampleCollection.ToList().FindAll(l => l.RectangleLayer.First().IsSelected);

            if (result == null || result.Count == 0)
            {
                Debug.WriteLine("没有选中项！");
                return null;
            }

            return result.First().Model;
        }

        public void ImgPlaySpeedDown()
        {
            this.Speed = 2 * this.Speed;

            this.RefreshSpeedText();
        }

        void RefreshSpeedText()
        {
            this.btn_play_speed.Content = $"间隔{this.Speed }秒";
        }

        public void ImgPlaySpeedUp()
        {
            this.Speed = this.Speed / 2;

            this.RefreshSpeedText();
        }

        public void LoadCodes(Dictionary<string, string> codeDic)
        {
            if (this.ViewModel == null)
            {
                Debug.WriteLine("请先加载图片数据，在添加标定信息");
                return;
            }

            this.ViewModel.CodeCollection = codeDic;
        }

        public void LoadImg(string imgPath)
        {
            this.Collection.AddLast(imgPath);

            this.Current = this.Collection.Last;

            this.LoadImage(imgPath);

            //this.SetFullImage();
        }

        public void LoadImg(List<string> imgPathes)
        {
            this.ImagePaths = imgPathes;
        }

        public void LoadMarkEntitys(List<ImgMarkEntity> markEntityList)
        {
            if (markEntityList == null)
            {
                Debug.WriteLine("加载标定数据为空");
                return;
            }

            if (this.ViewModel == null)
            {
                Debug.WriteLine("请先加载图片数据，在添加标定信息");
                return;
            }

            foreach (var item in markEntityList)
            {
                SampleVieModel vm = new SampleVieModel(item);

                this.ViewModel.SampleCollection.Add(vm);
            }

            this.RefreshAll();
        }

        /// <summary>
        /// 样本缺陷模型可编辑
        /// </summary>
        /// <param name="entity"></param>
        public void MarkOperate(ImgMarkEntity entity)
        {
            //  Do：新增
            if (entity.markOperateType == ImgMarkOperateType.Insert)
            {
                SampleVieModel vm = new SampleVieModel(entity);

                this.ViewModel.SampleCollection.Add(vm);
            }
            else
            {
                //var find = this.ViewModel.SampleCollection.ToList().Find(l => l.Name == entity.Name && l.Code == entity.Code);

                var find = this.ViewModel.SampleCollection.ToList().Find(l => l.Model == entity);

                if (find == null)
                {
                    Debug.WriteLine("不存在标记：" + entity.Name);
                    return;
                }

                find.RectangleLayer.First().Clear();

                this.ViewModel.SampleCollection.Remove(find);

                //  Do：修改
                if (entity.markOperateType == ImgMarkOperateType.Update)
                {
                    SampleVieModel vm = new SampleVieModel(entity);

                    this.ViewModel.SampleCollection.Add(vm);

                }
            }

        }

        public void NextImg()
        {
            this.OnNextClick();
        }

        public void PreviousImg()
        {
            this.OnLastClicked();
        }

        ShortCutHookService _shortCutHookService = new ShortCutHookService();

        public bool IsWheelPlay
        {
            get => this.btn_wheel.IsChecked.HasValue && this.btn_wheel.IsChecked.Value;
            set
            {
                this.btn_wheel.IsChecked = value;
            }
        }

        /// <summary> 此方法的说明 </summary>
        public void RegisterPartShotCut(ShortCutEntitys shortcut)
        {
            bool flag = false;

            //  Message：先清理事件
            this.ShowDefaultDefectPart(flag);

            _shortCutHookService.Clear();

            // Todo ：双击大小写切换 
            ShortCutEntitys s = new ShortCutEntitys();

            s = new ShortCutEntitys();

            KeyEntity up = new KeyEntity();
            up.Key = System.Windows.Forms.Keys.Up;
            s.Add(up);

            _shortCutHookService.RegisterCommand(s, () =>
            {
                Debug.WriteLine("按键：↑");

                if (!flag) return;

                //if (this.control_ImagePartView.Visibility == Visibility.Collapsed) return;

                this.ShowNextShape();
            });

            s = new ShortCutEntitys();

            KeyEntity down = new KeyEntity();
            down.Key = System.Windows.Forms.Keys.Down;
            s.Add(down);

            _shortCutHookService.RegisterCommand(s, () =>
            {
                Debug.WriteLine("按键：↓");

                if (!flag) return;

                //if (this.control_ImagePartView.Visibility == Visibility.Collapsed) return;

                this.ShowNextShape();
            });

            // Todo ：双击Ctrl键 
            ShortCutEntitys d = new ShortCutEntitys();

            KeyEntity c1 = new KeyEntity();
            c1.Key = System.Windows.Forms.Keys.LControlKey;
            d.Add(c1);

            KeyEntity c2 = new KeyEntity();
            c2.Key = System.Windows.Forms.Keys.LControlKey;
            d.Add(c2);


            bool _initFlag = false;

            Action action = () =>
            {
                Debug.WriteLine(shortcut);

                if (this.ViewModel == null) return;

                //if (this.Visibility == Visibility.Visible)
                //{
                //    this.OnClosed();
                //}
                ////else
                ////{
                ////    flag = !flag;

                ////    if(flag)
                ////    {
                ////        Debug.WriteLine("进入模式");
                ////    }
                ////    else
                ////    {
                ////        Debug.WriteLine("退出模式");
                ////    }

                ////    Debug.WriteLine(flag);

                ////    this.control_imageView.ShowDefaultDefectPart(flag);
                ////}

                flag = !flag;

                if (flag)
                {
                    Debug.WriteLine("进入模式");
                }
                else
                {
                    Debug.WriteLine("退出模式");
                }

                this.ShowDefaultDefectPart(flag);

                //  Message：如果是是默认加载第一个
                //if (flag)
                //{
                //    //Action<RectangleShape> mouseEnterAction = l =>
                //    //  {
                //    //      if (!flag) return;

                //    //      if (l == null) return;

                //    //      this.control_imageView.ShowPartWithShape(l);
                //    //  };


                //}


            };

            _shortCutHookService.RegisterCommand(shortcut, action);
        }


        public void RegisterDefaltApi()
        {
            // Todo ：双击Ctrl键 
            ShortCutEntitys d = new ShortCutEntitys();

            KeyEntity c1 = new KeyEntity();
            c1.Key = System.Windows.Forms.Keys.LControlKey;
            d.Add(c1);

            KeyEntity c2 = new KeyEntity();
            c2.Key = System.Windows.Forms.Keys.LControlKey;
            d.Add(c2);

            this.RegisterPartShotCut(d);
        }

        public void Rotate()
        {
            btnRotate_Click(null, null);
        }

        public void ScreenShot(string saveFullName)
        {
            byte[] screenshot = ComponetProvider.Instance.GetScreenShot(this.canvas, 1, 90);
            FileStream fileStream = new FileStream(saveFullName, FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(screenshot);
            binaryWriter.Close();
        }

        public void SetFullScreen(bool isFullScreen)
        {

            this.ShowFullScreen();

            //if (isFullScreen)
            //{
            //    ImageViewCommands.FullScreen.Execute(null, this);
            //}
            //else
            //{
            //    this.ShowFullScreen();
            //}
        }

        public void SetImgPlay(ImgPlayMode imgPlayMode)
        {
            this.ImgPlayMode = imgPlayMode;
        }

        public void SetSelectMarkEntity(Predicate<ImgMarkEntity> match)
        {
            if (this.ViewModel == null) return;

            var result = this.ViewModel.SampleCollection.ToList().Find(l => match(l.Model));

            if (result == null)
            {
                Debug.WriteLine("没有找到匹配项");
                return;
            }

            result.RectangleLayer.First().SetSelected();
        }

        public void ShowDefects()
        {
            foreach (var item in this.ViewModel.SampleCollection)
            {
                item.Visible = item.Type == "0";
            }
        }

        public void ShowLocates()
        {
            foreach (var item in this.ViewModel.SampleCollection)
            {
                item.Visible = item.Type == "1";
            }
        }


        public void ShowMarks()
        {
            foreach (var item in this.ViewModel.SampleCollection)
            {
                item.Visible = true;
            }
        }

        public void ShowMarks(List<string> markCodes)
        {
            foreach (var item in this.ViewModel.SampleCollection)
            {
                item.Visible = markCodes.Exists(l => l == item.Code);
            }
        }

        public void SetEnlarge()
        {
            if (imgWidth == 0 || imgHeight == 0)
                return;

            btnNarrow.IsEnabled = true;
            if (Scale > 16)
                return;
            Scale = Scale * 1.2;
            SetbtnActualsizeEnable();

            if (Scale > 16)
            {
                btnEnlarge.IsEnabled = false;
            }
            this.txtZoom.Text = ((int)(Scale * 100)).ToString() + "%";
            if (sb_Tip != null)
                sb_Tip.Begin();
            SetImageByScale();
        }

        public void SetNarrow()
        {
            if (imgWidth == 0 || imgHeight == 0)
                return;

            btnEnlarge.IsEnabled = true;
            if (Scale < 0.15)
                return;
            Scale = Scale * (1 / 1.2);

            SetbtnActualsizeEnable();
            if (Scale < 0.15)
            {
                btnNarrow.IsEnabled = false;
            }
            this.txtZoom.Text = ((int)(Scale * 100)).ToString() + "%";
            if (sb_Tip != null)
                sb_Tip.Begin();
            SetImageByScale();
        }

        public void SetRotateLeft()
        {
            this.Rotate(-90);
        }

        public void SetRotateRight()
        {
            this.Rotate(90);
        }



        public void SetMarkType(MarkType markType)
        {

            this._markType = markType;

            if (markType == MarkType.None)
            {
                this.gridMouse.Visibility = Visibility.Visible;

                _dynamic.Visibility = Visibility.Collapsed;

                //  Message：设置光标和区域放大
                this.gridMouse.Cursor = Cursors.Hand;
            }
            else if (markType == MarkType.Enlarge)
            {
                this.gridMouse.Visibility = Visibility.Hidden;

                //  Message：设置光标和区域放大
                this.canvas.Cursor = Cursors.Pen;

            }
            else
            {
                this.gridMouse.Visibility = Visibility.Hidden;

                //  Message：设置光标和区域放大
                this.canvas.Cursor = Cursors.Cross;
            }


        }
    }
}
