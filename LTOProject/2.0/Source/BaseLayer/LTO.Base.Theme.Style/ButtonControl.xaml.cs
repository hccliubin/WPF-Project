


using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace LTO.Base.Theme.Style
{
    /// <summary> 左右侧按钮 </summary>
    public partial class ButtonControl : System.Windows.Controls.Button
    {
        static ButtonControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ButtonControl), new FrameworkPropertyMetadata(typeof(ButtonControl)));

        }

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ButtonControl), new PropertyMetadata(default(ImageSource), (d, e) =>
             {
                 ButtonControl control = d as ButtonControl;

                 if (control == null) return;

                 ImageSource config = e.NewValue as ImageSource;

             }));

    }
}
