using Microsoft.Win32;
using SureDream.Appliaction.Demo.ImageControl;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApp2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        //  Message：接口实现用例
        ImageViews _imgOperate = new ImageViews();
        public MainWindow()
        {
            InitializeComponent();

            //  Message：设置初始速度
            _imgOperate.Speed = 4;

            _imgOperate.WheelScale = 0.01;

            //  Do：加载图片浏览主键
            this.grid_center.Children.Add(_imgOperate.BuildEntity());

          
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //List<string> images = new List<string>();

            //images.Add(@"F:\GitHub\WPF-Project\SureDream 9.0\Product\Debug\images\1-3.jpg");
            //images.Add(@"F:\GitHub\WPF-Project\SureDream 9.0\Product\Debug\images\2-27.jpg");
            ////control_image.LoadImages(images);

            //_imgOperate.LoadImg(images);

            //ShellWindow window = new ShellWindow();
            //window.Show();
            //return;


            OpenFileDialog open = new OpenFileDialog();

            open.InitialDirectory = @"F:\GitHub\WPF-Project\SureDream 9.0\Product\Debug\images\";

            var result = open.ShowDialog();

            List<string> images = new List<string>();

            if (result.HasValue && result.Value)
            {
                var files = Directory.GetFiles(System.IO.Path.GetDirectoryName(open.FileName));

                foreach (var item in files)
                {
                    if (System.IO.Path.GetExtension(item).EndsWith("jpg") || System.IO.Path.GetExtension(item).EndsWith("png"))
                    {
                        images.Add(item);
                    }
                }
            } 
            _imgOperate.LoadImg(images);

        }
    }
}
