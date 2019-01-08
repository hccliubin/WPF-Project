using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ty.Component.ImageControl;

namespace Ty.Component.MediaControl
{
    /// <summary>
    /// VedioImagePlayerControl.xaml 的交互逻辑
    /// </summary>
    public partial class VedioImagePlayerControl : UserControl, IVdeioImagePlayerService
    {

        public VedioImagePlayerControl()
        {
            InitializeComponent();

            this.MediaPlayerService = this.control_media;

            this.ImagePlayerService = this.control_image;
        }

        public IMediaPlayerService MediaPlayerService { get; set; }

        public IImagePlayerService ImagePlayerService { get; set; }

        void RefreshPlayType(MediaPlayType type)
        {
            this.control_media.Visibility = type == MediaPlayType.Video ? Visibility.Visible : Visibility.Collapsed;
            this.control_image.Visibility = type == MediaPlayType.Image ? Visibility.Visible : Visibility.Collapsed;
        }

        public void LoadVedio(string path)
        {
            this.RefreshPlayType(MediaPlayType.Video);

            this.MediaPlayerService.Load(path);
        }

        public void LoadImages(List<string> paths)
        {
            this.RefreshPlayType(MediaPlayType.Image);

            this.ImagePlayerService.LoadImages(paths);
        }

        public void LoadImageFolder(string path)
        {
            this.RefreshPlayType(MediaPlayType.Image);

            this.ImagePlayerService.LoadImageFolder(path);
        }

        public void LoadFtpImageFolder(List<string> paths,string user,string password)
        {
            this.RefreshPlayType(MediaPlayType.Image);

            this.ImagePlayerService.LoadFtpImageFolder(paths, user,password);
        }
    }
}
