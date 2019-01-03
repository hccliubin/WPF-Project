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
using Ty.Base.WpfBase;

namespace Ty.Component.SignsControl
{
    /// <summary>
    /// 缺陷标定信息页面绑定实体
    /// </summary>
    public partial class DefectViewModel 
    {
        #region - 成员属性 -

        private TyeBaseDepartmentEntity _selectResponsibilityWorkArea;
        /// <summary> 选择的责任工区  </summary>
        public TyeBaseDepartmentEntity SelectResponsibilityWorkArea
        {
            get { return _selectResponsibilityWorkArea; }
            set
            {
                _selectResponsibilityWorkArea = value;
                RaisePropertyChanged("SelectResponsibilityWorkArea");
            }
        }


        private TyeBaseDepartmentEntity _selectResponsibilityWorkshop;
        /// <summary> 选择的责任车间  </summary>
        public TyeBaseDepartmentEntity SelectResponsibilityWorkshop
        {
            get { return _selectResponsibilityWorkshop; }
            set
            {
                _selectResponsibilityWorkshop = value;
                RaisePropertyChanged("SelectResponsibilityWorkshop");
            }
        }


        private TyeBasePillarEntity _selectBasicUnit;
        /// <summary> 选择的单元  </summary>
        public TyeBasePillarEntity SelectBasicUnit
        {
            get { return _selectBasicUnit; }
            set
            {
                _selectBasicUnit = value;
                RaisePropertyChanged("SelectBasicUnit");

                this.RefreshPHMCode();
            }
        }


        private TyeBaseSiteEntity _selectDedicatedStation;
        /// <summary> 选择的站  </summary>
        public TyeBaseSiteEntity SelectDedicatedStation
        {
            get { return _selectDedicatedStation; }
            set
            {
                _selectDedicatedStation = value;
                RaisePropertyChanged("SelectDedicatedStation");

                this.RefreshPHMCode();
            }
        }


        private TyeBaseLineEntity _selectDedicatedLine;
        /// <summary> 选择的段  </summary>
        public TyeBaseLineEntity SelectDedicatedLine
        {
            get { return _selectDedicatedLine; }
            set
            {
                _selectDedicatedLine = value;
                RaisePropertyChanged("SelectDedicatedLine");

                this.RefreshPHMCode();
            }
        }


        private TyeBaseRailwaystationEntity _selectRailwaySsequence;
        /// <summary> 选择的铁路局顺序码  </summary>
        public TyeBaseRailwaystationEntity SelectRailwaySsequence
        {
            get { return _selectRailwaySsequence; }
            set
            {
                _selectRailwaySsequence = value;
                RaisePropertyChanged("SelectRailwaySsequence");

                this.RefreshPHMCode();
            }
        }


        private TyeBaseDatacollecttypeEntity _selectDataAcquisitionMode;
        /// <summary> 选择的数据采集方式  </summary>
        public TyeBaseDatacollecttypeEntity SelectDataAcquisitionMode
        {
            get { return _selectDataAcquisitionMode; }
            set
            {
                _selectDataAcquisitionMode = value;
                RaisePropertyChanged("SelectDataAcquisitionMode");

                this.RefreshPHMCode();
            }
        }

        private TyeEncodeDeviceEntity _selectDefectOrMarkCodes;
        /// <summary> 选择的缺陷  </summary>
        public TyeEncodeDeviceEntity SelectDefectOrMarkCodes
        {
            get { return _selectDefectOrMarkCodes; }
            set
            {
                _selectDefectOrMarkCodes = value;
                RaisePropertyChanged("SelectDefectOrMarkCodes");
            }
        }

        private DefectCommonUsed _selectCommonHistoricalDefectsOrMark;
        /// <summary> 历史数据  </summary>
        public DefectCommonUsed SelectCommonHistoricalDefectsOrMark
        {
            get { return _selectCommonHistoricalDefectsOrMark; }
            set
            {
                _selectCommonHistoricalDefectsOrMark = value;
                RaisePropertyChanged("SelectCommonHistoricalDefectsOrMark");

                this.RefreshPHMCode();
            }
        }



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



