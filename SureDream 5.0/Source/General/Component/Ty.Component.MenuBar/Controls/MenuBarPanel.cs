using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ty.Component.MenuBar
{
    /// <summary>
    /// 工具栏容器
    /// </summary> 
    [TemplatePart(Name = "toggle", Type = typeof(ToggleButton))]
    public class MenuBarPanel : ItemsControl
    {
        static MenuBarPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuBarPanel), new FrameworkPropertyMetadata(typeof(MenuBarPanel)));
        }

        ToggleButton toggleButton;
        /// <summary>
        /// 重绘模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            toggleButton = this.GetTemplateChild("toggle") as ToggleButton;

            var collection = this.Items.Cast<MenuBar>();

            foreach (var item in collection)
            {
                //  Do：当注册工具栏时，注册各个工具栏Header变化时的事件，如果有一个变化则触发其他变化
                item.HeaderChanged += (l, k) =>
                {
                    int maxLenght = collection.Max(m =>
                    {
                        if (m.Header == null) return 0;

                        return m.Header.Trim().Length;
                    });

                    
                    if (maxLenght == 0) return;

                    foreach (var c in collection)
                    {
                        //  Message：第一次初始化时过滤
                        if (c.Header == null)
                        {
                            c.Header = string.Empty.PadRight(maxLenght, ' ');
                        }
                        else if (c.Header.Length < maxLenght)
                        {
                            c.Header = c.Header.PadRight(maxLenght, ' ');
                        }
                        else if (c.Header.Length > maxLenght)
                        {
                            c.Header = c.Header.Trim();
                        }
                    }
                };
            }
        }



        public bool IsExpend
        {
            get { return (bool)GetValue(IsExpendProperty); }
            set { SetValue(IsExpendProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsExpendProperty =
            DependencyProperty.Register("IsExpend", typeof(bool), typeof(MenuBarPanel), new PropertyMetadata(false, (d, e) =>
             {
                 MenuBarPanel control = d as MenuBarPanel;

                 if (control == null) return;

                 bool config = (bool)e.NewValue;

                 control.toggleButton.IsChecked = config;
             }));


        //声明和注册路由事件
        public static readonly RoutedEvent ExpendChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("ExpendChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(MenuBarPanel));
        //CLR事件包装
        public event RoutedEventHandler ExpendChanged
        {
            add { this.AddHandler(ExpendChangedRoutedEvent, value); }
            remove { this.RemoveHandler(ExpendChangedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnExpendChanged()
        {
            RoutedEventArgs args = new RoutedEventArgs(ExpendChangedRoutedEvent, this);
            this.RaiseEvent(args);
        }



    }
}
