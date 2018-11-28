using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Ty.Component.ImageControl
{
    /// <summary>
    /// ImageOprateCtrEntity.xaml 的交互逻辑
    /// </summary>
    public partial class ImageOprateCtrEntity : UserControl
    {

        LinkedList<string> _collection = new LinkedList<string>();

        LinkedListNode<string> current;


        Timer timer = new Timer();

        public ImageOprateCtrEntity()
        {
            InitializeComponent();

            timer.Interval = 1000;

            timer.Elapsed += (l, k) =>
              {
                  Application.Current.Dispatcher.Invoke(() =>
                  {
                      timer.Interval = 1000 * this.Speed;

                      if (this.ImgPlayMode == ImgPlayMode.正序)
                      {
                          this.OnNextClick();
                      }
                      else if (this.ImgPlayMode == ImgPlayMode.倒叙)
                      {
                          this.OnLastClicked();
                      }
                  });

              };

        }

        public ImageControlViewModel ViewModel
        {
            get
            {
                return (ImageControlViewModel)this.DataContext;
            }
            set
            {
                this.DataContext = value;
            }
        }


        private void control_imageView_BegionShowPartView(object sender, RoutedEventArgs e)
        {
            this.control_ImagePartView.Visibility = Visibility.Visible;

            this.control_imageView.ShowRectangleClip();
        }

        private void control_ImagePartView_Closed(object sender, RoutedEventArgs e)
        {
            this.control_imageView.HideRectangleClip();
        }



        public List<string> ImagePaths
        {
            get { return (List<string>)GetValue(ImagePathsProperty); }
            set { SetValue(ImagePathsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImagePathsProperty =
            DependencyProperty.Register("ImagePaths", typeof(List<string>), typeof(ImageOprateCtrEntity), new PropertyMetadata(default(List<string>), (d, e) =>
             {
                 ImageOprateCtrEntity control = d as ImageOprateCtrEntity;

                 if (control == null) return;

                 List<string> config = e.NewValue as List<string>;

                 if (config == null) return;

                 if (config.Count == 0) return;

                 if (!File.Exists(config.First())) return;

                 foreach (var item in config)
                 {
                     control._collection.AddLast(item);
                 }

                 control.Current = control._collection.First;

                 //control.current = control._collection.Find(config.First());

                 control.LoadImage(control.Current.Value);


             }));

        void LoadImage(string imagePath)
        {
            if (imagePath == null) return;

            if (!File.Exists(imagePath)) return;

            ImageControlViewModel viewModel = new ImageControlViewModel();

            viewModel.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Absolute));

            //viewModel.ImgMarkOperateEvent += this.ImgMarkOperateEvent;

            this.ViewModel = viewModel;
        }


        public ImgPlayMode ImgPlayMode
        {
            get { return (ImgPlayMode)GetValue(ImgPlayModeProperty); }
            set { SetValue(ImgPlayModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImgPlayModeProperty =
            DependencyProperty.Register("ImgPlayMode", typeof(ImgPlayMode), typeof(ImageOprateCtrEntity), new PropertyMetadata(ImgPlayMode.停止播放, (d, e) =>
             {
                 ImageOprateCtrEntity control = d as ImageOprateCtrEntity;

                 if (control == null) return;

                 ImgPlayMode config = (ImgPlayMode)e.NewValue;

                 if (config == ImgPlayMode.正序 || config == ImgPlayMode.倒叙)
                 {
                     control.timer.Start();
                 }
                 else if (config == ImgPlayMode.停止播放)
                 {
                     control.timer.Stop();
                 }

             }));


        public double Speed
        {
            get { return (double)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }

        public LinkedListNode<string> Current { get => current; set => current = value; }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(double), typeof(ImageOprateCtrEntity), new PropertyMetadata(1.0, (d, e) =>
             {
                 ImageOprateCtrEntity control = d as ImageOprateCtrEntity;

                 if (control == null) return;

                 //int config = e.NewValue as int;

             }));


        //声明和注册路由事件
        public static readonly RoutedEvent LastClickedRoutedEvent =
            EventManager.RegisterRoutedEvent("LastClicked", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ImageOprateCtrEntity));
        //CLR事件包装
        public event RoutedEventHandler LastClicked
        {
            add { this.AddHandler(LastClickedRoutedEvent, value); }
            remove { this.RemoveHandler(LastClickedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        public void OnLastClicked()
        {
            this.RefreshPart();

            Current = Current.Previous;

            if (Current == null)
            {
                Current = _collection.Last;
            }

            this.LoadImage(Current.Value);


            RoutedEventArgs args = new RoutedEventArgs(LastClickedRoutedEvent, this);
            this.RaiseEvent(args);
        }


        //声明和注册路由事件
        public static readonly RoutedEvent NextClickRoutedEvent =
            EventManager.RegisterRoutedEvent("NextClick", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ImageOprateCtrEntity));
        //CLR事件包装
        public event RoutedEventHandler NextClick
        {
            add { this.AddHandler(NextClickRoutedEvent, value); }
            remove { this.RemoveHandler(NextClickRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        public void OnNextClick()
        {
            this.RefreshPart();

            Current = Current.Next;

            if (Current == null)
            {
                Current = _collection.First;
            }

            this.LoadImage(Current.Value);

            RoutedEventArgs args = new RoutedEventArgs(NextClickRoutedEvent, this);
            this.RaiseEvent(args);
        }

        private void button_last_Click(object sender, RoutedEventArgs e)
        {


            this.OnLastClicked();
        }

        private void button_next_Click(object sender, RoutedEventArgs e)
        {


            this.OnNextClick();
        }

        void RefreshPart()
        {
            this.control_ImagePartView.Visibility = Visibility.Collapsed;
            this.control_imageView.Clear();
        }

        private void CommandBinding_LastImage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.RefreshPart();

            this.OnLastClicked();
        }

        private void CommandBinding_LastImage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ViewModel != null;
        }

        private void CommandBinding_NextImage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.RefreshPart();

            this.OnNextClick();
        }

        private void CommandBinding_NextImage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ViewModel != null;
        }


        private void CommandBinding_FullScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.ShowFullScreen();
        }

        void ShowFullScreen()
        {
            ImageFullScreenWindow window = new ImageFullScreenWindow();

            window.DataContext = this.ViewModel;
            this.ClearToScreen();
            window.CenterContent = this.grid_all;
            window.ShowDialog();
            this.RecoverFromScreen();
        }

        private void CommandBinding_FullScreen_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ViewModel != null;
        }

        private void CommandBinding_ShowStyleTool_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.btn_imageStyle.IsChecked = !this.btn_imageStyle.IsChecked;
        }

        private void CommandBinding_ShowStyleTool_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ViewModel != null;
        }

        void ClearToScreen()
        {
            this.Content = null;
            this.btn_fullScreen.Visibility = Visibility.Collapsed;
        }

        void RecoverFromScreen()
        {
            this.Content = this.grid_all;
            this.btn_fullScreen.Visibility = Visibility.Visible;
        }


        //声明和注册路由事件
        public static readonly RoutedEvent SaveClickRoutedEvent =
            EventManager.RegisterRoutedEvent("SaveClick", RoutingStrategy.Bubble, typeof(EventHandler<ImgMarkRoutedEventArgs>), typeof(ImageOprateCtrEntity));
        //CLR事件包装
        public event RoutedEventHandler SaveClick
        {
            add { this.AddHandler(SaveClickRoutedEvent, value); }
            remove { this.RemoveHandler(SaveClickRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnSaveClick()
        {
            ImageMarkEngine engine = new ImageMarkEngine();

            ImgMarkRoutedEventArgs args = new ImgMarkRoutedEventArgs(SaveClickRoutedEvent, this, engine);

            this.RaiseEvent(args);
        }


        private void CommandBinding_Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //this.OnSaveClick();

            foreach (var item in this.ViewModel.SampleCollection)
            {
                this.ImgMarkOperateEvent?.Invoke(item.Model);
            }

            Debug.WriteLine("保存");
        }

        private void CommandBinding_Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ViewModel != null;
        }

        public void RefreshAll()
        {
            this.control_imageView.RefreshAll();
        }

        private void IconButton_Click(object sender, RoutedEventArgs e)
        {
            IconButton btn = e.OriginalSource as IconButton;

            string imgPath = this.Current.Value;

            ImgProcessType imgProcessType = (ImgProcessType)Enum.Parse(typeof(ImgProcessType), btn.ToolTip.ToString());

            this.ImgProcessEvent?.Invoke(imgPath, imgProcessType);

        }
    }


    /// <summary> 接口 </summary>
    partial class ImageOprateCtrEntity : IImgOperate
    {
        public event ImgMarkHandler ImgMarkOperateEvent;

        public event ImgProcessHandler ImgProcessEvent;

        public void AddImgFigure(Dictionary<string, string> imgFigures)
        {
            if (this.ViewModel == null)
            {
                Debug.WriteLine("请先加载图片数据，在添加标定信息");
                return;
            }

            this.ViewModel.FigureCollection = imgFigures;
        }

        public ImageOprateCtrEntity BuildEntity()
        {
            return this;
        }

        public void ImgPlaySpeedDown()
        {
            this.Speed = 2 * this.Speed;
        }

        public void ImgPlaySpeedUp()
        {
            this.Speed = this.Speed / 2;
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

            this._collection.AddLast(imgPath);

            this.Current = this._collection.Last;

            this.LoadImage(imgPath);
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

        public void NextImg()
        {
            this.OnNextClick();
        }

        public void PreviousImg()
        {
            this.OnLastClicked();
        }

        public void SetFullScreen(bool isFullScreen)
        {
            if (isFullScreen)
            {
                ImageViewCommands.FullScreen.Execute(null, this);
            }
            else
            {
                //ApplicationCommands.Close.Execute(null, this.FullWindow);

                this.ShowFullScreen();


            }
        }

        public void SetImgPlay(ImgPlayMode imgPlayMode)
        {
            this.ImgPlayMode = imgPlayMode;
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
    }
}
