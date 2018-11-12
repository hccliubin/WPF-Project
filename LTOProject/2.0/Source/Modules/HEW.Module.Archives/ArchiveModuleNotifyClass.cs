using HEW.Base.Theme.Style;
using HEW.General.Data.Manager;
using HEW.General.Model.Enums;
using HEW.General.Model.Network;
using HEW.General.Model.Network.Form;
using HEW.UserControls.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HEW.Module.Archives
{

    partial class ArchiveModuleNotifyClass : ReportModuleNotifyClass
    {
        private bool _isShow;
        /// <summary> 说明  </summary>
        public bool IsShow
        {
            get { return _isShow; }
            set
            {
                _isShow = value;
                RaisePropertyChanged("IsShow");
            }
        }


        public override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Init")
            {
                

            }
            //  Do：取消
            else if (command == "LoginModuleSuccessed")
            {
                //  Message：检查是否可以打印
                this.CheckEnblePrint();

                HEWArchivesDetailDataEntity entity = LoginInfo.Data as HEWArchivesDetailDataEntity;
                this.Controls = PrintService.Instance.GetArchivesPages(entity.Person,entity.ProductInformation.OrgName);

            }

            if (obj is TPageControl)
            {
                TPageControl control = obj as TPageControl;

                HEWArchivesDetailDataEntity entity = LoginInfo.Data as HEWArchivesDetailDataEntity;

                string err;

                bool enble = DataManager.DatasManager.PrintEnable(entity.PrintRecord, ReportTypeEnum.Report_Detail_Archives, out err);

                if (enble)
                {

                    MessageSingleControl.Show("正在打印,请稍等...", 10);

                    control.Print();

                    DataManager.DatasManager.UpdateReportPrintMark(entity.Person.ID, ReportTypeEnum.Report_Detail_Archives, out err);

                    if(!string.IsNullOrEmpty(err))
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


        }
    }
}
