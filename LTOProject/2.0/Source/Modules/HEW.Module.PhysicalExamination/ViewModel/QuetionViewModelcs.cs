using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HEW.Module.PhysicalExamination
{
  
    public partial class QuetionViewModelcs
    {
        private string _index = "1";

        /// <summary> 手机号码 </summary>
        public string Index
        {
            get { return _index; }
            set
            {
                _index = value;
                RaisePropertyChanged("Index");
            }
        }

        private string _quetion = "你精神充沛吗？";

        /// <summary> 手机号码 </summary>
        public string Quetion
        {
            get { return string.Format(_quetion,this._index); }
            set
            {
                _quetion = value;
                RaisePropertyChanged("Quetion");
            }
        }

        private string _detail= "指精神头足乐于做事";

        /// <summary> 手机号码 </summary>
        public string Detail
        {
            get { return _detail; }
            set
            {
                _detail = value;
                RaisePropertyChanged("Detail");
            }
        }

      
        private ObservableCollection<AnswerViewModel> _collection=new ObservableCollection<AnswerViewModel>();
        /// <summary> 说明 </summary>
        public ObservableCollection<AnswerViewModel> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }
      
    }

    partial class QuetionViewModelcs : INotifyPropertyChanged
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
