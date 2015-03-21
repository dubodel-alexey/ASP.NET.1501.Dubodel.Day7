using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerLogic
{
    public sealed class TimerEventArgs : EventArgs
    {
        private int ms;

        public TimerEventArgs(int milliseconds)
        {
            ms = milliseconds;
        }

        public int TimeSpan { get { return ms; } }
    }
}
