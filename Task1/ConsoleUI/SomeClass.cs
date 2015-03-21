using System;
using TimerLogic;

namespace ConsoleUI
{
    class SomeClass
    {
        public SomeClass(Timer timer)
        {
            timer.TimerAlert += doWork;
        }

        private void doWork(Object sender, EventArgs eventArgs)
        {
            Console.WriteLine("SomeClass doWork");
        }

        public void Unregister(Timer timer)
        {
            timer.TimerAlert -= doWork;
        }
    }
}
