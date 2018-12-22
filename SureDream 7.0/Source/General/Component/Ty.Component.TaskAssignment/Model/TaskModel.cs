using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ty.Component.TaskAssignment
{
    /// <summary>
    /// 分配任务表实体类
    /// <summary>
    public class TaskModel
    {
        private string _id;
        /// <summary>
        /// 任务Id，数据库自增
        /// <summary>
        public string ID { get => _id; set { _id = value; } }

        private string _taskname;
        /// <summary>
        /// 任务名称
        /// <summary>
        public string TaskName { get => _taskname; set { _taskname = value; } }

        private string _tasktype;
        /// <summary>
        /// 任务类型：1C/2C/4C
        /// <summary>
        public string TaskType { get => _tasktype; set { _tasktype = value; } }

        private int _processtype;
        /// <summary>
        /// 1:检测任务  2:标定任务（目前只存在检测任务）
        /// <summary>
        public int ProcessType { get => _processtype; set { _processtype = value; } }

        private string _analystid;
        /// <summary>
        /// 分析员用户ID
        /// <summary>
        public string AnalystID { get => _analystid; set { _analystid = value; } }

        private string _leaderid;
        /// <summary>
        /// 组长用户ID
        /// <summary>
        public string LeaderID { get => _leaderid; set { _leaderid = value; } }

        private DateTime? _taskstarttime;
        /// <summary>
        /// 任务指定开始时间，默认为当前时间
        /// <summary>
        public DateTime? TaskStartTime { get => _taskstarttime; set { _taskstarttime = value; } }

        private DateTime? _taskendtime;
        /// <summary>
        /// 任务指定完成时间
        /// <summary>
        public DateTime? TaskEndTime { get => _taskendtime; set { _taskendtime = value; } }

        private int _processedfilecount;
        /// <summary>
        /// 用户已经查看的文件数，数据库提供
        /// <summary>
        public int ProcessedFileCount { get => _processedfilecount; set { _processedfilecount = value; } }

        private int _totalfilecount;
        /// <summary>
        /// 用户任务中包含的文件总数，数据库提供
        /// <summary>
        public int TotalFileCount { get => _totalfilecount; set { _totalfilecount = value; } }

        private DateTime? _completetime;
        /// <summary>
        /// 
        /// <summary>
        public DateTime? CompleteTime { get => _completetime; set { _completetime = value; } }

        private int _isabandoned;
        /// <summary>
        /// 是否放弃了本次任务
        /// <summary>
        public int IsAbandoned { get => _isabandoned; set { _isabandoned = value; } }

        private string _lineid;
        /// <summary>
        /// 线路ID
        /// <summary>
        public string LineID { get => _lineid; set { _lineid = value; } }

        private string _startsiteid;
        /// <summary>
        /// 起始站区ID，可缺省
        /// <summary>
        public string StartSiteID { get => _startsiteid; set { _startsiteid = value; } }

        private string _endsiteid;
        /// <summary>
        /// 结束站区ID，可缺省
        /// <summary>
        public string EndSiteID { get => _endsiteid; set { _endsiteid = value; } }

        private string _startpoleid;
        /// <summary>
        /// 起始杆号，可缺省
        /// <summary>
        public string StartPoleID { get => _startpoleid; set { _startpoleid = value; } }

        private string _endpoleid;
        /// <summary>
        /// 结束杆号，可缺省
        /// <summary>
        public string EndPoleID { get => _endpoleid; set { _endpoleid = value; } }

        private string _linedirection;
        /// <summary>
        /// 线路方向：上行/下行
        /// <summary>
        public string LineDirection { get => _linedirection; set { _linedirection = value; } }

        private string _origindatapatchid;
        /// <summary>
        /// 上传文件包Id
        /// <summary>
        public string OriginDataPatchID { get => _origindatapatchid; set { _origindatapatchid = value; } }

        private string _remark;
        /// <summary>
        /// 备注，新加字段，用来存储选择端的范围(第一段-第二段)
        /// <summary>
        public string Remark { get => _remark; set { _remark = value; } }
    }


}
