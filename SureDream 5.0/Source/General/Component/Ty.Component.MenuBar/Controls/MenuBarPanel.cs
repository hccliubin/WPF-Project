using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Ty.Component.MenuBar
{
    /// <summary>
    /// 工具栏容器
    /// </summary>
    public class MenuBarPanel : ItemsControl
    {
        static MenuBarPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuBarPanel), new FrameworkPropertyMetadata(typeof(MenuBarPanel)));
        }

        /// <summary>
        /// 重绘模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
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


    }
}
