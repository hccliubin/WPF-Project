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
    /// 矢量图标库选中按钮控件
    /// </summary>    
    public class MenuToggleButton : ToggleButton, IMenuToggleButton
    {
        static MenuToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuToggleButton), new FrameworkPropertyMetadata(typeof(MenuToggleButton)));
        }

        #region - 依赖属性 -

        public static readonly DependencyProperty PressedBackgroundProperty =
                 DependencyProperty.Register("PressedBackground", typeof(Brush), typeof(MenuToggleButton), new PropertyMetadata(Brushes.DarkBlue));
        /// <summary> 鼠标按下背景样式 </summary>
        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty PressedForegroundProperty =
            DependencyProperty.Register("PressedForeground", typeof(Brush), typeof(MenuToggleButton), new PropertyMetadata(Brushes.White));
        /// <summary> 鼠标按下前景样式（图标、文字） </summary>
        public Brush PressedForeground
        {
            get { return (Brush)GetValue(PressedForegroundProperty); }
            set { SetValue(PressedForegroundProperty, value); }
        }

        public static readonly DependencyProperty MouseOverBackgroundProperty =
            DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(MenuToggleButton), new PropertyMetadata(Brushes.RoyalBlue));
        /// <summary> 鼠标进入背景样式 </summary>
        public Brush MouseOverBackground
        {
            get { return (Brush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }

        public static readonly DependencyProperty MouseOverForegroundProperty =
            DependencyProperty.Register("MouseOverForeground", typeof(Brush), typeof(MenuToggleButton), new PropertyMetadata(Brushes.White));
        /// <summary> 鼠标进入前景样式 </summary>
        public Brush MouseOverForeground
        {
            get { return (Brush)GetValue(MouseOverForegroundProperty); }
            set { SetValue(MouseOverForegroundProperty, value); }
        }

        public static readonly DependencyProperty FIconProperty =
            DependencyProperty.Register("FIcon", typeof(string), typeof(MenuToggleButton), new PropertyMetadata("\xe607"));
        /// <summary> 按钮字体图标编码 </summary>
        public string FIcon
        {
            get { return (string)GetValue(FIconProperty); }
            set { SetValue(FIconProperty, value); }
        }

        public static readonly DependencyProperty FIconSizeProperty =
            DependencyProperty.Register("FIconSize", typeof(int), typeof(MenuToggleButton), new PropertyMetadata(20));
        /// <summary> 按钮字体图标大小 </summary>
        public int FIconSize
        {
            get { return (int)GetValue(FIconSizeProperty); }
            set { SetValue(FIconSizeProperty, value); }
        }

        public static readonly DependencyProperty FIconMarginProperty = DependencyProperty.Register(
            "FIconMargin", typeof(Thickness), typeof(MenuToggleButton), new PropertyMetadata(new Thickness(0, 1, 3, 1)));
        /// <summary> 字体图标间距 </summary>
        public Thickness FIconMargin
        {
            get { return (Thickness)GetValue(FIconMarginProperty); }
            set { SetValue(FIconMarginProperty, value); }
        }

        public static readonly DependencyProperty AllowsAnimationProperty = DependencyProperty.Register(
            "AllowsAnimation", typeof(bool), typeof(MenuToggleButton), new PropertyMetadata(true));
        /// <summary> 是否启用Ficon动画 </summary>
        public bool AllowsAnimation
        {
            get { return (bool)GetValue(AllowsAnimationProperty); }
            set { SetValue(AllowsAnimationProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(MenuToggleButton), new PropertyMetadata(new CornerRadius(2)));
        /// <summary> 按钮圆角大小,左上，右上，右下，左下 </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty ContentDecorationsProperty = DependencyProperty.Register(
            "ContentDecorations", typeof(TextDecorationCollection), typeof(MenuToggleButton), new PropertyMetadata(null));
        public TextDecorationCollection ContentDecorations
        {
            get { return (TextDecorationCollection)GetValue(ContentDecorationsProperty); }
            set { SetValue(ContentDecorationsProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(MenuToggleButton), new PropertyMetadata(Orientation.Vertical));

        /// <summary> 图标和文字的布局方式 </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty IconFontWeightProperty = DependencyProperty.Register("IconFontWeight", typeof(FontWeight), typeof(MenuToggleButton), new PropertyMetadata(null));
        /// <summary> 按钮字体图标编码 </summary>
        public FontWeight IconFontWeight
        {
            get { return (FontWeight)GetValue(IconFontWeightProperty); }
            set { SetValue(IconFontWeightProperty, value); }
        }

        #endregion

        #region - 接口实现 -

        public LeftRightAlignment LeftRightAlignment { get; set; }

        #endregion
    }
}
