using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using LTO.General.NetWork;
using System.Diagnostics;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {

            NetWorkService service = new NetWorkService();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("jgdm", "729818173");

            string err;

            string url = "http://192.168.5.15:8080/register/countNum.do";

           var result= service.Post(url, dic, out err);

            Debug.WriteLine(result.ToString());


            string str = string.Empty;
        }
    }
}
