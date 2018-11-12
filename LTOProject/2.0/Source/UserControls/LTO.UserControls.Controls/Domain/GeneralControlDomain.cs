
using LTO.Domain.DataService;
using LTO.General.ModuleManager;
using LTO.Module.GetNumberModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTO.UserControls.Controls
{
    /// <summary> 领域模型 </summary>
    class GeneralControlDomain
    {
        public static GeneralControlDomain Instance = new GeneralControlDomain();

        /// <summary> 获取开机启动页</summary>
        public string GetConfigStartIndex()
        {
           return ServiceManager.DataService.GetConfigByID("StartPage");
        }

        /// <summary> 获取打印配置 </summary>
        public string GetConfigPrintLimit()
        {
            return ServiceManager.DataService.GetConfigByID("EnblePrint");
        }

        public void SetConfigStartIndex(string value)
        {
            ServiceManager.DataService.SetConfigByID("StartPage", value);
        }

        public void SetConfigPrintLimit(string value)
        {
            ServiceManager.DataService.SetConfigByID("EnblePrint", value);
        }


        /// <summary> 保存打印 配置 </summary>
        public bool SavePringConfig(out string err)
        {
            //DataManager.DatasManager.PrintConfigSave(out err);

            //return string.IsNullOrEmpty(err);
            err = string.Empty;
            return false;
        }


        /// <summary> 显示系统键盘 </summary>
        public void ShowKeyBoard()
        {
            //DataManager.DatasManager.ShowKeyBoard();
        }

    }
}