        private string _dbType;
        /// <summary> 数据库类型  </summary>
        public string DbType
        {
            get { return _dbType; }
            set
            {
                _dbType = value;
                RaisePropertyChanged("DbType");
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

        /// <summary>
        /// 当前用户
        /// </summary>
        private TyeAdminUserEntity _tyeAdminUserEntity;
        /// <summary> 说明  </summary>
        public TyeAdminUserEntity tyeAdminUserEntity
        {
            get { return _tyeAdminUserEntity; }
            set
            {
                _tyeAdminUserEntity = value;
                RaisePropertyChanged("tyeAdminUserEntity");
            }
        }

        private DateTime? _detectDate;
        /// <summary> 检测日期  </summary>
        public DateTime? DetectDate
        {
            get { return _detectDate; }
            set
            {
                _detectDate = value;
                RaisePropertyChanged("DetectDate");
            }
        }

        private string _kmLog;
        /// <summary> 公里标  </summary>
        public string KmLog
        {
            get { return _kmLog; }
            set
            {
                _kmLog = value;
                RaisePropertyChanged("KmLog");
            }
        }


        private TyeEncodeDeviceEntity _detectionVehicles;
        /// <summary> 检测车辆  </summary>
        public TyeEncodeDeviceEntity DetectionVehicles
        {
            get { return _detectionVehicles; }
            set
            {
                _detectionVehicles = value;
                RaisePropertyChanged("DetectionVehicles");
            }
        }



        private Func<object, string, bool> _filterMatch;
        /// <summary> 匹配样本与缺陷检索时支持编码检索、名称检索与拼音检索  </summary>
        public Func<object, string, bool> FilterMatch
        {
            get { return _filterMatch; }
            set
            {
                _filterMatch = value;
                RaisePropertyChanged("FilterMatch");
            }
        }


        #endregion

        /// <summary> 绑定命令 </summary>
        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：保存
            if (command == "btn_sumit")
            {
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
                this.FilterMatch += (l, k) =>
                  {
                      if (this.DefectMenuEntity.CommonHistoricalDefectsOrMark == null) return false;

                      DefectCommonUsed defectCommonUsed = l as DefectCommonUsed;

                      if (defectCommonUsed == null) return false;

                      //  Message：匹配缺陷样本搜索规则
                      return defectCommonUsed.Code.Contains(k) || defectCommonUsed.Name.Contains(k) || defectCommonUsed.NamePY.Contains(k);

                  };

             //   //  Message：更新PHMCodes
             //   this.Codes.CollectionChanged += (l, k) =>
             //{
             //    this.PHMCodes = this.Codes.ToList().Aggregate((m, n) => m + " " + n);

             //};

            }

            //  Do：Reset
            else if (command == "btn_clear")
            {
                this.Reset();
            }


        }

        void RefreshPHMCode()
        {
            this.Codes[0] = this.DbType;
            this.Codes[1] = this.SelectDataAcquisitionMode?.Code;
            this.Codes[2] = this.SelectRailwaySsequence?.Code;
            this.Codes[3] = this.SelectDedicatedLine?.LineCode;
            this.Codes[4] = this.SelectDedicatedStation?.SiteCode;
            this.Codes[5] = this.SelectBasicUnit?.PoleMarkCode;

            this.Codes[6] = this.SelectCommonHistoricalDefectsOrMark?.Code;

            this.PHMCodes = this.Codes.ToList().Aggregate((m, n) => m + " " + n);
        }

    }

    partial class DefectViewModel : IDefectSign
    {
        public event Action<string> ConfirmData;

        public event Action CancelClick;

        public event Action SumitClick;

