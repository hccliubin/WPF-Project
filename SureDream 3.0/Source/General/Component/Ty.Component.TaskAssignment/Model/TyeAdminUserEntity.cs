using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ty.Base.WpfBase.Service;

namespace Ty.Component.TaskAssignment
{
    /// <summary>
    /// 分析员模型
    /// </summary>
    public class TyeAdminUserEntity : NotifyPropertyChanged
    {
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
}
