using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Ty.Component.MediaControl.Provider;

namespace WpfApp3
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        ImageCacheEngine engine = null;



        public MainWindow()
        {
            InitializeComponent();

            //List<string> files = new List<string>();

            //for (int i = 0; i < 100; i++)
            //{
            //    files.Add("dgdgdgd" + i);
            //}

            var forder = Directory.CreateDirectory(@"F:\GitHub\WPF-Project\SureDream 6.0\Product\Debug\images");

            var collection = forder.GetFiles().Select(l => l.FullName).ToList();

            string local = AppDomain.CurrentDomain.BaseDirectory;

            engine = new ImageCacheEngine(collection, local, collection.First(), "ss", "217.0.0.1");

            this.lll.ItemsSource = collection;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            engine.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            var ss = engine.BeginPlay(this.lll.SelectedItem.ToString());

            Debug.WriteLine("播放完成:" + ss);

        }
    }
}
