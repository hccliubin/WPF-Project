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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ty.Base.WpfBase.Service;
using Ty.Component.TaskAssignment;

namespace SureDream.Appliaction.Demo.TaskAssignment
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        MainViewModel _vm = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = _vm;
        }

    }

    class MainViewModel : NotifyPropertyChanged
    {

        private RowIdEntity _current;
        /// <summary> 说明  </summary>
        public RowIdEntity Current
        {
            get { return _current; }
            set
            {
                _current = value;
                RaisePropertyChanged("Current");
            }
        }


        private ObservableCollection<RowIdEntity> _rawIdCollection = new ObservableCollection<RowIdEntity>();
        /// <summary> 说明  </summary>
        public ObservableCollection<RowIdEntity> RawIdCollection
        {
            get { return _rawIdCollection; }
            set
            {
                _rawIdCollection = value;
                RaisePropertyChanged("RawIdCollection");
            }
        }

        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "init")
            {
                for (int i = 0; i < 4; i++)
                {
                    RowIdEntity vm = new RowIdEntity();
                    vm.ID = "贵广线_上行_佛山站_肇庆站_" + i;
                    this.RawIdCollection.Add(vm);
                }

            }
            //  Do：取消
            else if (command == "btn_divied")
            {
                TaskAssignmentWindow window = new TaskAssignmentWindow();
                window.DataContext = this.Current.Model;
                window.ShowDialog();

            }
            //  Do：取消
            else if (command == "btn_showTask")
            {
                TaskLookUpWindow window = new TaskLookUpWindow();
                window.DataContext = this.Current.Model;
                window.ShowDialog();

            }
            //  Do：取消
            else if (command == "btn_add")
            {
                RowIdEntity entity = new RowIdEntity();

                TaskAllocation task = new TaskAllocation();

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

                entity.Model.RefreshConfig(task);

                entity.ID = Guid.NewGuid().ToString();

                this.RawIdCollection.Add(entity);

            }
            
        }

    }

    class RowIdEntity
    {
        public string ID { get; set; }

        public RawTaskViewModel Model { get; set; } = new RawTaskViewModel();
    }
}
