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

namespace HEW.Module.TCM
{
    partial class TCMModuleNotifyClass : ReportModuleNotifyClass
    {
       
        public override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Init")
            {
                

            }
            //  Do：登录成功触发
            else if (command == "LoginModuleSuccessed")
            {
                //  Message：检查是否可以打印
                this.CheckEnblePrint();

                this.Controls = PrintService.Instance.GetTCMPages(this.LoginInfo.Data as HealthFormAndArchivesJsonEntity);
            }

            if (obj is TPageControl)
            {
                TPageControl control = obj as TPageControl;

                HealthFormAndArchivesJsonEntity entity = LoginInfo.Data as HealthFormAndArchivesJsonEntity;

                string err;
                

                bool enble = DataManager.DatasManager.PrintEnable(entity.PrintRecord, ModuleManager.ModuleConfig[LoginInfo.ModuleName], out err);

                if (enble)
                {
                    MessageSingleControl.Show("正在打印,请稍等...", 10);

                    control.Print();

                    DataManager.DatasManager.UpdateReportPrintMark(entity.HealthFormDTO.ID, ModuleManager.ModuleConfig[LoginInfo.ModuleName], out err);

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
