﻿using System;
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
        public string Name { get; set; }

        public string Code { get; set; }

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

        public RectangleShape(double x,double y,double width,double height)
        {
            this.InitComponent();

            this.Width = width;
            this.Height = height;

            this.Position = new Point(x, y);
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

        public DynamicShape(double x, double y, double width, double height) : base(x,y,width,height)
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

        //public string Name { get; set; } 

        //public string Code { get; set; } 



        //public string Code
        //{
        //    get { return (string)GetValue(CodeProperty); }
        //    set { SetValue(CodeProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty CodeProperty =
        //    DependencyProperty.Register("Code", typeof(string), typeof(DefectShape), new PropertyMetadata(default(string), (d, e) =>
        //     {
        //         DefectShape control = d as DefectShape;

        //         if (control == null) return;

        //         string config = e.NewValue as string;

        //     }));


        //public string Name
        //{
        //    get { return (string)GetValue(NameProperty); }
        //    set { SetValue(NameProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty NameProperty =
        //    DependencyProperty.Register("Name", typeof(string), typeof(DefectShape), new PropertyMetadata(default(string), (d, e) =>
        //     {
        //         DefectShape control = d as DefectShape;

        //         if (control == null) return;

        //         string config = e.NewValue as string;

        //     }));


        public DefectShape() : base()
        {

        }
        public DefectShape(Point start, Point end) : base(start, end)
        {

        }

        public DefectShape(RectangleShape rectangle) : base(rectangle)
        {

        }

        public DefectShape(double x, double y, double width, double height) : base(x, y, width, height)
        {

        }
    }

    public class SampleShape : RectangleShape
    {

        //public string Name { get; set; }

        //public string Code { get; set; }

        public SampleShape() : base()
        {

        }
        public SampleShape(Point start, Point end) : base(start, end)
        {

        }

        public SampleShape(RectangleShape rectangle) : base(rectangle)
        {

        }

        public SampleShape(double x, double y, double width, double height) : base(x, y, width, height)
        {

        }
    }

}