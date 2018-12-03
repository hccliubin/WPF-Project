using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;
using System.Windows.Media;

namespace Ty.Component.ImageControl
{
   public class RectangleLayer : Collection<IRectangleStroke>
    {
        public bool IsVisible
        {
            set
            {
                foreach (var item in this)
                {
                    item.Visibility = value?System.Windows.Visibility.Visible:System.Windows.Visibility.Collapsed;
                }
            }
        }
    }
}
