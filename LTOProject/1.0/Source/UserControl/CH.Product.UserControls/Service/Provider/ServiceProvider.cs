#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/4/4 9:48:28 
 * 文件名：ServiceProvider 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using CH.Product.Base.Model;
using CH.Product.Domain.DataService.Service;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Timers;

namespace CH.Product.UserControls
{
    class ServiceProvider
    {
        //Timer time = new Timer(1000);
        Timer time = new Timer(60000);
        public ServiceProvider()
        {
            time.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                this.RefreshLoad();
            };
        }
        public void Start(LeaveToObserveEngineViewModel viewModel)
        {
            _viewModel = viewModel;

            this.RefreshLoad();

            time.Start();
        }

        public static ServiceProvider Instance = new ServiceProvider();

        /// <summary> 加载列表 </summary>
        public List<LeaveToObserveItemViewModel> GetList()
        {
            List<LeaveToObserveItemViewModel> collection = new List<LeaveToObserveItemViewModel>();

            string err;

            var childs = DataService.Instance.GetList(out err);

            if (childs == null)
            {
                MessageProvider.Instance.ShowWith(err);
                return null;
            }

            foreach (var item in childs)
            {
                LeaveToObserveItemViewModel it = new LeaveToObserveItemViewModel();
                it.Seq = item.lgId;
                it.Name = item.hzxm;
                it.Sex = item.xb;
                it.StartTime = item.yfjzsj;
                it.CountDown = DataService.Instance.GetDownCount();
                it.State = item.lgzt;
                it.LeaveTime = item.yfjzsj.ToDateTime().AddSeconds(it.CountDown).ToDateTimeString("HH:mm:ss");
                it.CountDown = item.second;

                //var longtime=  DataService.Instance.GetDownCount(item.yfjzsj);

                // if (longtime < 0)
                // {
                //     it.CountDown = 0;
                // }
                // else
                // {
                //     it.CountDown = longtime;
                // }

                collection.Add(it);
            }

            return collection;
        }


        /// <summary> 再去后台数据库读取儿童信息 </summary>
        ChildInfo GetChild(string code, out string err)
        {
            return DataService.Instance.GetChild(code, out err);
        }

        /// <summary> 设置完成 </summary>
        public void SetChildState(string id)
        {
            string errMsg;

            bool result = DataService.Instance.SetChildState(id, out errMsg);

            if (!result)
            {
                DataService.Instance.LogWithSpeech(errMsg);
            }
        }

        LeaveToObserveEngineViewModel _viewModel;

        object _lock = new object();

        bool flag;
        /// <summary> 执行工作流 </summary>
        public void DoWork(string str, LeaveToObserveEngineViewModel viewModel)
        {
            _viewModel = viewModel;

            string err;

            if (flag)
            {
                MessageProvider.Instance.ShowWithLog("正在处理，请稍等!");
                return;
            }

            lock (_lock)
            {
               

                flag = true;


                try
                {
                    ChildInfo child = this.GetChild(str, out err);

                    if (child == null)
                    {
                        MessageProvider.Instance.ShowWithLog(err);
                        return;
                    }
                    else
                    {
                        MessageProvider.Instance.ShowWithLog(err, 3);

                        this._viewModel.AddMessage(err);

                        //刷新列表
                        this.RefreshLoad();

                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    flag = false;
                }

            }

        }

        void RefreshLoad()
        {
            var colletion = this.GetList();

            if (colletion == null) return;

            Application.Current.Dispatcher.Invoke(() =>
            {
                _viewModel.Clear();

                foreach (var item in colletion)
                {
                    _viewModel.AddItem(item);
                }

                _viewModel.RefreshData();
            });




            //Application.Current.Dispatcher.Invoke(() =>
            //{
            //    //_viewModel.Clear();


            //    _viewModel.AddItem(new LeaveToObserveItemViewModel() { Name = DateTime.Now.ToString("HH:mm:ss"), CountDown = 44 });

            //    _viewModel.RefreshData();
            //});

        }
    }
}
