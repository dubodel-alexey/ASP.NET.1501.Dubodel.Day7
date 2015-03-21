using System;
using TimerLogic;

namespace ConsoleUI
{
    class AnotherClass
    {
        public AnotherClass(Timer timer)
        {
            timer.TimerAlert += doWork;
        }

        private void doWork(Object sender, EventArgs eventArgs)
        {
            Console.WriteLine("AnotherClass doWork");
        }

        public void Unregister(Timer timer)
        {
            timer.TimerAlert -= doWork;
        }
    }
}
