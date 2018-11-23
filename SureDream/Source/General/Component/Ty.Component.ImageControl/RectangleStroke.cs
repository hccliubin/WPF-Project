using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Ty.Component.ImageControl
{
    class RectangleStroke
    {
        public Rectangle Rectangle { get; set; }

        public Point Position { get; set; }

        public virtual void Draw(InkCanvas canvas)
        {
            InkCanvas.SetLeft(Rectangle, Position.X);
            InkCanvas.SetTop(Rectangle, Position.Y);
            canvas.Children.Add(Rectangle);
        }

        public RectangleStroke(Point start, Point end)
        {

            Rectangle path = new Rectangle();
            path.Width = Math.Abs(start.X - end.X);
            path.Height = Math.Abs(start.Y - end.Y);

            Position = new Point(Math.Min(start.X, end.X), Math.Min(start.Y, end.Y));

            this.Rectangle = path;

        }

    }

    class DynamicRectangleStroke: RectangleStroke
    {
        public DynamicRectangleStroke(Point start, Point end,Brush stroke,Brush fill):base(start,end)
        {
            this.Rectangle.Stroke = stroke;
            this.Rectangle.Fill = fill;
        }

        public DynamicRectangleStroke(Point start, Point end, Brush stroke) : base(start, end)
        {
            this.Rectangle.Stroke = stroke;
        }

        public override void Draw(InkCanvas canvas)
        {
            canvas.Children.Clear();
            InkCanvas.SetLeft(Rectangle, Position.X);
            InkCanvas.SetTop(Rectangle, Position.Y);
            canvas.Children.Add(Rectangle);
        }
    }
}
