#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/4/2 16:13:16 
 * 文件名：Class1 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CH.Product.UserControls
{

    /// <summary> 说明 </summary>
    partial class LeaveToObserveEngineViewModel
    {

        private ObservableCollection<LeaveToObserveItemViewModel> _collection = new ObservableCollection<LeaveToObserveItemViewModel>();
        /// <summary> 说明 </summary>
        public ObservableCollection<LeaveToObserveItemViewModel> Collection
        {
            get { return _collection; }
            set
            {
                if (_collection != value)
                {
                    _collection = value;
                    RaisePropertyChanged();
                }

            }
        }

        private ObservableCollection<string> _message = new ObservableCollection<string>();
        /// <summary> 提示信息 </summary>
        public ObservableCollection<string> Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }

        public void AddMessage(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);

            Application.Current.Dispatcher.Invoke(()=> this.Message.Insert(0, message));
        }

        private string _currentCode;
        /// <summary> 读到的编码 </summary>
        public string CurrentCode
        {
            get { return _currentCode; }
            set
            {
                _currentCode = value;
                RaisePropertyChanged();
            }
        }



        private LeaveToObserveItemViewModel _current;
        /// <summary> 说明 </summary>
        public LeaveToObserveItemViewModel Current
        {
            get { return _current; }
            set
            {
                _current = value;
                RaisePropertyChanged();
            }
        }



        public int _total;
        public int Total
        {
            get
            {
                return _total;
            }
            set
            {
                if (_total != value)
                {
                    _total = value;
                    RaisePropertyChanged();
                }
            }
        }


        private Visibility _isBuzy = Visibility.Hidden;
        /// <summary> 说明 </summary>
        public Visibility IsBuzy
        {
            get { return _isBuzy; }
            set
            {
                _isBuzy = value;
                RaisePropertyChanged();
            }
        }


        public LeaveToObserveEngineViewModel()
        {

            ScanningPrivder.Instance.CallBackScanning += Instance_CallBackScanning;
            ScanningPrivder.Instance.StartEngine();
            // Todo ：加载数据 
            Action action = () =>
            {
                this.IsBuzy = Visibility.Visible;

                // Todo ：注册扫描枪钩子 
           

            ServiceProvider.Instance.Start(this);

                this.IsBuzy = Visibility.Hidden;
            };

            action.DoTask();


            // Todo ：注册系统时间 
            ShowTimer = new System.Windows.Threading.DispatcherTimer();
            ShowTimer.Tick += (object sender, EventArgs e) =>
            {
                this.SystemTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            };
            ShowTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            ShowTimer.Start();

            //// Todo ：注册集合改变事件 
            //_collection.CollectionChanged += (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) =>
            //{
            //    this.RefreshData();
            //};

        }


        private DispatcherTimer ShowTimer;

        public void Instance_CallBackScanning(string obj)
        {
            Action action = () =>
              {
                  this.IsBuzy = Visibility.Visible;

                  this.CurrentCode = obj;

                  ServiceProvider.Instance.DoWork(obj, this);

                  this.CurrentCode = string.Empty;

                  this.IsBuzy = Visibility.Hidden;
              };

            action.DoTask();

        }

        private string _leaveCount;
        /// <summary> 留观人数 </summary>
        public string LeaveCount
        {
            get { return _leaveCount; }
            set
            {
                _leaveCount = value;
                RaisePropertyChanged();
            }
        }


        private string _inoculateCount;
        /// <summary> 接种人数 </summary>
        public string InoculateCount
        {
            get { return _inoculateCount; }
            set
            {
                _inoculateCount = value;
                RaisePropertyChanged();
            }
        }

        private string _systemTime;
        /// <summary> 说明 </summary>
        public string SystemTime
        {
            get { return _systemTime; }
            set
            {
                _systemTime = value;
                RaisePropertyChanged();
            }
        }


        /// <summary> 刷新数据 </summary>
        public void RefreshData()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {

                try
                {
                    // Todo ：刷新 
                    if (this.Collection == null) return;

                    this.InoculateCount = this.Collection.ToList().Count.ToString();

                    //this.LeaveCount = this.Collection.ToList().FindAll(l => l.State == LeaveState.Running).Count.ToString();

                    this.LeaveCount = this.Collection.ToList().FindAll(l => l.State == "0").Count.ToString();
                }
                catch
                {

                }
            }
                //// Todo ：排序 
                //for (int i = 0; i < this.Collection.Count - 1; i++)
                //{
                //    for (int j = 0; j < this.Collection.Count - 1 - i; j++)
                //    {
                //        if (this.Collection[j].CountDown > this.Collection[j + 1].CountDown)
                //        {
                //            var temp = this.Collection[j];
                //            this.Collection[j] = this.Collection[j + 1];
                //            this.Collection[j + 1] = temp;
                //        }
                //    }
                //}

                
            );
         

        }


        /// <summary> 增加项 </summary>
        public void AddItem(params LeaveToObserveItemViewModel[] items)
        {
            foreach (var item in items)
            {
                //// Todo ：注册值改变刷新数据 
                //item.ValueChanged += l =>
                //{
                //    this.RefreshData();
                //};

                this.Collection.Add(item);
            }

            this.RefreshData();
        }

        public void Clear()
        {
            //foreach (var item in this.Collection)
            //{
            //    //// Todo ：注册值改变刷新数据 
            //    //item.ValueChanged -= l =>
            //    //{
            //    //    this.RefreshData();
            //    //};
            //}

            this.Collection.Clear();
        }
    }
        

    partial class LeaveToObserveEngineViewModel : INotifyPropertyChanged
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
