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

        private ObservableCollection<RawTaskViewModel> _collection = new ObservableCollection<RawTaskViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<RawTaskViewModel> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }


        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "init")
            {
               
            }
        }

        public IEnumerable<TaskViewModel> Where(Predicate<TaskViewModel> match)
        {
            foreach (var item in this.Collection)
            {
                foreach (var m in item.TaskCollection.Where(l => match(l)))
                {
                    yield return m;
                }
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
            var result = this.Where(l => l.TaskID == task.TaskID);

            if (result == null || result.Count() == 0)
            {

                Debug.WriteLine("未查找到对应Task：" + task.TaskID);
                return;
            }

            result.First().ConvertFromTask(task);

        }

        public void DeleteTask(string taskID)
        {
            foreach (var item in this.Collection)
            {
                item.TaskCollection.ToList().RemoveAll(l => l.TaskID == taskID);
            }

        }

        public List<Task> GetAnalystHistoryTask(string analystID, DateTime fromDate, DateTime toDate)
        {
            var result = this.Where(l => l.Analyst.ID == analystID && l.StartDate >= fromDate && l.EndDate <= toDate);

            return result == null ? null : result.Select(l => l.ConvertToTask()).ToList();
        }

        public List<Task> GetAnalystProcedingTask(string analystID)
        {

            //  ToDo：获取分析员当前正在处理的任务(未完成任务）列表 删除TaskViewModel Progress<100
            var result = this.Where(l => l.Analyst.ID == analystID && l.Progress < 100);

            return result == null ? null : result.Select(l => l.ConvertToTask()).ToList();
        }

        public double GetAnalystTaskProgress(string taskID, string analystID)
        {
            var result = this.Where(l => l.Analyst.ID == analystID && l.TaskID == taskID);

            if (result == null || result.Count() == 0)
            {
                Debug.WriteLine("未查找到对应Task：" + taskID + "未查找到对应analystID：" + analystID);
                return 0;
            }

            //  Message：计算平均值作为进度
            return result.Sum(l => l.Progress) / result.Count();
        }

        /// <summary>
        /// 分析员对某次跑车试验数据总体分析工作统计
        /// </summary>
        /// <param name="analystID">分析员ID</param>
        /// <param name="rawTaskID">原始数据包ID</param>
        /// <returns>统计结果报表内容</returns>
        public string GetAnalystWorkStat(string analystID, string rawTaskID)
        {
            var current = this.Collection.Where(l => l.RawTaskID == rawTaskID);

            if (current == null || current.Count() == 0)
            {
                Debug.WriteLine("未查找到对应rawTaskID：" + rawTaskID);
                return null;
            }

            var tasks = current.First().TaskCollection.Where(l => l.Analyst.ID == analystID);

            if (tasks == null || tasks.Count() == 0)
            {
                Debug.WriteLine("未查找到对应analystID：" + analystID);
                return null;
            }

            StringBuilder sb = new StringBuilder();

            foreach (var item in tasks)
            {
                sb.AppendLine(string.Format($"分析员:{item.Analyst.Name} 任务ID:{item.TaskName} 完成情况:{item.Progress}"));
            }

            return sb.ToString();
        }

        public string GetAnalystWorkStat(string analystID, DateTime starttime, DateTime endtime)
        {
            var result = this.Where(l => l.Analyst.ID == analystID && l.StartDate >= starttime && l.EndDate <= endtime);

            if (result == null || result.Count() == 0)
            {
                Debug.WriteLine("未查找到对应结果");
                return null;
            }

            StringBuilder sb = new StringBuilder();

            foreach (var item in result)
            {
                sb.AppendLine(string.Format($"分析员:{item.Analyst.Name} 任务ID:{item.TaskName} 完成情况:{item.Progress}"));
            }

            return sb.ToString();
        }

        public string GetDefectStat(string rawTaskID)
        {
            //  ToDo：获取某次跑车数据分析得到的缺陷统计结果  缺陷统计结果如何计算
            throw new NotImplementedException();
        }

        public string GetLevelOneDefectStat(string rawTaskID)
        {
            //  ToDo：获取某次跑车数据分析得到的二级缺陷统计结果   缺陷统计结果如何计算

            throw new NotImplementedException();
        }

        public string GetLevelTwoDefectStat(string rawTaskID)
        {
            //  ToDo：获取某次跑车数据分析得到的二级缺陷统计结果   缺陷统计结果如何计算

            throw new NotImplementedException();
        }

        public double GetTaskProgress(string rawTaskID)
        {
            var result = this.Collection.Where(l => l.RawTaskID == rawTaskID);

            if (result == null || result.Count() == 0)
            {
                Debug.WriteLine("未查找到对应结果");
                return 0;
            }
            if(result.First().TaskCollection.Count()==0)
            {
                return 0;
            }

            return result.First().TaskCollection.Sum(l => l.Progress) / result.First().TaskCollection.Count();
        }

        public string GetXFactorStat(string rawTaskID, List<string> factorNameList)
        {
            throw new NotImplementedException();
        }

        public void LoadAnalyst(List<Analyst> analystList)
        {
            foreach (var item in this.Collection)
            {
                item.AnalystCollection = analystList;
            }
        }

        public void LoadRawTask(string rawTaskID)
        {
            if(this.Collection.ToList().Exists(l=>l.RawTaskID==rawTaskID))
            {
                Debug.WriteLine("已经存在该项值");
                return;
            }

            RawTaskViewModel vm = new RawTaskViewModel();
            vm.RawTaskID = rawTaskID;
            vm.TaskDeleteEvent += this.TaskDeleteEvent;
            vm.TaskListCommitEvent += this.TaskListCommitEvent;
             
            this.Collection.Add(vm);
        }

    

        public void LoadSites(List<Site> siteList)
        {
            foreach (var item in this.Collection)
            {
                item.SiteCollection = siteList;
            }
        }

        public bool SetTaskDone(string taskID)
        {
            var task = this.Where(l => l.TaskID == taskID);

            if (task == null || task.Count() == 0)
            {
                Debug.WriteLine("没有查找到指定任务ID:" + taskID);
                return false;
            }
            task.First().Progress = 100;

            return true;

        }
    }
}
