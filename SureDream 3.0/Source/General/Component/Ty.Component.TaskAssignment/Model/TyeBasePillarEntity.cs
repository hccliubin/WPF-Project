using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ty.Base.WpfBase.Service;

namespace Ty.Component.TaskAssignment 
{
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
}
