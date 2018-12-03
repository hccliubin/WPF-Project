using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ty.Base.WpfBase.Service;

namespace Ty.Component.TaskManager
{

    public partial class TaskManagement : NotifyPropertyChanged
    {

        private string _rawTaskID;
        /// <summary> 说明  </summary>
        public string RawTaskID
        {
            get { return _rawTaskID; }
            set
            {
                _rawTaskID = value;
                RaisePropertyChanged("RawTaskID");
            }
        }

        private List<Analyst> _analystCollection;
        /// <summary> 说明  </summary>
        public List<Analyst> AnalystCollection
        {
            get { return _analystCollection; }
            set
            {
                _analystCollection = value;
                RaisePropertyChanged("AnalystCollection");
            }
        }

        private List<Site> _siteCollection;
        /// <summary> 说明  </summary>
        public List<Site> SiteCollection
        {
            get { return _siteCollection; }
            set
            {
                _siteCollection = value;
                RaisePropertyChanged("SiteCollection");
            }
        }

        private ObservableCollection<TaskViewModel> _taskCollection = new ObservableCollection<TaskViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TaskViewModel> TaskCollection
        {
            get { return _taskCollection; }
            set
            {
                _taskCollection = value;
                RaisePropertyChanged("TaskCollection");
            }
        }

        private TaskViewModel _selectItem;
        /// <summary> 说明  </summary>
        public TaskViewModel SelectItem
        {
            get { return _selectItem; }
            set
            {
                _selectItem = value;
                RaisePropertyChanged("SelectItem");
            }
        }

        private TaskViewModel _addItem = new TaskViewModel();
        /// <summary> 说明  </summary>
        public TaskViewModel AddItem
        {
            get { return _addItem; }
            set
            {
                _addItem = value;
                RaisePropertyChanged("AddItem");
            }
        }

        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();


            Debug.WriteLine(command);


            //  Do：应用
            if (command == "init")
            {


            }
            //  Do：取消
            else if (command == "btn_add")
            {
                if (this.AddItem == null) return;

                if (this.AddItem.StartSite == null) return;

                if (this.AddItem.EndSite == null) return;

                if (this.AddItem.Analyst == null) return;

                if (this.AddItem.StartSite.ID != this.AddItem.EndSite.ID)
                {
                    if (this.AddItem.StartPole != null)
                    {
                        this.AddItem.StartPole.ID = string.Empty;
                        this.AddItem.StartPole.Name = "*";
                    }

                    if (this.AddItem.EndPole != null)
                    {
                        this.AddItem.EndPole.ID = string.Empty;
                        this.AddItem.EndPole.Name = "*";
                    }

                }

                this.TaskCollection.Add(AddItem);
                AddItem = new TaskViewModel();

            }
            //  Do：取消
            else if (command == "btn_delete")
            {
                if (this.SelectItem == null) return;

                this.TaskCollection.Remove(this.SelectItem);

            }
            //  Do：取消
            else if (command == "btn_sumit")
            {
                if (this.TaskCollection == null || this.TaskCollection.Count == 0) return;

                var result = this.TaskCollection.Select(l => ConvertToTask(l)).ToList();

                this.TaskListCommitEvent?.Invoke(result);

            }
            //  Do：取消
            else if (command == "btn_divied")
            {
                DividedWindow window = new DividedWindow();
                window.DataContext = this;
                window.ShowDialog();

            }

            //  Do：取消
            else if (command == "btn_showTask")
            {
                ShowTaskWindow window = new ShowTaskWindow();
                window.DataContext = this;
                window.ShowDialog();
            }

        }

