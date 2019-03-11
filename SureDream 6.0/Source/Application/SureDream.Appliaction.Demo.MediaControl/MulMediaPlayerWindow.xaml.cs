using CDTY.DataAnalysis.Entity;
using Newtonsoft.Json;
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
using System.Windows.Shapes;
using Ty.Component.ImageControl;
using Ty.Component.MediaControl;

namespace SureDream.Appliaction.Demo.MediaControl
{
    /// <summary>
    /// MulMediaPlayerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MulMediaPlayerWindow : Window
    {
        public MulMediaPlayerWindow()
        {
            InitializeComponent();
        }

        /// <summary> 获取标定信息存放路径 </summary>
        string GetMarkFileName(string imgName)
        {
            string file = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");

            string tempFiles = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Marks", imgName + "[" + file + "].mark");

            if (!File.Exists(tempFiles)) File.WriteAllText(tempFiles, string.Empty);

            return tempFiles;
        }

        private void Btn_loadImages_Click(object sender, RoutedEventArgs e)
        {
            //  Do：根据数量初始化控件
            int c = int.Parse(this.txt_count.Text);

            List<Tuple<List<string>, string>> imageFoders = new List<Tuple<List<string>, string>>();

            

            for (int i = 0; i < c; i++)
            {
                List<string> folders = new List<string>();

                string filePath1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");
                string filePath2 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images1");

                folders.Add(filePath1);
                folders.Add(filePath2);

                imageFoders.Add(new Tuple<List<string>, string>(folders, filePath2));

            }

            this.media.LoadImageFolders(imageFoders.ToArray());
            
        }

        private void Btn_loadShareImages_Click(object sender, RoutedEventArgs e)
        {
            //  Do：根据数量初始化控件
            int c = int.Parse(this.txt_count.Text);

            string filePath1 = @"\\192.168.1.22\Document\images1";
            string filePath2 = @"\\192.168.1.22\Document\images2";
            string filePath3 = @"\\192.168.1.22\Document\images3";
            string filePath4 = @"\\192.168.1.22\Document\images4";
            string filePath5 = @"\\192.168.1.22\Document\images5";

            List<string> folders = new List<string>();

            folders.Add(filePath1);
            folders.Add(filePath2);
            folders.Add(filePath3);
            folders.Add(filePath4);
            folders.Add(filePath5);

            List<Tuple<List<string>, string>> collection = new List<Tuple<List<string>, string>>();

            for (int i = 0; i < c; i++)
            {
                collection.Add(new Tuple<List<string>, string>(folders, filePath1));
            }

            this.media.LoadImageShareFolders("Healthy", "870210lhj", "192.168.1.22", collection.ToArray());
            
        }

        private void Btn_loadFTPImages_Click(object sender, RoutedEventArgs e)
        {
            //  Do：根据数量初始化控件
            int c = int.Parse(this.txt_count.Text);

            string filePath = @"ftp://127.0.0.1/images/";

            List<string> folders = new List<string>();

            folders.Add(@"ftp://127.0.0.1/images/");
            //folders.Add(@"ftp://127.0.0.1/images1/");
            //folders.Add(@"ftp://127.0.0.1/images2/");
            //folders.Add(@"ftp://127.0.0.1/images3/");
            //folders.Add(@"ftp://127.0.0.1/images4/");
            //folders.Add(@"ftp://127.0.0.1/images5/");
            //folders.Add(@"ftp://127.0.0.1/images6/");
            //folders.Add(@"ftp://127.0.0.1/images7/");
            //folders.Add(@"ftp://127.0.0.1/images8/"); 

            List<Tuple<List<string>, string>> collection = new List<Tuple<List<string>, string>>(); 

            for (int i = 0; i < c; i++)
            {
                collection.Add(new Tuple<List<string>, string>(folders, filePath));
            }

            this.media.LoadImageFtpFolders("Healthy", "870210lhj", collection.ToArray());
        }
    }
}
