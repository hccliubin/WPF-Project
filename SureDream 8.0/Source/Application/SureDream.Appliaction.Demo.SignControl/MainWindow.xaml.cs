using CDTY.DataAnalysis.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            DefectViewModel defectViewModel = new DefectViewModel();
            defectViewModel.Load(this.GetEntity());

            Window window = new Window();
            window.Content = new DefectControl();
            window.DataContext = defectViewModel;
            window.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Window window = new Window();
            //window.Content = new SignSignControl();
            //window.ShowDialog();
        }

        DefectMenuEntity GetEntity()
        {
            DefectMenuEntity entity = new DefectMenuEntity();

            //  Message：数据采集方式
            List<TyeEncodeCategoryconfigEntity> tyeEncodeCategoryconfigEntities = new List<TyeEncodeCategoryconfigEntity>();

            for (int i = 0; i < 20; i++)
            {
                TyeEncodeCategoryconfigEntity tyeEncodeCategoryconfigEntity = new TyeEncodeCategoryconfigEntity();

                tyeEncodeCategoryconfigEntity.ID = i.ToString();
                tyeEncodeCategoryconfigEntity.Code = i.ToString();
                tyeEncodeCategoryconfigEntity.Name = "N00" + i.ToString();

                tyeEncodeCategoryconfigEntities.Add(tyeEncodeCategoryconfigEntity);
            }

            entity.DataAcquisitionMode = tyeEncodeCategoryconfigEntities;

            entity.RailwaySsequence = tyeEncodeCategoryconfigEntities;

            List<TyeBaseLineEntity> tyeBaseLineEntities = new List<TyeBaseLineEntity>();

            for (int i = 0; i < 10; i++)
            {
                TyeBaseLineEntity tyeBaseLineEntity = new TyeBaseLineEntity();
                tyeBaseLineEntity.ID = i.ToString();
                tyeBaseLineEntity.LineName = "LN00" + i.ToString();
                tyeBaseLineEntities.Add(tyeBaseLineEntity);
            }

            entity.DedicatedLine = tyeBaseLineEntities;

            List<TyeBaseSiteEntity> tyeBaseSiteEntities = new List<TyeBaseSiteEntity>();

            for (int i = 0; i < 10; i++)
            {
                TyeBaseSiteEntity tyeBaseSiteEntity = new TyeBaseSiteEntity();
                tyeBaseSiteEntity.ID = i.ToString();
                tyeBaseSiteEntity.SiteName = "SN00" + i.ToString();
                tyeBaseSiteEntities.Add(tyeBaseSiteEntity);
            }

            entity.DedicatedStation = tyeBaseSiteEntities;

            entity.BasicUnit = tyeEncodeCategoryconfigEntities;

            entity.ResponsibilityWorkArea = tyeEncodeCategoryconfigEntities;

            entity.ResponsibilityWorkshop = tyeEncodeCategoryconfigEntities;

            ObservableCollection<TyeEncodeDeviceEntity> tyeEncodeDeviceEntities = new ObservableCollection<TyeEncodeDeviceEntity>();

            for (int i = 0; i < 10; i++)
            {
                TyeEncodeDeviceEntity tyeEncodeDeviceEntity = new TyeEncodeDeviceEntity();
                tyeEncodeDeviceEntity.ID = i.ToString();
                tyeEncodeDeviceEntity.Name = "DN00" + i.ToString();
                tyeEncodeDeviceEntities.Add(tyeEncodeDeviceEntity);
            }

            entity.DefectOrMarkCodes = tyeEncodeDeviceEntities;

            List<DefectCommonUsed> defectCommonUseds = new List<DefectCommonUsed>();

            for (int i = 0; i < 100; i++)
            {
                DefectCommonUsed defectCommonUsed = new DefectCommonUsed();

                defectCommonUsed.ID = i.ToString();
                defectCommonUsed.Name = "DN00" + i.ToString();
                defectCommonUsed.Describletion = i.ToString().PadLeft(3, '0') + "        01020304        接触线张力xx脱落   (F14)      历史使用次数" + i.ToString() + "次";
                defectCommonUsed.CountUse = i;
                defectCommonUsed.OrderNo = i;
                defectCommonUseds.Add(defectCommonUsed);
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
