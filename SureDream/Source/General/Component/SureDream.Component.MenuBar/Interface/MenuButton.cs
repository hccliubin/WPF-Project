using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SureDream.Component.MenuBar
{
    /// <summary> 动态添加控件接口 </summary>
    public class MenuButton : IMenuIconButton
    {
        /// <summary> 显示的名称 </summary>
        public object Content { get; set; }

        /// <summary> 显示的图标 </summary>
        public string IconFont { get; set; }

        /// <summary> 水平对齐方式 </summary>
        public LeftRightAlignment LeftRightAlignment { get; set; }

        /// <summary> 控件的显示形式 </summary>
        public MenuButtonStyle MenuButtonStyle { get; set; }

        /// <summary> 是否可用 </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary> 快捷键 </summary>
        public MenuKey MenuKey { get; set; }

        /// <summary> 图片布局方式 </summary>
        public Orientation Orientation { get; set; } = Orientation.Vertical;

        public ImageSource ImageSource { get; set; }



    }

    /// <summary>
    /// 按钮快捷键
    /// </summary>
    public class MenuKey : KeyGesture
    {
        public MenuKey(Key key) : base(key)
        {

        }

        public MenuKey(Key key, ModifierKeys modifiers) : base(key, modifiers)
        {

        }

        /// <summary>
        /// 快捷键文本
        /// </summary>
        public string String
        {
            get
            {
                if (this.Key == Key.None || this.Modifiers == ModifierKeys.None)
                {
                    return null;

                }

                if (this.Key == Key.None)
                {
                    return this.Modifiers.ToString();
                }

                if (this.Modifiers == ModifierKeys.None)
                {
                    return this.Key.ToString();
                }

                return this.Modifiers.ToString() + "+" + this.Key.ToString();

            }

        }
    }

}
