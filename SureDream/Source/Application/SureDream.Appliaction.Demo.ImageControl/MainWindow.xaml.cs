using Microsoft.Win32;
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

namespace SureDream.Appliaction.Demo.ImageControl
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

        LinkedList<string> _collection = new LinkedList<string>();


        LinkedListNode<string> current;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

            var result = open.ShowDialog();

            if (result.HasValue && result.Value)
            {
                var files = Directory.GetFiles(System.IO.Path.GetDirectoryName(open.FileName));

                foreach (var item in files)
                {
                    if (System.IO.Path.GetExtension(item).EndsWith("jpg") || System.IO.Path.GetExtension(item).EndsWith("png"))
                    {
                        _collection.AddLast(item);
                    }
                }

                current = _collection.First;

                current = _collection.Find(open.FileName);

                //viewModel.ImageSource = new BitmapImage(new Uri(current.Value, UriKind.Absolute));

                this.Refresh(current.Value);
            }
        }

        private void image_LastClicked(object sender, RoutedEventArgs e)
        {
            current = current.Previous;

            if (current == null)
            {
                current = _collection.Last;
            }

            //viewModel.ImageSource = new BitmapImage(new Uri(current.Value, UriKind.Absolute));

            this.Refresh(current.Value);
        }

        private void image_NextClick(object sender, RoutedEventArgs e)
        {
            current = current.Next;

            if (current == null)
            {
                current = _collection.First;
            }

            //viewModel.ImageSource = new BitmapImage(new Uri(current.Value, UriKind.Absolute));

            this.Refresh(current.Value);
        }

        void Refresh(string path)
        {
            ImageControlViewModel viewModel = new ImageControlViewModel();

            viewModel.ImageSource = new BitmapImage(new Uri(path, UriKind.Absolute));

            this.image.ViewModel = viewModel;

            this.listbox_samples.DataContext = viewModel;
        }

        private void image_Save(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("正在保存..");
        }
    }
}
