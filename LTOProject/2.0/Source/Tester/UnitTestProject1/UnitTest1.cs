using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;
using System.Text;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SerialPort ComDevice = new SerialPort("COM6", 115200, Parity.None, 8, StopBits.One);

            //ComDevice.Encoding = System.Text.Encoding.GetEncoding("GB2312");

            var collection = SerialPort.GetPortNames();

            foreach (var item in collection)
            {

                Debug.WriteLine(item);
            }


            //口相关属性
            //ComDevice.PortName = "Com6";
            //ComDevice.BaudRate = 115200;
            //ComDevice.Parity = (Parity)Convert.ToInt32(comboBox_CheckBits.SelectedIndex.ToString());
            //ComDevice.DataBits = Convert.ToInt32(comboBox_DataBits.SelectedItem.ToString());
            //ComDevice.StopBits = (StopBits)Convert.ToInt32(comboBox_StopBits.SelectedItem.ToString());




            Debug.WriteLine(ComDevice.IsOpen);

            ComDevice.DataReceived += (l, k) =>
            {

                Debug.WriteLine(DateTime.Now + "接收到数据");

                // 开辟接收缓冲区
                byte[] ReDatas = new byte[ComDevice.BytesToRead];
                //从串口读取数据
                ComDevice.Read(ReDatas, 0, ReDatas.Length);
                //实现数据的解码与显示

                AddData(ReDatas);
            };

            //开启串口
            ComDevice.Open();

            while (true)
            {
                Thread.Sleep(3000);
            }
        }

        /// <summary>
        /// 解码过程
        /// </summary>
        /// <param name="data">串口通信的数据编码方式因串口而异，需要查询串口相关信息以获取</param>
        public void AddData(byte[] data)
        {
            string s = string.Empty;
            foreach (var item in data)
            {
                s += ((int)item) + ",";
                //Debug.WriteLine((int)(item & 0xff));

                //BitConverter.ToInt32(item, 0)

                Debug.WriteLine(item);

            }

            int ii = System.BitConverter.ToInt32(data, 0);
            Debug.WriteLine(ii);

            Debug.WriteLine(System.Text.Encoding.ASCII.GetString(data));

            //for (int i = 0; i < data.Length; i++)
            //{
            //    try
            //    {
            //        var iii = System.BitConverter.ToInt64(data, i);
            //        Debug.WriteLine(iii);
            //    }
            //    catch
            //    {
            //        Debug.WriteLine("异常" + data);
            //    }
            //}

            for (int i = 0; i < data.Length; i++)
            {
                try
                {

                    var iii = byteToHexStr(data, i);
                    Debug.WriteLine(iii);
                }
                catch
                {
                    Debug.WriteLine("异常" + data);
                }
            }




            // var t = System.BitConverter.ToInt64(data, 0);

            //Debug.WriteLine(t);

            //var ss = System.BitConverter.ToSingle(data, 0);

            //Debug.WriteLine(ss);


            //Debug.WriteLine(BitConverter.ToString(data, 0));

            //Debug.WriteLine(System.Text.Encoding.ASCII.GetString(data));

            //Debug.WriteLine(System.Text.Encoding.GetEncoding("GB2312").GetString(data));


            //Debug.WriteLine("BitConverter.ToInt16(data, 8)");
            //Debug.WriteLine(BitConverter.ToInt16(data, 8));

            //Debug.WriteLine("BitConverter.ToInt32(data, 8)");
            //Debug.WriteLine(BitConverter.ToInt32(data, 4));

            //Debug.WriteLine("Encoding.Unicode.GetString(data)");
            //Debug.WriteLine(Encoding.Unicode.GetString(data));

            //Debug.WriteLine("Encoding.ASCII.GetString(data)");
            //Debug.WriteLine("Encoding.ASCII.GetString(data)");

            //Debug.WriteLine("Encoding.UTF8.GetString(data)");
            //Debug.WriteLine(Encoding.UTF8.GetString(data));


            //Debug.WriteLine("Encoding.Default.GetString(data)");
            //Debug.WriteLine(Encoding.Default.GetString(data));


            Debug.WriteLine(s.Trim(','));


            //if (radioButton_Hex.Checked)
            //{
            //    StringBuilder sb = new StringBuilder();

            //    for (int i = 0; i<data.Length; i++)
            //    {
            //        sb.AppendFormat("{0:x2}" + " ", data[i]);
            //    }
            //    AddContent(sb.ToString().ToUpper());
            //}
            //else if (radioButton_ASCII.Checked)
            //{
            //    AddContent(new ASCIIEncoding().GetString(data));
            //}
            //else if (radioButton_UTF8.Checked)
            //{
            //    AddContent(new UTF8Encoding().GetString(data));
            //}
            //else if (radioButton_Unicode.Checked)
            //{
            //    AddContent(new UnicodeEncoding().GetString(data));
            //}
            //else
            //{
            //    StringBuilder sb = new StringBuilder();
            //    for (int i = 0; i<data.Length; i++)
            //    {
            //        sb.AppendFormat("{0:x2}" + " ", data[i]);
            //    }
            //    AddContent(sb.ToString().ToUpper());
            //}
        }


        [TestMethod]
        public void TestMethod2()
        {
            byte[] bs = new byte[] { 0x30, 0x31 };




            foreach (var item in bs)
            {

                Debug.WriteLine(item.ToString());

            }

            var s = BitConverter.ToString(bs, 0);

            var re = Convert.ToString(bs);

            string str = System.Text.Encoding.Default.GetString(bs);

            var f = BitConverter.ToSingle(bs, 0);

            var v = BitConverter.ToInt16(bs, 0);
            var k = BitConverter.ToInt32(bs, 0);

            bs = new byte[] { 0, 0, 243, 94 };


            k = BitConverter.ToInt32(bs, 0);
            v = BitConverter.ToInt16(bs, 0);

            string str1 = string.Empty;
        }

        [TestMethod]
        public void TestMethod3()
        {
            //byte[] bs = new byte[] { 255, 254, 255, 54, 22, 246, 141, 229 };

            //var s = BitConverter.ToString(bs, 0);

            //var f = BitConverter.ToSingle(bs, 0);

            //var v = BitConverter.ToInt16(bs, 0);
            //var k = BitConverter.ToInt32(bs, 0);

            //bs = new byte[] { 0, 0, 243, 94 };

            //k = BitConverter.ToInt32(bs, 0);
            //v = BitConverter.ToInt16(bs, 0);

            //string str = string.Empty;

            //byte b = Convert.ToByte("00000101", 2);
            //byte b1 = Convert.ToByte("00000101", 8);
            //byte b2 = Convert.ToByte("00000101", 10);
            //byte b3 = Convert.ToByte("00000101", 16);
            //byte b4 = Convert.ToByte("00000101", 32);

            string s1 = ((char)101).ToString();

            Debug.WriteLine(s1);

        }


        public static int byteToInt2(byte[] b)
        {

            int mask = 0xff;

            int temp = 0;

            int n = 0;

            for (int i = 0; i < b.Length; i++)
            {

                n <<= 8;

                temp = b[i] & mask;

                n |= temp;

            }

            return n;

        }


        public static int byteArrayToInt(byte[] b)
        {
            byte[] a = new byte[4];
            int i = a.Length - 1, j = b.Length - 1;
            for (; i >= 0; i--, j--)
            {//从b的尾部(即int值的低位)开始copy数据
                if (j >= 0)
                    a[i] = b[j];
                else
                    a[i] = 0;//如果b.length不足4,则将高位补0
            }
            int v0 = (a[0] & 0xff) << 24;//&0xff将byte值无差异转成int,避免Java自动类型提升后,会保留高位的符号位
            int v1 = (a[1] & 0xff) << 16;
            int v2 = (a[2] & 0xff) << 8;
            int v3 = (a[3] & 0xff);
            return v0 + v1 + v2 + v3;
        }


        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string byteToHexStr(byte[] bytes, int index)
        {
            bytes = bytes.Skip(index).ToArray();
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }


        [TestMethod]
        public void TestMethod6()
        {
            string str = "mykh=444411&type=1&prepay_id=a6939fa2-fc85-451c-afb5-1fabe140331b&idx=A5";

            if (str.Trim().StartsWith("mykh"))
            {
                var result = str.Trim().Split('=', '&');

            }


        }
    }
}
