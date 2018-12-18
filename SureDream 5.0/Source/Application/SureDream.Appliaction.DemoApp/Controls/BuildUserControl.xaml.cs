using Microsoft.Win32;
using Ty.Component.MenuBar;
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

namespace SureDream.Appliaction.DemoApp
{
    /// <summary>
    /// 添加按钮组件
    /// </summary>
    public partial class BuildUserControl : UserControl, ICommandSource
    {
        public BuildUserControl()
        {
            InitializeComponent();
        }


        public MenuButton MenuButton
        {
            get { return (MenuButton)GetValue(MenuButtonProperty); }
            set { SetValue(MenuButtonProperty, value); }
        }


        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(BuildUserControl), new PropertyMetadata(default(ICommand)));


        public object CommandParameter { get; set; }

        public IInputElement CommandTarget { get; set; }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuButtonProperty =
            DependencyProperty.Register("MenuButton", typeof(MenuButton), typeof(BuildUserControl), new PropertyMetadata(default(MenuButton), (d, e) =>
             {
                 BuildUserControl control = d as BuildUserControl;

                 if (control == null) return;

                 //MenuButton config = e.NewValue as MenuButton;

             }));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MenuButton btn = new MenuButton();

            btn.IconFont = ((TextBlock)this.icon.SelectedValue).Text.ToString();
            btn.LeftRightAlignment = (LeftRightAlignment)this.leftright.SelectedItem;
            btn.MenuButtonStyle = (MenuButtonStyle)this.type.SelectedValue;
            btn.Content = this.name.Text;
            btn.IsEnabled = this.cb_isenbled.IsChecked ?? this.cb_isenbled.IsChecked.Value;

            btn.MenuKey = new MenuKey((Key)this.cb_key.SelectedValue, (ModifierKeys)this.cb_ModifierKeys.SelectedValue);
            btn.Orientation = (Orientation)this.cb_Orientation.SelectedValue;

            btn.ImageSource = ((Image)this.image.SelectedValue).Source;

            this.MenuButton = btn;

            if (this.Command != null)
            {
                this.Command.Execute(this.CommandParameter);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            var result = open.ShowDialog();

            if (result.HasValue && result.Value)
            {
                Image image = new Image();

                image.Source = new BitmapImage(new Uri(open.FileName, UriKind.Absolute));

                this.image.Items.Add(image);
            }
        }
    }
}
