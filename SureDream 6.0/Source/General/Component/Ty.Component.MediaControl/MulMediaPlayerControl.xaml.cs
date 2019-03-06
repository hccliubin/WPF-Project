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

namespace Ty.Component.MediaControl
{
    /// <summary>
    /// MulMediaPlayerControl.xaml 的交互逻辑
    /// </summary>
    public partial class MulMediaPlayerControl : UserControl
    {
        public MulMediaPlayerControl()
        {
            InitializeComponent();

        }


        public List<IVdeioImagePlayerService> MediaSources
        {
            get { return (List<IVdeioImagePlayerService>)GetValue(MediaSourcesProperty); }
            set { SetValue(MediaSourcesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MediaSourcesProperty =
            DependencyProperty.Register("MediaSources", typeof(List<IVdeioImagePlayerService>), typeof(MulMediaPlayerControl), new PropertyMetadata(default(List<IVdeioImagePlayerService>), (d, e) =>
             {
                 MulMediaPlayerControl control = d as MulMediaPlayerControl;

                 if (control == null) return;

                 List<IVdeioImagePlayerService> config = e.NewValue as List<IVdeioImagePlayerService>;

                 foreach (var item in config)
                 {
                     item.PlayerToolControl = control.playtool;
                 }

                 if (config.Count == 1)
                 {
                     control.RowCount = 1;
                     control.ColCount = 1;
                 }

                 if (config.Count == 2)
                 {
                     control.RowCount = 1;
                     control.ColCount = 2;
                 }

                 if (config.Count == 3 || config.Count == 4)
                 {
                     control.RowCount = 2;
                     control.ColCount = 2;
                 }

                 if (config.Count == 5 || config.Count == 6)
                 {
                     control.RowCount = 3;
                     control.ColCount = 2;
                 }

                 if (config.Count == 7 || config.Count == 8 || config.Count == 9)
                 {
                     control.RowCount = 3;
                     control.ColCount = 3;
                 }


             }));



        public int RowCount
        {
            get { return (int)GetValue(RowCountProperty); }
            set { SetValue(RowCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.Register("RowCount", typeof(int), typeof(MulMediaPlayerControl), new PropertyMetadata(1, (d, e) =>
             {
                 MulMediaPlayerControl control = d as MulMediaPlayerControl;

                 if (control == null) return;

                 //int config = e.NewValue as int;

             }));


        public int ColCount
        {
            get { return (int)GetValue(ColCountProperty); }
            set { SetValue(ColCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColCountProperty =
            DependencyProperty.Register("ColCount", typeof(int), typeof(MulMediaPlayerControl), new PropertyMetadata(1, (d, e) =>
             {
                 MulMediaPlayerControl control = d as MulMediaPlayerControl;

                 if (control == null) return;

                 //int config = e.NewValue as int;

             }));


    }
}
