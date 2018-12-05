using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ty.Component.ImageControl
{
    /// <summary>
    /// 应用到的绑定命令
    /// </summary>
    public static class ImageViewCommands
    {
        /// <summary>
        /// 上一张
        /// </summary>
        public static RoutedUICommand LastImage = new RoutedUICommand() { Text="上一张"};

        /// <summary>
        /// 下一张
        /// </summary>
        public static RoutedUICommand NextImage = new RoutedUICommand() { Text = "下一张" };

        /// <summary>
        /// 全屏显示
        /// </summary>
        public static RoutedUICommand FullScreen= new RoutedUICommand() { Text = "全屏显示" };

        /// <summary>
        /// 显示全部
        /// </summary>
        public static RoutedUICommand ShowStyleTool = new RoutedUICommand() { Text = "显示全部" };

        /// <summary>
        /// 保存
        /// </summary>
        public static RoutedUICommand Save = new RoutedUICommand() { Text = "保存" };
    }
}
