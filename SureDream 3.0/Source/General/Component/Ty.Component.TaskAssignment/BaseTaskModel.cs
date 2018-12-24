﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ty.Component.TaskAssignment
{
   public class BaseTaskModel
    {
        public string EndPoleID { get; set; }
        public string StartPoleID { get; set; }
        public string EndSiteID { get; set; }
        public string StartSiteID { get; set; }
        public string LineID { get; set; }
        public int IsAbandoned { get; set; }
        public DateTime? CompleteTime { get; set; }
        public int TotalFileCount { get; set; }
        public int ProcessedFileCount { get; set; }
        public DateTime? TaskEndTime { get; set; }
        public DateTime? TaskStartTime { get; set; }
        public string LeaderID { get; set; }
        public string AnalystID { get; set; }
        public int ProcessType { get; set; }
        public string TaskType { get; set; }
        public string TaskName { get; set; }
        public string ID { get; set; }
        public string LineDirection { get; set; }
        public string OriginDataPatchID { get; set; }
    }
}
