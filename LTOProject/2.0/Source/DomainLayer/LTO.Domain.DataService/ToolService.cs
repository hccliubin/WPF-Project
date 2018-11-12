using LTO.General.Logger;
using LTO.General.SystemTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LTO.Domain.DataService
{
    public class ToolService : IDisposable
    {

        public void Init()
        {
            string err;

            bool result = service.Init(out err);

            if (!result)
            {
                LogService.Instance.Log4Net.Info("初始化扫描设备错误：" + err);
            }
        }

        public void PrintGrid(Grid grid)
        {
            PrintService.Instance.PrintGrid(grid);
        }

        /// <summary> 获取是否是win7操作系统 </summary>
        public bool IsWin7()
        {
            Version ver = System.Environment.OSVersion.Version;

            return true;

            string strClient = "";

            if (ver.Major == 5 && ver.Minor == 1)
            {
                strClient = "Win XP";
            }
            else if (ver.Major == 6 && ver.Minor == 0)
            {
                strClient = "Win Vista";
            }
            else if (ver.Major == 6 && ver.Minor == 1)
            {
                strClient = "Win 7";
                return true;
            }
            else if (ver.Major == 5 && ver.Minor == 0)
            {
                strClient = "Win 2000";
            }
            else
            {
                strClient = "未知";
            }

            return false;
        }


        ScanService service = new ScanService();


        /// <summary> 执行刷卡引擎 </summary>
        public void StartScanEngine(Action<string, string> action)
        {
            if (this.IsWin7())
            {
                ScanningPrivder.Instance.CallBackScanning += l =>
                {
                    action(l, "识别到条形码！" + l);
                };
                ScanningPrivder.Instance.StartEngine();
            }
            else
            {
                service.StartEngine(action);
            }
        }



        public void Dispose()
        {
            service.Dispose();
        }

        public void PowerOff()
        {
            System.Diagnostics.Process bootProcess = new System.Diagnostics.Process();
            bootProcess.StartInfo.FileName = "shutdown";
            bootProcess.StartInfo.Arguments = "/s";
            bootProcess.Start();
        }


        public void ShutDown()
        {
            ServiceManager.Dispose();
            Environment.Exit(0);
        }
    }
}
