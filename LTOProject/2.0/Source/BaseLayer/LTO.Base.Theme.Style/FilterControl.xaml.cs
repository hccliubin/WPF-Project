


using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;


namespace LTO.Base.Theme.Style
{
    /// <summary> 筛选控件 </summary>
    public partial class FilterControl : ComboBox
    {
        static FilterControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FilterControl), new FrameworkPropertyMetadata(typeof(FilterControl)));

        }

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(FilterControl), new PropertyMetadata(default(ImageSource), (d, e) =>
             {
                 FilterControl control = d as FilterControl;

                 if (control == null) return;

                 ImageSource config = e.NewValue as ImageSource;

             }));

    }

    
}
