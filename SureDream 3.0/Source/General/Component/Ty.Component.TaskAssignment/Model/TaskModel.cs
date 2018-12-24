using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ty.Component.TaskAssignment
{
    public class TaskModel_4C : BaseTaskModel
    {
        /// <summary>
        /// 站相同时用到的杆号列表
        /// </summary> 
        public List<TyeBasePillarEntity> Pillars { get; set; }

    }

    public class TaskModel_2C : BaseTaskModel
    {
        /// <summary>
        /// 备注，新加字段，用来存储选择端的范围(第一段-第二段)
        /// <summary>
        public string Remark { get; set; }

    }
}
