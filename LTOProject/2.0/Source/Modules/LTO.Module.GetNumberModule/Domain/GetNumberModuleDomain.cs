using LTO.Domain.DataService;
using LTO.General.Model;
using LTO.General.ModuleManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ZHJK.Library.General.Printer.SPRT;

namespace LTO.Module.GetNumberModule
{
    class GetNumberModuleDomain
    {
        public static GetNumberModuleDomain Instance = new GetNumberModuleDomain();

        /// <summary> 获取打印配置 </summary>
        public string GetConfigPrintLimit()
        {
            return ServiceManager.DataService.GetConfigByID("EnblePrint");
        }

        /// <summary> 获取地址抬头</summary>
        public string GetConfigAddress()
        {
            return ServiceManager.DataService.GetConfigByID("Address");
        }

        Random r = new Random();
        public void PostGetChildInfo(string id, string type, string idx, Action<RegisterEntity, string, string> action)
        {
            string err;

            var result = ServiceManager.DataService.PostRegisterDefend(id, type, idx,out err);

            if (result == null)
            {
                action(null, "取号失败！请联系【预检台】", err);
                return;
            }

            if (result.code == "201")
            {
                action(result, "取号失败！", "请联系预检台");
            }
            else if (result.code == "200")
            {
                action(result, "取号成功!" + result.hzxm + "排号为【" + result.rowNum + "】", "请到预检台排队，目前有" + result.preRowNum + "人");
            }
            else if (result.code == "205")
            {
                action(result, result.hzxm + "排号为【" + result.rowNum + "】已呼叫", "您的排号已呼叫，请立刻到【预检台】");
            }
            else if (result.code == "204")
            {
                action(result, result.hzxm + "排号为【" + result.rowNum + "】正在呼叫", "正在呼叫您的排号，请立刻到【预检台】");
            }
            else if (result.code == "202")
            {
                action(result, result.hzxm + "排号为【" + result.rowNum + "】未签退", " 接种完请留观结束后签退");
            }
            else if (result.code == "203")
            {
                action(result, result.hzxm + "排号为【" + result.rowNum + "】已签退", "接种完成，请注意下次接种时间");
            }
            else if (result.code == "206")
            {
                action(result, result.hzxm + "排号为【" + result.rowNum + "】已取号", "请到预检台排队，目前有" + result.preRowNum + "人");
            }
            else
            {
                //action(result.code, "张小小小，排号为【B163】", "请到预检台排队，目前有43人");
                action(result, "提示信息", result.msg);
            }
        }

        /// <summary> 开始读取卡号 </summary>
        public void BegionGetChildID(Action<string, string> action)
        {
            ServiceManager.ToolService.StartScanEngine(action);

            //var result = r.Next(5);

            //if (result == 1)
            //    return null;

            //return "824888519445";
        }


        public void PrintGrid(Grid grid)
        {
            ServiceManager.ToolService.PrintGrid(grid);
        }

        /// <summary> 根据当前操作系统返回指定页面 </summary>
        public IModulePage GetModulePage()
        {
            var result = ServiceManager.ToolService.IsWin7();


            if (result)
            {
                return new GetNumModuleControl_Santai();
            }
            return new GetNumModuleControl();
        }


        SPRTPrinterService service = new SPRTPrinterService();

        /// <summary> 打印 </summary>
        public bool Print(string orgName, string num, string name, string date, out string err, uint printType = 0)
        {
            return service.Print(orgName, num, name, date, out err);
        }

    }
}
