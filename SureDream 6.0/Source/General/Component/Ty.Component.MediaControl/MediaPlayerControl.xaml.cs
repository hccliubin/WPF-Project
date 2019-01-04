﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ty.Component.MediaControl
{
    /// <summary>
    /// 视频播放控件
    /// </summary>
    public partial class MediaPlayerControl : UserControl
    {
        public MediaPlayerControl()
        {
            InitializeComponent();

            this.media_media.MediaEnded += Player_MediaEnded;
            this.media_media.MediaOpened += Player_MediaOpened;
            this.media_media.MediaFailed += Player_MediaFailed;
            this.media_media.Loaded += Player_Loaded;

            _timer.Elapsed += Timer_Elapsed;
            _timer.Interval = 1000;

        }


        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.RefreshSlider();
        }

        Timer _timer = new Timer();

        private void Player_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Player_Loaded");
        }

        void InitSlider()
        {
            if (this.media_media.Source == null) return;

            if (this.media_media.NaturalDuration.HasTimeSpan)
            {
                this.media_slider.Maximum = this.media_media.NaturalDuration.TimeSpan.Ticks;
            }
        }

        void RefreshSlider()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (_mediaPlayMode == MediaPlayMode.RepeatFromTo)
                {
                    if (this._repeatFromTo == null) return;

                    if(this.media_media.Position<_repeatFromTo.Item1)
                    {
                        this.media_media.Position = _repeatFromTo.Item1;
                    }

                    if (this.media_media.Position > _repeatFromTo.Item2)
                    {
                        this.media_media.Position = _repeatFromTo.Item1;
                    }
                }

                this.media_slider.Value = this.media_media.Position.Ticks;
            });
        }

        private void Player_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message);
            Debug.WriteLine("Player_MediaFailed");

        }

        private void Player_MediaOpened(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Player_MediaOpened");

            this.InitSlider();

            this.InitSound();

            this._timer.Start();
        }

        private void Player_MediaEnded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Player_MediaEnded");

            this.Stop();
        }

        private void media_slider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (this.media_media == null) return;

            this.media_media.Position = TimeSpan.FromTicks((long)this.media_slider.Value);

            this._timer.Start();
        }

        void InitSound()
        {
            this.slider_sound.Value = this.media_media.Volume;
        }

        private void slider_sound_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            this.media_media.Volume = this.slider_sound.Value;
        }

        private void media_slider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            this._timer.Stop();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.toggle_play.IsChecked.Value)
            {
                this.Pause();
            }
            else
            {

                this.Play();
            }
        }

        private void CommandBinding_Executed_Play(object sender, ExecutedRoutedEventArgs e)
        {

            Debug.WriteLine("CommandBinding_Executed_Play");

        }

        private void CommandBinding_CanExecute_Play(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MediaBrower_PlayClick(object sender, RoutedEventArgs e)
        {
            this.media_media.Stop();
            this.media_media.Play();
            this.toggle_play.IsChecked = false;
        }

        void Play()
        {
            this.media_media.Play();
            this._timer.Start();
        }

        void Pause()
        {
            this.media_media.Pause();
            this._timer.Stop();
        }

        void Stop()
        {
            this.media_media.Position = TimeSpan.FromTicks(0);
            this.media_slider.Value = 0;
            this.media_media.Stop();
            this._timer.Stop();
            this.toggle_play.IsChecked = true;
            this.media_media.LoadedBehavior = MediaState.Manual;
        }

        Point start;

        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InkCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {

            _dynamic.BegionMatch(true);

            start = e.GetPosition(sender as InkCanvas);

            System.Diagnostics.Debug.WriteLine("说明");

        }

        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InkCanvas_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.LeftButton != MouseButtonState.Pressed) return;

            if (this.start.X <= 0) return;

            Point end = e.GetPosition(this.canvas);

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
            //  Do：检查选择区域是否可用
            if (!_dynamic.IsMatch())
            {
                _dynamic.Visibility = Visibility.Collapsed;
                return;
            };

            if (this.start.X <= 0) return;

            //  Do：如果是选择局部放大
            if (this.r_screen.IsChecked.HasValue && this.r_screen.IsChecked.Value)
            {
                RectangleGeometry rect = new RectangleGeometry(new Rect(0, 0, this.canvas.ActualWidth, this.canvas.ActualHeight));

                //  Do：设置覆盖的蒙版
                var geo = Geometry.Combine(rect, new RectangleGeometry(this._dynamic.Rect), GeometryCombineMode.Exclude, null);

                DynamicShape shape = new DynamicShape(this._dynamic);

                ////  Do：设置形状、用来提供给局部放大页面
                //this.DynamicShape = shape;

                ////  Do：设置提供局部放大在全局的范围的视图
                //this.ImageVisual = this.canvas;

                //this.OnBegionShowPartView();

                ////  Do：设置当前蒙版的剪切区域
                //this.rectangle_clip.Clip = geo;

                _dynamic.Visibility = Visibility.Collapsed;

                MediaPartControl mediaPartControl = new MediaPartControl();
                mediaPartControl.DynamicShape = shape;
                mediaPartControl.ImageVisual = this.canvas;

                Window window = new Window();

                window.Content = mediaPartControl;
                window.ShowDialog();



            }


            //  Do：将数据初始化
            start = new Point(-1, -1);


        }

        private void Btn_rotateTransform_Click(object sender, RoutedEventArgs e)
        {
            //TransformGroup transformGroup = this.media_media.RenderTransform as  TransformGroup;
            RotateTransform rotate = this.media_media.RenderTransform as RotateTransform;
            rotate.CenterX = this.media_media.ActualWidth / 2;
            rotate.CenterY = this.media_media.ActualHeight / 2;
            rotate.Angle = rotate.Angle + 90;

        }

        private void Btn_addspeed_Click(object sender, RoutedEventArgs e)
        {
            this.media_media.SpeedRatio = this.media_media.SpeedRatio * 2;
        }

        private void Btn_multipspeed_Click(object sender, RoutedEventArgs e)
        {
            this.media_media.SpeedRatio = this.media_media.SpeedRatio / 2;
        }

        private void Btn_stop_Click(object sender, RoutedEventArgs e)
        {
            this.Stop();
        }
    }

    public partial class MediaPlayerControl : IMediaPlayerService
    {

        MediaPlayType _mediaPlayType= MediaPlayType.Video;

        MediaPlayMode _mediaPlayMode= MediaPlayMode.Normal;

        Tuple<TimeSpan, TimeSpan> _repeatFromTo;

        /// <summary>
        /// 获取当前帧
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetCurrentFrame()
        {
            if (this.media_media.Source == null) return TimeSpan.MinValue;

            if (!this.media_media.NaturalDuration.HasTimeSpan) return TimeSpan.MinValue;

            return this.media_media.Position;
        }

        public string GetCurrentUrl()
        {
            if (_mediaPlayType == MediaPlayType.Video)
            {
                return this.media_media.Source.AbsoluteUri;
            }
            else if (_mediaPlayType == MediaPlayType.ImageFoder)
            {
                return null;
            }
            else if (_mediaPlayType == MediaPlayType.ImageList)
            {
                return null;
            }
            else
            {
                return null;
            }
        }

        public TimeSpan GetTotalFrame()
        {
            if (this.media_media.Source == null) return TimeSpan.MinValue;

            if (!this.media_media.NaturalDuration.HasTimeSpan) return TimeSpan.MinValue;

            return TimeSpan.FromTicks(this.media_media.NaturalDuration.TimeSpan.Ticks);
        }

        public void Load(string mediaPath)
        {
            Uri uri = new Uri(mediaPath, UriKind.Absolute);

            this.media_media.Source = uri;

            this.Play();
        }

        public void LoadImageFolder(string imageFoder)
        {
            throw new NotImplementedException();
        }

        List<string> _imageUrls = new List<string>();

        public void LoadImages(List<string> ImageUrls)
        {
            this._mediaPlayType = MediaPlayType.ImageList;

            _imageUrls = ImageUrls;

            Uri uri = new Uri(ImageUrls.First(), UriKind.Absolute);

            this.media_media.Source = uri;

            this.Play();
        }

        public void RepeatFromTo(TimeSpan from, TimeSpan to)
        {
            if (from > to) return;

            this._mediaPlayMode = MediaPlayMode.RepeatFromTo;

            _repeatFromTo = new Tuple<TimeSpan, TimeSpan>(from, to);

            //this.media_media.Position = from;

            //Task t = Task.Delay(to - from);

            //t.ContinueWith(l =>
            //{
            //    Application.Current.Dispatcher.Invoke(() =>
            //    {
            //        this.media_media.Position = from;

            //        t = Task.Delay(to - from);
            //    });
            //});
        }

        public void ScreenShot(TimeSpan from, string saveFullName)
        {
            //byte[] screenshot = this.media_media.GetScreenShot(1, 90);
            //FileStream fileStream = new FileStream(@"Capture.jpg", FileMode.Create, FileAccess.ReadWrite);
            //BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            //binaryWriter.Write(screenshot);
            //binaryWriter.Close();

            byte[] screenshot = this.media_media.GetScreenShot(1, 90);
            FileStream fileStream = new FileStream(saveFullName, FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(screenshot);
            binaryWriter.Close();

        }

        public void SetPositon(TimeSpan timeSpan)
        {
            this.media_media.Position = timeSpan;
        }
    }

    public class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            //if (value.ToString() == "0") return "0";
            //if (value.ToString() == "100") return "100";

            var d = double.Parse(value.ToString());

            var sp = TimeSpan.FromTicks((long)d);

            return sp.ToString().Split('.')[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    enum MediaPlayType
    {
        Video = 0, ImageList, ImageFoder
    }

    enum MediaPlayMode
    {
        Normal = 0, RepeatFromTo
    }

    #region Extension Methods
    public static class ScreenShot
    {
        public static byte[] GetScreenShot(this UIElement source, double scale, int quality)
        {
            double actualHeight = source.RenderSize.Height;
            double actualWidth = source.RenderSize.Width;
            double renderHeight = actualHeight * scale;
            double renderWidth = actualWidth * scale;
            RenderTargetBitmap renderTarget = new RenderTargetBitmap((int)renderWidth,
                (int)renderHeight, 96, 96, PixelFormats.Pbgra32);
            VisualBrush sourceBrush = new VisualBrush(source);
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            using (drawingContext)
            {
                drawingContext.PushTransform(new ScaleTransform(scale, scale));
                drawingContext.DrawRectangle(sourceBrush, null, new Rect(new Point(0, 0),
                    new Point(actualWidth, actualHeight)));
            }
            renderTarget.Render(drawingVisual);
            JpegBitmapEncoder jpgEncoder = new JpegBitmapEncoder();
            jpgEncoder.QualityLevel = quality;
            jpgEncoder.Frames.Add(BitmapFrame.Create(renderTarget));
            Byte[] imageArray;
            using (MemoryStream outputStream = new MemoryStream())
            {
                jpgEncoder.Save(outputStream);
                imageArray = outputStream.ToArray();
            }
            return imageArray;
        }
    }
    #endregion
}
