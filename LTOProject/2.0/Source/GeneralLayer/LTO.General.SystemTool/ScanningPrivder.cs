using LTO.Base.Tool;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTO.General.SystemTool
{
    public class ScanningPrivder
    {
        public static ScanningPrivder Instance = new ScanningPrivder();

        public void StartEngine()
        {
            HookKeyboardEngine.KeyDown += HookKeyboardEngine_KeyDown;
        }

        public void StopEngine()
        {
            HookKeyboardEngine.KeyDown -= HookKeyboardEngine_KeyDown;
        }
        private StringBuilder inputKey = new StringBuilder();

        DateTime previewTime;

        private void HookKeyboardEngine_KeyDown(object sender, KeyEventArgs e)
        {
            string temp = string.Empty;

            DateTime nowTime = DateTime.Now;

            //判断是不是数字的键盘值
            if ((e.KeyData >= Keys.D0 && e.KeyData <= Keys.D9) || (e.KeyData >= Keys.NumPad0 && e.KeyData <= Keys.NumPad9))
            {

                temp = (e.KeyData.ToString()).Last().ToString();
            }
            else
            {
                temp = e.KeyData.ToString();
            }
            //通过判断键盘输入的间隔来确定是扫描枪还是通过键盘输入的
            if ((nowTime - previewTime).Milliseconds < 50)
            {
                Debug.WriteLine(e.KeyData);

                if (this.IsMatch(e.KeyValue, inputKey))
                {
                    //this.tb_Key.Text = inputKey.ToString();


                    Debug.WriteLine(inputKey);

                    if (CallBackScanning != null)
                    {
                        string str = this.FilterString(inputKey.ToString());
                        CallBackScanning(str);
                    }
                    inputKey = new StringBuilder();
                    previewTime = DateTime.Now;
                    return;
                }

                inputKey.Append(temp);

            }
            else
            {
                inputKey = new StringBuilder(temp);
            }
            previewTime = DateTime.Now;
        }

        public event Action<string> CallBackScanning;


        public bool IsMatch(int lastKeyValue, StringBuilder inputKey)
        {
            return (lastKeyValue == (int)Keys.Return || lastKeyValue == (int)Keys.Down) && inputKey.Length > 10;
        }

        string FilterString(string str)
        {

            str = str.Replace(Keys.LShiftKey.ToString(), "");
            str = str.Replace(Keys.LControlKey.ToString(), "");
            str = str.Replace(Keys.Return.ToString(), "");
            str = str.Replace(Keys.CapsLock.ToString(), "");
            str = str.Replace(Keys.Cancel.ToString(), "");
            str = str.Replace(Keys.Space.ToString(), " ");
            str = str.Replace(Keys.Down.ToString(), "");
            str = str.Replace(Keys.OemMinus.ToString(), "-");
            return str;
        }
    }
}
