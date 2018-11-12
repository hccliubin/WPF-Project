


using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace LTO.Base.Theme.Style
{
    /// <summary> 单选按钮 </summary>
    public partial class RadioControl : System.Windows.Controls.RadioButton
    {
        static RadioControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadioControl), new FrameworkPropertyMetadata(typeof(RadioControl)));

        }



        public Thickness TextMargin
        {
            get { return (Thickness)GetValue(TextMarginProperty); }
            set { SetValue(TextMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextMarginProperty =
            DependencyProperty.Register("TextMargin", typeof(Thickness), typeof(RadioControl), new PropertyMetadata(default(Thickness), (d, e) =>
             {
                 RadioControl control = d as RadioControl;

                 if (control == null) return;

                 //Thickness config = e.NewValue as Thickness;

             }));

    }
}
