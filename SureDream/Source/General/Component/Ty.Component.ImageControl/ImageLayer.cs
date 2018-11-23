using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;
using System.Windows.Media;

namespace Ty.Component.ImageControl
{
    class ImageLayer : StrokeCollection
    {
        public ImageLayer Clone()
        {
            ImageLayer n = new ImageLayer();

            foreach (var item in this)
            {
                n.Add(item.Clone());
            }

            return n;
        }

        //ImageLayer _cache = new ImageLayer();

        //public bool IsVisible
        //{
        //    set
        //    {
        //        if(value)
        //        {
        //            //  Do：还原
        //            this.Clear();

        //            foreach (var item in _cache)
        //            {
        //                this.Add(item);
        //            }
        //        }
        //        else
        //        {
        //            //  Do：设置隐藏
        //            _cache = this.Clone();

        //            foreach (var item in this)
        //            {
        //                item.DrawingAttributes.Color = Colors.Transparent;
        //            }
        //        }
        //    }
        //}
    }
}
