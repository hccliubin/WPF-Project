using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace SureDream.Component.MenuBar
{
    /// <summary>
    /// 通用工具栏
    /// </summary>
    public class MenuBar : ItemsControl
    {
        static MenuBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuBar), new FrameworkPropertyMetadata(typeof(MenuBar)));
        }

        #region - 依赖属性 -

        /// <summary>
        /// 标题抬头
        /// </summary>
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(MenuBar), new PropertyMetadata(default(string), (d, e) =>
            {
                MenuBar control = d as MenuBar;

                if (control == null) return;

                string config = e.NewValue as string;

            }));
        /// <summary>
        /// 左侧控件集合依赖属性
        /// </summary>
        public ObservableCollection<ButtonBase> LeftControls
        {
            get { return (ObservableCollection<ButtonBase>)GetValue(LeftControlsProperty); }
            set { SetValue(LeftControlsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftControlsProperty =
            DependencyProperty.Register("LeftControls", typeof(ObservableCollection<ButtonBase>),
                typeof(MenuBar), new PropertyMetadata(new ObservableCollection<ButtonBase>(), (d, e) =>
                {
                    MenuBar control = d as MenuBar;

                    if (control == null) return;

                    List<ButtonBase> config = e.NewValue as List<ButtonBase>;

                }));

        /// <summary>
        /// 右侧侧控件集合依赖属性
        /// </summary>
        public ObservableCollection<ButtonBase> RightControls
        {
            get { return (ObservableCollection<ButtonBase>)GetValue(RightControlsProperty); }
            set { SetValue(RightControlsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightControlsProperty =
            DependencyProperty.Register("RightControls", typeof(ObservableCollection<ButtonBase>), typeof(MenuBar), new PropertyMetadata(new ObservableCollection<ButtonBase>(), (d, e) =>
            {
                MenuBar control = d as MenuBar;

                if (control == null) return;

                List<ButtonBase> config = e.NewValue as List<ButtonBase>;
            }));


        /// <summary>
        /// 动态绑定按钮接口依赖属性
        /// </summary>
        public ObservableCollection<MenuButton> ButtonSource
        {
            get { return (ObservableCollection<MenuButton>)GetValue(ButtonSourceProperty); }
            set { SetValue(ButtonSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonSourceProperty =
            DependencyProperty.Register("ButtonSource", typeof(ObservableCollection<MenuButton>), typeof(MenuBar), new PropertyMetadata(default(ObservableCollection<MenuButton>), (d, e) =>
            {
                MenuBar control = d as MenuBar;

                if (control == null) return;

                ObservableCollection<MenuButton> config = e.NewValue as ObservableCollection<MenuButton>;

                config.CollectionChanged += (l, k) =>
                {
                    control.RefreshWith(config);
                };

                control.RefreshWith(config);

            }));


        #endregion

        #region - 依赖事件 -
        //声明和注册路由事件
        public static readonly RoutedEvent MenuClickedRoutedEvent =
            EventManager.RegisterRoutedEvent("MenuClicked", RoutingStrategy.Direct, typeof(EventHandler<MenuRoutedEventArgs>), typeof(MenuBar));

        /// <summary>
        /// 点击事件依赖事件
        /// </summary>
        public event EventHandler<MenuRoutedEventArgs> MenuClicked
        {
            add { this.AddHandler(MenuClickedRoutedEvent, value); }
            remove { this.RemoveHandler(MenuClickedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法
        protected void OnMenuClicked(IMenuIconButton button)
        {
            MenuRoutedEventArgs args = new MenuRoutedEventArgs(MenuClickedRoutedEvent, this, button);
            this.RaiseEvent(args);
        }

        //声明和注册路由事件
        public static readonly RoutedEvent CheckedChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("CheckedChanged", RoutingStrategy.Bubble, typeof(EventHandler<MenuCheckedRoutedEventArgs>), typeof(MenuBar));
        /// <summary>
        /// 选择事件依赖事件
        /// </summary>
        public event EventHandler<MenuCheckedRoutedEventArgs> CheckedChanged
        {
            add { this.AddHandler(CheckedChangedRoutedEvent, value); }
            remove { this.RemoveHandler(CheckedChangedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnCheckedChanged(IMenuToggleButton button)
        {
            MenuCheckedRoutedEventArgs args = new MenuCheckedRoutedEventArgs(CheckedChangedRoutedEvent, this, button);
            this.RaiseEvent(args);
        }

        /// <summary>
        /// 刷新动态控件布局
        /// </summary>
        /// <param name="source"></param>
        void RefreshWith(ObservableCollection<MenuButton> source)
        {
            ObservableCollection<ButtonBase> left = new ObservableCollection<ButtonBase>();
            ObservableCollection<ButtonBase> right = new ObservableCollection<ButtonBase>();

            if (source == null)
            {
                this.LeftControls = left;
                this.RightControls = right;
                return;
            }

            //  Do：按类别添加到组内
            Action<IMenuIconButton> addAction = l =>
              {
                  if (l.LeftRightAlignment == LeftRightAlignment.Left)
                  {
                      left.Add(l as ButtonBase);
                  }

                  if (l.LeftRightAlignment == LeftRightAlignment.Right)
                  {
                      right.Add(l as ButtonBase);
                  }
              };

            //  Do：按类别转换按钮
            Func<MenuButton, IMenuIconButton> fuction = l =>
              {
                  if (l.MenuButtonStyle == MenuButtonStyle.Default)
                  {
                      MenuDefaultButton btn = new MenuDefaultButton();
                      btn.Content = l.Content;
                      btn.FIcon = l.IconFont;
                      btn.LeftRightAlignment = l.LeftRightAlignment;

                      btn.Click += (s, e) =>
                        {
                            this.OnMenuClicked(btn);
                        };

                      return btn;
                  }

                  if (l.MenuButtonStyle == MenuButtonStyle.IconButton)
                  {
                      MenuIconButton btn = new MenuIconButton();
                      btn.Content = l.Content;
                      btn.FIcon = l.IconFont;
                      btn.LeftRightAlignment = l.LeftRightAlignment;

                      btn.Click += (s, e) =>
                      {
                          this.OnMenuClicked(btn);
                      };

                      return btn;
                  }

                  if (l.MenuButtonStyle == MenuButtonStyle.ToggleButton)
                  {
                      MenuToggleButton btn = new MenuToggleButton();
                      btn.Content = l.Content;
                      btn.FIcon = l.IconFont;
                      btn.LeftRightAlignment = l.LeftRightAlignment;

                      btn.Checked += (s, e) =>
                      {
                          this.OnCheckedChanged(btn);
                      };

                      btn.Unchecked += (s, e) =>
                      {
                          this.OnCheckedChanged(btn);
                      };

                      return btn;
                  }

                  return null;
              };


            foreach (var item in source)
            {

                IMenuIconButton btnInstance = fuction(item);

                if (btnInstance == null)
                {
                    Debug.WriteLine("未识别按钮样式");
                    return;
                }

                addAction(btnInstance);

            }

            this.LeftControls = left;
            this.RightControls = right;
        }

        #endregion

        #region - 成员方法 -
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.RefreshWith();
        }

        /// <summary>
        /// 刷新静态控件布局
        /// </summary>
        void RefreshWith()
        {
            ObservableCollection<ButtonBase> left = new ObservableCollection<ButtonBase>();
            ObservableCollection<ButtonBase> right = new ObservableCollection<ButtonBase>();

            if (this.Items == null)
            {
                this.LeftControls = left;
                this.RightControls = right;
                return;
            }

            foreach (var item in this.Items)
            {
                if (item is IMenuIconButton)
                {
                    IMenuIconButton button = item as IMenuIconButton;

                    if (item is IMenuToggleButton)
                    {
                        ToggleButton b = button as ToggleButton;

                        b.Checked += (l, k) =>
                        {
                            this.OnMenuClicked(button);
                        };

                        b.Unchecked += (l, k) =>
                        {
                            this.OnMenuClicked(button);
                        };

                        if (button.LeftRightAlignment == LeftRightAlignment.Left)
                        {
                            left.Add(button as ButtonBase);
                        }
                        if (button.LeftRightAlignment == LeftRightAlignment.Right)
                        {
                            right.Add(button as ButtonBase);
                        }
                    }
                    else
                    {

                        if (button.LeftRightAlignment == LeftRightAlignment.Left)
                        {
                            ButtonBase b = button as ButtonBase;

                            b.Click += (l, k) =>
                            {
                                this.OnMenuClicked(button);
                            };

                            left.Add(button as ButtonBase);
                        }

                        if (button.LeftRightAlignment == LeftRightAlignment.Right)
                        {
                            ButtonBase b = button as ButtonBase;

                            b.Click += (l, k) =>
                            {
                                this.OnMenuClicked(button);
                            };

                            right.Add(button as ButtonBase);
                        }
                    }
                }
            }

            this.LeftControls = left;
            this.RightControls = right;
        }

        #endregion
    }


}
