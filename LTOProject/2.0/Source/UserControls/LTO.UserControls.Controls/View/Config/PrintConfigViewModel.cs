using LTO.Base.Frame.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LTO.UserControls.Controls
{
    partial class PrintConfigViewModel
    {
        private string _defaultIndex;
        /// <summary> 开机默认启动页面  0-首页 1-接种取号 2-留观签退  </summary>
        public string DefaultIndex
        {
            get { return _defaultIndex; }
            set
            {
                _defaultIndex = value;
                RaisePropertyChanged("DefaultIndex");
            }
        }


        private string _printLimit;
        /// <summary> 接种取号单打印限制  </summary>
        public string PrintLimit
        {
            get { return _printLimit; }
            set
            {
                _printLimit = value;
                RaisePropertyChanged("PrintLimit");
            }
        }



        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {


            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }
    }

    partial class PrintConfigViewModel : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public PrintConfigViewModel()
        {
            RelayCommand = new RelayCommand(RelayMethod);

        }
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
