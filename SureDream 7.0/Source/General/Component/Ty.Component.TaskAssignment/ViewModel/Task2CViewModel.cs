using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Ty.Base.WpfBase;
using Ty.Base.WpfBase.Service;

namespace Ty.Component.TaskAssignment
{
    /// <summary> 具体任务项 </summary>
    public class Task2CViewModel : NotifyPropertyChanged
    {
        #region - 成员属性 -

        private string _taskID = Guid.NewGuid().ToString();
        /// <summary> 任务ID </summary>
        public string TaskID
        {
            get { return _taskID; }
            set
            {
                _taskID = value;
                RaisePropertyChanged("TaskID");
            }
        }

        private string _seriaNumber = Guid.NewGuid().ToString();
        /// <summary> 序号  </summary>
        public string SeriaNumber
        {
            get { return _seriaNumber; }
            set
            {
                _seriaNumber = value;
                RaisePropertyChanged("SeriaNumber");
            }
        }

        private TaskTypeEnum _taskTypeEnum = TaskTypeEnum.DetectionTask;
        /// <summary> 任务类型  </summary>
        public TaskTypeEnum TaskTypeEnum
        {
            get { return _taskTypeEnum; }
            set
            {
                _taskTypeEnum = value;
                RaisePropertyChanged("TaskTypeEnum");
            }
        }

        private TyeAdminUserEntity _analyst;
        /// <summary> 分析员  </summary>
        public TyeAdminUserEntity Analyst
        {
            get { return _analyst; }
            set
            {
                _analyst = value;
                RaisePropertyChanged("Analyst");
            }
        } 

        private DateTime? _startDate = DateTime.Now;
        /// <summary> 起始日期  </summary>
        public DateTime? StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                RaisePropertyChanged("StartDate");
            }
        }

        private DateTime? _endDate = DateTime.Now;
        /// <summary> 结束日期  </summary>
        public DateTime? EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                RaisePropertyChanged("EndDate");
            }
        }

        private TyeLineEntity _startLine;
        /// <summary> 起始杆号  </summary>
        public TyeLineEntity StartLine
        {
            get { return _startLine; }
            set
            {
                _startLine = value;
                RaisePropertyChanged("StartLine");
            }
        }

        private TyeLineEntity _endLine;
        /// <summary> 结束杆号  </summary>
        public TyeLineEntity EndLine
        {
            get { return _endLine; }
            set
            {
                _endLine = value;
                RaisePropertyChanged("EndLine");
            }
        }

        private double _progress;
        /// <summary> 进度  </summary>
        public double Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                RaisePropertyChanged("Progress");
            }
        } 

        #endregion

        /// <summary> 复制 </summary>
        public Task2CViewModel Clone()
        {
            Task2CViewModel vm = new Task2CViewModel();
            //vm.TaskID = this.TaskID;
            vm.Analyst = new TyeAdminUserEntity();
            vm.Analyst.ID = this.Analyst.ID;
            vm.Analyst.Name = this.Analyst.Name;
            vm.EndDate = this.EndDate;
            vm.StartDate = this.StartDate;   
            //vm.TaskID = Guid.NewGuid().ToString();
            vm.TaskTypeEnum = this.TaskTypeEnum;
            vm.SeriaNumber = this.SeriaNumber;
            vm.Progress = this.Progress;
            vm.StartLine = this.StartLine;
            vm.EndLine = this.EndLine;  

            return vm;
        } 

        /// <summary> 转换为输出类型 </summary>
        public TaskModel_2C ConvertTo()
        {
            TaskModel_2C model = new TaskModel_2C();
            model.AnalystID = this.Analyst.ID;
            model.TaskEndTime = this.EndDate;
            model.TaskStartTime = this.StartDate;
            //model.StartSiteID = this.StartSite?.ID;
            //model.EndSiteID = this.EndSite?.ID;

            //model.ID = int.Parse(this.TaskID);
            model.ProcessType = (int)this.TaskTypeEnum;
            //model.SeriaNumber = this.SeriaNumber;
            //model.Progress = this.Progress;
            model.TaskEndTime = this._endDate;
            model.StartPoleID = this.StartLine?.ID;
            model.EndPoleID = this.EndLine?.ID;
            return model;
        }
        

    }

}
