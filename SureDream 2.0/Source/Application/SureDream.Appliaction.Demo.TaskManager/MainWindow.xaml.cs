using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

            this.control_rawTaskControl.DataContext = _vm.TaskManagement;
        }
    }

    class MainViewModel : NotifyPropertyChanged
    {

        private ITaskManagement _taskManagement = new TaskManagement();
        /// <summary> 说明  </summary>
        public ITaskManagement TaskManagement
        {
            get { return _taskManagement; }
            set
            {
                _taskManagement = value;
                RaisePropertyChanged("TaskManagement");
            }
        }


        private ObservableCollection<string> _rawIdCollection = new ObservableCollection<string>();
        /// <summary> 说明  </summary>
        public ObservableCollection<string> RawIdCollection
        {
            get { return _rawIdCollection; }
            set
            {
                _rawIdCollection = value;
                RaisePropertyChanged("RawIdCollection");
            }
        }


        private string _selectRawId;
        /// <summary> 说明  </summary>
        public string SelectRawId
        {
            get { return _selectRawId; }
            set
            {
                _selectRawId = value;
                RaisePropertyChanged("SelectRawId");
            }
        }


        private ObservableCollection<Analyst> _analystcollection = new ObservableCollection<Analyst>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Analyst> AnalystCollection
        {
            get { return _analystcollection; }
            set
            {
                _analystcollection = value;
                RaisePropertyChanged("AnalystCollection");
            }
        }

        private ObservableCollection<Site> _siteCollection = new ObservableCollection<Site>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Site> SiteCollection
        {
            get { return _siteCollection; }
            set
            {
                _siteCollection = value;
                RaisePropertyChanged("SiteCollection");
            }
        }



        private ObservableCollection<Task> _taskCollection = new ObservableCollection<Task>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Task> TaskCollection
        {
            get { return _taskCollection; }
            set
            {
                _taskCollection = value;
                RaisePropertyChanged("TaskCollection");
            }
        }



        private Task _selectTask;
        /// <summary> 说明  </summary>
        public Task SelectTask
        {
            get { return _selectTask; }
            set
            {
                _selectTask = value;
                RaisePropertyChanged("SelectTask");
            }
        }


        private Analyst _selectAnalyst;
        /// <summary> 说明  </summary>
        public Analyst SelectAnalyst
        {
            get { return _selectAnalyst; }
            set
            {
                _selectAnalyst = value;
                RaisePropertyChanged("SelectAnalyst");
            }
        }


        private DateTime _startTime = DateTime.Now.AddDays(-7);
        /// <summary> 说明  </summary>
        public DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                RaisePropertyChanged("StartTime");
            }
        }


        private DateTime _endTime = DateTime.Now.AddDays(1);
        /// <summary> 说明  </summary>
        public DateTime EndTime
        {
            get { return _endTime; }
            set
            {
                _endTime = value;
                RaisePropertyChanged("EndTime");
            }
        }

        private ObservableCollection<Task> _findTaskCollection = new ObservableCollection<Task>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Task> FindTaskCollection
        {
            get { return _findTaskCollection; }
            set
            {
                _findTaskCollection = value;
                RaisePropertyChanged("FindTaskCollection");
            }
        }



        private string _message;
        /// <summary> 说明  </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }


        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "init")
            {

                this.TaskManagement.TaskDeleteEvent += l =>
                  {
                      this.TaskCollection.Remove(this.TaskCollection.Where(k => k.TaskID == l).First());

                      Message += $"删除任务事件：ID - {l}" + Environment.NewLine;

                  };
                this.TaskManagement.TaskListCommitEvent += l =>
                  {
                      foreach (var item in l)
                      {
                          this.TaskCollection.Add(item);

                          Message += $"提交任务列表事件：ID - {item.TaskID}  Name - {item.TaskName}" + Environment.NewLine;
                      }
                  };

                for (int i = 0; i < 4; i++)
                {
                    this.RawIdCollection.Add("贵广线_上行_佛山站_肇庆站_" + i);
                }

                List<Pole> _poles = new List<Pole>();
                for (int i = 1; i < 10; i++)
                {
                    _poles.Add(new Pole() { ID = i.ToString(), Name = i.ToString() });
                }

                this.SiteCollection.Add(new Site() { ID = "1001", Name = "北京站", Poles = _poles });
                this.SiteCollection.Add(new Site() { ID = "1002", Name = "上海站", Poles = _poles });
                this.SiteCollection.Add(new Site() { ID = "1003", Name = "天津站", Poles = _poles });
                this.SiteCollection.Add(new Site() { ID = "1004", Name = "佛山站", Poles = _poles });
                this.SiteCollection.Add(new Site() { ID = "1005", Name = "广州站", Poles = _poles });
                this.SiteCollection.Add(new Site() { ID = "1006", Name = "肇庆站", Poles = _poles });

                this.AnalystCollection.Add(new Analyst() { ID = "2001", Name = "刘德华" });
                this.AnalystCollection.Add(new Analyst() { ID = "2002", Name = "张国荣" });
                this.AnalystCollection.Add(new Analyst() { ID = "2003", Name = "贝克汉姆" });
                this.AnalystCollection.Add(new Analyst() { ID = "2004", Name = "齐达内" });
                this.AnalystCollection.Add(new Analyst() { ID = "2005", Name = "劳尔" });
                this.AnalystCollection.Add(new Analyst() { ID = "2006", Name = "马拉多纳" });
                this.AnalystCollection.Add(new Analyst() { ID = "2007", Name = "郝海东" });

            }
            //  Do：取消
            else if (command == "btn_add_analyst")
            {
                if (string.IsNullOrEmpty(SelectRawId)) return;

                _taskManagement.LoadAnalyst(this.AnalystCollection.ToList());

                foreach (var item in this.AnalystCollection)
                {
                    Message += $"加载分析员：ID - {item.ID} Name - {item.Name}" + Environment.NewLine;
                }


            }
            //  Do：取消
            else if (command == "btn_add_site")
            {
                if (string.IsNullOrEmpty(SelectRawId)) return;

                _taskManagement.LoadSites(this.SiteCollection.ToList());

                foreach (var item in this.SiteCollection)
                {
                    Message += $"加载站区：ID - {item.ID} Name - {item.Name}" + Environment.NewLine;
                }
            }
            //  Do：取消
            else if (command == "btn_addrawTask_Click")
            {
                if (string.IsNullOrEmpty(SelectRawId)) return;

                _taskManagement.LoadRawTask(SelectRawId);


                Message += $"加载待分配的原始数据包信息：ID {SelectRawId} " + Environment.NewLine;

            }
            //  Do：取消
            else if (command == "btn_addAll_Click")
            {
                this.RelayMethod("btn_addrawTask_Click");
                this.RelayMethod("btn_add_site");
                this.RelayMethod("btn_add_analyst");
            }

            //  Do：取消
            else if (command == "btn_delete_task")
            {
                if (this.SelectTask == null) return;

                _taskManagement.DeleteTask(this.SelectTask.TaskID);

                Message += $"删除任务：ID {this.SelectTask.TaskID} Name {this.SelectTask.TaskName}" + Environment.NewLine;

            }
            //  Do：取消
            else if (command == "btn_progress_over")
            {
                if (this.SelectTask == null) return;

                _taskManagement.SetTaskDone(this.SelectTask.TaskID);

                Message += $"手动设置任务已完成：ID {this.SelectTask.TaskID} Name {this.SelectTask.TaskName}" + Environment.NewLine;

            }

            //  Do：取消
            else if (command == "btn_rawtask_progress")
            {
                if (this.SelectTask == null) return;

                var result = _taskManagement.GetTaskProgress(this.SelectRawId);

                Message += "获取某个原始数据包总体处理进度：" + result + Environment.NewLine;
            }

            //  Do：取消
            else if (command == "btn_rawtask_progress")
            {
                if (this.SelectTask == null) return;

                var result = _taskManagement.GetTaskProgress(this.SelectRawId);

                Message += "获取某个原始数据包总体处理进度：" + result + Environment.NewLine;
            }
            //  Do：取消
            else if (command == "btn_find_result")
            {
                if (this.SelectAnalyst == null) return;

                var result = _taskManagement.GetAnalystWorkStat(this.SelectAnalyst.ID, this.StartTime, this.EndTime);

                Message += "获取分析员在某段时间内工作内容统计结果：" + result + Environment.NewLine;
            }
            //  Do：取消
            else if (command == "btn_find_task")
            {
                if (this.SelectAnalyst == null) return;

                if (this.SelectTask == null) return;

                var result = _taskManagement.GetAnalystTaskProgress(this.SelectTask.TaskID, this.SelectAnalyst.ID);

                Message += "获取某个分析员的某个任务处理进度：" + result + Environment.NewLine;
            }
            //  Do：取消
            else if (command == "btn_find_alltasks")
            {
                if (this.SelectAnalyst == null) return;

                if (this.SelectTask == null) return;

                var result = _taskManagement.GetAnalystHistoryTask(this.SelectAnalyst.ID, this.StartTime, this.EndTime);

                this.FindTaskCollection.Clear();

                if (result == null) return;

                foreach (var item in result)
                {
                    this.FindTaskCollection.Add(item);

                    Message += $"获取分析员某段时间内所有任务：ID {item.TaskID} Name {item.TaskName}" + Environment.NewLine;

                }


            }
            //  Do：取消
            else if (command == "btn_find_undone")
            {
                if (this.SelectAnalyst == null) return;

                var result = _taskManagement.GetAnalystProcedingTask(this.SelectAnalyst.ID);

                this.FindTaskCollection.Clear();

                if (result == null) return;

                foreach (var item in result)
                {
                    this.FindTaskCollection.Add(item);

                    Message += $"获取分析员当前正在处理的任务(未完成任务）列表：ID {item.TaskID} Name {item.TaskName}" + Environment.NewLine;

                }

            }

            else if (command == "btn_find_rawtaskid_result")
            {
                if (this.SelectAnalyst == null) return;

                if (this.SelectRawId == null) return;

                var result = _taskManagement.GetAnalystWorkStat(this.SelectAnalyst.ID, this.SelectRawId);

                Message += "分析员对某次跑车试验数据总体分析工作统计：" + result + Environment.NewLine;


            }







        }




    }
}
