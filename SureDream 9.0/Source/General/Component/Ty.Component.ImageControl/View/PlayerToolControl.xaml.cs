using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// PlayerToolControl.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerToolControl : UserControl
    {
        public PlayerToolControl()
        {
            InitializeComponent();
        }

        public event DragCompletedEventHandler DragCompleted;

        //  Message：标识拖动条是否随播放变化
        internal bool SliderFlag { get; set; } = false;

        private void media_slider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            this.SliderFlag = true;
        }

        private void media_slider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            this.SliderFlag = false;

            this.DragCompleted?.Invoke(sender, e);
        }

        /// <summary> 多个图片播放时用于检测播放同步 </summary>
        public List<IImgOperate> IImgOperateCollection { get; set; } = new List<IImgOperate>();



        public bool IsBuzy
        {
            get { return (bool)GetValue(IsBuzyProperty); }
            set { SetValue(IsBuzyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBuzyProperty =
            DependencyProperty.Register("IsBuzy", typeof(bool), typeof(PlayerToolControl), new PropertyMetadata(default(bool), (d, e) =>
             {
                 PlayerToolControl control = d as PlayerToolControl;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));


        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(PlayerToolControl), new PropertyMetadata(default(string), (d, e) =>
             {
                 PlayerToolControl control = d as PlayerToolControl;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


    }
}
