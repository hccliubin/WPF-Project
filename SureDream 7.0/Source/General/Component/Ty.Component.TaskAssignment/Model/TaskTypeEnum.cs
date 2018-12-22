using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ty.Component.TaskAssignment 
{
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
}
