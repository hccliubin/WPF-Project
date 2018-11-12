using HEW.Base.Frame.MVVM;
using HEW.Base.Theme.Style;
using HEW.UserControls.Reports;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HEW.Module.Expenses
{
    /// <summary> 住院/门诊费用 </summary>
    [ModuleAttribute(ModuleName = ModuleManager.ExpensesModule)]
    public partial class ExpensesModuleControl : BaseModuleControl
    {
        ExpensesModuleNotifyClass _vm = new ExpensesModuleNotifyClass();

        public ExpensesModuleControl()
        {
            InitializeComponent();


        }

        //激发路由事件,借用Click事件的激发方法

        public override void OnLoginModuleSuccessed()
        {

            this.DataContext = _vm;

            base.OnLoginModuleSuccessed();
        }

    }



    partial class ExpensesModuleNotifyClass
    {

        private bool _isShowReport;
        /// <summary> 说明  </summary>
        public bool IsShowReport
        {
            get { return _isShowReport; }
            set
            {
                _isShowReport = value;
                RaisePropertyChanged("IsShowReport");
            }
        }


        private ObservableCollection<ExpenseModel> _collection = new ObservableCollection<ExpenseModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ExpenseModel> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }


        private ExpenseModel _selectValue;
        /// <summary> 说明  </summary>
        public ExpenseModel SelectValue
        {
            get { return _selectValue; }
            set
            {
                _selectValue = value;
                RaisePropertyChanged("SelectValue");
            }
        }


        private LoginInfo _loginInfo;
        /// <summary> 说明  </summary>
        public LoginInfo LoginInfo
        {
            get { return _loginInfo; }
            set
            {
                _loginInfo = value;
                RaisePropertyChanged("LoginInfo");


            }
        }


        private bool _isShow;
        /// <summary> 说明  </summary>
        public bool IsShow
        {
            get { return _isShow; }
            set
            {
                _isShow = value;
                RaisePropertyChanged("IsShow");
            }
        }



        private string _filterSelection = "全部费用";
        /// <summary> 说明  </summary>
        public string FilterSelection
        {
            get { return _filterSelection; }
            set
            {
                _filterSelection = value;
                RaisePropertyChanged("FilterSelection");
            }
        }



        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "ItemMouseDoubleClick")
            {
                //if (this.SelectValue == null) return;

                //  ToDo：读取数据

                this.IsShowReport = true;
            }

            else if (command == "SelectionChanged")
            {
                if (this.Collection == null) return;

                foreach (var item in this.Collection)
                {
                    item.Visiblility = FilterSelection == "全部费用" ? Visibility.Visible : item.Type == FilterSelection.Replace("费","") 
                        ? Visibility.Visible : Visibility.Collapsed;
                }

            }

            //  Do：取消
            else if (command == "Test")
            {

                this.Collection.Clear();

                for (int i = 0; i < 50; i++)
                {
                    ExpenseModel m = new ExpenseModel();

                    m.Time = DateTime.Now.AddMonths(i).ToString("yyyy-MM-dd HH:mm");

                    m.Type = i % 3 == 0 ? "门诊" : "住院";

                    m.Value = "总费用：1234335.934";

                    this.Collection.Add(m);


                }
            }
            //  Do：取消
            else if (command == "LoginModuleSuccessed")
            {
                Debug.WriteLine("登录成功！登录信息");

            }
        }
    }

    partial class ExpensesModuleNotifyClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public ExpensesModuleNotifyClass()
        {
            RelayCommand = new RelayCommand(RelayMethod);

            RelayMethod("Test");

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


    partial class ExpenseModel
    {

        private string _time;
        /// <summary> 说明  </summary>
        public string Time
        {
            get { return _time; }
            set { _time = value; }
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private string _Value;

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }



        private Visibility _visibility;
        /// <summary> 说明  </summary>
        public Visibility Visiblility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                RaisePropertyChanged("Visiblility");
            }
        }

    }

    partial class ExpenseModel : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public ExpenseModel()
        {


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
