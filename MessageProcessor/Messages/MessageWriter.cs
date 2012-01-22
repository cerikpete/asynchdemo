using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MessageProcessor.Messages
{
    class MessageWriter
    {
        public void WriteMessage(string message, int sleepSeconds = 0)
        {
            if (sleepSeconds > 0)
                Thread.Sleep(sleepSeconds * 1000);

            Console.WriteLine(message);
        }
    }    
}
