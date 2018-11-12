using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace LTO.Base.Theme.Style
{
    /// <summary>
    /// PageBar.xaml 的交互逻辑
    /// </summary>
    public partial class PageBar : UserControl
    {

        //圆点直径
        readonly int ellipse_Diameter = 50;
        //圆点间距
        readonly int ellipse_Peripheral = 22;
        //圆点列表
        readonly List<Rectangle> ellipseList = new List<Rectangle>();

        public PageBar()
        {
            InitializeComponent();
        }

        public void CreatePageEllipse(int pagecout)
        {
            canvas1.Children.Clear();
            ellipseList.Clear();

            System.Windows.Style rectangleStyle = this.FindResource("Rectangle_PageBar") as System.Windows.Style;

            //设置控件长度
            canvas1.Width = this.Width = ellipse_Peripheral + (ellipse_Diameter + ellipse_Peripheral) * pagecout;
            //画点
            for (int i = 1; i <= pagecout; i++)
            {
                Rectangle ellipse = new Rectangle();
                //ellipse.Width = ellipse.Height = ellipse_Diameter;
                //ellipse.StrokeThickness = 0;
                //ellipse.Fill = new SolidColorBrush(Colors.Gray);

                ellipse.Style = rectangleStyle;

                Canvas.SetLeft(ellipse, ellipse_Peripheral * i + ellipse_Diameter * (i - 1));
                Canvas.SetTop(ellipse, 1);
                canvas1.Children.Add(ellipse);
                ellipseList.Add(ellipse);
            }
        }

        public void CreatePageEllipse(int pagecout, Action<int> action)
        {
            canvas1.Children.Clear();
            ellipseList.Clear();
            //设置控件长度
            canvas1.Width = this.Width = ellipse_Peripheral + (ellipse_Diameter + ellipse_Peripheral) * pagecout;
            //画点
            for (int i = 1; i <= pagecout; i++)
            {
                Rectangle ellipse = new Rectangle();
                ellipse.Width = ellipse.Height = ellipse_Diameter;
                ellipse.StrokeThickness = 0;
                ellipse.Fill = new SolidColorBrush(Colors.Gray);
                Canvas.SetLeft(ellipse, ellipse_Peripheral * i + ellipse_Diameter * (i - 1));
                Canvas.SetTop(ellipse, 1);
                canvas1.Children.Add(ellipse);
                ellipseList.Add(ellipse);

                ellipse.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) =>
                {
                    int index = ellipseList.IndexOf(ellipse) + 1;

                    // Todo ：触发点击 
                    action(index);

                    this.SelectPage(index);
                };
            }
        }

        public void SelectPage(int pageselect)
        {
            if (ellipseList.Count >= pageselect)
            {
                for (int i = 0; i < ellipseList.Count; i++)
                {
                    if (i == pageselect - 1)
                        ellipseList[i].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0096FF"));
                    else
                        ellipseList[i].Fill = new SolidColorBrush(Colors.Gray);
                }
            }
        }
    }
}
