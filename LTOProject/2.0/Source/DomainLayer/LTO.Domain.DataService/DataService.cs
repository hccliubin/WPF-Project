using LTO.Base.Product.Provider;
using LTO.Base.Theme.Style;
using LTO.General.Logger;
using LTO.General.Model;
using LTO.General.NetWork;
using LTO.General.SystemTool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LTO.Domain.DataService
{
    public class DataService : IDisposable
    {

        public void Init()
        {
            //  Message：初始化日志
            this.InitLogger();

            this.InitSystemInfo();


        }

        #region - 日志 -

        void InitLogger()
        {
            string exeFileFullPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string exeName = Path.GetFileNameWithoutExtension(exeFileFullPath);
            string binPath = Path.GetDirectoryName(exeFileFullPath);

            binPath = Path.GetDirectoryName(binPath);

            string logFilePath = Path.GetDirectoryName(binPath);

            var exe = System.Diagnostics.Process.GetCurrentProcess();

            if (exe == null) return;

            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string logPath = Path.Combine(documentPath, "Logs", exeName);

            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            LogService.Instance.Log4Net.InitLogger(logPath, System.Diagnostics.Process.GetCurrentProcess().ProcessName);
        }

        public void LogInfo(params string[] message)
        {
            LogService.Instance.Log4Net.Info(message);
        }

        public void LogError(params Exception[] ex)
        {
            LogService.Instance.Log4Net.Error(ex);
        }

        #endregion

        #region - 初始化系统目录 -

        public void InitSystemInfo()
        {
            string exeFileFullPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string exeName = Path.GetFileNameWithoutExtension(exeFileFullPath);
            string binPath = Path.GetDirectoryName(exeFileFullPath);

            binPath = Path.GetDirectoryName(binPath);

            AppSystemInfoService.Instance.Init(binPath, exeName);
        }

        #endregion

        #region - 定时任务 -

        MonitorActiveService _monitor = new MonitorActiveService();
        public void StartMonitor()
        {
            _monitor.StartMonitor();
        }

        public Action OnCheckCount
        {
            get
            {
                return _monitor.OnCheckCount;
            }
            set
            {
                _monitor.OnCheckCount = value;
            }
        }


        #endregion

        #region - 配置文件 -

        /// <summary> 读取配置文件中制定节点的信息 </summary>
        public string GetConfigByID(string id)
        {
            return StringResourceService.Instance.GetStringByID(id);
        }

        /// <summary> 设置配置文件中制定节点的信息 </summary>
        public void SetConfigByID(string id, string value)
        {
            StringResourceService.Instance.SetStringByID(id, value);
        }


        #endregion


        #region - NetWork -

        NetWorkService _netWorkService = new NetWorkService();

        /// <summary> 人数统计接口 </summary>
        public CountEntity GetCountEntity(out string err)
        {
            string jgdm = this.GetConfigByID("GovernmentUnit");

            return _netWorkService.GetCountEntity(jgdm, out err);
        }

        /// <summary> 挂号接口 </summary>
        public RegisterEntity PostRegisterDefend(string code, string type, string idx, out string err)
        {

            string jgdm = this.GetConfigByID("GovernmentUnit");

            return _netWorkService.PostRegisterDefend(code, jgdm, type, idx, out err);

        }

        /// <summary> 签退接口 </summary>
        public RegisterEntity PostUpdateObservByMykh(string code, out string err)
        {
            return _netWorkService.PostUpdateObservByMykh(code, out err);

        }


        /// <summary> 可签退列表数据接口 </summary>
        public List<LeaveEntity> PostQueryObservHz(out string err)
        {

            string jgdm = this.GetConfigByID("GovernmentUnit");

            return _netWorkService.PostQueryObservHz(jgdm, out err);

        }

        public void Dispose()
        {
           
        }
        #endregion
    }
}
