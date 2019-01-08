using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ty.Component.ImageControl;

namespace Ty.Component.MediaControl
{
    interface IVdeioImagePlayerService 
    {
        IMediaPlayerService MediaPlayerService { get; set; }

        IImagePlayerService ImagePlayerService { get; set; }

        void LoadVedio(string path);

        void LoadImages(List<string> paths);

        void LoadImageFolder(string path);

        void LoadFtpImageFolder(List<string> paths, string user, string password);

    }

   public enum MediaPlayType
    {
        Video = 0, Image
    }
}
