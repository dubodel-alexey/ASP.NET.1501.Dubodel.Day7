using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TimerLogic
{
    public sealed class Timer
    {
        public int Interval { get; set; }
        public event EventHandler<EventArgs> TimerAlert = delegate{ }; 
        
        public Timer(int milliseconds)
        {
            Interval = milliseconds;
        }

        private void OnTimerAlert(EventArgs e)
        {
            TimerAlert(this, e);
        }

        public void StartTimer()
        {
            Thread.Sleep(Interval);
            OnTimerAlert(new EventArgs());
        }
    }
}
