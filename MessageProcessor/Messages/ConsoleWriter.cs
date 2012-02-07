using System;
using System.Threading;

namespace MessageProcessor.Messages
{
    class ConsoleWriter
    {
        public void WriteToConsole()
        {
            Thread.Sleep(5000);
            Console.WriteLine("I'm writing to the console after a 5 second delay");
        }
    }
}