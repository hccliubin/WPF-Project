using CH.Product.Base.AppSystemInfo.Service;
using CH.Product.Domain.DataService.Service;
using CH.Product.General.Logger;
using Changhong.Product.HealthyCottage;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace LeaveToObserveApp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log4Servcie.Instance.Error((Exception)e.ExceptionObject);

            Application.Current.Dispatcher.Invoke(() => MessageBox.Show("当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员", "意外的操作"));

        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Log4Servcie.Instance.Error(e.Exception);

            //Application.Current.Dispatcher.Invoke(() => MessageBox.Show(e.Exception.Message));

            e.Handled = true;
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            Process thisProc = Process.GetCurrentProcess();

            if (Process.GetProcessesByName(thisProc.ProcessName).Length > 1)
            {
                //MessageWindow.ShowSumit("当前程序已经运行！");

                MessageBox.Show("当前程序已经运行！");
                Application.Current.Shutdown();

                return;
            }

           


        string exeFileFullPath = Assembly.GetEntryAssembly().Location;
            string exeName = System.IO.Path.GetFileNameWithoutExtension(exeFileFullPath);
            string binPath = System.IO.Path.GetDirectoryName(exeFileFullPath);

            binPath = System.IO.Path.GetDirectoryName(binPath);

            AppSystemInfoService.Instance.Init(binPath,exeName);

            string logFilePath = System.IO.Path.GetDirectoryName(binPath);

            //  初始化日志
            Log4Servcie.Instance.InitLogger(logFilePath, System.Diagnostics.Process.GetCurrentProcess().ProcessName);

            Log4Servcie.Instance.Info("程序启动");

            this.ChekVersion();

            base.OnStartup(e);
        }

        /// <summary> 检查版本号 </summary>
        public void ChekVersion()
        {
            FileVersionInfo myFileVersion = FileVersionInfo.GetVersionInfo(System.Windows.Forms.Application.ExecutablePath);
            string version = myFileVersion.FileVersion;

            string message;

           string url=  DataService.Instance.CheckVersion(version, out message);

            if (string.IsNullOrEmpty(url)) return;

            int len = url.LastIndexOf("/");

            string programName = url.Substring(len + 1);

            UpdateProgram form = new UpdateProgram(url, programName);
            form.ShowDialog();

        }
    }
}
