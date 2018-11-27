﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;
using System.Windows.Media;
using Ty.Base.WpfBase;

namespace Ty.Component.ImageControl
{
    public partial class ImageControlViewModel
    {
        private ImageSource _imageSource;
        /// <summary> 说明  </summary>
        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                RaisePropertyChanged("ImageSource");
            }
        }

        private ObservableCollection<SampleVieModel> _samplecollection = new ObservableCollection<SampleVieModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<SampleVieModel> SampleCollection
        {
            get { return _samplecollection; }
            set
            {
                _samplecollection = value;
                RaisePropertyChanged("SampleCollection");
            }
        }



        private Dictionary<string, string> _codeCollection=new Dictionary<string, string>();
        /// <summary> 说明  </summary>
        public Dictionary<string, string> CodeCollection
        {
            get { return _codeCollection; }
            set
            {
                _codeCollection = value;
                RaisePropertyChanged("CodeCollection");
            }
        }


        private Dictionary<string, string> _figureCollection=new Dictionary<string, string>();
        /// <summary> 说明  </summary>
        public Dictionary<string, string> FigureCollection
        {
            get { return _figureCollection; }
            set
            {
                _figureCollection = value;
                RaisePropertyChanged("FigureCollection");
            }
        }



        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "text")
            {
                this.SampleCollection.Clear();
                for (int i = 0; i < 10; i++)
                {
                    SampleVieModel sample = new SampleVieModel();

                    sample.Name = "Name" + i;
                    sample.Flag = i % 3 == 0 ? "\xeac5" : "\xeac3";
                    sample.Code = "Code" + i;

                    this.SampleCollection.Add(sample);
                }
            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }

    }

    partial class ImageControlViewModel : INotifyPropertyChanged
    {

        public RelayCommand RelayCommand { get; set; }

        public ImageControlViewModel()
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
