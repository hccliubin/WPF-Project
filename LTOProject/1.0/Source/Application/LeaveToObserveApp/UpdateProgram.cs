using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net;

namespace Changhong.Product.HealthyCottage
{
    public partial class UpdateProgram : Form
    {
        //public delegate void UpdateSuccess(object sender, bool issuccess);
        //public event UpdateSuccess OnUpdateSuccess;
        string updateurl;
        string programName;
        public UpdateProgram(string url,string name)
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;//跨线程关闭该界面。
            InitializeComponent();
            if (!Directory.Exists(@"C:\update"))
            {
                Directory.CreateDirectory(@"C:\update");
            }
            updateurl = url;
            programName = name;
            Thread tup = new Thread(() =>
            {
                DownloadFile(updateurl, @"C:\update\"+programName, progressBar1, label1);
            });
            tup.Name = "更新线程";
            tup.IsBackground = true;
            tup.Start();
            //DownloadFile(updateurl, @"C:\update\WeChat.exe", progressBar1, label1);
        }
        /// <summary>        
        /// c#,.net 下载文件        
        /// </summary>        
        /// <param name="URL">下载文件地址</param>  
        /// <param name="Filename">下载后的存放地址</param>        
        /// <param name="Prog">用于显示的进度条</param>        
        /// 
        public void DownloadFile(string URL, string filename, System.Windows.Forms.ProgressBar prog, System.Windows.Forms.Label label1)
        {
            float percent = 0;
            try
            {
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
                if (prog != null)
                {
                    prog.Maximum = (int)totalBytes;
                }
                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    System.Windows.Forms.Application.DoEvents();
                    so.Write(by, 0, osize);
                    if (prog != null)
                    {
                        prog.Value = (int)totalDownloadedByte;
                    }
                    osize = st.Read(by, 0, (int)by.Length);

                    percent = (float)totalDownloadedByte / (float)totalBytes * 100;
                    BeginInvoke((MethodInvoker)delegate
                    {
                        label1.Text = "当前新版程序下载进度" + percent.ToString() + "%";
                    });
                    //label1.Text = "当前补丁下载进度" + percent.ToString() + "%";
                    System.Windows.Forms.Application.DoEvents(); //必须加注这句代码，否则label1将因为循环执行太快而来不及显示信息
                }
                so.Close();
                st.Close();
                //if (OnUpdateSuccess!=null)
                //{
                //    OnUpdateSuccess(this, true);
                //    Close();
                //}
                StartInstall();

            }
            catch (System.Exception e)
            {
                MessageBox.Show("更新失败，请重试");
                this.Close();
                
            }
        }

        private void UpdateProgram_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
            //this.Close();
        }

        void StartInstall()
        {
            Thread ts = new Thread(() =>
            {
                try
                {
                    System.Diagnostics.Process.Start(@"C:\update\"+programName);
                    this.Close();
                }
                catch (System.Exception ex)
                {
                    System.Environment.Exit(0);
                }

            });
            ts.Name = "启动安装线程";
            ts.IsBackground = true;
            ts.Start();
        }
       
    }

}
