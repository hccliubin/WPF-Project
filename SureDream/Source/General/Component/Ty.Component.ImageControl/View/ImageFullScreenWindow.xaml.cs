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


        public UIElement CenterContent
        {
            set
            {
                this.grid_all.Children.Add(value);
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.grid_all.Children.Clear();

            this.Close();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

    }
}
