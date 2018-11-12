using HEW.Base.Frame.MVVM;
using HEW.Base.Theme.Style;
using HEW.UserControls.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace HEW.Module.TCM
{
    /// <summary> 健康体检 </summary>
    [ModuleAttribute(ModuleName = ModuleManager.TCMModule)]
    public partial class TCMModuleControl : BaseModuleControl
    {

        TCMModuleNotifyClass _vm = new TCMModuleNotifyClass();
        public TCMModuleControl()
        {
            InitializeComponent();
        }

        public override void OnLoginModuleSuccessed()
        {
            _vm.LoginInfo = this.LoginInfo;

            this.DataContext = _vm;

            base.OnLoginModuleSuccessed();
        }
    }

}
