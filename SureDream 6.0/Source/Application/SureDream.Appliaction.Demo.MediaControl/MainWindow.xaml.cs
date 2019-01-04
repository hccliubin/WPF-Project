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

namespace SureDream.Appliaction.Demo.MediaControl
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_play_avi_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi";

            this.media.Load(filePath);
        }

        private void Btn_play_mkv_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("说明");

        }

        private void Btn_play_mp4_Click(object sender, RoutedEventArgs e)
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "media.mp4");

            this.media.Load(filePath);
        }

        private void Btn_play_local_Click(object sender, RoutedEventArgs e)
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "media.mp4");

            this.media.Load(filePath);
        }

        private void Btn_play_localarea_Click(object sender, RoutedEventArgs e)
        {
            string filePath = @"\\Desktop-bem7r0b\视频格式大全\6-9+有关梯度下降法的更多深入讨论.mp4";

            this.media.Load(filePath);
        }

        private void Btn_play_http_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi";

            this.media.Load(filePath);
        }

        private void Btn_play_setpostion_Click(object sender, RoutedEventArgs e)
        {
            this.media.SetPositon(TimeSpan.FromSeconds(60));
        }

        private void Btn_play_repeat_Click(object sender, RoutedEventArgs e)
        {
            this.media.RepeatFromTo(TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(70));
        }

        private void Btn_play_screen_Click(object sender, RoutedEventArgs e)
        {

            string fileName = DateTime.Now.ToString("yyyyMMddhhmmss");

            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images", fileName + ".jpg");

            this.media.ScreenShot(TimeSpan.FromSeconds(60), filePath);

            Process.Start(filePath);
        }

        private void Btn_play_imagelist_Click(object sender, RoutedEventArgs e)
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");

            var forder = Directory.CreateDirectory(filePath);

            List<string> imgs = forder.GetFiles().Select(l => l.FullName).ToList();

            this.media.LoadImages(imgs);
        }

        private void Btn_play_imagefoder_Click(object sender, RoutedEventArgs e)
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");

            this.media.LoadImageFolder(filePath);
        }

        private void Btn_play_currentUrl_Click(object sender, RoutedEventArgs e)
        {
            var result = this.media.GetCurrentUrl();

            MessageBox.Show(result);
        }

        private void Btn_play_currentframe_Click(object sender, RoutedEventArgs e)
        {
            var result = "当前：" + this.media.GetCurrentFrame();

            result += " - 总计：" + this.media.GetTotalFrame();

            MessageBox.Show(result);
        }

        private void Btn_play_localftp_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("ftp");
        }
    }
}
