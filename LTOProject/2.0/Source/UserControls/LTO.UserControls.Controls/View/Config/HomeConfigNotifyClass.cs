using LTO.Base.Frame.MVVM;
using LTO.Base.Theme.Style;
using LTO.Domain.DataService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LTO.UserControls.Controls
{
    partial class HomeConfigNotifyClass
    {

        private string _passWord = "";
        /// <summary> 输入的密码 </summary>
        public string PassWord
        {
            get { return _passWord; }
            set
            {
                _passWord = value;
                RaisePropertyChanged("PassWord");
            }
        }

        private bool _isShowPassWord = true;
        /// <summary> 是否显示输入密码页面  </summary>
        public bool IsShowPassWord
        {
            get { return _isShowPassWord; }
            set
            {
                _isShowPassWord = value;
                RaisePropertyChanged("IsShowPassWord");
            }
        }



        private bool _isShow = false;
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



        private bool _isShowConfigMessage;
        /// <summary> 说明  </summary>
        public bool IsShowConfigMessage
        {
            get { return _isShowConfigMessage; }
            set
            {
                _isShowConfigMessage = value;
                RaisePropertyChanged("IsShowConfigMessage");
            }
        }




        PrintConfigViewModel _config = new PrintConfigViewModel();
        /// <summary> 说明  </summary>
        public PrintConfigViewModel Config
        {
            get { return _config; }
            set
            {
                _config = value;
                RaisePropertyChanged("Config");
            }
        }

        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {
                //  Do：保存配置
                GeneralControlDomain.Instance.SetConfigPrintLimit(this.Config.PrintLimit);
                GeneralControlDomain.Instance.SetConfigStartIndex(this.Config.DefaultIndex);

                //if (!result)
                //{
                //    MessageSingleControl.Show(err);
                //    return;
                //}

                MessageSingleControl.Show("保存成功！");

                this.IsShow = false;


            }

            //  Do：取消
            else if (command == "PasswordLoginClick")
            {

                if (this.PassWord.ToLower() == "tty123")
                {
                    //  Do：显示设置、开机、关机页面
                    this.IsShowConfigMessage = true;

                    //  Do：加载配置
                    this.Config.PrintLimit = GeneralControlDomain.Instance.GetConfigPrintLimit();
                    this.Config.DefaultIndex = GeneralControlDomain.Instance.GetConfigStartIndex();

                    this.IsShowPassWord = false;
                    this.PassWord = "";
                }
                else
                {
                    MessageSingleControl.Show("密码错误，请重新输入！");
                    this.PassWord = "";
                }
            }


            else if (command == "PassWordGotFocus")
            {
                GeneralControlDomain.Instance.ShowKeyBoard();
            }

            else if (command == "SetDownClicked")
            {

                Action<MessageResult> Action = l =>
                {

                    if (l == MessageResult.Cancel)
                    {

                    }
                    else if (l == MessageResult.Sumit)
                    {
                        ServiceManager.ToolService.PowerOff();
                    }
                    else
                    {

                    }
                };
                MessageSingleControl.ShowWithCancelAndSumit("确定要关闭计算机?", -9, Action);

            }

            else if (command == "ShutDownAppClicked")
            {

                ServiceManager.ToolService.ShutDown();

            }
        }
    }

    partial class HomeConfigNotifyClass : INotifyPropertyChanged
    {



        public RelayCommand RelayCommand { get; set; }

        public HomeConfigNotifyClass()
        {
            RelayCommand = new RelayCommand(RelayMethod);


            RelayMethod("Init");

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
