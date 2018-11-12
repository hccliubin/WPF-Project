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

namespace HEW.Module.PhysicalExamination
{
    /// <summary> 中医体质辨识 </summary>
    [ModuleAttribute(ModuleName = ModuleManager.PhysicalExaminationModule)]
    public partial class PhysicalExaminationModuleControl : BaseModuleControl
    {

        PhysicalExaminationModuleNotifyClass _vm = new PhysicalExaminationModuleNotifyClass();
        public PhysicalExaminationModuleControl()
        {
            InitializeComponent();
        }

        //激发路由事件,借用Click事件的激发方法

        public override void OnLoginModuleSuccessed()
        {
            _vm = new PhysicalExaminationModuleNotifyClass();

            _vm.LoginInfo = this.LoginInfo;

            this.DataContext = _vm;

            base.OnLoginModuleSuccessed();
        }
    }
    

}
