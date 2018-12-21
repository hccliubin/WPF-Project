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
    /// <summary>
    /// 数据包ViewModel
    /// </summary>
    public partial class TaskDivisionViewModel : NotifyPropertyChanged
    {
        #region - 成员属性 -

        private ObservableCollection<TyeBaseSiteEntity> _tyeBaseSiteList = new ObservableCollection<TyeBaseSiteEntity>();
        /// <summary> 站列表  </summary>
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
        /// <summary> 分析员列表  </summary>
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
        /// <summary> 当前的杆号信息列表  </summary>
        public ObservableCollection<TyeBasePillarEntity> TyeBasePillarEntityList
        {
            get { return _tyeBasePillarEntityListn; }
            set
            {
                _tyeBasePillarEntityListn = value;
                RaisePropertyChanged("TyeBasePillarEntityList");
            }
        }

        private ObservableCollection<TaskViewModel> _taskModelList = new ObservableCollection<TaskViewModel>();
        /// <summary> 当前的任务列表  </summary>
        public ObservableCollection<TaskViewModel> TaskModelList
        {
            get { return _taskModelList; }
            set
            {
                _taskModelList = value;
                RaisePropertyChanged("TaskModelList");
            }
        }

        private ObservableCollection<TaskViewModel> _deleteCollection = new ObservableCollection<TaskViewModel>();
        /// <summary> 删除的任务列表 目前没用到  </summary>
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
        /// <summary> 选择的任务项  </summary>
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
        /// <summary> 要添加的左侧数据项  </summary>
        public TaskViewModel AddItem
        {
            get { return _addItem; }
            set
            {
                _addItem = value;
                RaisePropertyChanged("AddItem");
            }
        }

        private Dictionary<string, ObservableCollection<TyeBasePillarEntity>> _pilarCache = new Dictionary<string, ObservableCollection<TyeBasePillarEntity>>();
        /// <summary> 存储杆号历史加载的数据  </summary>
        public Dictionary<string, ObservableCollection<TyeBasePillarEntity>> PilarCache
        {
            get { return _pilarCache; }
            set
            {
                _pilarCache = value;
                RaisePropertyChanged("PilarCache");
            }
        }


        private bool _isBuzy = false;
        /// <summary> 等待框是否显示  </summary>
        public bool IsBuzy
        {
            get { return _isBuzy; }
            set
            {
                _isBuzy = value;
                RaisePropertyChanged("IsBuzy");
            }
        }

        #endregion

        #region - 成员方法 -

        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();


            Debug.WriteLine(command);


            //  Do：初始化
            if (command == "init")
            {
                this.TaskModelList.CollectionChanged += (l, k) =>
                {

                    for (int i = 0; i < this.TaskModelList.Count; i++)
                    {
                        this.TaskModelList[i].SeriaNumber = (i + 1).ToString().PadLeft(2, '0');
                    }
                };
                //  Do：注册选择相同站区事件
                AddItem.SeletctSameSiteEvent += this.OnSeletctSameSiteEvent;

                //  this.TaskModelList.CollectionChanged += (l, k) =>
                //{
                //    ObservableCollection<TaskViewModel> collection = new ObservableCollection<TaskViewModel>();

                //    foreach (var item in this.TaskModelList)
                //    {
                //        collection.Add(item);
                //    }

                //    this.TaskModelList = collection;

                //};


            }
            //  Do：添加
            else if (command == "btn_add")
            {
                if (this.AddItem == null) return;

                if (this.AddItem.StartSite == null || this.AddItem.EndSite == null
                    || this.AddItem.Analyst == null)
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

                //  Do：注销选择相同站区事件
                AddItem.SeletctSameSiteEvent -= this.OnSeletctSameSiteEvent;

                this.TaskModelList.Add(AddItem);

                //  Do：复制一个新数据
                AddItem = this.AddItem.Clone();

                //  Message：刷新可选杆号
                this.RefreshCanSelection();

                //  Do：注册选择相同站区事件
                AddItem.SeletctSameSiteEvent += this.OnSeletctSameSiteEvent;

            }
            //  Do：删除
            else if (command == "btn_delete")
            {
                if (this.SelectItem == null) return;

                if (this.SelectItem.EditFlag == 0)
                {
                    //  Do：修改和历史数据则标识为删除，当确认时触发删除事件
                    this.DeleteCollection.Add(this.SelectItem);

                }

                this.TaskModelList.Remove(this.SelectItem);

                //  Message：刷新可选杆号
                this.RefreshCanSelection();

            }
            //  Do：保存
            else if (command == "btn_sumit")
            {
                if (this.TaskModelList == null || this.TaskModelList.Count == 0)
                {
                    MessageWindow.ShowDialogWithSumit("列表中没有数据，请先分工");
                    return;
                }


                string err;

                if (!this.IsVaild(out err))
                {
                    //var result = MessageBox.Show(err + "是否继续保存？", "提示！", MessageBoxButton.YesNo, MessageBoxImage.Error);

                    //err = "是否继续保存是否继续保存是否继续保存是否继续保存是否继续保存是否继续保存是否继续保存是否继续保存是否继续保存";
                    if (err.Length > 40)
                    {
                        err = err.Substring(0, 40) + "...";
                    }

                    var result = MessageWindow.ShowDialog(err + "是否继续保存？");

                    //if (result == MessageBoxResult.No) return;

                    if (!result) return;
                }

                this.IsBuzy = true;


                Task.Run(() =>
                {
                    ObservableCollection<TaskModel> models = new ObservableCollection<TaskModel>();

                    foreach (var item in this.TaskModelList)
                    {
                        models.Add(item.ConvertTo());
                    }
                    //  Message：触发保存事件
                    this.SaveEvent?.Invoke(models);
                    this.IsBuzy = false;
                });

            }

        }

        void RefreshCanSelection()
        {
            ObservableCollection<TaskViewModel> collection = new ObservableCollection<TaskViewModel>();

            foreach (var item in this.TaskModelList)
            {
                collection.Add(item);
            }

            this.TaskModelList = collection;

            //  Message：刷新序号
            this.TaskModelList.CollectionChanged += (l, k) =>
            {

                for (int i = 0; i < this.TaskModelList.Count; i++)
                {
                    this.TaskModelList[i].SeriaNumber = (i + 1).ToString().PadLeft(2, '0');
                }
            };
        }

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
            var sites = this.TaskModelList.Where(l => l.StartSite.ID == l.EndSite.ID).GroupBy(l => l.StartSite.ID);
            {
                foreach (var site in sites)
                {
                    if (!this._pilarCache.ContainsKey(site.Key))
                    {

                        Debug.WriteLine("没有查到指定站区的缓存杆号：" + site.Key);
                        continue;
                    }

                    List<TyeBasePillarEntity> sitePoles = this._pilarCache[site.Key].ToList();
                    //  Do：差找杆号的最大值
                    int maxValue = sitePoles.Max(l => int.Parse(l.PoleCode));

                    int minValue = sitePoles.Min(l => int.Parse(l.PoleCode));

                    //  Do：获取当前站的所有任务信息
                    var collection = this.TaskModelList.Where(l => l.StartSite.ID == site.Key && l.EndSite.ID == site.Key);

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

        #endregion
    }


    /// <summary>
    /// viewmodel（仅供参考）接口定义部分
    /// </summary>
    public partial class TaskDivisionViewModel : NotifyPropertyChanged, ITaskItem
    {


        /// <summary> 保存时注册该事件 </summary>
        public event Action<ObservableCollection<TaskModel>> SaveEvent;

        /// <summary> 选择相同站时注册该事件 </summary>
        public event Action<TyeBaseSiteEntity> SeletctSameSiteEvent;


        public void SetTaskModelList(ObservableCollection<TaskModel> modelList)
        {
            if (this.TyeAdminUserList == null || this.TyeAdminUserList.Count == 0)
            {
                Debug.WriteLine("请先设置分析员列表");
                return;
            }
            if (this.TyeBaseSiteList == null || this.TyeBaseSiteList.Count == 0)
            {
                Debug.WriteLine("请先设置站区列表");
                return;
            }

            var sameStation = modelList.Where(l => l.StartSiteID == l.EndSiteID);

            foreach (var item in sameStation)
            {
                ObservableCollection<TyeBasePillarEntity> observable = new ObservableCollection<TyeBasePillarEntity>();

                if (item.Pillars == null)
                {
                    Debug.WriteLine("请先设置站区杆号列表" + item.ID);
                    continue;
                }

                foreach (var item1 in item.Pillars)
                {
                    observable.Add(item1);
                }

                //  Message：设置杆号列表
                this.SetTyeBasePillarEntity(observable);
            }

            this.TaskModelList.Clear();

            foreach (var item in modelList)
            {
                TaskViewModel vm = new TaskViewModel();
                vm.TaskID = item.ID.ToString();
                vm.Analyst = this.TyeAdminUserList.ToList().Find(l => l.ID == item.AnalystID.ToString());

                if (vm.Analyst == null)
                {
                    Debug.WriteLine($"没有找到分析员【{item.AnalystID}】的信息，请检查");
                }

                vm.StartSite = this.TyeBaseSiteList.ToList().Find(l => l.ID == item.StartSiteID);
                if (vm.StartSite == null)
                {
                    Debug.WriteLine($"没有找到起始站【{item.StartSiteID}】的信息，请检查");
                }
                vm.EndSite = this.TyeBaseSiteList.ToList().Find(l => l.ID == item.EndSiteID);

                if (vm.EndSite == null)
                {
                    Debug.WriteLine($"没有找到结束站【{item.EndSiteID}】的杆号信息，请检查");
                }

                vm.TaskTypeEnum = (TaskTypeEnum)item.ProcessType;
                //vm.SeriaNumber = item.SeriaNumber;
                vm.Progress = item.TotalFileCount == 0 ? 0 : item.ProcessedFileCount / item.TotalFileCount;
                vm.EndDate = item.TaskEndTime;
                vm.StartDate = item.TaskStartTime;

                //  Do：如果是相同站任务
                if (item.StartSiteID == item.EndSiteID)
                {
                    if (!this.PilarCache.ContainsKey(item.StartSiteID))
                    {
                        Debug.WriteLine($"没有找到站【{item.ID}】的杆号列表信息，请检查");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(item.StartPoleID))
                        {
                            vm.StartPole = this.PilarCache[item.StartSiteID].ToList().Find(l => l.ID == item.StartPoleID);

                            if (vm.StartPole == null)
                            {
                                Debug.WriteLine($"没有找到起始杆号【{item.StartPoleID}】的信息，请检查");
                            }
                        }

                        if (!string.IsNullOrEmpty(item.EndPoleID))
                        {
                            vm.EndPole = this.PilarCache[item.StartSiteID].ToList().Find(l => l.ID == item.EndPoleID);

                            if (vm.EndPole == null)
                            {
                                Debug.WriteLine($"没有找到起始杆号【{item.EndPoleID}】的信息，请检查");
                            }
                        }
                    }
                }

                this.TaskModelList.Add(vm);
            }
        }

        /// <summary> 设置分析员列表 </summary>
        public void SetTyeAdminUserEntity(ObservableCollection<TyeAdminUserEntity> users)
        {
            this.TyeAdminUserList = users;

            this.AddItem.Analyst = users.FirstOrDefault();
        }

        /// <summary> 设置杆号 当站选择相同时 历史站区缓存到内部列表中 </summary>
        public void SetTyeBasePillarEntity(ObservableCollection<TyeBasePillarEntity> Pillars)
        {
            if (Pillars == null || Pillars.Count == 0)
            {
                this.TyeBasePillarEntityList = Pillars;
                return;
            }
            //  Message：添加到缓存列表，该杆号列表用来计算保存检查数据完整性
            string currentSite = Pillars.First().SiteID;

            if (_pilarCache.ContainsKey(currentSite))
            {
                //  Message：更新
                _pilarCache[currentSite] = Pillars;
            }
            else
            {
                //  Message：添加
                _pilarCache.Add(currentSite, Pillars);
            }

            this.TyeBasePillarEntityList = Pillars;

            this.AddItem.StartPole = this.TyeBasePillarEntityList.FirstOrDefault();
            this.AddItem.EndPole = this.TyeBasePillarEntityList.LastOrDefault();

        }

        /// <summary> 设置站信息 </summary>
        public void SetTyeBaseSiteEntity(ObservableCollection<TyeBaseSiteEntity> sites)
        {
            this.TyeBaseSiteList = sites;
        }
    }
}
