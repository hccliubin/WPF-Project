using CDTY.DataAnalysis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ty.Component.SignsControl
{
    /// <summary>
    /// 缺陷标定规则接口
    /// </summary>
   public interface IDefectSign
    {
        /// <summary>
        /// 加载常用列表(历史和最近)
        /// </summary>
        /// <param name="entity"></param>
        void LoadDefectCommonUsed(List<DefectCommonUsed> uses);

        /// <summary>
        /// 加载预估缺陷列表
        /// </summary>
        /// <param name="entity"></param>
        void LoadEstimateDefectCommonUseds(List<DefectCommonUsed> uses);

        /// <summary>
        /// 加载树模型
        /// </summary>
        /// <param name="entity"></param>
        void LoadTyeEncodeDevice(List<TyeEncodeDeviceEntity> uses);

        /// <summary>
        /// 加载设备列表
        /// </summary>
        /// <param name="entity"></param>
        void LoadTyeEncodeCheckDevice(List<TyeEncodeDeviceEntity> uses);


        void LoadPHM(string phm);

        ///// <summary>
        ///// 事件确认数据后返回PHM编码
        ///// </summary>
        //event Action<string> ConfirmData;

        /// <summary>
        /// 点击确认按钮
        /// </summary>
        event Action CancelClick;

        /// <summary>
        /// 点击取消按钮
        /// </summary>
        event Action SumitClick;

        ///// <summary>
        ///// 清空所有设置
        ///// </summary>
        //void Reset();

        /// <summary>
        /// 返回的结果数据
        /// </summary>
        string PHMCodes { get;}
    }
}
