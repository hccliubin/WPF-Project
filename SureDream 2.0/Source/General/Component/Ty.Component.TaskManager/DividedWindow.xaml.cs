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

namespace Ty.Component.TaskManager
{
    /// <summary>
    /// DividedWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DividedWindow : Window
    {
        public DividedWindow()
        {
            InitializeComponent();


        }

        public RawTaskViewModel ViewModel { get { return this.DataContext as RawTaskViewModel; } }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string err;
            if (!ViewModel.IsVaild(out err))
            {
                return;
            }
            this.Closing -= Window_Closing;
            this.DialogResult = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ViewModel.IsEdit())
            {
                MessageBox.Show("当前页面是否存在未保存的编辑，请点击确定提交保存项！");
                e.Cancel = true;
            }

        }
    }
}
