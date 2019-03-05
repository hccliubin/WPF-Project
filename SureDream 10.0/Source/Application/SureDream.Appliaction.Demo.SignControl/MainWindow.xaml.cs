using CDTY.DataAnalysis.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
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
using Ty.Component.SignsControl;

namespace SureDream.Appliaction.Demo.SignControl
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //  Message：获取测试数据
            string url = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "测试数据.json");

            string txt = System.IO.File.ReadAllText(url, Encoding.Default);

            var collecion = JsonConvert.DeserializeObject<List<TyeEncodeDeviceEntity>>(txt);

            IDefectSign defectViewModel = DefectViewModel.CreateInstance();

            //  Message：初始化树形控件（只需初始化一遍）
            defectViewModel.InitTyeEncodeDevice(collecion);

            List<TyeEncodeDeviceEntity> tyeEncodeDeviceEntitieChecks = new List<TyeEncodeDeviceEntity>();

            defectViewModel.LoadTyeEncodeCheckDevice(collecion.Where(l => l.Code.Length == 2).ToList());

           

         
        }

        /// <summary> 缺陷管理 </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();

            window.Width = 1000;
            window.Height = 600;
            //window.WindowStyle = WindowStyle.None;

            IDefectSign defectViewModel = DefectViewModel.CreateInstance();

            //  Message：刷新常用数据
            List<DefectCommonUsed> defectCommonUseds = new List<DefectCommonUsed>();

            for (int i = 0; i < 7; i++)
            {
                DefectCommonUsed defectCommonUsed = new DefectCommonUsed();

                defectCommonUsed.Describletion = "统一跨距接头数量(n) -n>=2" + i.ToString();
                defectCommonUsed.CountUse = i;
                defectCommonUsed.Code = "Code" + i;
                defectCommonUsed.ID = Guid.NewGuid().ToString();

                defectCommonUseds.Add(defectCommonUsed);
            }

            defectViewModel.LoadDefectCommonUsed(defectCommonUseds);

            //  Message：刷新预估缺陷
            List<DefectCommonUsed> defectCommonUseds1 = new List<DefectCommonUsed>();

            for (int i = 0; i < 3; i++)
            {
                DefectCommonUsed defectCommonUsed = new DefectCommonUsed();
                defectCommonUsed.Code = "Code" + i;
                defectCommonUsed.Describletion = "统一跨距接头数量统一跨距接头数量(统一跨距接头数量((n) -n>=2" + i;
                defectCommonUsed.ID = Guid.NewGuid().ToString();
                defectCommonUsed.CountUse = i;

                defectCommonUseds1.Add(defectCommonUsed);
            }

            
            defectViewModel.LoadEstimateDefectCommonUseds(defectCommonUseds1);

            //  Message：刷新缺陷输入信息
            defectViewModel.LoadPHM("B 01 15 000045 000261 000033");

            //  Do：取消
            defectViewModel.CancelClick += () =>
            {
                window.Hide();
            };

            //  Do：q确定
            defectViewModel.SumitClick += () =>
            {
                window.Hide();

                Debug.WriteLine(defectViewModel.ToString());
            };

            DefectControl defect = DefectViewModel.CreateInstance().GetControlInstance();

            window.Content = defect;

            window.ShowDialog();

        }
    }

}
