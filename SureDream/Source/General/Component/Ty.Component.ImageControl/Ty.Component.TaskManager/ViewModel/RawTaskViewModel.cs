using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Ty.Base.WpfBase.Service;

namespace Ty.Component.TaskManager
{

    public partial class RawTaskViewModel : NotifyPropertyChanged
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


        private ObservableCollection<TaskViewModel> _deleteCollection = new ObservableCollection<TaskViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TaskViewModel> DeleteCollection
        {
            get { return _deleteCollection; }
            set
            {
                _deleteCollection = value;
                RaisePropertyChanged("DeleteCollection");
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

                AddItem = this.AddItem.Clone();

            }
            //  Do：取消
            else if (command == "btn_delete")
            {
                if (this.SelectItem == null) return;

                if (this.SelectItem.EditFlag == 0)
                {
                    //  Do：修改和历史数据则标识为删除，当确认时触发删除事件
                    this.DeleteCollection.Add(this.SelectItem);

                }

                this.TaskCollection.Remove(this.SelectItem);

            }
            //  Do：确定提交列表
            else if (command == "btn_sumit")
            {
                if (this.TaskCollection == null || this.TaskCollection.Count == 0) return;

                var adds = this.TaskCollection.Where(l => l.EditFlag == 1).Select(l => ConvertToTask(l)).ToList();

                //  Do：触发心中事件
                if (adds != null)
                {
                    this.TaskListCommitEvent?.Invoke(adds);
                }


                //  Do：触发删除事件
                if (this.DeleteCollection != null && this.DeleteCollection.Count > 0)
                {
                    foreach (var item in this.DeleteCollection)
                    {
                        this.TaskDeleteEvent?.Invoke(item.TaskID);
                    }
                }

                //  Do：删除提交后的删除任务
                this.DeleteCollection.Clear();

                //  Do：将新增任务设置成历史任务
                foreach (var item in this.TaskCollection.Where(l => l.EditFlag == 1))
                {
                    item.EditFlag = 0;
                }



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


        public event TaskDeleteHandler TaskDeleteEvent;

        public event TaskListCommitHandler TaskListCommitEvent;


    }


    public partial class RawTaskViewModel
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


}
