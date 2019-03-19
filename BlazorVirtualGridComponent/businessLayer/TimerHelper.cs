using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BlazorVirtualGridComponent.businessLayer
{
    public static class TimerHelper
    {

        public static Action OnTick { get; set; }

        private static Timer timer1;

        private static bool IsRunning=false;

        public static void Start(int DueTime, int Period)
        {
            if (IsRunning)
            {
                Stop();
            }
            else
            {
                IsRunning = true;
                timer1 = new Timer(Timer1Callback, null, DueTime, Period);
                
            }

        }


        private static void Timer1Callback(object o)
        {
                OnTick?.Invoke();
          
        }


        public static void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                if (timer1 != null)
                {
                    timer1.Dispose();
                }
            }

        }

    }
}
