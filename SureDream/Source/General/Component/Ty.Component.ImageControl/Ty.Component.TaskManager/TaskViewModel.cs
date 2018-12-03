using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Ty.Base.WpfBase;
using Ty.Base.WpfBase.Service;

namespace Ty.Component.TaskManager
{
   public class TaskViewModel : NotifyPropertyChanged
    {
        private string _taskID;
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

        private string _taskName;
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

        private Site _startSite;
        /// <summary> 说明  </summary>
        public Site StartSite
        {
            get { return _startSite; }
            set
            {
                _startSite = value;
                RaisePropertyChanged("StartSite");
            }
        }
        
        private Site _endSite;
        /// <summary> 说明  </summary>
        public Site EndSite
        {
            get { return _endSite; }
            set
            {
                _endSite = value;
                RaisePropertyChanged("EndSite");
            }
        }

        private DateTime _startDate=DateTime.Now;
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

        private Pole _startPole;
        /// <summary> 说明  </summary>
        public Pole StartPole
        {
            get { return _startPole; }
            set
            {
                _startPole = value;
                RaisePropertyChanged("StartPole");
            }
        }

        private Pole _endPole;
        /// <summary> 说明  </summary>
        public Pole EndPole
        {
            get { return _endPole; }
            set
            {
                _endPole = value;
                RaisePropertyChanged("EndPole");
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

        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {


            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }

    }

}
