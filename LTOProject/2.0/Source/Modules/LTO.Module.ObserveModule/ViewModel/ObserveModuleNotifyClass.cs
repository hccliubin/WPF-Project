using LTO.Base.Frame.MVVM;
using LTO.Base.Theme.Style;
using LTO.Domain.DataService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LTO.Module.ObserveModule
{

    partial class ObserveModuleNotifyClass
    {

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


        private string _step1;
        /// <summary> 说明  </summary>
        public string Step1
        {
            get { return _step1; }
            set
            {
                _step1 = value;
                RaisePropertyChanged("Step1");
            }
        }


        private string _step2;
        /// <summary> 说明  </summary>
        public string Step2
        {
            get { return _step2; }
            set
            {
                _step2 = value;
                RaisePropertyChanged("Step2");
            }
        }


        private string _step3;
        /// <summary> 说明  </summary>
        public string Step3
        {
            get { return _step3; }
            set
            {
                _step3 = value;
                RaisePropertyChanged("Step3");
            }
        }


        private string _step4;
        /// <summary> 说明  </summary>
        public string Step4
        {
            get { return _step4; }
            set
            {
                _step4 = value;
                RaisePropertyChanged("Step4");
            }
        }





        private List<UserControl> _controls = new List<UserControl>();
        /// <summary> 说明  </summary>
        public List<UserControl> Controls
        {
            get { return _controls; }
            set
            {
                _controls = value;
                RaisePropertyChanged("Controls");
            }
        }

        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Test")
            {
                //var collection = ObserveModeleDomain.Instance.GetChildObserveModel();

                //ObserveItemControl itemcontrol = null;

                //ObservableCollection<ChildObserveModel> temp = new ObservableCollection<ChildObserveModel>();

                //for (int i = 0; i < collection.Count; i++)
                //{
                //    var c = collection[i];

                //    if (i % 20 == 0)
                //    {
                //        itemcontrol = new ObserveItemControl();
                //        temp = new ObservableCollection<ChildObserveModel>();
                //        itemcontrol.ChildObserves = temp;
                //        this.Controls.Add(itemcontrol);
                //    }

                //    temp.Add(c);
                //}
            }
            //  Do：取消
            else if (command == "Loaded")
            {

                Action<string, string> scanEventAction = (o, n) =>
                   {
                       if (o == null && !string.IsNullOrEmpty(n))
                       {
                           MessageSingleControl.ShowWithError("未识别卡片信息", "请检查卡片是否正确并放置在指定扫描区");
                           ServiceManager.DataService.LogInfo(o);
                           ServiceManager.DataService.LogInfo(n);
                           return;
                       }

                       Action<string, string, string> action = (l, k, m) =>
                       {
                           MessageSingleControl.ShowWithSuccess(k, m);
                           ServiceManager.DataService.LogInfo(l, k, m);
                       };

                       ObserveModeleDomain.Instance.PostGetChildInfo(o, action);

                   };

                 ObserveModeleDomain.Instance.BegionGetChildID(scanEventAction);

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


                //定时刷新
                Action action1 = () =>
                {
                    //  Do：刷新统计信息
                    string err;

                    var result = ObserveModeleDomain.Instance.PostGetTotal(out err);

                    if (result == null)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if (!MessageSingleControl.Instance.IsShow)
                            {
                                MessageSingleControl.Show(err);
                            }
                        });
                        return;
                    }

                    this.Step1 = result.Item1;
                    this.Step2 = result.Item2;
                    this.Step3 = result.Item3;
                    this.Step4 = result.Item4;

                    //  Do：刷新列表信息
                    var collection = ObserveModeleDomain.Instance.GetChildObserveModel(out err);

                    if (collection == null)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            if (!MessageSingleControl.Instance.IsShow)
                            {
                                MessageSingleControl.Show(err);
                            }

                        });

                        return;
                    }

                    this.Controls.Clear();

                    ObserveItemControl itemcontrol = null;

                    ObservableCollection<ChildObserveModel> temp = new ObservableCollection<ChildObserveModel>();

                    List<UserControl> controls = new List<UserControl>();


                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        for (int i = 0; i < collection.Count; i++)
                        {
                            var c = collection[i];

                            if (i % 20 == 0)
                            {
                                itemcontrol = new ObserveItemControl();
                                temp = new ObservableCollection<ChildObserveModel>();
                                itemcontrol.ChildObserves = temp;
                                controls.Add(itemcontrol);
                            }

                            temp.Add(c);
                        }

                        this.Controls = controls;
                    });

                };


                Task.Run(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(3000);
                        action1();
                    }
                });

            }
        }
    }

    partial class ObserveModuleNotifyClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public ObserveModuleNotifyClass()
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
