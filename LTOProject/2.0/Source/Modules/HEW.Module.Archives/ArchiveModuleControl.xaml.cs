using HEW.Base.Frame.MVVM;
using HEW.Base.Theme.Style;
using HEW.General.Data.Manager;
using HEW.General.Model.Enums;
using HEW.General.Model.Network.Form;
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

namespace HEW.Module.Archives
{
    /// <summary> 个人健康档案 </summary>
    [ModuleAttribute(ModuleName = ModuleManager.ArchiveModule)]
    public partial class ArchiveModuleControl : BaseModuleControl
    {

        ArchiveModuleNotifyClass _vm = new ArchiveModuleNotifyClass();

        public ArchiveModuleControl()
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
