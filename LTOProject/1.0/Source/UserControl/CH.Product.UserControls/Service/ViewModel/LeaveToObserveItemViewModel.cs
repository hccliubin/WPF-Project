#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/4/2 16:08:43 
 * 文件名：Class1 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using CH.Product.Domain.DataService.Service;
using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;

namespace CH.Product.UserControls
{

    /// <summary> 说明 </summary>
    partial class LeaveToObserveItemViewModel
    {
        string _id;
        /// <summary> 说明 </summary>
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;

                RaisePropertyChanged();
            }
        }

        string _seq;
        /// <summary> 编号 自己处理取后三位 </summary>
        public string Seq
        {
            get
            {
                return _seq;
            }
            set
            {
                _seq = value;

                RaisePropertyChanged();
            }
        }

        string _name;
        /// <summary> 病人名字 </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;

                RaisePropertyChanged();
            }
        }

        string _sex;
        /// <summary> 性别 </summary>
        public string Sex
        {
            get
            {
                return _sex;
            }
            set
            {

                _sex = value=="1"?"男":value=="2"?"女":"未知";

                RaisePropertyChanged();
            }
        }

        //string _status;
        ///// <summary> 说明 </summary>
        //public string Status
        //{
        //    get
        //    {
        //        return _status;
        //    }
        //    set
        //    {
        //        _status = value;

        //        Action action = () =>
        //          {
        //              if (value == "正在留观")
        //              {
        //                  this.Color = Brushes.Green;
        //              }
        //              else if (value == "登记成功")
        //              {
        //                  this.Color = Brushes.Blue;
        //              }
        //              else if (value == "留观结束")
        //              {
        //                  this.Color = Brushes.Gray;
        //              }
        //          };

        //        Application.Current.Dispatcher.Invoke(action);


        //        RaisePropertyChanged();
        //    }
        //}
        //LeaveState _state;
        ///// <summary> 说明 </summary>
        //public LeaveState State
        //{
        //    get
        //    {
        //        return _state;
        //    }
        //    set
        //    {
        //        _state = value;

        //        this.Status = _state.GetAttribute<DescriptionAttribute>().Description;


        //        // Todo ：触发事件 
        //        if(this.ValueChanged!=null)
        //        {
        //            this.ValueChanged(_state);
        //        }

        //        RaisePropertyChanged();
        //    }
        //}
        string _createtime;
        /// <summary> 倒计时 </summary>
        public string CreateTime
        {
            get
            {
                return _createtime;
            }
            set
            {
                    _createtime = value;

                //// Todo ：触发事件 
                //if (this.ValueChanged != null)
                //{
                //    this.ValueChanged(value);
                //}

                RaisePropertyChanged();
            }
        }

        private long _countDown;

        public long CountDown
        {
            get { return _countDown; }
            set
            {
                _countDown = value;

                if (_countDown < 0)
                {
                    this.CreateTime = "00:00";
                    return;
                }

                TimeSpan time = TimeSpan.FromSeconds(value);

                if (time.Hours != 0)
                {
                    this.CreateTime = string.Format("{0}:{1}:{2}", time.Hours.ToString().PadLeft(2,'0')
                        , time.Minutes.ToString().PadLeft(2, '0'), time.Seconds.ToString().PadLeft(2, '0'));
                }
                else
                {
                    this.CreateTime = string.Format("{0}:{1}", time.Minutes.ToString().PadLeft(2, '0'), time.Seconds.ToString().PadLeft(2, '0'));
                }

            }
        }


        //string _animate;
        ///// <summary> 说明 </summary>
        //public string Animate
        //{
        //    get
        //    {
        //        return _animate;
        //    }
        //    set
        //    {
        //        _animate = value;

        //        RaisePropertyChanged();
        //    }
        //}
        //string _vaccinename;
        ///// <summary> 所打的疫苗种类 </summary>
        //public string VaccineName
        //{
        //    get
        //    {
        //        return _vaccinename;
        //    }
        //    set
        //    {
        //        _vaccinename = value;

        //        RaisePropertyChanged();
        //    }
        //}

        string _leavetime;
        /// <summary> 留观结束时间 </summary>
        public string LeaveTime
        {
            get
            {
                return _leavetime;
            }
            set
            {
                _leavetime = value;

                RaisePropertyChanged();
            }
        }

        private string _startTime;
        /// <summary> 说明 </summary>
        public string StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                RaisePropertyChanged();
            }
        }

        private SolidColorBrush _color = new SolidColorBrush(Colors.Red);
        /// <summary> 显示颜色 </summary>
        public SolidColorBrush Color
        {
            get { return _color; }
            set
            {
                _color = value;
                RaisePropertyChanged();
            }
        }

        public LeaveToObserveItemViewModel()
        {
            this.CountDown = DataService.Instance.GetDownCount();

            long auto = DataService.Instance.GetAutoLeaveDownCount();

            //this.RelayCommand = new RelayCommand(RelayMethod);

            time.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                long value = this.CountDown;

                if (value < 1)
                {
                    time.Interval = 60000;

                    //小于一个小时
                    if (value < -auto)
                    {
                        time.Stop();

                        //设置结束
                        ServiceProvider.Instance.SetChildState(this.Seq);
                    }

                    this.CountDown = this.CountDown - 60000;

                }
                else
                {

                    this.CountDown--;
                }

            };

            time.Start();

            //this.State = LeaveState.Running;
        }

        //public RelayCommand RelayCommand { get; set; }


        /// <summary> 此方法的说明 </summary>
        //public void RelayMethod(object obj)
        //{
        //    //if (obj == null) return;

        //    //string str = obj.ToString();

        //    //if (str == "SumitLeave")
        //    //{
        //    //    if (this.Status == "确认留观")
        //    //    {
        //    //        this.Status = "正在留观";

        //    //        time.Start();
        //    //    }
        //    //}

        //    //System.Diagnostics.Debug.WriteLine(str);
        //}

        Timer time = new Timer(1000);


        //public event Action<object> ValueChanged;


        private string _state;
        /// <summary> 留观状态 </summary>
        public string State
        {
            get { return _state; }
            set
            {
                _state = value;
                RaisePropertyChanged();
            }
        }


    }

    partial class LeaveToObserveItemViewModel : INotifyPropertyChanged
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


    /// <summary> 说明 </summary>
    enum LeaveState
    {
        [Description("登记成功")]
        UnStart = 0,
        [Description("正在留观")]
        Running,
        [Description("留观结束")]
        Ending
    }

}
