using HEW.Base.Theme.Style;
using HEW.General.Data.Manager;
using HEW.General.Model.Network.Form;
using HEW.UserControls.Reports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HEW.Module.PhysicalExamination
{
    class PhysicalExaminationModuleNotifyClass : ReportModuleNotifyClass
    {


        private List<UserControl> _qustionList;
        /// <summary> 问题页列表  </summary>
        public List<UserControl> QustionList
        {
            get { return _qustionList; }
            set
            {
                _qustionList = value;
                RaisePropertyChanged("QustionList");
            }
        }

        private bool _showQ;
        /// <summary> 显示问题也  </summary>
        public bool ShowQ
        {
            get { return _showQ; }
            set
            {
                _showQ = value;
                RaisePropertyChanged("ShowQ");
            }
        }


        private bool _isLogOffModule;
        /// <summary> 关闭模块  主要应用弹窗取消时调用  </summary>
        public bool IsLogOffModule
        {
            get { return _isLogOffModule; }
            set
            {
                _isLogOffModule = value;
                RaisePropertyChanged("IsLogOffModule");
            }
        }



        private int _pageSelect;
        /// <summary> 当前显示页  </summary>
        public int PageSelect
        {
            get { return _pageSelect; }
            set
            {
                _pageSelect = value;
                RaisePropertyChanged("PageSelect");
            }
        }


        public override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Init")
            {
                //  Message：答题列表

                string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "Config.xml");

                if (!File.Exists(configPath))
                {
                    DataManager.DatasManager.Info("配置文件不存在！" + configPath);
                    return;
                }

                XmlTools.Load(configPath);

                List<UserControl> cs = new List<UserControl>();


                var vm = QuetionFactory.Instance.Create();

                int count = vm.Models.Count % 2 == 0 ? vm.Models.Count / 2 : vm.Models.Count / 2 + 1;


                QuetionItemControl control = null;

                for (int i = 0; i < vm.Models.Count; i++)
                {
                    UserControl uc = new UserControl();

                    Button btn = new Button();

                    btn.Content = i;

                    uc.Content = btn;

                    if (i % 2 == 0)
                    {
                        control = new QuetionItemControl();
                        control.Item1 = vm.Models[i];
                        cs.Add(control);
                    }
                    else
                    {
                        control.Item2 = vm.Models[i];
                    }
                }

                this.QustionList = cs;

#if DEBUG

                //Random r = new Random();

                //foreach (var item in this.QustionList)
                //{
                //    QuetionItemControl quetion = item as QuetionItemControl;

                //    if (quetion.Item1 == null) continue;

                //    quetion.Item1.Collection[r.Next(4)].IsChecked = true;

                //    if (quetion.Item2 == null) continue;

                //    quetion.Item2.Collection[r.Next(4)].IsChecked = true;
                //}
#endif

            }

            //  Do：登录成功触发
            else if (command == "LoginModuleSuccessed")
            {

                //  Message：检查是否可以打印
                this.CheckEnblePrint();


                TCMAndArchivesJsonEntity entity = this.LoginInfo.Data as TCMAndArchivesJsonEntity;

                //  ToDo：测试用
                //entity.TCM = null;

                //  Do：没查询到数据
                if (entity == null || entity.TCM == null)
                {
                    Action<MessageResult> Action = l =>
                      {

                          if (l == MessageResult.Cancel)
                          {
                              //this.ShowQ = true;

                              this.IsLogOffModule = true;
                          }
                          else if (l == MessageResult.Sumit)
                          {
                              this.ShowQ = true;
                          }
                          else
                          {
                              //this.ShowQ = true;
                          }
                      };
                    MessageSingleControl.ShowWithCancelAndSumit("无辨识报告,请进行评估", -9, Action);

                    return;

                }

                this.ShowQ = false;

                //  Message：报表列表
                var result = this.Controls = PrintService.Instance.GetPhysicalPages(entity);



            }

            //  Do：取消
            else if (command == "Button_Save")
            {

                List<string> unCheck = new List<string>();

                foreach (var item in this.QustionList)
                {
                    QuetionItemControl quetion = item as QuetionItemControl;

                    if (quetion.Item1 == null) continue;

                    var result = quetion.Item1.Collection.ToList().Exists(l => l.IsChecked);

                    if (!result)
                    {
                        unCheck.Add(quetion.Item1.Index);
                    }

                    if (quetion.Item2 == null) continue;

                    result = quetion.Item2.Collection.ToList().Exists(l => l.IsChecked);

                    if (!result)
                    {
                        unCheck.Add(quetion.Item2.Index);
                    }
                }

                Debug.WriteLine("Button_Save");

                if (unCheck.Count > 0)
                {
                    List<string> collection1 = new List<string>();

                    if (unCheck.Count > 7)
                    {
                        collection1.AddRange(unCheck.TakeFromTo(0, 5));
                    }
                    else
                    {
                        collection1.AddRange(unCheck);
                    }

                    Action<MessageResult> ResultAction = l =>
                     {
                         Application.Current.Dispatcher.Invoke(() =>
                         {
                             this.PageSelect = collection1[0].ToInt() % 2 == 1? ((int)(collection1[0].ToInt() / 2) + 1):(int)(collection1[0].ToInt() / 2);
                         });

                     };

                    MessageSingleControl.Show("请回答下列问题..." + Environment.NewLine + collection1.Aggregate((l, k) => l + "、 " + k), 3, ResultAction);
                    return;
                }

                //  ToDo：上传
                TCMAndArchivesJsonEntity last = this.LoginInfo.Data as TCMAndArchivesJsonEntity;

                TCMDetailDataEntity entity = new TCMDetailDataEntity();

                entity.ItemDO = new TCMConstitutionItem();

                entity.Tcm = new TCM();

                entity.Tcm.UpdateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                List<string> collection = new List<string>();

                foreach (var item in this.QustionList)
                {

                    QuetionItemControl quetion = item as QuetionItemControl;

                    if (quetion.Item1 == null) continue;

                    var result = quetion.Item1.Collection.ToList().Find(l => l.IsChecked);

                    collection.Add(result.Value.ToString());

                    if (quetion.Item2 == null) continue;

                    result = quetion.Item2.Collection.ToList().Find(l => l.IsChecked);

                    collection.Add(result.Value.ToString());
                }

                entity.Tcm.TZBS = collection.Aggregate((l, k) => l + k);

                Action action = () =>
                {
                    string err;

                    last.TCM = entity;

                    last = DataManager.DatasManager.TCMQualityDiagnosis(last);

                    bool r = DataManager.DatasManager.AddTCMReport(last, out err);

                    if (!r)
                    {
                        MessageSingleControl.Show(err);
                        return;
                    }

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.RelayMethod("LoginModuleSuccessed");
                    });


                    this.ShowQ = false;
                };

                WaittingSingleControl.Show("正在保存，请等待...", action);


            }

            else if (command == "ReWork")
            {

                //  Message：清理數據
                foreach (var item in this.QustionList)
                {
                    QuetionItemControl quetion = item as QuetionItemControl;

                    if (quetion.Item1 == null) continue;

                    quetion.Item1.Collection.ToList().ForEach(l => l.IsChecked = false);

                    if (quetion.Item2 == null) continue;

                    quetion.Item2.Collection.ToList().ForEach(l => l.IsChecked = false);
                }

                this.PageSelect = 1;
            }


            if (obj is TPageControl)
            {
                TPageControl control = obj as TPageControl;

                TCMAndArchivesJsonEntity entity = LoginInfo.Data as TCMAndArchivesJsonEntity;

                string err;


                bool enble = DataManager.DatasManager.PrintEnable(entity.PrintRecord, ModuleManager.ModuleConfig[LoginInfo.ModuleName], out err);

                if (enble)
                {
                    MessageSingleControl.Show("正在打印,请稍等...", 10);

                    control.Print();

                    DataManager.DatasManager.UpdateReportPrintMark(entity.TCM.Tcm.ID, ModuleManager.ModuleConfig[LoginInfo.ModuleName], out err);

                    if (!string.IsNullOrEmpty(err))
                    {
                        MessageSingleControl.Show(err);
                        return;
                    }
                }
                else
                {
                    MessageSingleControl.Show(err);
                    return;
                }
            }


            ////  Do：取消
            //else if (command == "Qutionlist_GoBack")
            //{
            //    //this.IsShowquetion = true;
            //    this.ShowQ = false;

            //}






        }
    }
}
