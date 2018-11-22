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

namespace SureDream.Appliaction.Demo.ImageControl
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private Rectangle HawkeyeBack { get; set; }
        private Grid HawEyeBox { get; set; }

        private Rectangle EyeWindow { get; set; }


        #region 鹰眼视窗


        /// <summary>

        /// 初始化鹰眼视窗

        /// </summary>

        void HawkeyeInit()

        {

            VisualBrush myVisualBrush = new VisualBrush();

            EyeWindow = new Rectangle();

            HawEyeBox = new Grid();

            myVisualBrush.Visual = Diagram1;

            HawEyeBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;

            HawEyeBox.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;

            HawEyeBox.Width = 300;

            HawEyeBox.Height = 200;

            EyeWindow.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

            EyeWindow.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            EyeWindow.Width = 300;

            EyeWindow.Height = 200;

            EyeWindow.Stroke = Brushes.White;

            HawkeyeBack = new Rectangle();

            HawkeyeBack.Width = Double.NaN;

            HawkeyeBack.Height = Double.NaN;

            HawkeyeBack.Stroke = Brushes.Yellow;

            HawkeyeBack.Margin = new Thickness(0, 0, 0, 0);

            HawkeyeBack.Fill = myVisualBrush;

            HawkeyeBack.MouseMove += myRectangle_MouseMove;

            HawkeyeBack.MouseDown += myRectangle_MouseDown;

            HawkeyeBack.MouseUp += HawkeyeBack_MouseUp;

            HawEyeBox.Children.Add(HawkeyeBack);

            HawEyeBox.Children.Add(EyeWindow);

            grid.Children.Add(HawEyeBox);

        }



        void myRectangle_MouseDown(object sender, MouseButtonEventArgs e)

        {

            Rectangle Sender = sender as Rectangle;

            //标记下鼠标按下

            if (e.LeftButton == MouseButtonState.Pressed)

            {

                Sender.Tag = new object();

                myRectangle_MouseMove(sender, e);

            }

        }

        bool b_lbuttondown = false;

        private void HawkeyeBack_MouseUp(object sender, MouseButtonEventArgs e)

        {

            Rectangle Sender = sender as Rectangle;

            //去掉鼠标按下的标记

            Sender.Tag = null;



            b_lbuttondown = false;

        }



        /// <summary>

        /// 鼠标操作视窗 移动 定位

        /// </summary>

        /// <param name="sender"></param>

        /// <param name="e"></param>

        void myRectangle_MouseMove(object sender, MouseEventArgs e)

        {

            Rectangle Sender = sender as Rectangle;

            Point P = e.GetPosition(Sender);

            Double ewx = 0;

            Double ewy = 0;

            if (Sender.Tag != null)

            {

                if (e.LeftButton == MouseButtonState.Pressed)

                {

                    //计算鹰眼框位置

                    {

                        ewx = P.X - EyeWindow.Width / 2;

                        ewy = P.Y - EyeWindow.Height / 2;

                        ewx = ewx < 0 ? 0 : ewx;

                        ewy = ewy < 0 ? 0 : ewy;



                        ewx = ewx + EyeWindow.Width > HawEyeBox.Width ? HawEyeBox.Width - EyeWindow.Width : ewx;

                        ewy = ewy + EyeWindow.Height > HawEyeBox.Height ? HawEyeBox.Height - EyeWindow.Height : ewy;



                        EyeWindow.Margin = new Thickness(Double.IsNaN(ewx) ? 0 : Math.Round(ewx), Double.IsNaN(ewy) ? 0 : Math.Round(ewy), 0, 0);

                    }



                    //移动视窗到指定位置

                    {

                        Double secx = ewx / (HawEyeBox.Width - EyeWindow.Width);

                        Double secy = ewy / (HawEyeBox.Height - EyeWindow.Height);

                        var X = scr.ScrollableWidth * secx;

                        var Y = scr.ScrollableHeight * secy;

                        scr.ScrollToHorizontalOffset(Double.IsNaN(X) ? 0 : X);

                        scr.ScrollToVerticalOffset(Double.IsNaN(Y) ? 0 : Y);

                    }

                }

            }

            else

            {

                if (b_lbuttondown)

                {

                    Diagram1_MouseMove(Diagram1, e);

                }

            }

        }



        /// <summary>

        /// 更新视窗大小和位置

        /// </summary>

        private void UpDateHawEyeWindow()

        {

            //更新鹰眼视窗大小

            Double scale_x = Diagram1.ActualWidth / scr.ActualWidth;

            Double scale_y = Diagram1.ActualHeight / scr.ActualHeight;

            EyeWindow.Width = Math.Round(HawEyeBox.Width / scale_x);

            EyeWindow.Height = Math.Round(HawEyeBox.Height / scale_y);



            EyeWindow.Width = EyeWindow.Width > HawEyeBox.Width ? HawEyeBox.Width : EyeWindow.Width;

            EyeWindow.Height = EyeWindow.Height > HawEyeBox.Height ? HawEyeBox.Height : EyeWindow.Height;



            //定位鹰眼视窗

            var X = (HorizontalOffset / scr.ScrollableWidth) * (HawEyeBox.Width - EyeWindow.Width);

            var Y = (VerticalOffset / scr.ScrollableHeight) * (HawEyeBox.Height - EyeWindow.Height);

            EyeWindow.Margin = new Thickness(Double.IsNaN(X) ? 0 : Math.Round(X), Double.IsNaN(Y) ? 0 : Math.Round(Y), 0, 0);



            //EyeWindow.UpdateLayout();

        }

        #endregion

    }
}
