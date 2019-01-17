using System;
using System.Collections.Generic;
using System.IO;
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
    /// 图片浏览和标定缺陷控件
    /// </summary>
    public partial class ImageView : UserControl
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ImageView()
        {
            InitializeComponent();


            //  Message：注册鼠标悬停事件，注意删除和新增的时候
            mouseEventHandler = (l, k) =>
            {
                RectangleShape rectangleShape = l as RectangleShape;

                ShowPartWithShape(rectangleShape);
            };

        }

        #region - 依赖属性 -

        /// <summary>
        /// 动态显示的矩形标定范围
        /// </summary>
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

        /// <summary>
        /// 用于显示局部放大的视图
        /// </summary>
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

        #endregion

        #region - 成员变量 -

        /// <summary>
        /// 绑定模型
        /// </summary>
        public ImageControlViewModel ViewModel
        {
            get
            {
                if (this.DataContext is ImageControlViewModel)
                {
                    return (ImageControlViewModel)this.DataContext;
                }

                return null;
            }
            set
            {
                this.DataContext = value;
            }
        }

        Point start;

        #endregion

        #region - 成员方法 -

        /// <summary>
        /// 关闭局部放大时隐藏蒙版
        /// </summary>
        public void HideRectangleClip()
        {
            this.rectangle_clip.Visibility = Visibility.Collapsed;

            this.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 显示局部放大时显示蒙版
        /// </summary>
        public void ShowRectangleClip()
        {
            this.rectangle_clip.Width = this.canvas.ActualWidth;

            this.rectangle_clip.Height = this.canvas.ActualHeight;

            this.rectangle_clip.Visibility = Visibility.Visible;

            this.Visibility = Visibility.Hidden;

        }

        /// <summary>
        /// 上一页、下一页时清理局部放大还有蒙版等页面
        /// </summary>
        public void Clear()
        {
            if (this.ViewModel == null) return;

            //  Do：清理动态形状
            this._dynamic.Visibility = Visibility.Collapsed;

            //  Do：清理所有样本形状
            foreach (var sample in this.ViewModel.SampleCollection)
            {
                foreach (var item in sample.RectangleLayer)
                {
                    item.Clear(this.canvas);
                }

                sample.RectangleLayer.Clear();
            }

            this.ViewModel.SampleCollection.Clear();

            //  Do：隐藏蒙版
            this.HideRectangleClip();

        }

        /// <summary>
        /// 重新刷新绘制所有样本数据
        /// </summary>
        public void RefreshAll()
        {
            foreach (var items in this.ViewModel.SampleCollection)
            {
                foreach (var item in items.RectangleLayer)
                {
                    item.Clear(this.canvas);

                    item.Draw(this.canvas);
                }
            }
        }

        /// <summary>
        /// 隐藏动态框
        /// </summary>
        public void ClearDynamic()
        {
            this._dynamic.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region - 成员事件 -

        //声明和注册路由事件
        public static readonly RoutedEvent BegionShowPartViewRoutedEvent =
            EventManager.RegisterRoutedEvent("BegionShowPartView", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ImageView));
        //CLR事件包装
        public event RoutedEventHandler BegionShowPartView
        {
            add { this.AddHandler(BegionShowPartViewRoutedEvent, value); }
            remove { this.RemoveHandler(BegionShowPartViewRoutedEvent, value); }
        }

        /// <summary>
        /// 显示局部放大视图
        /// </summary>
        protected void OnBegionShowPartView()
        {
            RoutedEventArgs args = new RoutedEventArgs(BegionShowPartViewRoutedEvent, this);
            this.RaiseEvent(args);
        }


        /// <summary>
        /// 菜单点击确定 生成缺陷和样本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SampleVieModel sample = new SampleVieModel();

            sample.Name = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            sample.Code = this.cb_code.Text;

            //  Do：根据选择的样本类型来生成缺陷/样本
            if (this.ImageOprateCtrEntity.MarkType == MarkType.Defect)
            //if (this.r_defect.IsChecked.HasValue && this.r_defect.IsChecked.Value)
            {
                DefectShape resultStroke = new DefectShape(this._dynamic);
                sample.Flag = "\xe688";
                sample.Type = "0";
                resultStroke.Name = sample.Name;
                resultStroke.Code = sample.Code;
                resultStroke.Draw(this.canvas);
                sample.Add(resultStroke);
            }
            else if (this.ImageOprateCtrEntity.MarkType == MarkType.Sample)
            {
                SampleShape resultStroke = new SampleShape(this._dynamic);
                sample.Flag = "\xeaf3";
                sample.Type = "1";
                resultStroke.Name = sample.Name;
                resultStroke.Code = sample.Code;
                resultStroke.Draw(this.canvas);
                sample.Add(resultStroke);
            }

            this.ViewModel.Add(sample);

            //  Do：触发新增事件
            this.ImageOprateCtrEntity.OnImgMarkOperateEvent(sample.Model);


            this.popup.IsOpen = false;
        }

        internal void AddMark(ImgMarkEntity imgMarkEntity)
        {
            SampleVieModel sample = new SampleVieModel();

            //sample.Name = imgMarkEntity.Name;

            //sample.Code = imgMarkEntity.PHMCodes;

            sample.Model = imgMarkEntity;

            //  Do：根据选择的样本类型来生成缺陷/样本
            if (this.ImageOprateCtrEntity.MarkType == MarkType.Defect)
            {
                DefectShape resultStroke = new DefectShape(this._dynamic);
                sample.Flag = "\xe688";
                sample.Type = "0";
                sample.Code = imgMarkEntity.PHMCodes;
                resultStroke.Name = sample.Name;
                resultStroke.Code = sample.Code;
                resultStroke.Draw(this.canvas);
                sample.Add(resultStroke);
            }
            else if (this.ImageOprateCtrEntity.MarkType == MarkType.Sample)
            {
                SampleShape resultStroke = new SampleShape(this._dynamic);
                sample.Flag = "\xeaf3";
                sample.Type = "1";
                sample.Code = imgMarkEntity.PHMCodes;
                resultStroke.Name = sample.Name;
                resultStroke.Code = sample.Code;
                resultStroke.Draw(this.canvas);
                sample.Add(resultStroke);
            }




            this.ViewModel.Add(sample);

            //  Do：触发新增事件
            this.ImageOprateCtrEntity.OnImgMarkOperateEvent(sample.Model);

            //  Do：清除动态框
            _dynamic.BegionMatch(false);
        }

        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InkCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.ViewModel == null) return;

            if ((this.ImageOprateCtrEntity.MarkType == MarkType.None)) return;

            _dynamic.BegionMatch(true);

            start = e.GetPosition(sender as InkCanvas);

            System.Diagnostics.Debug.WriteLine("说明");

        }

        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InkCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.ViewModel == null) return;

            if ((this.ImageOprateCtrEntity.MarkType == MarkType.None)) return;

            if (e.LeftButton != MouseButtonState.Pressed) return;

            if (this.start.X <= 0) return;

            Point end = e.GetPosition(this.canvas);

            //this._isMatch = Math.Abs(start.X - end.X) > 50 && Math.Abs(start.Y - end.Y) > 50;

            _dynamic.Visibility = Visibility.Visible;

            _dynamic.Refresh(start, end);

        }

        /// <summary>
        /// 鼠标抬起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InkCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if ((this.ImageOprateCtrEntity.MarkType == MarkType.None)) return;


            //  Do：检查选择区域是否可用
            if (!_dynamic.IsMatch())
            {
                _dynamic.Visibility = Visibility.Collapsed;
                return;
            };

            if (this.start.X <= 0) return;

            //  Do：如果是选择局部放大
            if (this.r_screen.IsChecked.HasValue && this.r_screen.IsChecked.Value)
            {
                RectangleGeometry rect = new RectangleGeometry(new Rect(0, 0, this.canvas.ActualWidth, this.canvas.ActualHeight));

                //  Do：设置覆盖的蒙版
                var geo = Geometry.Combine(rect, new RectangleGeometry(this._dynamic.Rect), GeometryCombineMode.Exclude, null);

                DynamicShape shape = new DynamicShape(this._dynamic);

                //  Do：设置形状、用来提供给局部放大页面
                this.DynamicShape = shape;

                //  Do：设置提供局部放大在全局的范围的视图
                this.ImageVisual = this.canvas;

                this.OnBegionShowPartView();

                //  Do：设置当前蒙版的剪切区域
                this.rectangle_clip.Clip = geo;

                _dynamic.Visibility = Visibility.Collapsed;
            }
            else
            {

                this.ImageOprateCtrEntity.OnDrawMarkedMouseUp();

                ////  Do：不是局部放大功能则显示弹出窗口
                //this.popup.IsOpen = true;

            }

            //  Do：将数据初始化
            start = new Point(-1, -1);


        }


        void ShowPartWithShape(RectangleShape rectangle)
        {
            RectangleGeometry rect = new RectangleGeometry(new Rect(0, 0, this.canvas.ActualWidth, this.canvas.ActualHeight));

            //  Do：设置覆盖的蒙版
            var geo = Geometry.Combine(rect, new RectangleGeometry(rectangle.Rect), GeometryCombineMode.Exclude, null);

            DynamicShape shape = new DynamicShape(rectangle);

            //  Do：设置形状、用来提供给局部放大页面
            this.DynamicShape = shape;

            //  Do：设置提供局部放大在全局的范围的视图
            this.ImageVisual = this.canvas;

            this.OnBegionShowPartView();

            //  Do：设置当前蒙版的剪切区域
            this.rectangle_clip.Clip = geo;

            _dynamic.Visibility = Visibility.Collapsed;
        }

        RectangleShape _currentShap;

        MouseEventHandler mouseEventHandler;

        public void ShowDefaultDefectPart(bool flag)
        {
            if (this.ViewModel == null) return;

            if (this.ViewModel.SampleCollection.Count == 0) return;

            _currentShap = this.ViewModel.SampleCollection.First().RectangleLayer.First() as RectangleShape;


     

            foreach (var sample in this.ViewModel.SampleCollection)
            {
                foreach (var shape in sample.RectangleLayer)
                {
                    RectangleShape rectangleShape = shape as RectangleShape;

                    if (flag)
                    {
                        rectangleShape.MouseEnter += mouseEventHandler;
                    }
                    else
                    {
                        rectangleShape.MouseEnter -= mouseEventHandler;
                    }

                }
            }

        }

        void ShowCurrentShape()
        {
            if (_currentShap == null)
            {
                _currentShap = this.ViewModel.SampleCollection.First().RectangleLayer.First() as RectangleShape;
            }

            this.ShowPartWithShape(_currentShap);
        }



        public void ShowNextShape()
        {
            if (this.ViewModel == null) return;

            if (this.ViewModel.SampleCollection.Count == 0) return;

            var sample = this.ViewModel.SampleCollection.Where(l => l.RectangleLayer.Contains(this._currentShap));

            if (sample == null || sample.Count() == 0) return;

            int index = this.ViewModel.SampleCollection.IndexOf(sample.First());

            //  Message：如果是最后一项则跳转到第一项
            index = this.ViewModel.SampleCollection.Count - 1 == index ? 0 : index + 1;

            RectangleShape shape = this.ViewModel.SampleCollection[index].RectangleLayer.First() as RectangleShape;

            _currentShap = shape;

            this.ShowCurrentShape();
        }

        public void ShowPreShape()
        {
            if (this.ViewModel == null) return;

            if (this.ViewModel.SampleCollection.Count == 0) return;

            var sample = this.ViewModel.SampleCollection.Where(l => l.RectangleLayer.Contains(this._currentShap));

            if (sample == null || sample.Count() == 0) return;

            int index = this.ViewModel.SampleCollection.IndexOf(sample.First());

            //  Message：如果是最后一项则跳转到第一项
            index = 0 == index ? this.ViewModel.SampleCollection.Count - 1 : index - 1;

            RectangleShape shape = this.ViewModel.SampleCollection[index].RectangleLayer.First() as RectangleShape;

            _currentShap = shape;

            this.ShowCurrentShape();
        }



        #endregion

        /// <summary>
        /// 弹出框关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popup_Closed(object sender, EventArgs e)
        {
            //  Do：结束矩形区域检测
            _dynamic.BegionMatch(false);
        }


        public ImageOprateCtrEntity ImageOprateCtrEntity
        {
            get { return (ImageOprateCtrEntity)GetValue(MarkTypeProperty); }
            set { SetValue(MarkTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkTypeProperty =
            DependencyProperty.Register("ImageOprateCtrEntity", typeof(ImageOprateCtrEntity), typeof(ImageView), new PropertyMetadata(null, (d, e) =>
             {
                 ImageView control = d as ImageView;

                 if (control == null) return;

                 //MarkType config = e.NewValue as MarkType;

             }));


        public void Rotate()
        {
            RotateTransform rotate = this.canvas.RenderTransform as RotateTransform;
            rotate.CenterX = this.canvas.ActualWidth / 2;
            rotate.CenterY = this.canvas.ActualHeight / 2;
            rotate.Angle = rotate.Angle + 90;
        }

        public void ScreenShot(string saveFullName)
        {
            byte[] screenshot = ComponetProvider.Instance.GetScreenShot(this.canvas, 1, 90);
            FileStream fileStream = new FileStream(saveFullName, FileMode.Create, FileAccess.ReadWrite);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(screenshot);
            binaryWriter.Close();
        }

    }
}
