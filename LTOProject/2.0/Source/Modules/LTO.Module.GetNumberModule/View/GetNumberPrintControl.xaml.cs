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

namespace LTO.Module.GetNumberModule
{
    /// <summary>
    /// GetNumberPrintControl.xaml 的交互逻辑
    /// </summary>
    public partial class GetNumberPrintControl : UserControl
    {
        public GetNumberPrintControl()
        {
            InitializeComponent();
        }

       void Print()
        {
            GetNumberModuleDomain.Instance.PrintGrid(this.grid_all);
        }


        public bool IsPrint
        {
            get { return (bool)GetValue(IsPrintProperty); }
            set { SetValue(IsPrintProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPrintProperty =
            DependencyProperty.Register("IsPrint", typeof(bool), typeof(GetNumberPrintControl), new PropertyMetadata(default(bool), (d, e) =>
             {
                 GetNumberPrintControl control = d as GetNumberPrintControl;

                 if (control == null) return;

                 bool config = (bool)e.NewValue;

                 if(config)
                 {
                     control.Print();

                     control.IsPrint = false;
                 }

             }));


    }
}
