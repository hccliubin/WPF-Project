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
using System.Windows.Shapes;

namespace Ty.Component.ImageControl
{
    /// <summary>
    /// ImageFullScreenWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ImageFullScreenWindow : Window
    {
        public ImageFullScreenWindow()
        {
            InitializeComponent();
        }


        public Visual ImageVisual
        {
            set
            {
                this.visualbrush_image.Visual = value;
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        //    public Visual ImageVisual
        //    {
        //        get { return (Visual)GetValue(ImageVisualProperty); }
        //        set { SetValue(ImageVisualProperty, value); }
        //    }

        //    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //    public static readonly DependencyProperty ImageVisualProperty =
        //        DependencyProperty.Register("ImageVisual", typeof(Visual), typeof(ImageFullScreenWindow), new PropertyMetadata(default(Visual), (d, e) =>
        //        {
        //            ImageFullScreenWindow control = d as ImageFullScreenWindow;

        //            if (control == null) return;

        //            Visual config = e.NewValue as Visual;

        //            control.visualbrush_image.Visual = config;

        //        }));
    }
}
