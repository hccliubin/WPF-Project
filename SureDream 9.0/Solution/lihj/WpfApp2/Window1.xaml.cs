using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();



            this.Loaded += Window1_Loaded;

        }

        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            // 设置原图
            //img.Source = new BitmapImage(new Uri(@"F:\GitHub\WPF-Project\SureDream 9.0\Document\Files\2-27.jpg", UriKind.Absolute));

            //// 切割图片
            //ImageSource imageSource = img.Source;
            //Bitmap bitmap = SystemUtils.ImageSourceToBitmap(imageSource);
            //BitmapSource bitmapSource = SystemUtils.BitmapToBitmapImage(bitmap);
            //BitmapSource newBitmapSource = SystemUtils.CutImage(bitmapSource, new Int32Rect(125, 60, 235, 285));

            //// 使用切割后的图源
            //img1.Source = newBitmapSource;
        }


        ////图片原        
        //private BitmapImage _BitSource;
        //public BitmapImage BitSource
        //{
        //    get
        //    {
        //        return this.img.Source as BitmapImage;
        //    }
        //    set
        //    {
        //        this._BitSource = value;
        //        this.img.Source = value;
        //    }
        //}

        //public void CutImage()
        //{
        //    double ImageAreaWidth = this.img.ActualWidth;
        //    double ImageAreaHeight = this.img.ActualHeight;
        //    double GridWidth = this.img.ActualWidth;
        //    double GridHeight = this.img.ActualHeight;

        //    BitmapSource source = (BitmapSource)this.BitSource;
        //    //计算比例                  
        //    System.Windows.Point Locate = this.img.TransformToAncestor((UIElement)this.img).Transform(new System.Windows.Point(0, 0));

        //    int dWidth = (int)((ImageAreaWidth * 1.0 / GridWidth) * source.PixelWidth);
        //    int dHeight = (int)((ImageAreaHeight * 1.0 / GridHeight) * source.PixelHeight);
        //    int dLeft = (int)((Locate.X * 1.0 / GridWidth) * source.PixelWidth);
        //    int dTop = (int)((Locate.Y * 1.0 / GridHeight) * source.PixelHeight);
        //    //像素区域                   
        //    Int32Rect cutRect = new Int32Rect(dLeft, dTop, dWidth, dHeight);
        //    //数组字节数                 
        //    int stride = source.Format.BitsPerPixel * cutRect.Width / 8;
        //    byte[] data = new byte[cutRect.Height * stride];
        //    source.CopyPixels(cutRect, data, stride, 0);
        //    //创建                 
        //    BitmapSource bit = BitmapSource.Create(dWidth, dHeight, 0, 0, PixelFormats.Bgr32, null, data, stride);
        //    ////通知订阅                 
        //    //if (this.OnCutImage != null)
        //    //{
        //    //    OnCutImage(bit);
        //    //}

        //    this.SoureceImage1.Source = bit;
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // 切割图片
            ImageSource imageSource = img.Source;
            Bitmap bitmap = SystemUtils.ImageSourceToBitmap(imageSource);
            BitmapSource bitmapSource = SystemUtils.BitmapToBitmapImage(bitmap);
            BitmapSource newBitmapSource = SystemUtils.CutImage(bitmapSource, new Int32Rect(3300, 1000, 3300, 2200));

            Byte[] bytes = SystemUtils.ToBytes(newBitmapSource);

            BitmapImage bitmapImage = SystemUtils.ByteArrayToBitmapImage(bytes);

            // 使用切割后的图源
            img1.Source = newBitmapSource;

            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,Guid.NewGuid().ToString()+".jpg");

            SystemUtils.SaveBitmapImageIntoFile(bitmapImage, path);
        }


        //public void CutImage()
        //{
        //    if (this.BitSource != null)
        //    {
        //        try
        //        {
        //            double ImageAreaWidth = this.ImageArea.ActualWidth;
        //            double ImageAreaHeight = this.ImageArea.ActualHeight;
        //            double GridWidth = this.MainGrid.ActualWidth;
        //            double GridHeight = this.MainGrid.ActualHeight;
        //            BitmapSource source = (BitmapSource)this.BitSource;
        //            //计算比例                  
        //            Point Locate = this.ImageArea.TransformToAncestor((UIElement)this.MainGrid).Transform(new Point(0, 0));

        //            int dWidth = (int)((ImageAreaWidth * 1.0 / GridWidth) * source.PixelWidth);
        //            int dHeight = (int)((ImageAreaHeight * 1.0 / GridHeight) * source.PixelHeight);
        //            int dLeft = (int)((Locate.X * 1.0 / GridWidth) * source.PixelWidth);
        //            int dTop = (int)((Locate.Y * 1.0 / GridHeight) * source.PixelHeight);
        //            //像素区域                   
        //            Int32Rect cutRect = new Int32Rect(dLeft, dTop, dWidth, dHeight);
        //            //数组字节数                 
        //            int stride = source.Format.BitsPerPixel * cutRect.Width / 8;
        //            byte[] data = new byte[cutRect.Height * stride];
        //            source.CopyPixels(cutRect, data, stride, 0);
        //            //创建                 
        //            BitmapSource bit = BitmapSource.Create(dWidth, dHeight, 0, 0, PixelFormats.Bgr32, null, data, stride);
        //            ////通知订阅                 
        //            //if (this.OnCutImage != null)
        //            //{
        //            //    OnCutImage(bit);
        //            //}
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}
    }


    public static class SystemUtils
    {
        /// <summary>
        /// 切图
        /// </summary>
        /// <param name="bitmapSource">图源</param>
        /// <param name="cut">切割区域</param>
        /// <returns></returns>
        public static BitmapSource CutImage(BitmapSource bitmapSource, Int32Rect cut)
        {
            //计算Stride
            var stride = bitmapSource.Format.BitsPerPixel * cut.Width / 8;

            ////var stride = ((bitmapSource.PixelWidth * bitmapSource.Format.BitsPerPixel + 31) / 32) * 4;

            ////var stride = ((bitmapSource.PixelWidth * bitmapSource.Format.BitsPerPixel + 31) >> 5) << 2;

            //int stride = bitmapSource.PixelWidth * ((bitmapSource.Format.BitsPerPixel + 7) / 8);

            //声明字节数组
            byte[] data = new byte[cut.Height * stride];
            //调用CopyPixels
            bitmapSource.CopyPixels(cut, data, stride, 0);

            return BitmapSource.Create(cut.Width, cut.Height, 0, 0, PixelFormats.Gray8, null, data, stride);

            //Bitmap bitmap1 = ImageSourceToBitmap(bitmapSource);

            //var width = bitmapSource.PixelWidth;
            //var height = bitmapSource.PixelHeight;
            //var stride = width * ((bitmapSource.Format.BitsPerPixel + 7) / 8);
            //var memoryBlockPointer = System.Runtime.InteropServices.Marshal.AllocHGlobal(height * stride);
            //bitmapSource.CopyPixels(new Int32Rect(0, 0, width, height), memoryBlockPointer, height * stride, stride);


            //var bitmap = new Bitmap(width, height, stride, System.Drawing.Imaging.PixelFormat.Format32bppPArgb, memoryBlockPointer);

            //return BitmapToBitmapImage(bitmap);

        }

        // ImageSource --> Bitmap
        public static System.Drawing.Bitmap ImageSourceToBitmap(ImageSource imageSource)
        {
            BitmapSource m = (BitmapSource)imageSource;

            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(m.PixelWidth, m.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            System.Drawing.Imaging.BitmapData data = bmp.LockBits(
            new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            m.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride); bmp.UnlockBits(data);

            return bmp;
        }

        // Bitmap --> BitmapImage
        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Bmp);

                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                // According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
                // Force the bitmap to load right now so we can dispose the stream.
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();

                return result;
            }
        }

        public static byte[] BitmapImageToByteArray(BitmapImage bmp)
        {
            byte[] byteArray = null;
            try
            {
                Stream sMarket = bmp.StreamSource;
                if (sMarket != null && sMarket.Length > 0)
                {
                    //很重要，因为Position经常位于Stream的末尾，导致下面读取到的长度为0。 
                    sMarket.Position = 0;

                    using (BinaryReader br = new BinaryReader(sMarket))
                    {
                        byteArray = br.ReadBytes((int)sMarket.Length);
                    }
                }
            }
            catch
            {
                //other exception handling 
            }
            return byteArray;
        }

        public static byte[] ToBytes(BitmapSource bitmapSource)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            //encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            encoder.QualityLevel = 100;
            //byte[] bit;
            using (MemoryStream stream = new MemoryStream())
            {
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(stream);
                byte[] bit = stream.ToArray();
                stream.Close();
                return bit;
            }
        }


        public static BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            BitmapImage bmp = null;
            try
            {
                bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(byteArray);
                bmp.EndInit();
            }
            catch
            {
                bmp = null;
            }
            return bmp;
        }


        /// <summary>
        /// 把内存里的BitmapImage数据保存到硬盘中
        /// </summary>
        /// <param name="bitmapImage">BitmapImage数据</param>
        /// <param name="filePath">输出的文件路径</param>
        public static void SaveBitmapImageIntoFile(BitmapImage bitmapImage, string filePath)
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

            using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }
    }
}
