using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Ty.Component.ImageControl
{

    public class RectangleShape : Shape, IRectangleStroke
    {


        public RectangleShape() : base()
        {

            this.InitComponent();
        }

        void InitComponent()
        {
            this.MouseEnter += (l, k) =>
            {
                this.Fill = new SolidColorBrush() { Color = ((SolidColorBrush)this.Fill).Color, Opacity = 0.2 };
                this.StrokeThickness *= 3;
            };

            this.MouseLeave += (l, k) =>
            {
                this.Fill = new SolidColorBrush() { Color = ((SolidColorBrush)this.Fill).Color, Opacity = 0.1 };
                this.StrokeThickness /= 3;

            };
        }

        public RectangleShape(Point start, Point end)
        {
            this.InitComponent();

            this.Width = Math.Abs(start.X - end.X);
            this.Height = Math.Abs(start.Y - end.Y);

            Position = new Point(Math.Min(start.X, end.X), Math.Min(start.Y, end.Y));


            //Debug.WriteLine(this.Width + "*" + this.Height);
            //Debug.WriteLine(Position.X + "*" + Position.Y);

        }

        public RectangleShape(RectangleShape rectangle)
        {
            this.InitComponent();

            this.Width = rectangle.Width;
            this.Height = rectangle.Height;

            this.Position = new Point(rectangle.Position.X, rectangle.Position.Y);
        }


        public Point Position { get; set; }


        public Rect Rect
        {
            get
            {
                return new Rect(this.Position, new Size(this.Width,
                     this.Height));
            }
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                RectangleGeometry geo = new RectangleGeometry();

                geo.Rect = new Rect(new Point(0, 0), new Size(this.Width,
                    this.Height));
                return geo;
            }
        }

        public override Geometry RenderedGeometry
        {
            get
            {
                RectangleGeometry geo = new RectangleGeometry();

                geo.Rect = new Rect(new Point(0, 0), new Size(this.Width,
                    this.Height));
                return geo;
            }
        }

        public virtual void Draw(InkCanvas canvas)
        {
            InkCanvas.SetLeft(this, Position.X);
            InkCanvas.SetTop(this, Position.Y);
            canvas.Children.Add(this);
        }

        public void Clear(InkCanvas canvas)
        {
            canvas.Children.Remove(this);
        }


    }


    public class DynamicShape : RectangleShape
    {
        public DynamicShape() : base()
        {

        }
        public DynamicShape(Point start, Point end) : base(start, end)
        {

        }

        public DynamicShape(RectangleShape rectangle) : base(rectangle)
        {

        }

        public void Refresh(Point start, Point end)
        {
            this.Width = Math.Abs(start.X - end.X);
            this.Height = Math.Abs(start.Y - end.Y);

            this.Position = new Point(Math.Min(start.X, end.X), Math.Min(start.Y, end.Y));
            InkCanvas.SetLeft(this, Position.X);
            InkCanvas.SetTop(this, Position.Y);

            //Debug.WriteLine(this.Width + "*" + this.Height);
            //Debug.WriteLine(Position.X + "*" + Position.Y);
        }


        //public int WidthMatch { get; set; }
        //public int HeightMatch { get; set; }


        public int HeightMatch
        {
            get { return (int)GetValue(HeightMatchProperty); }
            set { SetValue(HeightMatchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeightMatchProperty =
            DependencyProperty.Register("HeightMatch", typeof(int), typeof(DynamicShape), new PropertyMetadata(10, (d, e) =>
             {
                 DynamicShape control = d as DynamicShape;

                 if (control == null) return;

                 //int config = e.NewValue as int;

             }));


        public int WidthMatch
        {
            get { return (int)GetValue(WidthMatchProperty); }
            set { SetValue(WidthMatchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthMatchProperty =
            DependencyProperty.Register("WidthMatch", typeof(int), typeof(DynamicShape), new PropertyMetadata(10, (d, e) =>
             {
                 DynamicShape control = d as DynamicShape;

                 if (control == null) return;

                 //int config = e.NewValue as int;

             }));



        public bool IsMatch()
        {
            return _initFlag && this.Width > this.WidthMatch && this.Height > this.HeightMatch;
        }

        bool _initFlag = false;

        public void BegionMatch(bool flag)
        {
            _initFlag = flag;
        }

    }

    public class DefectShape : RectangleShape
    {
        public DefectShape() : base()
        {

        }
        public DefectShape(Point start, Point end) : base(start, end)
        {

        }

        public DefectShape(RectangleShape rectangle) : base(rectangle)
        {

        }
    }

    public class SampleShape : RectangleShape
    {
        public SampleShape() : base()
        {

        }
        public SampleShape(Point start, Point end) : base(start, end)
        {

        }

        public SampleShape(RectangleShape rectangle) : base(rectangle)
        {

        }
    }

}
