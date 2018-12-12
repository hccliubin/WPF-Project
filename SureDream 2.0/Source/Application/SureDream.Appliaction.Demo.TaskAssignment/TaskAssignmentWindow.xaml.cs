using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using Ty.Component.TaskAssignment;

namespace SureDream.Appliaction.Demo.TaskAssignment
{
    /// <summary>
    /// TaskAssignmentWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TaskAssignmentWindow : Window
    {
        public TaskAssignmentWindow()
        {
            InitializeComponent();
        }

        private void TaskAssignmentControl_SaveClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("保存中...");
            this.Close();
        }


        private void TaskAssignmentControl_SameStation(object sender, RoutedEventArgs e)
        {
            SameStationRoutedEventArgs args = e as SameStationRoutedEventArgs;

            Debug.WriteLine(args.Station.StationName);
        }
    }
}
