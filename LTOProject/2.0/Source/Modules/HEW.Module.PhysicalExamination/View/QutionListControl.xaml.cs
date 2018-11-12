using HEW.UserControls.Reports;
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
    /// QutionListControl.xaml 的交互逻辑
    /// </summary>
    public partial class QutionListControl : BaseModuleControl
    {
        public QutionListControl()
        {
            InitializeComponent();
        }

        //声明和注册路由事件
        public static readonly RoutedEvent GoBackRoutedEvent =
            EventManager.RegisterRoutedEvent("GoBack", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(QutionListControl));
        //CLR事件包装
        public event RoutedEventHandler GoBack
        {
            add { this.AddHandler(GoBackRoutedEvent, value); }
            remove { this.RemoveHandler(GoBackRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnGoBack()
        {
            RoutedEventArgs args = new RoutedEventArgs(GoBackRoutedEvent, this);
            this.RaiseEvent(args);
        }


        private void ButtonControl_Click(object sender, RoutedEventArgs e)
        {
            this.OnGoBack();
        }




        public int PageSelect
        {
            get { return (int)GetValue(PageSelectProperty); }
            set { SetValue(PageSelectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PageSelectProperty =
            DependencyProperty.Register("PageSelect", typeof(int), typeof(QutionListControl), new PropertyMetadata(default(int), (d, e) =>
             {
                 QutionListControl control = d as QutionListControl;

                 if (control == null) return;

                 int config = (int)e.NewValue;

                 control.tpage_control.SetPage(config);

                 control.PageSelect = -9;

             }));



    }
}
