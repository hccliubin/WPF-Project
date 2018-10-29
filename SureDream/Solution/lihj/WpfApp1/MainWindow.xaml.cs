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
