using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControl6C_Right2C
{
    /// <summary>
    /// 任务分工Model
    /// </summary>
    public class TaskAllocation
    {
        /// <summary>
        /// 站区
        /// </summary>
        public ObservableCollection<Station> Stations { get; set; }

        /// <summary>
        /// 分析员
        /// </summary>
        public ObservableCollection<Analyst> Analysts { get; set; }

    }
    public class Station {
        public int ID { get; set; }
       /// <summary>
       /// 站区名称
       /// </summary>
        public string StationName { get; set; }
        /// <summary>
        /// 杆号
        /// </summary>
        public ObservableCollection<Rod> Rods { get; set; }
    }
    public class Rod
    {
        public int ID { get; set; }
        /// <summary>
        /// 杆号名称
        /// </summary>
        public string RodName { get; set; }
    }
    public class Analyst {
        public int ID { get; set; }
        /// <summary>
        /// 分析员名称
        /// </summary>
        public string AnalystName { get; set; }
    }
}
