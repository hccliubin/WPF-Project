using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
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

namespace Ty.Component.ImageControl
{
    /// <summary>
    /// ImagePlayerControl.xaml 的交互逻辑
    /// </summary>
    public partial class ImagePlayerControl : UserControl
    {
        public ImagePlayerControl()
        {
            InitializeComponent();

            this.image_control.NextClick += Image_control_IndexChangedClick;

            this.image_control.LastClicked += Image_control_IndexChangedClick;

            //  Message：默认0.5s一张
            this.image_control.Speed = this.image_control.Speed / 2;

            //  Message：隐藏上一页下一页按钮
            this.image_control.button_next.Visibility = Visibility.Collapsed;
            this.image_control.button_last.Visibility = Visibility.Collapsed;

        }

        bool _sliderFlag = false;
        private void Image_control_IndexChangedClick(object sender, RoutedEventArgs e)
        {
            if (_sliderFlag) return;

            var index = this.image_control.ImagePaths.FindIndex(l => l == this.image_control.Current.Value);

            this.media_slider.Value = this.GetSliderValue(index);

        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.image_control.NextImg();
            });

        }

        private void media_slider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            this._sliderFlag = true;
        }

        private void media_slider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int index = (int)((this.media_slider.Value / this.media_slider.Maximum) * this.image_control.ImagePaths.Count);

            string value = this.image_control.ImagePaths[index];

            this.image_control.Current = this.image_control.Collection.Find(value);

            this.image_control.LoadImage(this.image_control.ImagePaths[index]);

            this._sliderFlag = false;
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

        void InitSlider()
        {
            if (this.image_control.ImagePaths == null) return;

            this.media_slider.Maximum = this.GetSliderValue(this.image_control.ImagePaths.Count);

            this.media_slider.Value = 0;
        }

        double GetSliderValue(int index)
        {
            return TimeSpan.FromMilliseconds(1000 * index).Ticks;

            //return TimeSpan.FromMilliseconds((1000 / this.image_control.Speed) * index).Ticks;
        }


        void Play()
        {
            this.image_control.SetImgPlay(ImgPlayMode.正序);

        }

        void Pause()
        {
            this.image_control.SetImgPlay(ImgPlayMode.停止播放);
        }

        private void Btn_multipspeed_Click(object sender, RoutedEventArgs e)
        {
            this.image_control.ImgPlaySpeedDown();
        }

        private void Btn_stop_Click(object sender, RoutedEventArgs e)
        {
            this.Pause();
        }

        private void Btn_addspeed_Click(object sender, RoutedEventArgs e)
        {
            this.image_control.ImgPlaySpeedUp();
        }
    }


    public partial class ImagePlayerControl : IImagePlayerService
    {
        public void LoadImageFolder(string imageFoder)
        {
            var dir = Directory.CreateDirectory(imageFoder);

            var files = dir.GetFiles();

            this.LoadImages(files.Select(l => l.FullName).ToList());

        }

        public void LoadFtpImageFolder(List<string> imageFoders, string useName, string passWord)
        {
            FtpHelper.Login(useName, passWord);

            List<string> files = new List<string>();

            foreach (var item in imageFoders)
            {
                var file = FtpHelper.GetFileList(item).Select(l => System.IO.Path.Combine(item, l)).ToList();

                files.AddRange(file);
            }

            this.LoadImages(files);
        }

        public void LoadImages(List<string> ImageUrls)
        {
            this.image_control.LoadImg(ImageUrls);

            this.InitSlider();
        }


        public string GetCurrentUrl()
        {
            return this.image_control.Current.Value;
        }

        public Tuple<string, string> GetIndexWithTotal()
        {
            var index = this.image_control.ImagePaths.FindIndex(l => l == this.image_control.Current.Value);

            return new Tuple<string, string>(index.ToString(), this.image_control.ImagePaths.Count().ToString());
        }


        public IImgOperate GetImgOperate()
        {
            return this.image_control;
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


}
