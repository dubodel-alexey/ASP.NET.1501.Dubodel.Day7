using System;
using TimerLogic;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new Timer(1000);
            var someClass = new SomeClass(timer);
            var anotherClass = new AnotherClass(timer);

            timer.StartTimer();
            Console.ReadKey();
        }
    }
}
