
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace HEW.Module.PhysicalExamination
{
    partial class QuetionListViewModel
    {

        ObservableCollection<QuetionViewModelcs> _models = new ObservableCollection<QuetionViewModelcs>();

        /// <summary> 体检项集合 </summary>
        public ObservableCollection<QuetionViewModelcs> Models
        {
            get
            {
                return _models;
            }
            set
            {
                _models = value;
                RaisePropertyChanged("Models");
            }
        }

        QuetionViewModelcs _current;

        /// <summary> 手机号码 </summary>
        public QuetionViewModelcs Current
        {
            get { return _current; }
            set
            {
                _current = value;
            }
        }
    

        #region - 属性 -

        private string _guidance;
        /// <summary> 指导 </summary>
        public string Guidance
        {
            get { return _guidance; }
            set { _guidance = value; }
        }

        private string _otherGuidance;
        /// <summary> 其他指导 </summary>
        public string OtherGuidance
        {
            get { return _otherGuidance; }
            set { _otherGuidance = value; }
        }


        #endregion

        #region - 命令 -

        //DelegateCommand _sumitClick = new DelegateCommand();
        ///// <summary> 提交 </summary>
        //public DelegateCommand SumitClick
        //{
        //    get
        //    {
        //        if (_sumitClick.Excute == null)
        //        {
        //            _sumitClick.Excute = () =>
        //            {
        //                // Do ：没有选择选项 
        //                var unVisble = this.Models.Where(l => l.Collection.ToList().TrueForAll(k => k.IsChecked == false));

        //                if (unVisble == null || unVisble.ToList().Count == 0)
        //                {
        //                    // Do ：提交 

        //                    WaitingBox.Show(() => Thread.Sleep(2000), "提交中...");

        //                    LogProviderHandler.Instance.OnRunActionLog("中医体质辨识提交成功！");
        //                }
        //                else
        //                {
        //                    Action action = () =>
        //                      {
        //                          this.Current = unVisble.ToList()[0];
        //                      };

        //                    string str = string.Format("转到[{0}]题", unVisble.ToList()[0].Index);

        //                    Tuple<string, Action> t = new Tuple<string, Action>(str, action);

        //                    MessageWindow.ShowDialog("存在未选择的题目", "提交失败！", t);
        //                }

        //            };
        //        }
        //        return _sumitClick;
        //    }
        //}

        #endregion


    }

    partial class QuetionListViewModel : INotifyPropertyChanged
    {
        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }


}
