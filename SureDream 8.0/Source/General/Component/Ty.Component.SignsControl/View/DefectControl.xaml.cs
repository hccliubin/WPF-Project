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
using Ty.Base.WpfBase;

namespace Ty.Component.SignsControl
{
    /// <summary>
    /// 缺陷管理控件
    /// </summary>
    public partial class DefectControl : UserControl
    {
        public DefectControl()
        {
            InitializeComponent();
        }


        public KeyGesture KeyGestureForHistList
        {
            get { return (KeyGesture)GetValue(KeyGestureForHistListProperty); }
            set { SetValue(KeyGestureForHistListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyGestureForHistListProperty =
            DependencyProperty.Register("KeyGestureForHistList", typeof(KeyGesture), typeof(DefectControl), new PropertyMetadata(default(KeyGesture), (d, e) =>
             {
                 DefectControl control = d as DefectControl;
                 if (control == null) return;

                 KeyGesture config = e.NewValue as KeyGesture;

                 //KeyGesture old = e.OldValue as KeyGesture;

                 RelayCommand cmd = new RelayCommand(l =>
                 {
                     //control.list_hist.Focus();

                     //control.list_hist.();

                     Keyboard.Focus(control.list_hist);
                 });

                 InputBinding input = new InputBinding(cmd, config);

                 //  Message：注册到本控件内
                 control.InputBindings.Clear();

                 control.InputBindings.Add(input);

                 ////  Do：注册到窗口级别
                 //var collection = ControlsSearchHelper.GetParentObject<Window>(control, string.Empty).InputBindings;

                 ////  Message：添加新的
                 //collection.Add(input);


             }));

      
    }

    
}
