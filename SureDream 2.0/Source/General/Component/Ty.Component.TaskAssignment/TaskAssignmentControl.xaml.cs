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

            this.DataContextChanged += TaskAssignmentControl_DataContextChanged;

        }

        private void TaskAssignmentControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            RawTaskViewModel vm = e.NewValue as RawTaskViewModel;

            vm.SaveEvent += Vm_SaveEvent;

        }

        private void Vm_SaveEvent(RawTaskViewModel obj)
        {
            this.OnSaveClick();

            obj.SaveEvent -= Vm_SaveEvent;
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

        private void StackPanel_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if((bool)e.NewValue)
            {
                this.OnSameStation();
            }
        }


        //声明和注册路由事件
        public static readonly RoutedEvent SameStationRoutedEvent =
            EventManager.RegisterRoutedEvent("SameStation", RoutingStrategy.Bubble, typeof(EventHandler<SameStationRoutedEventArgs>), typeof(TaskAssignmentControl));
        //CLR事件包装
        public event RoutedEventHandler SameStation
        {
            add { this.AddHandler(SameStationRoutedEvent, value); }
            remove { this.RemoveHandler(SameStationRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnSameStation()
        {
            SameStationRoutedEventArgs args = new SameStationRoutedEventArgs(SameStationRoutedEvent, this);

            args.Station = this.cb_first.SelectedItem as Station;

            this.RaiseEvent(args);
        }

    }

    public class SameStationRoutedEventArgs: RoutedEventArgs
    {
        public Station Station { get; set; }

        public SameStationRoutedEventArgs(RoutedEvent routedEvent, object source):base(routedEvent, source)
        {

        }
    }
}
