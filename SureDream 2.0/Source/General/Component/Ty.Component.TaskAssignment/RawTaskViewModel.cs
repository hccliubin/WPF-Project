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

namespace Ty.Component.TaskAssignment
{

    public partial class RawTaskViewModel : NotifyPropertyChanged
    {

        //private string _rawTaskID;
        ///// <summary> 说明  </summary>
        //public string RawTaskID
        //{
        //    get { return _rawTaskID; }
        //    set
        //    {
        //        _rawTaskID = value;
        //        RaisePropertyChanged("RawTaskID");
        //    }
        //}

        private ObservableCollection<Analyst> _analystCollection = new ObservableCollection<Analyst>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Analyst> AnalystCollection
        {
            get { return _analystCollection; }
            set
            {
                _analystCollection = value;
                RaisePropertyChanged("AnalystCollection");
            }
        }

        private ObservableCollection<Station> _siteCollection = new ObservableCollection<Station>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Station> SiteCollection
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
                this.TaskCollection.CollectionChanged += (l, k) =>
              {

                  for (int i = 0; i < this.TaskCollection.Count; i++)
                  {
                      this.TaskCollection[i].TaskID = (i+1).ToString().PadLeft(2,'0');
                  }
              };

            }
            //  Do：取消
            else if (command == "btn_add")
            {
                if (this.AddItem == null) return;

                if (this.AddItem.StartSite == null || this.AddItem.EndSite == null || this.AddItem.Analyst == null || string.IsNullOrEmpty(this.AddItem.TaskName))
                {
                    MessageBox.Show("信息不完整，请选择有效信息");
                    return;
                }

                if (this.AddItem.StartSite.ID == this.AddItem.EndSite.ID)
                {
                    if (this.AddItem.StartPole == null || this.AddItem.EndPole == null)
                    {
                        MessageBox.Show("信息不完整，请选择有效信息");
                        return;
                    }
                }

                AddItem.StartDate = DateTime.Now;

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
                if (this.TaskCollection == null) return;


                string err;
                if (!this.IsVaild(out err))
                {
                    var result = MessageBox.Show(err + "是否继续保存？", "提示！", MessageBoxButton.YesNo, MessageBoxImage.Error);

                    if (result == MessageBoxResult.No) return;
                }


                //var adds = this.TaskCollection.Where(l => l.EditFlag == 1).Select(l => ConvertToTask(l)).ToList();

                ////  Do：触发心中事件
                //if (adds != null)
                //{
                //    this.TaskListCommitEvent?.Invoke(adds);
                //}

                ////  Do：触发删除事件
                //if (this.DeleteCollection != null && this.DeleteCollection.Count > 0)
                //{
                //    foreach (var item in this.DeleteCollection)
                //    {
                //        this.TaskDeleteEvent?.Invoke(item.TaskID);
                //    }
                //}

                ////  Do：删除提交后的删除任务
                //this.DeleteCollection.Clear();

                ////  Do：将新增任务设置成历史任务
                //foreach (var item in this.TaskCollection.Where(l => l.EditFlag == 1))
                //{
                //    item.EditFlag = 0;
                //}

            }
            ////  Do：取消
            //else if (command == "btn_divied")
            //{
            //    DividedWindow window = new DividedWindow();
            //    window.DataContext = this;
            //    window.ShowDialog();

            //}

