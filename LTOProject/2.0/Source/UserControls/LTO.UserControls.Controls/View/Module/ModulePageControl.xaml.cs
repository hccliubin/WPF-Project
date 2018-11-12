using LTO.Base.Theme.Style;
using LTO.General.ModuleManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LTO.UserControls.Controls
{
    /// <summary> 具体模块的容器 </summary>
    public partial class ModulePageControl : BaseUserControl
    {
        public ModulePageControl()
        {
            InitializeComponent();
        }




        //声明和注册路由事件
        public static readonly RoutedEvent ImageClickRoutedEvent =
            EventManager.RegisterRoutedEvent("ImageClick", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ModulePageControl));
        //CLR事件包装
        public event RoutedEventHandler ImageClick
        {
            add { this.AddHandler(ImageClickRoutedEvent, value); }
            remove { this.RemoveHandler(ImageClickRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnImageClick()
        {
            RoutedEventArgs args = new RoutedEventArgs(ImageClickRoutedEvent, this);
            this.RaiseEvent(args);
        }



        public IModulePage ModulePage
        {
            get { return (IModulePage)GetValue(ModulePageProperty); }
            set { SetValue(ModulePageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModulePageProperty =
            DependencyProperty.Register("ModulePage", typeof(IModulePage), typeof(ModulePageControl), new PropertyMetadata(default(IModulePage), (d, e) =>
             {
                 ModulePageControl control = d as ModulePageControl;

                 if (control == null) return;
                 

                 IModulePage config = e.NewValue as IModulePage;

                 if (config == null) return;

                 control.Content = config;

                 //  Message：触发点击图片依赖事件
                 config.ImageClick = () =>
                   { 
                       control.OnImageClick();

                       //control.IsShow = false;
                   };
             }));


    }
}
