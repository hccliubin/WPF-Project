using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ty.Base.WpfBase;

namespace Ty.Component.ImageControl
{
    /// <summary>
    /// ImageControl.xaml 的交互逻辑
    /// </summary>
    public partial class ImageControl : UserControl
    {

        ImageControlViewModel _vm = new ImageControlViewModel();
        public ImageControl()
        {
            InitializeComponent();

            this.DataContext = _vm;

            DrawingAttributes draw = new DrawingAttributes();

            draw.Color = Colors.Red;
            draw.IsHighlighter = true;
            draw.IgnorePressure = true;
            draw.StylusTip = StylusTip.Ellipse;
            //draw.FitToCurve = true;
            
            this.canvas.DefaultDrawingAttributes = draw;
        }


        bool isdown = false;

        Point start;

        private void InkCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.canvas.EditingMode != InkCanvasEditingMode.None) return;

            if (e.LeftButton != MouseButtonState.Pressed) return;

            isdown = true;

            _isMatch = false;

            start = e.GetPosition(sender as InkCanvas);

        }

        bool _isMatch = false;

        ImageLayer _drawingLayer = new ImageLayer();

        private void InkCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.canvas.EditingMode != InkCanvasEditingMode.None) return;

            if (e.LeftButton != MouseButtonState.Pressed) return;

            InkCanvas inkcanvas = sender as InkCanvas;

            System.Windows.Point endP = e.GetPosition(inkcanvas);

            this._isMatch = Math.Abs(start.X - endP.X) > 50 && Math.Abs(start.Y - endP.Y) > 5;

            List<System.Windows.Point> pointList = new List<System.Windows.Point>
                    {
                        new System.Windows.Point(start.X, start.Y),
                        new System.Windows.Point(start.X, endP.Y),
                        new System.Windows.Point(endP.X, endP.Y),
                        new System.Windows.Point(endP.X, start.Y),
                        new System.Windows.Point(start.X, start.Y),

                    };

            StylusPointCollection point = new StylusPointCollection(pointList);

            Stroke stroke = new Stroke(point)
            {
                DrawingAttributes = inkcanvas.DefaultDrawingAttributes.Clone()
            };

            Rectangle path = new Rectangle();
            path.Width = Math.Abs(start.X - endP.X);
            path.Height = Math.Abs(start.Y - endP.Y);
            path.Stroke = Brushes.Red;
            //path.Fill = Brushes.Black;

            InkCanvas.SetLeft(path,Math.Min(start.X,endP.X));
            InkCanvas.SetTop(path, Math.Min(start.Y, endP.Y));

         
            DynamicRectangleStroke rs = new DynamicRectangleStroke(start, endP, Brushes.Red);

            rs.Draw(this.canvas);

         

            //this.canvas.Children.Add(path);

            //this.RemoveLayer();

            //_drawingLayer.Clear();
            //_drawingLayer.Add(stroke);

            //this.AddLayer();

        }

        private void InkCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.canvas.EditingMode != InkCanvasEditingMode.None) return;

            if (!_isMatch)
            {
                this.RemoveLayer();

                return;
            };

            MessageBoxResult result = MessageBox.Show("是否保存?", "提示！", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
            {
                this.RemoveLayer();

                return;
            }

            SampleVieModel sample = new SampleVieModel();
            sample.Name = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            sample.Flag = "\xeac4";
            sample.Code = sample.Name;
            sample.ImageLayer.Add(this._drawingLayer.Clone());

            this._vm.AddSample(sample);

            this.RemoveLayer();

            this.isdown = false;

        }

        public void RemoveLayer()
        {
            foreach (var item in _drawingLayer)
            {
                this.canvas.Strokes.Remove(item);
            }
        }

        public void AddLayer()
        {
            foreach (var item in _drawingLayer)
            {
                this.canvas.Strokes.Add(item);
            }
        }
    }



    partial class ImageControlViewModel
    {
        public void AddSample(SampleVieModel model)
        {
            model.VisibleChanged = l =>
             {
                 this.RefreshStrokes();
             };

            this.SampleCollection.Add(model);
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


        void RefreshStrokes()
        {
            this.StrokeCollection.Clear();

            foreach (var sample in this.SampleCollection)
            {
                if (!sample.Visible) continue;

                foreach (var item in sample.ImageLayer)
                {
                    this.StrokeCollection.Add(item);
                }
            }
        }
    }

    partial class ImageControlViewModel : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public ImageControlViewModel()
        {
            RelayCommand = new RelayCommand(RelayMethod);

            //RelayMethod("text");

            this.SampleCollection.CollectionChanged += (l, k) =>
              {
                  this.RefreshStrokes();

              };


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


    partial class SampleVieModel
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

                if (VisibleChanged != null)
                {
                    VisibleChanged(value);
                }
            }
        }

        public Action<bool> VisibleChanged;

        private ImageLayer _imageLayer = new ImageLayer();
        /// <summary> 说明  </summary>
        public ImageLayer ImageLayer
        {
            get { return _imageLayer; }
            set
            {
                _imageLayer = value;
                RaisePropertyChanged("ImageLayer");
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
