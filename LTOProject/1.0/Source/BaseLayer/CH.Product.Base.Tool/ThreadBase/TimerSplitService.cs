using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq
{

    /// <summary> 异步执行任务 </summary>
    public class TimerSplitService : BaseFactory<TimerSplitService>
    {

        /// <summary> 异步倒计时执行任务 </summary>
        /// <param name="count"> 倒计时的次数 </param>
        /// <param name="split"> 每次要做的任务 </param>
        /// <param name="todo"> 倒计时结束要做的任务 </param>
        /// <param name="splitTime"> 倒计时的间隔 单位毫秒 </param>
        public void CountDownSecond(int count = 5, Action<int> split = null, Action todo = null, int splitTime = 1000)
        {
            // Todo ：倒计时启动 
            Action act = () =>
            {
                for (int i = count; i > 0; i--)
                {
                    if (split != null)
                        split(i);
                    Thread.Sleep(splitTime);
                }

                if (todo != null)
                    todo();
            };

            Task t = new Task(act);
            t.Start();
        }


        /// <summary> 阻塞线程直到满足匹配 </summary>
        public void Wait(Func<bool> match, int splitTime = 1000, int? outTime = null)
        {
            int temp = 0;

            while (true)
            {
                if (match())
                {
                    return;
                }
                else
                {
                    Thread.Sleep(1000);
                    temp += 1000;


                    if (outTime != null && temp > outTime.Value)
                    {
                        throw new Exception("阻塞超时！");
                    }
                }
            }
        }

        /// <summary> 异步等待直到满足匹配 执行任务 </summary>
        public void WaitThread(Func<bool> match, Action action, Func<bool> stopMatch = null, int splitTime = 1000, int? outTime = null)
        {
            int temp = 0;

            Action act = () =>
              {
                  while (true)
                  {
                      if (stopMatch != null && stopMatch())
                      {
                          return;
                      }

                      if (match())
                      {
                          action();
                          return;
                      }
                      else
                      {
                          Thread.Sleep(1000);

                          temp += 1000;

                          if (outTime != null && temp > outTime.Value)
                          {
                              throw new Exception("阻塞超时！");
                          }
                      }
                  }

              };

            Task t = new Task(act);
            t.Start();
        }
    }
}
