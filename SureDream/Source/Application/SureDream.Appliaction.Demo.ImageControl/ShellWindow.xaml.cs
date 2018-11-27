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
using System.Windows.Shapes;
using Ty.Component.ImageControl;

namespace SureDream.Appliaction.Demo.ImageControl
{
    /// <summary>
    /// ShellWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShellWindow : Window
    {
        ImgOperate _imgOperate = new ImgOperate();

        bool _isload = false;

        public ShellWindow()
        {
            InitializeComponent();

            this.grid_center.Children.Add(_imgOperate.BuildEntity());
        }

        private void CommandBinding_Search_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

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

            this._isload = true;
        }

        private void CommandBinding_Search_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Previous_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _imgOperate.PreviousImg();

        }

        private void CommandBinding_Previous_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload;
        }

        private void CommandBinding_Next_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _imgOperate.NextImg();
        }

        private void CommandBinding_Next_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload;
        }

        private void CommandBinding_Next_CanExecut(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload;
        }

        private void CommandBinding_FullScreen_CanExecut(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload && !_isfullscreen;
        }

        bool _isfullscreen = false;
        private void CommandBinding_FullScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _imgOperate.SetFullScreen(true);
        }

        private void CommandBinding_UnFullScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _imgOperate.SetFullScreen(false);
        }

        private void CommandBinding_UnFullScreen_CanExecut(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload&& _isfullscreen;
        }

        private void CommandBinding_ShowLocates_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _imgOperate.ShowLocates();
        }

        private void CommandBinding_ShowDefects_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _imgOperate.ShowDefects();
        }

        private void CommandBinding_ShowMarks_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _imgOperate.ShowMarks();
        }

        private void CommandBinding_ImgPlaySpeedUp_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void CommandBinding_ImgPlaySpeedDown_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void CommandBinding_ShowLocates_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload;
        }

        private void CommandBinding_ShowDefects_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload;
        }

        private void CommandBinding_ShowMarks_CanExecut(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload;
        }

        private void CommandBinding_ImgPlaySpeedUp_CanExecut(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload;
        }

        private void CommandBinding_ImgPlaySpeedDown_CanExecut(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload;
        }
    }
}
