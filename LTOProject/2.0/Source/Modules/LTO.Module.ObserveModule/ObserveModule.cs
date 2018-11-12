using LTO.General.ModuleManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LTO.Module.ObserveModule
{
    public class ObserveModule : ILTOModule
    {
        public string ModuleName
        {
            get
            {
                return "留观签退";
            }
        }

        public ImageSource Image
        {
            get
            {
                BitmapImage image = new BitmapImage(new Uri("Image/闹钟.png", UriKind.Relative));
                
                return image;
            }
        }

        public IModulePage ModulePage
        {
            get
            {
                return ObserveModeleDomain.Instance.GetModulePage();
            }
        }
    }
}
