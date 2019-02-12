using FtpSyn;
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

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;


            string[] str = { "1", "3", "4", "5", "7" };

           var co=  str.Take(str.ToList().IndexOf("4")).ToList();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
           //this.media.Source= new Uri(" ftp://Healthy:870210lhj@127.0.0.1/media.mp4", UriKind.RelativeOrAbsolute);


           // //this.media.Source = new Uri("ftp://192.168.0.104/media.mkv", UriKind.Absolute);
           // //ftp://Healthy:870210lhj@127.0.0.1/images/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          var ss=   FtpHelper.GetFileList(@"ftp://192.168.0.104/images/");
        }

        private void Slider_MouseDown(object sender, MouseButtonEventArgs e)
        {

            Debug.WriteLine("Slider_MouseDown");


        }

        private void Slider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            Debug.WriteLine("Slider_DragDelta");
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Debug.WriteLine("Slider_ValueChanged");
        }
    }
}
