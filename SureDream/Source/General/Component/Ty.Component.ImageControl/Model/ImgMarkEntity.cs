using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ty.Component.ImageControl
{
    /// <summary>
    /// 图片标定实体
    /// </summary>
    public class ImgMarkEntity
    {
        /// <summary>
        /// 由大括号包起来的全部大写的36位guidID，与表中的记录保持一致
        /// </summary>
        public string ID;

        /// <summary>
        /// 标定矩形框左上角X坐标
        /// </summary>
        public int X;

        /// <summary>
        /// 标定矩形框左上角Y坐标
        /// </summary>
        public int Y;

        /// <summary>
        /// 标定矩形框高度
        /// </summary>
        public int Height;

        /// <summary>
        /// 标定矩形框宽度
        /// </summary>
        public int Width;

        /// <summary>
        /// 标定类型代码（代码唯一，从代码表中查询）
        /// </summary>
        public string Code;

        /// <summary>
        /// 标定类型名称（通过标定类型代码到表中查询）
        /// </summary>
        public string Name;

        /// <summary>
        /// 标注操作类型，枚举值，默认是新增
        /// </summary>
        ImgMarkOperateType markOperateType;
    }

    /// <summary>
    /// 图片自动播放顺序
    /// </summary>
    public enum ImgPlayMode
    {
        正序 = 0,
        倒叙 = 1,
        停止播放 = 2,
    }

    /// <summary>
    /// 图片处理类型
    /// </summary>
    public enum ImgProcessType
    {
        滤镜 = 0,
        对比度 = 1,
        曝光补偿 = 2,
        夜视 = 3,
        锐化 = 4,
        边缘锐化 = 5,
    }

    /// <summary>
    /// 标定实体操作类型
    /// </summary>
    public enum ImgMarkOperateType
    {
        /// <summary>
        /// 新增标定
        /// </summary>
        Insert = 0,

        /// <summary>
        /// 修改已有标定
        /// </summary>
        Update = 1,

        /// <summary>
        /// 删除标定
        /// </summary>
        Delete = 2,
    }

    /// <summary>
    /// 标定内容变更委托，包括：新增、修改、删除
    /// </summary>
    /// <param name="markEntity">标定实体，实体内包含了实体是新增、修改还是删除的相关信息</param>
    public delegate void ImgMarkHandler(ImgMarkEntity markEntity);

    /// <summary>
    /// 图片风格化处理委托，
    /// </summary>
    /// <param name="imgPath">图片路径</param>
    /// <param name="imgProcessType">风格化处理类型</param>
    public delegate void ImgProcessHandler(string imgPath, ImgProcessType imgProcessType);



}
