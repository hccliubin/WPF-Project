using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZHJK.Library.General.Scanner.VGuang;

namespace LTO.General.SystemTool
{
    public class ScanService : IDisposable
    {

        VGuangService service = new VGuangService();

        /// <summary> 初始化 </summary>
        public bool Init(out string err)
        {
            err = string.Empty;

            if (!service.OpenDevice(out err))
            {
                Debug.WriteLine(err);
                return false;
            }

            Thread.Sleep(1000);

            if (!service.LightOn(out err))
            {
                Debug.WriteLine(err);
                return false;
            }

            return true;
        }

        async Task<Tuple<string, string>> AsyncBeginRead()
        {

            string err = null;

            string result = null;

            int n = 0;

            await Task.Run(() =>
            {
                while (true)
                {
                    n++;

                    result = service.MessageRead(out err);

                    if (result == null && !string.IsNullOrEmpty(err))
                    {
                        Debug.WriteLine("读取错误:" + err);
                        return new Tuple<string, string>(result, err);
                    }

                    if (result != null)
                    {
                        //service.LightOff(out err);
                        Debug.WriteLine("读取成功:" + result);
                        return new Tuple<string, string>(result, err);
                    }

                    Thread.Sleep(100);
                }
            });

            return new Tuple<string, string>(result, err);
        }


        public async void StartEngine(Action<string, string> action)
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    var result = await this.AsyncBeginRead();

                    action(result.Item1, result.Item2);

                    //  Message：读取成功或读取失败都等待3秒再读取
                    Thread.Sleep(3000);

                }
            });
        }

        public void Dispose()
        {
            string err;

            service.LightOff(out err);

            service.CloseDevice(out err);
        }
    }
}
