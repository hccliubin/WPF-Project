using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ty.Component.TaskManager
{
    /// <summary>
    /// 分配任务实体
    /// </summary>
    public class Task
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        public string TaskID;

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName;

        /// <summary>
        /// 分析员ID
        /// </summary>
        public string AnalystID;

        /// <summary>
        /// 分析员名称
        /// </summary>
        public string AnalystName;

        /// <summary>
        /// 起始站区ID
        /// </summary>
        public string StartSiteID;

        /// <summary>
        /// 起始站区名称
        /// </summary>
        public string StartSiteName;

        /// <summary>
        /// 结束站区ID
        /// </summary>
        public string EndSiteID;

        /// <summary>
        /// 结束站区名称
        /// </summary>
        public string EndSiteName;

        /// <summary>
        /// 任务开始时间
        /// </summary>
        public DateTime StartDate;

        /// <summary>
        /// 任务截止时间
        /// </summary>
        public DateTime EndDate;

        /// <summary>
        /// 任务当前完成进度
        /// </summary>
        public double Progress;
    }


}
