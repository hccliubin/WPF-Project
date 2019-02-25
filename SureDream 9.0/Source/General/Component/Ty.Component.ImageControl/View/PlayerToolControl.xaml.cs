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

            this.DragCompleted?.Invoke(sender,e);
        }
    }
}
