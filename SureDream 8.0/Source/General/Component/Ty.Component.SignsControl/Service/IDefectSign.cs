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
        /// 加载
        /// </summary>
        /// <param name="entity"></param>
        void Load(DefectMenuEntity entity);

        /// <summary>
        /// 事件确认数据后返回PHM编码
        /// </summary>
        event Action<string> ConfirmData;

        /// <summary>
        /// 点击确认按钮
        /// </summary>
        event Action CancelClick;

        /// <summary>
        /// 点击取消按钮
        /// </summary>
        event Action SumitClick;

        /// <summary>
        /// 清空所有设置
        /// </summary>
        void Reset();

        /// <summary>
        /// 设置光标位置
        /// </summary>
        void SetTabIndex(int index);

        /// <summary>
        /// P
        /// </summary>
        string PHMCodes { get; set; }

        /// <summary>
        /// 选择的单元
        /// </summary>
        TyeEncodeCategoryconfigEntity SelectBasicUnit { get; }

        /// <summary>
        /// 选择的历史信息
        /// </summary>
        DefectCommonUsed SelectCommonHistoricalDefectsOrMark { get;}
        /// <summary>
        /// 选择的数据采集方式
        /// </summary>
        TyeEncodeCategoryconfigEntity SelectDataAcquisitionMode { get; }
        /// <summary>
        /// 选择的段
        /// </summary>
        TyeBaseLineEntity SelectDedicatedLine { get; }
        /// <summary>
        /// 选择的站
        /// </summary>
        TyeBaseSiteEntity SelectDedicatedStation { get;}
        /// <summary>
        /// 选择的缺陷
        /// </summary>
        TyeEncodeDeviceEntity SelectDefectOrMarkCodes { get; }
        /// <summary>
        /// 选择的铁路局顺序码
        /// </summary>
        TyeEncodeCategoryconfigEntity SelectRailwaySsequence { get;}
        /// <summary>
        /// 选择的责任工区
        /// </summary>
        TyeEncodeCategoryconfigEntity SelectResponsibilityWorkArea { get;}
        /// <summary>
        /// 选择的责任车间
        /// </summary>
        TyeEncodeCategoryconfigEntity SelectResponsibilityWorkshop { get; }

    }
}
