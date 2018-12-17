using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WpfApp1
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            this.label1.Parent = this.pictureBox1;
        }
        //是否开始画了

        bool bDrawStart = false;

        //矩形的起始点

        Point pointStart = Point.Empty;

        //矩形的起始点的对角点，鼠标移动点

        Point pointContinue = Point.Empty;

        //是否要修改矩形，以鼠标点击点在矩形内部为准，修改时自动在4个端点创建4个小方框，不能创建其他的矩形

        bool bChange = false;



        //修改矩形时移动点的X、Y记录，好根据它在dicPoints找到对应的矩形，或dicPointsChange找到对应小方框

        Point pMove1 = Point.Empty;

        Point pMove2 = Point.Empty;

        //修改矩形时鼠标点击进4个小方框内，确认要根据鼠标移动修改

        bool bChangeMove = false;

        //当前全部创建的矩形的记录

        Dictionary<Point, Point> dicPoints = new Dictionary<Point, Point>();

        //矩形要修改大小时4个端点创建4个小方框的记录

        Dictionary<Point, Point> dicPointsChange = new Dictionary<Point, Point>();

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)

        {

            if (bDrawStart)

            {

                bDrawStart = false;

            }

            else

            {

                bDrawStart = true;

                pointStart = e.Location;

            }



            if (bChange)

            {

                bDrawStart = false;

                foreach (var item in dicPointsChange)

                {

                    Point p1 = item.Key;

                    Point p2 = item.Value;



                    //如果在4个小框里

                    if ((e.Location.X > p1.X && e.Location.X < p2.X && e.Location.Y > p1.Y && e.Location.Y < p2.Y) ||

                        (e.Location.X > p2.X && e.Location.X < p1.X && e.Location.Y > p2.Y && e.Location.Y < p1.Y) ||

                        (e.Location.X > p2.X && e.Location.X < p1.X && e.Location.Y > p1.Y && e.Location.Y < p2.Y) ||

                        (e.Location.X > p1.X && e.Location.X < p2.X && e.Location.Y > p2.Y && e.Location.Y < p1.Y))

                    {

                        bChangeMove = true;



                        //p1、p2为小方框的对角点，中心点为矩形的端点,首先获取端点，

                        //然后根据pMove1，pMove2确定小方框在的端点是矩形的哪个端点

                        //最后确定起始点，起始点pointStart为移动点（鼠标点击点、小方框在的端点）的对角点

                        //pointContinue为移动点（鼠标点击点、小方框在的端点）

                        Point p = Point.Empty;

                        p.X = p1.X > p2.X ? p1.X - 5 : p1.X + 5;

                        p.Y = p1.Y > p2.Y ? p1.Y - 5 : p1.Y + 5;



                        if (p == pMove1)

                        {

                            pointStart = pMove2;

                        }

                        else

                            if (p == pMove2)

                        {

                            pointStart = pMove1;

                        }

                        else

                                if (p == new Point(pMove1.X, pMove2.Y))

                        {

                            pointStart = new Point(pMove2.X, pMove1.Y);

                        }

                        else

                                    if (p == new Point(pMove2.X, pMove1.Y))

                        {

                            pointStart = new Point(pMove1.X, pMove2.Y);

                        }



                        break;

                    }

                }



                //如果没点击4个小方框，则默认放弃修改

                if (!bChangeMove)

                {

                    bDrawStart = true;

                    dicPointsChange.Clear();

                    bChange = false;

                }

            }

            else

            {

                foreach (var item in dicPoints)

                {

                    Point p1 = item.Key;

                    Point p2 = item.Value;



                    if ((e.Location.X > p1.X && e.Location.X < p2.X && e.Location.Y > p1.Y && e.Location.Y < p2.Y) ||

                        (e.Location.X > p2.X && e.Location.X < p1.X && e.Location.Y > p2.Y && e.Location.Y < p1.Y) ||

                        (e.Location.X > p2.X && e.Location.X < p1.X && e.Location.Y > p1.Y && e.Location.Y < p2.Y) ||

                        (e.Location.X > p1.X && e.Location.X < p2.X && e.Location.Y > p2.Y && e.Location.Y < p1.Y))

                    {

                        bChange = true;

                        bDrawStart = false; //不能画新矩形了

                        dicPointsChange.Clear();



                        pMove1 = p1;

                        pMove2 = p2;



                        //画出点击矩形的4个端点的4个小方框，并记录入dicPointsChange

                        Graphics g = pictureBox1.CreateGraphics();

                        System.Drawing.Pen pen = new System.Drawing.Pen(Color.Red);

                        g.DrawRectangle(pen, p1.X - 5, p1.Y - 5, 10, 10);

                        g.DrawRectangle(pen, p2.X - 5, p2.Y - 5, 10, 10);

                        g.DrawRectangle(pen, p1.X - 5, p2.Y - 5, 10, 10);

                        g.DrawRectangle(pen, p2.X - 5, p1.Y - 5, 10, 10);

                        pen.Dispose();



                        dicPointsChange.Add(new Point(p1.X - 5, p1.Y - 5), new Point(p1.X + 5, p1.Y + 5));

                        dicPointsChange.Add(new Point(p2.X - 5, p2.Y - 5), new Point(p2.X + 5, p2.Y + 5));

                        dicPointsChange.Add(new Point(p2.X - 5, p1.Y - 5), new Point(p2.X + 5, p1.Y + 5));

                        dicPointsChange.Add(new Point(p1.X - 5, p2.Y - 5), new Point(p1.X + 5, p2.Y + 5));

                        break;

                    }

                }

            }

        }



        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)

        {

            if (bDrawStart)

            {

                pointContinue = e.Location;

                Refresh();

            }



            if (bChangeMove)

            {

                pointContinue = e.Location;

                Point p = Point.Empty;

                foreach (var item in dicPoints)

                {

                    Point p1 = item.Key;

                    Point p2 = item.Value;

                    //找到dicPoints里的原始记录

                    if (p1 == pointStart || p2 == pointStart || new Point(p1.X, p2.Y) == pointStart || new Point(p2.X, p1.Y) == pointStart)

                    {

                        p = p1;

                        break;

                    }

                }



                if (p != Point.Empty)

                {

                    //先删除dicPoints里的原始记录，再根据新点创建新的记录

                    dicPoints.Remove(p);

                    dicPoints.Add(pointStart, pointContinue);



                    dicPointsChange.Clear();



                    Point p1 = pointStart;

                    Point p2 = pointContinue;

                    //移动过程即时创建4个小方框，看起来小方框和矩形一起变大变小的效果

                    dicPointsChange.Add(new Point(p1.X - 5, p1.Y - 5), new Point(p1.X + 5, p1.Y + 5));

                    dicPointsChange.Add(new Point(p2.X - 5, p2.Y - 5), new Point(p2.X + 5, p2.Y + 5));

                    dicPointsChange.Add(new Point(p2.X - 5, p1.Y - 5), new Point(p2.X + 5, p1.Y + 5));

                    dicPointsChange.Add(new Point(p1.X - 5, p2.Y - 5), new Point(p1.X + 5, p2.Y + 5));

                    Refresh();

                }

            }

        }



        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)

        {

            if (bDrawStart)

            {   //此时矩形画完，记录矩形两个对角点，在Paint里画

                dicPoints.Add(pointStart, pointContinue);

            }



            if (bChangeMove)

            {

                bChangeMove = false;

                bChange = false;

            }



            bDrawStart = false;

        }



        private void pictureBox1_Paint(object sender, PaintEventArgs e)

        {

            System.Drawing.Pen pen = new System.Drawing.Pen(Color.LimeGreen);

            pen.Width = 2;

            if (bDrawStart)

            {

                //实时的画矩形

                Graphics g = e.Graphics;

                g.DrawRectangle(pen, pointStart.X, pointStart.Y, pointContinue.X - pointStart.X, pointContinue.Y - pointStart.Y);

            }



            //实时的画之前已经画好的矩形

            foreach (var item in dicPoints)

            {

                Point p1 = item.Key;

                Point p2 = item.Value;



                e.Graphics.DrawRectangle(pen, p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);

            }



            if (bChangeMove)

            {

                pen.Color = Color.Red;

                foreach (var item in dicPointsChange)

                {

                    Point p1 = item.Key;

                    Point p2 = item.Value;



                    e.Graphics.DrawRectangle(pen, p1.X, p1.Y, p2.X - p1.X, p2.Y - p1.Y);

                }

            }



            pen.Dispose();

        }
    }
}
