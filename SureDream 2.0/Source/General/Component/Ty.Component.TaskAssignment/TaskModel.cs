using System;
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
    public class TaskModel : NotifyPropertyChanged
    {
        private string _taskID = Guid.NewGuid().ToString();
        /// <summary>
        /// 任务ID
        /// </summary>
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
        /// <summary> 说明  </summary>
        public string SeriaNumber
        {
            get { return _seriaNumber; }
            set
            {
                _seriaNumber = value;
                RaisePropertyChanged("SeriaNumber");
            }
        }

        private TaskTypeEnum _taskTypeEnum= TaskTypeEnum.DetectionTask;
        /// <summary> 说明  </summary>
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
        /// <summary> 说明  </summary>
        public TyeAdminUserEntity Analyst
        {
            get { return _analyst; }
            set
            {
                _analyst = value;
                RaisePropertyChanged("Analyst");
            }
        }

        private TyeBaseSiteEntity _startSite;
        /// <summary> 说明  </summary>
        public TyeBaseSiteEntity StartSite
        {
            get { return _startSite; }
            set
            {
                _startSite = value;
                RaisePropertyChanged("StartSite");

                if (this.EndSite == null) return;

                if (value == null) return;

                if(value.SiteName==this.EndSite.SiteName)
                {
                    this.SeletctSameSiteEvent?.Invoke(value);
                }
            }
        }

        public event Action<TyeBaseSiteEntity> SeletctSameSiteEvent;

        private TyeBaseSiteEntity _endSite;
        /// <summary> 说明  </summary>
        public TyeBaseSiteEntity EndSite
        {
            get { return _endSite; }
            set
            {
                _endSite = value;

                RaisePropertyChanged("EndSite");

                if (this.StartSite == null) return;

                if (value == null) return;

                if (value.SiteName == this.StartSite.SiteName)
                {
                    this.SeletctSameSiteEvent?.Invoke(value);
                }
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

        private TyeBasePillarEntity _startPole;
        /// <summary> 说明  </summary>
        public TyeBasePillarEntity StartPole
        {
            get { return _startPole; }
            set
            {
                _startPole = value;
                RaisePropertyChanged("StartPole");
            }
        }

        private TyeBasePillarEntity _endPole;
        /// <summary> 说明  </summary>
        public TyeBasePillarEntity EndPole
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

        public TaskModel Clone()
        {
            TaskModel vm = new TaskModel();
            //vm.TaskID = this.TaskID;
            vm.Analyst = new TyeAdminUserEntity();
            vm.Analyst.ID = this.Analyst.ID;
            vm.Analyst.Name = this.Analyst.Name;
            vm.EndDate = this.EndDate;
            vm.StartDate = this.StartDate;
            vm.StartSite = this.StartSite;
            vm.StartSite.ID = this.StartSite.ID;
            vm.StartSite.SiteName = this.StartSite.SiteName;
            vm.TaskID = Guid.NewGuid().ToString();
            vm.TaskTypeEnum = this.TaskTypeEnum;
            vm.SeriaNumber= this.SeriaNumber;
            vm.Progress = this.Progress;
            vm.EndSite = this.EndSite;
            vm.EndSite.SiteName = this.EndSite.SiteName;
            vm.EndSite.ID = this.EndSite.ID;

            return vm;
        }

       

    }

}