            ////  Do：取消
            //else if (command == "btn_showTask")
            //{
            //    ShowTaskWindow window = new ShowTaskWindow();
            //    window.DataContext = this;
            //    window.ShowDialog();
            //}

        }

        //Task ConvertToTask(TaskViewModel vm)
        //{
        //    Task task = new Task();
        //    task.TaskID = vm.TaskID;
        //    task.AnalystID = vm.Analyst.ID;
        //    task.AnalystName = vm.Analyst.Name;
        //    task.EndDate = vm.EndDate;
        //    task.StartDate = vm.StartDate;
        //    task.StartSiteID = vm.StartSite.ID;
        //    task.StartSiteName = vm.StartSite.Name;
        //    task.TaskName = vm.TaskName;
        //    //task.Progress = double.Parse(vm.Progress);
        //    task.EndSiteName = vm.EndSite.Name;
        //    task.EndSiteID = vm.EndSite.ID;
        //    return task;

        //}


        /// <summary>
        /// 当前页面是否存在未保存的编辑
        /// </summary>
        /// <returns></returns>
        public bool IsEdit()
        {
            var adds = this.TaskCollection.Where(l => l.EditFlag == 1);

            if (adds.Count() > 0) return true;

            if (this.DeleteCollection.Count > 0) return true;

            return false;
        }

        /// <summary>
        /// 检查当前数据是否完整(检查一：所有站和杆号是否分配，检查二：是否重复分配)
        /// </summary>
        /// <returns></returns>
        public bool IsVaild(out string err)
        {
            err = string.Empty;

            #region - 检查区间站 -
            {
                //  ToDo：检查站区是否全部分配
                var startToEnd = this.TaskCollection.Select(l => new Tuple<int, int>(Math.Min(this.SiteCollection.IndexOf(l.StartSite),
                          this.SiteCollection.IndexOf(l.EndSite)), Math.Max(this.SiteCollection.IndexOf(l.StartSite), this.SiteCollection.IndexOf(l.EndSite)))).OrderBy(l => l.Item1).OrderBy(l => l.Item2).ToList();


                List<Station> results = new List<Station>();

                for (int i = 0; i < this.SiteCollection.Count; i++)
                {
                    if (!startToEnd.Exists(l => l.Item1 <= i && l.Item2 >= i))
                    {
                        results.Add(this.SiteCollection[i]);
                    }
                }

                if (results != null && results.Count > 0)
                {
                    string param = results.Select(l => l.StationName).Aggregate((l, k) => l + "," + k);

                    err = $"存在未分配的区间站[{param.Trim(',')}]";
                    return false;
                }

                string value = string.Empty;

                //  Do：返回前一个结束站台与后一个起始站台的差 如果为正数则返回前一个站台区间
                startToEnd.Aggregate((l, k) =>
                {
                    //  Message：同一站台不检查数据重复
                    if (l.Item1 == l.Item2)
                    {
                        return k;
                    }

                    if ((l.Item2 - k.Item1) > 0)
                    {
                        value += this.SiteCollection[l.Item2].StationName + ",";
                        return k;
                    }
                    else if ((l.Item2 - k.Item1) == 0 && l.Item1 == k.Item2)
                    {
                        value += this.SiteCollection[l.Item2].StationName + ",";
                        return k;
                    }
                    else
                    {
                        return k;
                    }
                });

                if (!string.IsNullOrEmpty(value))
                {
                    err = $"任务列表中存在重复分配的区间站[{value.Trim(',')}]";
                    return false;
                }

            }


            #endregion

            #region - 检查杆号 -

            //  ToDo：检查数据完整性 同一站台杆号是否都选择完全
            var sites = this.TaskCollection.Where(l => l.StartSite.ID == l.EndSite.ID).GroupBy(l => l.StartSite.ID);
            {
                foreach (var site in sites)
                {
                    //  Do：差找杆号的最大值
                    int maxValue = site.First().StartSite.Rods.Max(l => int.Parse(l.RodName));

                    int minValue = site.First().StartSite.Rods.Min(l => int.Parse(l.RodName));

                    //  Do：获取当前站的所有任务信息
                    var collection = this.TaskCollection.Where(l => l.StartSite.ID == site.Key && l.EndSite.ID == site.Key);

                    //  Do：提取当前站的杆号起始和结束杆号信息并从小到大排序
                    var startToEnd = collection.Select(l => new Tuple<int, int>(Math.Min(int.Parse(l.StartPole.RodName),
                        int.Parse(l.EndPole.RodName)), Math.Max(int.Parse(l.StartPole.RodName), int.Parse(l.EndPole.RodName)))).OrderBy(l => l.Item1).OrderBy(l => l.Item2).ToList();

                    //int maxValue = startToEnd.Max(l => l.Item2);

                    var allValue = site.First().StartSite.Rods.Select(l => int.Parse(l.RodName)).ToList();

                    //  Do：遍历每一个杆号，检查是否所有杆号都落在任务杆号区间内部
                    var result = allValue.FindAll(k => !startToEnd.Exists(l => l.Item1 <= k && l.Item2 >= k));

                    if (result != null && result.Count > 0)
                    {
                        string param = result.Select(l => l.ToString()).Aggregate((l, k) => l.ToString() + "," + k.ToString());

                        err = $"[{site.First().StartSite.StationName}]存在未分配的杆号[{param.Trim(',')}]";
                        return false;
                    }

                    string value = string.Empty;

                    //  Do：返回前一个结束杆号与后一个起始杆号的差 如果为正数则返回前一个杆号区间
                    startToEnd.Aggregate((l, k) =>
                    {

                        if ((l.Item2 - k.Item1) > 0)
                        {
                            value += l.ToString() + ",";
                            return k;
                        }
                        else if ((l.Item2 - k.Item1) == 0 && l.Item1 == k.Item2)
                        {
                            value += l.ToString() + ",";
                            return k;
                        }
                        else
                        {
                            return k;
                        }
                    });


                    if (!string.IsNullOrEmpty(value))
                    {
                        err = $"[{site.First().StartSite.StationName}]任务列表中存在重复分配的杆号[{value.Trim(',')}]";
                        return false;
                    }

                }
            }

            #endregion

            return true;
        }



        //public event TaskDeleteHandler TaskDeleteEvent;

        //public event TaskListCommitHandler TaskListCommitEvent;

        public void RefreshConfig(TaskAllocation entity)
        {
            this.SiteCollection = entity.Stations;
            this.AnalystCollection = entity.Analysts;
        }
    }


    public partial class RawTaskViewModel
    {

        //private string _rawTaskName;
        ///// <summary> 说明  </summary>
        //public string RawTaskName
        //{
        //    get { return _rawTaskName; }
        //    set
        //    {
        //        _rawTaskName = value;
        //        RaisePropertyChanged("RawTaskName");
        //    }
        //}

        //private string _machineType;
        ///// <summary> 说明  </summary>
        //public string MachineType
        //{
        //    get { return _machineType; }
        //    set
        //    {
        //        _machineType = value;
        //        RaisePropertyChanged("MachineType");
        //    }
        //}

        //private DateTime _realDate;
        ///// <summary> 说明  </summary>
        //public DateTime RealDate
        //{
        //    get { return _realDate; }
        //    set
        //    {
        //        _realDate = value;
        //        RaisePropertyChanged("RealDate");
        //    }
        //}

        //private string _siteRange;
        ///// <summary> 说明  </summary>
        //public string SiteRange
        //{
        //    get { return _siteRange; }
        //    set
        //    {
        //        _siteRange = value;
        //        RaisePropertyChanged("SiteRange");
        //    }
        //}

        //private string _progress;
        ///// <summary> 说明  </summary>
        //public string Progress
        //{
        //    get { return _progress; }
        //    set
        //    {
        //        _progress = value;
        //        RaisePropertyChanged("Progress");
        //    }
        //}
    }


}
