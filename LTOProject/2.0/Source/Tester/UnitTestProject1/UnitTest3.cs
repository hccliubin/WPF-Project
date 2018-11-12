using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using ZHJK.Library.General.Scanner.VGuang;
using System.Diagnostics;
using System.Threading.Tasks;
using LTO.General.SystemTool;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        /// <summary> 重新初始化循环读取 </summary>
        public void TestMethod1()
        {
            //  Message：开关了两次就读取不到了
            while (true)
            {
                string err;
                string result = this.BeginRead(out err);
                Debug.WriteLine(result);
                Debug.WriteLine(err);

                Thread.Sleep(5000); 
            }
        }


        string BeginRead(out string err)
        {

            int n = 0;

            string Title = "";

            VGuangService service = new VGuangService();

            service.OpenDevice(out err);
            //service.LightOff(out err);
            Thread.Sleep(1000);

            //var t = new Thread(() =>
            //{


            Debug.WriteLine(err);
            service.LightOn(out err);

            Debug.WriteLine(err);

            while (true)
            {
                n++;
                var item = service.MessageRead(out err);

                Debug.WriteLine(err);

                Title = item ?? err;

                if (item != null)
                {
                    //service.LightOff(out err);
                    //Debug.WriteLine(item);
                    return item;
                    //break;
                }
                Thread.Sleep(100);
                //Debug.WriteLine(n);
            }
        }

        [TestMethod]
        public void TestMethod2()
        {

            string err;

            VGuangService service = new VGuangService();
            service.OpenDevice(out err);
            service.LightOff(out err);

            //while(true)
            //{

            //}


        }


        [TestMethod]
        /// <summary> 内存循环读取 </summary>
        public void TestMethod3()
        {
            //  Message：存在问题是读了一段时间后就不再读取了
            string err;

            int n = 0;

            string Title = "";

            VGuangService service = new VGuangService();
            service.OpenDevice(out err);
            //service.LightOff(out err);
            Thread.Sleep(1000);
            Debug.WriteLine(err);
            service.LightOn(out err);
            Debug.WriteLine(err);

            while (true)
            {
                n++;
                var item = service.MessageRead(out err);

                Debug.WriteLine(err);

                Title = item ?? err;

                if (item != null)
                {
                    //service.LightOff(out err);
                    Debug.WriteLine(item);
                    //break;
                }
                Thread.Sleep(100);
                Debug.WriteLine(n);
            }
        }


        VGuangService service = new VGuangService();

        async Task<string> BeginRead()
        {
            string err;

            string Title = "";

            if (!service.OpenDevice(out err))
            {
                Debug.WriteLine(err);
                return err;

            }
            //service.LightOff(out err);
            Thread.Sleep(1000);

            if (!service.LightOn(out err))
            {
                Debug.WriteLine(err);
                return err;
            }
            int n = 0;

            string result = null;

            await Task.Run(() =>
            {
                while (true)
                {
                    n++;

                    result = service.MessageRead(out err);

                    if (!string.IsNullOrEmpty(err))
                    {
                        Debug.WriteLine(err);
                        return err;
                    }

                    if (result != null)
                    {
                        service.LightOff(out err);
                        Debug.WriteLine(result);
                        return result;
                    }
                    Thread.Sleep(100);
                }
            });

            return result;


        }


        [TestMethod]
        public void TestMethod4()
        {

            Task<string> t = this.BeginRead();

            t.ContinueWith(l => Debug.WriteLine(l.Result));

            while (true)
            {

                Thread.Sleep(1000);

                Debug.WriteLine(DateTime.Now);

            }

        }
        [TestMethod]
        public void TestMethod5()
        {

            ScanService service = new ScanService();

            string err;

            bool result = service.Init(out err);

            if (!result)
            {
                Debug.WriteLine(err);
                Assert.Fail("初始化失败");
            }

            Action<string, string> action = (l, k) =>
               {
                   Debug.WriteLine(l);
                   Debug.WriteLine(k);
               };

            service.StartEngine(action);

            while(true)
            {
                Debug.WriteLine("running...");

                Thread.Sleep(3000);

            }

        }

    }
}

