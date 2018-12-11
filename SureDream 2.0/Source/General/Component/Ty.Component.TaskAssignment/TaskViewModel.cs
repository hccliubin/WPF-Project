﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Ty.Base.WpfBase;
using Ty.Base.WpfBase.Service;

namespace Ty.Component.TaskAssignment
{
    public class TaskViewModel : NotifyPropertyChanged
    {
        private string _taskID = Guid.NewGuid().ToString();
        /// <summary> 说明  </summary>
        public string TaskID
        {
            get { return _taskID; }
            set
            {
                _taskID = value;
                RaisePropertyChanged("TaskID");
            }
        }

        private string _taskName = "检测任务";
        /// <summary> 说明  </summary>
        public string TaskName
        {
            get { return _taskName; }
            set
            {
                _taskName = value;
                RaisePropertyChanged("TaskName");
            }
        }

        private Analyst _analyst;
        /// <summary> 说明  </summary>
        public Analyst Analyst
        {
            get { return _analyst; }
            set
            {
                _analyst = value;
                RaisePropertyChanged("Analyst");
            }
        }

        private Station _startSite;
        /// <summary> 说明  </summary>
        public Station StartSite
        {
            get { return _startSite; }
            set
            {
                _startSite = value;
                RaisePropertyChanged("StartSite");
            }
        }

        private Station _endSite;
        /// <summary> 说明  </summary>
        public Station EndSite
        {
            get { return _endSite; }
            set
            {
                _endSite = value;
                RaisePropertyChanged("EndSite");
            }
        }

        private DateTime _startDate = DateTime.Now;
        /// <summary> 说明  </summary>
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                RaisePropertyChanged("StartDate");
            }
        }

        private DateTime _endDate = DateTime.Now;
        /// <summary> 说明  </summary>
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                RaisePropertyChanged("EndDate");
            }
        }

        private Rod _startPole;
        /// <summary> 说明  </summary>
        public Rod StartPole
        {
            get { return _startPole; }
            set
            {
                _startPole = value;
                RaisePropertyChanged("StartPole");
            }
        }

        private Rod _endPole;
        /// <summary> 说明  </summary>
        public Rod EndPole
        {
            get { return _endPole; }
            set
            {
                _endPole = value;
                RaisePropertyChanged("EndPole");
            }
        }

        private double _progress;
        /// <summary> 说明  </summary>
        public double Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                RaisePropertyChanged("Progress");
            }
        }

        private int _editFlag = 1;
        /// <summary> 修改标识：1 新增 0 历史数据  </summary>
        public int EditFlag
        {
            get { return _editFlag; }
            set
            {
                _editFlag = value;
                RaisePropertyChanged("EditFlag");
            }
        }



        private string _poleSpace;
        /// <summary> 杆号范围  </summary>
        public string PoleSpace
        {
            get { return _poleSpace; }
            set
            {
                _poleSpace = value;
                RaisePropertyChanged("PoleSpace");
            }
        }

        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Init")
            {


            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }

        //public Task ConvertToTask()
        //{
        //    Task task = new Task();
        //    task.TaskID = this.TaskID;
        //    task.AnalystID = this.Analyst.ID;
        //    task.AnalystName = this.Analyst.Name;
        //    task.EndDate = this.EndDate;
        //    task.StartDate = this.StartDate;
        //    task.StartSiteID = this.StartSite.ID;
        //    task.StartSiteName = this.StartSite.Name;
        //    task.TaskName = this.TaskName;
        //    task.Progress = this.Progress;
        //    task.EndSiteName = this.EndSite.Name;
        //    task.EndSiteID = this.EndSite.ID;
        //    return task;

        //}

        //public void ConvertFromTask(Task task)
        //{
        //    this.TaskID = task.TaskID;
        //    this.Analyst.ID = task.AnalystID;
        //    this.Analyst.Name = task.AnalystName;
        //    this.EndDate = task.EndDate;
        //    this.StartDate = task.StartDate;
        //    this.StartSite.ID = task.StartSiteID;
        //    this.StartSite.Name = task.StartSiteName;
        //    this.TaskName = task.TaskName;
        //    this.Progress = task.Progress;
        //    this.EndSite.Name = task.EndSiteName;
        //    this.EndSite.ID = task.EndSiteID;

        //}

        public TaskViewModel Clone()
        {
            TaskViewModel vm = new TaskViewModel();
            //vm.TaskID = this.TaskID;
            vm.Analyst = new Analyst();
            vm.Analyst.ID = this.Analyst.ID;
            vm.Analyst.AnalystName = this.Analyst.AnalystName;
            vm.EndDate = this.EndDate;
            vm.StartDate = this.StartDate;
            vm.StartSite = this.StartSite;
            vm.StartSite.ID = this.StartSite.ID;
            vm.StartSite.StationName = this.StartSite.StationName;
            vm.TaskName = this.TaskName;
            vm.Progress = this.Progress;
            vm.EndSite = this.EndSite;
            vm.EndSite.StationName = this.EndSite.StationName;
            vm.EndSite.ID = this.EndSite.ID;

            return vm;
        }

    }

}
