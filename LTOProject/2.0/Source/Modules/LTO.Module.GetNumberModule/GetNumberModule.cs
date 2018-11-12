using LTO.General.ModuleManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LTO.Module.GetNumberModule
{
    public class GetNumberModule : ILTOModule
    {
        public string ModuleName
        {
            get
            {
                return "接种取号";
            }
        }

        public ImageSource Image
        {
            get
            {
                BitmapImage image = new BitmapImage(new Uri("Image/针筒.png", UriKind.Relative));

                return image;
            }
        }

        public IModulePage ModulePage
        {
            get
            {
                return GetNumberModuleDomain.Instance.GetModulePage();
            }
        }
    }
}
