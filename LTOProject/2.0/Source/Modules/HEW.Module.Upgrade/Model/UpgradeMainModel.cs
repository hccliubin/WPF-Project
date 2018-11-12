using HEW.Base.Frame.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HEW.Module.Upgrade.Model
{
    partial class UpgradeMainModel
    {
        private int prograssBarValue = 0;
        private string prograssBarPercentage = "0%";
        private string upgradeInfor;

        public int PrograssBarValue { get => prograssBarValue; set { prograssBarValue = value; RaisePropertyChanged("PrograssBarValue"); } }
        public string PrograssBarPercentage { get => prograssBarPercentage; set { prograssBarPercentage = value; RaisePropertyChanged("PrograssBarPercentage"); } }
        public string UpgradeInfor { get => upgradeInfor; set { upgradeInfor = value; RaisePropertyChanged("UpgradeInfor"); } }

        public string URL { get; set; }
    }
    
    partial class UpgradeMainModel : INotifyPropertyChanged
    {
        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

}
