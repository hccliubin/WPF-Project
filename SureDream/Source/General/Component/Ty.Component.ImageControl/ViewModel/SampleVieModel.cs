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
    public partial class SampleVieModel
    {
        ImgMarkEntity _model = new ImgMarkEntity();

        public SampleVieModel(ImgMarkEntity imgMarkEntity)
        {
            RelayCommand = new RelayCommand(RelayMethod);

            Model = imgMarkEntity;

            this.Flag = "\xe76c";

            this.Name = imgMarkEntity.Name;
            this.Code = imgMarkEntity.Code;
            this.Type = "0";

            DefectShape defect = new DefectShape(imgMarkEntity.X, imgMarkEntity.Y, imgMarkEntity.Width, imgMarkEntity.Height);

            this.RectangleLayer.Add(defect);

            //sample.Flag = "\xeac5";
        }

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

        public void Add(RectangleShape shape)
        {
            this._model.X = (int)shape.Position.X;
            this._model.Y = (int)shape.Position.Y;

            this._model.Width = (int)shape.Width;
            this._model.Height = (int)shape.Height;

            this.RectangleLayer.Add(shape);
        }

        /// <summary> 说明  </summary>
        public string Name
        {
            get { return this.Model.Name; }
            set
            {
                this.Model.Name = value;
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

        
        /// <summary> 说明  </summary>
        public string Code
        {
            get { return _model.Code; }
            set
            {
                _model.Code = value;
                RaisePropertyChanged("Code");
            }
        }

        public ImgMarkEntity Model { get => _model; set => _model = value; }

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
