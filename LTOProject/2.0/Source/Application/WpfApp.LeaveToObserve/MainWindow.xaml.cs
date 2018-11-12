using LTO.Base.Frame.MVVM;
using LTO.Base.Theme.Style;
using LTO.Domain.DataService;
using LTO.General.ModuleManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace WpfApp.LeaveToObserve
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        MainWindowNotifyClass _vm = new MainWindowNotifyClass();
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = _vm;

            MessageSingleControl.Instance = this.control_message;
            WaittingSingleControl.Instance = this.control_waitting;

#if DEBUG

            if(ApplicationDomain.Instance.IsWin7())
            {
                this.Width = 768;
                this.Height = 1024;
            }
            else
            {
                this.Width = 1280;
                this.Height = 1024;
            }
          
           
            this.WindowState = WindowState.Normal;
#endif

            Action<MessageResult> result = l => 
            {
                if (l == MessageResult.TimeOver)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        //this.control_module.BeginLogin = false;
                        this.control_config.IsShow = false;
                    });
                }
            };

            ServiceManager.DataService.OnCheckCount = () =>
            {

                //if (this.control_module.IsShow || this.control_config.IsShow)
                if (this.control_config.IsShow)
                {
                    MessageSingleControl.ShowWithCancel("操作超时,将自动退出!", 5, result);
                }

            };

            ServiceManager.DataService.StartMonitor();
        }
    }

    partial class MainWindowNotifyClass
    {
        private bool _isShowModule = false;
        /// <summary> 显示模块页面  </summary>
        public bool IsShowModule
        {
            get { return _isShowModule; }
            set
            {
                _isShowModule = value;
                RaisePropertyChanged("IsShowModule");
            }
        }

        private string _title;
        /// <summary> 标题抬头  </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged("Title");
            }
        }


        private string _tel;
        /// <summary> 电话  </summary>
        public string Tel
        {
            get { return _tel; }
            set
            {
                _tel = value;
                RaisePropertyChanged("Tel");
            }
        }


        private string _date;
        /// <summary> 日期  </summary>
        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged("Date");
            }
        }


        private ILTOModule _currentModule;
        /// <summary> 说明  </summary>
        public ILTOModule CurrentModule
        {
            get { return _currentModule; }
            set
            {
                _currentModule = value;
                RaisePropertyChanged("CurrentModule");
            }
        }


        private List<ILTOModule> _modules;
        /// <summary> 说明  </summary>
        public List<ILTOModule> Modules
        {
            get { return _modules; }
            set
            {
                _modules = value;
                RaisePropertyChanged("Modules");
            }
        }



        public void RelayMethod(object obj)
        {
            string command = obj.ToString();


            Debug.WriteLine(command);

            //  Do：刷卡
            if (command == "SwingCardBegionRead")
            {


            }

            else if (command == "Init")
            {
                this.Tel = ApplicationDomain.Instance.GetUIConfig().Item2;

                this.Title = ApplicationDomain.Instance.GetUIConfig().Item1;

                //星期二 2018 - 08 - 01 15:13
                System.Timers.Timer time = new System.Timers.Timer();

                Action action = () =>
                {
                    this.Date = string.Format(("星期{0} {1}"), "日一二三四五六".Substring((int)DateTime.Now.DayOfWeek, 1), DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

                };

                action();

                time.Elapsed += (l, k) =>
                {
                    action();
                };

                time.Interval = 1000 * 30;

                time.Start();


                this.Modules = ApplicationDomain.Instance.GetModules();

                //  Do：根据配置文件加载默认窗口
                this.CurrentModule = ApplicationDomain.Instance.GetConfigDefaultModule(this.Modules);

                if (this.CurrentModule != null)
                {
                    this.IsShowModule = true;


                }
            }
        }
    }

    partial class MainWindowNotifyClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public MainWindowNotifyClass()
        {
            RelayCommand = new RelayCommand(RelayMethod);

            RelayMethod("Init");

        }
        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
