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
            defectViewModel.Load(this.GetEntity());

            defectViewModel.ConfirmData += l =>
            {

                Debug.WriteLine(l);

            };

            window.Width = 1200;
            window.Height = 500;

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
          
            KeyGesture keyGesture = new KeyGesture(Key.W,ModifierKeys.Control);
            defect.KeyGestureForHistList = keyGesture;

            window.ShowDialog();



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

        /// <summary> 获取测试数据 </summary>
        DefectMenuEntity GetEntity()
        {
            DefectMenuEntity entity = new DefectMenuEntity();

            entity.PHMCodes = "A";

            //  Message：数据采集方式
            List<TyeBaseDatacollecttypeEntity> tyeEncodeCategoryconfigEntities = new List<TyeBaseDatacollecttypeEntity>();

            for (int i = 0; i < 20; i++)
            {
                TyeBaseDatacollecttypeEntity tyeEncodeCategoryconfigEntity = new TyeBaseDatacollecttypeEntity();

                //tyeEncodeCategoryconfigEntity.ID = i.ToString();
                tyeEncodeCategoryconfigEntity.Code = "Data" + i.ToString();
                tyeEncodeCategoryconfigEntity.Name = "N00" + i.ToString();

                tyeEncodeCategoryconfigEntities.Add(tyeEncodeCategoryconfigEntity);
            }

            entity.DataAcquisitionMode = tyeEncodeCategoryconfigEntities;

            List<TyeBaseRailwaystationEntity> RailwaySsequences = new List<TyeBaseRailwaystationEntity>();

            for (int i = 0; i < 20; i++)
            {
                TyeBaseRailwaystationEntity tyeEncodeCategoryconfigEntity = new TyeBaseRailwaystationEntity();

                //tyeEncodeCategoryconfigEntity.ID = i.ToString();
                tyeEncodeCategoryconfigEntity.Code = "Data" + i.ToString();
                tyeEncodeCategoryconfigEntity.Name = "N00" + i.ToString();

                RailwaySsequences.Add(tyeEncodeCategoryconfigEntity);
            }

            entity.RailwaySsequence = RailwaySsequences;

            List<TyeBaseLineEntity> tyeBaseLineEntities = new List<TyeBaseLineEntity>();

            for (int i = 0; i < 10; i++)
            {
                TyeBaseLineEntity tyeBaseLineEntity = new TyeBaseLineEntity();
                tyeBaseLineEntity.ID = i.ToString();
                tyeBaseLineEntity.LineCode = "Line" + i.ToString();
                tyeBaseLineEntity.LineName = "LN00" + i.ToString();
                tyeBaseLineEntities.Add(tyeBaseLineEntity);
            }

            entity.DedicatedLine = tyeBaseLineEntities;

            List<TyeBaseSiteEntity> tyeBaseSiteEntities = new List<TyeBaseSiteEntity>();

            for (int i = 0; i < 10; i++)
            {
                TyeBaseSiteEntity tyeBaseSiteEntity = new TyeBaseSiteEntity();
                tyeBaseSiteEntity.ID = i.ToString();
                tyeBaseSiteEntity.SiteCode = "Site" + i.ToString();
                tyeBaseSiteEntity.SiteName = "SN00" + i.ToString();
                tyeBaseSiteEntities.Add(tyeBaseSiteEntity);
            }

            entity.DedicatedStation = tyeBaseSiteEntities;

            List<TyeBaseDepartmentEntity> responsibilityworkarea = new List<TyeBaseDepartmentEntity>();

            for (int i = 0; i < 10; i++)
            {
                TyeBaseDepartmentEntity tyeBaseSiteEntity = new TyeBaseDepartmentEntity();
                tyeBaseSiteEntity.ID = i.ToString();
                tyeBaseSiteEntity.Code = "Site" + i.ToString();
                tyeBaseSiteEntity.Name = "SN00" + i.ToString();
                responsibilityworkarea.Add(tyeBaseSiteEntity);
            }


            List<TyeBasePillarEntity> basicunit = new List<TyeBasePillarEntity>();

            for (int i = 0; i < 10; i++)
            {
                TyeBasePillarEntity tyeBaseSiteEntity = new TyeBasePillarEntity();
                tyeBaseSiteEntity.ID = i.ToString();
                tyeBaseSiteEntity.PoleMarkCode = "Site" + i.ToString();
                //tyeBaseSiteEntity. = "SN00" + i.ToString();
                basicunit.Add(tyeBaseSiteEntity);
            }


            entity.BasicUnit = basicunit;

            entity.ResponsibilityWorkArea = responsibilityworkarea;

            entity.ResponsibilityWorkshop = responsibilityworkarea;

            ObservableCollection<TyeEncodeDeviceEntity> tyeEncodeDeviceEntities = new ObservableCollection<TyeEncodeDeviceEntity>();

            for (int i = 0; i < 3000; i++)
            {
                TyeEncodeDeviceEntity tyeEncodeDeviceEntity = new TyeEncodeDeviceEntity();
                tyeEncodeDeviceEntity.ID = i.ToString();
                tyeEncodeDeviceEntity.Code= "Defect" + i.ToString();
                tyeEncodeDeviceEntity.Name = "DN00" + i.ToString();
                tyeEncodeDeviceEntities.Add(tyeEncodeDeviceEntity);
            }

            entity.DefectOrMarkCodes = tyeEncodeDeviceEntities;

            List<DefectCommonUsed> defectCommonUseds = new List<DefectCommonUsed>();

            for (int i = 0; i < 50; i++)
            {
                DefectCommonUsed defectCommonUsed = new DefectCommonUsed();

                defectCommonUsed.ID = i.ToString();
                defectCommonUsed.Name = "DN00" + i.ToString();
                defectCommonUsed.Code = "DefectCommonUsed" + i.ToString();
                defectCommonUsed.NamePY = "NamePY" + i.ToString();
                defectCommonUsed.Describletion = i.ToString().PadLeft(3, '0') + " " + defectCommonUsed.Code + " 01020304 " + defectCommonUsed.NamePY + " 接触线张力xx脱落 (F14) 次数" + i.ToString() + "次";
                defectCommonUsed.CountUse = i;
                defectCommonUsed.OrderNo = i;
                defectCommonUseds.Add(defectCommonUsed);
            }

            ObservableCollection<TyeEncodeDeviceEntity> defectOrMarkCodes = new ObservableCollection<TyeEncodeDeviceEntity>();

            for (int i = 0; i < 3000; i++)
            {
                TyeEncodeDeviceEntity defectOrMarkCode = new TyeEncodeDeviceEntity();

                defectOrMarkCode.ID = i.ToString();
                defectOrMarkCode.Name = "DN00" + i.ToString();
                defectOrMarkCode.Code = "CommonHistoricalDefectsOrMark" + i.ToString();
                defectOrMarkCode.NamePY = "NamePY" + i.ToString();
                defectOrMarkCode.OrderNo = i;
                defectOrMarkCodes.Add(defectOrMarkCode);
            }

            entity.CommonHistoricalDefectsOrMark = defectCommonUseds;

            TyeAdminUserEntity tyeAdminUserEntity = new TyeAdminUserEntity();
            tyeAdminUserEntity.ID = "1";
            tyeAdminUserEntity.Name = "王杰结";
            entity.tyeAdminUserEntity = tyeAdminUserEntity;

            entity.KmLog = "K102+123";
            entity.DetectionVehicles = "川A213213";
            entity.DetectDate = DateTime.Now;

            return entity;

        }
    }
    
}
