using System.Threading;

namespace MessageProcessor.Messages
{
    class MessageGenerator
    {
        public string GenerateMessage(string messageSuffix, int sleepSeconds = 0)
        {
            if (sleepSeconds > 0)
                Thread.Sleep(sleepSeconds * 1000);
         
            return "Here's the message: " + messageSuffix;
        }
    }
}
