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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LTO.Base.Theme.Style
{
    /// <summary> 等待页面 </summary>
    public partial class WaittingSingleControl : BaseUserControl
    {

        public static WaittingSingleControl Instance;


        public static void Show(string message, Action action = null, int count = 5)
        {
            if (Instance == null) return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                Instance.Message = message;

                Instance.IsShow = true;
            });

            if (action != null)
            {
                Task task = Task.Run(action);

                task.ContinueWith(l =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Instance.IsShow = false;
                    });
                });
            }
        }

        public static void Hide()
        {
            if (Instance != null)
            {
                Instance.IsShow = false;
            }
        }

        /// <summary> 消息 </summary>
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(WaittingSingleControl), new PropertyMetadata(default(string), (d, e) =>
            {
                MessageSingleControl control = d as MessageSingleControl;

                if (control == null) return;

                string config = e.NewValue as string;

            }));

        public WaittingSingleControl()
        {
            InitializeComponent();
        }
    }
}
