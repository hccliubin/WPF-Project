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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ty.Component.ImageControl
{
    /// <summary>
    /// ImageOprateCtrEntity.xaml 的交互逻辑
    /// </summary>
    public partial class ImageOprateCtrEntity : UserControl
    {
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

        protected void OnLastClicked()
        {
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

        protected void OnNextClick()
        {
            RoutedEventArgs args = new RoutedEventArgs(NextClickRoutedEvent, this);
            this.RaiseEvent(args);
        }

        private void button_last_Click(object sender, RoutedEventArgs e)
        {
            this.RefreshPart();

            this.OnLastClicked();
        }

        private void button_next_Click(object sender, RoutedEventArgs e)
        {
            this.RefreshPart();

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
    }
}
