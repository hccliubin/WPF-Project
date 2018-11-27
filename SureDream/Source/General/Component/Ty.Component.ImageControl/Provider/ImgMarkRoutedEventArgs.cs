using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ty.Component.ImageControl
{
    public class ImgMarkRoutedEventArgs : RoutedEventArgs
    {
        public ImageMarkEngine MarkSource { get; set; }

        public ImgMarkRoutedEventArgs(RoutedEvent routedEvent, object source, ImageMarkEngine marksource) : base(routedEvent, source)
        {
            MarkSource = marksource;
        }
    }
}
