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
    public partial class TaskDivision2CViewModel : NotifyPropertyChanged
    {
        #region - 成员属性 - 
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

        private ObservableCollection<TyeLineEntity> _tyeBLineModelList = new ObservableCollection<TyeLineEntity>();
        /// <summary> 当前的杆号信息列表  </summary>
        public ObservableCollection<TyeLineEntity> TyeLineModelList
        {
            get { return _tyeBLineModelList; }
            set
            {
                _tyeBLineModelList = value;
                RaisePropertyChanged("TyeLineModelList");
            }
        }

        private ObservableCollection<Task2CViewModel> _taskModelList = new ObservableCollection<Task2CViewModel>();
        /// <summary> 当前的任务列表  </summary>
        public ObservableCollection<Task2CViewModel> TaskModelList
        {
            get { return _taskModelList; }
            set
            {
                _taskModelList = value;
                RaisePropertyChanged("TaskModelList");
            }
        }


        private Task2CViewModel _selectItem;
        /// <summary> 选择的任务项  </summary>
        public Task2CViewModel SelectItem
        {
            get { return _selectItem; }
            set
            {
                _selectItem = value;
                RaisePropertyChanged("SelectItem");
            }
        }

        private Task2CViewModel _addItem = new Task2CViewModel();
        /// <summary> 要添加的左侧数据项  </summary>
        public Task2CViewModel AddItem
        {
            get { return _addItem; }
            set
            {
                _addItem = value;
                RaisePropertyChanged("AddItem");
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
            }
            //  Message：刪除
            if (command == "btn_delete")
            {
                this.TaskModelList.Remove(this.SelectItem);
            }
            //  Do：添加
            else if (command == "btn_add")
            {
                if (this.AddItem == null) return;

                if (this.AddItem.StartLine == null || this.AddItem.EndLine == null
                    || this.AddItem.Analyst == null)
                {
                    MessageBox.Show("信息不完整，请选择有效信息");
                    return;
                }

                AddItem.StartDate = DateTime.Now;

                this.TaskModelList.Add(AddItem);

                //  Do：复制一个新数据
                AddItem = this.AddItem.Clone();

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
                    ObservableCollection<TaskModel_2C> models = new ObservableCollection<TaskModel_2C>();

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
            ObservableCollection<Task2CViewModel> collection = new ObservableCollection<Task2CViewModel>();

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

        /// <summary>
        /// 检查当前数据是否完整(检查一：所有站和杆号是否分配，检查二：是否重复分配)
        /// </summary>
        /// <returns></returns>
        public bool IsVaild(out string err)
        {
            err = string.Empty;

            #region - 检查杆号 -

            ////  ToDo：检查数据完整性 同一站台杆号是否都选择完全
            var collection = this.TaskModelList.ToList();

            //foreach (var site in sites)
            //{
            //List<LineModel> sitePoles = this.TyeLineModelList.ToList();
            ////  Do：差找杆号的最大值
            //int maxValue = sitePoles.Max(l => int.Parse(l.ID));

            //int minValue = sitePoles.Min(l => int.Parse(l.ID));

            //  Do：获取当前站的所有任务信息
            //var collection = this.TaskModelList.Where(l => l.StartSite.ID == site.Key && l.EndSite.ID == site.Key);

            //  Do：提取当前站的杆号起始和结束杆号信息并从小到大排序
            var startToEnd = collection.Select(l => new Tuple<int, int>(Math.Min(int.Parse(l.StartLine.ID),
                int.Parse(l.EndLine.ID)), Math.Max(int.Parse(l.StartLine.ID), int.Parse(l.EndLine.ID)))).OrderBy(l => l.Item1).OrderBy(l => l.Item2).ToList();

            //int maxValue = startToEnd.Max(l => l.Item2);

            var allValue = this.TyeLineModelList.ToList();

            //  Do：遍历每一个杆号，检查是否所有杆号都落在任务杆号区间内部
            var result = allValue.FindAll(k => !startToEnd.Exists(l => l.Item1 <= int.Parse(k.ID) && l.Item2 >= int.Parse(k.ID)));

            if (result != null && result.Count > 0)
            {
                string param = result.Select(l => l.Name).Aggregate((l, k) => l + "," + k);

                err = $"存在未分配的段[{param.Trim(',')}]";
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
                err = $"任务列表中存在重复分配的段[{value.Trim(',')}]";
                return false;
            }
            //}


            #endregion

            return true;
        }

        #endregion
    }


    /// <summary>
    /// viewmodel（仅供参考）接口定义部分
    /// </summary>
    public partial class TaskDivision2CViewModel : NotifyPropertyChanged, ITaskItem
    {


        /// <summary> 保存时注册该事件 </summary>
        public event Action<ObservableCollection<TaskModel_2C>> SaveEvent;


        public void SetTaskModelList(ObservableCollection<TaskModel_2C> modelList)
        {
            if (this.TyeAdminUserList == null || this.TyeAdminUserList.Count == 0)
            {
                Debug.WriteLine("请先设置分析员列表");
                return;
            }

            if (this.TyeLineModelList == null || this.TyeLineModelList.Count == 0)
            {
                Debug.WriteLine("请先设置站区列表");
                return;
            }

            this.TaskModelList.Clear();

            foreach (var item in modelList)
            {
                Task2CViewModel vm = new Task2CViewModel();

                vm.TaskID = item.ID.ToString();
                vm.Analyst = this.TyeAdminUserList.ToList().Find(l => l.ID == item.AnalystID.ToString());

                if (vm.Analyst == null)
                {
                    Debug.WriteLine($"没有找到分析员【{item.AnalystID}】的信息，请检查");
                }

                var split = item.Remark.Split(',');

                if (split.Length != 2)
                {
                    Debug.WriteLine($"Remark格式不正确");
                    return;
                }

                string startLine = split[0];
                string endLine = split[1];

                vm.StartLine = this.TyeLineModelList.ToList().Find(l => l.ID == startLine);

                if (vm.StartLine == null)
                {
                    Debug.WriteLine($"没有找到起始站【{item.StartSiteID}】的信息，请检查");
                }
                vm.EndLine = this.TyeLineModelList.ToList().Find(l => l.ID == endLine);

                if (vm.EndLine == null)
                {
                    Debug.WriteLine($"没有找到结束站【{item.EndSiteID}】的杆号信息，请检查");
                }

                vm.TaskTypeEnum = (TaskTypeEnum)item.ProcessType;
                //vm.SeriaNumber = item.SeriaNumber;
                vm.Progress = item.TotalFileCount == 0 ? 0 :double.Parse(item.ProcessedFileCount.ToString())  / double.Parse(item.TotalFileCount.ToString());
                vm.Progress = vm.Progress * 10;
                vm.EndDate = item.TaskEndTime;
                vm.StartDate = item.TaskStartTime;

                this.TaskModelList.Add(vm);
            }
        }

        /// <summary> 设置分析员列表 </summary>
        public void SetTyeAdminUserEntity(ObservableCollection<TyeAdminUserEntity> users)
        {
            this.TyeAdminUserList = users;

            this.AddItem.Analyst = users.FirstOrDefault();
        }

        /// <summary> 设置分析员列表 </summary>
        public void SetTyeLineEntity(ObservableCollection<TyeLineEntity> users)
        {
            this.TyeLineModelList = users;
        }
    }
}
