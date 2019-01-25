using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageView
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            btnSetImage.Click += btnSetImage_Click;
           // thumbImage = img..GetThumbnailImage(thumbWidth, thumbHeight, null, IntPtr.Zero) as Bitmap;
        }

        void btnSetImage_Click(object sender, RoutedEventArgs e)
        {
            imageViews.Source = new BitmapImage(new Uri("2-27.jpg", UriKind.Relative));
            //throw new NotImplementedException();
        }

      
    }
}
