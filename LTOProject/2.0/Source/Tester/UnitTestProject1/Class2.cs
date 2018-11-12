using System;
using System.Diagnostics;
using System.Text;
using ZHJK.Library.General.Scanner.VGuang.API;

namespace ZHJK.Library.General.Scanner.VGuang
{
    public class VGuangService
    {
        private readonly VGuangScannerAPI VGuangAPI = new VGuangScannerAPI();

        public bool OpenDevice(out string err)
        {
            bool result;
            try
            {
                err = "";
                if (this.VGuangAPI.OpenDevice(1))
                {
                    result = true;
                }
                else
                {
                    err = "二维码扫描器连接失败";
                    result = false;
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                result = false;
            }
            return result;
        }

        public string MessageRead(out string err)
        {
            string result;
            try
            {
                byte[] bytes;

                int num;

                string text;

                if (this.VGuangAPI.GetResultStr(out bytes, out num))
                {
                    Debug.WriteLine(bytes);
                    Debug.WriteLine(num);

                    string @string = Encoding.Default.GetString(bytes);
                    byte[] bytes2 = Encoding.UTF8.GetBytes(@string);
                    text = Encoding.UTF8.GetString(bytes2, 0, bytes2.Length);

                }
                else
                {
                    text = null;
                }

                err = "";
                result = text;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                result = null;
            }
            return result;
        }

        public bool CloseDevice(out string err)
        {
            bool result;
            try
            {
                this.VGuangAPI.DisConnected();
                err = "";
                result = true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                result = false;
            }
            return result;
        }

        public bool LightOn(out string err)
        {
            bool result;
            try
            {
                this.VGuangAPI.Backlight(true);
                err = "";
                result = true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                result = false;
            }
            return result;
        }

        public bool LightOff(out string err)
        {
            bool result;
            try
            {
                this.VGuangAPI.Backlight(false);
                err = "";
                result = true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                result = false;
            }
            return result;
        }
    }
}