        private DefectMenuEntity _defectMenuEntity;
        /// <summary> 传入的参数实体  </summary>
        public DefectMenuEntity DefectMenuEntity
        {
            get { return _defectMenuEntity; }
            set
            {
                _defectMenuEntity = value;
                RaisePropertyChanged("DefectMenuEntity");
            }
        }

        //private ObservableCollection<DefectCommonUsed> _histCollection = new ObservableCollection<DefectCommonUsed>();
        ///// <summary> 最近和历史标定信息  </summary>
        //public ObservableCollection<DefectCommonUsed> HistCollection
        //{
        //    get { return _histCollection; }
        //    set
        //    {
        //        _histCollection = value;
        //        RaisePropertyChanged("HistCollection");
        //    }
        //}


        public void Load(DefectMenuEntity entity)
        {
            DefectMenuEntity = entity;

            //  Do：加载最近使用和历史次数最多的
            //var useCount = entity.CommonHistoricalDefectsOrMark.OrderByDescending(l => l.CountUse).Take(10);

            //this.HistCollection.Clear();

            //foreach (var item in useCount)
            //{
            //    this.HistCollection.Add(item);
            //}

            if(string.IsNullOrEmpty(entity.PHMCodes))
            {
                Debug.WriteLine("PHMCodes格式不正确，请检查：" + entity.PHMCodes);
                return;
            }

            this.DbType = entity.PHMCodes;



            //var count = entity.PHMCodes.Split(' ');

            //  Message：初始化7位Code
            for (int i = 0; i < 7; i++)
            {
                this.Codes.Add(string.Empty);
            }

            //this.PHMCodes = entity.PHMCodes;

        }


        public void Reset()
        {
            foreach (var item in this.GetType().GetProperties())
            {
                //  Message：选择的控件项都以Select开头
                if (item.Name.StartsWith("Select"))
                {
                    item.SetValue(this, null);
                }
            }
        }

        public void SetTabIndex(int index)
        {
            //  ToDo：需要确定具体选中方式
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0} - {1}" + Environment.NewLine, "PHM编码（基本由界面属性组合而成）", this.PHMCodes);
            sb.AppendFormat("{0} - {1}" + Environment.NewLine, "缺陷标记信息", this.SelectDefectOrMarkCodes?.Code);
            sb.AppendFormat("{0} - {1}" + Environment.NewLine, "数据采集方式", this.SelectDataAcquisitionMode?.Name);
            sb.AppendFormat("{0} - {1}" + Environment.NewLine, "铁路局顺序码", this.SelectRailwaySsequence?.Name);
            sb.AppendFormat("{0} - {1}" + Environment.NewLine, "供电专业专用线路名称", this.SelectDedicatedLine?.LineName);
            sb.AppendFormat("{0} - {1}" + Environment.NewLine, "供电专业专用站\\区间 ", this.SelectDedicatedStation?.SiteName);
            sb.AppendFormat("{0} - {1}" + Environment.NewLine, "基本单元", this.SelectBasicUnit?.PoleMarkCode);
            sb.AppendFormat("{0} - {1}" + Environment.NewLine, "检测车辆", this.DetectionVehicles);
            sb.AppendFormat("{0} - {1}" + Environment.NewLine, "责任车间", this.SelectResponsibilityWorkshop?.Name);
            sb.AppendFormat("{0} - {1}" + Environment.NewLine, "责任工区", this.SelectResponsibilityWorkArea?.Name);

            sb.AppendFormat("{0} - {1}" + Environment.NewLine, "公里标", this.KmLog);
            sb.AppendFormat("{0} - {1}" + Environment.NewLine, "检测日期", this.DetectDate);
            sb.AppendFormat("{0} - {1}" + Environment.NewLine, "当前用户", this.tyeAdminUserEntity?.Name);
            sb.AppendFormat("{0} - {1}" + Environment.NewLine, "历史信息", this.SelectCommonHistoricalDefectsOrMark?.Name);

            return sb.ToString();
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

}
