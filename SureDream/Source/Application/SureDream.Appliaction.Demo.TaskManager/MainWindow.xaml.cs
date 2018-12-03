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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ty.Base.WpfBase.Service;
using Ty.Component.TaskManager;

namespace SureDream.Appliaction.Demo.TaskManager
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
        private ObservableCollection<TaskManagement> _collection = new ObservableCollection<TaskManagement>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TaskManagement> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }


        private TaskManagement _current;
        /// <summary> 说明  </summary>
        public TaskManagement Current
        {
            get { return _current; }
            set
            {
                _current = value;
                RaisePropertyChanged("Current");
            }
        }


        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "init")
            {

                //  Message：加载初始化信息
                List<Analyst> Analysts = new List<Analyst>();

                for (int i = 0; i < 10; i++)
                {
                    Analyst a = new Analyst();
                    a.ID = i.ToString();
                    a.Name = "Analyst" + i.ToString();
                    Analysts.Add(a);
                }

                List<Site> Sites = new List<Site>();

                for (int i = 0; i < 10; i++)
                {
                    Site a = new Site();
                    a.ID = i.ToString();
                    a.Name = "Site" + i.ToString();

                    for (int j = 0; j < 20; j++)
                    {
                        Pole p = new Pole();
                        p.ID = j.ToString();
                        p.Name = "Pole_" + i.ToString() + "_" + j.ToString();
                        a.Poles.Add(p);
                    }
                    Sites.Add(a);
                }

                for (int i = 1; i < 10; i++)
                {
                    TaskManagement r = new TaskManagement();
                    r.RawTaskID = i.ToString().PadLeft(5, '0');
                    r.RawTaskName = DateTime.Now.AddDays(-1 * i).ToString("yyyyMMddHHmmss") + "_贵广线_上行_佛山站_肇庆站";
                    r.MachineType = "1C";
                    r.SiteRange = ((i - 1) * 20).ToString() + "-" + (i * 20).ToString();
                    r.RealDate = DateTime.Now.AddDays(-1 * i);
                    r.Progress = i.ToString();
                    r.LoadAnalyst(Analysts);
                    r.LoadSites(Sites);

                    this.Collection.Add(r);

                }
            }
            //  Do：取消
            else if (command == "btn_Division")
            {
                DivisionWindow window = new DivisionWindow();
                window.DataContext = this.Current;
                window.ShowDialog();
                
            }

            //  Do：取消
            else if (command == "btn_Show")
            {
                ShowWindow window = new ShowWindow();
                window.DataContext = this.Current;
                window.ShowDialog();
            }
        }

    }

    //class RawTaskViewModel : NotifyPropertyChanged
    //{

    //    private string _rawTaskID;
    //    /// <summary> 说明  </summary>
    //    public string RawTaskID
    //    {
    //        get { return _rawTaskID; }
    //        set
    //        {
    //            _rawTaskID = value;
    //            RaisePropertyChanged("RawTaskID");
    //        }
    //    }

    //    private string _rawTaskName;
    //    /// <summary> 说明  </summary>
    //    public string RawTaskName
    //    {
    //        get { return _rawTaskName; }
    //        set
    //        {
    //            _rawTaskName = value;
    //            RaisePropertyChanged("RawTaskName");
    //        }
    //    }

    //    private string _machineType;
    //    /// <summary> 说明  </summary>
    //    public string MachineType
    //    {
    //        get { return _machineType; }
    //        set
    //        {
    //            _machineType = value;
    //            RaisePropertyChanged("MachineType");
    //        }
    //    }

    //    private DateTime _realDate;
    //    /// <summary> 说明  </summary>
    //    public DateTime RealDate
    //    {
    //        get { return _realDate; }
    //        set
    //        {
    //            _realDate = value;
    //            RaisePropertyChanged("RealDate");
    //        }
    //    }

    //    private string _siteRange;
    //    /// <summary> 说明  </summary>
    //    public string SiteRange
    //    {
    //        get { return _siteRange; }
    //        set
    //        {
    //            _siteRange = value;
    //            RaisePropertyChanged("SiteRange");
    //        }
    //    }

    //    private string _progress;
    //    /// <summary> 说明  </summary>
    //    public string Progress
    //    {
    //        get { return _progress; }
    //        set
    //        {
    //            _progress = value;
    //            RaisePropertyChanged("Progress");
    //        }
    //    }


    //    protected override void RelayMethod(object obj)
    //    {
    //        string command = obj.ToString();

    //        Debug.WriteLine(command);

    //        //  Do：应用
    //        if (command == "Sumit")
    //        {


    //        }
    //        //  Do：取消
    //        else if (command == "Cancel")
    //        {


    //        }
    //    }

    //}
}
