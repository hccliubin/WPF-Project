using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace Ty.Component.ImageControl
{
    public static class BitmapSourceExtension
    {

        public static Bitmap GetBitmap(this BitmapSource bitmapsource)
        {

            Bitmap bitmap;

            using (MemoryStream outStream = new MemoryStream())
            {

                BitmapEncoder enc = new BmpBitmapEncoder();

                enc.Frames.Add(BitmapFrame.Create(bitmapsource));

                enc.Save(outStream);

                bitmap = (new Bitmap(outStream)).Clone() as Bitmap;

            }

            return bitmap;

        }

    }
}
