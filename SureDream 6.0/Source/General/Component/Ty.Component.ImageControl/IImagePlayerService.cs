﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ty.Component.ImageControl
{
    /// <summary>
    /// 图片播放操作服务类
    /// </summary>
    public interface IImagePlayerService
    {
        /// <summary>
        /// 9）播放图片集合功能（List<string> ImageUrls），将集合内的图片按顺序反复播放，默认间隔为0.5秒。
        /// </summary>
        /// <param name="ImageUrls"></param>
        void LoadImages(List<string> ImageUrls);

        /// <summary>
        /// 10）播放图片文件夹功能，按照文件名按名称排序正序播放，默认间隔为0.5秒
        /// </summary>
        /// <param name="imageFoder"></param>
        void LoadImageFolder(string imageFoder);

        /// <summary>
        /// 播放 ftp图片文件夹
        /// </summary>
        /// <param name="imageFoder"> 文件夹路径 </param>
        /// <param name="useName"> 用户名 </param>
        /// <param name="passWord"> 密码 </param>
        void LoadFtpImageFolder(List<string> imageFoders, string startForder, string useName, string passWord);

        /// <summary>
        /// 获取当前图片URL
        /// </summary>
        /// <returns></returns>
        string GetCurrentUrl();

        /// <summary>
        /// 获取当前索引和总数
        /// </summary>
        /// <returns></returns>
        Tuple<int, int> GetIndexWithTotal();

        /// <summary>
        /// 获取图片操作控件类
        /// </summary>
        /// <returns></returns>
        IImgOperate GetImgOperate();

        /// <summary>
        /// 设置播放模式
        /// </summary>
        /// <param name="imgPlayMode"></param>
        void SetImgPlay(ImgPlayMode imgPlayMode);

        /// <summary>
        /// 设置播放位置
        /// </summary>
        /// <param name="index"></param>
        void SetPositon(int index);

        /// <summary>
        /// 图片索引发生变化时触发，P=当前URL
        /// </summary>
        event Action<string> ImageIndexChanged;

        /// <summary>
        /// 播放类型变化时触发
        /// </summary>
        event Action<ImgPlayMode> ImgPlayModeChanged;

        /// <summary>
        /// 拖拽进度条触发 P1= 索引 P2=路径
        /// </summary>
        event Action<int,string> SliderDragCompleted;

        /// <summary>
        /// 获取当前播放状态
        /// </summary>
        ImgPlayMode ImgPlayMode
        {
            get;
        }
    }
}
