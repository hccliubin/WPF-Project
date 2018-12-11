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

namespace Ty.Component.TaskAssignment
{
    /// <summary>
    /// TaskAssignmentControl.xaml 的交互逻辑
    /// </summary>
    public partial class TaskAssignmentControl : UserControl
    {
        
        public TaskAssignmentControl()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.OnSaveClick();
        }
       

        //声明和注册路由事件
        public static readonly RoutedEvent SaveClickRoutedEvent =
            EventManager.RegisterRoutedEvent("SaveClick", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(TaskAssignmentControl));
        //CLR事件包装
        public event RoutedEventHandler SaveClick
        {
            add { this.AddHandler(SaveClickRoutedEvent, value); }
            remove { this.RemoveHandler(SaveClickRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnSaveClick()
        {
            RoutedEventArgs args = new RoutedEventArgs(SaveClickRoutedEvent, this);
            this.RaiseEvent(args);
        }




        //public TaskAllocation TaskAllocation
        //{
        //    get { return (TaskAllocation)GetValue(TaskAllocationProperty); }
        //    set { SetValue(TaskAllocationProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty TaskAllocationProperty =
        //    DependencyProperty.Register("TaskAllocation", typeof(TaskAllocation), typeof(TaskAssignmentControl), new PropertyMetadata(default(TaskAllocation), (d, e) =>
        //     {
        //         TaskAssignmentControl control = d as TaskAssignmentControl;

        //         if (control == null) return;

        //         TaskAllocation config = e.NewValue as TaskAllocation;

        //         if (config == null) return;

        //         control.RefreshConfig();




        //     }));

        //public void RefreshConfig()
        //{
        //    this._vm.SiteCollection = this.TaskAllocation.Stations;
        //    this._vm.AnalystCollection = this.TaskAllocation.Analysts;
        //}
    }
}
