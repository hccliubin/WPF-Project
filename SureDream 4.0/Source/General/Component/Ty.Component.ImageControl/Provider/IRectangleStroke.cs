﻿using System.Windows;
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
        /// 清理图形
        /// </summary>
        /// <param name="canvas"></param>
        void Clear();

        /// <summary>
        /// 绘制图形
        /// </summary>
        /// <param name="canvas"></param>
        void Draw(InkCanvas canvas);

        /// <summary>
        /// 图形是否可见
        /// </summary>
        Visibility Visibility { get; set; }

        /// <summary>
        /// 是否被选中
        /// </summary>
        bool IsSelected { get; }

        /// <summary>
        /// 设置选中
        /// </summary>
        void SetSelected();
    }
}