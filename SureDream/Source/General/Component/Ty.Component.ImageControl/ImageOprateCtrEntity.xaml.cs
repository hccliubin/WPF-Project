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

namespace Ty.Component.ImageControl
{
    /// <summary>
    /// ImageOprateCtrEntity.xaml 的交互逻辑
    /// </summary>
    public partial class ImageOprateCtrEntity : UserControl
    {

        ImageFullScreenWindow _fullWindow = new ImageFullScreenWindow();
        public ImageOprateCtrEntity()
        {
            InitializeComponent();
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

        public ImageFullScreenWindow FullWindow { get => _fullWindow; set => _fullWindow = value; }

        private void control_imageView_BegionShowPartView(object sender, RoutedEventArgs e)
        {
            this.control_ImagePartView.Visibility = Visibility.Visible;

            this.control_imageView.ShowRectangleClip();
        }

        private void control_ImagePartView_Closed(object sender, RoutedEventArgs e)
        {
            this.control_imageView.HideRectangleClip();
        }


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
            //ImageFullScreenWindow window = new ImageFullScreenWindow();
            
            FullWindow.DataContext = this.ViewModel;
            this.ClearToScreen();
            FullWindow.CenterContent = this.grid_all;
            FullWindow.ShowDialog();
            this.RecoverFromScreen();
        }

        private void CommandBinding_FullScreen_CanExecute(object sender, CanExecuteRoutedEventArgs e)
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
            this.OnSaveClick();

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
    }
}
