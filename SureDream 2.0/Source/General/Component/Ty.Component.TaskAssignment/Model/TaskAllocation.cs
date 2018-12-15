using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ty.Base.WpfBase.Service;

namespace Ty.Component.TaskAssignment
{
    ///// <summary>
    ///// 任务分工完成后返回该模型集合
    ///// </summary>
    //public class TaskModel : NotifyPropertyChanged
    //{
    //    /// <summary>
    //    /// 任务类型
    //    /// </summary>
    //    public TaskTypeEnum TaskTypeEnum
    //    {
    //        set; get;
    //    }
    //    /// <summary>
    //    /// 序号
    //    /// </summary>
    //    public int SeriaNumber
    //    {
    //        set; get;
    //    }

    //    private string _taskID = Guid.NewGuid().ToString();
    //    /// <summary>
    //    /// 任务ID
    //    /// </summary>
    //    public string TaskID
    //    {
    //        get { return _taskID; }
    //        set
    //        {
    //            _taskID = value;
    //            RaisePropertyChanged("TaskID");
    //        }
    //    }

    //    private string _analystId;
    //    /// <summary>
    //    /// 分析员Id
    //    /// </summary>
    //    public string AnalystId
    //    {
    //        get { return _analystId; }
    //        set
    //        {
    //            _analystId = value;
    //            RaisePropertyChanged("AnalystId");
    //        }
    //    }

    //    private string _startSiteId;
    //    /// <summary>
    //    /// 开始站Id（如果是跨站显示所有杆号）
    //    /// </summary>
    //    public string StartSiteId
    //    {
    //        get { return _startSiteId; }
    //        set
    //        {
    //            _startSiteId = value;
    //            RaisePropertyChanged("StartSiteId");
    //        }
    //    }

    //    private string _endSiteId;
    //    /// <summary>
    //    /// 结束站Id（如果是跨站显示所有杆号）
    //    /// </summary>
    //    public string EndSiteId
    //    {
    //        get { return _endSiteId; }
    //        set
    //        {
    //            _endSiteId = value;
    //            RaisePropertyChanged("EndSiteId");
    //        }
    //    }

    //    private DateTime _startDate = DateTime.Now;
    //    /// <summary>
    //    /// 开始时间（默认创建该任务时间）
    //    /// </summary>
    //    public DateTime StartDate
    //    {
    //        get { return _startDate; }
    //        set
    //        {
    //            _startDate = value;
    //            RaisePropertyChanged("StartDate");
    //        }
    //    }

    //    private DateTime _endDate = DateTime.Now;
    //    /// <summary>
    //    /// 任务截止时间
    //    /// </summary>
    //    public DateTime EndDate
    //    {
    //        get { return _endDate; }
    //        set
    //        {
    //            _endDate = value;
    //            RaisePropertyChanged("EndDate");
    //        }
    //    }

    //    private string _startPoleId;
    //    /// <summary>
    //    /// 开始杆号
    //    /// </summary>
    //    public string StartPoleId
    //    {
    //        get { return _startPoleId; }
    //        set
    //        {
    //            StartPoleId = value;
    //            RaisePropertyChanged("StartPoleId");
    //        }
    //    }

    //    private string _endPoleId;
    //    /// <summary>
    //    /// 结束杆号
    //    /// </summary>
    //    public string EndPoleId
    //    {
    //        get { return _endPoleId; }
    //        set
    //        {
    //            _endPoleId = value;
    //            RaisePropertyChanged("EndPoleId");
    //        }
    //    }

    //    private double _progress;
    //    /// <summary>
    //    /// 任务进度
    //    /// </summary>
    //    public double Progress
    //    {
    //        get { return _progress; }
    //        set
    //        {
    //            _progress = value;
    //            RaisePropertyChanged("Progress");
    //        }
    //    }
    //}
    /// <summary>
    /// 分析员模型
    /// </summary>
    public class TyeAdminUserEntity : NotifyPropertyChanged
    {
        public TyeAdminUserEntity()
        {

        }

        public string ID { get; set; }
        public string LeaderID { get; set; }
        public string Name { get; set; }
        public string PassWord { get; set; }
        public string EngName { get; set; }
        public string Sector { get; set; }
        public DateTime? CreateTime { get; set; }
        public byte[] HeadShot { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string OtherInfo { get; set; }
    }
    /// <summary>
    /// 站模型
    /// </summary>
    public class TyeBaseSiteEntity : NotifyPropertyChanged
    {
        public TyeBaseSiteEntity() { }

        public int IsRolledBack { get; set; }
        public DateTime? PublishTime { get; set; }
        public string Publisher { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Creater { get; set; }
        public string EndDeviceID { get; set; }
        public string StartDeviceID { get; set; }
        public long EndKMLogo { get; set; }
        public long StartKMLogo { get; set; }
        public decimal EndLatitude { get; set; }
        public decimal EndLongitude { get; set; }
        public decimal StartLatitude { get; set; }
        public decimal StartLongitude { get; set; }
        public string Direction { get; set; }
        public string SiteName { get; set; }
        public string SiteCode { get; set; }
        public string ID { get; set; }
        public int VERSION { get; set; }
        public DateTime? VERSIONINUSETIME { get; set; }
    }
    /// <summary>
    /// 杆号模型
    /// </summary>
    public class TyeBasePillarEntity : NotifyPropertyChanged
    {
        public TyeBasePillarEntity() { }

        public string ID { get; set; }
        public string PoleCode { get; set; }
        public string KMLogo { get; set; }
        public int IsDelete { get; set; }
        public string SiteID { get; set; }
        public string Creater { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Publisher { get; set; }
        public DateTime? PublishTime { get; set; }
        public int IsRolledBack { get; set; }
        public int Version { get; set; }
        public DateTime? VersionInUseTime { get; set; }
    }
    /// <summary>
    /// 任务类型
    /// </summary>
    public enum TaskTypeEnum
    {
        /// <summary>
        /// 检测任务
        /// </summary>
        DetectionTask = 1,
        /// <summary>
        /// 样本标定任务
        /// </summary>
        SamplesCalibrationTask = 2
    }

    ///// <summary>
    ///// 任务分工Model
    ///// </summary>
    //public class TaskAllocation
    //{
    //    /// <summary>
    //    /// 数据包ID
    //    /// </summary>
    //    public string PacketId { get; set; }
    //    /// <summary>
    //    /// 站区
    //    /// </summary>
    //    public ObservableCollection<Station> Stations { get; set; }

    //    /// <summary>
    //    /// 分析员
    //    /// </summary>
    //    public ObservableCollection<Analyst> Analysts { get; set; }

    //}
    //public class Station {
    //    public int ID { get; set; }
    //   /// <summary>
    //   /// 站区名称
    //   /// </summary>
    //    public string StationName { get; set; }
    //    /// <summary>
    //    /// 杆号
    //    /// </summary>
    //    public ObservableCollection<Rod> Rods { get; set; }
    //}
    //public class Rod
    //{
    //    public int ID { get; set; }
    //    /// <summary>
    //    /// 杆号名称
    //    /// </summary>
    //    public string RodName { get; set; }
    //}
    //public class Analyst {
    //    public int ID { get; set; }
    //    /// <summary>
    //    /// 分析员名称
    //    /// </summary>
    //    public string AnalystName { get; set; }
    //}
}
