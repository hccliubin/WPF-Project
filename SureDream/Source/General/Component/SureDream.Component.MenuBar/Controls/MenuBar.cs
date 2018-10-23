using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:SureDream.Component.MenuBar"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:SureDream.Component.MenuBar;assembly=SureDream.Component.MenuBar"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:MenuBar/>
    ///
    /// </summary>
    
    public class MenuBar : ItemsControl
    {
        static MenuBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuBar), new FrameworkPropertyMetadata(typeof(MenuBar)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.RefreshWith();
        }

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

        public ObservableCollection<ButtonBase> LeftControls
        {
            get { return (ObservableCollection<ButtonBase>)GetValue(LeftControlsProperty); }
            set { SetValue(LeftControlsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftControlsProperty =
            DependencyProperty.Register("LeftControls", typeof(ObservableCollection<ButtonBase>), typeof(MenuBar), new PropertyMetadata(new ObservableCollection<ButtonBase>(), (d, e) =>
             {
                 MenuBar control = d as MenuBar;

                 if (control == null) return;

                 List<ButtonBase> config = e.NewValue as List<ButtonBase>;

                 //control._leftControl.ItemsSource = config;

             }));


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

                 //control._rightControl.ItemsSource = config;
             }));
    


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
                if(item is IMenuIconButton)
                {
                    IMenuIconButton button = item as IMenuIconButton;

                    if(button.LeftRightAlignment== LeftRightAlignment.Left)
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

            this.LeftControls = left;
            this.RightControls = right;
        }



        //声明和注册路由事件
        public static readonly RoutedEvent MenuClickedRoutedEvent =
            EventManager.RegisterRoutedEvent("MenuClicked", RoutingStrategy.Bubble, typeof(EventHandler<MenuRoutedEventArgs>), typeof(MenuBar));
        //CLR事件包装
        public event MenuRoutedEventHandler MenuClicked
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


    }


}
