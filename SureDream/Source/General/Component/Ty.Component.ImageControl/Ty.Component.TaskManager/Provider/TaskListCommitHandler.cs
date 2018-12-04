﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ty.Component.TaskManager
{

    /// <summary>
    /// 提交任务委托
    /// </summary>
    /// <param name="taskList"></param>
    public delegate void TaskListCommitHandler(List<Task> taskList);

}