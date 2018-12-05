using System.Windows;
using System.Windows.Controls;

namespace Ty.Component.ImageControl
{
    /// <summary>
    /// 矩形图形接口
    /// </summary>
    public interface IRectangleStroke
    {
        /// <summary>
        /// 清理图形
        /// </summary>
        /// <param name="canvas"></param>
        void Clear(InkCanvas canvas);
        /// <summary>
        /// 绘制图形
        /// </summary>
        /// <param name="canvas"></param>
        void Draw(InkCanvas canvas);

        /// <summary>
        /// 图形是否可见
        /// </summary>
        Visibility Visibility { get; set; }
    }
}