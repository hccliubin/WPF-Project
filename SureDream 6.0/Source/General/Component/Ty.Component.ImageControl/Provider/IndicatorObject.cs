using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Ty.Component.ImageControl
{
    internal class IndicatorObject : Canvas
    {
        private MaskCanvas canvasOwner;

        public IndicatorObject(MaskCanvas canvasOwner)
        {
            this.canvasOwner = canvasOwner;
        }

        static IndicatorObject()
        {
            var ownerType = typeof(IndicatorObject);

            FocusVisualStyleProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(null));
            DefaultStyleKeyProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(ownerType));
            MinWidthProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(5.0));
            MinHeightProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(5.0));
        }

        public void Move(System.Windows.Point offset)
        {
            var x = Canvas.GetLeft(this) + offset.X;
            var y = Canvas.GetTop(this) + offset.Y;

            x = x < 0 ? 0 : x;
            y = y < 0 ? 0 : y;

            x = Math.Min(x, this.canvasOwner.Width - this.Width);
            y = Math.Min(y, this.canvasOwner.Height - this.Height);

            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);

            canvasOwner.UpdateSelectionRegion(new Rect(x, y, Width, Height), true);
        }



    }
}
