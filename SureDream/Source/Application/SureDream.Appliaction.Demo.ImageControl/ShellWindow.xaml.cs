﻿using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Ty.Base.WpfBase;
using Ty.Component.ImageControl;

namespace SureDream.Appliaction.Demo.ImageControl
{
    /// <summary>
    /// ShellWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShellWindow : Window
    {

        MainViewModel _vm = new MainViewModel();

        IImgOperate _imgOperate = new ImageOprateCtrEntity();

        bool _isload = false;

        public ShellWindow()
        {
            InitializeComponent();

            this.grid_center.Children.Add(_imgOperate.BuildEntity());

            List<ImgMarkEntity> temp = new List<ImgMarkEntity>();

            _imgOperate.ImgMarkOperateEvent += l =>
              {
                  string fn = System.IO.Path.GetFileNameWithoutExtension(this._imgOperate.BuildEntity().Current.Value);

                  string file = this.GetMarkFileName(fn);

                  Debug.WriteLine("添加：" + l.Name + "-" + l.Code + $"({l.X},{l.Y}) {l.Width}*{l.Height}");

                  temp.Add(l);

                  string result = JsonConvert.SerializeObject(temp);

                  File.WriteAllText(file, result);

                  MessageBox.Show("添加：" + l.Name + "-" + l.Code + $"({l.X},{l.Y}) {l.Width}*{l.Height}", "保存成功");

              };

            _imgOperate.ImgProcessEvent +=(l, k) =>
             {
                 Debug.WriteLine("图片路径：" + l);

                 Debug.WriteLine("操作参数：" + k);

                 MessageBox.Show(k.ToString());
             };

        }

        private void CommandBinding_Search_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.InitialDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

            var result = open.ShowDialog();

            List<string> images = new List<string>();

            if (result.HasValue && result.Value)
            {
                var files = Directory.GetFiles(System.IO.Path.GetDirectoryName(open.FileName));

                foreach (var item in files)
                {
                    if (System.IO.Path.GetExtension(item).EndsWith("jpg") || System.IO.Path.GetExtension(item).EndsWith("png"))
                    {
                        images.Add(item);
                    }
                }
            }

            _imgOperate.LoadImg(images);

            this._isload = true;
        }

        private void CommandBinding_Search_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Previous_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _imgOperate.PreviousImg();

        }

        private void CommandBinding_Previous_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload;
        }

        private void CommandBinding_Next_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _imgOperate.NextImg();
        }

        private void CommandBinding_Next_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload;
        }

        private void CommandBinding_Next_CanExecut(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload;
        }

        private void CommandBinding_FullScreen_CanExecut(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload && !_isfullscreen;
        }

        bool _isfullscreen = false;
        private void CommandBinding_FullScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _imgOperate.SetFullScreen(true);
        }

        private void CommandBinding_UnFullScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _imgOperate.SetFullScreen(false);
        }

        private void CommandBinding_UnFullScreen_CanExecut(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload && _isfullscreen;
        }

        private void CommandBinding_ShowLocates_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _imgOperate.ShowLocates();
        }

        private void CommandBinding_ShowDefects_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _imgOperate.ShowDefects();
        }

        private void CommandBinding_ShowMarks_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _imgOperate.ShowMarks();
        }

        private void CommandBinding_ImgPlaySpeedUp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _imgOperate.ImgPlaySpeedUp();
        }

        private void CommandBinding_ImgPlaySpeedDown_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _imgOperate.ImgPlaySpeedDown();
        }

        private void CommandBinding_ShowLocates_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload;
        }

        private void CommandBinding_ShowDefects_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload;
        }

        private void CommandBinding_ShowMarks_CanExecut(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload;
        }

        private void CommandBinding_ImgPlaySpeedUp_CanExecut(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload;
        }

        private void CommandBinding_ImgPlaySpeedDown_CanExecut(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload;
        }

        private void CommandBinding_LoadMarkEntitys_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.InitialDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Marks");

            var result = open.ShowDialog();

            if (result.HasValue && result.Value)
            {
                string marks = File.ReadAllText(open.FileName);

                var list = JsonConvert.DeserializeObject<List<ImgMarkEntity>>(marks);

                _imgOperate.LoadMarkEntitys(list);
            }


        }

        private void CommandBinding_LoadMarkEntitys_CanExecut(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload;
        }

        private void CommandBinding_LoadCodes_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            Dictionary<string, string> dic = new Dictionary<string, string>();

            var count = r.Next(5,10);

            for (int i = 0; i < count; i++)
            {
                dic.Add((i + 1).ToString(), "D10" + i.ToString());
            }


            _imgOperate.LoadCodes(dic);
        }

        private void CommandBinding_LoadCodes_CanExecut(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload;
        }

        Random r = new Random();
        private void CommandBinding_AddImgFigure_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            var count = r.Next(5, 10);

            for (int i = 0; i < count; i++)
            {
                dic.Add((i + 1).ToString(), "D10" + i.ToString());
            }

            _imgOperate.AddImgFigure(dic);
        }

        private void CommandBinding_AddImgFigure_CanExecut(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload;
        }

        private void CommandBinding_SetImgPlay_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _imgOperate.SetImgPlay((ImgPlayMode)this.cb_playmode.SelectedItem);
        }

        private void CommandBinding_SetImgPlay_CanExecut(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._isload;
        }

        private void CommandBinding_LoadImg_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.InitialDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

            var result = open.ShowDialog();

            if (result.HasValue && result.Value)
            {
                _imgOperate.LoadImg(open.FileName);
            }



            this._isload = true;
        }

        private void CommandBinding_LoadImg_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {


        }

        public string GetMarkFileName(string imgName)
        {
            string file = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");

            string tempFiles = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory+"\\Marks", imgName + "[" + file + "].mark");

            if (!File.Exists(tempFiles)) File.WriteAllText(tempFiles, string.Empty);

            return tempFiles;
        }
    }


    partial class MainViewModel
    {

        private List<ImgMarkEntity> _collection = new List<ImgMarkEntity>();
        /// <summary> 说明  </summary>
        public List<ImgMarkEntity> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        public string tempFiles;
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

    partial class MainViewModel : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public MainViewModel()
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
