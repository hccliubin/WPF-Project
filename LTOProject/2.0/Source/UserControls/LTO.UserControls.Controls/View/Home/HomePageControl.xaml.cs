using LTO.Base.Theme.Style;
using LTO.General.ModuleManager;
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

namespace LTO.UserControls.Controls
{
    /// <summary> 首页 </summary>
    public partial class HomePageControl : BaseUserControl
    {
        public HomePageControl()
        {
            InitializeComponent();
        }


        //声明和注册路由事件
        public static readonly RoutedEvent ClickedRoutedEvent =
            EventManager.RegisterRoutedEvent("Clicked", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ModuleButtonControl));
        //CLR事件包装
        public event RoutedEventHandler Clicked
        {
            add { this.AddHandler(ClickedRoutedEvent, value); }
            remove { this.RemoveHandler(ClickedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnClicked()
        {
            RoutedEventArgs args = new RoutedEventArgs(ClickedRoutedEvent, this);
            this.RaiseEvent(args);
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {

            ModuleButtonControl module = e.Source as ModuleButtonControl;

            int index = int.Parse(module.Tag.ToString());

            this.CurrentModule = this.Modules[index];

            //this.SelectModuleName = module.BindModuleName;

            //module.IsChecked = true;

            this.OnClicked();
        }

        public string SelectModuleName
        {
            get { return (string)GetValue(SelectModuleNameProperty); }
            set { SetValue(SelectModuleNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectModuleNameProperty =
            DependencyProperty.Register("SelectModuleName", typeof(string), typeof(HomePageControl), new PropertyMetadata(default(string), (d, e) =>
             {
                 HomePageControl control = d as HomePageControl;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        //声明和注册路由事件
        public static readonly RoutedEvent ImageClickRoutedEvent =
            EventManager.RegisterRoutedEvent("ImageClick", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(HomePageControl));
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


        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.OnImageClick();
        }

        public ILTOModule CurrentModule
        {
            get { return (ILTOModule)GetValue(CurrentModuleProperty); }
            set { SetValue(CurrentModuleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentModuleProperty =
            DependencyProperty.Register("CurrentModule", typeof(ILTOModule), typeof(HomePageControl), new PropertyMetadata(default(ILTOModule), (d, e) =>
             {
                 HomePageControl control = d as HomePageControl;

                 if (control == null) return;

                 ILTOModule config = e.NewValue as ILTOModule;

             }));



        public List<ILTOModule> Modules
        {
            get { return (List<ILTOModule>)GetValue(ModulesProperty); }
            set { SetValue(ModulesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModulesProperty =
            DependencyProperty.Register("Modules", typeof(List<ILTOModule>), typeof(HomePageControl), new PropertyMetadata(default(List<ILTOModule>), (d, e) =>
             {
                 HomePageControl control = d as HomePageControl;

                 if (control == null) return;

                 List<ILTOModule> config = e.NewValue as List<ILTOModule>;

             }));


    }
}
