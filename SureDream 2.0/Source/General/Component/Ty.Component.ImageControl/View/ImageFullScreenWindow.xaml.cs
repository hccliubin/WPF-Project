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
    /// 全屏显示窗口
    /// </summary>
    public partial class ImageFullScreenWindow : Window
    {
        public ImageFullScreenWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 全屏中放的控件
        /// </summary>
        public UIElement CenterContent
        {
            set
            {
                this.grid_all.Children.Add(value);
            }
        }

        /// <summary>
        /// 退出关闭命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.grid_all.Children.Clear();

            this.Close();
        }

        /// <summary>
        /// 是否可以退出关闭验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

    }
}
