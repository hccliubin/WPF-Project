using System;
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



        private StrokeCollection _strokeCollection = new StrokeCollection();
        /// <summary> 说明  </summary>
        public StrokeCollection StrokeCollection
        {
            get { return _strokeCollection; }
            set
            {
                _strokeCollection = value;

                RaisePropertyChanged("StrokeCollection");
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


    public partial class SampleVieModel
    {

        private bool _visible = true;
        /// <summary> 说明  </summary>
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;

                RaisePropertyChanged("Visible");

                _rectangleLayerLayer.IsVisible = value;
            }
        }


        private RectangleLayer _rectangleLayerLayer = new RectangleLayer();
        /// <summary> 说明  </summary>
        public RectangleLayer RectangleLayer
        {
            get { return _rectangleLayerLayer; }
            set
            {
                _rectangleLayerLayer = value;
                RaisePropertyChanged("RectangleLayer");
            }
        }


        private string _name;
        /// <summary> 说明  </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }


        private string _flag;
        /// <summary> 说明  </summary>
        public string Flag
        {
            get { return _flag; }
            set
            {
                _flag = value;
                RaisePropertyChanged("Flag");
            }
        }


        private string _type;
        /// <summary> 说明  </summary>
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                RaisePropertyChanged("Type");
            }
        }


        private string _code;
        /// <summary> 说明  </summary>
        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                RaisePropertyChanged("Code");
            }
        }



        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {


            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }
    }

    partial class SampleVieModel : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public SampleVieModel()
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
