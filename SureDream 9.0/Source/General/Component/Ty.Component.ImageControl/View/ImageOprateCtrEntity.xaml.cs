using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
using Ty.Component.ImageControl.Provider.Hook;

namespace Ty.Component.ImageControl
{
    /// <summary>
    /// 图片浏览工具主页面
    /// </summary>
    public partial class ImageOprateCtrEntity : UserControl
    {



        /// <summary>
        /// 构造函数
        /// </summary>
        public ImageOprateCtrEntity()
        {
            InitializeComponent();


            ////  Do：初始化自动播放
            //timer.Interval = 1000;

            //timer.Elapsed += (l, k) =>
            //{
            //    Application.Current.Dispatcher.Invoke(() =>
            //    {
            //        timer.Interval = 1000 * this.Speed;

            //        if (this.ImgPlayMode == ImgPlayMode.正序)
            //        {
            //            this.OnNextClick();
            //        }
            //        else if (this.ImgPlayMode == ImgPlayMode.倒叙)
            //        {
            //            this.OnLastClicked();
            //        }
            //    });

            //};

            this.RegisterDefaltApi();

        }


        #region - 成员属性 -

        //  Do：所有图片路径集合
        LinkedList<string> _collection = new LinkedList<string>();

        //  Do：当前图片路径
        LinkedListNode<string> current;

        ////  Do：自动播放时间处理
        //Timer timer = new Timer();

        public LinkedListNode<string> Current { get => current; set => current = value; }

        /// <summary>
        /// 绑定的ViewModel模型
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
            DependencyProperty.Register("ImagePaths", typeof(List<string>), typeof(ImageOprateCtrEntity), new PropertyMetadata(default(List<string>), (d, e) =>
            {
                ImageOprateCtrEntity control = d as ImageOprateCtrEntity;

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



        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImgPlayModeProperty =
            DependencyProperty.Register("ImgPlayMode", typeof(ImgPlayMode), typeof(ImageOprateCtrEntity), new PropertyMetadata(ImgPlayMode.停止播放, (d, e) =>
            {
                ImageOprateCtrEntity control = d as ImageOprateCtrEntity;

                if (control == null) return;

                ImgPlayMode config = (ImgPlayMode)e.NewValue;

                //  Do：设置自动播放模式
                if (config == ImgPlayMode.正序 || config == ImgPlayMode.倒叙)
                {
                    control.Start();

                    control._tempMarkType = control.MarkType;

                    control.SetMarkType(MarkType.None);
                }
                else if (config == ImgPlayMode.停止播放)
                {
                    control.Stop();

                    control.SetMarkType(control._tempMarkType);
                }

            }));

        MarkType _tempMarkType;

        /// <summary>
        /// 自动播放速度
        /// </summary>
        public double Speed
        {
            get { return (double)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }

        public LinkedList<string> Collection { get => _collection; set => _collection = value; }


        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(double), typeof(ImageOprateCtrEntity), new PropertyMetadata(1.0, (d, e) =>
            {
                ImageOprateCtrEntity control = d as ImageOprateCtrEntity;

                if (control == null) return;

            }));

        #endregion

        #region - 路由事件 -

        //声明和注册路由事件
        public static readonly RoutedEvent LastClickedRoutedEvent =
            EventManager.RegisterRoutedEvent("LastClicked", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ImageOprateCtrEntity));

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
            this.RefreshPart();

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

        //声明和注册路由事件
        public static readonly RoutedEvent NextClickRoutedEvent =
            EventManager.RegisterRoutedEvent("NextClick", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ImageOprateCtrEntity));

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
            this.RefreshPart();

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

        #region - 绑定命令 -

        /// <summary>
        /// 上一页绑定命令
        /// </summary>
        private void CommandBinding_LastImage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.RefreshPart();

            this.OnLastClicked();
        }

        /// <summary>
        /// 上一页绑定命令是否可以执行
        /// </summary>
        private void CommandBinding_LastImage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ViewModel != null;
        }

