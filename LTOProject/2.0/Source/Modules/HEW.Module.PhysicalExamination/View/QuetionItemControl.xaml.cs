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

namespace HEW.Module.PhysicalExamination
{
    /// <summary>
    /// QuetionItemControl.xaml 的交互逻辑
    /// </summary>
    public partial class QuetionItemControl : UserControl
    {
        public QuetionItemControl()
        {
            InitializeComponent();
        }


        public QuetionViewModelcs Item1
        {
            get { return (QuetionViewModelcs)GetValue(Item1Property); }
            set { SetValue(Item1Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Item1Property =
            DependencyProperty.Register("Item1", typeof(QuetionViewModelcs), typeof(QuetionItemControl), new PropertyMetadata(default(QuetionViewModelcs), (d, e) =>
             {
                 QuetionItemControl control = d as QuetionItemControl;

                 if (control == null) return;

                 QuetionViewModelcs config = e.NewValue as QuetionViewModelcs;

             }));



        public QuetionViewModelcs Item2
        {
            get { return (QuetionViewModelcs)GetValue(Item2Property); }
            set { SetValue(Item2Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Item2Property =
            DependencyProperty.Register("Item2", typeof(QuetionViewModelcs), typeof(QuetionItemControl), new PropertyMetadata(default(QuetionViewModelcs), (d, e) =>
             {
                 QuetionItemControl control = d as QuetionItemControl;

                 if (control == null) return;

                 QuetionViewModelcs config = e.NewValue as QuetionViewModelcs;

             }));

    }
}
