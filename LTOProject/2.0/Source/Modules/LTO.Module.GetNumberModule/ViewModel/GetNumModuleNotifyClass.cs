using LTO.Base.Frame.MVVM;
using LTO.Base.Theme.Style;
using LTO.Domain.DataService;
using LTO.General.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZHJK.Library.General.Printer.SPRT;

namespace LTO.Module.GetNumberModule
{

    partial class GetNumModuleNotifyClass
    {


        private bool _isPrint;
        /// <summary> 说明  </summary>
        public bool IsPrint
        {
            get { return _isPrint; }
            set
            {
                _isPrint = value;
                RaisePropertyChanged("IsPrint");
            }
        }

        private string _date;
        /// <summary> 日期  </summary>
        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                RaisePropertyChanged("Date");
            }
        }


        private string _printDate;
        /// <summary> 说明  </summary>
        public string PrintDate
        {
            get { return _printDate; }
            set
            {
                _printDate = value;
                RaisePropertyChanged("PrintDate");
            }
        }


        private string _printName;
        /// <summary> 说明  </summary>
        public string PrintName
        {
            get { return _printName; }
            set
            {
                _printName = value;
                RaisePropertyChanged("PrintName");
            }
        }


        private string _printNumber;
        /// <summary> 说明  </summary>
        public string PrintNumber
        {
            get { return _printNumber; }
            set
            {
                _printNumber = value;
                RaisePropertyChanged("PrintNumber");
            }
        }


        private string _printAddress;
        /// <summary> 说明  </summary>
        public string PrintAddress
        {
            get { return _printAddress; }
            set
            {
                _printAddress = value;
                RaisePropertyChanged("PrintAddress");
            }
        }



        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Test")
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    string err;

                    bool result = GetNumberModuleDomain.Instance.Print(this.PrintAddress, "B045", "张晓小效", this.PrintDate, out err);

                    if (result)
                    {
                        ServiceManager.DataService.LogInfo("打印成功张晓小效");
                    }
                    else
                    {
                        MessageSingleControl.ShowWithSuccess("打印错误！", err);
                    }
                });
            }
            //  Do：取消
            else if (command == "Loaded")
            {

                RelayMethod("Init");

                Action<string, string> scanEventAction = (o, n) =>
                   {
                       if (o == null && !string.IsNullOrEmpty(n))
                       {
                           MessageSingleControl.ShowWithError("未识别卡片信息", "请检查卡片是否正确并放置在指定扫描区");
                           ServiceManager.DataService.LogInfo(o);
                           ServiceManager.DataService.LogInfo(n);
                           return;
                       }

                       Action<RegisterEntity, string, string> action = (l, k, m) =>
                       {
                           if (l == null) return;

                           MessageSingleControl.ShowWithSuccess(k, m);

                           ServiceManager.DataService.LogInfo(l.code, k, m);

                           if (l.code != "200") return;

                           if (GetNumberModuleDomain.Instance.GetConfigPrintLimit() != "1") return;

                           ServiceManager.DataService.LogInfo("开始打印" + k);

                           Application.Current.Dispatcher.Invoke(() =>
                           {
                               string err;

                               bool result = GetNumberModuleDomain.Instance.Print(this.PrintAddress, l.rowNum, l.hzxm, this.PrintDate, out err);

                               if (result)
                               {
                                   ServiceManager.DataService.LogInfo("打印成功！" + l.hzxm);
                               }
                               else
                               {
                                   MessageSingleControl.ShowWithSuccess("打印错误！", err);
                               }
                           });


                       };

                       //mykh=undefined&type=1&no=b1

                       string type = "0";
                       string no = null;
                       string code = o.Trim();
                       string prepayid = null;

                       ServiceManager.DataService.LogInfo(o);

                       if (o.Trim().StartsWith("mykh"))
                       {
                           var result = o.Trim().Split('=', '&');

                           code = result[0];
                           type = result[1];
                           prepayid= result[2];
                           no = result[3];
                       }

                       GetNumberModuleDomain.Instance.PostGetChildInfo(code, type, no, action);


                   };

                GetNumberModuleDomain.Instance.BegionGetChildID(scanEventAction);

            }

            else if (command == "Init")
            {

                //星期二 2018 - 08 - 01 15:13
                System.Timers.Timer time = new System.Timers.Timer();

                Action action = () =>
                {
                    this.Date = string.Format(("星期{0} {1}"), "日一二三四五六".Substring((int)DateTime.Now.DayOfWeek, 1), DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

                };

                action();

                time.Elapsed += (l, k) =>
                {
                    action();
                };

                time.Interval = 1000 * 30;

                time.Start();


                this.PrintAddress = GetNumberModuleDomain.Instance.GetConfigAddress();

                this.PrintDate = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
    }

    partial class GetNumModuleNotifyClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public GetNumModuleNotifyClass()
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
