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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ty.Component.ImageControl;
using Ty.Component.ImageControl.Provider.Hook;

namespace Ty.Component.MediaControl
{
    /// <summary>
    /// MulMediaPlayerControl.xaml 的交互逻辑
    /// </summary>
    public partial class MulMediaPlayerControl : UserControl
    {
        public MulMediaPlayerControl()
        {
            InitializeComponent();

        }

        public List<IVdeioImagePlayerService> MediaSources
        {
            get { return (List<IVdeioImagePlayerService>)GetValue(MediaSourcesProperty); }
            set { SetValue(MediaSourcesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MediaSourcesProperty =
            DependencyProperty.Register("MediaSources", typeof(List<IVdeioImagePlayerService>), typeof(MulMediaPlayerControl), new PropertyMetadata(default(List<IVdeioImagePlayerService>), (d, e) =>
             {
                 MulMediaPlayerControl control = d as MulMediaPlayerControl;

                 if (control == null) return;

                 List<IVdeioImagePlayerService> config = e.NewValue as List<IVdeioImagePlayerService>;

                 foreach (var item in config)
                 {
                     item.PlayerToolControl = control.playtool;
                 }

                 if (config.Count == 1)
                 {
                     control.RowCount = 1;
                     control.ColCount = 1;
                 }

                 if (config.Count == 2)
                 {
                     control.RowCount = 1;
                     control.ColCount = 2;
                 }

                 if (config.Count == 3 || config.Count == 4)
                 {
                     control.RowCount = 2;
                     control.ColCount = 2;
                 }

                 if (config.Count == 5 || config.Count == 6)
                 {
                     control.RowCount = 3;
                     control.ColCount = 2;
                 }

                 if (config.Count == 7 || config.Count == 8 || config.Count == 9)
                 {
                     control.RowCount = 3;
                     control.ColCount = 3;
                 }


             }));



        public int RowCount
        {
            get { return (int)GetValue(RowCountProperty); }
            set { SetValue(RowCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.Register("RowCount", typeof(int), typeof(MulMediaPlayerControl), new PropertyMetadata(1, (d, e) =>
             {
                 MulMediaPlayerControl control = d as MulMediaPlayerControl;

                 if (control == null) return;

                 //int config = e.NewValue as int;

             }));


        public int ColCount
        {
            get { return (int)GetValue(ColCountProperty); }
            set { SetValue(ColCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColCountProperty =
            DependencyProperty.Register("ColCount", typeof(int), typeof(MulMediaPlayerControl), new PropertyMetadata(1, (d, e) =>
             {
                 MulMediaPlayerControl control = d as MulMediaPlayerControl;

                 if (control == null) return;

                 //int config = e.NewValue as int;

             }));


    }

    public partial class MulMediaPlayerControl : IMulMediaPlayer
    {
        MediaPlayType _type;


        #region - 图片操作 -

        #region - 加载功能 -

        public void LoadImageFolders(params Tuple<List<string>, string>[] imageFoders)
        {
            List<IVdeioImagePlayerService> services = new List<IVdeioImagePlayerService>();

            if (imageFoders == null) return;

            // 检查文件数量是否一样
            //bool check = imageFoders.ToList().TrueForAll(l => l.Count == imageFoders.First().Count);

            //if (!check)
            //{
            //    Debug.WriteLine("参数错误！请检查，传入的多个数组中，数量必须相等"); return;
            //}

            foreach (var item in imageFoders)
            {
                VedioImagePlayerControl control = new VedioImagePlayerControl();

                services.Add(control);
            }

            this.MediaSources = services;

            for (int i = 0; i < imageFoders.Length; i++)
            {
                services[i].LoadImageFolder(imageFoders[i].Item1, imageFoders[i].Item2);

            }


        }

        public void LoadImageFtpFolders(string useName, string passWord, params Tuple<List<string>, string>[] imageFoders)
        {
            throw new NotImplementedException();
        }

        public void LoadImageList(params List<string>[] ImageUrls)
        {
            List<IVdeioImagePlayerService> services = new List<IVdeioImagePlayerService>();

            if (ImageUrls == null) return;

            bool check = ImageUrls.ToList().TrueForAll(l => l.Count == ImageUrls.First().Count);

            if (!check)
            {
                Debug.WriteLine("参数错误！请检查，传入的多个数组中，数量必须相等"); return;
            }

            foreach (var item in ImageUrls)
            {
                VedioImagePlayerControl control = new VedioImagePlayerControl();

                control.LoadImages(item);

                services.Add(control);
            }

            this.MediaSources = services;
        }

        public void LoadImageShareFolders(string useName, string passWord, string ip, params Tuple<List<string>, string>[] imageFoders)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region - 事件功能 -
        public event Action<ImgMarkEntity, int> ImageIndexMarkOperateEvent;
        public event Action<string, ImgProcessType, int> ImageIndexProcessEvent;
        public event Action<int> ImageIndexPreviousEvent;
        public event Action<int> ImageIndexNextEvent;
        public event Action<ImgMarkEntity, MarkType, int> ImageIndexDrawMarkedMouseUp;
        public event Action<string, int> ImageIndexDeletedClicked;
        public event Action<ImgMarkEntity, int> ImageMarkEntitySelectChanged;
        public event Action<string, ImgSliderMode> ImageIndexChanged;
        public event Action<ImgPlayMode> ImgPlayModeChanged;
        public event Action<int> ImageIndexFullScreenEvent;

        #endregion

        #region - 图片交互 -

        public void AddImageIndexMark(ImgMarkEntity imgMarkEntity, int index = 0)
        {
            throw new NotImplementedException();
        }

        public void CancelAddImageIndexMark(int index = 0)
        {
            throw new NotImplementedException();
        }

        public void DeleteImageIndexSelectMark(int index = 0)
        {
            throw new NotImplementedException();
        }

        public ImgMarkEntity GetImageIndexSelectMark(int index = 0)
        {
            throw new NotImplementedException();
        }



        public void ScreenShotImageIndex(string saveFullName, int index = 0)
        {
            throw new NotImplementedException();
        }

        public void SetImageEnlarge(int index = 0)
        {
            throw new NotImplementedException();
        }

        public void SetImageIndexAdaptiveSize(int index = 0)
        {
            throw new NotImplementedException();
        }

        public void SetImageIndexBubbleScale(double value, int index = 0)
        {
            throw new NotImplementedException();
        }

        public void SetImageIndexDetialText(string value, int index = 0)
        {
            throw new NotImplementedException();
        }

        public void SetImageIndexMarkOperate(ImgMarkEntity entity, int index = 0)
        {
            throw new NotImplementedException();
        }

        public void SetImageIndexMarkType(MarkType markType, int index = 0)
        {
            throw new NotImplementedException();
        }

        public void SetImageIndexOriginalSize(int index = 0)
        {
            throw new NotImplementedException();
        }

        public void SetImageIndexPartShotCut(ShortCutEntitys shortcut, int index = 0)
        {
            throw new NotImplementedException();
        }

        public void SetImageIndexPositon(int postion, int index = 0)
        {
            throw new NotImplementedException();
        }

        public void SetImageIndexRotateLeft(int index)
        {
            throw new NotImplementedException();
        }

        public void SetImageIndexRotateRight(int index)
        {
            throw new NotImplementedException();
        }

        public void SetImageIndexSelectMark(Predicate<ImgMarkEntity> match, int index = 0)
        {
            throw new NotImplementedException();
        }

        public void SetImageIndexSpeed(double value, int index = 0)
        {
            throw new NotImplementedException();
        }

        public void SetImageIndexWheelMode(bool value, int index = 0)
        {
            throw new NotImplementedException();
        }

        public void SetImageIndexWheelScale(double value, int index = 0)
        {
            throw new NotImplementedException();
        }

        public void SetImageNarrow(int index = 0)
        {
            throw new NotImplementedException();
        }



        public void ShowAllImageIndexDefects(int index = 0)
        {
            throw new NotImplementedException();
        }

        public void ShowImageIndexLocates(int index = 0)
        {
            throw new NotImplementedException();
        }

        public void ShowImageIndexMarks(int index = 0)
        {
            throw new NotImplementedException();
        }

        public void ShowImageIndexMarks(List<string> markCodes, int index)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region - 播放控制 -


        public ImgPlayMode GetImagePlayMode()
        {
            throw new NotImplementedException();
        }

        public void SetImagePlayMode(ImgPlayMode imgPlayMode)
        {
            throw new NotImplementedException();
        }

        public void SetImagePlayStepDown()
        {
            throw new NotImplementedException();
        }

        public void SetImagePlayStepUp()
        {
            throw new NotImplementedException();
        }

        public void SetImageScale(int index = 0)
        {
            throw new NotImplementedException();
        }

        public void SetImageSpeedDown()
        {
            throw new NotImplementedException();
        }

        public void SetImageVoiceStepDown()
        {
            throw new NotImplementedException();
        }

        public void SetImageVoiceStepUp()
        {
            throw new NotImplementedException();
        }

        public void SetImageWeelPlayMode(bool value)
        {
            throw new NotImplementedException();
        }

        public void SetImagNext()
        {
            throw new NotImplementedException();
        }

        public void SetImagPrevious()
        {
            throw new NotImplementedException();
        }

        public void SetImagSpeedUp()
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion




    }

}
