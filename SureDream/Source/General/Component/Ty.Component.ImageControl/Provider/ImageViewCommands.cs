using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ty.Component.ImageControl
{
    public static class ImageViewCommands
    {
        public static RoutedUICommand LastImage = new RoutedUICommand() { Text="上一张"};

        public static RoutedUICommand NextImage = new RoutedUICommand() { Text = "下一张" };

        public static RoutedUICommand FullScreen= new RoutedUICommand() { Text = "全屏显示" };

        public static RoutedUICommand ShowStyleTool = new RoutedUICommand() { Text = "显示全部" };

        public static RoutedUICommand Save = new RoutedUICommand() { Text = "保存" };
    }
}
