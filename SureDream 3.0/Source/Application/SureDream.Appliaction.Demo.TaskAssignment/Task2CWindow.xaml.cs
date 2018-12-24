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
using System.Windows.Shapes;
using Ty.Base.WpfBase.Service;
using Ty.Component.TaskAssignment;

namespace SureDream.Appliaction.Demo.TaskAssignment
{
    /// <summary>
    /// Task2CWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Task2CWindow : Window
    {
        Task2CViewModel _vm = new Task2CViewModel();
        public Task2CWindow()
        {
            InitializeComponent();

            this.DataContext = _vm;
        }
    }

    class Task2CViewModel : NotifyPropertyChanged
    {

        private RowId2CEntity _current;
        /// <summary> 说明  </summary>
        public RowId2CEntity Current
        {
            get { return _current; }
            set
            {
                _current = value;
                RaisePropertyChanged("Current");
            }
        }


        private ObservableCollection<RowId2CEntity> _rawIdCollection = new ObservableCollection<RowId2CEntity>();
        /// <summary> 说明  </summary>
        public ObservableCollection<RowId2CEntity> RawIdCollection
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

            //  Do：初始化
            if (command == "init")
            {

                RowId2CEntity entity = new RowId2CEntity();
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
                ObservableCollection<TaskModel_2C> collection = new ObservableCollection<TaskModel_2C>();

                TaskModel_2C model = new TaskModel_2C();
                model.ID = "100000";
                model.AnalystID = "2005";
                model.TaskEndTime = DateTime.Now;
                model.TaskStartTime = DateTime.Now;
                model.StartSiteID = "1001";
                model.ProcessType = 1;
                model.ProcessedFileCount = 44;
                model.TotalFileCount = 100;
                model.EndSiteID = "1004";
                model.Remark = "第1段,第2段,第3段,第4段";
                collection.Add(model);

                model = new TaskModel_2C();
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
                model.Remark = "第5段,第9段";
                collection.Add(model);

                entity.Model.SetTyeAdminUserEntity(analysts);
                entity.Model.SetTyeLineEntity(stations);

                //  Message：调用此方法前需要优先设置分析员和站信息列表
                entity.Model.SetTaskModelList(collection);

                entity.Model.SaveEvent += l =>
                {
                    foreach (var item in l)
                    {
                        Debug.WriteLine(item.Remark);
                    }
                };

                this.RawIdCollection.Add(entity);



            }
            //  Do：分工
            else if (command == "btn_divied")
            {
                Window window = new Window();
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Width = 1100;
                window.Height = 400;
                window.Content = new Task2CAssignmentControl();
                window.DataContext = this.Current.Model;

                Action<ObservableCollection<TaskModel_2C>> action = l =>
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
            //  Do：查看
            else if (command == "btn_showTask")
            {
                Window window = new Window();
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Width = 1100;
                window.Height = 400;
                window.Content = new Task2CLookUpControl();
                window.DataContext = this.Current.Model;
                window.ShowDialog();

            }
            //  Do：添加
            else if (command == "btn_add")
            {
                RowId2CEntity entity = new RowId2CEntity();
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
                ObservableCollection<TaskModel_2C> collection = new ObservableCollection<TaskModel_2C>();


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

    class RowId2CEntity
    {
        public string ID { get; set; }

        public ITaskItemFor2C Model { get; set; } = new TaskDivision2CViewModel();
    }
}
