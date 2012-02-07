using System.Threading;
using Core;

namespace MessageProcessor.Messages
{
    class ConsoleWriter
    {
        public void WriteToConsole()
        {
            Thread.Sleep(5000);
            Log<ConsoleWriter>.Info("I'm writing to the console after a 5 second delay");
        }
    }
}