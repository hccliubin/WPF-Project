using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// ShellWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShellWindow : Window
    {
        public ShellWindow()
        {
            InitializeComponent();
        }
        //  Message：分工
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //  Message：初始化数据
            TaskAllocation task = new TaskAllocation();

            task.PacketId = "000000000000000";

            ObservableCollection<Rod> _poles = new ObservableCollection<Rod>();

            for (int i = 1; i < 10; i++)
            {
                _poles.Add(new Rod() { ID = i, RodName = i.ToString() });
            }

            task.Stations = new ObservableCollection<Station>();
            task.Stations.Add(new Station() { ID = 1001, StationName = "北京站", Rods = _poles });
            task.Stations.Add(new Station() { ID = 1002, StationName = "上海站", Rods = _poles });
            task.Stations.Add(new Station() { ID = 1003, StationName = "天津站", Rods = _poles });
            task.Stations.Add(new Station() { ID = 1004, StationName = "佛山站", Rods = _poles });
            task.Stations.Add(new Station() { ID = 1005, StationName = "广州站", Rods = _poles });
            task.Stations.Add(new Station() { ID = 1006, StationName = "肇庆站", Rods = _poles });

            task.Analysts = new ObservableCollection<Analyst>();
            task.Analysts.Add(new Analyst() { ID = 2001, AnalystName = "刘德华" });
            task.Analysts.Add(new Analyst() { ID = 2002, AnalystName = "张国荣" });
            task.Analysts.Add(new Analyst() { ID = 2003, AnalystName = "贝克汉姆" });
            task.Analysts.Add(new Analyst() { ID = 2004, AnalystName = "齐达内" });
            task.Analysts.Add(new Analyst() { ID = 2005, AnalystName = "劳尔" });
            task.Analysts.Add(new Analyst() { ID = 2006, AnalystName = "马拉多纳" });
            task.Analysts.Add(new Analyst() { ID = 2007, AnalystName = "郝海东" });
      
            TaskAssignmentWindow window = new TaskAssignmentWindow();
            window.DataContext = TaskService.Instance.CreateTaskViewModel(task);
            window.ShowDialog();
        }

        //  Message：查看
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TaskLookUpWindow window = new TaskLookUpWindow();
            window.DataContext = TaskService.Instance.GetTaskViewModel("000000000000000");
            window.ShowDialog();
        }
    }
}
