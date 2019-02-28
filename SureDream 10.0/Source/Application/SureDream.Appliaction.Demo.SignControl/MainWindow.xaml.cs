using CDTY.DataAnalysis.Entity;
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


        Window window = new Window();

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            IDefectSign defectViewModel = new DefectViewModel();

            List<DefectCommonUsed> defectCommonUseds = new List<DefectCommonUsed>();

            for (int i = 0; i < 7; i++)
            {
                DefectCommonUsed defectCommonUsed = new DefectCommonUsed();

                defectCommonUsed.Describletion = "统一跨距接头数量(n) -n>=2" + i.ToString();
                defectCommonUsed.CountUse = i;
                defectCommonUsed.ID = Guid.NewGuid().ToString();

                defectCommonUseds.Add(defectCommonUsed);
            }


            defectViewModel.LoadDefectCommonUsed(defectCommonUseds);


            List<DefectCommonUsed> defectCommonUseds1 = new List<DefectCommonUsed>();

            for (int i = 0; i < 3; i++)
            {
                DefectCommonUsed defectCommonUsed = new DefectCommonUsed();

                defectCommonUsed.Describletion = "统一跨距接头数量统一跨距接头数量(统一跨距接头数量((n) -n>=2" + i;
                defectCommonUsed.ID = Guid.NewGuid().ToString();
                defectCommonUsed.CountUse = i;

                defectCommonUseds1.Add(defectCommonUsed);
            }


            defectViewModel.LoadEstimateDefectCommonUseds(defectCommonUseds1);

            List<TyeEncodeDeviceEntity> tyeEncodeDeviceEntities = new List<TyeEncodeDeviceEntity>();

            for (int i = 0; i < 6000; i++)
            {
                TyeEncodeDeviceEntity tyeEncodeDeviceEntity = new TyeEncodeDeviceEntity();

                tyeEncodeDeviceEntity.Code = i.ToString();
                tyeEncodeDeviceEntity.Name = "承受力" + i;
                tyeEncodeDeviceEntity.ID = i.ToString();
                tyeEncodeDeviceEntity.ParentID = (i / 10 == 0 ? string.Empty : (i / 10).ToString()).ToString();

                tyeEncodeDeviceEntities.Add(tyeEncodeDeviceEntity);
            }

            defectViewModel.LoadTyeEncodeDevice(tyeEncodeDeviceEntities);


            List<TyeEncodeDeviceEntity> tyeEncodeDeviceEntitieChecks = new List<TyeEncodeDeviceEntity>();

            for (int i = 0; i < 30; i++)
            {
                TyeEncodeDeviceEntity tyeEncodeDeviceEntity = new TyeEncodeDeviceEntity();

                tyeEncodeDeviceEntity.Code = i.ToString();
                tyeEncodeDeviceEntity.Name = "承受力" + i;
                tyeEncodeDeviceEntity.ID = i.ToString();
                tyeEncodeDeviceEntity.ParentID = (i / 10 == 0 ? string.Empty : (i / 10).ToString()).ToString();

                tyeEncodeDeviceEntitieChecks.Add(tyeEncodeDeviceEntity);
            }

            defectViewModel.LoadTyeEncodeCheckDevice(tyeEncodeDeviceEntitieChecks);

            defectViewModel.ConfirmData += l =>
            {

                Debug.WriteLine(l);

            };

            window.Width = 1000;
            window.Height = 600;
            //window.WindowStyle = WindowStyle.None;

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

            DefectControl defect = new DefectControl();
            window.Content = defect;
            window.DataContext = defectViewModel;

            KeyGesture keyGesture = new KeyGesture(Key.W, ModifierKeys.Control);
            defect.KeyGestureForHistList = keyGesture;

            //window.ShowDialog();



        }

        /// <summary> 缺陷管理 </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            window.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Window window = new Window();
            //window.Content = new SignSignControl();
            //window.ShowDialog();
        }


    }

}
