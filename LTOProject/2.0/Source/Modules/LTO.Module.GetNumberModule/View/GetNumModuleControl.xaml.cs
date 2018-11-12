using LTO.Base.Frame.MVVM;
using LTO.Base.Theme.Style;
using LTO.Domain.DataService;
using LTO.General.ModuleManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace LTO.Module.GetNumberModule
{
    /// <summary> 首页 </summary>
    public partial class GetNumModuleControl : BaseUserControl, IModulePage
    {

        GetNumModuleNotifyClass _vm = new GetNumModuleNotifyClass();
        public GetNumModuleControl()
        {
            InitializeComponent();

            this.DataContext = _vm;
        }

        //声明和注册路由事件
        public static readonly RoutedEvent ImageClickRoutedEvent =
            EventManager.RegisterRoutedEvent("ImageClick", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(GetNumModuleControl));

        Action _imageClick;
        Action IModulePage.ImageClick
        {
            get
            {
                return _imageClick;
            }
            set
            {
                _imageClick = value;
            }
        }


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

            if (_imageClick != null)
            {
                _imageClick();
            }
        }
    }





}
