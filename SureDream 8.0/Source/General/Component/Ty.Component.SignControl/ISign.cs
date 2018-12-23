using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ty.Component.SignControl
{
    delegate void DefectSaveHandle(object sender, StrDefect defect);
    delegate void SignSaveHandle(object sender, StrSign sign);


    /// <summary>
    /// 缺陷、样本标记控件接口
    /// </summary>
    interface ISign
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns>1:成功；其他失败(请注明失败时各代码含义)</returns>
        int Initialize();
        /// <summary>
        /// 重置数据源
        /// </summary>
        /// <returns>1:成功；其他失败(请注明失败时各代码含义)</returns>
        int DataSourceReset();
        /// <summary>
        /// 弹出样本标定框
        /// </summary>
        /// <param name="isDialog">是否对话框</param>
        void ShowCalibration(bool isDialog);
        /// <summary>
        /// 弹出缺陷标记框
        /// </summary>
        /// <param name="isDialog">是否对话框</param>
        void ShowDefect(bool isDialog);

        /// <summary>
        /// 设置焦点
        /// </summary>
        /// <param name="tabIndex">焦点控件index</param>
        /// <returns></returns>
        int Setfocus(int tabIndex);

        /// <summary>
        /// 设置控件数据源(Txt,lbl)
        /// </summary>
        /// <param name="tabIndex">index</param>
        /// <param name="dataSource">数据源</param>
        /// <returns></returns>
        int SetDataSource(int tabIndex, object dataSource);
        /// <summary>
        /// 设置左侧描述信息
        /// </summary>
        /// <param name="tabIndex"></param>
        /// <param name="describle"></param>
        /// <returns></returns>
        int SetDescribe(int tabIndex, string describle);
        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="tabIndex">index</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        int SetValue(int tabIndex, string value);

        /// <summary>
        /// 控件宽度
        /// </summary>
        int Width { get; set; }
        /// <summary>
        /// 控件高度
        /// </summary>
        int Height { get; set; }

        //缺陷保存事件
        event DefectSaveHandle DefectSave;
        //样本保存事件
        event SignSaveHandle SignSave;

    }


    /// <summary>
    /// 缺陷 以下数据类型为暂定类型，后期需要修改
    /// </summary>
    struct StrDefect
    {
        //数据库类型
        string DbType;
        //数据采集方式
        string AcquisitionMethod;
        //铁路局
        string RailwayAdministration;
        //线路名
        string LineName;
        //站区
        string Station;
        //基本单元
        string BackUnit;
        //设备设施种类
        string DevType;
        //设备参数项目
        string DevParmPrj;
        //设备参数属性
        string DevParmPrp;
        //设备参数参考
        string DevRefVal;
        //测试日期yyyy-MM-dd
        string TestDate;
        //公里标K000+000.0
        string KmScale;
        //创建人
        string Creater;
        //检测车
        string TestTrain;
        //责任车间
        string ResponseShop;
        //责任工区
        string ResponseArea;

    }
    /// <summary>
    /// 样本标定 以下数据类型为暂定类型，后期需要修改
    /// </summary>
    struct StrSign
    {
        //区域/部位
        string Part;
        //装置类型
        string DevType;
        //创建人
        string Creater;
    }
    /// <summary>
    /// 控件类型
    /// </summary>
    enum CType
    {
        //文本输入
        Txt,
        //下拉列表
        Cbb,
        //文本
        Lbl
    }

}
