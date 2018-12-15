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

namespace Ty.Component.TaskAssignment
{
    /// <summary>
    /// 提示弹窗
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow()
        {
            InitializeComponent();
        }

        /// <summary> 取消 </summary>
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        /// <summary> 提交保存 </summary>
        private void btn_sumit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        /// <summary> 显示消息框 </summary>
        public static bool ShowDialog(string message)
        {
            MessageWindow window = new MessageWindow();

            window.tb_message.Text = message;

            var r = window.ShowDialog();

            return r.HasValue && r.Value;
        }

        /// <summary> 拖拽 </summary>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
