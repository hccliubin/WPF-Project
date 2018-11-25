using System.Windows;
using System.Windows.Controls;

namespace Ty.Component.ImageControl
{
    public interface IRectangleStroke
    {
        void Clear(InkCanvas canvas);
        void Draw(InkCanvas canvas);

        Visibility Visibility { get; set; }
    }
}