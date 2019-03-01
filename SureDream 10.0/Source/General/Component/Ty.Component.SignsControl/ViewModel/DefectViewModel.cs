using CDTY.DataAnalysis.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Ty.Base.WpfBase;

namespace Ty.Component.SignsControl
{
    /// <summary>
    /// 缺陷标定信息页面绑定实体
    /// </summary>
    public partial class DefectViewModel
    {
        #region - 成员属性 -


        private string _pHMCodes;
        /// <summary> PHM编码（基本由界面属性组合而成）  </summary>
        public string PHMCodes
        {
            get { return _pHMCodes; }
            set
            {
                _pHMCodes = value;

                RaisePropertyChanged("PHMCodes");
            }
        }

        private ObservableCollection<string> _codes = new ObservableCollection<string>();
        /// <summary> 说明  </summary>
        public ObservableCollection<string> Codes
        {
            get { return _codes; }
            set
            {
                _codes = value;

                RaisePropertyChanged("Codes");
            }
        }

        //private Func<object, string, bool> _filterMatch;
        ///// <summary> 匹配样本与缺陷检索时支持编码检索、名称检索与拼音检索  </summary>
        //public Func<object, string, bool> FilterMatch
        //{
        //    get { return _filterMatch; }
        //    set
        //    {
        //        _filterMatch = value;
        //        RaisePropertyChanged("FilterMatch");
        //    }
        //}


        #endregion

        /// <summary> 绑定命令 </summary>
        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：保存
            if (command == "btn_sumit")
            {

                //if (this.SelectDefectOrMarkCodes == null)
                //{
                //    MessageBox.Show("请先选择缺陷类别！");

                //    return;
                //}

                this.SumitClick?.Invoke();

            }
            //  Do：取消
            else if (command == "btn_cancel")
            {
                this.CancelClick?.Invoke();

            }
            //  Do：初始化
            else if (command == "init")
            {
                //this.FilterMatch += (l, k) =>
                //  {
                //      //if (this.DefectMenuEntity.CommonHistoricalDefectsOrMark == null) return false;

                //      TyeEncodeDeviceEntity defectCommonUsed = l as TyeEncodeDeviceEntity;

                //      if (defectCommonUsed == null) return false;

                //      if (string.IsNullOrEmpty(k)) return true;

                //      //  Message：匹配字段的规则
                //      Func<string, string, bool> match = (m, n) =>
                //          {
                //              if (string.IsNullOrEmpty(m)) return false;

                //              return m.Contains(n);

                //          };

                //      //  Message：匹配缺陷样本搜索规则
                //      return match(defectCommonUsed.Code, k) || match(defectCommonUsed.Name, k) || match(defectCommonUsed.NamePY, k);

                //  };

                //   //  Message：更新PHMCodes
                //   this.Codes.CollectionChanged += (l, k) =>
                //{
                //    this.PHMCodes = this.Codes.ToList().Aggregate((m, n) => m + " " + n);

                //};

            }

            //  Do：Reset
            else if (command == "btn_clear")
            {
                //this.Reset();
            }


        }

        //void RefreshPHMCode()
        //{
        //    //this.Codes[0] = this.DbType;
        //    //this.Codes[1] = this.SelectDataAcquisitionMode?.Code;
        //    //this.Codes[2] = this.SelectRailwaySsequence?.Code;
        //    //this.Codes[3] = this.SelectDedicatedLine?.LineCode;
        //    //this.Codes[4] = this.SelectDedicatedStation?.SiteCode;
        //    //this.Codes[5] = this.SelectBasicUnit?.PoleMarkCode;

        //    //////  Message：最近使用与历史使用显示面板问题，与PHMCode码生成有关 当历史缺陷类别没有选中项时，PHMCode码后几位用缺陷类别选中项的Code  当历史缺陷类别有选中项时，PHMCode码后面几位用历史缺陷类别选中项的code
        //    ////if (this.SelectCommonHistoricalDefectsOrMark == null)
        //    ////{
        //    //this.Codes[6] = this.SelectDefectOrMarkCodes?.Code;
        //    ////}
        //    ////else
        //    ////{
        //    ////    this.Codes[6] = this.SelectCommonHistoricalDefectsOrMark?.Code;
        //    ////}


