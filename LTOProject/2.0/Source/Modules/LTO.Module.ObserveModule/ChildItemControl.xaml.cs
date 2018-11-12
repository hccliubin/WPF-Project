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

namespace LTO.Module.ObserveModule
{
    /// <summary>
    /// ChildItemControl.xaml 的交互逻辑
    /// </summary>
    public partial class ChildItemControl : UserControl
    {
        public ChildItemControl()
        {
            InitializeComponent();
        }


        public ChildObserveModel ChildModel
        {
            get { return (ChildObserveModel)GetValue(ChildModelProperty); }
            set { SetValue(ChildModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChildModelProperty =
            DependencyProperty.Register("ChildModel", typeof(ChildObserveModel), typeof(ChildItemControl), new PropertyMetadata(default(ChildObserveModel), (d, e) =>
             {
                 ChildItemControl control = d as ChildItemControl;

                 if (control == null) return;

                 ChildObserveModel config = e.NewValue as ChildObserveModel;

                 control.Visibility = config == null ? Visibility.Collapsed : Visibility.Visible;

             }));

    }
}
