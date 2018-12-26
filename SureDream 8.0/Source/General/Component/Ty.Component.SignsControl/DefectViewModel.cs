using CDTY.DataAnalysis.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Ty.Base.WpfBase;

namespace Ty.Component.SignsControl
{


   public partial class DefectViewModel
    {

        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "init")
            {


            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }
    }

    partial class DefectViewModel : IDefectSign
    {
        public event Action<string> ConfirmData;


        private DefectMenuEntity _defectMenuEntity;
        /// <summary> 说明  </summary>
        public DefectMenuEntity DefectMenuEntity
        {
            get { return _defectMenuEntity; }
            set
            {
                _defectMenuEntity = value;
                RaisePropertyChanged("DefectMenuEntity");
            }
        }


        private ObservableCollection<TyeEncodeCategoryconfigEntity> _dataAcquisitionMode = new ObservableCollection<TyeEncodeCategoryconfigEntity>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TyeEncodeCategoryconfigEntity> DataAcquisitionMode
        {
            get { return _dataAcquisitionMode; }
            set
            {
                _dataAcquisitionMode = value;
                RaisePropertyChanged("DataAcquisitionMode");
            }
        }


        private ObservableCollection<DefectCommonUsed> _histCollection = new ObservableCollection<DefectCommonUsed>();
        /// <summary> 说明  </summary>
        public ObservableCollection<DefectCommonUsed> HistCollection
        {
            get { return _histCollection; }
            set
            {
                _histCollection = value;
                RaisePropertyChanged("HistCollection");
            }
        }
        


        public void Load(DefectMenuEntity entity)
        {
            DefectMenuEntity = entity;

            foreach (var item in entity.DataAcquisitionMode)
            {
                this.DataAcquisitionMode.Add(item);

            }

            //  Do：加载最近使用和历史次数最多的

            var useCount= entity.CommonHistoricalDefectsOrMark.OrderByDescending(l => l.CountUse).Take(10);

            this.HistCollection.Clear();

            foreach (var item in useCount)
            {
                this.HistCollection.Add(item);
            }
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

    partial class DefectViewModel : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public DefectViewModel()
        {
            RelayCommand = new RelayCommand(RelayMethod);
            RelayMethod("init");

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
