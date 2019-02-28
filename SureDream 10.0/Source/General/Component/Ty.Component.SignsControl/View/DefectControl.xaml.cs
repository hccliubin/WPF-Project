using System;
using System.Collections.Generic;
using System.Diagnostics;
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

                     //Keyboard.Focus(control.list_hist);
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

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Debug.WriteLine("ListBox_SelectionChanged");

            DefectViewModel defectViewModel = this.DataContext as DefectViewModel;

            if (defectViewModel == null) return;

            List<TyeEncodeDeviceEntityNode> add = new List<TyeEncodeDeviceEntityNode>();

            foreach (var item in e.AddedItems)
            {
                add.Add(item as TyeEncodeDeviceEntityNode);
            }

            List<TyeEncodeDeviceEntityNode> remove = new List<TyeEncodeDeviceEntityNode>();

            foreach (var item in e.RemovedItems)
            {
                remove.Add(item as TyeEncodeDeviceEntityNode);
            }
            

            //defectViewModel.RefreshNodes(this.tb_text.Text, add.ToList(), remove);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.tb_text.Text = "";
        }

        private void Txt_filter_TextChanged(object sender, TextChangedEventArgs e)
        {

            DefectViewModel defectViewModel = this.DataContext as DefectViewModel;

            if (defectViewModel == null) return;


            Debug.WriteLine(this.tb_text.Text);

            if(string.IsNullOrEmpty(this.tb_text.Text))
            {
                this.txt_mark.Visibility = Visibility.Visible;
            }
            else
            {
                this.txt_mark.Visibility = Visibility.Collapsed;
            }


            defectViewModel.RefreshFilter(this.tb_text.Text);
        }
    }

    
}
