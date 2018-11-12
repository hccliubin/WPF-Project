using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CH.Product.General.NetWork;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using CH.Product.Domain.DataService.Service;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            HttpPostHelper helper = new HttpPostHelper();

            IDictionary<string, string> dic = new Dictionary<string, string>();
            
            //dic.Add("code", "123456");

            string err;

            JContainer json;

            helper.PostData("http://192.168.31.180:8080/child/registerDefend.do?code=123456", dic, 
                out json, out err);


            if (json == null)

            {
                Debug.WriteLine(err);

            }
            else
            {
                Debug.WriteLine(json.ToString());
            }

           
        }

        [TestMethod]
        public void TestMethodLogin()
        {

            HttpPostHelper helper = new HttpPostHelper();

            IDictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("username", "admin");
            dic.Add("password", "123456");
            string err;

            JContainer json;

            helper.PostData("http://192.168.31.180:8080/child/login.do", dic, out json, out err);

            if (json == null)

            {
                Debug.WriteLine(err);

            }
            else
            {
                Debug.WriteLine(json.ToString());
            }


        }

        [TestMethod]
        public void TestMethodChangeState()
        {

            HttpPostHelper helper = new HttpPostHelper();

            IDictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("username", "admin");
            dic.Add("password", "123456");
            string err;

            JContainer json;

            helper.PostData("http://192.168.31.180:8080/child/login.do", dic, out json, out err);

            if (json == null)

            {
                Debug.WriteLine(err);

            }
            else
            {
                Debug.WriteLine(json.ToString());
            }


        }

        [TestMethod]
        public void TestMethodNetWorkGetList()
        {

            HttpPostHelper helper = new HttpPostHelper();

            IDictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("yljgdm", "67613231-0");
            string err;

            JContainer json;

            helper.PostData("http://192.168.31.180:8080/child/queryObservHz.do", dic, out json, out err);

            if (json == null)

            {
                Debug.WriteLine(err);

            }
            else
            {
                Debug.WriteLine(json.ToString());
            }


        }
        //http:/192.168.31.180:8080/login.do

        [TestMethod]
        public void TestMethodSpeek()
        {

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                SpeechService.Instance.Speek("hello");
            }

        }

        [TestMethod]
        public void TestMethodGetList()
        {
            string err;

            DataService.Instance.GetList(out err);

        }

        [TestMethod]
        public void TestMethodWcf()
        {

            HttpPostHelper helper = new HttpPostHelper();

            IDictionary<string, string> dic = new Dictionary<string, string>();

            string path = @"D:\LeaveToObserveProgram\Solution";
            dic.Add("parent", path);
            string err;

            string param = "value=2";

            JContainer json;

            helper.postSend(@"http://localhost:8733/Design_Time_Addresses/WcfServiceLibrary1/Service1/GetData", param);

            //if (json == null)

            //{
            //    //Debug.WriteLine(err);

            //}
            //else
            //{
            //    Debug.WriteLine(json.ToString());
            //}


        }
    }
}
