using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using Ty.Base.WpfBase.Service;
using Ty.Component.TaskAssignment;

namespace SureDream.Appliaction.Demo.TaskAssignment
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        MainViewModel _vm = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = _vm;
        }

    }

    class MainViewModel : NotifyPropertyChanged
    {

        private RowIdEntity _current;
        /// <summary> 说明  </summary>
        public RowIdEntity Current
        {
            get { return _current; }
            set
            {
                _current = value;
                RaisePropertyChanged("Current");
            }
        }


        private ObservableCollection<RowIdEntity> _rawIdCollection = new ObservableCollection<RowIdEntity>();
        /// <summary> 说明  </summary>
        public ObservableCollection<RowIdEntity> RawIdCollection
        {
            get { return _rawIdCollection; }
            set
            {
                _rawIdCollection = value;
                RaisePropertyChanged("RawIdCollection");
            }
        }

        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "init")
            {
                //for (int i = 0; i < 4; i++)
                //{
                //    RowIdEntity vm = new RowIdEntity();
                //    vm.ID = "贵广线_上行_佛山站_肇庆站_" + i;
                //    this.RawIdCollection.Add(vm);
                //}

            }
            //  Do：取消
            else if (command == "btn_divied")
            {
                TaskAssignmentWindow window = new TaskAssignmentWindow();
                window.DataContext = this.Current.Model;

                Action<ObservableCollection<TaskModel>> action= l =>
                {
                    foreach (var item in l)
                    {
                        Debug.WriteLine(item.SeriaNumber);

                    }

                    window.Close();
                };
                this.Current.Model.SaveEvent += action;
                window.ShowDialog();
                this.Current.Model.SaveEvent -= action;

            }
            //  Do：取消
            else if (command == "btn_showTask")
            {
                TaskLookUpWindow window = new TaskLookUpWindow();
                window.DataContext = this.Current.Model;
                window.ShowDialog();

            }
            //  Do：取消
            else if (command == "btn_add")
            {
                RowIdEntity entity = new RowIdEntity();

                ObservableCollection<TyeBaseSiteEntity> stations = new ObservableCollection<TyeBaseSiteEntity>();

                stations.Add(new TyeBaseSiteEntity() { ID = "1001", SiteName = "北京站" });
                stations.Add(new TyeBaseSiteEntity() { ID = "1001", SiteName = "上海站" });
                stations.Add(new TyeBaseSiteEntity() { ID = "1001", SiteName = "天津站" });
                stations.Add(new TyeBaseSiteEntity() { ID = "1001", SiteName = "佛山站" });
                stations.Add(new TyeBaseSiteEntity() { ID = "1001", SiteName = "广州站" });
                stations.Add(new TyeBaseSiteEntity() { ID = "1001", SiteName = "肇庆站" });

                ObservableCollection<TyeAdminUserEntity>  analysts = new ObservableCollection<TyeAdminUserEntity>();
                analysts.Add(new TyeAdminUserEntity() { ID = "1001", Name = "刘德华" });
                analysts.Add(new TyeAdminUserEntity() { ID = "1001", Name = "张国荣" });
                analysts.Add(new TyeAdminUserEntity() { ID = "1001", Name = "贝克汉姆" });
                analysts.Add(new TyeAdminUserEntity() { ID = "1001", Name = "齐达内" });
                analysts.Add(new TyeAdminUserEntity() { ID = "1001", Name = "劳尔" });
                analysts.Add(new TyeAdminUserEntity() { ID = "1001", Name = "马拉多纳" });
                analysts.Add(new TyeAdminUserEntity() { ID = "1001", Name = "郝海东" });

                //entity.Model.RefreshConfig(task);

                entity.ID = Guid.NewGuid().ToString();

                entity.Model.SetTyeAdminUserEntity(analysts);
                entity.Model.SetTyeBaseSiteEntity(stations);

                entity.Model.SeletctSameSiteEvent += l =>
                  {
                      MessageBox.Show("选择了相同站:"+l.SiteName);

                      

                      ObservableCollection<TyeBasePillarEntity> _poles = new ObservableCollection<TyeBasePillarEntity>();
                      for (int i = 1; i < 10; i++)
                      {
                          _poles.Add(new TyeBasePillarEntity() { ID = i.ToString(), PoleCode = i.ToString() });
                      }

                      entity.Model.SetTyeBasePillarEntity(_poles);
                  };

                this.RawIdCollection.Add(entity);

            }
            
        }

    }

    class RowIdEntity
    {
        public string ID { get; set; }

        public TaskDivisionViewModel Model { get; set; } = new TaskDivisionViewModel();
    }
}