        Task ConvertToTask(TaskViewModel vm)
        {
            Task task = new Task();
            task.TaskID = vm.TaskID;
            task.AnalystID = vm.Analyst.ID;
            task.AnalystName = vm.Analyst.Name;
            task.EndDate = vm.EndDate;
            task.StartDate = vm.StartDate;
            task.StartSiteID = vm.StartSite.ID;
            task.StartSiteName = vm.StartSite.Name;
            task.TaskName = vm.TaskName;
            //task.Progress = double.Parse(vm.Progress);
            task.EndSiteName = vm.EndSite.Name;
            task.EndSiteID = vm.EndSite.ID;
            return task;

        }

    }


    public partial class TaskManagement
    {

        private string _rawTaskName;
        /// <summary> 说明  </summary>
        public string RawTaskName
        {
            get { return _rawTaskName; }
            set
            {
                _rawTaskName = value;
                RaisePropertyChanged("RawTaskName");
            }
        }

        private string _machineType;
        /// <summary> 说明  </summary>
        public string MachineType
        {
            get { return _machineType; }
            set
            {
                _machineType = value;
                RaisePropertyChanged("MachineType");
            }
        }

        private DateTime _realDate;
        /// <summary> 说明  </summary>
        public DateTime RealDate
        {
            get { return _realDate; }
            set
            {
                _realDate = value;
                RaisePropertyChanged("RealDate");
            }
        }

        private string _siteRange;
        /// <summary> 说明  </summary>
        public string SiteRange
        {
            get { return _siteRange; }
            set
            {
                _siteRange = value;
                RaisePropertyChanged("SiteRange");
            }
        }

        private string _progress;
        /// <summary> 说明  </summary>
        public string Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                RaisePropertyChanged("Progress");
            }
        }
    }

    partial class TaskManagement : ITaskManagement
    {
        public event TaskDeleteHandler TaskDeleteEvent;

        public event TaskListCommitHandler TaskListCommitEvent;

        public void AlterTask(Task task)
        {
            //  ToDo：修改TaskViewModel
            throw new NotImplementedException();
        }

        public void DeleteTask(string taskID)
        {
            //  ToDo：删除TaskViewModel
            throw new NotImplementedException();
        }

        public List<Task> GetAnalystHistoryTask(string analystID, DateTime fromDate, DateTime toDate)
        {

            throw new NotImplementedException();
        }

        public List<Task> GetAnalystProcedingTask(string analystID)
        {
        
            //  ToDo：获取分析员当前正在处理的任务(未完成任务）列表 删除TaskViewModel Progress<100
            throw new NotImplementedException();
        }

        public double GetAnalystTaskProgress(string taskID, string analystID)
        {
            throw new NotImplementedException();
        }

        public string GetAnalystWorkStat(string analystID, string rawTaskID)
        {
            throw new NotImplementedException();
        }

        public string GetAnalystWorkStat(string analystID, DateTime starttime, DateTime endtime)
        {
            throw new NotImplementedException();
        }

        public string GetDefectStat(string rawTaskID)
        {
            throw new NotImplementedException();
        }

        public string GetLevelOneDefectStat(string rawTaskID)
        {
            throw new NotImplementedException();
        }

        public string GetLevelTwoDefectStat(string rawTaskID)
        {
            throw new NotImplementedException();
        }

        public double GetTaskProgress(string rawTaskID)
        {
            throw new NotImplementedException();
        }

        public string GetXFactorStat(string rawTaskID, List<string> factorNameList)
        {
            throw new NotImplementedException();
        }

        public void LoadAnalyst(List<Analyst> analystList)
        {
            this.AnalystCollection = analystList;
        }

        public void LoadRawTask(string rawTaskID)
        {
            this.RawTaskID = rawTaskID;
        }

        public void LoadSites(List<Site> siteList)
        {
            this.SiteCollection = siteList;
        }

        public bool SetTaskDone(string taskID)
        {
            var task = this.TaskCollection.Where(l => l.TaskID == taskID);

            if (task == null|| task.Count()==0)
            {
                Debug.WriteLine("没有查找到指定任务ID:"+ taskID);
                return false;
            }
            task.First().Progress = "100";

            return true;
            
        }
    }
}
