using HEW.Base.Theme.Style;
using HEW.UserControls.Reports;
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

namespace HEW.Module.Followup
{
    /// <summary> 慢病随访记录 </summary>
    [ModuleAttribute(ModuleName = ModuleManager.FollowupModule)]
    public partial class FollowupModuleControl : BaseModuleControl
    {

        FollowupModuleNotifyClass _vm = new FollowupModuleNotifyClass();
        public FollowupModuleControl()
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
