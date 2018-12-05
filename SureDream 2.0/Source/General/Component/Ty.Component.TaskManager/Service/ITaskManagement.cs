using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ty.Component.TaskManager
{
    /// <summary>
    /// 任务分配与管理模块接口
    /// </summary>
    public interface ITaskManagement
    {
        /// <summary>
        /// 加载待分配的原始数据包信息
        /// </summary>
        /// <param name="rawTaskID">原始数据包ID</param>
        void LoadRawTask(string rawTaskID);

        /// <summary>
        /// 加载站区
        /// </summary>
        /// <param name="siteList"></param>
        void LoadSites(List<Site> siteList);

        /// <summary>
        /// 加载分析员
        /// </summary>
        /// <param name="analystList"></param>
        void LoadAnalyst(List<Analyst> analystList);

        /// <summary>
        /// 删除任务事件
        /// </summary>
        event TaskDeleteHandler TaskDeleteEvent;

        /// <summary>
        /// 提交任务列表事件
        /// </summary>
        event TaskListCommitHandler TaskListCommitEvent;

        /// <summary>
        /// 修改任务
        /// </summary>
        /// <param name="task">更新后的任务</param>
        void AlterTask(Task task);

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="taskID">任务ID</param>
        void DeleteTask(string taskID);

        /// <summary>
        /// 获取分析员当前正在处理的任务(未完成任务）列表
        /// </summary>
        /// <param name="analystID">分析员ID</param>
        /// <returns>分析员正在处理的任务列表</returns>
        List<Task> GetAnalystProcedingTask(string analystID);

        /// <summary>
        /// 获取分析员某段时间内所有任务
        /// </summary>
        /// <param name="analystID">分析员ID</param>
        /// <param name="fromDate">查询开始日期</param>
        /// <param name="toDate">查询结束日期</param>
        /// <returns>分析员指定时间范围内收到的任务</returns>
        List<Task> GetAnalystHistoryTask(string analystID, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// 手动设置任务已完成
        /// </summary>
        /// <param name="taskID">任务ID</param>
        /// <returns>操作是否成功</returns>
        bool SetTaskDone(string taskID);

        /// <summary>
        /// 获取某个原始数据包总体处理进度
        /// </summary>
        /// <param name="rawTaskID">原始数据包ID</param>
        /// <returns>进度（%）</returns>
        double GetTaskProgress(string rawTaskID);

        /// <summary>
        /// 获取某个分析员的某个任务处理进度
        /// </summary>
        /// <param name="taskID">任务ID</param>
        /// <param name="analystID">分析员ID</param>
        /// <returns>进度</returns>
        double GetAnalystTaskProgress(string taskID, string analystID);

        /// <summary>
        /// 分析员对某次跑车试验数据总体分析工作统计
        /// </summary>
        /// <param name="analystID">分析员ID</param>
        /// <param name="rawTaskID">原始数据包ID</param>
        /// <returns>统计结果报表内容</returns>
        string GetAnalystWorkStat(string analystID, string rawTaskID);

        /// <summary>
        /// 获取分析员在某段时间内工作内容统计结果
        /// </summary>
        /// <param name="analystID">分析员ID</param>
        /// <param name="starttime">开始统计时间</param>
        /// <param name="endtime">结束统计时间</param>
        /// <returns>统计结果报表内容</returns>
        string GetAnalystWorkStat(string analystID, DateTime starttime, DateTime endtime);

        /// <summary>
        /// 获取某次跑车数据分析得到的一级缺陷统计结果
        /// </summary>
        /// <param name="rawTaskID">原始数据包ID</param>
        /// <returns>统计结果报表内容</returns>
        string GetLevelOneDefectStat(string rawTaskID);

        /// <summary>
        /// 获取某次跑车数据分析得到的二级缺陷统计结果
        /// </summary>
        /// <param name="rawTaskID">原始数据包ID</param>
        /// <returns>统计结果报表内容</returns>
        string GetLevelTwoDefectStat(string rawTaskID);

        /// <summary>
        /// 获取某次跑车数据分析得到的缺陷统计结果
        /// </summary>
        /// <param name="rawTaskID">原始数据包ID</param>
        /// <returns>统计结果报表内容</returns>
        string GetDefectStat(string rawTaskID);

        /// <summary>
        /// 根据给定的指标列表统计任务
        /// </summary>
        /// <param name="rawTaskID">原始数据包ID</param>
        /// <param name="factorNameList">统计的指标列表</param>
        /// <returns>统计结果报表内容</returns>
        string GetXFactorStat(string rawTaskID, List<string> factorNameList);
    }

    
}
