using HEW.Base.Theme.Style;
using HEW.General.Data.Manager;
using HEW.General.Model.Network;
using HEW.UserControls.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HEW.Module.Signing
{
    partial class SigningModuleNotifyClass : ReportModuleNotifyClass
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


                SigningDetailJsonModel model=LoginInfo.Data as SigningDetailJsonModel;

                this.Controls = PrintService.Instance.GetSignPages(LoginInfo.Data as SigningDetailJsonModel);

            }

            if (obj is TPageControl)
            {
                TPageControl control = obj as TPageControl;

                SigningDetailJsonModel entity = LoginInfo.Data as SigningDetailJsonModel;

                string err;


                bool enble = DataManager.DatasManager.PrintEnable(entity.PrintRecord, ModuleManager.ModuleConfig[LoginInfo.ModuleName], out err);

                if (enble)
                {
                    MessageSingleControl.Show("正在打印,请稍等...", 10);

                    control.Print();

                    DataManager.DatasManager.UpdateReportPrintMark(entity.ID, ModuleManager.ModuleConfig[LoginInfo.ModuleName], out err);

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
