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
    /// <summary> 模块按钮 </summary>
    public partial class ModuleButtonControl : Button
    {
        static ModuleButtonControl()
        {

            DefaultStyleKeyProperty.OverrideMetadata(typeof(ModuleButtonControl), new FrameworkPropertyMetadata(typeof(ModuleButtonControl)));

        }

        public string BindModuleName
        {
            get { return (string)GetValue(BindModuleNameProperty); }
            set { SetValue(BindModuleNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindModuleNameProperty =
            DependencyProperty.Register("BindModuleName", typeof(string), typeof(ModuleButtonControl), new PropertyMetadata(default(string), (d, e) =>
             {
                 ModuleButtonControl control = d as ModuleButtonControl;

                 if (control == null) return;

                 string config = e.NewValue as string;


                 //control.btn_center.Tag = config;

             }));


        public string ModuleName
        {
            get { return (string)GetValue(ModuleNameProperty); }
            set { SetValue(ModuleNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModuleNameProperty =
            DependencyProperty.Register("ModuleName", typeof(string), typeof(ModuleButtonControl), new PropertyMetadata(default(string), (d, e) =>
             {
                 ModuleButtonControl control = d as ModuleButtonControl;

                 if (control == null) return;

                 string config = e.NewValue as string;

                 //control.tb_name.Text = config;

             }));


        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ModuleButtonControl), new PropertyMetadata(default(ImageSource), (d, e) =>
             {
                 ModuleButtonControl control = d as ModuleButtonControl;

                 if (control == null) return;

                 ImageSource config = e.NewValue as ImageSource;

                 control.ImageSource = config;

             }));


        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(ModuleButtonControl), new PropertyMetadata(default(bool), (d, e) =>
             {
                 ModuleButtonControl control = d as ModuleButtonControl;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));



    }
}