        /// <summary>
        /// 下一页绑定命令
        /// </summary>
        private void CommandBinding_NextImage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.RefreshPart();

            this.OnNextClick();
        }

        /// <summary>
        /// 下一页绑定命令是否可以执行
        /// </summary>
        private void CommandBinding_NextImage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ViewModel != null;
        }

        /// <summary>
        /// 全屏绑定命令
        /// </summary>
        private void CommandBinding_FullScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.ShowFullScreen();
        }

        /// <summary>
        /// 全屏绑定命令是否可以执行
        /// </summary>
        private void CommandBinding_FullScreen_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ViewModel != null;
        }

        /// <summary>
        /// 显示/隐藏图像滤镜处理
        /// </summary>
        private void CommandBinding_ShowStyleTool_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.btn_imageStyle.IsChecked = !this.btn_imageStyle.IsChecked;
        }

        /// <summary>
        /// 显示/隐藏图像滤镜处理是否可以执行
        /// </summary>
        private void CommandBinding_ShowStyleTool_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ViewModel != null;
        }

        /// <summary>
        /// 保存绑定命令
        /// </summary>
        private void CommandBinding_Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            foreach (var item in this.ViewModel.SampleCollection)
            {
                this.ImgMarkOperateEvent?.Invoke(item.Model);
            }

            Debug.WriteLine("保存");
        }

        /// <summary>
        /// 保存绑定命令是否可以执行
        /// </summary>
        private void CommandBinding_Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ViewModel != null;
        }

        ///// <summary>
        ///// 放大显示缺陷控件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void CommandBinding_ShowDefectPart_Executed(object sender, ExecutedRoutedEventArgs e)
        //{

        //    Debug.WriteLine("CommandBinding_ShowDefectPart_Executed");

        //    if (this.control_ImagePartView.Visibility == Visibility.Visible)
        //    {
        //        this.control_ImagePartView.OnClosed();
        //    }
        //    else
        //    {
        //        this.control_imageView.ShowDefaultDefectPart();
        //    }
        //}

        //private void CommandBinding_ShowDefectPart_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = this.ViewModel != null;

        //    Debug.WriteLine("CommandBinding_ShowDefectPart_CanExecute");
        //}


        //private void CommandBinding_UpKey_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    this.control_imageView.ShowPreShape();
        //}

        //private void CommandBinding_UpKey_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = this.ViewModel != null;

        //    Debug.WriteLine("CommandBinding_UpKey_CanExecute");
        //}

        //private void CommandBinding_DownKey_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    this.control_imageView.ShowPreShape();
        //}

        //private void CommandBinding_DownKey_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = this.ViewModel != null;

        //    Debug.WriteLine("CommandBinding_DownKey_CanExecute");
        //}

        ShortCutHookService _shortCutHookService = new ShortCutHookService();
        /// <summary> 此方法的说明 </summary>
        public void RegisterPartShotCut(ShortCutEntitys shortcut)
        {
            bool flag = false;

            //  Message：先清理事件
            this.control_imageView.ShowDefaultDefectPart(flag);

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

                this.control_imageView.ShowNextShape();
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

                this.control_imageView.ShowNextShape();
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

                if (this.control_ImagePartView.Visibility == Visibility.Visible)
                {
                    this.control_ImagePartView.OnClosed();
                }
                //else
                //{
                //    flag = !flag;

                //    if(flag)
                //    {
                //        Debug.WriteLine("进入模式");
                //    }
                //    else
                //    {
                //        Debug.WriteLine("退出模式");
                //    }

                //    Debug.WriteLine(flag);

                //    this.control_imageView.ShowDefaultDefectPart(flag);
                //}

                flag = !flag;

                if (flag)
                {
                    Debug.WriteLine("进入模式");
                }
                else
                {
                    Debug.WriteLine("退出模式");
                } 

                this.control_imageView.ShowDefaultDefectPart(flag);

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
        #endregion

        #region - 成员方法 -

        //Random random = new Random();

        //  Message：播放任务
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
        /// 加载图片(上一张下一张切换用)
        /// </summary>
        /// <param name="imagePath"> 图片路径 </param>
        public void LoadImage(string imagePath)
        {
            if (imagePath == null) return;

            //if (!File.Exists(imagePath)) return;

            this.RefreshPart();

            Application.Current.Dispatcher.Invoke(() =>
            {
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

                        //Thread.Sleep(5000);

                        s.EndInit();
                        //这一句很重要，少了UI线程就不认了。
                        s.Freeze();



                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            //this.ViewModel.IsBuzy = false;

                            //this.ViewModel = viewModel;

                            this.ViewModel.ImageSource = s;

                            this.ViewModel.IsBuzy = false;
                        });

                    });

                }
                catch (Exception ex)
                {

                    Debug.WriteLine(ex);

                }


                ////  Message：存在上一张先不清理图片，适用于播放
                //if (this.ViewModel != null)
                //{
                //    ImageControlViewModel viewModel = new ImageControlViewModel(this);

                //    this.ViewModel.IsBuzy = true;

                //    try
                //    {
                //        Task.Run(() =>
                //        {
                //            var p = imagePath;
                //            var s = new BitmapImage();
                //            s.BeginInit();
                //            s.CacheOption = BitmapCacheOption.OnLoad;

                //            s.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                //            //Thread.Sleep(5000);


                //            s.EndInit();
                //            //这一句很重要，少了UI线程就不认了。
                //            s.Freeze();

                //            viewModel.ImageSource = s;



                //            Application.Current.Dispatcher.Invoke(() =>
                //            {
                //                this.ViewModel.IsBuzy = false;

                //                this.ViewModel = viewModel;
                //            });

                //        });

                //    }
                //    catch { }
                //}
                //else
                //{
                //    ImageControlViewModel viewModel = new ImageControlViewModel(this);

                //    this.ViewModel = viewModel;

                //    this.ViewModel.IsBuzy = true;

                //    try
                //    {
                //        Task.Run(() =>
                //        {
                //            var p = imagePath;
                //            var s = new BitmapImage();
                //            s.BeginInit();
                //            s.CacheOption = BitmapCacheOption.OnLoad;

                //            s.UriSource = new Uri(imagePath, UriKind.RelativeOrAbsolute);

                //            //Thread.Sleep(5000);

                //            //////打开文件流
                //            ////using (var stream = File.OpenRead(p))
                //            ////{
                //            ////    s.StreamSource = stream;
                //            ////    s.EndInit();
                //            ////    //这一句很重要，少了UI线程就不认了。
                //            ////    s.Freeze();
                //            ////}

                //            s.EndInit();
                //            //这一句很重要，少了UI线程就不认了。
                //            s.Freeze();

                //            viewModel.ImageSource = s;

                //            Application.Current.Dispatcher.Invoke(() =>
                //            {
                //                this.ViewModel.IsBuzy = false;
                //            });


                //        });

                //    }
                //    catch { }
                //}






            });

        }

        //private void button_last_Click(object sender, RoutedEventArgs e)
        //{


        //    this.OnLastClicked();
        //}

        //private void button_next_Click(object sender, RoutedEventArgs e)
        //{


        //    this.OnNextClick();
        //}

        /// <summary>
        /// 关闭时刷新局部放大页面
        /// </summary>
        void RefreshPart()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.control_ImagePartView.Visibility = Visibility.Collapsed;
                this.control_imageView.Clear();
            });
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
            window.ShowDialog();
            this.RecoverFromScreen();
        }

        /// <summary>
        /// 退出全屏模式清理
        /// </summary>
        void ClearToScreen()
        {
            this.Content = null;
            this.btn_fullScreen.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// 刷新显示全屏
        /// </summary>
        void RecoverFromScreen()
        {
            this.Content = this.grid_all;
            this.btn_fullScreen.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 重新页面刷新数据
        /// </summary>
        public void RefreshAll()
        {
            this.control_imageView.RefreshAll();
        }

        /// <summary>
        /// 点击滤镜按钮 触发接口注册事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IconButton_Click(object sender, RoutedEventArgs e)
        {
            IconButton btn = e.OriginalSource as IconButton;

            if (this.Current == null) return;

            string imgPath = this.Current.Value;

            ImgProcessType imgProcessType = (ImgProcessType)Enum.Parse(typeof(ImgProcessType), btn.ToolTip.ToString());

            this.ImgProcessEvent?.Invoke(imgPath, imgProcessType);

        }

        /// <summary>
        /// 显示局部放大页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void control_imageView_BegionShowPartView(object sender, RoutedEventArgs e)
        {
            this.control_ImagePartView.Visibility = Visibility.Visible;

            this.control_imageView.ShowRectangleClip();
        }

        /// <summary>
        /// 关闭局部放大页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void control_ImagePartView_Closed(object sender, RoutedEventArgs e)
        {
            this.control_imageView.HideRectangleClip();
        }


        #endregion

    }


    /// <summary> 外部接口实现 </summary>
    partial class ImageOprateCtrEntity : IImgOperate
    {
        public MarkType MarkType { get; set; }
        public double Scale { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsWheelPlay { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event ImgMarkHandler ImgMarkOperateEvent;

        /// <summary>
        /// 触发新增事件（此方法）
        /// </summary>
        /// <param name="entity"></param>
        internal void OnImgMarkOperateEvent(ImgMarkEntity entity)
        {
            this.ImgMarkOperateEvent?.Invoke(entity);
        }

        public event ImgProcessHandler ImgProcessEvent;

        public event Action PreviousImgEvent;

        public event Action NextImgEvent;

        public event Action<ImgMarkEntity, MarkType> DrawMarkedMouseUp;
        public event Action<string> DeleteImgEvent;
        public event Action<bool> FullScreenChangedEvent;

        internal void OnDrawMarkedMouseUp()
        {
            ImgMarkEntity imgMarkEntity = new ImgMarkEntity();

            this.DrawMarkedMouseUp?.Invoke(imgMarkEntity, this.MarkType);
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

        public Control BuildEntity()
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

            this.Collection.AddLast(imgPath);

            this.Current = this.Collection.Last;

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

        /// <summary>
        /// 设置标识位
        /// </summary>
        /// <param name="markType"></param>
        public void SetMarkType(MarkType markType)
        {
            this.MarkType = markType;

            if (markType == MarkType.None)
            {
                //  Message：设置光标和区域放大
                this.control_imageView.canvas.Cursor = Cursors.Arrow;

                this.control_imageView.r_screen.IsEnabled = false;
            }
            else
            {
                //  Message：设置光标和区域放大
                this.control_imageView.canvas.Cursor = Cursors.Cross;

                this.control_imageView.r_screen.IsEnabled = true;
            }
        }

        /// <summary>
        /// 获取当前选择项
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 设置当前选中图片已标定的矩形框
        /// </summary>
        /// <returns></returns>
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

        public void AddMark(ImgMarkEntity imgMarkEntity)
        {
            this.control_imageView.AddMark(imgMarkEntity);
        }

        public void CancelAddMark()
        {
            this.control_imageView.ClearDynamic();
        }

        public void Rotate()
        {
            this.control_imageView.Rotate();
        }

        public void ScreenShot(string saveFullName)
        {
            this.control_imageView.ScreenShot(saveFullName);
        }

        public void DeleteSelectMark()
        {
            var entity = this.GetSelectMarkEntity();

            entity.markOperateType = ImgMarkOperateType.Delete;

            this.MarkOperate(entity);
        }

        void IImgOperate.OnImgMarkOperateEvent(ImgMarkEntity entity)
        {
            throw new NotImplementedException();
        }

        public void SetEnlarge()
        {
            throw new NotImplementedException();
        }

        public void SetNarrow()
        {
            throw new NotImplementedException();
        }

        public void SetRotateLeft()
        {
            throw new NotImplementedException();
        }

        public void SetRotateRight()
        {
            throw new NotImplementedException();
        }

        public string GetCurrentUrl()
        {
            throw new NotImplementedException();
        }
    }
}
