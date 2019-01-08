using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ty.Component.ImageControl
{

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
        void LoadFtpImageFolder(List<string> imageFoders, string useName, string passWord);

        string GetCurrentUrl();

        Tuple<string, string> GetIndexWithTotal();

        IImgOperate GetImgOperate();
    }
}
