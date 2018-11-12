using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
   /// <summary> 语音服务 </summary>
    public class SpeechService
    {
        public static SpeechService Instance = new SpeechService();

        SpeechSynthesizer hello = new SpeechSynthesizer();

        /// <summary> 输出语音 </summary>
        public void Speek(string str)
        {
            this.Stop();

            hello.SpeakAsync(str);
        }

        /// <summary> 停止播放语音 </summary>
        public void Stop()
        {
            hello.SpeakAsyncCancelAll();
        }
    }
}
