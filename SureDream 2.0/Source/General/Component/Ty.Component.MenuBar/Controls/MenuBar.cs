using Ty.Base.WpfBase;
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

namespace Ty.Component.MenuBar
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

                if (string.IsNullOrEmpty(config)) return;

                control.RefreshTiltle(config);

                control.OnHeaderChanged();

            }));



        /// <summary>
        /// 刷新抬头布局
        /// </summary>
        /// <param name="header"></param>
        void RefreshTiltle(string header)
        {

            int count = header.Length % 3 == 0 ? header.Length / 3 : header.Length / 3 + 1;

            if (header.Length > 20)
            {
                MessageBox.Show("标题栏长度限定为20字(40字节)以内。");
                return;
            }

            List<string> collection = new List<string>();

            for (int i = 0; i < count; i++)
            {
                var v = header.Skip(i * 3).Take(3).Select(l => l.ToString()).Aggregate((l, k) =>
                {
                    return l.ToString() + k.ToString();
                });

                collection.Add(v);
            }

            this.HeaderCollection = collection;
        }

        /// <summary>
        /// 用于拆分标题抬头
        /// </summary>
        List<string> HeaderCollection
        {
            get { return (List<string>)GetValue(HeaderCollectionProperty); }

            set
            {
                SetValue(HeaderCollectionProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderCollectionProperty =
            DependencyProperty.Register("HeaderCollection", typeof(List<string>), typeof(MenuBar), new PropertyMetadata(new List<string>() { "工具栏", "惨淡蓝", "测试蓝" }, (d, e) =>
              {
                  MenuBar control = d as MenuBar;

                  if (control == null) return;

                  List<string> config = e.NewValue as List<string>;

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


        /// <summary>
        /// 设置按钮是否可用规则
        /// </summary>
        public Predicate<IMenuIconButton> CanMenuButtonEnble
        {
            get { return (Predicate<IMenuIconButton>)GetValue(CanMenuButtonEnbleProperty); }
            set { SetValue(CanMenuButtonEnbleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanMenuButtonEnbleProperty =
            DependencyProperty.Register("CanMenuButtonEnble", typeof(Predicate<IMenuIconButton>), typeof(MenuBar), new PropertyMetadata(default(Predicate<IMenuIconButton>), (d, e) =>
             {
                 MenuBar control = d as MenuBar;

                 if (control == null) return;

                 Predicate<IMenuIconButton> config = e.NewValue as Predicate<IMenuIconButton>;

                 foreach (var item in control.LeftControls)
                 {
                     if (item is IMenuIconButton)
                     {
                         item.IsEnabled = config(item as IMenuIconButton);
                     }
                 }


                 foreach (var item in control.RightControls)
                 {
                     if (item is IMenuIconButton)
                     {
                         item.IsEnabled = config(item as IMenuIconButton);
                     }
                 }

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

        //声明和注册路由事件
        public static readonly RoutedEvent HeaderChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("HeaderChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(MenuBar));
        /// <summary>
        /// 抬头文本发生变化触发
        /// </summary>
        public event RoutedEventHandler HeaderChanged
        {
            add { this.AddHandler(HeaderChangedRoutedEvent, value); }
            remove { this.RemoveHandler(HeaderChangedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnHeaderChanged()
        {
            RoutedEventArgs args = new RoutedEventArgs(HeaderChangedRoutedEvent, this);
            this.RaiseEvent(args);
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
                    btn.IsEnabled = l.IsEnabled;
                    //btn.Orientation = l.Orientation;
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
                    btn.IsEnabled = l.IsEnabled;
                    btn.Orientation = l.Orientation;

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
                    btn.IsEnabled = l.IsEnabled;
                    btn.Orientation = l.Orientation;

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

                if (l.MenuButtonStyle == MenuButtonStyle.MenuImageButton)
                {
                    MenuImageButton btn = new MenuImageButton();
                    btn.Content = l.Content;
                    btn.ImageSource = l.ImageSource;
                    btn.IsEnabled = l.IsEnabled;
                    btn.Orientation = l.Orientation;

                    btn.LeftRightAlignment = l.LeftRightAlignment;

                    btn.Click += (s, e) =>
                    {
                        this.OnMenuClicked(btn);
                    };

                    return btn;
                }

                if (l.MenuButtonStyle == MenuButtonStyle.MenuToggleImageButton)
                {
                    MenuToggleImageButton btn = new MenuToggleImageButton();
                    btn.Content = l.Content;
                    btn.ImageSource = l.ImageSource;
                    btn.IsEnabled = l.IsEnabled;
                    btn.Orientation = l.Orientation;

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

                if (item.MenuKey != null&& !string.IsNullOrEmpty(item.MenuKey.String))
                {
                    ////  Do：注册快捷键
                    //InputGesture inputgesture = new KeyGesture(Key.O, ModifierKeys.Control);

                    RelayCommand cmd = new RelayCommand(l =>
                    {
                        if (btnInstance is IMenuToggleButton)
                        {
                            this.OnCheckedChanged(btnInstance as IMenuToggleButton);
                        }
                        else
                        {
                            this.OnMenuClicked(btnInstance);
                        }

                    });

                    InputBinding input = new InputBinding(cmd, item.MenuKey);

                    //  Do：注册到窗口级别
                    var collection = ControlsSearchHelper.GetParentObject<Window>(this, string.Empty).InputBindings;

                    collection.Add(input);

                }
            }

            this.LeftControls = left;
            this.RightControls = right;
        }


        #endregion
    }


}
