using SureDream.Base.WpfBase;
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

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            btn.MouseLeftButtonDown += btn_MouseLeftButtonDown;
            btn.MouseMove += btn_MouseMove;
            btn.MouseLeftButtonUp += btn_MouseLeftButtonUp;

            ////  Do：注册快捷键
            //KeyBinding keybinding = new KeyBinding();
            InputGesture inputgesture = new KeyGesture(Key.O, ModifierKeys.Control);
            ////keybinding.Gesture = inputgesture;
            //keybinding.Key = Key.H;

            RelayCommand cmd = new RelayCommand(l =>
            {
                MessageBox.Show("RelayCommand");
            });

            InputBinding input = new InputBinding(cmd, inputgesture);
            this.InputBindings.Add(input);
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("tetet");
        }

        private void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {

        }

        //实例化一个RoutedUICommand对象来进行升序排列，指定其Text属性，并设置快捷键
        public static RoutedUICommand SortCommand  = new RoutedUICommand("Sort", "Sort", typeof(MainWindow)
                , new InputGestureCollection(new KeyGesture[] { new KeyGesture(key: Key.F3, modifiers: ModifierKeys.None) }));

        Point pos = new Point();
        void btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Button tmp = (Button)sender;
            pos = e.GetPosition(null);
            tmp.CaptureMouse();
            tmp.Cursor = Cursors.Hand;
        }

        void btn_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Button tmp = (Button)sender;
                double dx = e.GetPosition(null).X - pos.X + tmp.Margin.Left;
                double dy = e.GetPosition(null).Y - pos.Y + tmp.Margin.Top;
                tmp.Margin = new Thickness(dx, dy, 0, 0);
                pos = e.GetPosition(null);
            }
        }

        void btn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Button tmp = (Button)sender;
            tmp.ReleaseMouseCapture();
        }

    }

    //将所有命令封装在一个类里面
    public class MyCommands
    {
        public static RoutedUICommand MyCommand = new RoutedUICommand();

        public static void DoCommand()
        {
            MyCommand.Execute("erer", null);
        }

    }

}
