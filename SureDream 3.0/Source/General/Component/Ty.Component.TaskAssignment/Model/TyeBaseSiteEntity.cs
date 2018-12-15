using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ty.Base.WpfBase.Service;

namespace Ty.Component.TaskAssignment
{
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
}
