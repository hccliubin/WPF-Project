//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Media;
//using System.Windows.Shapes;

//namespace Ty.Component.ImageControl
//{
//    class RectangleStroke : IRectangleStroke
//    {
//        public RectangleShape Rectangle { get; set; }

//        public Point Position { get; set; }

//        public bool Visible { get => this.Rectangle.Visibility == Visibility.Visible; set => this.Rectangle.Visibility = value ? Visibility.Visible : Visibility.Collapsed; }

//        public virtual void Draw(InkCanvas canvas)
//        {
//            InkCanvas.SetLeft(Rectangle, Position.X);
//            InkCanvas.SetTop(Rectangle, Position.Y);
//            canvas.Children.Add(Rectangle);
//        }

//        public void Clear(InkCanvas canvas)
//        {
//            canvas.Children.Remove(this.Rectangle);
//        }

//        public RectangleStroke(Point start, Point end)
//        {

//            Rectangle path = new Rectangle();
//            path.Width = Math.Abs(start.X - end.X);
//            path.Height = Math.Abs(start.Y - end.Y);

//            Position = new Point(Math.Min(start.X, end.X), Math.Min(start.Y, end.Y));

//            this.Rectangle = path;

//        }

//        public RectangleStroke(Rectangle rectangle, Point postion)
//        {
//            Rectangle r = new Rectangle();
//            r.Width = rectangle.Width;
//            r.Height = rectangle.Height;

//            this.Rectangle = r;
//            this.Position = postion;
//        }

//    }

//    class DynamicRectangleStroke : RectangleStroke
//    {
//        public DynamicRectangleStroke(Point start, Point end, Brush stroke, Brush fill) : base(start, end)
//        {
//            this.Rectangle.Stroke = stroke;
//            this.Rectangle.Fill = fill;
//        }

//        public DynamicRectangleStroke(Point start, Point end, Brush stroke) : base(start, end)
//        {
//            this.Rectangle.Stroke = stroke;
//        }

//        public override void Draw(InkCanvas canvas)
//        {
//            InkCanvas.SetLeft(Rectangle, Position.X);
//            InkCanvas.SetTop(Rectangle, Position.Y);
//            canvas.Children.Add(Rectangle);
//        }

//    }

//    class ResultRectangleStroke : RectangleStroke
//    {
//        public ResultRectangleStroke(Point start, Point end, Brush stroke, Brush fill) : base(start, end)
//        {
//            this.Rectangle.Stroke = stroke;
//            this.Rectangle.Fill = fill;
//        }

//        public ResultRectangleStroke(Point start, Point end, Brush stroke) : base(start, end)
//        {
//            this.Rectangle.Stroke = stroke;
//        }

//        public ResultRectangleStroke(RectangleStroke rectangleStroke, Brush stroke) : base(rectangleStroke.Rectangle, rectangleStroke.Position)
//        {
//            this.Rectangle.Stroke = stroke;
//        }

//        public override void Draw(InkCanvas canvas)
//        {
//            InkCanvas.SetLeft(Rectangle, Position.X);
//            InkCanvas.SetTop(Rectangle, Position.Y);
//            canvas.Children.Add(Rectangle);
//        }
//    }


//    class DefectRectangleStroke : RectangleStroke
//    {
//        public DefectRectangleShape DefectRectangleShape { get; set; }

//        public DefectRectangleStroke(Point start, Point end, Brush stroke, Brush fill) : base(start, end)
//        {
//            this.Rectangle.Stroke = stroke;
//            this.Rectangle.Fill = fill;
//        }

//        public DefectRectangleStroke(Point start, Point end, Brush stroke) : base(start, end)
//        {
//            this.Rectangle.Stroke = stroke;
//        }

//        public DefectRectangleStroke(RectangleStroke rectangleStroke, Brush stroke) : base(rectangleStroke.Rectangle, rectangleStroke.Position)
//        {
//            DefectRectangleShape shape = new DefectRectangleShape();
//            shape.Rectangle = this.Rectangle;
//            shape.Stroke = stroke;
//            shape.Height = this.Rectangle.Height;
//            shape.Width = this.Rectangle.Width;

//            this.DefectRectangleShape = shape;

//            this.Rectangle.Stroke = stroke;
//        }

//        public override void Draw(InkCanvas canvas)
//        {
//            InkCanvas.SetLeft(DefectRectangleShape, Position.X);
//            InkCanvas.SetTop(DefectRectangleShape, Position.Y);
//            canvas.Children.Add(DefectRectangleShape);
//        }
//    }


//}
