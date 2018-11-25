using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ty.Component.ImageControl
{
    /// <summary>
    /// ImageView.xaml 的交互逻辑
    /// </summary>
    public partial class ImageView : UserControl
    {
        public ImageView()
        {
            InitializeComponent();
        }

        public ImageControlViewModel ViewModel
        {
            get
            {
                return (ImageControlViewModel)this.DataContext;
            }
            set
            {
                this.DataContext = value;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            SampleVieModel sample = new SampleVieModel();
            sample.Name = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            sample.Code = sample.Name;

            RectangleShape resultStroke;

            if (this.r_defect.IsChecked.HasValue && this.r_defect.IsChecked.Value)
            {
                resultStroke = new DefectShape(this._dynamic);
                sample.Flag = "\xeac4";
            }
            else
            {
                resultStroke = new SampleShape(this._dynamic);
                sample.Flag = "\xeac5";
            }

            resultStroke.Draw(this.canvas);

            sample.RectangleLayer.Add(resultStroke);

            this.ViewModel.SampleCollection.Add(sample);

            //_dynamic.Clear(this.canvas);

            //_dynamic.Visibility = Visibility.Visible;

            this.popup.IsOpen = false;
        }

        Point start;

        private void InkCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isMatch = false;

            start = e.GetPosition(sender as InkCanvas);

            System.Diagnostics.Debug.WriteLine("说明");

        }

        bool _isMatch = false;

        private void InkCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;

            if (this.start.X<0) return;

            Point end = e.GetPosition(this.canvas);

            this._isMatch = Math.Abs(start.X - end.X) > 50 && Math.Abs(start.Y - end.Y) > 50;

            _dynamic.Visibility = Visibility.Visible;
            _dynamic.Refresh(start, end);

        }


        public DynamicShape DynamicShape
        {
            get { return (DynamicShape)GetValue(DynamicShapeProperty); }
            set { SetValue(DynamicShapeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DynamicShapeProperty =
            DependencyProperty.Register("DynamicShape", typeof(DynamicShape), typeof(ImageView), new PropertyMetadata(default(DynamicShape), (d, e) =>
             {
                 ImageView control = d as ImageView;

                 if (control == null) return;

                 DynamicShape config = e.NewValue as DynamicShape;

             }));

        public Visual ImageVisual
        {
            get { return (Visual)GetValue(ImageVisualProperty); }
            set { SetValue(ImageVisualProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageVisualProperty =
            DependencyProperty.Register("ImageVisual", typeof(Visual), typeof(ImageView), new PropertyMetadata(default(Visual), (d, e) =>
             {
                 ImageView control = d as ImageView;

                 if (control == null) return;

                 Visual config = e.NewValue as Visual;

             }));

        //声明和注册路由事件
        public static readonly RoutedEvent BegionShowPartViewRoutedEvent =
            EventManager.RegisterRoutedEvent("BegionShowPartView", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ImageView));
        //CLR事件包装
        public event RoutedEventHandler BegionShowPartView
        {
            add { this.AddHandler(BegionShowPartViewRoutedEvent, value); }
            remove { this.RemoveHandler(BegionShowPartViewRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnBegionShowPartView()
        {
            RoutedEventArgs args = new RoutedEventArgs(BegionShowPartViewRoutedEvent, this);
            this.RaiseEvent(args);
        }

        private void InkCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_isMatch)
            {
                _dynamic.Visibility = Visibility.Collapsed;
                return;
            };

            _isMatch = false;

            if (this.r_screen.IsChecked.HasValue && this.r_screen.IsChecked.Value)
            {

                Image im;

                this.rectangle_clip.Visibility = Visibility.Visible;

                this.rectangle_clip.Width = this.canvas.ActualWidth;

                this.rectangle_clip.Height = this.canvas.ActualHeight;

                RectangleGeometry rect = new RectangleGeometry(new Rect(0, 0, this.canvas.Width, this.canvas.Height));

                var geo = Geometry.Combine(this.rectangle_clip.RenderedGeometry, new RectangleGeometry(this._dynamic.Rect), GeometryCombineMode.Exclude, null);
                //var geo = Geometry.Combine(this.rectangle_clip.RenderedGeometry, new RectangleGeometry(this._dynamic.Rect), GeometryCombineMode.Exclude, null);

                DynamicShape shape = new DynamicShape(this._dynamic);

                this.DynamicShape = shape;

                this.ImageVisual = this.canvas;

               

                this.OnBegionShowPartView();

                //this.ShowScreen();

                //this.VisualBrush.Viewbox = this._dynamic.Rect;

                this.rectangle_clip.Clip = geo;

                _dynamic.Visibility = Visibility.Collapsed;

            }
            else
            {
                this.popup.IsOpen = true;
            }


            start = new Point(-1,-1);

        }

        public void RefreshData()
        {
            this.rectangle_clip.Visibility = Visibility.Collapsed;
        }

        public void Clear()
        {
            this._dynamic.Visibility = Visibility.Collapsed;

            foreach (var sample in this.ViewModel.SampleCollection)
            {
                foreach (var item in sample.RectangleLayer)
                {
                    item.Clear(this.canvas);
                }
            }

            this.RefreshData();


        }
        
    }
}
