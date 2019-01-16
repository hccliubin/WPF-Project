using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
    /// <summary> 图片播放控件 </summary>
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

            this.image_control.SetMarkType(MarkType.None);

        }

        //  Message：标识拖动条是否随播放变化
        bool _sliderFlag = false;

        //  Message：上一页下一页触发
        private void Image_control_IndexChangedClick(object sender, RoutedEventArgs e)
        {
            if (_sliderFlag) return;

            if (this.image_control.Current == null) return;

            //  Do：设置进度条位置
            var index = this.image_control.ImagePaths.FindIndex(l => l == this.image_control.Current.Value);

            this.media_slider.Value = this.GetSliderValue(index);

            //  Do：触发页更改事件
            this.ImageIndexChanged?.Invoke(this.GetCurrentUrl(),ImgSliderMode.System);

        }

        //  Message：开始拖动进度条
        private void media_slider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            this._sliderFlag = true;
        }

        //  Message：结束拖动进度条
        private void media_slider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int index = (int)((this.media_slider.Value / this.media_slider.Maximum) * this.image_control.ImagePaths.Count);

            //string value = this.image_control.ImagePaths[index];

            //this.image_control.Current = this.image_control.Collection.Find(value);

            //this.image_control.LoadImage(this.image_control.ImagePaths[index]);
            ////  Do：触发页更改事件
            //this.ImageIndexChanged?.Invoke(this.GetCurrentUrl());

            //  Do：设置播放位置
            this.SetPositon(index);

            this._sliderFlag = false;

            this.SliderDragCompleted?.Invoke(index, this.GetCurrentUrl());

        }

        private void Media_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //  Message：当是鼠标点击引起的改变是触发SetPositon
            if (Mouse.LeftButton != MouseButtonState.Pressed) return;
            if (!this.media_slider.IsMouseOver) return;

            int index = (int)((this.media_slider.Value / this.media_slider.Maximum) * this.image_control.ImagePaths.Count);


            Debug.WriteLine("MouseButtonState.Pressed");

            //  Do：设置播放位置
            this.SetPositon(index);
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.toggle_play.IsChecked.Value)
            {
                //  Do：播放
                this.Pause();
            }
            else
            {
                //  Do：暂停
                this.Play();
            }
        }

        /// <summary> 初始化进度条 </summary>
        void InitSlider()
        {
            if (this.image_control.ImagePaths == null) return;

            this.media_slider.Maximum = this.GetSliderValue(this.image_control.ImagePaths.Count);

            this.media_slider.Value = 0;
        }

        /// <summary>
        /// 根据索引更新进度条
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        double GetSliderValue(int index)
        {
            return TimeSpan.FromMilliseconds(1000 * index).Ticks;

            //return TimeSpan.FromMilliseconds((1000 / this.image_control.Speed) * index).Ticks;
        }

        /// <summary> 播放 </summary>
        void Play()
        {
            //this.image_control.SetImgPlay(ImgPlayMode.正序);

            this.SetImgPlay(ImgPlayMode.正序);

        }

        /// <summary> 暂停 </summary>
        void Pause()
        {
            //this.image_control.SetImgPlay(ImgPlayMode.停止播放);

            this.SetImgPlay(ImgPlayMode.停止播放);
        }

        /// <summary> 减速 </summary>
        private void Btn_multipspeed_Click(object sender, RoutedEventArgs e)
        {
            this.image_control.ImgPlaySpeedDown();
        }

        /// <summary> 停止 </summary>
        private void Btn_stop_Click(object sender, RoutedEventArgs e)
        {
            this.Pause();
        }

        /// <summary> 加速 </summary>
        private void Btn_addspeed_Click(object sender, RoutedEventArgs e)
        {
            this.image_control.ImgPlaySpeedUp();
        }
        
    }

    public partial class ImagePlayerControl : IImagePlayerService
    {
        public ImgPlayMode ImgPlayMode => this.image_control.ImgPlayMode;

        public event Action<string, ImgSliderMode> ImageIndexChanged;

        public event Action<ImgPlayMode> ImgPlayModeChanged;

        public event Action<int, string> SliderDragCompleted;


        public void LoadImageFolder(List<string> imageFoders, string startForder)
        {
            //  Do：默认加载位置
            int startPostion = 0;

            List<string> files = new List<string>();

            Task.Run(() =>
            {
                //  Do：是否存在startForder
                bool exist = false;

                foreach (var item in imageFoders)
                {
                    //var dir = Directory.CreateDirectory(item);

                    DirectoryInfo dir = new DirectoryInfo(item);

                    var file = dir.GetFiles().Where(l => ComponetProvider.Instance.IsValidImage(l.FullName)).Select(l => l.FullName).ToList();

                    if (item == startForder)
                    {
                        exist = true;
                    }

                    if (!exist)
                    {
                        startPostion += file.Count;
                    }


                    //Thread.Sleep(500);

                    files.AddRange(file);
                }

                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.InitImages(files);

                    //  Do：加载起始位置
                    if (exist)
                    {
                        this.SetPositon(startPostion);
                    }

                });
            });

        }

        public void LoadShareImageFolder(List<string> imageFoders, string startForder, string useName, string passWord,string ip)
        {
            //foreach (var item in imageFoders)
            //{
            //    ComponetProvider.Instance.GetAccessControl(item, "administrator", "123456");
            //}
            using (SharedTool tool = new SharedTool(useName, passWord, ip))
            {
                this.LoadImageFolder(imageFoders, startForder);
            }

              
        }

        public void LoadFtpImageFolder(List<string> imageFoders, string startForder, string useName, string passWord)
        {
            FtpHelper.Login(useName, passWord);

            List<string> files = new List<string>();

            string loginParam = $"ftp://{useName}:{passWord}@";

            //  Do：默认加载位置
            int startPostion = 0;

            Task.Run(() =>
            {
                //  Do：是否存在startForder
                bool exist = false;

                foreach (var item in imageFoders)
                {
                    string url = item.Replace("ftp://", loginParam).Replace("FTP://", loginParam.ToUpper());

                    var file = FtpHelper.GetFileList(item).Where(l => ComponetProvider.Instance.IsValidImage(l)).Select(l => System.IO.Path.Combine(url, l)).ToList();

                    if (item == startForder)
                    {
                        exist = true;
                    }

                    if (!exist)
                    {
                        startPostion += file.Count;
                    }


                    //Thread.Sleep(500);

                    files.AddRange(file);
                }

                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.InitImages(files);

                    //  Do：加载起始位置
                    if (exist)
                    {
                        this.SetPositon(startPostion);
                    }

                });
            });

        }

        /// <summary> 加载图片列表 不触发ImageIndexChanged事件 </summary>
        /// <param name="ImageUrls"></param>
        void InitImages(List<string> ImageUrls)
        {
            this.image_control.LoadImg(ImageUrls);

            //  Do：初始化进度条
            this.InitSlider();
        }


        public void LoadImages(List<string> ImageUrls)
        {
            //this.image_control.LoadImg(ImageUrls);

            ////  Do：初始化进度条
            //this.InitSlider();

            this.InitImages(ImageUrls);

            //  Do：触发页更改事件
            this.ImageIndexChanged?.Invoke(this.GetCurrentUrl(), ImgSliderMode.System);

        }

        public string GetCurrentUrl()
        {
            //  Message：截取ftp部分
            string result = this.image_control.Current.Value;

            if (result.ToUpper().StartsWith("FTP:") && result.Contains("@"))
            {
                return "ftp://" + result.Substring(result.IndexOf('@') + 1);
            }
            else
            {
                return result;
            }
        }

        public Tuple<int, int> GetIndexWithTotal()
        {
            var index = this.image_control.ImagePaths.FindIndex(l => l == this.image_control.Current.Value);

            return new Tuple<int, int>(index, this.image_control.ImagePaths.Count());
        }


        public IImgOperate GetImgOperate()
        {
            return this.image_control;
        }

        public void SetImgPlay(ImgPlayMode imgPlayMode)
        {
            this.image_control.SetImgPlay(imgPlayMode);

            this.toggle_play.IsChecked = imgPlayMode == ImgPlayMode.停止播放;

            this.ImgPlayModeChanged?.Invoke(imgPlayMode);

        }

        public void SetPositon(int index)
        {
            if (this.image_control.ImagePaths.Count <= index) return;

            string value = this.image_control.ImagePaths[index];

            var current = this.image_control.Collection.Find(value);

            if (this.image_control.Current == current) return;

            this.image_control.Current = current;

            this.image_control.LoadImage(this.image_control.ImagePaths[index]);

            this.media_slider.Value = this.GetSliderValue(index);

            //  Do：触发页更改事件
            this.ImageIndexChanged?.Invoke(this.GetCurrentUrl(), ImgSliderMode.User);
        }

        public void ShowDefects()
        {
            this.image_control.ShowDefects();
        }

        public void ShowLocates()
        {
            this.image_control.ShowLocates();
        }

        public void ShowMarks()
        {
            this.image_control.ShowMarks();
        }

        public void ShowMarks(List<string> markCodes)
        {
            this.image_control.ShowMarks(markCodes);
        }

        public void SetFullScreen(bool isFullScreen)
        {
            this.image_control.SetFullScreen(isFullScreen);
        }

        public void AddImgFigure(Dictionary<string, string> imgFigures)
        {
            this.image_control.AddImgFigure(imgFigures);
        }

        public void PreviousImg()
        {
            this.image_control.PreviousImg();
        }

        public void NextImg()
        {
            this.image_control.NextImg();
        }

        public void ImgPlaySpeedUp()
        {
            this.image_control.ImgPlaySpeedUp();
        }

        public void ImgPlaySpeedDown()
        {
            this.image_control.ImgPlaySpeedDown();
        }

        public void ScreenShot(string saveFullName)
        {
            this.image_control.ScreenShot(saveFullName);
        }

        public void Rotate()
        {
            this.image_control.Rotate();
        }

        public void DeleteSelectMark()
        {
            this.image_control.DeleteSelectMark();
        }
    }


    /// <summary> 时间格式化字符串 </summary>
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