        //    this.PHMCodes = this.Codes.ToList().Aggregate((m, n) => m + " " + n);


        //    Debug.WriteLine(this.PHMCodes);

        //}

    }

    partial class DefectViewModel : IDefectSign
    {
        //public event Action<string> ConfirmData;

        public event Action CancelClick;

        public event Action SumitClick;

        private ObservableCollection<DefectCommonUsed> _estimateDefectCommonUseds = new ObservableCollection<DefectCommonUsed>();
        /// <summary> 预估列表  </summary>
        public ObservableCollection<DefectCommonUsed> EstimateDefectCommonUseds
        {
            get { return _estimateDefectCommonUseds; }
            set
            {
                _estimateDefectCommonUseds = value;
                RaisePropertyChanged("EstimateDefectCommonUseds");
            }
        }


        private ObservableCollection<DefectCommonUsed> _recentlyDefectCommonUseds = new ObservableCollection<DefectCommonUsed>();
        /// <summary> 最近使用列表  </summary>
        public ObservableCollection<DefectCommonUsed> RecentlyDefectCommonUseds
        {
            get { return _recentlyDefectCommonUseds; }
            set
            {
                _recentlyDefectCommonUseds = value;
                RaisePropertyChanged("RecentlyDefectCommonUseds");
            }
        }

        private ObservableCollection<DefectCommonUsed> _historyDefectCommonUseds = new ObservableCollection<DefectCommonUsed>();
        /// <summary> 历史使用列表  </summary>
        public ObservableCollection<DefectCommonUsed> HistoryDefectCommonUseds
        {
            get { return _historyDefectCommonUseds; }
            set
            {
                _historyDefectCommonUseds = value;
                RaisePropertyChanged("HistoryDefectCommonUseds");
            }
        }


        private DefectCommonUsed _selectDefectCommonUsed;
        /// <summary> 说明  </summary>
        public DefectCommonUsed SelectDefectCommonUsed
        {
            get { return _selectDefectCommonUsed; }
            set
            {
                //  Do：先清理所有内容再添加
                _selectDefectCommonUsed = null;
                RaisePropertyChanged("SelectDefectCommonUsed");

                _selectDefectCommonUsed = value;
                RaisePropertyChanged("SelectDefectCommonUsed");
            }
        }



        private int _count;
        /// <summary> 说明  </summary>
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                RaisePropertyChanged("Count");
            }
        }


        public void LoadDefectCommonUsed(List<DefectCommonUsed> uses)
        {
            ////  Do：预估
            //this.EstimateDefectCommonUseds.Clear();

            //foreach (var item in uses.Where(l => l.DefectType == 0))
            //{
            //    this.EstimateDefectCommonUseds.Add(item);
            //}

            //  Do：最近
            this.RecentlyDefectCommonUseds.Clear();

            foreach (var item in uses)
            {
                this.RecentlyDefectCommonUseds.Add(item);
            }

            ////  Do：历史
            //this.HistoryDefectCommonUseds.Clear();

            //foreach (var item in uses.Where(l => l.DefectType == 1))
            //{
            //    this.HistoryDefectCommonUseds.Add(item);
            //}
        }

        public void LoadEstimateDefectCommonUseds(List<DefectCommonUsed> uses)
        {
            //  Do：预估
            this.EstimateDefectCommonUseds.Clear();

            foreach (var item in uses)
            {
                this.EstimateDefectCommonUseds.Add(item);
            }

        }


        private ObservableCollection<TyeEncodeDeviceEntity> _tyeEncodeDeviceEntitys = new ObservableCollection<TyeEncodeDeviceEntity>();
        /// <summary> 设备列表  </summary>
        public ObservableCollection<TyeEncodeDeviceEntity> TyeEncodeDeviceEntitys
        {
            get { return _tyeEncodeDeviceEntitys; }
            set
            {
                _tyeEncodeDeviceEntitys = value;
                RaisePropertyChanged("TyeEncodeDeviceEntitys");
            }
        }



        public void LoadTyeEncodeDevice(List<TyeEncodeDeviceEntity> uses)
        {
            this.TyeEncodeDeviceEntitys.Clear();

            foreach (var item in uses)
            {
                this.TyeEncodeDeviceEntitys.Add(item);
            }

            List<TyeEncodeDeviceEntityNode> nodes = new List<TyeEncodeDeviceEntityNode>();

            foreach (var item in uses)
            {
                nodes.Add(new TyeEncodeDeviceEntityNode(item));
            }

            stopwatch.Start();

            this.Nodes = this.Bind(nodes);

            Debug.WriteLine(stopwatch.Elapsed);

            this.RefreshCount();

            Debug.WriteLine(stopwatch.Elapsed);
        }
        Stopwatch stopwatch = new Stopwatch();


        private List<TyeEncodeDeviceEntityNode> _nodes = new List<TyeEncodeDeviceEntityNode>();
        /// <summary> 说明  </summary>
        public List<TyeEncodeDeviceEntityNode> Nodes
        {
            get { return _nodes; }
            set
            {
                _nodes = value;
                RaisePropertyChanged("Nodes");
            }
        }



        /// <summary>
        /// 绑定树
        /// </summary>
        List<TyeEncodeDeviceEntityNode> Bind(List<TyeEncodeDeviceEntityNode> nodes)
        {
            List<TyeEncodeDeviceEntityNode> outputList = new List<TyeEncodeDeviceEntityNode>();

            this.Count = 0;

            for (int i = 0; i < nodes.Count; i++)
            {
                if (string.IsNullOrEmpty(nodes[i].ParentID))
                {
                    outputList.Add(nodes[i]);

                    this.Count++;
                }
                else
                {
                    var result = FindDownward(nodes, nodes[i].ParentID);

                    if (result != null)
                    {
                        nodes[i].Parent = result;

                        result.Nodes.Add(nodes[i]);

                        this.Count++;
                    }
                }
            }



            Debug.WriteLine(stopwatch.Elapsed);

            return outputList;
        }

        /// <summary>
        /// 递归向下查找
        /// </summary>
        TyeEncodeDeviceEntityNode FindDownward(List<TyeEncodeDeviceEntityNode> nodes, string id)
        {
            if (nodes == null) return null;

            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].ID == id)
                {
                    return nodes[i];
                }
                TyeEncodeDeviceEntityNode node = FindDownward(nodes[i].Nodes, id);

                if (node != null)
                {
                    return node;
                }
            }
            return null;
        }



        private string _filterText;
        /// <summary> 过滤信息  </summary>
        public string FilterText
        {
            get { return _filterText; }
            set
            {
                _filterText = value;

                RaisePropertyChanged("FilterText");
            }
        }



        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            //sb.AppendFormat("{0} - {1}" + Environment.NewLine, "PHM编码（基本由界面属性组合而成）", this.PHMCodes);
            //sb.AppendFormat("{0} - {1}" + Environment.NewLine, "缺陷标记信息", this.SelectDefectOrMarkCodes?.Code);
            //sb.AppendFormat("{0} - {1}" + Environment.NewLine, "数据采集方式", this.SelectDataAcquisitionMode?.Name);
            //sb.AppendFormat("{0} - {1}" + Environment.NewLine, "铁路局顺序码", this.SelectRailwaySsequence?.Name);
            //sb.AppendFormat("{0} - {1}" + Environment.NewLine, "供电专业专用线路名称", this.SelectDedicatedLine?.LineName);
            //sb.AppendFormat("{0} - {1}" + Environment.NewLine, "供电专业专用站\\区间 ", this.SelectDedicatedStation?.SiteName);
            //sb.AppendFormat("{0} - {1}" + Environment.NewLine, "基本单元", this.SelectBasicUnit?.PoleMarkCode);
            //sb.AppendFormat("{0} - {1}" + Environment.NewLine, "检测车辆", this.DetectionVehicles);
            //sb.AppendFormat("{0} - {1}" + Environment.NewLine, "责任车间", this.SelectResponsibilityWorkshop?.Name);
            //sb.AppendFormat("{0} - {1}" + Environment.NewLine, "责任工区", this.SelectResponsibilityWorkArea?.Name);

            //sb.AppendFormat("{0} - {1}" + Environment.NewLine, "公里标", this.KmLog);
            //sb.AppendFormat("{0} - {1}" + Environment.NewLine, "检测日期", this.DetectDate);
            //sb.AppendFormat("{0} - {1}" + Environment.NewLine, "当前用户", this.tyeAdminUserEntity?.Name);
            //sb.AppendFormat("{0} - {1}" + Environment.NewLine, "历史信息", this.SelectCommonHistoricalDefectsOrMark?.Name);

            return sb.ToString();
        }


        private ObservableCollection<TyeEncodeDeviceEntityNode> _tyeEncodeDeviceEntityCheck = new ObservableCollection<TyeEncodeDeviceEntityNode>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TyeEncodeDeviceEntityNode> TyeEncodeDeviceEntityCheck
        {
            get { return _tyeEncodeDeviceEntityCheck; }
            set
            {
                _tyeEncodeDeviceEntityCheck = value;
                RaisePropertyChanged("TyeEncodeDeviceEntityCheck");
            }
        }


        private TyeEncodeDeviceEntityNode _selectTyeEncodeDeviceEntityNode;
        /// <summary> 设备过滤选择项  </summary>
        public TyeEncodeDeviceEntityNode SelectTyeEncodeDeviceEntityNode
        {
            get { return _selectTyeEncodeDeviceEntityNode; }
            set
            {
                _selectTyeEncodeDeviceEntityNode = value;

                RaisePropertyChanged("SelectTyeEncodeDeviceEntityNode");

                //  Message：触发筛选
                this.RefreshNodes(this.FilterText, value);
            }
        }


        private TyeEncodeDeviceEntityNode _selectTreeTyeEncodeDeviceEntityNode;
        /// <summary> 设备过滤选择项  </summary>
        public TyeEncodeDeviceEntityNode SelectTreeTyeEncodeDeviceEntityNode
        {
            get { return _selectTreeTyeEncodeDeviceEntityNode; }
            set
            {
                _selectTreeTyeEncodeDeviceEntityNode = value;

                RaisePropertyChanged("SelectTreeTyeEncodeDeviceEntityNode");

                if (this.Codes.Count < 1) return;

                this.Codes[1] = value?.Code;

                this.PHMCodes = this.Codes.ToList().Aggregate((m, n) => m + " " + n);
            }
        }


        public void LoadTyeEncodeCheckDevice(List<TyeEncodeDeviceEntity> uses)
        {
            ObservableCollection<TyeEncodeDeviceEntityNode> result = new ObservableCollection<TyeEncodeDeviceEntityNode>();

            var all = new TyeEncodeDeviceEntityNode(new TyeEncodeDeviceEntity() { Name = "全部" });
            all.IsSelected = true;
            result.Add(all);


            foreach (var item in uses)
            {
                result.Add(new TyeEncodeDeviceEntityNode(item));
            }

            this.TyeEncodeDeviceEntityCheck = result;

            //this.SelectTyeEncodeDeviceEntityNode = all;
        }



        void RefreshCount()
        {
            Action<List<TyeEncodeDeviceEntityNode>> action = null;

            int c = 0;

            action = k =>
            {
                foreach (var item in k)
                {
                    //  Do：匹配则显示
                    if (item.Visibility == Visibility.Visible)
                    {
                        //  Do：前序遍历
                        if (item.Nodes.Count > 0)
                        {
                            action(item.Nodes);
                        }

                        c++;
                    }



                }
            };

            action(this.Nodes);

            this.Count = c;
        }


        //public void RefreshFilter(string text)
        //{

        //    Predicate<string> strMatch = l =>
        //    {
        //        if (l == null) return false;

        //        return l.Contains(text);
        //    };

        //    Predicate<TyeEncodeDeviceEntityNode> match = l =>
        //    {
        //        return strMatch(l.Name) || strMatch(l.NamePY) || strMatch(l.Code);
        //    };

        //    Action<List<TyeEncodeDeviceEntityNode>> action = null;

        //    action = k =>
        //    {
        //        foreach (var item in k)
        //        {
        //            //  Do：前序遍历
        //            if (item.Nodes.Count > 0)
        //            {
        //                action(item.Nodes);
        //            }

        //            //  Do：匹配则显示
        //            if (match(item))
        //            {
        //                item.Visibility = Visibility.Visible;
        //            }
        //            else
        //            {
        //                item.Visibility = Visibility.Collapsed;
        //            }

        //            if (item.Nodes.Count > 0)
        //            {
        //                //  Do：检查是否有匹配
        //                item.Visibility = item.Nodes.Exists(l => l.Visibility == Visibility.Visible) ? Visibility.Visible : Visibility.Collapsed;

        //                if (item.Visibility == Visibility.Visible)
        //                    this.Count++;
        //            }

        //        }
        //    };

        //    action(this.Nodes);

        //    this.RefreshCount();

        //}

        public void RefreshFilter(string text)
        {
            this.RefreshNodes(text, this.SelectTyeEncodeDeviceEntityNode);
        }

        List<TyeEncodeDeviceEntityNode> matchNodes = new List<TyeEncodeDeviceEntityNode>();

        public void RefreshNodes(string text, TyeEncodeDeviceEntityNode selectNode)
        {
            if (selectNode == null) return;

            ////  Message：设置全部按钮关系
            //if (addlist.Count == 1 && addlist.First().Name == "全部")
            //{
            //    foreach (var item in this.TyeEncodeDeviceEntityCheck)
            //    {
            //        item.IsSelected = true;
            //    }

            //}

            //if (removelist.Count == 1 && removelist.First().Name == "全部")
            //{
            //    foreach (var item in this.TyeEncodeDeviceEntityCheck)
            //    {
            //        item.IsSelected = false;
            //    } 
            //}

            //List<TyeEncodeDeviceEntityNode> collection = new List<TyeEncodeDeviceEntityNode>();

            //foreach (var item in this.TyeEncodeDeviceEntityCheck)
            //{
            //    if(item.IsSelected)
            //    {
            //        collection.Add(item);
            //    }
            //}

            matchNodes.Clear();

            Predicate<string> strMatch = l =>
            {
                if (l == null) return false;

                if (string.IsNullOrEmpty(text)) return true;

                return l.Contains(text);
            };

            Predicate<TyeEncodeDeviceEntityNode> match = l =>
            {
                bool result = strMatch(l.Name) || strMatch(l.NamePY) || strMatch(l.Code);

                if (result)
                {
                    matchNodes.Add(l);
                }

                return result;
            };



            Action<List<TyeEncodeDeviceEntityNode>> action = null;

            action = k =>
         {
             foreach (var item in k)
             {
                 //item.Visibility = Visibility.Visible;

                 //  Do：前序遍历
                 if (item.Nodes.Count > 0)
                 {
                     action(item.Nodes);
                 }

                 ////  Do：不匹配不显示
                 //if (!match(item))
                 //{
                 item.Visibility = match(item) ? Visibility.Visible : Visibility.Collapsed;
                 //}

                 //  Do：检查子项是否有显示的
                 if (item.Nodes.Count > 0)
                 {
                     item.Visibility = item.Nodes.Exists(l => l.Visibility == Visibility.Visible) ? Visibility.Visible : Visibility.Collapsed;
                 }

                 ////  Message：只检查第一级别的
                 //if (string.IsNullOrEmpty(item.ParentID))
                 //{
                 //    item.Visibility = selectNode.Name == "全部" || selectNode.Name == item.Name ?
                 //    item.Visibility = Visibility.Visible : item.Visibility = Visibility.Collapsed;
                 //}
                 //else
                 //{
                 //    //  Do：不匹配不显示
                 //    if (!match(item))
                 //    {
                 //        item.Visibility = Visibility.Collapsed;
                 //    }
                 //}

                 ////  Do：检查子项是否有显示的
                 //if (item.Nodes.Count > 0)
                 //{
                 //    item.Visibility = item.Nodes.Exists(l => l.Visibility == Visibility.Visible) ? Visibility.Visible : Visibility.Collapsed;
                 //}

                 ////  Do：在取消列表中的不显示
                 //if (removelist.Exists(l => l.Name == item.Name))
                 //{
                 //    item.Visibility = Visibility.Collapsed;
                 //}
                 //else
                 //{
                 //    //  Do：检查子项是否有显示的
                 //    if (item.Nodes.Count > 0)
                 //    {
                 //        item.Visibility = item.Nodes.Exists(l => l.Visibility == Visibility.Visible) ? Visibility.Visible : Visibility.Collapsed;
                 //    }
                 //}

                 ////  Do：不匹配不显示
                 //if (!match(item))
                 //{
                 //    item.Visibility = Visibility.Collapsed;
                 //}
                 //else
                 //{
                 //    //  Do：在取消列表中的不显示
                 //    if (removelist.Exists(l => l.Name == item.Name))
                 //    {
                 //        item.Visibility = Visibility.Collapsed;
                 //    }

                 //    //  Message：匹配并且新增
                 //    if (addlist.Exists(l => l.Name == item.Name))
                 //    {
                 //        item.Visibility = Visibility.Visible;
                 //    }
                 //}


             }
         };



            foreach (var item in this.Nodes)
            {
                //  Do：先检查第一级别过滤设备节点
                item.Visibility = selectNode.Name == "全部" || selectNode.Name == item.Name ?
                    item.Visibility = Visibility.Visible : item.Visibility = Visibility.Collapsed;

                if (item.Visibility == Visibility.Collapsed) continue;

                item.IsExpanded = selectNode.Name == item.Name;

                //  Do：递归检查下面子节点，只检查是否匹配
                action(item.Nodes);

                //  Do：当前项匹配或者有匹配的子项时显示
                item.Visibility = match(item) || item.Nodes.Exists(l => l.Visibility == Visibility.Visible) ? Visibility.Visible : Visibility.Collapsed;

            }

            //  Do：只有一项匹配
            if (matchNodes.Count == 1)
            {
                //matchNodes.First().IsExpanded = true;

                Action<TyeEncodeDeviceEntityNode> ExpandParent = null;

                ExpandParent = l =>
                 {
                     l.IsExpanded = true;

                     if (l.Parent != null)
                     {
                         ExpandParent(l.Parent);
                     }
                 };

                ExpandParent(matchNodes.First());

                matchNodes.First().IsSelected = true;
            }

            this.RefreshCount();

        }

        public void LoadPHM(string phm)
        {
            this.Codes.Clear();

            this.Codes.Add(phm);

            this.Codes.Add(this.SelectTyeEncodeDeviceEntityNode?.Code);
        }


    }

    partial class DefectViewModel : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public DefectViewModel()
        {
            RelayCommand = new RelayCommand(RelayMethod);
            RelayMethod("init");

        }
        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public partial class TyeEncodeDeviceEntityNode
    {

        public TyeEncodeDeviceEntityNode(TyeEncodeDeviceEntity tyeEncodeDeviceEntity)
        {
            this.TyeEncodeDeviceEntity = tyeEncodeDeviceEntity;

            this.ID = tyeEncodeDeviceEntity.ID;

            this.ParentID = tyeEncodeDeviceEntity.ParentID;

            this.Code = tyeEncodeDeviceEntity.Code;

            this.Name = tyeEncodeDeviceEntity.Name;

            this.NamePY = tyeEncodeDeviceEntity.NamePY;
        }

        public TyeEncodeDeviceEntity TyeEncodeDeviceEntity { get; set; }

        public string ID { get; set; }

        public string Name { get; set; }

        public string NamePY { get; set; }

        public string Code { get; set; }

        public string ParentID { get; set; }


        private bool _isSelected = false;
        /// <summary> 是否选中  </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }


        private Visibility _visibility;
        /// <summary> 说明  </summary>
        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                RaisePropertyChanged("Visibility");
            }
        }



        private bool _isExpanded;
        /// <summary> 是否展开  </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                RaisePropertyChanged("IsExpanded");
            }
        }

        public TyeEncodeDeviceEntityNode Parent { get; set; }

        public List<TyeEncodeDeviceEntityNode> Nodes { get; set; } = new List<TyeEncodeDeviceEntityNode>();

        /// <summary> 绑定命令 </summary>
        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：保存
            if (command == "btn_sumit")
            {



            }
            //  Do：取消
            else if (command == "btn_cancel")
            {


            }
            //  Do：初始化
            else if (command == "init")
            {

            }

            //  Do：Reset
            else if (command == "btn_clear")
            {
                //this.Reset();
            }


        }

    }

    partial class TyeEncodeDeviceEntityNode : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public TyeEncodeDeviceEntityNode()
        {
            RelayCommand = new RelayCommand(RelayMethod);
            RelayMethod("init");

        }
        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

}
