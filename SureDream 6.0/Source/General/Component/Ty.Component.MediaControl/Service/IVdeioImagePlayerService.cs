using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ty.Component.ImageControl;

namespace Ty.Component.MediaControl
{
    /// <summary>
    /// 播放控件服务类（包括：视频和图片）
    /// </summary>
    interface IVdeioImagePlayerService
    {
        /// <summary> 全屏事件 </summary>
        event Action FullScreenHandle;

        /// <summary> 视频播放服务 </summary>
        IMediaPlayerService MediaPlayerService { get; set; }

        /// <summary> 图片播放服务 </summary>
        IImagePlayerService ImagePlayerService { get; set; }

        /// <summary> 加载视频 </summary>
        void LoadVedio(string path);

        /// <summary> 加载图片 </summary>
        void LoadImages(List<string> paths);

        /// <summary> 加载图片文件夹 </summary>
        void LoadImageFolder(List<string> paths, string start);

        /// <summary> 加载ftp图片文件夹类表和设置默认播放位置 </summary>
        void LoadFtpImageFolder(List<string> paths, string start, string user, string password);

        /// <summary> 加载共享图片文件列表和设置默认播放位置 </summary>
        void LoadShareImageFolder(List<string> paths, string start, string user, string password, string ip);


        /// <summary>
        /// 加快图片播放速度
        /// </summary>
        void PlaySpeedUp();

        /// <summary>
        /// 减慢图片播放速度
        /// </summary>
        void PlaySpeedDown();

    }

    /// <summary> 播放类型 </summary>
    public enum MediaPlayType
    {
        /// <summary> 视频 </summary>
        Video = 0,
        /// <summary> 图片 </summary>
        Image
    }
}
