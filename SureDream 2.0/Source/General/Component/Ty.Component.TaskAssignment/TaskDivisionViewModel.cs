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

    public partial class TaskDivisionViewModel : NotifyPropertyChanged
    {


        private string _packetId;
        /// <summary> 说明  </summary>
        public string PacketId
        {
            get { return _packetId; }
            set
            {
                _packetId = value;
                RaisePropertyChanged("PacketId");
            }
        }

        private ObservableCollection<TyeBaseSiteEntity> _tyeBaseSiteList = new ObservableCollection<TyeBaseSiteEntity>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TyeBaseSiteEntity> TyeBaseSiteList
        {
            get { return _tyeBaseSiteList; }
            set
            {
                _tyeBaseSiteList = value;
                RaisePropertyChanged("TyeBaseSiteList");
            }
        }

        private ObservableCollection<TyeAdminUserEntity> _tyeAdminUserList = new ObservableCollection<TyeAdminUserEntity>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TyeAdminUserEntity> TyeAdminUserList
        {
            get { return _tyeAdminUserList; }
            set
            {
                _tyeAdminUserList = value;
                RaisePropertyChanged("TyeAdminUserList");
            }
        }



        private ObservableCollection<TyeBasePillarEntity> _tyeBasePillarEntityListn = new ObservableCollection<TyeBasePillarEntity>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TyeBasePillarEntity> TyeBasePillarEntityList
        {
            get { return _tyeBasePillarEntityListn; }
            set
            {
                _tyeBasePillarEntityListn = value;
                RaisePropertyChanged("TyeBasePillarEntityList");
            }
        }



        private ObservableCollection<TaskModel> _taskModelList = new ObservableCollection<TaskModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TaskModel> TaskModelList
        {
            get { return _taskModelList; }
            set
            {
                _taskModelList = value;
                RaisePropertyChanged("TaskCollection");
            }
        }


        private ObservableCollection<TaskModel> _deleteCollection = new ObservableCollection<TaskModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TaskModel> DeleteCollection
        {
            get { return _deleteCollection; }
            set
            {
                _deleteCollection = value;
                RaisePropertyChanged("DeleteCollection");
            }
        }


        private TaskModel _selectItem;
        /// <summary> 说明  </summary>
        public TaskModel SelectItem
        {
            get { return _selectItem; }
            set
            {
                _selectItem = value;
                RaisePropertyChanged("SelectItem");
            }
        }

        private TaskModel _addItem = new TaskModel();
        /// <summary> 说明  </summary>
        public TaskModel AddItem
        {
            get { return _addItem; }
            set
            {
                _addItem = value;
                RaisePropertyChanged("AddItem");
            }
        }

        //public event Action<TaskDivisionViewModel> SaveEvent;

        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();


            Debug.WriteLine(command);


            //  Do：应用
            if (command == "init")
            {
                this.TaskModelList.CollectionChanged += (l, k) =>
              {

                  for (int i = 0; i < this.TaskModelList.Count; i++)
                  {
                      this.TaskModelList[i].SeriaNumber = (i + 1).ToString().PadLeft(2, '0');
                  }
              };

                AddItem.SeletctSameSiteEvent += this.OnSeletctSameSiteEvent;

            }
            //  Do：取消
            else if (command == "btn_add")
            {
                if (this.AddItem == null) return;

                if (this.AddItem.StartSite == null || this.AddItem.EndSite == null 
                    || this.AddItem.Analyst == null)
                {
                    MessageBox.Show("信息不完整，请选择有效信息");
                    return;
                }

                if (this.AddItem.StartSite.SiteName == this.AddItem.EndSite.SiteName)
                {
                    if (this.AddItem.StartPole == null || this.AddItem.EndPole == null)
                    {
                        MessageBox.Show("信息不完整，请选择有效信息");
                        return;
                    }
                }

                AddItem.StartDate = DateTime.Now;

                //AddItem.Progress = 4;

                AddItem.SeletctSameSiteEvent -= this.OnSeletctSameSiteEvent;

                AddItem.Progress = 9.7;
                this.TaskModelList.Add(AddItem);

                AddItem = this.AddItem.Clone();

                AddItem.SeletctSameSiteEvent += this.OnSeletctSameSiteEvent;

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

                this.TaskModelList.Remove(this.SelectItem);

            }
            //  Do：确定提交列表
            else if (command == "btn_sumit")
            {
                if (this.TaskModelList == null) return;


                string err;
                if (!this.IsVaild(out err))
                {
                    var result = MessageBox.Show(err + "是否继续保存？", "提示！", MessageBoxButton.YesNo, MessageBoxImage.Error);

                    if (result == MessageBoxResult.No) return;
                }

                //  Message：触发保存事件
                this.SaveEvent?.Invoke(this.TaskModelList);


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

        public void OnSeletctSameSiteEvent(TyeBaseSiteEntity entity)
        {
            this.SeletctSameSiteEvent?.Invoke(entity);
        }

        /// <summary>
        /// 当前页面是否存在未保存的编辑
        /// </summary>
        /// <returns></returns>
        public bool IsEdit()
        {
            var adds = this.TaskModelList.Where(l => l.EditFlag == 1);

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


            List<TyeBasePillarEntity> rods = this.TyeBasePillarEntityList.ToList();

            #region - 检查区间站 -
            {
                //  ToDo：检查站区是否全部分配
                var startToEnd = this.TaskModelList.Select(l => new Tuple<int, int>(Math.Min(this.TyeBaseSiteList.IndexOf(l.StartSite),
                          this.TyeBaseSiteList.IndexOf(l.EndSite)), Math.Max(this.TyeBaseSiteList.IndexOf(l.StartSite), this.TyeBaseSiteList.IndexOf(l.EndSite)))).OrderBy(l => l.Item1).OrderBy(l => l.Item2).ToList();


                List<TyeBaseSiteEntity> results = new List<TyeBaseSiteEntity>();

                for (int i = 0; i < this.TyeBaseSiteList.Count; i++)
                {
                    if (!startToEnd.Exists(l => l.Item1 <= i && l.Item2 >= i))
                    {
                        results.Add(this.TyeBaseSiteList[i]);
                    }
                }

                if (results != null && results.Count > 0)
                {
                    string param = results.Select(l => l.SiteName).Aggregate((l, k) => l + "," + k);

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
                        value += this.TyeBaseSiteList[l.Item2].SiteName + ",";
                        return k;
                    }
                    else if ((l.Item2 - k.Item1) == 0 && l.Item1 == k.Item2)
                    {
                        value += this.TyeBaseSiteList[l.Item2].SiteName + ",";
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
            var sites = this.TaskModelList.Where(l => l.StartSite.ID == l.EndSite.ID).GroupBy(l => l.StartSite.SiteName);
            {
                foreach (var site in sites)
                {
                    if (!this._pilarCache.ContainsKey(site.Key))
                    {

                        Debug.WriteLine("没有查到指定站区的缓存杆号："+site.Key);
                        continue;
                    }

                    List<TyeBasePillarEntity> sitePoles = this._pilarCache[site.Key].ToList();
                    //  Do：差找杆号的最大值
                    int maxValue = sitePoles.Max(l => int.Parse(l.PoleCode));

                    int minValue = sitePoles.Min(l => int.Parse(l.PoleCode));

                    //  Do：获取当前站的所有任务信息
                    var collection = this.TaskModelList.Where(l => l.StartSite.SiteName == site.Key && l.EndSite.SiteName == site.Key);

                    //  Do：提取当前站的杆号起始和结束杆号信息并从小到大排序
                    var startToEnd = collection.Select(l => new Tuple<int, int>(Math.Min(int.Parse(l.StartPole.PoleCode),
                        int.Parse(l.EndPole.PoleCode)), Math.Max(int.Parse(l.StartPole.PoleCode), int.Parse(l.EndPole.PoleCode)))).OrderBy(l => l.Item1).OrderBy(l => l.Item2).ToList();

                    //int maxValue = startToEnd.Max(l => l.Item2);

                    var allValue = sitePoles.Select(l => int.Parse(l.PoleCode)).ToList();

                    //  Do：遍历每一个杆号，检查是否所有杆号都落在任务杆号区间内部
                    var result = allValue.FindAll(k => !startToEnd.Exists(l => l.Item1 <= k && l.Item2 >= k));

                    if (result != null && result.Count > 0)
                    {
                        string param = result.Select(l => l.ToString()).Aggregate((l, k) => l.ToString() + "," + k.ToString());

                        err = $"[{site.First().StartSite.SiteName}]存在未分配的杆号[{param.Trim(',')}]";
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
                        err = $"[{site.First().StartSite.SiteName}]任务列表中存在重复分配的杆号[{value.Trim(',')}]";
                        return false;
                    }
                }
            }

            #endregion

            return true;
        }
   

        private Dictionary<string, ObservableCollection<TyeBasePillarEntity>> _pilarCache = new Dictionary<string, ObservableCollection<TyeBasePillarEntity>>();
        /// <summary> 说明  </summary>
        public Dictionary<string, ObservableCollection<TyeBasePillarEntity>> PilarCache
        {
            get { return _pilarCache; }
            set
            {
                _pilarCache = value;
                RaisePropertyChanged("PilarCache");
            }
        }
        

        //public event TaskDeleteHandler TaskDeleteEvent;

        //public event TaskListCommitHandler TaskListCommitEvent;

        //public void RefreshConfig(TaskAllocation entity)
        //{
        //    this.PacketId = entity.PacketId;
        //    this.TyeAdminUserList = entity.Stations;

        //    this.TyeBaseSiteList = entity.Analysts;
        //    this.AddItem.Analyst = entity.Analysts.FirstOrDefault();
        //}
    }


    /// <summary>
    /// viewmodel（仅供参考）
    /// </summary>
    public partial class TaskDivisionViewModel : NotifyPropertyChanged, ITaskItemInterface
    {
        ///// <summary>
        ///// 任务集合
        ///// </summary>
        //public ObservableCollection<TaskModel> TaskModelList
        //{
        //    set; get;
        //}
        ///// <summary>
        ///// 站区集合
        ///// </summary>
        //public ObservableCollection<TyeBaseSiteEntity> TyeBaseSiteList
        //{
        //    set; get;
        //}
        ///// <summary>
        ///// 分析员集合
        ///// </summary>
        //public ObservableCollection<TyeAdminUserEntity> TyeAdminUserList
        //{
        //    set; get;
        //}

        ///// <summary>
        ///// 杆号集合
        ///// </summary>
        //public ObservableCollection<TyeAdminUserEntity> TyeBasePillarEntityList
        //{
        //    set; get;
        //}

        public event Action<ObservableCollection<TaskModel>> SaveEvent;

        public event Action<TyeBaseSiteEntity> SeletctSameSiteEvent;

        public void SetTyeAdminUserEntity(ObservableCollection<TyeAdminUserEntity> users)
        {
            this.TyeAdminUserList = users;

            this.AddItem.Analyst = users.FirstOrDefault();
        }

        public void SetTyeBasePillarEntity(ObservableCollection<TyeBasePillarEntity> Pillars)
        {
            if(this.AddItem.StartSite.SiteName!=this.AddItem.EndSite.SiteName)
            {
                Debug.WriteLine("请选择同一个站区再赋值杆号！");
                return;
            }

            this.TyeBasePillarEntityList = Pillars;

            //  Message：添加到缓存列表，该杆号列表用来计算保存检查数据完整性
            string currentSite = this.AddItem.StartSite.SiteName;

            if (_pilarCache.ContainsKey(currentSite))
            {
                _pilarCache[currentSite] = Pillars;
            }
            else
            {
                _pilarCache.Add(currentSite, Pillars);
            }
        }

        public void SetTyeBaseSiteEntity(ObservableCollection<TyeBaseSiteEntity> sites)
        {
            this.TyeBaseSiteList = sites;
        }
    }


}
