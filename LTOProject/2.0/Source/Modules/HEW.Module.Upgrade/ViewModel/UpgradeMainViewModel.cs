using HEW.Base.Frame.MVVM;
using HEW.Module.Upgrade.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HEW.Module.Upgrade.ViewModel
{
    class UpgradeMainViewModel
    {

        public UpgradeMainModel UpgradeMainModel { get; set; }

        public RelayCommand LoadDelegateCommand { get; set; }

        public UpgradeMainViewModel()
        {
            UpgradeMainModel = new UpgradeMainModel();
            LoadDelegateCommand = new RelayCommand(LoadFunc);
        }

        /// <summary>
        /// 页面加载数据
        /// </summary>
        /// <param name="obj"></param>
        private void LoadFunc(object obj)
        {
            System.Threading.Thread tup = new System.Threading.Thread(() =>
            {
                int len = UpgradeMainModel.URL.LastIndexOf("/");
                string programName = UpgradeMainModel.URL.Substring(len + 1);
                DownloadFile(UpgradeMainModel.URL, @"C:\update\" + programName);
            });
            tup.Name = "更新线程";
            tup.IsBackground = true;
            tup.Start();

        }


        /// <summary>        
        /// c#,.net 下载文件        
        /// </summary>        
        /// <param name="URL">下载文件地址</param>  
        /// <param name="Filename">下载后的存放地址</param>        
        /// <param name="Prog">用于显示的进度条</param>        
        /// 
        public void DownloadFile(string URL, string filename)
        {
            float percent = 0;
            try
            {
                if (!Directory.Exists(@"C:\update\")) Directory.CreateDirectory(@"C:\update\");
                HttpWebRequest Myrq = HttpWebRequest.Create(URL) as HttpWebRequest;
                //Myrq.Method = "POST";
                //Myrq.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                Myrq.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705;)";
                //Myrq.Accept = "*/*";
                //Myrq.KeepAlive = true;
                //Myrq.ProtocolVersion = HttpVersion.Version10;
                //Myrq.Timeout = 30000;
                HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;

                Stream st = myrp.GetResponseStream();
                Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;

                    so.Write(by, 0, osize);

                    osize = st.Read(by, 0, (int)by.Length);

                    percent = (float)totalDownloadedByte / (float)totalBytes * 100;

                    UpgradeMainModel.PrograssBarValue = (int)percent;

                    UpgradeMainModel.PrograssBarPercentage = percent.ToString("0.0") + "%";
                }
                so.Close();
                st.Close();
                StartInstall();

            }
            catch (System.Exception e)
            {
                System.Windows.MessageBox.Show("更新失败，请重试");

            }
        }

        private void StartInstall()
        {
            System.Threading.Thread ts = new System.Threading.Thread(() =>
            {
                try
                {
                    int len = UpgradeMainModel.URL.LastIndexOf("/");
                    string programName = UpgradeMainModel.URL.Substring(len + 1);
                    System.Diagnostics.Process.Start(@"C:\update\" + programName);
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    Environment.Exit(0);
                }

            });
            ts.Name = "启动安装线程";
            ts.IsBackground = true;
            ts.Start();
        }
    }
}
