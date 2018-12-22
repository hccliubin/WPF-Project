using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

                RowIdEntity entity = new RowIdEntity();
                entity.ID = "初始化加载的任务列表示例";

                //  Message：设置站列表
                ObservableCollection<TyeLineEntity> stations = new ObservableCollection<TyeLineEntity>();

                stations.Add(new TyeLineEntity() { ID = "1001",  Name = "第1段" });
                stations.Add(new TyeLineEntity() { ID = "1002", Name = "第2段" });
                stations.Add(new TyeLineEntity() { ID = "1003", Name = "第3段" });
                stations.Add(new TyeLineEntity() { ID = "1004", Name = "第4段" });
                stations.Add(new TyeLineEntity() { ID = "1005", Name = "第5段" });
                stations.Add(new TyeLineEntity() { ID = "1006", Name = "第6段" });
                stations.Add(new TyeLineEntity() { ID = "1007", Name = "第7段" });
                stations.Add(new TyeLineEntity() { ID = "1008", Name = "第8段" });
                stations.Add(new TyeLineEntity() { ID = "1009", Name = "第9段" });
                stations.Add(new TyeLineEntity() { ID = "1010", Name = "第10段" });
                stations.Add(new TyeLineEntity() { ID = "1011", Name = "第11段" });
                stations.Add(new TyeLineEntity() { ID = "1012", Name = "第12段" });

                //  Message：设置分析人员列表
                ObservableCollection<TyeAdminUserEntity> analysts = new ObservableCollection<TyeAdminUserEntity>();
                analysts.Add(new TyeAdminUserEntity() { ID = "2001", Name = "刘德华" });
                analysts.Add(new TyeAdminUserEntity() { ID = "2002", Name = "张国荣" });
                analysts.Add(new TyeAdminUserEntity() { ID = "2003", Name = "贝克汉姆" });
                analysts.Add(new TyeAdminUserEntity() { ID = "2004", Name = "齐达内" });
                analysts.Add(new TyeAdminUserEntity() { ID = "2005", Name = "劳尔" });
                analysts.Add(new TyeAdminUserEntity() { ID = "2006", Name = "马拉多纳" });
                analysts.Add(new TyeAdminUserEntity() { ID = "2007", Name = "郝海东" });

                //  Message：加载历史任务信息
                ObservableCollection<TaskModel> collection = new ObservableCollection<TaskModel>();

                TaskModel model = new TaskModel();
                model.ID = "100000";
                model.AnalystID = "2005";
                model.TaskEndTime = DateTime.Now;
                model.TaskStartTime = DateTime.Now;
                model.StartSiteID = "1001";
                model.ProcessType = 1;
                model.ProcessedFileCount = 44;
                model.TotalFileCount = 100;
                model.EndSiteID = "1004";
                model.Remark = "1001,1002";
                collection.Add(model);

                model = new TaskModel();
                model.ID = "100001";
                model.AnalystID = "2001";
                model.TaskEndTime = DateTime.Now;
                model.TaskStartTime = DateTime.Now;
                model.StartSiteID = "1001";
                model.ProcessedFileCount = 95;
                model.TotalFileCount = 100;
                model.EndSiteID = "1001";

                model.StartPoleID = "1";
                model.EndPoleID = "3";
                model.Remark = "1005,1009";
                collection.Add(model);

                entity.Model.SetTyeAdminUserEntity(analysts);
                entity.Model.SetTyeLineEntity(stations);  

                //  Message：调用此方法前需要优先设置分析员和站信息列表
                entity.Model.SetTaskModelList(collection); 

                entity.Model.SaveEvent += l =>
                  {

                      Debug.WriteLine("说明");

                  };

                this.RawIdCollection.Add(entity);



            }
            //  Do：取消
            else if (command == "btn_divied")
            {
                TaskAssignmentWindow window = new TaskAssignmentWindow();
                window.DataContext = this.Current.Model;

                Action<ObservableCollection<TaskModel>> action = l =>
                 {
                     Thread.Sleep(3000);

                     foreach (var item in l)
                     {
                         Debug.WriteLine(item.ID + "- " + item.StartSiteID + "- " + item.EndSiteID);
                     }

                     //  Message：调用主线程用Dispatcher
                     Application.Current.Dispatcher.Invoke(() =>
                     {
                         window.Close();
                     });

                 };

                //  Message：注册保存事件
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
                entity.ID = "初始化加载的任务列表示例";

                //  Message：设置站列表
                ObservableCollection<TyeLineEntity> stations = new ObservableCollection<TyeLineEntity>();

                stations.Add(new TyeLineEntity() { ID = "1001", Name = "第1段" });
                stations.Add(new TyeLineEntity() { ID = "1002", Name = "第2段" });
                stations.Add(new TyeLineEntity() { ID = "1003", Name = "第3段" });
                stations.Add(new TyeLineEntity() { ID = "1004", Name = "第4段" });
                stations.Add(new TyeLineEntity() { ID = "1005", Name = "第5段" });
                stations.Add(new TyeLineEntity() { ID = "1006", Name = "第6段" });
                stations.Add(new TyeLineEntity() { ID = "1007", Name = "第7段" });
                stations.Add(new TyeLineEntity() { ID = "1008", Name = "第8段" });
                stations.Add(new TyeLineEntity() { ID = "1009", Name = "第9段" });
                stations.Add(new TyeLineEntity() { ID = "1010", Name = "第10段" });
                stations.Add(new TyeLineEntity() { ID = "1011", Name = "第11段" });
                stations.Add(new TyeLineEntity() { ID = "1012", Name = "第12段" });

                //  Message：设置分析人员列表
                ObservableCollection<TyeAdminUserEntity> analysts = new ObservableCollection<TyeAdminUserEntity>();
                analysts.Add(new TyeAdminUserEntity() { ID = "2001", Name = "刘德华" });
                analysts.Add(new TyeAdminUserEntity() { ID = "2002", Name = "张国荣" });
                analysts.Add(new TyeAdminUserEntity() { ID = "2003", Name = "贝克汉姆" });
                analysts.Add(new TyeAdminUserEntity() { ID = "2004", Name = "齐达内" });
                analysts.Add(new TyeAdminUserEntity() { ID = "2005", Name = "劳尔" });
                analysts.Add(new TyeAdminUserEntity() { ID = "2006", Name = "马拉多纳" });
                analysts.Add(new TyeAdminUserEntity() { ID = "2007", Name = "郝海东" });

                //  Message：加载历史任务信息
                ObservableCollection<TaskModel> collection = new ObservableCollection<TaskModel>();
                

                entity.Model.SetTyeAdminUserEntity(analysts);
                entity.Model.SetTyeLineEntity(stations);

                //  Message：调用此方法前需要优先设置分析员和站信息列表
                entity.Model.SetTaskModelList(collection);

                entity.Model.SaveEvent += l =>
                {

                    Debug.WriteLine("说明");

                };

                this.RawIdCollection.Add(entity);


            }

        }

    }

    class RowIdEntity
    {
        public string ID { get; set; }

        public ITaskItem Model { get; set; } = new TaskDivisionViewModel();
    }
}
