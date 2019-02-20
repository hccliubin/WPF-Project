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
    /// BubbleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BubbleWindow : Window
    {
        public BubbleWindow()
        {
            InitializeComponent();
        }

        public Visual Visual
        {
            get { return (Visual)GetValue(VisualProperty); }
            set { SetValue(VisualProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisualProperty =
            DependencyProperty.Register("Visual", typeof(Visual), typeof(BubbleWindow), new PropertyMetadata(default(Visual), (d, e) =>
            {
                BubbleWindow control = d as BubbleWindow;

                if (control == null) return;

                Visual config = e.NewValue as Visual;

                //control.visualbrush_part.Visual = config;

            }));

    }

}


