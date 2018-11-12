using System;
using System.Runtime.InteropServices;

namespace ZHJK.Library.General.Scanner.VGuang.API
{
    internal class VGuangScannerAPI
    {
        private IntPtr dev;

        [DllImport("vbar.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vbar_connectDevice")]
        public static extern IntPtr VbarConnectDevice(long arg);

        [DllImport("vbar.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vbar_disconnectDevice")]
        public static extern void VbarDisconnectDevice(IntPtr dev);

        [DllImport("vbar.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vbar_backlight")]
        public static extern int VbarBacklight(IntPtr dev, bool bswitch);

        [DllImport("vbar.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vbar_interval")]
        public static extern int VbarInterval(IntPtr dev, int time);

        [DllImport("vbar.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vbar_beepControl")]
        public static extern int VbarBeepControl(IntPtr dev, byte times);

        [DllImport("vbar.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vbar_getResultStr")]
        public static extern int VbarGetResultStr(IntPtr dev, byte[] result_buffer, ref int result_size, ref int result_type);

        [DllImport("vbar.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vbar_addCodeFormat")]
        public static extern int VbarAddCodeFormat(IntPtr dev, byte codeFormat);

        public bool OpenDevice(int devnum)
        {
            this.dev = VGuangScannerAPI.VbarConnectDevice((long)devnum);
            return !(this.dev == IntPtr.Zero);
        }

        public void Backlight(bool bswitch)
        {
            IntPtr arg_06_0 = this.dev;
            VGuangScannerAPI.VbarBacklight(this.dev, bswitch);
        }

        public void Interval(int time)
        {
            IntPtr arg_06_0 = this.dev;
            VGuangScannerAPI.VbarInterval(this.dev, time);
        }

        public bool AddCodeFormat(byte codeFormat)
        {
            IntPtr arg_06_0 = this.dev;
            return VGuangScannerAPI.VbarAddCodeFormat(this.dev, codeFormat) == 0;
        }

        public void BeepControl(byte times)
        {
            IntPtr arg_06_0 = this.dev;
            VGuangScannerAPI.VbarBeepControl(this.dev, times);
        }

        public void DisConnected()
        {
            IntPtr arg_06_0 = this.dev;
            VGuangScannerAPI.VbarDisconnectDevice(this.dev);
            this.dev = IntPtr.Zero;
        }

        public bool GetResultStr(out byte[] result_buffer, out int result_size)
        {
            byte[] array = new byte[256];
            int num = 0;
            int num2 = 0;
            IntPtr arg_15_0 = this.dev;
            if (VGuangScannerAPI.VbarGetResultStr(this.dev, array, ref num, ref num2) == 0)
            {
                result_buffer = array;
                result_size = num;
                return true;
            }


            //System.Diagnostics.Debug.WriteLine(VGuangScannerAPI.VbarGetResultStr(this.dev, array, ref num, ref num2));
            //System.Diagnostics.Debug.WriteLine(array);
            //System.Diagnostics.Debug.WriteLine(num);
            //System.Diagnostics.Debug.WriteLine(num2);

            result_buffer = null;
            result_size = 0;
            return false;
        }
    }
}
