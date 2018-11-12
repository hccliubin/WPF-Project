using HEW.Base.Theme.Style;
using HEW.General.Data.Manager;
using HEW.General.Model.Network;
using HEW.General.Model.Network.Form;
using HEW.UserControls.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HEW.Module.Followup
{
    class FollowupModuleNotifyClass : ReportModuleNotifyClass
    {
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

                this.Controls = PrintService.Instance.GetFollowUpPages(this.LoginInfo.Data as ChronicFollowUpDataEntity);

            }

            if (obj is TPageControl)
            {
                TPageControl control = obj as TPageControl;

                ChronicFollowUpDataEntity entity = LoginInfo.Data as ChronicFollowUpDataEntity;

                string err;

                bool enble = DataManager.DatasManager.PrintEnable(entity.PrintRecord, ModuleManager.ModuleConfig[LoginInfo.ModuleName], out err);

                if (enble)
                {
                    MessageSingleControl.Show("正在打印,请稍等...", 10);

                    control.Print();

                    DataManager.DatasManager.UpdateReportPrintMark(entity.Person.ID, ModuleManager.ModuleConfig[LoginInfo.ModuleName], out err);

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
        }
    }
}
